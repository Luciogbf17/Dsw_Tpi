using TpiDSW.Domain.Entities;

namespace TpiDSW.Domain.Interfaces;

public interface IDisponibilidadRepository
{
    List<Disponibilidad> GetAll();

    Disponibilidad? GetById(Guid id);

    List<Disponibilidad> GetByMedicoId(Guid medicoId);

    List<Disponibilidad> GetByMedicoIdAndMes(Guid medicoId, int mes, int anio);

    void Add(Disponibilidad disponibilidad);

    void Update(Disponibilidad disponibilidad);

    void Delete(Guid id);
}
