using MazeGame.Engine.GameEngine.Models.Item;
using MazeGame.Engine.GameEngine.Models.Maze;
using MazeGame.Engine.GameEngine.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGame.Engine.GameEngine.Services;

public static class MazeItemSpawner
{
    public static void GenerateItems(Maze maze)
    {
        var itemsToGenerate = new List<(ItemName, bool, bool, bool, ItemEffect, int)>
        {
            (ItemName.Key, false, false, true, ItemEffect.Unlock, MazeConfig.KeyQuantity),
            (ItemName.Potion, false, true, true, ItemEffect.Heal, MazeConfig.PotionQuantity),
            (ItemName.Lantern, false, false, true, ItemEffect.LightRadiusIncrease, MazeConfig.LanternQuantity),
            (ItemName.Compass, false, false, true, ItemEffect.ShowDirection, MazeConfig.CompassQuantity),
            (ItemName.TeleportCircle, true, false, false, ItemEffect.Teleport, MazeConfig.TeleportQuantity),
            (ItemName.Trap, true, false, false, ItemEffect.Damage, MazeConfig.TrapQuantity)
        };

        var tiles = ItemPlacementUtils.ShuffleWalkableTiles(maze);
        int index = 0;

        // Generate items based on the specified quantities using a pre-shuffled list of walkable tiles
        foreach (var (name, walk, interact, collect, effect, count) in itemsToGenerate)
        {
            for (int i = 0; i < count && index < tiles.Count; i++)
            {
                var (x, y) = tiles[index++];
                var sprite = name.GetItemSprite();
                maze.ItemGrid.AddItem(name, x, y, sprite, walk, interact, collect, effect);
            }
        }

        PlaceStartAndGoalMarkers(maze);
    }

    private static void PlaceStartAndGoalMarkers(Maze maze)
    {
        maze.ItemGrid.AddItem(ItemName.Start, maze.StartPosition.x, maze.StartPosition.y,
            ItemName.Start.GetItemSprite(), false, false, false, ItemEffect.None);

        maze.ItemGrid.AddItem(ItemName.Goal, maze.GoalPosition.x, maze.GoalPosition.y,
            ItemName.Goal.GetItemSprite(), false, false, false, ItemEffect.None);
    }

    // Method to spawn a single item at a random walkable tile (safe method that check for used tiles)
    public static void SpawnItem(Maze maze, ItemName name, bool walkable, bool interactable, bool collectible, ItemEffect effect)
    {
        var position = ItemPlacementUtils.GetRandomWalkableTile(maze);

        if (position.HasValue)
        {
            var sprite = name.GetItemSprite();
            maze.ItemGrid.AddItem(name, position.Value.x, position.Value.y, sprite, walkable, interactable, collectible, effect);
        }
    }

}
