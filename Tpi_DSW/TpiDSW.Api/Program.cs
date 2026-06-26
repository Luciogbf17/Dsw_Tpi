using TpiDSW.Api.Middleware;
using TpiDSW.Data.Sources;
using TpiDSW.Domain.Interfaces;

namespace TpiDSW.Api;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddSwaggerGen();
        builder.Services.AddHealthChecks();

        builder.Services.AddSingleton<IEspecialidadRepository, EspecialidadRepository>();
        builder.Services.AddSingleton<IMedicoRepository, MedicoRepository>();
        builder.Services.AddSingleton<IPacienteRepository, PacienteRepository>();
        builder.Services.AddSingleton<IDisponibilidadRepository, DisponibilidadRepository>();
        builder.Services.AddSingleton<ITurnoRepository, TurnoRepository>();
        builder.Services.AddSingleton<ICitaRepository, CitaRepository>();

        WebApplication app = builder.Build();

        app.UseMiddleware<ExceptionMiddleware>();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHealthChecks("/health-check");

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
