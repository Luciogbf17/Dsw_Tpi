using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TpiDSW.Api.Models;
using TpiDSW.Domain.Entities;
using TpiDSW.Domain.Enum;
using TpiDSW.Domain.Exceptions;
using TpiDSW.Domain.Interfaces;

namespace TpiDSW.Api.Controllers;

[ApiController]
[Route("api/appointments")]
public class AppointmentsController : ControllerBase
{
    private readonly ICitaRepository _citaRepository;
    private readonly ITurnoRepository _turnoRepository;
    private readonly IPacienteRepository _pacienteRepository;

    public AppointmentsController(
        ICitaRepository citaRepository,
        ITurnoRepository turnoRepository,
        IPacienteRepository pacienteRepository)
    {
        _citaRepository = citaRepository;
        _turnoRepository = turnoRepository;
        _pacienteRepository = pacienteRepository;
    }

    [HttpGet]
    public ActionResult<AppointmentListResponse> GetByDate([FromQuery] DateTime date)
    {
        List<AppointmentResponse> data = _citaRepository.GetByFecha(date)
            .Select(MapToResponse)
            .ToList();

        return Ok(new AppointmentListResponse
        {
            Data = data,
            Total = data.Count
        });
    }

    [HttpGet("patient/{patientId}")]
    public ActionResult<AppointmentListResponse> GetByPatient(Guid patientId)
    {
        List<AppointmentResponse> data = _citaRepository.GetByPacienteId(patientId)
            .Select(MapToResponse)
            .ToList();

        return Ok(new AppointmentListResponse
        {
            Data = data,
            Total = data.Count
        });
    }

    [HttpGet("turns")]
    public ActionResult<TurnListResponse> GetTurnsByDate([FromQuery] DateOnly date)
    {
        List<TurnResponse> data = _turnoRepository.GetByFecha(date)
            .Select(MapToResponse)
            .ToList();

        return Ok(new TurnListResponse
        {
            Data = data,
            Total = data.Count
        });
    }

    [HttpPost]
    public ActionResult<AppointmentResponse> Create([FromBody] AppointmentRequest request)
    {
        ValidateRequest(request);

        Turno? turno = _turnoRepository.GetById(request.TurnId);

        if (turno == null)
        {
            throw new ValidationException("El turno indicado no existe.");
        }

        Paciente? paciente = _pacienteRepository.GetById(request.PatientId);

        if (paciente == null)
        {
            throw new ValidationException("El paciente indicado no existe.");
        }

        if (turno.Fecha < DateOnly.FromDateTime(DateTime.Today))
        {
            throw new ValidationException("No se puede reservar un turno pasado.");
        }

        if (turno.Estado != EstadoTurno.Disponible)
        {
            throw new ValidationException("El turno no está disponible.");
        }

        bool alreadyBooked = _citaRepository.GetAll().Any(cita =>
            cita.TurnoId == turno.Id &&
            !cita.Deleted &&
            cita.Estado == CitaEstado.Confirmada);

        if (alreadyBooked)
        {
            throw new ValidationException("El turno ya tiene una cita confirmada.");
        }

        DateTime careDate = turno.Fecha.ToDateTime(turno.HoraInicio);

        Cita cita = new Cita(
            turno.Id,
            paciente.Id,
            careDate,
            request.Reason.Trim());

        turno.Estado = EstadoTurno.Reservado;

        _citaRepository.Add(cita);
        _turnoRepository.Update(turno);

        AppointmentResponse response = MapToResponse(cita);

        return Created($"api/appointments/{response.Id}", response);
    }

    [HttpDelete("{id}")]
    public IActionResult Cancel(Guid id)
    {
        Cita? cita = _citaRepository.GetById(id);

        if (cita == null)
        {
            return NotFound(new
            {
                errorCode = "APPOINTMENT_NOT_FOUND",
                message = "La cita no existe."
            });
        }

        if (cita.Estado == CitaEstado.Cancelada)
        {
            throw new ValidationException("La cita ya se encuentra cancelada.");
        }

        Turno? turno = _turnoRepository.GetById(cita.TurnoId);

        cita.Estado = CitaEstado.Cancelada;
        cita.FechaDeCancelacion = DateTime.Now;
        cita.Deleted = true;

        if (turno != null)
        {
            turno.Estado = EstadoTurno.Disponible;
            _turnoRepository.Update(turno);
        }

        _citaRepository.Update(cita);

        return NoContent();
    }

    private static AppointmentResponse MapToResponse(Cita cita)
    {
        return new AppointmentResponse
        {
            Id = cita.Id,
            TurnId = cita.TurnoId,
            PatientId = cita.PacienteId,
            CareDate = cita.FechaDeAtencion,
            CancellationDate = cita.FechaDeCancelacion,
            Status = cita.Estado.ToString(),
            Reason = cita.Motivo
        };
    }

    private static TurnResponse MapToResponse(Turno turno)
    {
        return new TurnResponse
        {
            Id = turno.Id,
            DoctorId = turno.MedicoId,
            AvailabilityId = turno.DisponibilidadId,
            Date = turno.Fecha,
            StartTime = turno.HoraInicio.ToString("HH:mm"),
            EndTime = turno.HoraFin.ToString("HH:mm"),
            Status = turno.Estado.ToString()
        };
    }

    private static void ValidateRequest(AppointmentRequest request)
    {
        if (request == null)
        {
            throw new ValidationException("La solicitud no puede estar vacía.");
        }

        if (request.TurnId == Guid.Empty)
        {
            throw new ValidationException("El turno es obligatorio.");
        }

        if (request.PatientId == Guid.Empty)
        {
            throw new ValidationException("El paciente es obligatorio.");
        }

        if (string.IsNullOrWhiteSpace(request.Reason))
        {
            throw new ValidationException("El motivo de la cita es obligatorio.");
        }

        if (request.Reason.Trim().Length < 3 || request.Reason.Trim().Length > 200)
        {
            throw new ValidationException("El motivo debe tener entre 3 y 200 caracteres.");
        }
    }
}
