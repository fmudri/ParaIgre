namespace ParaIgre.Api.Entities;

public class Game
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public int TagId { get; set; }

    public Tag? Tag { get; set; }

    public decimal Price { get; set; }

    public DateOnly ReleaseDate { get; set; }

    public required string Description { get; set; }

    public string? Studio { get; set; }
}
