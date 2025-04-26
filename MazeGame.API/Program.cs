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

            var app = builder.Build();

            // Swagger is completely removed from production

            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}