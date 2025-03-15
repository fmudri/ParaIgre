// DTOs are simple objects used to transfer data between layers 
// They usually contain only properties and no business logic

// Import the DataAnnotations namespace which provides attributes for data validation.
using System.ComponentModel.DataAnnotations;

// Define the namespace for DTOs.
namespace ParaIgre.Api.DTOs;

// 'record class' is used to quickly create an immutable type with value-based equality.
// This DTO is used when updating a game.
public record class UpdateGameDTO
(
    // The [Required] attribute ensures that the Name must be provided.
    // [StringLength(50)] restricts the maximum length to 50 characters.
    [Required][StringLength(50)] string Name,

    // TagId is an integer that refers to a Tag entity.
    int TagId,

    // The Price is a decimal value with required validation.
    // [Range(0, 1000)] ensures the price is between 0 and 1000.
    [Required][Range(0, 1000)] decimal Price,

    // ReleaseDate uses DateOnly to store a date without a time.
    DateOnly ReleaseDate,

    // Description is required and cannot exceed 250 characters.
    [Required][StringLength(250)] string Description,

    // Studio is required and limited to 50 characters.
    [Required][StringLength(50)] string Studio
);
