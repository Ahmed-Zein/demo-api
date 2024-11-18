using AutoMapper;
using DemoApp.Dto.AppUser;
using DemoApp.Interfaces;
using DemoApp.LoggerHub;
using DemoApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace DemoApp.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(
    ITokenService tokenService,
    IUnitOfWork unitOfWork)
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

        var res = await _authService.Register(user, request.Password, RolesConstants.User);
        if (!res.Succeeded)
            return BadRequest(res.Errors);
        _ = unitOfWork.LoggerHub.Clients.All.OnLog(new LogMessage(0, $"user {user.UserName} successfully registered"));
        return Ok();
    }


    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var user = await _authService.ValidateCredentials(request.Email, request.Password);
        if (user is null)
            return BadRequest(new LoginResponse { Message = "Email or password is incorrect", Success = false });

        var roles = await _authService.GetRoles(user);
        var token = tokenService.GenerateToken(user, roles);
        return Ok(new LoginResponse { Success = true, Token = token });
    }
}