using Microsoft.EntityFrameworkCore;
using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.AllowanceAndPenalties;
using QuanLyNhanSu.Domain.Abstractions;
using QuanLyNhanSu.Domain.Abstractions.Repositories;
using QuanLyNhanSu.Domain.Entities;
using QuanLyNhanSu.Domain.Enumerations;
using QuanLyNhanSu.Domain.Exceptions;
using static QuanLyNhanSu.Domain.Enumerations.AllowanceAndPenaltyEnums;

namespace QuanLyNhanSu.Application.UserCases.V1.Commands.AllowanceAndPenalties;
public sealed class UpdateAllowanceCommandHandler : ICommandHandler<Command.UpdateAllowanceCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryBase<Domain.Entities.AllowanceAndPenalties, Guid> _allowanceAndPenaltiesRepository;
    private readonly IRepositoryBase<Domain.Entities.WorkPlace, Guid> _workPlaceRepository;
    private readonly IRepositoryBase<Domain.Entities.WorkPlaceAllowanceAndPenalties, Guid> _workPlaceAllowanceAndPenaltiesRepository;

    public UpdateAllowanceCommandHandler(IUnitOfWork unitOfWork,
        IRepositoryBase<Domain.Entities.AllowanceAndPenalties, Guid> allowanceAndPenaltiesRepository,
        IRepositoryBase<Domain.Entities.WorkPlace, Guid> workPlaceRepository,
        IRepositoryBase<WorkPlaceAllowanceAndPenalties, Guid> workPlaceAllowanceAndPenaltiesRepository)
    {
        _unitOfWork = unitOfWork;
        _allowanceAndPenaltiesRepository = allowanceAndPenaltiesRepository;
        _workPlaceRepository = workPlaceRepository;
        _workPlaceAllowanceAndPenaltiesRepository = workPlaceAllowanceAndPenaltiesRepository;
    }

    public async Task<Result> Handle(Command.UpdateAllowanceCommand request, CancellationToken cancellationToken)
    {
        var allowance = await _allowanceAndPenaltiesRepository.FindByIdAsync(request.Id)
           ?? throw new AllowanceAndPenaltiesException.AllowanceNotFoundException(request.Id);

        #region =====phu cap cho tat ca noi lam viec chuyển về 1 điểm làm việc=====

        if (request.IsAllWorkPlace == false && allowance.IsAllWorkPlace == true && request.WorkPlaceId != null)
        {
            var workPlaceAllowanceStatusInActive = _workPlaceAllowanceAndPenaltiesRepository
                .FindAll(x => x.AllowanceAndPenaltiesId == allowance.Id && x.Status == Domain.Enumerations.StatusEnums.Status.Active).ToList();

            foreach (var item in workPlaceAllowanceStatusInActive)
            {
                if (item.WorkPlaceId != request.WorkPlaceId)
                {
                    item.UpdateWorkPlaceAllowanceAndPenalties
                    (
                        StatusEnums.Status.InActive,
                        DateTime.Now
                    );

                    _workPlaceAllowanceAndPenaltiesRepository.Update(item);
                }
            }
        }

        #endregion

        #region=====phu cap chuyển từ 1 nơi làm việc cho tat ca noi lam viec=====

        if (request.IsAllWorkPlace == true && allowance.IsAllWorkPlace == false && request.WorkPlaceId == null)
        {
            var workPlaceAllowanceStatusInActive = await _workPlaceAllowanceAndPenaltiesRepository
                .FindAll(x => x.AllowanceAndPenaltiesId == allowance.Id).ToListAsync();

            if (workPlaceAllowanceStatusInActive.Any(x => x.Status == StatusEnums.Status.InActive && x.WorkPlace.IsDeleted != true))
            {
                foreach (var item in workPlaceAllowanceStatusInActive)
                {
                    if (item.Status == StatusEnums.Status.InActive)
                    {
                        item.UpdateWorkPlaceAllowanceAndPenalties
                        (
                            StatusEnums.Status.Active,
                            null
                        );
                        _workPlaceAllowanceAndPenaltiesRepository.Update(item);
                    }
                }
            }

            var workPlace = await _workPlaceRepository.FindAll(x=> x.IsDeleted != true).ToListAsync();
            
            foreach (var item in workPlace)
            {
                var workPlaceFindById = _workPlaceAllowanceAndPenaltiesRepository.FindAll(x=> x.WorkPlaceId == item.Id && x.AllowanceAndPenaltiesId == allowance.Id);
                if (!workPlaceFindById.Any())
                {
                    var workPlaceAllowanceAndPenalties = Domain.Entities.WorkPlaceAllowanceAndPenalties.CreateWorkPlaceAllowanceAndPenalties
                    (
                        Guid.NewGuid(),
                        item.Id,
                        allowance.Id
                    );
                    _workPlaceAllowanceAndPenaltiesRepository.Add(workPlaceAllowanceAndPenalties);
                }
            }
        }
        #endregion

        #region ======phu cap cho tung noi lam viec======
        if (request.IsAllWorkPlace == false && allowance.IsAllWorkPlace == false && request.WorkPlaceId != null)
        {
            var workPlaceIdInAllowance = await _workPlaceAllowanceAndPenaltiesRepository
                .FindAll().Where(x => x.AllowanceAndPenaltiesId == allowance.Id).AsQueryable().ToListAsync();

            if (workPlaceIdInAllowance.Any(x => x.Status == StatusEnums.Status.Active && x.WorkPlaceId == request.WorkPlaceId))
            {
                foreach (var item in workPlaceIdInAllowance)
                {
                    if (item.WorkPlaceId != request.WorkPlaceId && item.Status == StatusEnums.Status.Active)
                    {
                        item.UpdateWorkPlaceAllowanceAndPenalties
                        (
                            StatusEnums.Status.InActive,
                            DateTime.Now
                        );
                        _workPlaceAllowanceAndPenaltiesRepository.Update(item);
                    }
                }
            }
            else if (workPlaceIdInAllowance.Any(x => x.Status == StatusEnums.Status.InActive && x.WorkPlaceId == request.WorkPlaceId))
            {
                foreach (var item in workPlaceIdInAllowance)
                {
                    if (item.WorkPlaceId == request.WorkPlaceId && item.Status == StatusEnums.Status.InActive)
                    {
                        item.UpdateWorkPlaceAllowanceAndPenalties
                        (
                            StatusEnums.Status.Active,
                            null
                        );
                    }
                    if (item.WorkPlaceId != request.WorkPlaceId && item.Status == StatusEnums.Status.Active)
                    {
                        item.UpdateWorkPlaceAllowanceAndPenalties
                        (
                            StatusEnums.Status.InActive,
                            DateTime.Now
                        );
                    }
                    _workPlaceAllowanceAndPenaltiesRepository.Update(item);
                }
            }
            else
            {
                foreach (var item in workPlaceIdInAllowance)
                {
                    if (item.WorkPlaceId != request.WorkPlaceId && item.Status == StatusEnums.Status.Active)
                    {
                        item.UpdateWorkPlaceAllowanceAndPenalties
                        (
                            StatusEnums.Status.InActive,
                            DateTime.Now
                        );
                        _workPlaceAllowanceAndPenaltiesRepository.Update(item);
                    }
                }
                var workPlaceAllowance = Domain.Entities.WorkPlaceAllowanceAndPenalties.CreateWorkPlaceAllowanceAndPenalties
                (
                    Guid.NewGuid(),
                    request.WorkPlaceId,
                    allowance.Id
                );
                _workPlaceAllowanceAndPenaltiesRepository.Add(workPlaceAllowance);
            }
        }
        #endregion

        allowance.UpdateAllowanceAndPenalties
        (
            request.TypeOfAllowance,
            null,
            request.Money,
            request.EffectiveDate,
            request.Note,
            request.IsAllWorkPlace
        );

        _allowanceAndPenaltiesRepository.Update(allowance);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
