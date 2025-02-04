using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.Position;
using QuanLyNhanSu.Domain.Abstractions.Repositories;
using QuanLyNhanSu.Domain.Abstractions;
using QuanLyNhanSu.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using QuanLyNhanSu.Domain.Entities.Identity;

namespace QuanLyNhanSu.Application.UserCases.V1.Commands.Position;
public sealed class UpdatePositionCommandHandler : ICommandHandler<Command.UpdatePositionCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryBase<Domain.Entities.Position, Guid> _positionRepository;
    private readonly IRepositoryBase<Domain.Entities.Identity.PositionRole, Guid> _positionRoleRepository;

    public UpdatePositionCommandHandler(IUnitOfWork unitOfWork, IRepositoryBase<Domain.Entities.Position, Guid> positionRepository,
        IRepositoryBase<Domain.Entities.Identity.PositionRole, Guid> positionRoleRepository)
    {
        _unitOfWork = unitOfWork;
        _positionRepository = positionRepository;
        _positionRoleRepository = positionRoleRepository;
    }

    public async Task<Result> Handle(Command.UpdatePositionCommand request, CancellationToken cancellationToken)
    {
        var position = await _positionRepository.FindByIdAsync(request.Id)
            ?? throw new PositionException.PositionNotFoundException(request.Id);

        position.UpdatePosition(request.Name, request.Description);

        _positionRepository.Update(position);

        var currentPositionRoles = _positionRoleRepository.FindAll(x => x.PositionId == request.Id).ToList();


        foreach (var roleId in request.RoleIds)
        {
            var positionRole = currentPositionRoles
                .FirstOrDefault(pr => pr.RoleId == roleId);

            if (positionRole != null)
            {
                positionRole.UpdatePositionRole(position.Id, roleId);
                _positionRoleRepository.Update(positionRole);
            }
            else
            {
                var newPositionRole = new PositionRole
                {
                    PositionId = position.Id,
                    RoleId = roleId
                };
                _positionRoleRepository.Add(newPositionRole); 
            }
        }

        var rolesToRemove = currentPositionRoles
        .Where(pr => !request.RoleIds.Contains(pr.RoleId))
        .ToList();

        foreach (var roleToRemove in rolesToRemove)
        {
            _positionRoleRepository.Remove(roleToRemove);
        }


        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
