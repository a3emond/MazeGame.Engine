using MazeGame.Engine.GameEngine.Models.Maze;

namespace MazeGame.Engine.GameEngine.Utils;

public static class MazeBuilderUtils
{
    private static readonly Random Rand = new();

    public static List<(int, int)> RandomizedDirections()
    {
        List<(int, int)> directions = new() { (0, -1), (0, 1), (-1, 0), (1, 0) };

        for (var i = directions.Count - 1; i > 0; i--)
        {
            var j = Rand.Next(i + 1);
            (directions[i], directions[j]) = (directions[j], directions[i]);
        }

        return directions;
    }

    public static void FloodFill(Maze maze, int x, int y, int regionId, Dictionary<(int, int), int> regions)
    {
        Queue<(int, int)> queue = new();
        queue.Enqueue((x, y));

        while (queue.Count > 0)
        {
            var (cx, cy) = queue.Dequeue();
            if (regions.ContainsKey((cx, cy))) continue;

            regions[(cx, cy)] = regionId;

            foreach (var (nx, ny) in MazeAnalysis.GetNeighbors(maze, cx, cy))
                if (maze.Grid[nx, ny] == (int)TileType.FloorCenter && !regions.ContainsKey((nx, ny)))
                    queue.Enqueue((nx, ny));
        }
    }


    public static void CarvePath(Maze maze, int cx, int cy, int nx, int ny)
    {
        var x1 = cx;
        var y1 = cy;
        var x2 = nx;
        var y2 = ny;

        for (var i = -1; i <= 1; i++)
        for (var j = -1; j <= 1; j++)
        {
            var clearX1 = x1 + i;
            var clearY1 = y1 + j;
            var clearX2 = x2 + i;
            var clearY2 = y2 + j;

            if (MazeAnalysis.IsWithinBounds(maze, clearX1, clearY1))
                maze.Grid[clearX1, clearY1] = (int)TileType.FloorCenter;

            if (MazeAnalysis.IsWithinBounds(maze, clearX2, clearY2))
                maze.Grid[clearX2, clearY2] = (int)TileType.FloorCenter;
        }

        var midX = (x1 + x2) / 2;
        var midY = (y1 + y2) / 2;

        for (var i = -1; i <= 1; i++)
        for (var j = -1; j <= 1; j++)
        {
            var newX = midX + i;
            var newY = midY + j;

            if (MazeAnalysis.IsWithinBounds(maze, newX, newY))
                maze.Grid[newX, newY] = (int)TileType.FloorCenter;
        }
    }
}