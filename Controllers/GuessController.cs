using JogoDaForca.Models;
using JogoDaForca.Services;
using JogoDaForca.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JogoDaForca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuessController : ControllerBase
    {
        private readonly IGuessService _guessService;
        private readonly IGameResponseService _gameResponseService;
        private readonly IWordService _wordService;

        public GuessController(IGameResponseService gameResponseService, IWordService wordService)
        {
            _gameResponseService = gameResponseService ?? throw new ArgumentNullException(nameof(gameResponseService));
            _wordService = wordService ?? throw new ArgumentNullException(nameof(wordService));
        }

        [HttpPost]
        public async Task<IActionResult> Guess([FromBody] GuessModel guessModel)
        {
            var gameResponse = await _gameResponseService.GameProcess(_wordService, guessModel);
            return Ok(gameResponse);
        }

    }
}
