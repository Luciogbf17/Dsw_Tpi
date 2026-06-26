using System;

namespace TpiDSW.Domain.Entities;

public class Medico
{
    private Guid _id;
    private string _nombre;
    private string _matricula;
    private Guid _especialidadId;
    private Especialidad? _especialidad;
    private bool _deleted;

    public Medico()
    {
        _id = Guid.NewGuid();
        _nombre = string.Empty;
        _matricula = string.Empty;
        _especialidadId = Guid.Empty;
        _especialidad = null;
        _deleted = false;
    }

    public Medico(string nombre, Guid especialidadId, Especialidad especialidad)
    {
        _id = Guid.NewGuid();
        _nombre = nombre;
        _matricula = string.Empty;
        _especialidadId = especialidadId;
        _especialidad = especialidad;
        _deleted = false;
    }

    public Guid Id
    {
        get { return _id; }
        set { _id = value; }
    }

    public string Nombre
    {
        get { return _nombre; }
        set { _nombre = value; }
    }

    public string Matricula
    {
        get { return _matricula; }
        set { _matricula = value; }
    }

    public Guid EspecialidadId
    {
        get { return _especialidadId; }
        set { _especialidadId = value; }
    }

    public Especialidad? Especialidad
    {
        get { return _especialidad; }
        set { _especialidad = value; }
    }

    public bool Deleted
    {
        get { return _deleted; }
        set { _deleted = value; }
    }
}