using MazeGame.Engine.GameEngine.Models.Maze;

namespace MazeGame.Engine.GameEngine.Utils;

public static class MazeAnalysis
{
    public static bool IsWithinBounds(Maze maze, int x, int y)
    {
        return x > 0 && y > 0 && x < maze.Width - 1 && y < maze.Height - 1;
    }

    public static List<(int x, int y)> GetNeighbors(Maze maze, int x, int y)
    {
        List<(int x, int y)> neighbors = new();
        if (x > 1) neighbors.Add((x - 1, y));
        if (x < maze.Width - 2) neighbors.Add((x + 1, y));
        if (y > 1) neighbors.Add((x, y - 1));
        if (y < maze.Height - 2) neighbors.Add((x, y + 1));
        return neighbors;
    }

    
    public static (int, int) FindStartPosition(Maze maze)
    {
        if (maze.WalkableTiles.Count == 0)
            throw new InvalidOperationException("No walkable tiles found in the maze.");

        var rand = new Random();
        return maze.WalkableTiles.ElementAt(rand.Next(maze.WalkableTiles.Count));
    }

    public static (int, int) FindGoalPosition(Maze maze, (int, int) start)
    {
        var farthest = start;
        var maxDistance = 0;

        foreach (var (x, y) in maze.WalkableTiles)
        {
            var distance = Math.Abs(x - start.Item1) + Math.Abs(y - start.Item2);
            if (distance > maxDistance)
            {
                maxDistance = distance;
                farthest = (x, y);
            }
        }

        return farthest;
    }
}