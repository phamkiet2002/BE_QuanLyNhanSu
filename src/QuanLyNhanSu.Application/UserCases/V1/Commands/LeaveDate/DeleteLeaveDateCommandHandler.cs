using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.LeaveDate;
using QuanLyNhanSu.Domain.Abstractions.Repositories;
using QuanLyNhanSu.Domain.Abstractions;
using static QuanLyNhanSu.Domain.Exceptions.LeaveDateException;

namespace QuanLyNhanSu.Application.UserCases.V1.Commands.LeaveDate;
public sealed class DeleteLeaveDateCommandHandler : ICommandHandler<Command.DeleteLeaveDateCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryBase<Domain.Entities.LeaveDate, Guid> _leaveDateRepository;

    public DeleteLeaveDateCommandHandler(IUnitOfWork unitOfWork, IRepositoryBase<Domain.Entities.LeaveDate, Guid> leaveDateRepository)
    {
        _unitOfWork = unitOfWork;
        _leaveDateRepository = leaveDateRepository;
    }

    public async Task<Result> Handle(Command.DeleteLeaveDateCommand request, CancellationToken cancellationToken)
    {
        var leaveDate = await _leaveDateRepository.FindByIdAsync(request.Id)
            ?? throw new LeaveDateNotFoundException(request.Id);

        leaveDate.DeleteLeaveDate();

        _leaveDateRepository.Update(leaveDate);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
