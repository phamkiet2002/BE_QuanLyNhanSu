using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.Contract;
using QuanLyNhanSu.Domain.Abstractions.Repositories;
using QuanLyNhanSu.Domain.Abstractions;

namespace QuanLyNhanSu.Application.UserCases.V1.Commands.Contract;
public sealed class CreateContractCommandHandler : ICommandHandler<Command.CreateContractCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryBase<Domain.Entities.Contract, Guid> _contractRepository;

    public CreateContractCommandHandler(IUnitOfWork unitOfWork, IRepositoryBase<Domain.Entities.Contract, Guid> contractRepository)
    {
        _unitOfWork = unitOfWork;
        _contractRepository = contractRepository;
    }

    public async Task<Result> Handle(Command.CreateContractCommand request, CancellationToken cancellationToken)
    {
        var contract = Domain.Entities.Contract.CreateContract
        (
            Guid.NewGuid(),
            request.ContracNumber,
            request.SignDate,
            request.EffectiveDate,
            request.ExpirationDate,
            request.EmployeeId
        );
        _contractRepository.Add(contract);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
