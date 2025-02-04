using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.Contract;
using QuanLyNhanSu.Domain.Abstractions.Repositories;
using QuanLyNhanSu.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace QuanLyNhanSu.Application.UserCases.V1.Commands.Contract;
public sealed class CheckContractExpirationCommandHandler : ICommandHandler<Command.CheckContractNearExpirationCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryBase<Domain.Entities.Contract, Guid> _contractRepository;

    public CheckContractExpirationCommandHandler(IUnitOfWork unitOfWork, IRepositoryBase<Domain.Entities.Contract, Guid> contractRepository)
    {
        _unitOfWork = unitOfWork;
        _contractRepository = contractRepository;
    }
    public async Task<Result> Handle(Command.CheckContractNearExpirationCommand request, CancellationToken cancellationToken)
    {
        var contracts = await _contractRepository.FindAll().ToListAsync();

        foreach (var contract in contracts)
        {
            // Kiểm tra nếu hợp đồng gần hết hạn
            if (contract.ExpirationDate <= DateTime.Now.AddDays(7))
            {
                contract.CheckContractNearExpiration();
                _contractRepository.Update(contract);
            }
        }
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
