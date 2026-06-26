using System;

namespace TpiDSW.Domain.Entities;

public class Especialidad
{
    private Guid _id;
    private string _nombre;
    private string _descripcion;
    private bool _deleted;

    public Especialidad()
    {
        _id = Guid.NewGuid();
        _nombre = string.Empty;
        _descripcion = string.Empty;
        _deleted = false;
    }

    public Especialidad(string nombre, string descripcion)
    {
        _id = Guid.NewGuid();
        _nombre = nombre;
        _descripcion = descripcion;
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

    public string Descripcion
    {
        get { return _descripcion; }
        set { _descripcion = value; }
    }

    public bool Deleted
    {
        get { return _deleted; }
        set { _deleted = value; }
    }
}