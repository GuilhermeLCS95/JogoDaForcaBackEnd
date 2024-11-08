using JogoDaForca.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JogoDaForca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordController : ControllerBase
    {
        private readonly IWordServices _gameServices;

        public WordController(IWordServices gameServices)
        {
            _gameServices = gameServices;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var word = await _gameServices.GetWord();
            return Ok(word);
        }

        [HttpPut]
        public async Task<IActionResult> Update()
        {
            var word = await _gameServices.UpdateWord();
            return Ok(word);  
        }
    }
}
