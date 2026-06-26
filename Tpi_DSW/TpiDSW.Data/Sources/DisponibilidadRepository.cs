using TpiDSW.Domain.Entities;
using TpiDSW.Domain.Interfaces;

namespace TpiDSW.Data.Sources;

public class DisponibilidadRepository : IDisponibilidadRepository
{
    private static readonly List<Disponibilidad> _disponibilidades = new List<Disponibilidad>();

    public List<Disponibilidad> GetAll()
    {
        return _disponibilidades;
    }

    public Disponibilidad? GetById(Guid id)
    {
        return _disponibilidades.FirstOrDefault(disponibilidad => disponibilidad.Id == id);
    }

    public List<Disponibilidad> GetByMedicoId(Guid medicoId)
    {
        return _disponibilidades
            .Where(disponibilidad => disponibilidad.MedicoId == medicoId)
            .ToList();
    }

    public List<Disponibilidad> GetByMedicoIdAndMes(Guid medicoId, int mes, int anio)
    {
        return _disponibilidades
            .Where(disponibilidad =>
                disponibilidad.MedicoId == medicoId &&
                disponibilidad.Mes == mes &&
                disponibilidad.Anio == anio)
            .ToList();
    }

    public void Add(Disponibilidad disponibilidad)
    {
        _disponibilidades.Add(disponibilidad);
    }

    public void Update(Disponibilidad disponibilidad)
    {
        Disponibilidad? disponibilidadExistente = GetById(disponibilidad.Id);

        if (disponibilidadExistente is null)
        {
            return;
        }

        disponibilidadExistente.MedicoId = disponibilidad.MedicoId;
        disponibilidadExistente.Mes = disponibilidad.Mes;
        disponibilidadExistente.Anio = disponibilidad.Anio;
        disponibilidadExistente.Dia = disponibilidad.Dia;
        disponibilidadExistente.TEntrada = disponibilidad.TEntrada;
        disponibilidadExistente.TSalida = disponibilidad.TSalida;
    }

    public void Delete(Guid id)
    {
        Disponibilidad? disponibilidad = GetById(id);

        if (disponibilidad is null)
        {
            return;
        }

        _disponibilidades.Remove(disponibilidad);
    }
}
