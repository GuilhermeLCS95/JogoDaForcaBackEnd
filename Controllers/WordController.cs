using JogoDaForca.Services;
using JogoDaForca.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JogoDaForca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordController : ControllerBase
    {
        private readonly IWordService _wordService;
        private readonly IGameResponseService _gameResponseService;

        public WordController(IWordService wordServices, IGameResponseService gameResponseService)
        {
            _wordService = wordServices;
            _gameResponseService = gameResponseService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var word = await _wordService.GetWord();
            return Ok(word);
        }

        [HttpPut]
        public async Task<IActionResult> Update()
        {
            var word = await _wordService.UpdateWord();
            _gameResponseService.ResetGame();
            return Ok(word);  
        }
    }
}
