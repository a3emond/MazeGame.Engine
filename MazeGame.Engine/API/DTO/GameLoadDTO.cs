using MazeGame.Engine.API.DTO;

namespace MazeGame.Engine.API.DTO;

public class GameLoadDTO
{
    public List<string> TileGrid { get; set; } = new(); // 1D array of tile sprite URLs
    public List<WalkableTileDTO> WalkableTiles { get; set; } = new();

    public int MazeWidth { get; set; }
    public int MazeHeight { get; set; }

    public List<ItemLoadDTO> Items { get; set; } = new(); // List of placed items
    public int StartX { get; set; }
    public int StartY { get; set; }
    public int GoalX { get; set; }
    public int GoalY { get; set; }

    public Dictionary<string, List<string>> PlayerAnimations { get; set; } = new(); // Up, Down, Left, Right mapped to frame URLs

    public int DefaultLightRadius { get; set; } = 3;
    public int MaxHearts { get; set; } = 5;
    public double StartingHearts { get; set; } = 3.0;
    public int TimeLimitSeconds { get; set; } = 300;
}

public class ItemLoadDTO
{
    public int X { get; set; }
    public int Y { get; set; }
    public string Effect { get; set; } = string.Empty;
    public string SpritePath { get; set; } = string.Empty;
    public bool Collectible { get; set; }
}