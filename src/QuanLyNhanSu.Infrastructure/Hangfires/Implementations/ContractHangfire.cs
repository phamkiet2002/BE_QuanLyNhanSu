using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Service.V1.Contract;
using QuanLyNhanSu.Infrastructure.Hangfires.Abstractions;

namespace QuanLyNhanSu.Infrastructure.Hangfires.Implementations;
public sealed class ContractHangfire : IContractHangfire
{
    private readonly ICommandHandler<Command.CheckContractNearExpirationCommand> _checkContractExpirationCommandHandler;

    public ContractHangfire(ICommandHandler<Command.CheckContractNearExpirationCommand> checkContractExpirationCommandHandler)
    {
        _checkContractExpirationCommandHandler = checkContractExpirationCommandHandler;
    }

    public async Task CheckAndUpdateContractStatusAsync()
    {
        // Sử dụng CommandHandler để xử lý logic
        await _checkContractExpirationCommandHandler.Handle(new Command.CheckContractNearExpirationCommand(), CancellationToken.None);
    }
}
