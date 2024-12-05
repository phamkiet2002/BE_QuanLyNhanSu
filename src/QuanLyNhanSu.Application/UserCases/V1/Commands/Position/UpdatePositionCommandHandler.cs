using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.Position;
using QuanLyNhanSu.Domain.Abstractions.Repositories;
using QuanLyNhanSu.Domain.Abstractions;
using QuanLyNhanSu.Domain.Exceptions;

namespace QuanLyNhanSu.Application.UserCases.V1.Commands.Position;
public sealed class UpdatePositionCommandHandler : ICommandHandler<Command.UpdatePositionCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryBase<Domain.Entities.Position, Guid> _positionRepository;

    public UpdatePositionCommandHandler(IUnitOfWork unitOfWork, IRepositoryBase<Domain.Entities.Position, Guid> positionRepository)
    {
        _unitOfWork = unitOfWork;
        _positionRepository = positionRepository;
    }

    public async Task<Result> Handle(Command.UpdatePositionCommand request, CancellationToken cancellationToken)
    {
        var position = await _positionRepository.FindByIdAsync(request.Id)
            ?? throw new PositionException.PositionNotFoundException(request.Id);
        position.UpdatePosition(request.Name, request.Description);
        _positionRepository.Update(position);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
