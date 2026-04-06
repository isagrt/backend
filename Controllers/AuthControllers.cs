using BoschPizza.Models;
using BoschPizza.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;

namespace BoschPizza.Controllers;

[ApiController]

[Route("auth")]

public class AuthController: ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly TokenService _tokenService;

    //Metodo construtor
    public AuthController(IConfiguration configuration, TokenService tokenService)
    {
        _configuration = configuration;
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public IActionResult Login(UserLogin login)
    {
        if (login.Username != "admin" || login.Password != "123456789")
        {
            return Unauthorized(new {message = "Usuário ou senha inválidos"});
        }

        var key= _configuration["Jwt:Key"]!;
        var issuer= _configuration["Jwt:Issuer"]!;
        var audience= _configuration["Jwt:Audience"]!;

        var token = _tokenService.GenerateToken(login.Username, key, issuer, audience);
        
        return Ok (new {token});

    }
}

