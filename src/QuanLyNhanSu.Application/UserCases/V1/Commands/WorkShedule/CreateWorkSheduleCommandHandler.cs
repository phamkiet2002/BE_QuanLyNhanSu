using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.WorkShedule;
using QuanLyNhanSu.Domain.Abstractions;
using QuanLyNhanSu.Domain.Abstractions.Repositories;

namespace QuanLyNhanSu.Application.UserCases.V1.Commands.WorkShedule;

public sealed class CreateWorkSheduleCommandHandler : ICommandHandler<Command.CreateWorkSheduleCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryBase<Domain.Entities.WorkShedule, Guid> _workSheduleRepository;

    public CreateWorkSheduleCommandHandler(IUnitOfWork unitOfWork, IRepositoryBase<Domain.Entities.WorkShedule, Guid> workSheduleRepository)
    {
        _unitOfWork = unitOfWork;
        _workSheduleRepository = workSheduleRepository;
    }

    public async Task<Result> Handle(Command.CreateWorkSheduleCommand request, CancellationToken cancellationToken)
    {
        var StartTime = new TimeSpan(request.StartTime.Hours, request.StartTime.Minutes, 0);
        var EndTime = new TimeSpan(request.EndTime.Hours, request.EndTime.Minutes, 0);
        var BreakStartTime = new TimeSpan(request.BreakStartTime.Hours, request.BreakStartTime.Minutes, 0);
        var BreakEndTime = new TimeSpan(request.BreakEndTime.Hours, request.BreakEndTime.Minutes, 0);

        var workShedule = Domain.Entities.WorkShedule.CreateWorkShedule
        (
            request.Name,
            StartTime,
            EndTime,
            BreakStartTime,
            BreakEndTime
        );

        _workSheduleRepository.Add(workShedule);
        await _unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}
