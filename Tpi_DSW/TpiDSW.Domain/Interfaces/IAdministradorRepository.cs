using System;
using System.Collections.Generic;
using TpiDSW.Domain.Entities;

namespace TpiDSW.Domain.Interfaces
{
    public interface IAdministradorRepository
    {
        List<Administrador> GetAll();

        Administrador? GetById(Guid id);

        Administrador? GetByEmail(string email);

        void Add(Administrador administrador);

        void Update(Administrador administrador);

        void Delete(Guid id);
    }
}