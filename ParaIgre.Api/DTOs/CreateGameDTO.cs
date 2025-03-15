using System.ComponentModel.DataAnnotations;

namespace ParaIgre.Api.DTOs;

public record class CreateGameDTO
(
    [Required][StringLength(50)]string Name,
    int TagId,
    [Required][Range(0, 1000)]decimal Price,
    DateOnly ReleaseDate,
    [Required][StringLength(250)]string Description,
    [Required][StringLength(50)]string Studio
);