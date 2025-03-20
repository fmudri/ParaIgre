using System.ComponentModel.DataAnnotations;

namespace ParaIgre.Api.DTOs;

public record RegisterRequest(
    [Required] string Username,
    [Required] [MinLength(6)] string Password
);

public record LoginRequest(
    [Required] string Username,
    [Required] string Password
);

public record AuthResponse(
    string Token,
    string Username
); 