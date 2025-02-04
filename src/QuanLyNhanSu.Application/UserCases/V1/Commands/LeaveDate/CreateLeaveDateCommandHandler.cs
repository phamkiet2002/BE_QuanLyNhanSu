using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.LeaveDate;
using QuanLyNhanSu.Domain.Abstractions;
using QuanLyNhanSu.Domain.Abstractions.Repositories;

namespace QuanLyNhanSu.Application.UserCases.V1.Commands.LeaveDate;
public sealed class CreateLeaveDateCommandHandler : ICommandHandler<Command.CreateLeaveDateCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryBase<Domain.Entities.LeaveDate, Guid> _leaveDateRepository;

    public CreateLeaveDateCommandHandler(IUnitOfWork unitOfWork, IRepositoryBase<Domain.Entities.LeaveDate, Guid> leaveDateRepository)
    {
        _unitOfWork = unitOfWork;
        _leaveDateRepository = leaveDateRepository;
    }

    public async Task<Result> Handle(Command.CreateLeaveDateCommand request, CancellationToken cancellationToken)
    {
        var leaveDate = Domain.Entities.LeaveDate.CreateLeaveDate
        (
            Guid.NewGuid(),
            request.Name,
            request.TotalAnnualLeaveDate,
            request.MaximumDaysOffPerMonth,
            request.Description,
            request.IsHoliday,
            request.StartDate,
            request.EndDate
        );

        _leaveDateRepository.Add(leaveDate);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
