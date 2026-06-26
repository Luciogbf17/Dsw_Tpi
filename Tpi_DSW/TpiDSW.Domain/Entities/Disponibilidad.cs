namespace TpiDSW.Domain.Entities;

public class Disponibilidad
{
    private Guid _id;
    private Guid _medicoId;
    private int _mes;
    private int _anio;
    private int _dia;
    private TimeOnly _tEntrada;
    private TimeOnly _tSalida;

    public Disponibilidad()
    {
        _id = Guid.NewGuid();
    }

    public Disponibilidad(int mes, int anio, int dia, TimeOnly tEntrada, TimeOnly tSalida)
    {
        _id = Guid.NewGuid();
        _mes = mes;
        _anio = anio;
        _dia = dia;
        _tEntrada = tEntrada;
        _tSalida = tSalida;
    }

    public Disponibilidad(Guid medicoId, int mes, int anio, int dia, TimeOnly tEntrada, TimeOnly tSalida)
    {
        _id = Guid.NewGuid();
        _medicoId = medicoId;
        _mes = mes;
        _anio = anio;
        _dia = dia;
        _tEntrada = tEntrada;
        _tSalida = tSalida;
    }

    public Guid Id
    {
        get => _id;
        set => _id = value;
    }

    public Guid MedicoId
    {
        get => _medicoId;
        set => _medicoId = value;
    }

    public int Mes
    {
        get => _mes;
        set => _mes = value;
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
