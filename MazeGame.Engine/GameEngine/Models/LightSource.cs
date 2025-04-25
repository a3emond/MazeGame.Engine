namespace MazeGame.Engine.GameEngine.Models
{
    public class LightSource
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Radius { get; set; }

        public LightSource(int x, int y, int radius)
        {
            X = x;
            Y = y;
            Radius = radius;
        }
    }
}
