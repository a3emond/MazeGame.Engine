using MazeGame.Engine.GameEngine.Utils;

namespace MazeGame.Engine.GameEngine.Models.Item
{
    public class ItemGrid
    {
        public int Width { get; private set; } // 5x logical width
        public int Height { get; private set; } // 5x logical height
        private List<Item> Items { get; set; }

        public ItemGrid(int logicalWidth, int logicalHeight)
        {
            Width = logicalWidth * 5;
            Height = logicalHeight * 5;
            Items = new List<Item>();
        }

        public void AddItem(ItemName name, int x, int y, string sprite,
            bool walkable, bool interactable, bool collectible,
            ItemEffect effect)
        {
            Items.Add(new Item(name, x, y, sprite, walkable, interactable, collectible, effect));
        }

        public void RemoveItem(int x, int y)
        {
            Items.RemoveAll(item => item.X == x && item.Y == y);
        }

        public Item? GetItemAt(int x, int y)
        {
            return Items.FirstOrDefault(item => item.X == x && item.Y == y);
        }

        public List<Item> GetAllItems()
        {
            return Items;
        }
        public bool HasItemAt(int x, int y)
        {
            return Items.Any(item => item.X == x && item.Y == y);
        }

        public bool IsItemCollectible(int x, int y) => GetItemAt(x, y)?.Collectible ?? false;

        public bool IsItemInteractable(int x, int y) => GetItemAt(x, y)?.Interactable ?? false;

        public bool IsItemWalkable(int x, int y) => GetItemAt(x, y)?.Walkable ?? false;

        public ItemEffect GetItemEffect(int x, int y) => GetItemAt(x, y)?.Effect ?? ItemEffect.None;
    }
}