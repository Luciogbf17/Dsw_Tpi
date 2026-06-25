using System;

namespace TpiDSW.Domain.Entities;
public class Especialidad
{
    private string _nombre;
    private string _descripcion;
    private bool _activa;

    public Especialidad()
    {
    }

    public Especialidad(string nombre, string descripcion, bool activa)
    {
        _nombre = nombre;
        _descripcion = descripcion;
        _activa = activa;
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

    public bool Activa
    {
        get { return _activa; }
        set { _activa = value; }
    }
}
