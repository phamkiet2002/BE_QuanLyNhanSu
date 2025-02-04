using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.Position;
using QuanLyNhanSu.Domain.Abstractions;
using QuanLyNhanSu.Domain.Abstractions.Repositories;

namespace QuanLyNhanSu.Application.UserCases.V1.Commands.Position;
public sealed class CreatePositionCommandHandler : ICommandHandler<Command.CreatePositionCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryBase<Domain.Entities.Position, Guid> _positionRepository;
    private readonly IRepositoryBase<Domain.Entities.Identity.PositionRole, Guid> _positionRoleRepository;

    public CreatePositionCommandHandler
    (
        IUnitOfWork unitOfWork,
        IRepositoryBase<Domain.Entities.Position, Guid> positionRepository,
        IRepositoryBase<Domain.Entities.Identity.PositionRole, Guid> positionRoleRepository
    )
    {
        _unitOfWork = unitOfWork;
        _positionRepository = positionRepository;
        _positionRoleRepository = positionRoleRepository;
    }

    public async Task<Result> Handle(Command.CreatePositionCommand request, CancellationToken cancellationToken)
    {
        var position = Domain.Entities.Position.CreatePosition
        (
            Guid.NewGuid(),
            request.Name,
            request.Description
        );
        _positionRepository.Add(position);

        var positionRoles = request.RoleIds.Select
        (
            roleId =>
                Domain.Entities.Identity.PositionRole.CreatePositionRole(position.Id, roleId)
        )
        .ToList();

        foreach (var positionRole in positionRoles)
        {
            _positionRoleRepository.Add(positionRole);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
