// This is the entry point for the ASP.NET Core application
// It sets up services, maps endpoints, and runs the application


// Import our data context and endpoints for setting up the application.
using ParaIgre.Api.Data;       // Contains the ParaIgreContext (database context).
using ParaIgre.Api.Endpoints;  // Contains extension methods to map endpoints.
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ParaIgre.Api.Services;
using System.Text;

// Create the web application builder which sets up the configuration, services, and middleware.
var builder = WebApplication.CreateBuilder(args);

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactAppPolicy",
        builder => builder
            .WithOrigins("http://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Add Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value!)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

// Add Authorization
builder.Services.AddAuthorization();

// Register AuthService
builder.Services.AddScoped<AuthService>();

// Retrieve the connection string for the database from configuration settings.
var connString = builder.Configuration.GetConnectionString("ParaIgre");

// Register the SQLite database provider and configure the ParaIgreContext with the connection string.
// This makes the context available for dependency injection.
builder.Services.AddSqlite<ParaIgreContext>(connString);

// Build the WebApplication instance from the builder.
var app = builder.Build();

// Enable CORS
app.UseCors("ReactAppPolicy");

// Enable Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

// Map endpoints
app.MapAuthEndpoints();
app.MapGamesEndpoints();
app.MapTagsEndpoints();

// Migrate the database asynchronously.
// This applies any pending migrations to the database when the app starts.
await app.MigrateDbAsync();

// Run the application. This starts the web server.
app.Run();
