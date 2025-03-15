// Mapping classes contain extension methods to convert between entities and DTOs
// This helps isolate the conversion logic and keeps controllers and endpoints clean


// Import necessary namespaces for our DTOs and entities.
using System;
using ParaIgre.Api.DTOs;     // Contains CreateGameDTO, UpdateGameDTO, GameSummaryDTO, GameDetailsDTO.
using ParaIgre.Api.Entities;   // Contains Game and Tag classes.

namespace ParaIgre.Api.Mapping
{
    // A static class to hold mapping extension methods for Game.
    public static class GameMappings
    {
        // Converts a CreateGameDTO to a Game entity.
        // 'this CreateGameDTO game' makes this an extension method callable on CreateGameDTO instances.
        public static Game ToEntity(this CreateGameDTO game)
        {
            // Create and return a new Game instance with properties copied from the DTO.
            return new Game()
            {
                Name = game.Name,
                TagId = game.TagId,
                Price = game.Price,
                ReleaseDate = game.ReleaseDate,
                Description = game.Description,
                Studio = game.Studio
            };
        }

        // Converts an UpdateGameDTO to a Game entity and sets the provided id.
        public static Game ToEntity(this UpdateGameDTO game, int id)
        {
            // Create and return a new Game instance with updated properties.
            return new Game()
            {
                Id = id,  // Set the id to the provided id.
                Name = game.Name,
                TagId = game.TagId,
                Price = game.Price,
                ReleaseDate = game.ReleaseDate,
                Description = game.Description,
                Studio = game.Studio
            };
        }

        // Converts a Game entity to a GameSummaryDTO.
        // This DTO provides a summary view of a game, often used in listings.
        public static GameSummaryDTO ToGameSummaryDto(this Game game)
        {
            return new
            (
                game.Id,               // Game Id.
                game.Name,             // Game Name.
                game.Tag!.Name,        // The name of the tag; the '!' tells the compiler that Tag is not null.
                game.Price,            // Game Price.
                game.ReleaseDate,      // Game Release Date.
                game.Description,      // Game Description.
                game.Studio!           // Game Studio; '!' asserts this is not null.
            );
        }

        // Converts a Game entity to a GameDetailsDTO.
        // This DTO provides detailed information about a game.
        public static GameDetailsDTO ToGameDetailsDto(this Game game)
        {
            return new
            (
                game.Id,           // Game Id.
                game.Name,         // Game Name.
                game.TagId!,       // TagId associated with the game (asserted not null).
                game.Price,        // Game Price.
                game.ReleaseDate,  // Game Release Date.
                game.Description,  // Game Description.
                game.Studio!       // Game Studio (asserted not null).
            );
        }
    }
}
