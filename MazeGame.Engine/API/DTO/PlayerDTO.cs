using MazeGame.Engine.GameEngine.Models.Player;
namespace MazeGame.Engine.API.DTO
{
    public class PlayerDTO
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Direction { get; set; } = "down";
        public int LightRadius { get; set; }
        public int AnimationFrame { get; set; } = 0;

        public Dictionary<string, string[]>? Animations { get; set; } // Optional animation map

        public static PlayerDTO From(Player player, bool includeAnimations = false)
        {
            return new PlayerDTO
            {
                X = player.X,
                Y = player.Y,
                Direction = player.Direction ?? "down",
                LightRadius = player.LightRadius,
                AnimationFrame = player.AnimationFrame,
                Animations = includeAnimations ? PlayerSpriteResolver.GetAnimations() : null
            };
        }
    }
}