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
    )
];

app.MapGet("games", () => games);
app.MapGet("games/{id:int}", (int id) => games.Find((game) => game.Id == id));

app.Run();
