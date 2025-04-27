using MazeGame.Engine.API;

namespace MazeGame.API
{
    public class GameServiceFactory
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IServiceProvider _serviceProvider;

        public GameServiceFactory(IHttpContextAccessor httpContextAccessor, IServiceProvider serviceProvider)
        {
            _httpContextAccessor = httpContextAccessor;
            _serviceProvider = serviceProvider;
        }

        public MazeGameService GetOrCreateService()
        {
            var session = _httpContextAccessor.HttpContext?.Session;
            var serviceProvider = _serviceProvider;

            if (session == null)
                throw new Exception("No session available");

            if (session.TryGetValue("GameServiceId", out var idBytes))
            {
                var id = System.Text.Encoding.UTF8.GetString(idBytes);
                return GameSessionStore.GetService(id);
            }
            else
            {
                var newService = serviceProvider.GetRequiredService<MazeGameService>();
                var newId = Guid.NewGuid().ToString();
                GameSessionStore.RegisterService(newId, newService);

                session.SetString("GameServiceId", newId);
                return newService;
            }
        }
    }

}
