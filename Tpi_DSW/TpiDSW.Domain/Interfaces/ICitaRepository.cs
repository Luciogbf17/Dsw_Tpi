using System;
using System.Collections.Generic;
using TpiDSW.Domain.Entities;

namespace TpiDSW.Domain.Interfaces;

public interface ICitaRepository
{
    List<Cita> GetAll();

    Cita? GetById(Guid id);

    void Add(Cita cita);

    void Update(Cita cita);

    void Delete(Guid id);
}