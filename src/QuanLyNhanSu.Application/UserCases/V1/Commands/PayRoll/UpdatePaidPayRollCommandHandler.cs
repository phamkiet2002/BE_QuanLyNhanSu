using Microsoft.AspNetCore.Http;
using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.PayRoll;
using QuanLyNhanSu.Domain.Abstractions;
using QuanLyNhanSu.Domain.Abstractions.Repositories;

namespace QuanLyNhanSu.Application.UserCases.V1.Commands.PayRoll;
public sealed class UpdatePaidPayRollCommandHandler : ICommandHandler<Command.UpdatePaidPayRollCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryBase<Domain.Entities.PayRoll, Guid> _payRollRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UpdatePaidPayRollCommandHandler(IUnitOfWork unitOfWork, IRepositoryBase<Domain.Entities.PayRoll, Guid> payRollRepository)
    {
        _unitOfWork = unitOfWork;
        _payRollRepository = payRollRepository;
    }

    public async Task<Result> Handle(Command.UpdatePaidPayRollCommand request, CancellationToken cancellationToken)
    {
        var payRoll = await _payRollRepository.FindByIdAsync(request.Id)
            ?? throw new Exception($"không tìm thấy {request.Id}");

        if (request.PayRollStatus == Domain.Enumerations.PayRollEnums.PayRollStatus.PAID)
        {

            payRoll.PaidAndUnPaidPaymentDate
            (
                request.PayRollStatus,
                null,
                DateTime.Now
            );
        }
        else
        {
            payRoll.PaidAndUnPaidPaymentDate
            (
                request.PayRollStatus,
                request.ReasonNote,
                null
            );

        }
        _payRollRepository.Update(payRoll);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
