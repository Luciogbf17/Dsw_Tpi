using System.Runtime.InteropServices.JavaScript;
using TpiDSW.Domain.Enum;

namespace TpiDSW.Domain.Entities;

public class Turno
{
    private DateOnly _fecha;
    private TimeOnly _horaInicio;
    private TimeOnly _horaFin;
    private EstadoTurno _estado;
    
    
    public Turno(DateOnly fecha, TimeOnly horaInicio, TimeOnly horaFin)
    {
        _fecha = fecha;
        _horaInicio = horaInicio;
        _horaFin = horaFin;
        _estado = EstadoTurno.Disponible; //se inicializa en disponible el turno
    }

    public EstadoTurno Estado
    {
        get => _estado;
        set => _estado= value;
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