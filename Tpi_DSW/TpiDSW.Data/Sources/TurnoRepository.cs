using System;
using System.Collections.Generic;
using System.Text;
using TpiDSW.Domain.Interfaces;
using TpiDSW.Domain.Entities;

namespace TpiDSW.Data.Sources
{
    public class TurnoRepository : ITurnoRepository
    {
        public void Add(Turno turno)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Turno> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<Turno> GetByDisponibilidadId(Guid disponibilidadId)
        {
            throw new NotImplementedException();
        }

        public List<Turno> GetByFecha(DateOnly fecha)
        {
            throw new NotImplementedException();
        }

        public Turno? GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(Turno turno)
        {
            throw new NotImplementedException();
        }
    }
}
