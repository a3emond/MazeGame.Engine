using MazeGame.Engine.GameEngine.Models.Maze;
using MazeGame.Engine.GameEngine.Models.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGame.Engine.GameEngine.Services
{
    public static class PlayerMovementService
    {
        public static void Move(Player player, string direction, Maze maze)
        {
            player.SetDirection(direction);

            var (x, y) = (player.X, player.Y);
            switch (direction)
            {
                case "up":
                    if (maze.IsWalkable(x, y - 1)) player.SetPosition(x, y - 1);
                    break;
                case "down":
                    if (maze.IsWalkable(x, y + 1)) player.SetPosition(x, y + 1);
                    break;
                case "left":
                    if (maze.IsWalkable(x - 1, y)) player.SetPosition(x - 1, y);
                    break;
                case "right":
                    if (maze.IsWalkable(x + 1, y)) player.SetPosition(x + 1, y);
                    break;
            }
        }
    }
}

