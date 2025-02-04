using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.WorkShedule;
using QuanLyNhanSu.Domain.Abstractions.Repositories;
using QuanLyNhanSu.Domain.Abstractions;
using QuanLyNhanSu.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace QuanLyNhanSu.Application.UserCases.V1.Commands.WorkShedule;
public sealed class DeleteWorkSheduleCommandHandler : ICommandHandler<Command.DeleteWorkSheduleCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryBase<Domain.Entities.WorkShedule, Guid> _workSheduleRepository;
    private readonly IRepositoryBase<Domain.Entities.Department, Guid> _departmentRepository;

    public DeleteWorkSheduleCommandHandler(IUnitOfWork unitOfWork, IRepositoryBase<Domain.Entities.WorkShedule, Guid> workSheduleRepository, 
        IRepositoryBase<Domain.Entities.Department, Guid> departmentRepository)
    {
        _unitOfWork = unitOfWork;
        _workSheduleRepository = workSheduleRepository;
        _departmentRepository = departmentRepository;
    }

    public async Task<Result> Handle(Command.DeleteWorkSheduleCommand request, CancellationToken cancellationToken)
    {
        var workShedule = await _workSheduleRepository.FindByIdAsync(request.Id)
            ?? throw new WorkSheduleException.WorkSheduleNotFoundException(request.Id);

        var department = await _departmentRepository.FindAll(x => x.WorkSheduleId == workShedule.Id).AnyAsync();
        if (department) 
            throw new Exception("giờ làm việc này đã được áp dụng cho phòng ban vui lòng chuyển thời gian làm việc khác cho phòng ban trước khi xóa.");

        workShedule.DeleteWorkShedule();

        _workSheduleRepository.Update(workShedule);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
