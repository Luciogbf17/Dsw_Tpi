using System;
using System.Collections.Generic;

namespace TpiDSW.Api.Models;

public class DoctorRequest
{
    public string Name { get; set; }
    public Guid SpecialtyId { get; set; }

    public DoctorRequest()
    {
        Name = string.Empty;
    }
}

public class DoctorSpecialtyResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public DoctorSpecialtyResponse()
    {
        Name = string.Empty;
    }
}

public class DoctorResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DoctorSpecialtyResponse Specialty { get; set; }

    public DoctorResponse()
    {
        Name = string.Empty;
        Specialty = new DoctorSpecialtyResponse();
    }
}

public class DoctorListResponse
{
    public List<DoctorResponse> Data { get; set; }
    public int Total { get; set; }

    public DoctorListResponse()
    {
        Data = new List<DoctorResponse>();
    }
}

public class AvailabilityResponse
{
    public string Day { get; set; }
    public string StartTime { get; set; }
    public string EndTime { get; set; }

    public AvailabilityResponse()
    {
        Day = string.Empty;
        StartTime = string.Empty;
        EndTime = string.Empty;
    }
}