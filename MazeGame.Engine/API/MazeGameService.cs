using MazeGame.Engine.API.DTO;
using MazeGame.Engine.GameEngine.GeneratingAlgorithms;
using MazeGame.Engine.GameEngine.Services;

namespace MazeGame.Engine.API
{
    public class MazeGameService //	API exposure layer: Converts GameState into DTOs. Accepts frontend inputs (SetSession, MovePlayer, etc.)
    {
        private readonly MazeGameCore _core;
        private readonly GameState _state;

        public MazeGameService(GameState state)
        {
            _state = state;
            _core = new MazeGameCore(this, state); // start an instance of the game core
        }

        // =====================
        // 📌 Gameplay Actions
        // =====================

        public void ResetGameState()
        {
            _state.Reset();
            _core = new MazeGameCore(this, _state);
        }

        public void InitializeMaze(MazeAlgorithmType algorithm) => _core.InitializeMaze(algorithm);

        public void StartGame() => _core.StartGame();

        public void MovePlayer(string direction) => _core.Step(direction);


        // =====================
        // 📤 DTO Exposure
        // =====================

        // this is used to send the game session state to the frontend
        public GameSessionDTO GetSession() => GameSessionDTO.From(_state);

        // this is used to send the available algorithms to the frontend for dropdowns 
        public MazeAlgorithmListDTO GetAvailableAlgorithms()
        {
            var list = Enum.GetNames(typeof(MazeAlgorithmType)).ToList();

            return new MazeAlgorithmListDTO
            {
                AvailableAlgorithms = list
            };
        }

        // this is used to send a lightweight version of the maze with sprite mapping to the frontend for renderer
        public MazeGridDTO GetMazeGridDTO(bool includeSprites = true)
        {
            if (_state.Maze == null)
                throw new InvalidOperationException("Maze not initialized.");

            return MazeGridDTO.From(_state.Maze, includeSprites);
        }

        // this is used to send the item grid with sprite mapping to the frontend for renderer
        public ItemGridDTO GetItemGridDTO(bool includeSprites = true)
        {
            if (_state.Maze == null)
                throw new InvalidOperationException("Maze not initialized.");

            return ItemGridDTO.From(_state.Maze.ItemGrid, includeSprites);
        }

        // this is used to send the player object with animation mapping to the frontend for renderer
        public PlayerDTO GetPlayerDTO(bool includeAnimations = true)
        {
            if (_state.Player == null)
                throw new InvalidOperationException("Player not initialized.");

            return PlayerDTO.From(_state.Player, includeAnimations);
        }

        // this is used to send the sound effect map to the frontend for renderer
        public SoundEffectMapDTO GetSoundEffectMapDto() => SoundEffectService.GetSoundMap();
        // simple getter for the music playlist
        public MusicPlaylistDTO GetMusicPlaylistDto() => MusicPlaylistService.GetPlaylist();
    }
}
