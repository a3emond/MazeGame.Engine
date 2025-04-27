using MazeGame.Engine.API.DTO;
using MazeGame.Engine.GameEngine.GeneratingAlgorithms;
using MazeGame.Engine.GameEngine.Models.Maze;
using MazeGame.Engine.GameEngine.Models.Player;
using MazeGame.Engine.GameEngine.Services;

namespace MazeGame.Engine.API
{
    public class MazeGameCore
    {
        private readonly MazeGameService _gameService;
        private readonly GameState _state;

        public MazeGameCore(MazeGameService gameService, GameState state)
        {
            _gameService = gameService;
            _state = state;
        }

        // =====================
        // 📌 Game Lifecycle
        // =====================

        public void InitializeMaze(MazeAlgorithmType algorithm)
        {
            var mazeGenerator = new MazeGenerator();
            _state.MazeInitialized = false;
            _state.SelectedAlgorithm = algorithm;
            _state.Maze = mazeGenerator.GenerateMaze(algorithm);

            var (startX, startY) = _state.Maze.StartPosition;
            _state.Player = new Player(startX, startY);

            _state.MazeInitialized = true;
        }

        public void StartGame()
        {
            if (_state.Maze == null || _state.Player == null)
                throw new InvalidOperationException("Maze and Player must be initialized before starting the game.");

            _state.GameStarted = true;
            _state.GameRunning = true;
            _state.GameStartTime = DateTime.Now;
        }

        public void ResetGame()
        {
            _state.Reset();
        }
    }
}