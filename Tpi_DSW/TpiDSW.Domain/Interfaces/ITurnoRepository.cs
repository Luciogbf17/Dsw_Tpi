using TpiDSW.Domain.Entities;

namespace TpiDSW.Domain.Interfaces;

public interface ITurnoRepository
{
    List<Turno> GetAll();

    Turno? GetById(Guid id);

    List<Turno> GetByFecha(DateOnly fecha);

    List<Turno> GetByDisponibilidadId(Guid disponibilidadId);

    void Add(Turno turno);

    void Update(Turno turno);

    void Delete(Guid id);
}
