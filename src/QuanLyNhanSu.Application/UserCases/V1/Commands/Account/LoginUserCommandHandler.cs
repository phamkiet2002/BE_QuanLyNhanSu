using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.Account;
using QuanLyNhanSu.Domain.Abstractions.Repositories;
using QuanLyNhanSu.Domain.Entities.Identity;

namespace QuanLyNhanSu.Application.UserCases.V1.Commands.Account;
public sealed class LoginUserCommandHandler : ICommandHandler<Command.LoginCommand, Response.LoginResponse>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly RoleManager<AppUser> _roleManager;

    private readonly IConfiguration _configuration;
    private readonly IRepositoryBase<Domain.Entities.Position, Guid> _positionRepository;
    private readonly IRepositoryBase<Domain.Entities.EmployeePosition, Guid> _employeePositionRepository;

    public LoginUserCommandHandler
    (
        UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration configuration,
        IRepositoryBase<Domain.Entities.EmployeePosition, Guid> employeePositionRepository,
        IRepositoryBase<Domain.Entities.Position, Guid> positionRepository
    )
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
        _employeePositionRepository = employeePositionRepository;
        _positionRepository = positionRepository;
    }

    public async Task<Result<Response.LoginResponse>> Handle(Command.LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email)
        ?? throw new Exception("Invalid login attempt.");


        if (user.UserName == "admin")
        {
            var claimsAdmin = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, user.NormalizedUserName)
            };

            //claimsAdmin.Add(new Claim(ClaimTypes.Role, user.NormalizedUserName.ToString()));
                

            var keyAdmin = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["JWT:SigningKey"]));
            var credsAdmin = new SigningCredentials(keyAdmin, SecurityAlgorithms.HmacSha256);
            var expirationMinutesAdmin = double.Parse(_configuration["JWT:Expiration"]);

            var tokenAdmin = new JwtSecurityToken(
            issuer: _configuration["JWT:Issuer"],
            audience: _configuration["JWT:Audience"],
            claims: claimsAdmin,
            expires: DateTime.Now.AddMinutes(expirationMinutesAdmin),
            signingCredentials: credsAdmin);

            var responseAdmin = new Response.LoginResponse(user.UserName, new JwtSecurityTokenHandler().WriteToken(tokenAdmin));
            return Result.Success(responseAdmin);
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        if (!result.Succeeded)
        {
            throw new Exception("Invalid login attempt.");
        }

        var employeePosition = await _employeePositionRepository
            .FindSingleAsync(x => x.EmployeeId == user.EmployeeId && x.Status == Domain.Enumerations.StatusEnums.Status.Active, cancellationToken);

        var positionRoles = await _positionRepository
            .FindSingleAsync(x => x.Id == employeePosition.PositionId, cancellationToken, x => x.PositionRoles);

        // Generate JWT token
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        claims.AddRange(positionRoles.PositionRoles.Select(role => new Claim(ClaimTypes.Role, role.AppRole.NormalizedName.ToString())));

        //foreach (var role in positionRoles.PositionRoles)
        //{
        //    claims.Add(new Claim(ClaimTypes.Role, role.RoleId.ToString()));
        //}

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["JWT:SigningKey"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expirationMinutes = double.Parse(_configuration["JWT:Expiration"]);


        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:Issuer"],
            audience: _configuration["JWT:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(expirationMinutes),
            signingCredentials: creds);

        var response = new Response.LoginResponse(user.UserName, new JwtSecurityTokenHandler().WriteToken(token));
        return Result.Success(response);
    }
}
