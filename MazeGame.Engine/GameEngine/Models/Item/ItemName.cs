using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGame.Engine.GameEngine.Models.Item
{
    public enum ItemName
    {
        Key,
        Potion,
        Lantern,
        Compass,
        Door,
        TeleportCircle, // walkable
        Trap, // walkable
        Start,
        Goal
    }

}
