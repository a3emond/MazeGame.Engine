using MazeGame.Engine.API;

namespace MazeGame.API
{
    public static class GameSessionStore
    {
        private static readonly Dictionary<string, MazeGameService> _services = new();

        public static void RegisterService(string id, MazeGameService service)
        {
            _services[id] = service;
        }

        public static MazeGameService GetService(string id)
        {
            return _services[id];
        }
    }

}
