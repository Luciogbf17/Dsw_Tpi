using System;
using System.Collections.Generic;
using TpiDSW.Domain.Entities;

namespace TpiDSW.Domain.Interfaces;

public interface IMedicoRepository
{
    List<Medico> GetAll();

    Medico? GetById(Guid id);

    List<Medico> GetByNombre(string nombre);

    void Add(Medico medico);

    void Update(Medico medico);

    void Delete(Guid id);
}