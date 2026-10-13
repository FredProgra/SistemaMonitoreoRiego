using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.DTOs;
using ServicesContracts.Services;

namespace ServicesContracts.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterDTO dto,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await _authService.RegisterAsync(
                dto,
                HttpContext.Connection.RemoteIpAddress?.ToString(),
                cancellationToken);

            return StatusCode(StatusCodes.Status201Created, result);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
        catch (DbUpdateException)
        {
            return Conflict(new
            {
                message = "No se pudo registrar el usuario. " +
                          "Verifica que el nombre de usuario y correo no existan."
            });
        }
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginDTO dto,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await _authService.LoginAsync(
                dto,
                HttpContext.Connection.RemoteIpAddress?.ToString(),
                cancellationToken);

            return Ok(result);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized(new
            {
                message = "No se pudo iniciar sesión con esas credenciales."
            });
        }
    }

    [Authorize]
    [HttpGet("me")]
    public IActionResult Me()
    {
        return Ok(new
        {
            id = User.FindFirst(
                System.Security.Claims.ClaimTypes.NameIdentifier)?.Value,
            username = User.Identity?.Name,
            roles = User.FindAll(
                System.Security.Claims.ClaimTypes.Role)
                .Select(c => c.Value)
                .ToList()
        });
    }

    [Authorize(Roles = "ROLE_ADMIN")]
    [HttpGet("admin")]
    public IActionResult AdminOnly()
    {
        return Ok(new
        {
            message = "Acceso permitido para administradores."
        });
    }
}