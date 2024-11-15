using AutoMapper;
using DemoApp.Dto.AppUser;
using DemoApp.Interfaces;
using DemoApp.Models;
using DemoApp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DemoApp.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(UserManager<AppUser> userManager, ITokenService tokenService, IUnitOfWork unitOfWork)
    : ControllerBase
{
    private readonly IAuthService _authService = unitOfWork.AuthService;
    private readonly IMapper _mapper = unitOfWork.Mapper;

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserReq request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var user = _mapper.Map<AppUser>(request);
        user.UserName = request.Email;

        var res = await _authService.Register(user, request.Password, Roles.User);
        if (!res.Succeeded)
            return BadRequest(res.Errors);

        return Ok();
    }


    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var user = await userManager.FindByEmailAsync(request.Email);
        if (user is null || !await _authService.CheckPassword(user, request.Password))
            return BadRequest(new LoginResponse { Message = "Email or password is incorrect", Success = false });

        var roles = await userManager.GetRolesAsync(user);
        var token = tokenService.GenerateToken(user, roles);
        return Ok(new LoginResponse { Success = true, Token = token });
    }
}