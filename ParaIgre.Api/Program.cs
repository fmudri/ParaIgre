// This is the entry point for the ASP.NET Core application
// It sets up services, maps endpoints, and runs the application


// Import our data context and endpoints for setting up the application.
using ParaIgre.Api.Data;       // Contains the ParaIgreContext (database context).
using ParaIgre.Api.Endpoints;  // Contains extension methods to map endpoints.

// Create the web application builder which sets up the configuration, services, and middleware.
var builder = WebApplication.CreateBuilder(args);

// Retrieve the connection string for the database from configuration settings.
var connString = builder.Configuration.GetConnectionString("ParaIgre");

// Register the SQLite database provider and configure the ParaIgreContext with the connection string.
// This makes the context available for dependency injection.
builder.Services.AddSqlite<ParaIgreContext>(connString);

// Build the WebApplication instance from the builder.
var app = builder.Build();

// Map the game-related endpoints onto the application.
// This registers the routes defined in the GamesEndpoints class.
app.MapGamesEndpoints();

// Map the tag-related endpoints onto the application.
// This registers the routes defined in the TagsEndpoints class.
app.MapTagsEndpoints();

// Migrate the database asynchronously.
// This applies any pending migrations to the database when the app starts.
await app.MigrateDbAsync();

// Run the application. This starts the web server.
app.Run();
