namespace TicTacToe;

public class Game
{
    public string Id { get; set; }
    public string[,] Board { get; set; } 
    public GameStatus Status { get; set; } 
    public string WinnerId { get; set; } 

    public Game()
    {
        Id = null;
        Board = new string[3, 3]; 
        Status = GameStatus.Ongoing;
        WinnerId = null;
    }
}

public enum GameStatus
{
    Ongoing,
    Won,
    Tie
}