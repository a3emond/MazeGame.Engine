using MazeGame.Engine.GameEngine.Models.Item;
using MazeGame.Engine.GameEngine.Models.Maze;
using MazeGame.Engine.GameEngine.Models.Player;
using MazeGame.Engine.GameEngine.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGame.Engine.GameEngine.Services
{
    public static class PlayerInteractionService
    {
        public static ItemEffectResult TryPickupItem(Player player, Maze maze)
        {
            var result = new ItemEffectResult();
            var item = maze.ItemGrid.GetItemAt(player.X, player.Y);

            if (item == null) return result;

            switch (item.Effect)
            {
                case ItemEffect.Heal:
                    result.HealChange = 0.5;
                    result.Sound = "Heal";
                    break;
                case ItemEffect.Unlock:
                    result.GoalUnlocked = true;
                    result.StatusEffect = "Goal Unlocked";
                    result.Sound = "PowerUp";
                    break;
                case ItemEffect.Teleport:
                    var tile = ItemPlacementUtils.GetRandomWalkableTile(maze);
                    if (tile.HasValue)
                    {
                        player.TeleportTo(tile.Value.x, tile.Value.y);
                        result.Teleported = true;
                        result.NewX = player.X;
                        result.NewY = player.Y;
                        result.Sound = "Teleport";
                    }
                    break;
                case ItemEffect.LightRadiusIncrease:
                    player.IncreaseVision();
                    result.VisionIncreased = true;
                    result.StatusEffect = "Vision Radius Increased";
                    result.Sound = "PowerUp";
                    break;
                case ItemEffect.ShowDirection:
                    result.StatusEffect = "Guided";
                    result.Sound = "PowerUp";
                    break;
                case ItemEffect.Damage:
                    result.DamageTaken = 0.25;
                    result.Sound = "TrapDamage";
                    break;
            }

            if (item.Collectible)
            {
                result.CollectedSprite = item.Sprite;
                maze.ItemGrid.RemoveItem(item.X, item.Y);
            }

            return result;
        }
    }

}
