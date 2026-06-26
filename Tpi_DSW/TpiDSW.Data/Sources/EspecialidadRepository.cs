using System;
using System.Collections.Generic;
using System.Linq;
using TpiDSW.Domain.Entities;
using TpiDSW.Domain.Interfaces;

namespace TpiDSW.Data.Sources;

public class EspecialidadRepository : IEspecialidadRepository
{
    private readonly List<Especialidad> _especialidades;

    public EspecialidadRepository()
    {
        _especialidades = new List<Especialidad>();
    }

    public List<Especialidad> GetAll()
    {
        return _especialidades
            .Where(especialidad => !especialidad.Deleted)
            .ToList();
    }

    public Especialidad? GetById(Guid id)
    {
        return _especialidades
            .FirstOrDefault(especialidad => especialidad.Id == id && !especialidad.Deleted);
    }

    public List<Especialidad> GetByNombre(string nombre)
    {
        return _especialidades
            .Where(especialidad =>
                !especialidad.Deleted &&
                especialidad.Nombre.Contains(nombre, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public void Add(Especialidad especialidad)
    {
        _especialidades.Add(especialidad);
    }

    public void Update(Especialidad especialidad)
    {
        Especialidad? especialidadExistente = GetById(especialidad.Id);

        if (especialidadExistente == null)
        {
            return;
        }

        especialidadExistente.Nombre = especialidad.Nombre;
        especialidadExistente.Descripcion = especialidad.Descripcion;
    }

    public void Delete(Guid id)
    {
        Especialidad? especialidad = GetById(id);

        if (especialidad == null)
        {
            return;
        }

        especialidad.Deleted = true;
    }
}