using JogoDaForca.Models;

namespace JogoDaForca.Services.Interfaces
{
    public interface IWordServices
    {
        Task<WordModel> GetWord();
        Task<WordModel> UpdateWord();
    }
}
