namespace TpiDSW.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddSwaggerGen();
        builder.Services.AddHealthChecks();

        var app = builder.Build();
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseHealthChecks("/healt-check");

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            
            app.UseSwagger();
        }

        app.UseAuthorization();

        app.MapControllers();

        app.Run();

    }
}

