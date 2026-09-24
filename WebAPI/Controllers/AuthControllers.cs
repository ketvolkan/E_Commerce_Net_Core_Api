namespace WebAPI.Controllers;

using Business.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

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
    /// Kullanıcı Girişi
    /// POST: api/auth/login
    /// </summary>
    [HttpPost("login")]
    public IActionResult Login([FromBody] UserForLoginDto userForLoginDto)
    {
        var userToLogin = _authService.Login(userForLoginDto);
        if (!userToLogin.Success)
        {
            return BadRequest(userToLogin);
        }

        var result = _authService.CreateAccessToken(userToLogin.Data);
        if (result.Success)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }

    /// <summary>
    /// Yeni Kullanıcı Kaydı
    /// POST: api/auth/register
    /// </summary>
    [HttpPost("register")]
    public IActionResult Register([FromBody] UserForRegisterDto userForRegisterDto)
    {
        var userExists = _authService.UserExists(userForRegisterDto.Email);
        if (!userExists.Success)
        {
            return BadRequest(userExists);
        }

        var registerResult = _authService.Register(userForRegisterDto, userForRegisterDto.Password);
        var result = _authService.CreateAccessToken(registerResult.Data);
        if (result.Success)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }

    /// <summary>
    /// Giriş yapmış olan kullanıcının profil ve rol bilgilerini getirir
    /// GET: api/auth/me
    /// </summary>
    [HttpGet("me")]
    public IActionResult GetMe()
    {
        var result = _authService.GetMe();
        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
}