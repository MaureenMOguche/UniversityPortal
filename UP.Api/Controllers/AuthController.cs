using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using UP.Application.Contracts.Services;
using UP.Application.Models;
using UP.Application.Models.Dto;
using UP.Application.Models.Dto.Auth;
using UP.Domain;

namespace UP.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    
    /// <summary>
    /// check student admission status
    /// </summary>
    /// <param name="registrationDto"></param>
    /// <returns></returns>
    [HttpPost("NewStudentRegister")]
    [ProducesResponseType(typeof(APIResponse), StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> NewStudentRegisterion(int admittedStudentId, [FromBody] RegistrationDto registrationDto)
    {
        if (!ModelState.IsValid)
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);

        var response = await _authService.StudentRegister(registrationDto, admittedStudentId);
        return Ok(response);
    }

    /// <summary>
    /// check student admission status
    /// </summary>
    /// <param name="loginDto"></param>
    /// <returns></returns>
    [HttpPost("studentLogin")]
    [ProducesResponseType(typeof(APIResponse), StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> StudentLogin([Required]LoginDto loginDto)
    {
        if (!ModelState.IsValid)
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);

        var response = await _authService.StudentLogin(loginDto);
        return Ok(response);
    }
}