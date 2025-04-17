using Scalar.AspNetCore;

using Microsoft.EntityFrameworkCore;

using PlataformaMotora.Domain.Repositories;
using PlataformaMotora.Infrastructure.Persistence;
using PlataformaMotora.Infrastructure.Persistence.Repositories;

internal class Program
{
    private static void Main(string[] args)
    {
        DotNetEnv.Env.Load();

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddOpenApi();

        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            var connectionString = Environment.GetEnvironmentVariable("SUPABASE_CONNECTION");
            options.UseNpgsql(connectionString);
        });

        builder.Services.AddScoped<IVeiculoRepository, VeiculoRepository>();

        builder.Services.AddControllers();

        var app = builder.Build();

        app.UseHttpsRedirection();
        app.UseAuthorization();

        app.MapControllers();

        app.MapOpenApi();

        if (app.Environment.IsDevelopment())
        {
            app.MapScalarApiReference();
        }

        app.Run();
    }
}