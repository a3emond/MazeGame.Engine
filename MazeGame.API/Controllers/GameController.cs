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

        [HttpPost("move")]
        public IActionResult MovePlayer([FromBody] MoveRequest request)
        {
            var service = _factory.GetOrCreateService();
            service.MovePlayer(request.Direction);
            return Ok();
        }

        [HttpGet("state")]
        public ActionResult<GameSessionDTO> GetGameState()
        {
            var service = _factory.GetOrCreateService();
            return Ok(service.GetSession());
        }

        [HttpGet("maze")]
        public ActionResult<MazeGridDTO> GetMazeGrid()
        {
            var service = _factory.GetOrCreateService();
            return Ok(service.GetMazeGridDTO());
        }

        [HttpGet("items")]
        public ActionResult<ItemGridDTO> GetItemGrid()
        {
            var service = _factory.GetOrCreateService();
            return Ok(service.GetItemGridDTO());
        }

        [HttpGet("player")]
        public ActionResult<PlayerDTO> GetPlayer()
        {
            var service = _factory.GetOrCreateService();
            return Ok(service.GetPlayerDTO());
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
    }
}
