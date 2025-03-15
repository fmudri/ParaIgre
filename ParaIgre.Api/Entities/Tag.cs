// Entities represent the core data models in your application
// They are typically mapped to database tables


// Define the namespace for entity classes.
namespace ParaIgre.Api.Entities
{
    // The Tag class represents a tag or category for games.
    public class Tag
    {
        // Unique identifier for the tag.
        public int Id { get; set; }

        // The name of the tag; required means it must have a value.
        public required string Name { get; set; }
    }
}

