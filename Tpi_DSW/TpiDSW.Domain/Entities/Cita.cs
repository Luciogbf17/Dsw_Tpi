using TpiDSW.Domain.Enum;

namespace TpiDSW.Domain.Entities;

public class Cita
{
    private Guid _id;
    private Guid _turnoId;
    private Guid _pacienteId;
    private DateTime _fechaDeAtencion;
    private DateTime? _fechaDeCancelacion;
    private CitaEstado _estado;
    private bool _deleted;
    private string _motivo;

    public Cita()
    {
        _id = Guid.NewGuid();
        _estado = CitaEstado.Confirmada;
        _deleted = false;
        _motivo = string.Empty;
    }

    public Cita(DateTime fechaDeAtencion, CitaEstado estado)
    {
        _id = Guid.NewGuid();
        _fechaDeAtencion = fechaDeAtencion;
        _estado = estado;
        _deleted = false;
        _motivo = string.Empty;
    }

    public Cita(Guid turnoId, Guid pacienteId, DateTime fechaDeAtencion, string motivo)
    {
        _id = Guid.NewGuid();
        _turnoId = turnoId;
        _pacienteId = pacienteId;
        _fechaDeAtencion = fechaDeAtencion;
        _estado = CitaEstado.Confirmada;
        _deleted = false;
        _motivo = motivo;
    }

    public Guid Id
    {
        get => _id;
        set => _id = value;
    }

    public Guid TurnoId
    {
        get => _turnoId;
        set => _turnoId = value;
    }

    public Guid PacienteId
    {
        get => _pacienteId;
        set => _pacienteId = value;
    }

    public DateTime FechaDeAtencion
    {
        get => _fechaDeAtencion;
        set => _fechaDeAtencion = value;
    }

    public DateTime? FechaDeCancelacion
    {
        get => _fechaDeCancelacion;
        set => _fechaDeCancelacion = value;
    }

    public CitaEstado Estado
    {
        get => _estado;
        set => _estado = value;
    }

    public bool Deleted
    {
        get => _deleted;
        set => _deleted = value;
    }

    public string Motivo
    {
        get => _motivo;
        set => _motivo = value;
    }
}
