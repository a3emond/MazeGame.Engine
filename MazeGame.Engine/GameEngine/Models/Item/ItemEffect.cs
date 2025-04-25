namespace MazeGame.Engine.GameEngine.Models.Item
{
    public enum ItemEffect
    {
        None,        // No effect (default)
        Heal,        // Restore HP
        Damage,      // Inflict damage
        LightRadiusIncrease, // Increase light radius
        Unlock,      // Unlock a door or mechanism
        ShowDirection,// Compass effect to show direction
        Teleport    // Teleport to another location
    }
}
