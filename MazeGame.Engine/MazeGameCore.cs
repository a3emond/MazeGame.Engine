using MazeGame.Engine.GameEngine.Models;
using MazeGame.Engine.GameEngine.Services;
using MazeGame.Engine.GameEngine.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MazeGame.Engine
{
    /// <summary>
    /// Core logic class for controlling a Maze Game session.
    /// This class is completely decoupled from any UI/JS and can be used from an API, simulation, or automated test.
    /// </summary>
    public class MazeGameCore
    {
        private readonly MazeGameService _gameService;
        private readonly GameState _state;

        public MazeGameCore(MazeGameService gameService, GameState state)
        {
            _gameService = gameService;
            _state = state;
        }

        /// <summary>
        /// Initializes a new maze with the selected algorithm.
        /// Must be called before starting the game.
        /// </summary>
        public void InitializeMaze()
        {
            _state.MazeInitialized = false;
            _state.Maze = _gameService.GenerateMaze(_state.SelectedAlgorithm);

            var (x, y) = _state.Maze.StartPosition;
            _state.Player = new Player(x, y, _state.Maze, _state);

            _state.MazeInitialized = true;
        }

        /// <summary>
        /// Starts a new game session.
        /// Resets player, sets timers, and marks the game as running.
        /// </summary>
        public void StartGame()
        {
            _state.GameStarted = true;
            _state.GameStartTime = DateTime.Now;
            _state.Timer = _state.TimeLimit;

            var (x, y) = _state.Maze.StartPosition;
            _state.Player = new Player(x, y, _state.Maze, _state);
        }

        /// <summary>
        /// Processes one step of the game loop based on player movement.
        /// Should be called on each tick or input.
        /// </summary>
        public string? Step(string direction)
        {
            if (!_state.GameStarted || _state.GameOver || _state.Player == null)
                return null;

            _state.Player.Move(direction, _state.Maze);
            _state.Player.TryPickupItem(_state.Maze);

            // Check end conditions
            if (_state.Timer <= TimeSpan.Zero)
            {
                _state.StatusEffect = "Time's up!";
                _state.CurrentHearts = 0;
                EndGame();
                return "GameOver";
            }

            if (_state.CurrentHearts <= 0)
            {
                EndGame();
                return "GameOver";
            }

            if (_state.Maze.GoalPosition == (_state.Player.X, _state.Player.Y))
            {
                if (_state.GoalUnlocked)
                {
                    EndGame();
                    return "Win";
                }
                else
                {
                    _state.StatusEffect = "Goal Locked, find the key...";
                }
            }

            return null;
        }

        /// <summary>
        /// Ends the game session.
        /// </summary>
        public void EndGame()
        {
            _state.GameOver = true;
            _state.GameRunning = false;
        }

        /// <summary>
        /// Updates the internal timer. Should be called periodically.
        /// Returns true if game is still running.
        /// </summary>
        public bool UpdateTimer()
        {
            if (!_state.GameStarted || _state.GameOver)
                return false;

            var elapsed = DateTime.Now - _state.GameStartTime;
            _state.Timer = _state.TimeLimit - elapsed;

            if (_state.Timer < TimeSpan.Zero)
                _state.Timer = TimeSpan.Zero;

            return !_state.GameOver;
        }

        /// <summary>
        /// Gets a simplified version of the game state (for API/JSON export).
        /// </summary>
        public GameState GetState() => _state;

        /// <summary>
        /// Gets a list of currently active items for rendering or frontend sync.
        /// </summary>
        public List<object> GetItemRenderData()
        {
            return _state.Maze.ItemGrid.GetAllItems()
                .Select(i => new { i.X, i.Y, i.Sprite })
                .Cast<object>()
                .ToList();
        }
    }
}
