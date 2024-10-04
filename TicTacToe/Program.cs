
using TicTacToe;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IGameService, GameService>(); // Register your game service

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Define your API endpoints
app.MapPost("/api/game/start", (Player players, IGameService gameService) =>
{
    var game = gameService.StartGame(players.Id, players.Name);
    return Results.Ok(game);
});

app.MapPost("/api/game/{gameId}/move", (string gameId, Move move, IGameService gameService) =>
{
    var game = gameService.MakeMove(gameId, move.Row, move.Col, move.PlayerId);
    return Results.Ok(game);
});

app.MapGet("/api/game/{gameId}", (string gameId, IGameService gameService) =>
{
    var game = gameService.GetGame(gameId);
    return game != null ? Results.Ok(game) : Results.NotFound();
});

app.MapPost("/api/game/{gameId}/rematch", (string gameId, IGameService gameService) =>
{
    var game = gameService.Rematch(gameId);
    return game != null ? Results.Ok(game) : Results.NotFound();
});

app.Run();


