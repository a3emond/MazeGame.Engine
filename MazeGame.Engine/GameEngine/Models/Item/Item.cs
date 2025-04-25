namespace MazeGame.Engine.GameEngine.Models.Item
{
    public class Item
    {
        

        public ItemName Name { get; set; } // Item name (e.g., "Key", "Potion")
        public int X { get; set; } // Grid X position
        public int Y { get; set; } // Grid Y position
        public string Sprite { get; set; } // Path to sprite/image
        public bool Interactable { get; set; } // Can the player interact with it?
        public bool Collectible { get; set; } // Does it go into inventory?
        public bool Walkable { get; set; } // Can the player walk on it?
        public ItemEffect Effect { get; set; } // Effect of the item

        public Item(ItemName name, int x, int y, string sprite, bool walkable = false,
            bool interactable = true, bool collectible = false,
            ItemEffect effect = ItemEffect.None)
        {
            Name = name;
            X = x;
            Y = y;
            Sprite = sprite;
            Walkable = walkable;
            Interactable = interactable;
            Collectible = collectible;
            Effect = effect;
        }

        
    }



    

    


}
