using JogoDaForca.Models;

namespace JogoDaForca.Services.Interfaces
{
    public interface IGuessService
    {
        Task<GuessModel> GuessingLetter(char guess);
    }
}
