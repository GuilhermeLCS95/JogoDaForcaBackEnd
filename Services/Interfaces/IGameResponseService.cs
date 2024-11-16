using JogoDaForca.Models;

namespace JogoDaForca.Services.Interfaces
{
    public interface IGameResponseService
    {
        Task<GameResponseModel> GameProcess(IWordService word, GuessModel guessModel);
        void ResetGame();
    }
}
