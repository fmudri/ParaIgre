using System;
using ParaIgre.Api.DTOs;
using ParaIgre.Api.Entities;

namespace ParaIgre.Api.Mapping;

public static class GameMappings
{
    public static Game ToEntity(this CreateGameDTO game)
    {
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

    public static Game ToEntity(this UpdateGameDTO game, int id)
    {
        return new Game()
        {
            Id = id,
            Name = game.Name,
            TagId = game.TagId,
            Price = game.Price,
            ReleaseDate = game.ReleaseDate,
            Description = game.Description,
            Studio = game.Studio
        };
    }

    public static GameSummaryDTO ToGameSummaryDto(this Game game)
    {
        return new
        (
            game.Id,
            game.Name,
            game.Tag!.Name,
            game.Price,
            game.ReleaseDate,
            game.Description,
            game.Studio!
        );
    }

    public static GameDetailsDTO ToGameDetailsDto(this Game game)
    {
        return new
        (
            game.Id,
            game.Name,
            game.TagId!,
            game.Price,
            game.ReleaseDate,
            game.Description,
            game.Studio!
        );
    }
}
