using System;
namespace MedicalAppointments.Domain.Entities
{
    public class Paciente
    {
        private Guid _id;
        private int _dni;
        private string _nombre;
        private string _telefono;
        private bool _deleted;

        public Paciente()
        {
            _id = Guid.NewGuid();
            _nombre = string.Empty;
            _telefono = string.Empty;
            _deleted = false;
        }

        public Paciente(int dni, string nombre, string telefono)
        {
            _id = Guid.NewGuid();
            _dni = dni;
            _nombre = nombre;
            _telefono = telefono;
            _deleted = false;
        }

        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public int Dni
        {
            get { return _dni; }
            set { _dni = value; }
        }

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public string Telefono
        {
            get { return _telefono; }
            set { _telefono = value; }
        }

        public bool Deleted
        {
            get { return _deleted; }
            set { _deleted = value; }
        }
    }
}