// DTOs are simple objects used to transfer data between layers
// They usually contain only properties and no business logic


// Declare the namespace to group related DTO classes.
// This helps organize the code and avoid naming conflicts.
namespace ParaIgre.Api.DTOs;

// 'public record class' creates an immutable reference type with built-in value equality.
// TagDTO is used to transfer Tag data (its id and name) between layers or over the network.
public record class TagDTO(int Id, string Name);
