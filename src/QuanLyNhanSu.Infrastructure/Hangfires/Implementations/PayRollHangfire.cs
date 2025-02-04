using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Service.V1.PayRoll;
using QuanLyNhanSu.Infrastructure.Hangfires.Abstractions;

namespace QuanLyNhanSu.Infrastructure.Hangfires.Implementations;
public sealed class PayRollHangfire : IPayRollHangfire
{
    private readonly ICommandHandler<Command.CreatePayRollCommand> _createPayroll;

    public PayRollHangfire(ICommandHandler<Command.CreatePayRollCommand> createPayroll)
    {
        _createPayroll = createPayroll;
    }

    public async Task CreatePayRollAsync()
    {
        await _createPayroll.Handle(new Command.CreatePayRollCommand(), CancellationToken.None);
    }
}
