using MazeGame.Engine.API.DTO;
using MazeGame.Engine.GameEngine.GeneratingAlgorithms;
using MazeGame.Engine.GameEngine.Models.Maze;
using MazeGame.Engine.GameEngine.Models.Player;
using MazeGame.Engine.GameEngine.Services;

namespace MazeGame.Engine.API
{
    public class MazeGameService
    {
        private MazeGameCore _core;
        private readonly GameState _state;

        public MazeGameService(GameState state)
        {
            _state = state;
            _core = new MazeGameCore(this, state);
        }

        // =====================
        // 📌 Gameplay Actions
        // =====================

        public void ResetGameState()
        {
            _core.ResetGame();
        }

        public void InitializeMaze(MazeAlgorithmType algorithm) => _core.InitializeMaze(algorithm);

        public void StartGame() => _core.StartGame();

        // =====================
        // 📤 DTO Exposure
        // =====================

        public GameLoadDTO BuildGameLoadDTO()
        {
            if (_state.Maze == null || _state.Player == null)
                throw new InvalidOperationException("Maze or Player not initialized.");

            var tileGrid = new List<string>(_state.Maze.Width * _state.Maze.Height);
            for (int y = 0; y < _state.Maze.Height; y++)
            {
                for (int x = 0; x < _state.Maze.Width; x++)
                {
                    var tileType = (TileType)_state.Maze.Grid[x, y];
                    tileGrid.Add(tileType.GetTileSprite());
                }
            }

            var items = _state.Maze.ItemGrid.GetAllItems()
                .Select(item => new ItemLoadDTO
                {
                    X = item.X,
                    Y = item.Y,
                    Effect = item.Effect.ToString(),
                    SpritePath = item.Sprite,
                    Collectible = item.Collectible
                })
                .ToList();

            var playerAnimations = PlayerSpriteResolver.GetAnimations()
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToList());

            return new GameLoadDTO
            {
                TileGrid = tileGrid,
                MazeWidth = _state.Maze.Width,
                MazeHeight = _state.Maze.Height,
                Items = items,
                StartX = _state.Maze.StartPosition.x,
                StartY = _state.Maze.StartPosition.y,
                GoalX = _state.Maze.GoalPosition.x,
                GoalY = _state.Maze.GoalPosition.y,
                PlayerAnimations = playerAnimations,
                DefaultLightRadius = _state.Player.LightRadius,
                MaxHearts = _state.MaxHearts,
                StartingHearts = _state.CurrentHearts,
                TimeLimitSeconds = (int)_state.TimeLimit.TotalSeconds
            };
        }

        public GameSessionDTO GetSession() => GameSessionDTO.From(_state);

        public void SetSession(GameSessionDTO dto)
        {
            if (_state.Player == null)
                throw new InvalidOperationException("Player must be initialized before setting session.");

            _state.GameStarted = dto.GameStarted;
            _state.GameRunning = dto.GameRunning;
            _state.GameOver = dto.GameOver;
            _state.MazeInitialized = dto.MazeInitialized;
            _state.GoalUnlocked = dto.GoalUnlocked;

            _state.MaxHearts = dto.MaxHearts;
            _state.CurrentHearts = dto.CurrentHearts;
            _state.InventorySlots = dto.InventorySlots;
            _state.StatusEffect = dto.StatusEffect;

            _state.Player.SetPosition(dto.PlayerX, dto.PlayerY);
            _state.Player.SetDirection(dto.LastMoveDirection);

            _state.LastItemEffect = dto.LastItemEffect;
        }


        public MazeAlgorithmListDTO GetAvailableAlgorithms()
        {
            var list = Enum.GetNames(typeof(MazeAlgorithmType)).ToList();
            return new MazeAlgorithmListDTO
            {
                AvailableAlgorithms = list
            };
        }


        public SoundEffectMapDTO GetSoundEffectMapDto() => SoundEffectService.GetSoundMap();

        public MusicPlaylistDTO GetMusicPlaylistDto() => MusicPlaylistService.GetPlaylist();
    }
}
