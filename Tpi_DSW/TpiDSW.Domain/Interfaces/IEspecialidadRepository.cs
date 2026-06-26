using System;
using System.Collections.Generic;
using TpiDSW.Domain.Entities;

namespace TpiDSW.Domain.Interfaces;

public interface IEspecialidadRepository
{
    List<Especialidad> GetAll();

    Especialidad? GetById(Guid id);

    List<Especialidad> GetByNombre(string nombre);

    void Add(Especialidad especialidad);

    void Update(Especialidad especialidad);

    void Delete(Guid id);
}