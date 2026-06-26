using TpiDSW.Domain.Entities;
using TpiDSW.Domain.Enum;
using TpiDSW.Domain.Interfaces;

namespace TpiDSW.Data.Sources;

public class CitaRepository : ICitaRepository
{
    private static readonly List<Cita> _citas = new List<Cita>();

    public List<Cita> GetAll()
    {
        return _citas
            .Where(cita => !cita.Deleted)
            .ToList();
    }

    public Cita? GetById(Guid id)
    {
        return _citas.FirstOrDefault(cita => cita.Id == id && !cita.Deleted);
    }

    public List<Cita> GetByPacienteId(Guid pacienteId)
    {
        return _citas
            .Where(cita => cita.PacienteId == pacienteId && !cita.Deleted)
            .ToList();
    }

    public List<Cita> GetByFecha(DateTime fecha)
    {
        return _citas
            .Where(cita => cita.FechaDeAtencion.Date == fecha.Date && !cita.Deleted)
            .ToList();
    }

    public void Add(Cita cita)
    {
        _citas.Add(cita);
    }

    public void Update(Cita cita)
    {
        Cita? citaExistente = GetById(cita.Id);

        if (citaExistente is null)
        {
            return;
        }

        citaExistente.TurnoId = cita.TurnoId;
        citaExistente.PacienteId = cita.PacienteId;
        citaExistente.FechaDeAtencion = cita.FechaDeAtencion;
        citaExistente.FechaDeCancelacion = cita.FechaDeCancelacion;
        citaExistente.Estado = cita.Estado;
        citaExistente.Deleted = cita.Deleted;
        citaExistente.Motivo = cita.Motivo;
    }

    public void Delete(Guid id)
    {
        Cita? cita = GetById(id);

        if (cita is null)
        {
            return;
        }

        cita.Estado = CitaEstado.Cancelada;
        cita.FechaDeCancelacion = DateTime.Now;
        cita.Deleted = true;
    }
}
