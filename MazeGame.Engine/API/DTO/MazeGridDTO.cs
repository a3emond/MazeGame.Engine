using MazeGame.Engine.GameEngine.Models.Maze;

namespace MazeGame.Engine.API.DTO
{
    public class MazeGridDTO
    {
        public int Width { get; set; } // Width of the maze grid (expanded for walls if needed)
        public int Height { get; set; } // Height of the maze grid
        public List<int> Grid { get; set; } = new(); // Flattened 1D array representing the 2D maze layout
        public Dictionary<int, string>? SpriteMap { get; set; } // Optional: Maps tile type to sprite asset

        /// <summary>
        /// Converts a Maze object into a MazeGridDTO, flattening the 2D array into a 1D array for JSON compatibility.
        /// 
        /// The 2D grid [x, y] is serialized into a simple 1D list, row-major order.
        /// 
        /// To reconstruct the tile at position (x, y) on the frontend:
        ///     index = (y * Width) + x;
        ///     tileType = Grid[index];
        /// 
        /// This allows lightweight, consistent JSON transfer without complex structures.
        /// </summary>
        /// <param name="maze">Maze object containing the generated tile grid.</param>
        /// <param name="includeSpriteMap">If true, attaches the sprite mapping for frontend rendering.</param>
        /// <returns>MazeGridDTO ready for API transmission.</returns>
        public static MazeGridDTO From(Maze maze, bool includeSpriteMap = false)
        {
            var dto = new MazeGridDTO
            {
                Width = maze.Width,
                Height = maze.Height,
                Grid = new List<int>(maze.Width * maze.Height)
            };

            // Flatten the 2D maze grid [x,y] into a 1D list [ (y * Width) + x ]
            for (int y = 0; y < maze.Height; y++)
            {
                for (int x = 0; x < maze.Width; x++)
                {
                    dto.Grid.Add(maze.Grid[x, y]);
                }
            }

            // Optional: include a mapping from tile index to sprite file
            if (includeSpriteMap)
            {
                dto.SpriteMap = Enum.GetValues(typeof(TileType))
                    .Cast<TileType>()
                    .ToDictionary(t => (int)t, t => t.GetTileSprite());
            }

            return dto;
        }
    }
}
