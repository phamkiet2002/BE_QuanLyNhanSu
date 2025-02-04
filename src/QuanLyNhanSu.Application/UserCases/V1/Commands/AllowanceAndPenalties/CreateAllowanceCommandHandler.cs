using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.AllowanceAndPenalties;
using QuanLyNhanSu.Domain.Abstractions.Repositories;
using QuanLyNhanSu.Domain.Abstractions;

namespace QuanLyNhanSu.Application.UserCases.V1.Commands.AllowanceAndPenalties;
public sealed class CreateAllowanceCommandHandler : ICommandHandler<Command.CreateAllowanceCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryBase<Domain.Entities.AllowanceAndPenalties, Guid> _allowanceAndPenaltiesRepository;
    private readonly IRepositoryBase<Domain.Entities.WorkPlace, Guid> _workPlaceRepository;
    private readonly IRepositoryBase<Domain.Entities.WorkPlaceAllowanceAndPenalties, Guid> _workPlaceAllowanceAndPenaltiesRepository;

    public CreateAllowanceCommandHandler(
        IUnitOfWork unitOfWork,
        IRepositoryBase<Domain.Entities.AllowanceAndPenalties, Guid> allowanceAndPenaltiesRepository,
        IRepositoryBase<Domain.Entities.WorkPlace, Guid> workPlaceRepository,
        IRepositoryBase<Domain.Entities.WorkPlaceAllowanceAndPenalties, Guid> workPlaceAllowanceAndPenaltiesRepository)
    {
        _unitOfWork = unitOfWork;
        _allowanceAndPenaltiesRepository = allowanceAndPenaltiesRepository;
        _workPlaceRepository = workPlaceRepository;
        _workPlaceAllowanceAndPenaltiesRepository = workPlaceAllowanceAndPenaltiesRepository;
    }

    public async Task<Result> Handle(Command.CreateAllowanceCommand request, CancellationToken cancellationToken)
    {
        var allowance = Domain.Entities.AllowanceAndPenalties.CreateAllowance
        (
            Guid.NewGuid(),
            request.TypeOfAllowance,
            request.Money,
            request.EffectiveDate,
            request.Note,
            request.IsAllWorkPlace
        );
        _allowanceAndPenaltiesRepository.Add(allowance);

        if (allowance.IsAllWorkPlace == true && request.WorkPlaceId == null)
        {
            foreach (var workPlace in _workPlaceRepository.FindAll())
            {
                if (workPlace.IsDeleted != true)
                {
                    var workPlaceAllowanceAndPenalties = Domain.Entities.WorkPlaceAllowanceAndPenalties.CreateWorkPlaceAllowanceAndPenalties
                    (
                        Guid.NewGuid(),
                        workPlace.Id,
                        allowance.Id
                    );
                    _workPlaceAllowanceAndPenaltiesRepository.Add(workPlaceAllowanceAndPenalties);
                }
            }
        }

        if (allowance.IsAllWorkPlace == false && request.WorkPlaceId != null)
        {
            if (request.WorkPlaceId.HasValue)
            {
                var workPlaceAllowanceAndPenalties = Domain.Entities.WorkPlaceAllowanceAndPenalties.CreateWorkPlaceAllowanceAndPenalties
                (
                    Guid.NewGuid(),
                    request?.WorkPlaceId.Value,
                    allowance.Id
                );

                _workPlaceAllowanceAndPenaltiesRepository.Add(workPlaceAllowanceAndPenalties);
            }
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
