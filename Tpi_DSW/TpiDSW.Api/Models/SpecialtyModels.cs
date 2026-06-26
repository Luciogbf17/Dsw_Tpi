using System;
using System.Collections.Generic;

namespace TpiDSW.Api.Models;

public class SpecialtyRequest
{
    public string Name { get; set; }
    public string Description { get; set; }

    public SpecialtyRequest()
    {
        Name = string.Empty;
        Description = string.Empty;
    }
}

public class SpecialtyResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public SpecialtyResponse()
    {
        Name = string.Empty;
        Description = string.Empty;
    }
}

public class SpecialtyListResponse
{
    public List<SpecialtyResponse> Data { get; set; }
    public int Total { get; set; }

    public SpecialtyListResponse()
    {
        Data = new List<SpecialtyResponse>();
    }
}