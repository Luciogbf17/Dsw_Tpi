using System;
using System.Collections.Generic;
using System.Linq;
using TpiDSW.Domain.Entities;
using TpiDSW.Domain.Interfaces;

namespace TpiDSW.Data.Sources;

public class PacienteRepository : IPacienteRepository
{
    private readonly List<Paciente> _pacientes;

    public PacienteRepository()
    {
        _pacientes = new List<Paciente>();
    }

    public List<Paciente> GetAll()
    {
        return _pacientes
            .Where(paciente => !paciente.Deleted)
            .ToList();
    }

    public Paciente? GetById(Guid id)
    {
        return _pacientes
            .FirstOrDefault(paciente => paciente.Id == id && !paciente.Deleted);
    }

    public Paciente? GetByDni(int dni)
    {
        return _pacientes
            .FirstOrDefault(paciente => paciente.Dni == dni && !paciente.Deleted);
    }

    public void Add(Paciente paciente)
    {
        _pacientes.Add(paciente);
    }

    public void Update(Paciente paciente)
    {
        Paciente? pacienteExistente = GetById(paciente.Id);

        if (pacienteExistente == null)
        {
            return;
        }

        pacienteExistente.Dni = paciente.Dni;
        pacienteExistente.Nombre = paciente.Nombre;
        pacienteExistente.Telefono = paciente.Telefono;
    }

    public void Delete(Guid id)
    {
        Paciente? paciente = GetById(id);

        if (paciente == null)
        {
            return;
        }

        paciente.Deleted = true;
    }
}