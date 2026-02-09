using Microsoft.AspNetCore.Mvc;
using SplitIt.Application.Business.Users.DTOs;
using SplitIt.Application.Business.Users.UseCases;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly LoginUserInteractor _loginUser;
    private readonly RegisterUserInteractor _registerUser;

    public AuthController(LoginUserInteractor loginUser, RegisterUserInteractor registerUser)
    {
        _loginUser = loginUser;
        _registerUser = registerUser;
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterUserDTO dto)
    {
        try
        {
            var registeredUserId = await _registerUser.AddAsync(dto);
            return Ok(new { Id = registeredUserId, Message = "Usuario registrado correctamente." });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = "Ocurrió un error inesperado.", Details = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserDTO dto)
    {
        try
        {
            var userId = await _loginUser.LoginAsync(dto);
            return Ok(new { UserId = userId });
        }
        catch (InvalidOperationException ex)
        {
            return Unauthorized(new { Error = ex.Message });
        }
    }
}
