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
[Route("api/specialties")]
public class SpecialtiesController : ControllerBase
{
    private readonly IEspecialidadRepository _especialidadRepository;

    public SpecialtiesController(IEspecialidadRepository especialidadRepository)
    {
        _especialidadRepository = especialidadRepository;
    }

    [HttpGet]
    public ActionResult<SpecialtyListResponse> GetAll([FromQuery] string? name)
    {
        List<Especialidad> especialidades;

        if (string.IsNullOrWhiteSpace(name))
        {
            especialidades = _especialidadRepository.GetAll();
        }
        else
        {
            ValidateNameFilter(name);
            especialidades = _especialidadRepository.GetByNombre(name.Trim());
        }

        List<SpecialtyResponse> data = especialidades
            .Select(MapToResponse)
            .ToList();

        return Ok(new SpecialtyListResponse
        {
            Data = data,
            Total = data.Count
        });
    }

    [HttpPost]
    public ActionResult<SpecialtyResponse> Create([FromBody] SpecialtyRequest request)
    {
        ValidateRequest(request);

        Especialidad especialidad = new Especialidad(
            request.Name.Trim(),
            request.Description.Trim()
        );

        _especialidadRepository.Add(especialidad);

        SpecialtyResponse response = MapToResponse(especialidad);

        return Created($"api/specialties/{response.Id}", response);
    }

    [HttpPut("{id}")]
    public ActionResult<SpecialtyResponse> Update(Guid id, [FromBody] SpecialtyRequest request)
    {
        ValidateRequest(request);

        Especialidad? especialidad = _especialidadRepository.GetById(id);

        if (especialidad == null)
        {
            return NotFound(new
            {
                errorCode = "SPECIALTY_NOT_FOUND",
                message = "La especialidad no existe."
            });
        }

        especialidad.Nombre = request.Name.Trim();
        especialidad.Descripcion = request.Description.Trim();

        _especialidadRepository.Update(especialidad);

        return Ok(MapToResponse(especialidad));
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        Especialidad? especialidad = _especialidadRepository.GetById(id);

        if (especialidad == null)
        {
            return NotFound(new
            {
                errorCode = "SPECIALTY_NOT_FOUND",
                message = "La especialidad no existe."
            });
        }

        _especialidadRepository.Delete(id);

        return NoContent();
    }

    private static SpecialtyResponse MapToResponse(Especialidad especialidad)
    {
        return new SpecialtyResponse
        {
            Id = especialidad.Id,
            Name = especialidad.Nombre,
            Description = especialidad.Descripcion
        };
    }

    private static void ValidateRequest(SpecialtyRequest request)
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

        if (string.IsNullOrWhiteSpace(request.Description))
        {
            throw new ValidationException("La descripción es obligatoria.");
        }

        if (request.Description.Trim().Length < 10 || request.Description.Trim().Length > 100)
        {
            throw new ValidationException("La descripción debe tener entre 10 y 100 caracteres.");
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