using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGame.Engine.GameEngine.Models.Item
{
    public class ItemEffectResult
    {
        public double HealChange { get; set; } = 0;
        public double DamageTaken { get; set; } = 0;
        public bool GoalUnlocked { get; set; } = false;
        public bool Teleported { get; set; } = false;
        public int? NewX { get; set; } = null;
        public int? NewY { get; set; } = null;
        public bool VisionIncreased { get; set; } = false;
        public string? StatusEffect { get; set; } = null;
        public string? Sound { get; set; } = null;
        public string? CollectedSprite { get; set; } = null;
    }
}
