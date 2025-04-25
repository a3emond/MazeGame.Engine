using MazeGame.Engine.GameEngine.Models;

namespace MazeGame.Engine.GameEngine.GeneratingAlgorithms;

public interface IMazeAlgorithm
{
    void Generate(Maze maze);
}