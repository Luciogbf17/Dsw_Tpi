using System;

using MedicalAppointments.Domain.Entities;

namespace TpiDSW.Domain.Interfaces;
{
    public interface ICitaRepository
    {
        List<Cita> GetAll();

        Cita? GetById(Guid id);

        List<Cita> GetByPacienteId(Guid pacienteId);

        List<Cita> GetByFecha(DateTime fecha);

        void Add(Cita cita);

        void Update(Cita cita);

        void Delete(Guid id);
    }
}