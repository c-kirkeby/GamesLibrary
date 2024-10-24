using GamesLibrary.Api.Dtos;

namespace GamesLibrary.Api.Endpoints;

public static class GamesEndpoints
{
    private static readonly List<GameDto> Games = [
        new (
            1,
            "Betrayal at House on the Hill",
            3,
            6,
            60,
            60
        ),
        new (
            2,
            "Carcassonne",
            2,
            5,
            30,
            45
        ),
        new (
            3,
            "Lords of Waterdeep",
            2,
            5,
            60,
            120
        )
    ];

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games");
        group.MapGet("/", () => Games);
        
        group.MapGet("/{id:int}", (int id) =>
        {
           var game = Games.Find((game) => game.Id == id);
        
           return game == null ? Results.NotFound() : Results.Ok(game);
        }).WithName("GetGame");
        
        group.MapPost("/", (CreateGameDto createGameDto) =>
        {
            GameDto game = new(
                Games.Count +1,
                createGameDto.Name, 
                createGameDto.PlayersMin,
                createGameDto.PlayersMax,
                createGameDto.PlayTimeMin,
                createGameDto.PlayTimeMax
            );
            Games.Add(game);
        
            return Results.CreatedAtRoute("GetGame", new { id = game.Id }, game);
        });
        
        group.MapPut("/{id:int}", (int id, UpdateGameDto updateGameDto) =>
        {
            var index = Games.FindIndex(game => game.Id == id);
        
            if (index == -1)
            {
                return Results.NotFound();
            }
        
            Games[index] = new GameDto(
                id,
                updateGameDto.Name,
                updateGameDto.PlayersMin,
                updateGameDto.PlayersMax,
                updateGameDto.PlayTimeMin,
                updateGameDto.PlayTimeMax
            );
        
            return Results.NoContent();
        });
        
        group.MapDelete("/{id:int}", (int id) =>
        {
            Games.RemoveAll(game => game.Id == id);
        
            return Results.NoContent();
        });
        
        return group;
    }
}