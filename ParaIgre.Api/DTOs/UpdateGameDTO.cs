using System.ComponentModel.DataAnnotations;

namespace ParaIgre.Api.DTOs;

public record class UpdateGameDTO
(
    [Required][StringLength(50)]string Name,
    [Required][StringLength(100)]string Tags,
    [Required][Range(0, 1000)]decimal Price,
    DateOnly ReleaseDate,
    [Required][StringLength(250)]string Description,
    [Required][StringLength(50)]string Studio
);
