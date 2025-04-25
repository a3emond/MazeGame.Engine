using MazeGame.Engine.GameEngine.Models.Item;

namespace MazeGame.Engine.API.DTO;

public class GameStateDTO
{
    public int X { get; set; }
    public int Y { get; set; }
    public string Direction { get; set; } = "down";
    public int LightRadius { get; set; }
    public double CurrentHearts { get; set; }
    public int MaxHearts { get; set; }
    public int TimerSeconds { get; set; }
    public string[] Inventory { get; set; } = new string[3];
    public string Status { get; set; } = "Normal";
    public bool GameOver { get; set; }
    public bool GoalUnlocked { get; set; }
    public ItemEffectResult? LastEffect { get; set; }
}