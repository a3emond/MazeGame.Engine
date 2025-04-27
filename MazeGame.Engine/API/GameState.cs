using MazeGame.Engine.GameEngine.GeneratingAlgorithms;
using MazeGame.Engine.GameEngine.Models.Item;
using MazeGame.Engine.GameEngine.Models.Maze;
using MazeGame.Engine.GameEngine.Models.Others;
using MazeGame.Engine.GameEngine.Models.Player;

namespace MazeGame.Engine.API;

public class GameState
{
    public Maze? Maze { get; set; }
    public Player? Player { get; set; }
    public FogOfWar FogOfWar { get; set; } = new();

    public int PlayerX => Player?.X ?? 0;
    public int PlayerY => Player?.Y ?? 0;
    public string LastMoveDirection => Player?.Direction ?? "down";

    public bool GameStarted { get; set; }
    public bool GameRunning { get; set; }
    public bool MazeInitialized { get; set; }
    public bool GameOver { get; set; }
    public bool GoalUnlocked { get; set; }

    public MazeAlgorithmType SelectedAlgorithm { get; set; } = MazeAlgorithmType.RecursiveBacktracking;
    public MazeRendererType RendererType { get; set; } = MazeRendererType.Canvas2D;

    public int MaxHearts { get; set; } = 5;
    public double CurrentHearts { get; set; } = 3.0;
    public string[] InventorySlots { get; set; } = new string[3];
    public string? StatusEffect { get; set; }

    public DateTime GameStartTime { get; set; }
    public TimeSpan TimeLimit { get; set; } = TimeSpan.FromMinutes(5);
    public TimeSpan ElapsedTime => DateTime.Now - GameStartTime;
    public TimeSpan Timer =>
        (TimeLimit - ElapsedTime) > TimeSpan.Zero ? (TimeLimit - ElapsedTime) : TimeSpan.Zero;


    public ItemEffectResult? LastItemEffect { get; set; }

    public void Reset()
    {
        MazeInitialized = false;
        GameStarted = false;
        GameRunning = false;
        GameOver = false;
        SelectedAlgorithm = MazeAlgorithmType.RecursiveBacktracking;
        Maze = null;
        Player = null;
        CurrentHearts = MaxHearts;
        GoalUnlocked = false;
        StatusEffect = null;
        LastItemEffect = null;
        InventorySlots = new string[3];
        GameStartTime = DateTime.MinValue;
        TimeLimit = TimeSpan.FromMinutes(5);
    }

}