// Endpoints define the HTTP routes (URLs) and actions (methods) for the API
// They use ASP.NET Core’s routing and minimal API features


// Import required namespaces.
using System;
using Microsoft.EntityFrameworkCore;
using ParaIgre.Api.Data;     // Database context.
using ParaIgre.Api.Mapping;  // Contains methods to convert entities to DTOs.

namespace ParaIgre.Api.Endpoints
{
    // Static class to hold tag-related endpoints.
    public static class TagsEndpoints
    {
        // Extension method that maps endpoints for tags onto the WebApplication.
        public static RouteGroupBuilder MapTagsEndpoints(this WebApplication app)
        {
            // Map a group of endpoints with the base URL "tags".
            var group = app.MapGroup("tags");

            // GET /tags
            // Maps a GET request to retrieve all tags.
            group.MapGet("/", async (ParaIgreContext dbContext) =>
                // Query the database for tags.
                // For each tag, convert it to a TagDTO using the ToDTO() extension method.
                // AsNoTracking() indicates that the entities are not tracked for changes.
                // ToListAsync() executes the query asynchronously and returns the list of DTOs.
                await dbContext.Tags
                    .Select(tag => tag.ToDTO())
                    .AsNoTracking()
                    .ToListAsync()
            );

            // Return the group of endpoints.
            return group;
        }
    }
}
