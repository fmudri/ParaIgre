namespace ParaIgre.Api.DTOs;

public record class CreateGameDTO
(
    string Name,
    string Tags,
    decimal Price,
    DateOnly ReleaseDate,
    string Description,
    string Studio
);