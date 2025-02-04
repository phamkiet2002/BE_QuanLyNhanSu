using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.LeaveDate;
using QuanLyNhanSu.Domain.Abstractions.Repositories;
using QuanLyNhanSu.Domain.Abstractions;
using QuanLyNhanSu.Domain.Exceptions;

namespace QuanLyNhanSu.Application.UserCases.V1.Commands.LeaveDate;
public sealed class UpdateLeaveDateCommandHandler : ICommandHandler<Command.UpdateLeaveDateCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryBase<Domain.Entities.LeaveDate, Guid> _leaveDateRepository;

    public UpdateLeaveDateCommandHandler(IUnitOfWork unitOfWork, IRepositoryBase<Domain.Entities.LeaveDate, Guid> leaveDateRepository)
    {
        _unitOfWork = unitOfWork;
        _leaveDateRepository = leaveDateRepository;
    }

    public async Task<Result> Handle(Command.UpdateLeaveDateCommand request, CancellationToken cancellationToken)
    {
        var leaveDate = await _leaveDateRepository.FindByIdAsync(request.Id)
            ?? throw new LeaveDateException.LeaveDateNotFoundException(request.Id);

        leaveDate.UpdateLeaveDate
        (
            request.Name,
            request.TotalAnnualLeaveDate,
            request.MaximumDaysOffPerMonth,
            request.Description,
            request.IsHoliday,
            request.StartDate,
            request.EndDate
        );
        _leaveDateRepository.Update(leaveDate);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
