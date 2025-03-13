namespace ParaIgre.Api.DTOs;

public record class UpdateGameDTO
(
    string Name,
    string Tags,
    decimal Price,
    DateOnly ReleaseDate,
    string Description,
    string Studio
);
