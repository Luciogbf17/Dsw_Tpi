using TpiDSW.Domain.Entities;
using TpiDSW.Domain.Interfaces;

namespace TpiDSW.Data.Sources;

public class TurnoRepository : ITurnoRepository
{
    private static readonly List<Turno> _turnos = new List<Turno>();

    public List<Turno> GetAll()
    {
        return _turnos;
    }

    public Turno? GetById(Guid id)
    {
        return _turnos.FirstOrDefault(turno => turno.Id == id);
    }

    public List<Turno> GetByFecha(DateOnly fecha)
    {
        return _turnos
            .Where(turno => turno.Fecha == fecha)
            .ToList();
    }

    public List<Turno> GetByDisponibilidadId(Guid disponibilidadId)
    {
        return _turnos
            .Where(turno => turno.DisponibilidadId == disponibilidadId)
            .ToList();
    }

    public void Add(Turno turno)
    {
        _turnos.Add(turno);
    }

    public void Update(Turno turno)
    {
        Turno? turnoExistente = GetById(turno.Id);

        if (turnoExistente is null)
        {
            return;
        }

        turnoExistente.MedicoId = turno.MedicoId;
        turnoExistente.DisponibilidadId = turno.DisponibilidadId;
        turnoExistente.Fecha = turno.Fecha;
        turnoExistente.HoraInicio = turno.HoraInicio;
        turnoExistente.HoraFin = turno.HoraFin;
        turnoExistente.Estado = turno.Estado;
    }

    public void Delete(Guid id)
    {
        Turno? turno = GetById(id);

        if (turno is null)
        {
            return;
        }

        _turnos.Remove(turno);
    }
}
