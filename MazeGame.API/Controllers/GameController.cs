using Microsoft.AspNetCore.Mvc;
using MazeGame.Engine.API;
using MazeGame.Engine.API.DTO;
using MazeGame.Engine.GameEngine.GeneratingAlgorithms;
using MazeGame.API.DTO;

namespace MazeGame.API.Controllers
{
    [ApiController]
    [Route("api/game")]
    public class GameController : ControllerBase
    {
        private readonly GameServiceFactory _factory;

        public GameController(GameServiceFactory factory)
        {
            _factory = factory;
        }

        [HttpPost("init")]
        public IActionResult InitializeMaze([FromBody] InitRequest request)
        {
            var service = _factory.GetOrCreateService();
            service.ResetGameState(); // Clear previous session if needed
            service.InitializeMaze(request.Algorithm);
            return Ok();
        }

        [HttpPost("start")]
        public IActionResult StartGame()
        {
            var service = _factory.GetOrCreateService();
            service.StartGame();
            return Ok();
        }

        

        [HttpGet("state")]
        public ActionResult<GameSessionDTO> GetGameState()
        {
            var service = _factory.GetOrCreateService();
            return Ok(service.GetSession());
        }

        

        [HttpGet("algorithms")]
        public ActionResult<MazeAlgorithmListDTO> GetAvailableAlgorithms()
        {
            var service = _factory.GetOrCreateService();
            return Ok(service.GetAvailableAlgorithms());
        }

        [HttpGet("soundeffects")]
        public ActionResult<SoundEffectMapDTO> GetSoundEffects()
        {
            var service = _factory.GetOrCreateService();
            return Ok(service.GetSoundEffectMapDto());
        }

        [HttpGet("music")]
        public ActionResult<MusicPlaylistDTO> GetMusicPlaylist()
        {
            var service = _factory.GetOrCreateService();
            return Ok(service.GetMusicPlaylistDto());
        }

        [HttpPost("load")]
        public ActionResult<GameLoadDTO> LoadGame([FromBody] LoadRequest? request)
        {
            var service = _factory.GetOrCreateService();
            service.ResetGameState();

            // Determine algorithm
            MazeAlgorithmType algorithm = MazeAlgorithmType.RecursiveBacktracking; // Default

            if (!string.IsNullOrEmpty(request?.Algorithm))
            {
                if (Enum.TryParse<MazeAlgorithmType>(request.Algorithm, ignoreCase: true, out var parsed))
                {
                    algorithm = parsed;
                }
            }

            service.InitializeMaze(algorithm);
            service.StartGame();

            var dto = service.BuildGameLoadDTO();
            return Ok(dto);
        }

        [HttpPost("state/save")]
        public IActionResult SaveGameState([FromBody] GameSessionDTO sessionDto)
        {
            var service = _factory.GetOrCreateService();
            service.SetSession(sessionDto);
            return Ok();
        }

    }
}
