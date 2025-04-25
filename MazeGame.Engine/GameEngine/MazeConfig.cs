using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGame.Engine.GameEngine
{
    public static class MazeConfig
    {
        public const int MazeWidth = 40;
        public const int MazeHeight = 40;
        public const int TileSize = 16; // Size of each tile in pixels
        public const int LightRadius = 3; // Default light radius
        public const int MaxHearts = 3; // Maximum hearts for the player
        public const int TimerSeconds = 300; // Game timer in seconds (5 minutes)
        public const string DefaultDirection = "down"; // Default starting direction

        // Starting items quantity
        public const int KeyQuantity = 1;
        public const int PotionQuantity = 15;
        public const int LanternQuantity = 6;
        public const int CompassQuantity = 1;
        public const int TeleportQuantity = 20;
        public const int TrapQuantity = 200;
    }
}
