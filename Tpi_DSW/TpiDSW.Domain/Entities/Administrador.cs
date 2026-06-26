using System;

namespace TpiDSW.Domain.Entities
{
    public class Administrador
    {
        private Guid _id;
        private string _email;
        private string _passwordHash;
        private bool _deleted;

        public Administrador()
        {
            _id = Guid.NewGuid();
            _email = string.Empty;
            _passwordHash = string.Empty;
            _deleted = false;
        }

        public Administrador(string email, string passwordHash)
        {
            _id = Guid.NewGuid();
            _email = email;
            _passwordHash = passwordHash;
            _deleted = false;
        }

        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string PasswordHash
        {
            get { return _passwordHash; }
            set { _passwordHash = value; }
        }

        public bool Deleted
        {
            get { return _deleted; }
            set { _deleted = value; }
        }
    }
}