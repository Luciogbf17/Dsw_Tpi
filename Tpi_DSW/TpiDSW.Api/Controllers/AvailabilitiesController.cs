using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TpiDSW.Api.Models;
using TpiDSW.Domain.Entities;
using TpiDSW.Domain.Exceptions;
using TpiDSW.Domain.Interfaces;

namespace TpiDSW.Api.Controllers;

[ApiController]
[Route("api/availabilities")]
public class AvailabilitiesController : ControllerBase
{
    private readonly IDisponibilidadRepository _disponibilidadRepository;
    private readonly ITurnoRepository _turnoRepository;
    private readonly IMedicoRepository _medicoRepository;

    public AvailabilitiesController(
        IDisponibilidadRepository disponibilidadRepository,
        ITurnoRepository turnoRepository,
        IMedicoRepository medicoRepository)
    {
        _disponibilidadRepository = disponibilidadRepository;
        _turnoRepository = turnoRepository;
        _medicoRepository = medicoRepository;
    }

    [HttpGet]
    public ActionResult<AvailabilityListResponse> GetByDoctorAndMonth(
        [FromQuery] Guid doctorId,
        [FromQuery] int month,
        [FromQuery] int year)
    {
        if (doctorId == Guid.Empty)
        {
            throw new ValidationException("El médico es obligatorio.");
        }

        List<Disponibilidad> disponibilidades = _disponibilidadRepository.GetByMedicoIdAndMes(doctorId, month, year);

        List<AvailabilityDetailResponse> data = disponibilidades
            .Select(MapToResponse)
            .ToList();

        return Ok(new AvailabilityListResponse
        {
            Data = data,
            Total = data.Count
        });
    }

    [HttpPost]
    public ActionResult<AvailabilityListResponse> Create([FromBody] AvailabilityRequest request)
    {
        ValidateRequest(request);

        Medico? medico = _medicoRepository.GetById(request.DoctorId);

        if (medico == null)
        {
            throw new ValidationException("El médico indicado no existe.");
        }

        List<AvailabilityDetailResponse> data = new List<AvailabilityDetailResponse>();

        foreach (AvailabilityDayRequest dayRequest in request.Days)
        {
            ValidateDayRequest(dayRequest);

            TimeOnly startTime = ParseTime(dayRequest.StartTime, "hora de inicio");
            TimeOnly endTime = ParseTime(dayRequest.EndTime, "hora de fin");

            ValidateTimeRange(startTime, endTime);

            bool existsOverlap = ExistsOverlap(
                request.DoctorId,
                dayRequest.Day,
                request.Month,
                request.Year,
                startTime,
                endTime);

            if (existsOverlap)
            {
                throw new ValidationException("Ya existe una disponibilidad superpuesta para el médico en ese día y horario.");
            }

            Disponibilidad disponibilidad = new Disponibilidad(
                request.DoctorId,
                request.Month,
                request.Year,
                dayRequest.Day,
                startTime,
                endTime);

            _disponibilidadRepository.Add(disponibilidad);

            List<Turno> turnos = GenerateTurns(disponibilidad);

            foreach (Turno turno in turnos)
            {
                _turnoRepository.Add(turno);
            }

            data.Add(MapToResponse(disponibilidad));
        }

        return Created("api/availabilities", new AvailabilityListResponse
        {
            Data = data,
            Total = data.Count
        });
    }

    private AvailabilityDetailResponse MapToResponse(Disponibilidad disponibilidad)
    {
        return new AvailabilityDetailResponse
        {
            Id = disponibilidad.Id,
            DoctorId = disponibilidad.MedicoId,
            Day = disponibilidad.Dia,
            Month = disponibilidad.Mes,
            Year = disponibilidad.Anio,
            StartTime = disponibilidad.TEntrada.ToString("HH:mm"),
            EndTime = disponibilidad.TSalida.ToString("HH:mm"),
            GeneratedTurns = _turnoRepository.GetByDisponibilidadId(disponibilidad.Id).Count
        };
    }

