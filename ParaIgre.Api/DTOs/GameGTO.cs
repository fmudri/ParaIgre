namespace ParaIgre.Api.DTOs;

// Records are immutable
// Once their properties are set, usually at the creation time, they cannot be changed

// Records are perfect fit for DTOs because they typically commute data without the need for modification

public record class GameGTO
(
    int Id,
    string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate,
    string Description,
    string Studio
);
    