using MazeGame.Engine.API.DTO;
using MazeGame.Engine.GameEngine.GeneratingAlgorithms;
using MazeGame.Engine.GameEngine.Models.Item;
using MazeGame.Engine.GameEngine.Models.Maze;
using MazeGame.Engine.GameEngine.Models.Player;
using MazeGame.Engine.GameEngine.Services;

namespace MazeGame.Engine.API
{
    public class MazeGameService
    {
        private readonly MazeGameCore _core;
        private readonly GameState _state;

        public MazeGameService(GameState state)
        {
            _state = state;
            _core = new MazeGameCore(this, state);
        }

        // =====================
        // 📌 Gameplay Actions
        // =====================

        public void InitializeMaze(MazeAlgorithmType algorithm)
        {
            _core.InitializeMaze(algorithm);
        }


        public void StartGame() => _core.StartGame();

        public string? MovePlayer(string direction)
        {
            return _core.Step(direction); // Returns "Win", "GameOver", or null
        }

        public bool UpdateGameTimer() => _core.UpdateTimer();

        // =====================
        // 📤 DTO Exposure
        // =====================

        public GameSessionDTO GetSession() => GameSessionDTO.From(_state);

        public MazeAlgorithmListDTO GetAvailableAlgorithms()
        {
            var list = Enum.GetNames(typeof(MazeAlgorithmType)).ToList();

            return new MazeAlgorithmListDTO
            {
                AvailableAlgorithms = list
            };
        }


        public MazeGridDTO GetMazeGridDTO(bool includeSprites = true)
        {
            if (_state.Maze == null)
                throw new InvalidOperationException("Maze not initialized.");

            return MazeGridDTO.From(_state.Maze, includeSprites);
        }

        public ItemGridDTO GetItemGridDTO(bool includeSprites = true)
        {
            if (_state.Maze == null)
                throw new InvalidOperationException("Maze not initialized.");

            return ItemGridDTO.From(_state.Maze.ItemGrid, includeSprites);
        }

        public PlayerDTO GetPlayerDTO(bool includeAnimations = true)
        {
            if (_state.Player == null)
                throw new InvalidOperationException("Player not initialized.");

            return PlayerDTO.From(_state.Player, includeAnimations);
        }

        public SoundEffectMapDTO GetSoundEffectMapDto() => SoundEffectService.GetSoundMap();

        public MusicPlaylistDTO GetMusicPlaylistDto() => MusicPlaylistService.GetPlaylist();
    }
}
