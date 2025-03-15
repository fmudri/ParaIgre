// Entities represent the core data models in your application
// They are typically mapped to database tables


// Define the namespace for entity classes.
namespace ParaIgre.Api.Entities
{
    // The Game class represents a game in the application.
    // It includes properties that match the columns in the corresponding database table.
    public class Game
    {
        // Unique identifier for the game.
        public int Id { get; set; }

        // The name of the game; 'required' means it must have a value.
        public required string Name { get; set; }

        // Foreign key linking to a Tag entity.
        public int TagId { get; set; }

        // Navigation property to the related Tag.
        // The '?' indicates that it can be null if the relationship is not loaded.
        public Tag? Tag { get; set; }

        // The price of the game.
        public decimal Price { get; set; }

        // The release date of the game.
        public DateOnly ReleaseDate { get; set; }

        // A description of the game; required means it cannot be null.
        public required string Description { get; set; }

        // The studio that developed or published the game.
        // This property is optional (nullable) since it is marked with '?'.
        public string? Studio { get; set; }
    }
}
