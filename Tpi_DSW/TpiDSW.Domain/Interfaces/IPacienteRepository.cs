using System;
using System.Collections.Generic;
using TpiDSW.Domain.Entities;

namespace TpiDSW.Domain.Interfaces;

public interface IPacienteRepository
{
    List<Paciente> GetAll();

    Paciente? GetById(Guid id);

    Paciente? GetByDni(int dni);

    void Add(Paciente paciente);

    void Update(Paciente paciente);

    void Delete(Guid id);
}