    private static void ValidateRequest(AvailabilityRequest request)
    {
        if (request == null)
        {
            throw new ValidationException("La solicitud no puede estar vacía.");
        }

        if (request.DoctorId == Guid.Empty)
        {
            throw new ValidationException("El médico es obligatorio.");
        }

        if (request.Month < 1 || request.Month > 12)
        {
            throw new ValidationException("El mes debe estar entre 1 y 12.");
        }

        if (request.Year < DateTime.Now.Year)
        {
            throw new ValidationException("El año no puede ser anterior al actual.");
        }

        if (request.Days == null || request.Days.Count == 0)
        {
            throw new ValidationException("Debe indicar al menos un día de atención.");
        }
    }

    private static void ValidateDayRequest(AvailabilityDayRequest dayRequest)
    {
        if (dayRequest.Day < 1 || dayRequest.Day > 7)
        {
            throw new ValidationException("El día debe estar entre 1 y 7. Lunes = 1, Domingo = 7.");
        }

        if (string.IsNullOrWhiteSpace(dayRequest.StartTime))
        {
            throw new ValidationException("La hora de inicio es obligatoria.");
        }

        if (string.IsNullOrWhiteSpace(dayRequest.EndTime))
        {
            throw new ValidationException("La hora de fin es obligatoria.");
        }
    }

    private static TimeOnly ParseTime(string value, string fieldName)
    {
        bool isValid = TimeOnly.TryParseExact(
            value.Trim(),
            "HH:mm",
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out TimeOnly time);

        if (!isValid)
        {
            throw new ValidationException($"La {fieldName} debe tener formato HH:mm.");
        }

        return time;
    }

    private static void ValidateTimeRange(TimeOnly startTime, TimeOnly endTime)
    {
        if (startTime >= endTime)
        {
            throw new ValidationException("La hora de inicio debe ser menor que la hora de fin.");
        }

        TimeSpan duration = endTime.ToTimeSpan() - startTime.ToTimeSpan();

        if (duration.TotalMinutes < 30)
        {
            throw new ValidationException("La disponibilidad debe durar al menos 30 minutos.");
        }

        if (duration.TotalMinutes % 30 != 0)
        {
            throw new ValidationException("La duración de la disponibilidad debe ser múltiplo de 30 minutos.");
        }
    }

    private bool ExistsOverlap(Guid doctorId, int day, int month, int year, TimeOnly startTime, TimeOnly endTime)
    {
        List<Disponibilidad> disponibilidades = _disponibilidadRepository.GetByMedicoIdAndMes(doctorId, month, year);

        return disponibilidades.Any(disponibilidad =>
            disponibilidad.Dia == day &&
            startTime < disponibilidad.TSalida &&
            endTime > disponibilidad.TEntrada);
    }

    private static List<Turno> GenerateTurns(Disponibilidad disponibilidad)
    {
        List<Turno> turnos = new List<Turno>();
        List<DateOnly> dates = GetDatesByDay(disponibilidad.Mes, disponibilidad.Anio, disponibilidad.Dia);

        foreach (DateOnly date in dates)
        {
            TimeOnly startTime = disponibilidad.TEntrada;

            while (startTime.AddMinutes(30) <= disponibilidad.TSalida)
            {
                TimeOnly endTime = startTime.AddMinutes(30);

                Turno turno = new Turno(
                    disponibilidad.MedicoId,
                    disponibilidad.Id,
                    date,
                    startTime,
                    endTime);

                turnos.Add(turno);

                startTime = endTime;
            }
        }

        return turnos;
    }

    private static List<DateOnly> GetDatesByDay(int month, int year, int day)
    {
        List<DateOnly> dates = new List<DateOnly>();
        int daysInMonth = DateTime.DaysInMonth(year, month);

        for (int dayNumber = 1; dayNumber <= daysInMonth; dayNumber++)
        {
            DateOnly date = new DateOnly(year, month, dayNumber);

            if (NormalizeDayOfWeek(date.DayOfWeek) == day)
            {
                dates.Add(date);
            }
        }

        return dates;
    }

    private static int NormalizeDayOfWeek(DayOfWeek dayOfWeek)
    {
        return dayOfWeek switch
        {
            DayOfWeek.Monday => 1,
            DayOfWeek.Tuesday => 2,
            DayOfWeek.Wednesday => 3,
            DayOfWeek.Thursday => 4,
            DayOfWeek.Friday => 5,
            DayOfWeek.Saturday => 6,
            DayOfWeek.Sunday => 7,
            _ => 0
        };
    }
}
