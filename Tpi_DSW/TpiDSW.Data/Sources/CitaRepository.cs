using System;
using System.Collections.Generic;
using System.Text;
using TpiDSW.Domain.Interfaces;
using TpiDSW.Domain.Entities;


namespace TpiDSW.Data.Sources
{
    public class CitaRepository : ICitaRepository 
    {
        public void Add(Cita cita)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Cita> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<Cita> GetByFecha(DateTime fecha)
        {
            throw new NotImplementedException();
        }

        public Cita? GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Cita> GetByPacienteId(Guid pacienteId)
        {
            throw new NotImplementedException();
        }

        public void Update(Cita cita)
        {
            throw new NotImplementedException();
        }
    }
}
