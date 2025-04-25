namespace MazeGame.Engine.GameEngine.Models.Player;

public static class PlayerSpriteResolver
{
    private static readonly Dictionary<string, string[]> Animations = new()
    {
        { "up", [
            "/assets/sprites/player/character_walk/up_1.png", "/assets/sprites/player/character_walk/up_2.png",
            "/assets/sprites/player/character_walk/up_3.png", "/assets/sprites/player/character_walk/up_4.png"
        ]},
        { "down", [
            "/assets/sprites/player/character_walk/down_1.png", "/assets/sprites/player/character_walk/down_2.png",
            "/assets/sprites/player/character_walk/down_3.png", "/assets/sprites/player/character_walk/down_4.png"
        ]},
        { "left", [
            "/assets/sprites/player/character_walk/left_1.png", "/assets/sprites/player/character_walk/left_2.png",
            "/assets/sprites/player/character_walk/left_3.png", "/assets/sprites/player/character_walk/left_4.png"
        ]},
        { "right", [
            "/assets/sprites/player/character_walk/right_1.png", "/assets/sprites/player/character_walk/right_2.png",
            "/assets/sprites/player/character_walk/right_3.png", "/assets/sprites/player/character_walk/right_4.png"
        ]}
    };

    public static Dictionary<string, string[]> GetAnimations()
    {
        return Animations;
    }
}