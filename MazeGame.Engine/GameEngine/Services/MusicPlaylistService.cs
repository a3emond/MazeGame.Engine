using MazeGame.Engine.GameEngine.Models.Others;
using MazeGame.Engine.API.DTO;

namespace MazeGame.Engine.GameEngine.Services
{
    public static class MusicPlaylistService
    {
        /// <summary>
        /// Returns the background music playlist DTO.
        /// </summary>
        public static MusicPlaylistDTO GetPlaylist()
        {
            return new MusicPlaylistDTO
            {
                Tracks = AudioTracks.MusicPlaylist.ToList()
            };
        }
    }
}