using System;
using System.Collections.Generic;

namespace TpiDSW.Api.Models;

public class AppointmentRequest
{
    public Guid TurnId { get; set; }
    public Guid PatientId { get; set; }
    public string Reason { get; set; }

    public AppointmentRequest()
    {
        Reason = string.Empty;
    }
}

public class AppointmentResponse
{
    public Guid Id { get; set; }
    public Guid TurnId { get; set; }
    public Guid PatientId { get; set; }
    public DateTime CareDate { get; set; }
    public DateTime? CancellationDate { get; set; }
    public string Status { get; set; }
    public string Reason { get; set; }

    public AppointmentResponse()
    {
        Status = string.Empty;
        Reason = string.Empty;
    }
}

public class AppointmentListResponse
{
    public List<AppointmentResponse> Data { get; set; }
    public int Total { get; set; }

    public AppointmentListResponse()
    {
        Data = new List<AppointmentResponse>();
    }
}

public class TurnResponse
{
    public Guid Id { get; set; }
    public Guid DoctorId { get; set; }
    public Guid AvailabilityId { get; set; }
    public DateOnly Date { get; set; }
    public string StartTime { get; set; }
    public string EndTime { get; set; }
    public string Status { get; set; }

    public TurnResponse()
    {
        StartTime = string.Empty;
        EndTime = string.Empty;
        Status = string.Empty;
    }
}

public class TurnListResponse
{
    public List<TurnResponse> Data { get; set; }
    public int Total { get; set; }

    public TurnListResponse()
    {
        Data = new List<TurnResponse>();
    }
}
