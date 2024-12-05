using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.WorkShedule;
using QuanLyNhanSu.Domain.Abstractions.Repositories;
using QuanLyNhanSu.Domain.Abstractions;
using QuanLyNhanSu.Domain.Exceptions;

namespace QuanLyNhanSu.Application.UserCases.V1.Commands.WorkShedule;
public sealed class UpdateWorkSheduleCommandHandler : ICommandHandler<Command.UpdateWorkSheduleCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryBase<Domain.Entities.WorkShedule, Guid> _workSheduleRepository;

    public UpdateWorkSheduleCommandHandler(IUnitOfWork unitOfWork, IRepositoryBase<Domain.Entities.WorkShedule, Guid> workSheduleRepository)
    {
        _unitOfWork = unitOfWork;
        _workSheduleRepository = workSheduleRepository;
    }

    public async Task<Result> Handle(Command.UpdateWorkSheduleCommand request, CancellationToken cancellationToken)
    {
        var workShedule = await _workSheduleRepository.FindByIdAsync(request.Id)
            ?? throw new WorkSheduleException.WorkSheduleNotFoundException(request.Id);

        workShedule.UpdateWorkShedule(request.Name, request.StartTime, request.EndTime, request.BreakStartTime, request.BreakEndTime);

        _workSheduleRepository.Update(workShedule);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
