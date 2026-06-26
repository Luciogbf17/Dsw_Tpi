using TpiDSW.Domain.Enum;

namespace TpiDSW.Domain.Entities;

public class Turno
{
    private Guid _id;
    private Guid _medicoId;
    private Guid _disponibilidadId;
    private DateOnly _fecha;
    private TimeOnly _horaInicio;
    private TimeOnly _horaFin;
    private EstadoTurno _estado;

    public Turno()
    {
        _id = Guid.NewGuid();
        _estado = EstadoTurno.Disponible;
    }

    public Turno(DateOnly fecha, TimeOnly horaInicio, TimeOnly horaFin)
    {
        _id = Guid.NewGuid();
        _fecha = fecha;
        _horaInicio = horaInicio;
        _horaFin = horaFin;
        _estado = EstadoTurno.Disponible;
    }

    public Turno(Guid medicoId, Guid disponibilidadId, DateOnly fecha, TimeOnly horaInicio, TimeOnly horaFin)
    {
        _id = Guid.NewGuid();
        _medicoId = medicoId;
        _disponibilidadId = disponibilidadId;
        _fecha = fecha;
        _horaInicio = horaInicio;
        _horaFin = horaFin;
        _estado = EstadoTurno.Disponible;
    }

    public Guid Id
    {
        get => _id;
        set => _id = value;
    }

    public Guid MedicoId
    {
        get => _medicoId;
        set => _medicoId = value;
    }

    public Guid DisponibilidadId
    {
        get => _disponibilidadId;
        set => _disponibilidadId = value;
    }

    public EstadoTurno Estado
    {
        get => _estado;
        set => _estado = value;
    }

    public DateOnly Fecha
    {
        get => _fecha;
        set => _fecha = value;
    }

    public TimeOnly HoraInicio
    {
        get => _horaInicio;
        set => _horaInicio = value;
    }

    public TimeOnly HoraFin
    {
        get => _horaFin;
        set => _horaFin = value;
    }
}
