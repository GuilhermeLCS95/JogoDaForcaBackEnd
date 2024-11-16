namespace JogoDaForca.Models
{
    public class GameResponseModel
    {
        public int Attempts { get; set; } = 3;
        public bool LetterFound { get; set; }
        public int DistinctLetters { get; set; }
        public int GuessLettersFound { get; set; }
        public bool GameOver { get; set; }
        public bool PlayerWon { get; set; }
        public string Message { get; set; }
        public List<(string Letter, int Position)> LetterAndPosition { get; set; } = new List<(string, int)>();
    }
}
