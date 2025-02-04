using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.WorkPlace;
using QuanLyNhanSu.Domain.Abstractions;
using QuanLyNhanSu.Domain.Abstractions.Repositories;

namespace QuanLyNhanSu.Application.UserCases.V1.Commands.WorkPlace;

public sealed class CreateWorkPlaceCommandHandler : ICommandHandler<Command.CreateWorkPlaceCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryBase<Domain.Entities.WorkPlace, Guid> _workPlaceRepository;
    private readonly IRepositoryBase<Domain.Entities.AllowanceAndPenalties, Guid> _allowanceAndPenaltiesRepository;
    private readonly IRepositoryBase<Domain.Entities.WorkPlaceAllowanceAndPenalties, Guid> _workPlaceAllowanceAndPenaltiesRepository;

    public CreateWorkPlaceCommandHandler(IUnitOfWork unitOfWork, IRepositoryBase<Domain.Entities.WorkPlace, Guid> workPlaceRepository,
        IRepositoryBase<Domain.Entities.AllowanceAndPenalties, Guid> allowanceAndPenaltiesRepository,
        IRepositoryBase<Domain.Entities.WorkPlaceAllowanceAndPenalties, Guid> workPlaceAllowanceAndPenaltiesRepository)
    {
        _unitOfWork = unitOfWork;
        _workPlaceRepository = workPlaceRepository;
        _allowanceAndPenaltiesRepository = allowanceAndPenaltiesRepository;
        _workPlaceAllowanceAndPenaltiesRepository = workPlaceAllowanceAndPenaltiesRepository;
    }

    public async Task<Result> Handle(Command.CreateWorkPlaceCommand request, CancellationToken cancellationToken)
    {
        var workPlace = Domain.Entities.WorkPlace.CreateWorkPlace
        (
            request.Name,
            request.Phone,
            request.Email,
            request.Address
        );
        _workPlaceRepository.Add(workPlace);

        var allowanceAndPenalties = _allowanceAndPenaltiesRepository.FindAll(x => x.IsAllWorkPlace == true && x.IsDeleted != true);
        foreach (var item in allowanceAndPenalties)
        {
            var workPlaceAllowanceAndPenalties = Domain.Entities.WorkPlaceAllowanceAndPenalties.CreateWorkPlaceAllowanceAndPenalties
            (
                Guid.NewGuid(),
                workPlace.Id,
                item.Id
            );
            _workPlaceAllowanceAndPenaltiesRepository.Add(workPlaceAllowanceAndPenalties);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
