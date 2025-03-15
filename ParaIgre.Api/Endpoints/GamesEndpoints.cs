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
        group.MapGet("/", async (ParaIgreContext dbContext) => 
        await dbContext.Games.Include(game => game.Tag).Select(game => game.ToGameSummaryDto()).AsNoTracking().ToListAsync());	

        // GET/games/1
        group.MapGet("/{id}", async (int id, ParaIgreContext dbContext) =>
        {
            Game? game = await dbContext.Games.FindAsync(id);

            return game is null ? Results.NotFound() : Results.Ok(game.ToGameDetailsDto());
        })
        .WithName(GetGameEndpointName);

        // POST /games
        group.MapPost("/", async (CreateGameDTO newGame, ParaIgreContext dbContext) =>
        {

            Game game = newGame.ToEntity();
            game.Tag = dbContext.Tags.Find(newGame.TagId);  

            dbContext.Games.Add(game);
            await dbContext.SaveChangesAsync();

            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game.ToGameDetailsDto());
        });

        // PUT /games/1
        group.MapPut("/{id}", async (int id, UpdateGameDTO updatedGame, ParaIgreContext dbContext) =>
        {
            var existingGame = await dbContext.Games.FindAsync(id);

            if (existingGame is null)
            {
                return Results.NotFound();
            }

            dbContext.Entry(existingGame).CurrentValues.SetValues(updatedGame.ToEntity(id));

            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });

        // DELETE /games/1
        group.MapDelete("/{id}", async (int id, ParaIgreContext dbContext) =>
        {
            await dbContext.Games.Where(game => game.Id == id).ExecuteDeleteAsync();

            return Results.NoContent();
        });

        return group;
    }
}
