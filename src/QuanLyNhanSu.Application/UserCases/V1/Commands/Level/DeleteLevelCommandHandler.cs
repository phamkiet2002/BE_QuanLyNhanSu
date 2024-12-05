using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.Level;
using QuanLyNhanSu.Domain.Abstractions.Repositories;
using QuanLyNhanSu.Domain.Abstractions;
using QuanLyNhanSu.Domain.Entities;
using QuanLyNhanSu.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace QuanLyNhanSu.Application.UserCases.V1.Commands.Level;
public sealed class DeleteLevelCommandHandler : ICommandHandler<Command.DeleteLevelCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryBase<Domain.Entities.Level, Guid> _levelRepository;
    private readonly IRepositoryBase<Domain.Entities.EmployeeLevel, Guid> _employeeLevelRepository;

    public DeleteLevelCommandHandler
    (
        IUnitOfWork unitOfWork, IRepositoryBase<Domain.Entities.Level, Guid> levelRepository, 
        IRepositoryBase<EmployeeLevel, Guid> employeeLevelRepository
    )
    {
        _unitOfWork = unitOfWork;
        _levelRepository = levelRepository;
        _employeeLevelRepository = employeeLevelRepository;
    }

    public async Task<Result> Handle(Command.DeleteLevelCommand request, CancellationToken cancellationToken)
    {
        var level = await _levelRepository.FindByIdAsync(request.Id)
            ?? throw new LevelException.LevelNotFoundException(request.Id);

        var employeeLevel = await _employeeLevelRepository
            .FindAll(x => x.LevelId == level.Id && x.Employee.Status == Domain.Enumerations.StatusEnums.Status.Active).AnyAsync();

        if (employeeLevel)
            throw new Exception("Không thể xóa cấp bậc này vì cấp bậc này đang được sử dụng cho nhân viên.");

        level.DeleteLevel();
        _levelRepository.Update(level);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
