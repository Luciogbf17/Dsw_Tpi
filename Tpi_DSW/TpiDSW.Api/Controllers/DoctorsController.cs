using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TpiDSW.Api.Models;
using TpiDSW.Domain.Entities;
using TpiDSW.Domain.Exceptions;
using TpiDSW.Domain.Interfaces;

namespace TpiDSW.Api.Controllers;

[ApiController]
[Route("api/doctors")]
public class DoctorsController : ControllerBase
{
    private readonly IMedicoRepository _medicoRepository;
    private readonly IEspecialidadRepository _especialidadRepository;
    private readonly IDisponibilidadRepository _disponibilidadRepository;

    public DoctorsController(
        IMedicoRepository medicoRepository,
        IEspecialidadRepository especialidadRepository,
        IDisponibilidadRepository disponibilidadRepository)
    {
        _medicoRepository = medicoRepository;
        _especialidadRepository = especialidadRepository;
        _disponibilidadRepository = disponibilidadRepository;
    }

    [HttpGet]
    public ActionResult<DoctorListResponse> GetAll([FromQuery] string? name)
    {
        List<Medico> medicos;

        if (string.IsNullOrWhiteSpace(name))
        {
            medicos = _medicoRepository.GetAll();
        }
        else
        {
            ValidateNameFilter(name);
            medicos = _medicoRepository.GetByNombre(name.Trim());
        }

        List<DoctorResponse> data = medicos
            .Select(MapToResponse)
            .ToList();

        return Ok(new DoctorListResponse
        {
            Data = data,
            Total = data.Count
        });
    }

    [HttpPost]
    public ActionResult<DoctorResponse> Create([FromBody] DoctorRequest request)
    {
        ValidateRequest(request);

        Especialidad? especialidad = _especialidadRepository.GetById(request.SpecialtyId);

        if (especialidad == null)
        {
            throw new ValidationException("La especialidad indicada no existe.");
        }

        Medico medico = new Medico(
            request.Name.Trim(),
            especialidad.Id,
            especialidad
        );

        _medicoRepository.Add(medico);

        DoctorResponse response = MapToResponse(medico);

        return Created($"api/doctors/{response.Id}", response);
    }

    [HttpPut("{id}")]
    public ActionResult<DoctorResponse> Update(Guid id, [FromBody] DoctorRequest request)
    {
        ValidateRequest(request);

        Medico? medico = _medicoRepository.GetById(id);

        if (medico == null)
        {
            return NotFound(new
            {
                errorCode = "DOCTOR_NOT_FOUND",
                message = "El médico no existe."
            });
        }

        Especialidad? especialidad = _especialidadRepository.GetById(request.SpecialtyId);

        if (especialidad == null)
        {
            throw new ValidationException("La especialidad indicada no existe.");
        }

        medico.Nombre = request.Name.Trim();
        medico.EspecialidadId = especialidad.Id;
        medico.Especialidad = especialidad;

        _medicoRepository.Update(medico);

        return Ok(MapToResponse(medico));
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        Medico? medico = _medicoRepository.GetById(id);

        if (medico == null)
        {
            return NotFound(new
            {
                errorCode = "DOCTOR_NOT_FOUND",
                message = "El médico no existe."
            });
        }

        _medicoRepository.Delete(id);

        return NoContent();
    }

    [HttpGet("{id}/availabilities")]
    public ActionResult<List<AvailabilityResponse>> GetAvailabilities(Guid id)
    {
        Medico? medico = _medicoRepository.GetById(id);

        if (medico == null)
        {
            return NotFound(new
            {
                errorCode = "DOCTOR_NOT_FOUND",
                message = "El médico no existe."
            });
        }

        List<Disponibilidad> disponibilidades = _disponibilidadRepository.GetByMedicoId(id);

        List<AvailabilityResponse> response = disponibilidades
            .Select(disponibilidad => new AvailabilityResponse
            {
                Day = disponibilidad.Dia.ToString(),
                StartTime = disponibilidad.TEntrada.ToString("HH:mm"),
                EndTime = disponibilidad.TSalida.ToString("HH:mm")
            })
            .ToList();

        return Ok(response);
    }

    private DoctorResponse MapToResponse(Medico medico)
    {
        Especialidad? especialidad = medico.Especialidad;

        if (especialidad == null)
        {
            especialidad = _especialidadRepository.GetById(medico.EspecialidadId);
        }

        return new DoctorResponse
        {
            Id = medico.Id,
            Name = medico.Nombre,
            Specialty = new DoctorSpecialtyResponse
            {
                Id = especialidad?.Id ?? Guid.Empty,
                Name = especialidad?.Nombre ?? string.Empty
            }
        };
    }

    private static void ValidateRequest(DoctorRequest request)
    {
        if (request == null)
        {
            throw new ValidationException("La solicitud no puede estar vacía.");
        }

        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new ValidationException("El nombre es obligatorio.");
        }

        if (request.Name.Trim().Length < 3 || request.Name.Trim().Length > 100)
        {
            throw new ValidationException("El nombre debe tener entre 3 y 100 caracteres.");
        }

        if (request.SpecialtyId == Guid.Empty)
        {
            throw new ValidationException("La especialidad es obligatoria.");
        }
    }

    private static void ValidateNameFilter(string name)
    {
        if (name.Trim().Length < 3 || name.Trim().Length > 100)
        {
            throw new ValidationException("El filtro name debe tener entre 3 y 100 caracteres.");
        }
    }
}