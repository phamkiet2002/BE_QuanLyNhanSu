using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.AllowanceAndPenalties;
using QuanLyNhanSu.Domain.Abstractions.Repositories;
using QuanLyNhanSu.Domain.Abstractions;
using QuanLyNhanSu.Domain.Exceptions;
using static QuanLyNhanSu.Domain.Exceptions.AllowanceAndPenaltiesException;

namespace QuanLyNhanSu.Application.UserCases.V1.Commands.AllowanceAndPenalties;
public sealed class DeleteAllowanceAndPenaltiesCommandHandler : ICommandHandler<Command.DeleteAllowanceAndPenaltiesCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryBase<Domain.Entities.AllowanceAndPenalties, Guid> _allowanceAndPenaltiesRepository;

    public DeleteAllowanceAndPenaltiesCommandHandler(IUnitOfWork unitOfWork, IRepositoryBase<Domain.Entities.AllowanceAndPenalties, Guid> allowanceAndPenaltiesRepository)
    {
        _unitOfWork = unitOfWork;
        _allowanceAndPenaltiesRepository = allowanceAndPenaltiesRepository;
    }

    public async Task<Result> Handle(Command.DeleteAllowanceAndPenaltiesCommand request, CancellationToken cancellationToken)
    {
        var allowanceAndPenalties = await _allowanceAndPenaltiesRepository.FindByIdAsync(request.Id)
            ?? throw new AllowanceAndPenaltiesNotFoundException(request.Id);

        allowanceAndPenalties.DeleteAllowanceAndPenalties();
        _allowanceAndPenaltiesRepository.Update(allowanceAndPenalties);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
