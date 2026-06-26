using System;
using System.Collections.Generic;
using System.Linq;
using TpiDSW.Domain.Entities;
using TpiDSW.Domain.Interfaces;

namespace TpiDSW.Data.Sources;

public class MedicoRepository : IMedicoRepository
{
    private readonly List<Medico> _medicos;

    public MedicoRepository()
    {
        _medicos = new List<Medico>();
    }

    public List<Medico> GetAll()
    {
        return _medicos
            .Where(medico => !medico.Deleted)
            .ToList();
    }

    public Medico? GetById(Guid id)
    {
        return _medicos
            .FirstOrDefault(medico => medico.Id == id && !medico.Deleted);
    }

    public List<Medico> GetByNombre(string nombre)
    {
        return _medicos
            .Where(medico =>
                !medico.Deleted &&
                medico.Nombre.Contains(nombre, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public void Add(Medico medico)
    {
        _medicos.Add(medico);
    }

    public void Update(Medico medico)
    {
        Medico? medicoExistente = GetById(medico.Id);

        if (medicoExistente == null)
        {
            return;
        }

        medicoExistente.Nombre = medico.Nombre;
        medicoExistente.Matricula = medico.Matricula;
        medicoExistente.EspecialidadId = medico.EspecialidadId;
        medicoExistente.Especialidad = medico.Especialidad;
    }

    public void Delete(Guid id)
    {
        Medico? medico = GetById(id);

        if (medico == null)
        {
            return;
        }

        medico.Deleted = true;
    }
}