namespace MazeGame.Engine.GameEngine.Models.Item
{
    public static class ItemSpriteResolver
    {
        private static readonly Dictionary<ItemName, string> SpritePaths = new()
        {
            { ItemName.Key, "assets/sprites/items/keys/keys_1_1.png" },
            { ItemName.Potion, "assets/sprites/items/potions/potion_large_red.png" },
            { ItemName.Lantern, "assets/sprites/items/torch.png" },
            { ItemName.Compass, "assets/sprites/items/compass.png" },
            { ItemName.Door, "assets/sprites/items/door/door_1.png" },
            { ItemName.TeleportCircle, "assets/sprites/items/portal.png" },
            { ItemName.Trap, "assets/sprites/items/peaks/peaks_1.png" },
            { ItemName.Start, "assets/sprites/items/start.png" },
            { ItemName.Goal, "assets/sprites/items/goal.png" }
        };

        public static string GetItemSprite(this ItemName name)
        {
            return SpritePaths.GetValueOrDefault(name, "assets/sprites/items/unknown.png");
        }
    }
}

