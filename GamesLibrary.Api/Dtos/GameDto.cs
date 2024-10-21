namespace GamesLibrary.Api.Dtos;

public record GameDto(
    int Id, 
    string Name, 
    int PlayersMin,
    int PlayersMax,
    int PlayTimeMin,
    int PlayTimeMax
);