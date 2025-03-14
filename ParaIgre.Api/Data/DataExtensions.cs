using System;
using Microsoft.EntityFrameworkCore;

namespace ParaIgre.Api.Data;

public static class DataExtensions
{
    public static void MigrateDb(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ParaIgreContext>();
        dbContext.Database.Migrate();
    }
}
