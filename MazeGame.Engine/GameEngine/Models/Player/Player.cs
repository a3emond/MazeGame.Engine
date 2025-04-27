namespace MazeGame.Engine.GameEngine.Models.Player;

public class Player
{
    public int X { get; private set; }
    public int Y { get; private set; }
    public int LightRadius { get; private set; } = 3;
    public string? Direction { get; private set; }
    public int AnimationFrame { get; private set; } = 0;


    public Player(int startX, int startY)
    {
        X = startX;
        Y = startY;
        Direction = "down";
    }

    public void SetPosition(int x, int y)
    {
        X = x;
        Y = y;
    }

    public void IncreaseVision(int amount = 2)
    {
        LightRadius += amount;
    }

    public void TeleportTo(int x, int y)
    {
        X = x;
        Y = y;
    }

    public void SetDirection(string direction)
    {
        Direction = direction;
        AnimationFrame = (AnimationFrame + 1) % 4; // Cycle 0-1-2-3
    }

}