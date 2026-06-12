using System;

public class Medico
{
    private string _nombre;
    private string _matricula;
    private Especialidad _especialidad;

    public Medico()
    {
    }

    public Medico(string nombre, string matricula, Especialidad especialidad)
    {
        _nombre = nombre;
        _matricula = matricula;
        _especialidad = especialidad;
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

    public Especialidad Especialidad
    {
        get { return _especialidad; }
        set { _especialidad = value; }
    }
}
