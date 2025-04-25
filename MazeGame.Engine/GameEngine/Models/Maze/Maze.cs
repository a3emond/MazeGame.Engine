using MazeGame.Engine.GameEngine.Models.Item;

namespace MazeGame.Engine.GameEngine.Models.Maze;

public class Maze
{
    public Maze(int width, int height, int cellSize)
    {
        Width = width * cellSize;
        Height = height * cellSize;
        Grid = new int[Width, Height];
        WalkableTiles = new HashSet<(int x, int y)>();
        ItemGrid = new ItemGrid(width, height);
    }

    public int[,] Grid { get; }
    public int Width { get; }
    public int Height { get; }
    public HashSet<(int x, int y)> WalkableTiles { get; }

    public ItemGrid ItemGrid { get; }

    public (int x, int y) StartPosition { get; private set; }
    public (int x, int y) GoalPosition { get; private set; }

    public void SetStartAndGoal((int, int) start, (int, int) goal)
    {
        // Set the start and goal positions
        StartPosition = start;
        GoalPosition = goal;
        // Mark the start and goal positions in the grid
        Grid[start.Item1, start.Item2] = (int)TileType.Start;
        Grid[goal.Item1, goal.Item2] = (int)TileType.Goal;
    }

    public bool IsWalkable(int x, int y)
    {
        return WalkableTiles.Contains((x, y));
    }
}