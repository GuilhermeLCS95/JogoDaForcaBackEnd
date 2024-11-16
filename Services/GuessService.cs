using JogoDaForca.Models;
using JogoDaForca.Services.Interfaces;
using System.Globalization;
using System.Text;

namespace JogoDaForca.Services
{
    public class GuessService : IGuessService
    {
        private readonly GuessModel _guessModel;

        public GuessService()
        {
            _guessModel = new GuessModel(); 
        }

        public async Task<GuessModel> GuessingLetter(char guess)
        {
            _guessModel.GuessLetter = guess; 
           return await Task.FromResult(_guessModel); 
        }
    }

}
