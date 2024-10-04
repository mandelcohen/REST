using Microsoft.AspNetCore.Mvc;

namespace TicTacToe;

[ApiController]
[Route("api/[controller]")]
public class GameController : ControllerBase
{
    private readonly IGameService _gameService;

    public GameController(IGameService gameService)
    {
        _gameService = gameService;
    }

    [HttpPost("start")]
    public ActionResult<Game> StartGame([FromBody] Player players)
    {
        var game = _gameService.StartGame(players.Id, players.Name);
        return Ok(game);
    }

    [HttpPost("{gameId}/move")]
    public ActionResult<Game> MakeMove(string gameId, [FromBody] Move move)
    {
        var game = _gameService.MakeMove(gameId, move.Row, move.Col, move.PlayerId);
        return Ok(game);
    }

    [HttpGet("{gameId}")]
    public ActionResult<Game> GetGame(string gameId)
    {
        var game = _gameService.GetGame(gameId);
        return Ok(game);
    }

    [HttpPost("{gameId}/rematch")]
    public ActionResult<Game> Rematch(string gameId)
    {
        var game = _gameService.Rematch(gameId);
        return Ok(game);
    }
}
