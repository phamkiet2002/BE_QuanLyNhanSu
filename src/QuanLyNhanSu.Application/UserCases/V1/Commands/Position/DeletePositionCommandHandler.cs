using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.Position;
using QuanLyNhanSu.Domain.Abstractions.Repositories;
using QuanLyNhanSu.Domain.Abstractions;
using QuanLyNhanSu.Domain.Entities;
using QuanLyNhanSu.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace QuanLyNhanSu.Application.UserCases.V1.Commands.Position;
public sealed class DeletePositionCommandHandler : ICommandHandler<Command.DeletePositionCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryBase<Domain.Entities.Position, Guid> _positionRepository;
    private readonly IRepositoryBase<Domain.Entities.EmployeePosition, Guid> _employeePositionRepository;

    public DeletePositionCommandHandler
    (
        IUnitOfWork unitOfWork, IRepositoryBase<Domain.Entities.Position, Guid> positionRepository,
        IRepositoryBase<EmployeePosition, Guid> employeePositionRepository
    )
    {
        _unitOfWork = unitOfWork;
        _positionRepository = positionRepository;
        _employeePositionRepository = employeePositionRepository;
    }

    public async Task<Result> Handle(Command.DeletePositionCommand request, CancellationToken cancellationToken)
    {
        var position = await _positionRepository.FindByIdAsync(request.Id)
            ?? throw new PositionException.PositionNotFoundException(request.Id);

        var employeePosition = await _employeePositionRepository
            .FindAll(x => x.PositionId == request.Id && x.Employee.Status == Domain.Enumerations.StatusEnums.Status.Active).AnyAsync();

        if (employeePosition)
            throw new Exception("Không thể xóa vị trí vì đang có nhân viên.");

        position.DeletePosition();

        _positionRepository.Update(position);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
