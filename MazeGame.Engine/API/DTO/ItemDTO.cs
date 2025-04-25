using MazeGame.Engine.GameEngine.Models.Item;

namespace MazeGame.Engine.API.DTO;

public class ItemDTO
{
    public string Name { get; set; }          // Enum name as string (e.g., "Potion")
    public int X { get; set; }
    public int Y { get; set; }
    public bool Walkable { get; set; }
    public bool Interactable { get; set; }
    public bool Collectible { get; set; }
}