using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TpiDSW.Api.Models.Auth;
using TpiDSW.Domain.Constants;
using TpiDSW.Domain.Entities;
using TpiDSW.Domain.Interfaces;

namespace TpiDSW.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAdministradorRepository _administradorRepository;
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IConfiguration _configuration;

        public AuthController(
            IAdministradorRepository administradorRepository,
            IPacienteRepository pacienteRepository,
            IConfiguration configuration)
        {
            _administradorRepository = administradorRepository;
            _pacienteRepository = pacienteRepository;
            _configuration = configuration;
        }

        [HttpPost("admin/login")]
        public ActionResult<LoginResponse> LoginAdmin([FromBody] AdminLoginRequest request)
        {
            if (request == null)
            {
                return BadRequest("La solicitud no puede estar vacía.");
            }

            if (string.IsNullOrWhiteSpace(request.Email))
            {
                return BadRequest("El email es obligatorio.");
            }

            if (string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest("La contraseña es obligatoria.");
            }

            if (request.Password.Length < 8)
            {
                return BadRequest("La contraseña debe tener al menos 8 caracteres.");
            }

            Administrador? administrador = _administradorRepository.GetByEmail(request.Email);

            if (administrador == null)
            {
                return Unauthorized("Credenciales inválidas.");
            }

            string passwordHash = GenerateSha256Hash(request.Password);

            if (administrador.PasswordHash != passwordHash)
            {
                return Unauthorized("Credenciales inválidas.");
            }

            string token = GenerateJwtToken(
                administrador.Id,
                administrador.Email,
                UserRoles.ADMINISTRADOR
            );

            LoginResponse response = new LoginResponse
            {
                Token = token,
                Role = UserRoles.ADMINISTRADOR
            };

            return Ok(response);
        }

        [HttpPost("patient/login")]
        public ActionResult<LoginResponse> LoginPatient([FromBody] PatientLoginRequest request)
        {
            if (request == null)
            {
                return BadRequest("La solicitud no puede estar vacía.");
            }

            if (string.IsNullOrWhiteSpace(request.Email))
            {
                return BadRequest("El email es obligatorio.");
            }

            if (request.Dni < 1000000 || request.Dni > 99999999)
            {
                return BadRequest("El DNI debe tener 7 u 8 dígitos.");
            }

            Paciente? paciente = _pacienteRepository.GetByDni(request.Dni);

            if (paciente == null)
            {
                paciente = new Paciente(request.Dni, string.Empty, string.Empty);
                _pacienteRepository.Add(paciente);
            }

            string token = GenerateJwtToken(
                paciente.Id,
                request.Email,
                UserRoles.PACIENTE
            );

            LoginResponse response = new LoginResponse
            {
                Token = token,
                Role = UserRoles.PACIENTE
            };

            return Ok(response);
        }

        private string GenerateJwtToken(Guid userId, string email, string role)
        {
            string jwtKey = _configuration["Jwt:Key"]
                ?? throw new Exception("No se configuró Jwt:Key.");

            string jwtIssuer = _configuration["Jwt:Issuer"]
                ?? throw new Exception("No se configuró Jwt:Issuer.");

            string jwtAudience = _configuration["Jwt:Audience"]
                ?? throw new Exception("No se configuró Jwt:Audience.");

            int expirationMinutes = int.Parse(
                _configuration["Jwt:ExpirationMinutes"] ?? "60"
            );

            Claim[] claims =
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role)
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey)
            );

            SigningCredentials credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256
            );

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(expirationMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateSha256Hash(string text)
        {
            byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(text));

            StringBuilder builder = new StringBuilder();

            foreach (byte item in bytes)
            {
                builder.Append(item.ToString("x2"));
            }

            return builder.ToString();
        }
    }
}
