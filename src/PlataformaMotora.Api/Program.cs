using Microsoft.EntityFrameworkCore;

using PlataformaMotora.Infrastructure.Persistence;

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


        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.Run();
    }
}