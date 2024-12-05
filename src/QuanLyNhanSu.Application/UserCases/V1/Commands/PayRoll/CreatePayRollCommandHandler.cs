using Microsoft.EntityFrameworkCore;
using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.PayRoll;
using QuanLyNhanSu.Domain.Abstractions;
using QuanLyNhanSu.Domain.Abstractions.Repositories;

namespace QuanLyNhanSu.Application.UserCases.V1.Commands.PayRoll;
public sealed class CreatePayRollCommandHandler : ICommandHandler<Command.CreatePayRollCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryBase<Domain.Entities.PayRoll, Guid> _payRollRepository;
    private readonly IRepositoryBase<Domain.Entities.Employee, Guid> _employeeRepository;

    public CreatePayRollCommandHandler(IUnitOfWork unitOfWork, IRepositoryBase<Domain.Entities.PayRoll, Guid> payRollRepository, IRepositoryBase<Domain.Entities.Employee, Guid> employeeRepository)
    {
        _unitOfWork = unitOfWork;
        _payRollRepository = payRollRepository;
        _employeeRepository = employeeRepository;
    }

    public async Task<Result> Handle(Command.CreatePayRollCommand request, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.FindAll().ToListAsync();

        foreach (var item in employee)
        {
            if (item.Status != Domain.Enumerations.StatusEnums.Status.InActive)
            {
                var payRoll = Domain.Entities.PayRoll.CreatePayRoll
                (
                    Guid.NewGuid(),
                    item.Id
                );
                _payRollRepository.Add(payRoll);
            }
        }
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
