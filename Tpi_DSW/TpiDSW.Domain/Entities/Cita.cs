using System;
using TpiDSW.Domain.Enum;

namespace TpiDSW.Domain.Entities
{
    public class Cita
    {
        private Guid _id;
        private DateTime _fechaDeAtencion;
        private DateTime? _fechaDeCancelacion;
        private CitaEstado _estado;
        private bool _deleted;

        public Cita()
        {
            _id = Guid.NewGuid();
            _estado = CitaEstado.Confirmada;
            _deleted = false;
        }

        public Cita(DateTime fechaDeAtencion, CitaEstado estado)
        {
            _id = Guid.NewGuid();
            _fechaDeAtencion = fechaDeAtencion;
            _estado = estado;
            _deleted = false;
        }

        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public DateTime FechaDeAtencion
        {
            get { return _fechaDeAtencion; }
            set { _fechaDeAtencion = value; }
        }

        public DateTime? FechaDeCancelacion
        {
            get { return _fechaDeCancelacion; }
            set { _fechaDeCancelacion = value; }
        }

        public CitaEstado Estado
        {
            get { return _estado; }
            set { _estado = value; }
        }

        public bool Deleted
        {
            get { return _deleted; }
            set { _deleted = value; }
        }
    }
}
