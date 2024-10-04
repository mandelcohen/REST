namespace TicTacToe.Models;

public class Game
{
    public string Id { get; set; }
    public string[,] Board { get; set; } = new string[3, 3]; // 3x3 board
    public string CurrentPlayer { get; set; }
    public bool IsFinished { get; set; }
}