using ParaIgre.Api.DTOs;
using ParaIgre.Api.Services;

namespace ParaIgre.Api.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/auth");

        group.MapPost("/register", async (RegisterRequest request, AuthService authService) =>
        {
            try
            {
                var response = await authService.Register(request);
                return Results.Ok(response);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        group.MapPost("/login", async (LoginRequest request, AuthService authService) =>
        {
            try
            {
                var response = await authService.Login(request);
                return Results.Ok(response);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });
    }
} 