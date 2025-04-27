using MazeGame.Engine.GameEngine.Models.Item;


namespace MazeGame.Engine.API.DTO
{
    public class GameSessionDTO
    {
        public bool GameStarted { get; set; }
        public bool GameRunning { get; set; }
        public bool GameOver { get; set; }
        public bool MazeInitialized { get; set; }
        public bool GoalUnlocked { get; set; }

        public int MaxHearts { get; set; }
        public double CurrentHearts { get; set; }
        public string[] InventorySlots { get; set; } = new string[3];
        public string? StatusEffect { get; set; }
        public int PlayerX { get; set; }
        public int PlayerY { get; set; }
        public string LastMoveDirection { get; set; } = "down";
        public int AnimationFrame { get; set; }

        public int TimerSecondsLeft { get; set; } // (calculated when sent)

        public ItemEffectResult? LastItemEffect { get; set; }

        public static GameSessionDTO From(GameState state)
        {
            return new GameSessionDTO
            {
                GameStarted = state.GameStarted,
                GameRunning = state.GameRunning,
                GameOver = state.GameOver,
                MazeInitialized = state.MazeInitialized,
                GoalUnlocked = state.GoalUnlocked,

                MaxHearts = state.MaxHearts,
                CurrentHearts = state.CurrentHearts,
                InventorySlots = state.InventorySlots,
                StatusEffect = state.StatusEffect,

                PlayerX = state.PlayerX,
                PlayerY = state.PlayerY,
                LastMoveDirection = state.LastMoveDirection,
                AnimationFrame = state.AnimationFrame,

                TimerSecondsLeft = (int)Math.Max(0, (state.TimeLimit - (DateTime.Now - state.GameStartTime)).TotalSeconds),

                LastItemEffect = state.LastItemEffect
            };
        }
    }
}
