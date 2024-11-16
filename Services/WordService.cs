using JogoDaForca.Models;
using JogoDaForca.Services.Interfaces;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace JogoDaForca.Services
{
    public class WordService : IWordService
    {
        private readonly HttpClient _httpClient;
        private WordModel _wordModel;

        public WordService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private async Task<WordModel> GetRandomWord()
        {
            var response = await _httpClient.GetAsync("https://gist.githubusercontent.com/jonasruth/61bde1fcf0893bd35eea/raw/10ce80ddeec6b893b514c3537985072bbe9bb265/paises-gentilicos-google-maps.json");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var words = JsonConvert.DeserializeObject<List<WordModel>>(content);

            if(words == null || words.Count == 0)
            {
                return new WordModel { Message = "A lista está vazia ou houve um problema de carregamento." };
            }

            var random = new Random();
            int randomIndex = random.Next(words.Count);
            _wordModel = words[randomIndex];
            return _wordModel;
        }

        public async Task<WordModel> GetWord()
        {
            if (_wordModel == null)
            {
                _wordModel = await GetRandomWord();
            }
            return _wordModel;
        }

        public async Task<WordModel> UpdateWord()
        {
            _wordModel = await GetRandomWord();
            return _wordModel;
        }
    }
}
