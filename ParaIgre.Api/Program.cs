using ParaIgre.Api.Data;
using ParaIgre.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("ParaIgre");
builder.Services.AddSqlite<ParaIgreContext>(connString);

var app = builder.Build();

app.MapGamesEndpoints();

app.MigrateDb();

app.Run();
