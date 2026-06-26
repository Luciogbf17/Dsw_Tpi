using System;
using System.Collections.Generic;

namespace TpiDSW.Api.Models;

public class AvailabilityDayRequest
{
    public int Day { get; set; }
    public string StartTime { get; set; }
    public string EndTime { get; set; }

    public AvailabilityDayRequest()
    {
        StartTime = string.Empty;
        EndTime = string.Empty;
    }
}

public class AvailabilityRequest
{
    public Guid DoctorId { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public List<AvailabilityDayRequest> Days { get; set; }

    public AvailabilityRequest()
    {
        Days = new List<AvailabilityDayRequest>();
    }
}

public class AvailabilityDetailResponse
{
    public Guid Id { get; set; }
    public Guid DoctorId { get; set; }
    public int Day { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public string StartTime { get; set; }
    public string EndTime { get; set; }
    public int GeneratedTurns { get; set; }

    public AvailabilityDetailResponse()
    {
        StartTime = string.Empty;
        EndTime = string.Empty;
    }
}

public class AvailabilityListResponse
{
    public List<AvailabilityDetailResponse> Data { get; set; }
    public int Total { get; set; }

    public AvailabilityListResponse()
    {
        Data = new List<AvailabilityDetailResponse>();
    }
}
