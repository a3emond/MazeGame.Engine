using MazeGame.Engine.GameEngine.Models.Maze;

namespace MazeGame.Engine.GameEngine.Utils;

public static class ItemPlacementUtils
{
    private static readonly Random Rand = new();

    public static (int x, int y)? GetRandomWalkableTile(Maze maze)
    {
        // get tiles that are walkable and not occupied by items && start/goal positions
        var validTiles = maze.WalkableTiles
            .Where(t => !maze.ItemGrid.HasItemAt(t.x, t.y) && t != maze.StartPosition && t != maze.GoalPosition)
            .ToList();

        return validTiles.Count > 0 ? validTiles[Rand.Next(validTiles.Count)] : null;
    }


    // Method Used to gent randomized walkable tiles for bath item placement (only one random operation)
    public static List<(int x, int y)> ShuffleWalkableTiles(Maze maze)
    {
        return maze.WalkableTiles.OrderBy(_ => Rand.Next()).ToList();
    }
}