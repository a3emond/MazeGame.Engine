using MazeGame.Engine.API.DTO;
using MazeGame.Engine.GameEngine.Models.Item;

public class ItemGridDTO
{
    public int Width { get; set; }
    public int Height { get; set; }
    public List<ItemDTO> Items { get; set; }
    public Dictionary<int, string>? SpriteMap { get; set; } // optional

    public static ItemGridDTO From(ItemGrid grid, bool includeSpriteMap = false)
    {
        var dto = new ItemGridDTO
        {
            Width = grid.Width,
            Height = grid.Height,
            Items = grid.GetAllItems().Select(item => new ItemDTO
            {
                Name = item.Name.ToString(),
                X = item.X,
                Y = item.Y,
                Walkable = item.Walkable,
                Interactable = item.Interactable,
                Collectible = item.Collectible
            }).ToList()
        };

        if (includeSpriteMap)
        {
            dto.SpriteMap = Enum.GetValues(typeof(ItemName))
                .Cast<ItemName>()
                .ToDictionary(t => (int)t, t => t.GetItemSprite());
        }

        return dto;
    }
}