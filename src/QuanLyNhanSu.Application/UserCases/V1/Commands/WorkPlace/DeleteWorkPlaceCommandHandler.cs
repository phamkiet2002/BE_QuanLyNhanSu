using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.WorkPlace;
using QuanLyNhanSu.Domain.Abstractions.Repositories;
using QuanLyNhanSu.Domain.Abstractions;
using QuanLyNhanSu.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace QuanLyNhanSu.Application.UserCases.V1.Commands.WorkPlace;
public sealed class DeleteWorkPlaceCommandHandler : ICommandHandler<Command.DeleteWorkPlaceCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryBase<Domain.Entities.WorkPlace, Guid> _workPlaceRepository;
    private readonly IRepositoryBase<Domain.Entities.Employee, Guid> _employeeRepository;

    public DeleteWorkPlaceCommandHandler
    (
        IUnitOfWork unitOfWork, IRepositoryBase<Domain.Entities.WorkPlace, Guid> workPlaceRepository, 
        IRepositoryBase<Domain.Entities.Employee, Guid> employeeRepository
    )
    {
        _unitOfWork = unitOfWork;
        _workPlaceRepository = workPlaceRepository;
        _employeeRepository = employeeRepository;
    }

    public async Task<Result> Handle(Command.DeleteWorkPlaceCommand request, CancellationToken cancellationToken)
    {
        var workPlace = await _workPlaceRepository.FindByIdAsync(request.Id)
            ?? throw new WorkPlaceException.WorkPlaceNotFoundException(request.Id);

        var employees = await _employeeRepository.FindAll(x => x.WorkPlaceId == workPlace.Id && x.Status == Domain.Enumerations.StatusEnums.Status.Active).AnyAsync();

        if (employees)
            throw new Exception("điểm làm việc này còn nhân viên vui lòng chuyển nhân viên trước khi xóa.");

        workPlace.DeleteWorkPlace();

        _workPlaceRepository.Update(workPlace);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
