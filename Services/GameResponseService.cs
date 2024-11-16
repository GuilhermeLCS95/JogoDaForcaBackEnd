using JogoDaForca.Models;
using JogoDaForca.Services.Interfaces;
using System.Globalization;
using System.Text;

namespace JogoDaForca.Services
{
    public class GameResponseService : IGameResponseService
    {
        private GameResponseModel _gameResponseModel;

        public GameResponseService()
        {
            _gameResponseModel = new GameResponseModel();
        }

        public void ResetGame()
        {
            _gameResponseModel = new GameResponseModel
            {
                Attempts = 3,
                LetterFound = false,
                GameOver = false,
                PlayerWon = false,
                Message = string.Empty,
                LetterAndPosition = new List<(string Letter, int Position)>()
            };
        }

        public async Task<GameResponseModel> GameProcess(IWordService word, GuessModel guessModel)
        {
            if (_gameResponseModel.GameOver || _gameResponseModel.PlayerWon)
            {
                return _gameResponseModel;
            }

            var wordModel = await word.GetWord();

            var guess = RemoveAccents(char.ToUpper(guessModel.GuessLetter));
            var letterAndPosition = new List<(string Letter, int Position)>();
            var originalWord = wordModel.nome_pais.ToUpper();
            var normalizedWord = RemoveAccents(originalWord);

            AllCharactersFound(normalizedWord);

            bool letterFound = false;

            int attempts = _gameResponseModel.Attempts;

            _gameResponseModel.Message = "A letra " + guess + " foi encontrada na(s) posição(s): ";

            for (int i = 0; i < normalizedWord.Length; i++)
            {

                if (normalizedWord[i] == guess)
                {
                    letterAndPosition.Add((normalizedWord[i].ToString(), i + 1));
                    letterFound = true;
                    _gameResponseModel.Message += (i + 1) + ", ";
                }

            }

            if (letterFound)
            {
                _gameResponseModel.Message.TrimEnd(',', ' ');
                _gameResponseModel.GuessLettersFound++;

                if(_gameResponseModel.DistinctLetters == _gameResponseModel.GuessLettersFound)
                {
                    PlayerWon();
                }
            }
            else
            {
                attempts--;
                _gameResponseModel.Message = "A letra " + guess + " não encontrada. Tentativas restantes: " + attempts;

                if (attempts == 0)
                {
                    IsItGameOVer();
                }
            }

            _gameResponseModel.Attempts = attempts;
            _gameResponseModel.LetterFound = letterFound;
            _gameResponseModel.LetterAndPosition = letterAndPosition;

            return _gameResponseModel;
        }

        private void IsItGameOVer()
        {
            _gameResponseModel.Message = "Você perdeu.";
            _gameResponseModel.GameOver = true;
        }

        private int AllCharactersFound(string word)
        {
            HashSet<char> guess = new HashSet<char>();
            for (int i = 0; i < word.Length; i++)
            {
                guess.Add(word[i]);
            }
            _gameResponseModel.DistinctLetters = guess.Count;
            return _gameResponseModel.DistinctLetters;
        }

        private void PlayerWon()
        {
            _gameResponseModel.Message = "Você descobriu a palavra. Parabéns!";
            _gameResponseModel.PlayerWon = true;
        }


        private string RemoveAccents(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }
            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        private char RemoveAccents(char c)
        {
            return RemoveAccents(c.ToString())[0];
        }
    }
}
