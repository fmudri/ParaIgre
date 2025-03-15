using System;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ParaIgre.Api.Data;
using ParaIgre.Api.DTOs;
using ParaIgre.Api.Entities;
using ParaIgre.Api.Mapping;

namespace ParaIgre.Api.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame";

    private static readonly List<GameSummaryDTO> games =
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

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games").WithParameterValidation();

        // GET /games
        group.MapGet("/", (ParaIgreContext dbContext) => 
        dbContext.Games.Include(game => game.Tag).Select(game => game.ToGameSummaryDto()).AsNoTracking());	

        // GET/games/1
        group.MapGet("/{id}", (int id, ParaIgreContext dbContext) =>
        {
            Game? game = dbContext.Games.Find(id);

            return game is null ? Results.NotFound() : Results.Ok(game.ToGameDetailsDto());
        })
        .WithName(GetGameEndpointName);

        // POST /games
        group.MapPost("/", (CreateGameDTO newGame, ParaIgreContext dbContext) =>
        {

            Game game = newGame.ToEntity();
            game.Tag = dbContext.Tags.Find(newGame.TagId);  

            dbContext.Games.Add(game);
            dbContext.SaveChanges();

            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game.ToGameDetailsDto());
        });

        // PUT /games/1
        group.MapPut("/{id}", (int id, UpdateGameDTO updatedGame, ParaIgreContext dbContext) =>
        {
            var existingGame = dbContext.Games.Find(id);

            if (existingGame is null)
            {
                return Results.NotFound();
            }

            dbContext.Entry(existingGame).CurrentValues.SetValues(updatedGame.ToEntity(id));

            dbContext.SaveChanges();

            return Results.NoContent();
        });

        // DELETE /games/1
        group.MapDelete("/{id}", (int id, ParaIgreContext dbContext) =>
        {
            dbContext.Games.Where(game => game.Id == id).ExecuteDelete();

            return Results.NoContent();
        });

        return group;
    }
}
