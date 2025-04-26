using MazeGame.Engine.API.DTO;
using MazeGame.Engine.GameEngine.GeneratingAlgorithms;
using MazeGame.Engine.GameEngine.Models.Item;
using MazeGame.Engine.GameEngine.Models.Player;
using MazeGame.Engine.GameEngine.Services;

namespace MazeGame.Engine.API;

public class MazeGameCore //Pure backend game logic engine. No API exposure. Only mutates the internal GameState.
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

        HandlePickup(); // Pick item at spawn if any
        _state.MazeInitialized = true;
    }


    public void StartGame()
    {
        _state.GameStarted = true;
        _state.GameRunning = true;
        _state.GameStartTime = DateTime.Now;
        _state.TimeLimit = _state.TimeLimit;
        var (startX, startY) = _state.Maze?.StartPosition ?? (0, 0);
        _state.Player = new Player(startX, startY);
    }

    public void EndGame()
    {
        _state.GameOver = true;
        _state.GameRunning = false;
    }

    public bool UpdateTimer()
    {
        if (!_state.GameStarted || _state.GameOver)
            return false;

        // If timer reached 0, trigger game over automatically
        if (_state.Timer <= TimeSpan.Zero)
        {
            EndGame();
            return false;
        }

        return true;
    }


    // =====================
    // 🎮 Player Actions
    // =====================

    public void Step(string direction)
    {
        if (!_state.GameStarted || _state.GameOver || _state.Player == null)
            return;

        if (!UpdateTimer())
        {
            // Timer expired and game ended during this tick
            return;
        }

        PlayerMovementService.Move(_state.Player, direction, _state.Maze!);
        HandlePickup();
        CheckWinCondition();
        CheckLoseCondition();
    }


    private void HandlePickup()
    {
        if (_state.Maze == null || _state.Player == null)
            return;

        var item = _state.Maze.ItemGrid.GetItemAt(_state.Player.X, _state.Player.Y);
        if (item == null)
            return;

        var effect = PlayerInteractionService.TryPickupItem(_state.Player, _state.Maze);
        ApplyItemEffects(effect);
    }

    private void ApplyItemEffects(ItemEffectResult effect)
    {
        _state.LastItemEffect = effect;

        if (effect.HealChange > 0)
            _state.CurrentHearts = Math.Min(_state.MaxHearts, _state.CurrentHearts + effect.HealChange);

        if (effect.DamageTaken > 0)
            _state.CurrentHearts = Math.Max(0, _state.CurrentHearts - effect.DamageTaken);

        if (!string.IsNullOrEmpty(effect.CollectedSprite))
        {
            for (int i = 0; i < _state.InventorySlots.Length; i++)
            {
                if (string.IsNullOrEmpty(_state.InventorySlots[i]))
                {
                    _state.InventorySlots[i] = effect.CollectedSprite;
                    break;
                }
            }
        }

        if (effect.GoalUnlocked)
            _state.GoalUnlocked = true;

        if (!string.IsNullOrEmpty(effect.StatusEffect))
            _state.StatusEffect = effect.StatusEffect;
    }

    private bool CheckLoseCondition()
    {
        if (_state.Timer <= TimeSpan.Zero || _state.CurrentHearts <= 0)
        {
            _state.StatusEffect = "You Died!";
            EndGame();
            return true;
        }
        return false;
    }

    private bool CheckWinCondition()
    {
        if (_state.Maze?.GoalPosition == (_state.Player!.X, _state.Player.Y))
        {
            if (_state.GoalUnlocked)
            {
                _state.StatusEffect = "You Escaped!";
                EndGame();
                return true;
            }
            else
            {
                _state.StatusEffect = "Goal Locked, find the key!";
            }
        }
        return false;
    }

    
}
