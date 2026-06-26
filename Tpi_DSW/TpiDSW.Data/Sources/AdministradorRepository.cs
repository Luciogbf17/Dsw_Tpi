using System;
using System.Collections.Generic;
using System.Linq;
using TpiDSW.Domain.Entities;
using TpiDSW.Domain.Interfaces;

namespace TpiDSW.Data.Sources
{
    public class AdministradorRepository : IAdministradorRepository
    {
        private readonly List<Administrador> _administradores;

        public AdministradorRepository()
        {
            _administradores = new List<Administrador>();

            _administradores.Add(new Administrador(
                "admin@tpidsw.com",
                "60fe74406e7f353ed979f350f2fbb6a2e8690a5fa7d1b0c32983d1d8b3f95f67"
            ));
        }

        public List<Administrador> GetAll()
        {
            return _administradores
                .Where(administrador => !administrador.Deleted)
                .ToList();
        }

        public Administrador? GetById(Guid id)
        {
            return _administradores
                .FirstOrDefault(administrador => administrador.Id == id && !administrador.Deleted);
        }

        public Administrador? GetByEmail(string email)
        {
            return _administradores
                .FirstOrDefault(administrador =>
                    administrador.Email.Equals(email, StringComparison.OrdinalIgnoreCase)
                    && !administrador.Deleted);
        }

        public void Add(Administrador administrador)
        {
            _administradores.Add(administrador);
        }

        public void Update(Administrador administrador)
        {
            Administrador? administradorExistente = GetById(administrador.Id);

            if (administradorExistente == null)
            {
                return;
            }

            administradorExistente.Email = administrador.Email;
            administradorExistente.PasswordHash = administrador.PasswordHash;
        }

        public void Delete(Guid id)
        {
            Administrador? administrador = GetById(id);

            if (administrador == null)
            {
                return;
            }

            administrador.Deleted = true;
        }
    }
}