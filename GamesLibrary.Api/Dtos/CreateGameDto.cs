namespace GamesLibrary.Api.Dtos;

public record CreateGameDto(
    int Id, 
    string Name, 
    int PlayersMin,
    int PlayersMax,
    int PlayTimeMin,
    int? PlayTimeMax
);
