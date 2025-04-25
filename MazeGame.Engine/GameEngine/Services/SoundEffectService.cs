using MazeGame.Engine.GameEngine.Models.Others;
using MazeGame.Engine.API.DTO;

namespace MazeGame.Engine.GameEngine.Services
{
    public static class SoundEffectService
    {
        private static readonly Dictionary<string, string> SoundEffects = new()
        {
            { "Heal", AudioTracks.PotionPickup },
            { "PowerUp", AudioTracks.PowerUp },
            { "Teleport", AudioTracks.Teleport },
            { "TrapDamage", AudioTracks.TrapDamage },
            { "Win", AudioTracks.WinGame },
            { "GameOver", AudioTracks.GameOver }
        };

        /// <summary>
        /// Returns a full DTO containing all event sound effects.
        /// </summary>
        public static SoundEffectMapDTO GetSoundMap()
        {
            return new SoundEffectMapDTO
            {
                SoundEffects = new Dictionary<string, string>(SoundEffects)
            };
        }

        /// <summary>
        /// Resolves a sound path based on a known sound event (e.g., "Heal", "Teleport", "Win").
        /// </summary>
        public static string? GetSoundPath(string eventName)
        {
            return SoundEffects.TryGetValue(eventName, out var path) ? path : null;
        }
    }
}