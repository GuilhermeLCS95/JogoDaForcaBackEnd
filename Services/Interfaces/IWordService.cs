using JogoDaForca.Models;

namespace JogoDaForca.Services.Interfaces
{
    public interface IWordService
    {
        Task<WordModel> GetWord();
        Task<WordModel> UpdateWord();
    }
}
