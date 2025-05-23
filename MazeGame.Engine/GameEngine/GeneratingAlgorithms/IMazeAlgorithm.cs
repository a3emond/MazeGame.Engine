﻿using MazeGame.Engine.GameEngine.Models.Maze;

namespace MazeGame.Engine.GameEngine.GeneratingAlgorithms;

public interface IMazeAlgorithm
{
    void Generate(Maze maze);
}