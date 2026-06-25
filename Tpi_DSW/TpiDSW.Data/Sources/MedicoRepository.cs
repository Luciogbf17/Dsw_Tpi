using System;
using System.Collections.Generic;
using System.Text;
using TpiDSW.Domain.Interfaces;
using TpiDSW.Domain.Entities;

namespace TpiDSW.Data.Sources
{
    public class MedicoRepository : IMedicoRepository
    {
        public void Add(Medico medico)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Medico> GetAll()
        {
            throw new NotImplementedException();
        }

        public Medico? GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Medico> GetByNombre(string nombre)
        {
            throw new NotImplementedException();
        }

        public void Update(Medico medico)
        {
            throw new NotImplementedException();
        }
    }
}
