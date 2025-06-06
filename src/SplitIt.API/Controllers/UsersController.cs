﻿using Microsoft.AspNetCore.Mvc;
using SplitIt.Application.Users.DTOs;
using SplitIt.Application.Users.UseCases;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly DeleteUserInteractor _deleteUser;
    private readonly GetUserByEmailInteractor _getUserByEmail;
    private readonly GetUserByIdInteractor _getUserById;
    private readonly UpdateUserInteractor _updateUser;

    public UsersController(
        DeleteUserInteractor deleteUser,
        GetUserByEmailInteractor getUserByEmail,
        GetUserByIdInteractor getUserById,
        UpdateUserInteractor updateUser
        )
    {
        _deleteUser = deleteUser;
        _getUserByEmail = getUserByEmail;
        _getUserById = getUserById;
        _updateUser = updateUser;
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteUserDTO dto)
    {
        try
        {
            var deletedUserId = await _deleteUser.DeleteAsync(dto);
            return Ok(new { Id = deletedUserId, Message = "Usuario eliminado correctamente." });
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { Error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = "Ocurrió un error inesperado.", Details = ex.Message });
        }
    }

    [HttpGet("byemail/{email}")]
    public async Task<IActionResult> GetByEmail(string email)
    {
        try
        {
            var recoveredUser = await _getUserByEmail.GetByEmailAsync(email);
            return Ok(recoveredUser);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { Error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Errors = "Ocurrió un error inesperado.", Details = ex.Message });
        }
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetByID(Guid userId)
    {
        try
        {
            var recoveredUser = await _getUserById.GetByIdAsync(userId);
            return Ok(recoveredUser);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { Error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Errors = "Ocurrió un error inesperado.", Details = ex.Message });
        }
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDTO dto)
    {
        try
        {
            var updatedUserId = await _updateUser.UpdateAsync(dto);
            return Ok(updatedUserId);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { Error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Errors = "Ocurrió un error inesperado.", Details = ex.Message });
        }
    }
}