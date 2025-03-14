using System;
using Microsoft.EntityFrameworkCore;
using ParaIgre.Api.Entities;

namespace ParaIgre.Api.Data;

public class ParaIgreContext(DbContextOptions<ParaIgreContext> options) : DbContext(options)
{
    public DbSet<Game> Games => Set<Game>();

    public DbSet<Tag> Tags => Set<Tag>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tag>().HasData
        (
            new {Id = 1, Name = "Adventure"},
            new {Id = 2, Name = "MMORPG"},
            new {Id = 3, Name = "Open-World"},
            new {Id = 4, Name = "Racing"},
            new {Id = 5, Name = "Fighting"},
            new {Id = 6, Name = "Sports"},
            new {Id = 7, Name = "Action"}
        );
    }
}
