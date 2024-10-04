namespace TicTacToe;

public interface IGameService
{
    Game StartGame(string player1, string player2);
    Game MakeMove(string gameId, int row, int col, string playerId);
    Game GetGame(string gameId);
    Game Rematch(string gameId);
}

public class GameService : IGameService
{
    private readonly Dictionary<string, Game> _games = new();

    public Game StartGame(string player1, string player2)
    {
        var game = new Game()
        {
            Id = Guid.NewGuid().ToString(),
            Status = GameStatus.Ongoing,
        };
        _games[game.Id] = game;
        return game;
    }

    public Game MakeMove(string gameId, int row, int col, string playerId)
    {
        if (!_games.ContainsKey(gameId))
        {
            throw new ArgumentException("Game not found.");
        }

        var game = _games[gameId];

        // Validate the move
        if (row < 0 || row >= 3 || col < 0 || col >= 3)
        {
            throw new ArgumentException("Move is out of bounds.");
        }

        if (game.Board[row, col] != null)
        {
            throw new InvalidOperationException("Cell is already occupied.");
        }

        // Update the board with the player's move
        game.Board[row, col] = playerId;

        // Check for a win or tie
        if (CheckWin(game, playerId))
        {
            game.Status = GameStatus.Won;
            game.WinnerId = playerId;
        }
        else if (IsTie(game))
        {
            game.Status = GameStatus.Tie;
        }

        return game;
    }

    private bool CheckWin(Game game, string playerId)
    {
        // Check rows, columns, and diagonals for a win
        for (int i = 0; i < 3; i++)
        {
            if ((game.Board[i, 0] == playerId && game.Board[i, 1] == playerId && game.Board[i, 2] == playerId) || // Check rows
                (game.Board[0, i] == playerId && game.Board[1, i] == playerId && game.Board[2, i] == playerId)) // Check columns
            {
                return true;
            }
        }

        // Check diagonals
        if ((game.Board[0, 0] == playerId && game.Board[1, 1] == playerId && game.Board[2, 2] == playerId) || 
            (game.Board[0, 2] == playerId && game.Board[1, 1] == playerId && game.Board[2, 0] == playerId))
        {
            return true;
        }

        return false;
    }

    private bool IsTie(Game game)
    {
        foreach (var cell in game.Board)
        {
            if (cell == null) // If there's any empty cell, it's not a tie yet
            {
                return false;
            }
        }
        return true; // All cells are filled, and no winner
    }

    public Game GetGame(string gameId)
    {
        return _games.TryGetValue(gameId, out var game) ? game : null;
    }

    public Game Rematch(string gameId)
    {
        throw new InvalidOperationException();
    }
}
