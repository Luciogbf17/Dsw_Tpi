using System;
using System.Collections.Generic;
using TpiDSW.Domain.Entities;
using TpiDSW.Domain.Interfaces;

namespace TpiDSW.Data.Sources;

public class DisponibilidadRepository : IDisponibilidadRepository
{
    private readonly List<Disponibilidad> _disponibilidades;

    public DisponibilidadRepository()
    {
        _disponibilidades = new List<Disponibilidad>();
    }

    public List<Disponibilidad> GetAll()
    {
        return _disponibilidades;
    }

    public Disponibilidad? GetById(Guid id)
    {
        return null;
    }

    public List<Disponibilidad> GetByMedicoId(Guid medicoId)
    {
        return new List<Disponibilidad>();
    }

    public List<Disponibilidad> GetByMedicoIdAndMes(Guid medicoId, int mes, int anio)
    {
        return new List<Disponibilidad>();
    }

    public void Add(Disponibilidad disponibilidad)
    {
        _disponibilidades.Add(disponibilidad);
    }

    public void Update(Disponibilidad disponibilidad)
    {
    }

    public void Delete(Guid id)
    {
    }
}