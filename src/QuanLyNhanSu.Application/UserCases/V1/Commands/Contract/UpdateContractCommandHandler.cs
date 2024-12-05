using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.Contract;
using QuanLyNhanSu.Domain.Abstractions.Repositories;
using QuanLyNhanSu.Domain.Abstractions;
using QuanLyNhanSu.Domain.Exceptions;

namespace QuanLyNhanSu.Application.UserCases.V1.Commands.Contract;
public sealed class UpdateContractCommandHandler : ICommandHandler<Command.UpdateContractCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryBase<Domain.Entities.Contract, Guid> _contractRepository;

    public UpdateContractCommandHandler(IUnitOfWork unitOfWork, IRepositoryBase<Domain.Entities.Contract, Guid> contractRepository)
    {
        _unitOfWork = unitOfWork;
        _contractRepository = contractRepository;
    }

    public async Task<Result> Handle(Command.UpdateContractCommand request, CancellationToken cancellationToken)
    {
        var contract = await _contractRepository.FindByIdAsync(request.Id)
            ?? throw new ContractException.ContractNotFoundException(request.Id);
        contract.UpdateContract(request.ContracNumber, request.SignDate, request.EffectiveDate, request.ExpirationDate, request.EmployeeId);
        _contractRepository.Update(contract);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
