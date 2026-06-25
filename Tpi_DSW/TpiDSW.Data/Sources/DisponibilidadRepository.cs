using System;
using System.Collections.Generic;
using System.Text;
using TpiDSW.Domain.Interfaces;
using TpiDSW.Domain.Entities;

namespace TpiDSW.Data.Sources
{
    public class DisponibilidadRepository : IDisponibilidadRepository
    {
        public void Add(Disponibilidad disponibilidad)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Disponibilidad> GetAll()
        {
            throw new NotImplementedException();
        }

        public Disponibilidad? GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Disponibilidad> GetByMedicoId(Guid medicoId)
        {
            throw new NotImplementedException();
        }

        public List<Disponibilidad> GetByMedicoIdAndMes(Guid medicoId, int mes, int anio)
        {
            throw new NotImplementedException();
        }

        public void Update(Disponibilidad disponibilidad)
        {
            throw new NotImplementedException();
        }
    }
}
