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
}