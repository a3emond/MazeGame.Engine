using MazeGame.Engine.GameEngine.Models.Maze;

namespace MazeGame.Engine.API.DTO;

public class MazeGridDTO
{
    public int Width { get; set; }               // Actual grid width (after cell expansion)
    public int Height { get; set; }              // Actual grid height
    public int[,] Grid { get; set; }             // Raw tile index grid
    public Dictionary<int, string>? SpriteMap { get; set; } // Optional mapping: tile index -> sprite path

    public static MazeGridDTO From(Maze maze, bool includeSpriteMap = false)
    {
        var dto = new MazeGridDTO
        {
            Width = maze.Width,
            Height = maze.Height,
            Grid = maze.Grid
        };

        if (includeSpriteMap)
        {
            dto.SpriteMap = Enum.GetValues(typeof(TileType))
                .Cast<TileType>()
                .ToDictionary(t => (int)t, t => t.GetTileSprite());
        }

        return dto;
    }
}