namespace MazeGame.API.DTO
{
    public class LoadRequest
    {
        public string? Algorithm { get; set; } // Optional; if null, use default RecursiveBacktracking
    }
}