using System.Reflection.Emit;
using System.Runtime.InteropServices.JavaScript;

namespace TpiDSW.Domain.Entities;

public class Disponibilidad
{
    private int _mes;
    private int _anio;
    private int _dia;

    private TimeOnly _tEntrada;
    private TimeOnly _tSalida;

    public Disponibilidad(int mes, int anio, int dia, TimeOnly tEntrada, TimeOnly tSalida)
    {
        _mes = mes;
        _anio = anio;
        _dia = dia;
        _tEntrada = tEntrada;
        _tSalida = tSalida;
    }


    public int Mes
    {
        get => _mes; 
        set => _mes=value;
    }

    public int Anio
    {
        get => _anio;
        set => _anio = value;
    }

    public int Dia
    {
        get => _dia;
        set => _dia = value;
    }

    public TimeOnly TEntrada
    {
        get => _tEntrada;
        set => _tEntrada = value;
    }

    public TimeOnly TSalida
    {
        get => _tSalida;
        set => _tSalida = value;
    }
}