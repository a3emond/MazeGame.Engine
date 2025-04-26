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
        private readonly MazeGameService _service;

        public GameController(MazeGameService service)
        {
            _service = service;
        }

        [HttpPost("init")]
        public IActionResult InitializeMaze([FromBody] InitRequest request)
        {
            _service.InitializeMaze(request.Algorithm);
            return Ok();
        }

        [HttpPost("start")]
        public IActionResult StartGame()
        {
            _service.StartGame();
            return Ok();
        }

        [HttpPost("move")]
        public IActionResult MovePlayer([FromBody] MoveRequest request)
        {
            _service.MovePlayer(request.Direction);
            return Ok();
        }


        [HttpGet("state")]
        public ActionResult<GameSessionDTO> GetGameState()
        {
            return Ok(_service.GetSession());
        }

        [HttpGet("maze")]
        public ActionResult<MazeGridDTO> GetMazeGrid()
        {
            return Ok(_service.GetMazeGridDTO());
        }

        [HttpGet("items")]
        public ActionResult<ItemGridDTO> GetItemGrid()
        {
            return Ok(_service.GetItemGridDTO());
        }

        [HttpGet("player")]
        public ActionResult<PlayerDTO> GetPlayer()
        {
            return Ok(_service.GetPlayerDTO());
        }

        [HttpGet("algorithms")]
        public ActionResult<MazeAlgorithmListDTO> GetAvailableAlgorithms()
        {
            return Ok(_service.GetAvailableAlgorithms());
        }

        [HttpGet("soundeffects")]
        public ActionResult<SoundEffectMapDTO> GetSoundEffects()
        {
            return Ok(_service.GetSoundEffectMapDto());
        }

        [HttpGet("music")]
        public ActionResult<MusicPlaylistDTO> GetMusicPlaylist()
        {
            return Ok(_service.GetMusicPlaylistDto());
        }
    }
}
