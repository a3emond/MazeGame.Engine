using MazeGame.Engine.API.DTO;
using MazeGame.Engine.GameEngine.Models.Item;

namespace MazeGame.Engine.API.DTO
{
    public class ItemGridDTO
    {
        public int Width { get; set; } // Width of the item grid (same as Maze grid)
        public int Height { get; set; } // Height of the item grid
        public List<ItemDTO> Items { get; set; } = new(); // List of all placed items with position and properties
        public Dictionary<int, string>? SpriteMap { get; set; } // Optional: Maps item ID to sprite path

        /// <summary>
        /// Converts an ItemGrid into an ItemGridDTO for API transmission.
        /// 
        /// Items are extracted into a flat list, where each item has its:
        /// - Name (ItemName enum as string)
        /// - X and Y position
        /// - Walkability and interactivity flags
        /// 
        /// Frontend rendering:
        /// To find an item at a given (x, y), loop through the Items list and match coordinates.
        /// </summary>
        /// <param name="grid">ItemGrid object containing all placed items.</param>
        /// <param name="includeSpriteMap">If true, attaches the item sprite mapping for frontend rendering.</param>
        /// <returns>ItemGridDTO ready for JSON serialization.</returns>
        public static ItemGridDTO From(ItemGrid grid, bool includeSpriteMap = false)
        {
            var dto = new ItemGridDTO
            {
                Width = grid.Width,
                Height = grid.Height,
                Items = grid.GetAllItems()
                            .Select(item => new ItemDTO
                            {
                                Name = item.Name.ToString(),
                                X = item.X,
                                Y = item.Y,
                                Walkable = item.Walkable,
                                Interactable = item.Interactable,
                                Collectible = item.Collectible
                            })
                            .ToList()
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
}
