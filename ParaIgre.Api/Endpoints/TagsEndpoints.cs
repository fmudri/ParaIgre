using System;
using Microsoft.EntityFrameworkCore;
using ParaIgre.Api.Data;
using ParaIgre.Api.Mapping;

namespace ParaIgre.Api.Endpoints;

public static class TagsEndpoints
{
    public static RouteGroupBuilder MapTagsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("tags");

        group.MapGet("/", async (ParaIgreContext dbContext) =>
            await dbContext.Tags
                .Select(tag => tag.ToDTO())
                .AsNoTracking()
                .ToListAsync());

        return group;
    }
}
