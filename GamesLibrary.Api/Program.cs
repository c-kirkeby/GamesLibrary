using GamesLibrary.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

List<GameDto> games =
[
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

app.MapGet("games", () => games);
app.MapGet("games/{id:int}", (int id) => games.Find((game) => game.Id == id)).WithName("GetGame");
app.MapPost("games", (CreateGameDto createGameDto) =>
{
    GameDto game = new(
        games.Count +1,
        createGameDto.Name, 
        createGameDto.PlayersMin,
        createGameDto.PlayersMax,
        createGameDto.PlayTimeMin,
        createGameDto.PlayTimeMax
    );
    games.Add(game);

    return Results.CreatedAtRoute("GetGame", new { id = game.Id }, game);
});

app.MapPut("games/{id:int}", (int id, UpdateGameDto updateGameDto) =>
{
    var index = games.FindIndex(game => game.Id == id);

    games[index] = new GameDto(
        id,
        updateGameDto.Name,
        updateGameDto.PlayersMin,
        updateGameDto.PlayersMax,
        updateGameDto.PlayTimeMin,
        updateGameDto.PlayTimeMax
    );

    return Results.NoContent();
});

app.Run();
