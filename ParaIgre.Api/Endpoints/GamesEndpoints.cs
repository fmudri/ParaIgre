// Endpoints define the HTTP routes (URLs) and actions (methods) for the API
// They use ASP.NET Core’s routing and minimal API features


// Import necessary namespaces for functionality like EF Core, Authorization, and our custom types.
using System;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ParaIgre.Api.Data;       // Contains our database context (ParaIgreContext).
using ParaIgre.Api.DTOs;       // Contains our data transfer objects.
using ParaIgre.Api.Entities;   // Contains our domain models (Game, Tag, etc.).
using ParaIgre.Api.Mapping;    // Contains extension methods to map between entities and DTOs.

namespace ParaIgre.Api.Endpoints
{
    // Static classes can contain extension methods and are often used to group related endpoints.
    public static class GamesEndpoints
    {
        // Define a constant string for the name of the GetGame endpoint.
        // This is used later to generate URLs when returning results.
        const string GetGameEndpointName = "GetGame";

        // Create a sample in-memory list of GameSummaryDTO objects.
        // This list serves as a pre-defined sample set of games.
        // In real applications, data is typically retrieved from a database.
        private static readonly List<GameSummaryDTO> games =
        [
            // Create a new GameSummaryDTO instance with sample data.
            new (
                1, // Id
                "Crash Bandicoot", // Game Name
                "Arcade, Adventure, Platformer, Singleplayer", // Tags/genres
                15.99M, // Price (M suffix indicates a decimal literal)
                new DateOnly(2007, 5, 15), // Release Date
                "Fight the evil mastermind to stop him from gaining world control!", // Description
                "Naughty dog" // Studio name
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

        // This method is an extension method that adds game-related endpoints to the WebApplication.
        // 'this WebApplication app' means you can call it directly on the app instance.
        public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
        {
            // Map a group of endpoints with the base URL "games".
            // WithParameterValidation() sets up automatic parameter validation for these endpoints.
            var group = app.MapGroup("games").WithParameterValidation();

            // GET /games
            // Maps a GET request to the base "games" endpoint.
            group.MapGet("/", async (ParaIgreContext dbContext) =>
                // Query the database context to retrieve all games.
                // Include the related Tag data with each Game.
                // Convert each Game to a GameSummaryDTO using the mapping extension method.
                // AsNoTracking() improves performance by not tracking the entities.
                // ToListAsync() executes the query asynchronously and returns a list.
                await dbContext.Games
                    .Include(game => game.Tag)
                    .Select(game => game.ToGameSummaryDto())
                    .AsNoTracking()
                    .ToListAsync()
            );

            // GET /games/{id}
            // Maps a GET request for a specific game by id.
            group.MapGet("/{id}", async (int id, ParaIgreContext dbContext) =>
            {
                // Try to find the game with the provided id in the database.
                Game? game = await dbContext.Games.FindAsync(id);

                // If the game is not found, return a 404 Not Found response.
                // Otherwise, convert the game to a detailed DTO and return a 200 OK response.
                return game is null ? Results.NotFound() : Results.Ok(game.ToGameDetailsDto());
            })
            // Name the endpoint so that it can be referenced when creating URLs.
            .WithName(GetGameEndpointName);

            // POST /games
            // Maps a POST request to create a new game.
            group.MapPost("/", async (CreateGameDTO newGame, ParaIgreContext dbContext) =>
            {
                // Convert the CreateGameDTO to a Game entity using the mapping extension method.
                Game game = newGame.ToEntity();

                // Find the Tag entity from the database that matches the provided TagId in newGame.
                // This sets up the relationship between the game and its tag.
                game.Tag = dbContext.Tags.Find(newGame.TagId);

                // Add the new game entity to the Games DbSet.
                dbContext.Games.Add(game);

                // Save the changes asynchronously to persist the new game in the database.
                await dbContext.SaveChangesAsync();

                // Return a 201 Created response.
                // CreatedAtRoute creates a URL to the GetGame endpoint for the newly created game.
                return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game.ToGameDetailsDto());
            });

            // PUT /games/{id}
            // Maps a PUT request to update an existing game.
            group.MapPut("/{id}", async (int id, UpdateGameDTO updatedGame, ParaIgreContext dbContext) =>
            {
                // Find the existing game in the database by id.
                var existingGame = await dbContext.Games.FindAsync(id);

                // If the game is not found, return a 404 Not Found response.
                if (existingGame is null)
                {
                    return Results.NotFound();
                }

                // Update the existing game's values with the values from updatedGame.
                // The ToEntity(id) mapping creates a new Game entity with the given id.
                // CurrentValues.SetValues() applies these values to the existing game entry.
                dbContext.Entry(existingGame).CurrentValues.SetValues(updatedGame.ToEntity(id));

                // Save the updated game back to the database.
                await dbContext.SaveChangesAsync();

                // Return a 204 No Content response to indicate the update was successful.
                return Results.NoContent();
            });

            // DELETE /games/{id}
            // Maps a DELETE request to remove a game.
            group.MapDelete("/{id}", async (int id, ParaIgreContext dbContext) =>
            {
                // Delete the game with the matching id using ExecuteDeleteAsync.
                // This deletes all entries in the query result.
                await dbContext.Games.Where(game => game.Id == id).ExecuteDeleteAsync();

                // Return a 204 No Content response indicating the deletion was successful.
                return Results.NoContent();
            });

            // Return the configured group of endpoints.
            return group;
        }
    }
}
