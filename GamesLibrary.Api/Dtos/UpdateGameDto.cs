namespace GamesLibrary.Api.Dtos;

public record UpdateGameDto(
    string Name, 
    int PlayersMin,
    int PlayersMax,
    int PlayTimeMin,
    int? PlayTimeMax
);