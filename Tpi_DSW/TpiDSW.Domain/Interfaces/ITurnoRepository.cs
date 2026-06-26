using System;
using System.Collections.Generic;
using TpiDSW.Domain.Entities;

namespace TpiDSW.Domain.Interfaces;

public interface ITurnoRepository
{
    List<Turno> GetAll();

    Turno? GetById(Guid id);

    void Add(Turno turno);

    void Update(Turno turno);

    void Delete(Guid id);
}