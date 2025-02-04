using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.AppRole;
using QuanLyNhanSu.Domain.Abstractions.Repositories;

namespace QuanLyNhanSu.Application.UserCases.V1.Queries.AppRole;
public sealed class GetRoleQueryHandler : IQueryHandler<Query.GetAppRoleQuery, List<Response.GetAppRoleResponse>>
{
    public readonly IMapper _mapper;
    private readonly RoleManager<Domain.Entities.Identity.AppRole> _roleManager;

    public GetRoleQueryHandler
    (
        IMapper mapper, RoleManager<Domain.Entities.Identity.AppRole> roleManager
    )
    {
        _mapper = mapper;
        _roleManager = roleManager;
    }

    public async Task<Result<List<Response.GetAppRoleResponse>>> Handle(Query.GetAppRoleQuery request, CancellationToken cancellationToken)
    {
        var appRole = await _roleManager.Roles.ToListAsync();
        var result = _mapper.Map<List<Response.GetAppRoleResponse>>(appRole);
        return Result.Success(result);
    }
}
