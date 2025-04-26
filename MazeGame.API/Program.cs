using MazeGame.Engine.API;

namespace MazeGame.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Create a new web application builder
            var builder = WebApplication.CreateBuilder(args);

            // Set Kestrel server to listen explicitly on localhost:5055
            builder.WebHost.ConfigureKestrel(serverOptions =>
            {
                serverOptions.ListenLocalhost(5055); // Internal port for the app
            });

            // Add services to the container
            builder.Services.AddSingleton<GameState>();
            builder.Services.AddSingleton<MazeGameService>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });


            var app = builder.Build();

            app.UseCors("AllowAll");
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}