using System;
using ParaIgre.Api.DTOs;

namespace ParaIgre.Api.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame";

    private static readonly List<GameDTO> games =
[
    new (
            1,
            "Crash Bandicoot",
            "Arcade, Adventure, Platformer, Singleplayer",
            15.99M,
            new DateOnly(2007, 5, 15),
            "Fight the evil mastermind to stop him from gaining world control!",
            "Naughty dog"
        ),
    new (
            2,
            "Elden Ring",
            "RPG, Soulslike, Open-world, Singleplayer, Multiplayer",
            59.99M,
            new DateOnly(2007, 5, 15),
            "Rise, Tarnished, and be guided by grace to brandish the power of the Elden Ring and become an Elden Lord in the Lands Between.",
            "Bandai Namco"
        ),
    new (
            3,
            "Need 4 Speed Underground 2",
            "Racing, Cars, Open-world",
            9.99M,
            new DateOnly(2007, 5, 15),
            "Become the fastest street racer history has ever seen",
            "Softonic"
        )
];

    public static WebApplication MapGamesEndpoints(this WebApplication app)
    {
        // GET /games
        app.MapGet("games", () => games);

        // GET/games/1
        app.MapGet("games/{id}", (int id) =>
        {
            GameDTO? game = games.Find(game => game.Id == id);

            return game is null ? Results.NotFound() : Results.Ok(game);
        })
        .WithName(GetGameEndpointName);

        // POST /games
        app.MapPost("games", (CreateGameDTO newGame) =>
        {
            GameDTO game = new
            (
                games.Count + 1,
                newGame.Name,
                newGame.Tags,
                newGame.Price,
                newGame.ReleaseDate,
                newGame.Description,
                newGame.Studio
            );
            games.Add(game);

            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
        });

        // PUT /games/1
        app.MapPut("games/{id}", (int id, UpdateGameDTO updatedGame) =>
        {
            var index = games.FindIndex(game => game.Id == id);

            if (index == -1)
            {
                return Results.NotFound();
            }

            games[index] = new GameDTO
            (
                id,
                updatedGame.Name,
                updatedGame.Tags,
                updatedGame.Price,
                updatedGame.ReleaseDate,
                updatedGame.Description,
                updatedGame.Studio
            );

            return Results.NoContent();
        });

        // DELETE /games/1
        app.MapDelete("games/{id}", (int id) =>
        {
            games.RemoveAll(game => game.Id == id);

            return Results.NoContent();
        });

        return app;
    }
}
