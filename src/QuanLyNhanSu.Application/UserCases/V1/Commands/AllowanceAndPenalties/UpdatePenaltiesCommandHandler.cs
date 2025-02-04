using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.AllowanceAndPenalties;
using QuanLyNhanSu.Domain.Abstractions.Repositories;
using QuanLyNhanSu.Domain.Abstractions;
using QuanLyNhanSu.Domain.Entities;
using QuanLyNhanSu.Domain.Exceptions;
using static QuanLyNhanSu.Domain.Enumerations.AllowanceAndPenaltyEnums;
using Microsoft.EntityFrameworkCore;
using QuanLyNhanSu.Domain.Enumerations;

namespace QuanLyNhanSu.Application.UserCases.V1.Commands.AllowanceAndPenalties;
public sealed class UpdatePenaltiesCommandHandler : ICommandHandler<Command.UpdatePenaltiesCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryBase<Domain.Entities.AllowanceAndPenalties, Guid> _allowanceAndPenaltiesRepository;
    private readonly IRepositoryBase<Domain.Entities.WorkPlace, Guid> _workPlaceRepository;
    private readonly IRepositoryBase<Domain.Entities.WorkPlaceAllowanceAndPenalties, Guid> _workPlaceAllowanceAndPenaltiesRepository;

    public UpdatePenaltiesCommandHandler(IUnitOfWork unitOfWork,
        IRepositoryBase<Domain.Entities.AllowanceAndPenalties, Guid> allowanceAndPenaltiesRepository,
        IRepositoryBase<Domain.Entities.WorkPlace, Guid> workPlaceRepository,
        IRepositoryBase<WorkPlaceAllowanceAndPenalties, Guid> workPlaceAllowanceAndPenaltiesRepository)
    {
        _unitOfWork = unitOfWork;
        _allowanceAndPenaltiesRepository = allowanceAndPenaltiesRepository;
        _workPlaceRepository = workPlaceRepository;
        _workPlaceAllowanceAndPenaltiesRepository = workPlaceAllowanceAndPenaltiesRepository;
    }

    public async Task<Result> Handle(Command.UpdatePenaltiesCommand request, CancellationToken cancellationToken)
    {
        var penalties = await _allowanceAndPenaltiesRepository.FindByIdAsync(request.Id)
          ?? throw new AllowanceAndPenaltiesException.AllowanceNotFoundException(request.Id);

        #region =====chuyển tất cả nởi làm việc về 1 điểm làm việc=====

        if (request.IsAllWorkPlace == false && penalties.IsAllWorkPlace == true && request.WorkPlaceId != null)
        {
            var workPlacePenaltiesStatusInActive = _workPlaceAllowanceAndPenaltiesRepository
                .FindAll(x => x.AllowanceAndPenaltiesId == penalties.Id && x.Status == Domain.Enumerations.StatusEnums.Status.Active).ToList();

            foreach (var item in workPlacePenaltiesStatusInActive)
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

        #region=====phạt chuyển từ 1 nơi làm việc cho tat ca noi lam viec=====

        if (request.IsAllWorkPlace == true && penalties.IsAllWorkPlace == false && request.WorkPlaceId == null)
        {
            var workPlacePenaltiesStatusInActive = await _workPlaceAllowanceAndPenaltiesRepository
                .FindAll(x => x.AllowanceAndPenaltiesId == penalties.Id).ToListAsync();

            if (workPlacePenaltiesStatusInActive.Any(x => x.Status == StatusEnums.Status.InActive && x.WorkPlace.IsDeleted != true))
            {
                foreach (var item in workPlacePenaltiesStatusInActive)
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

            var workPlace = await _workPlaceRepository.FindAll(x => x.IsDeleted != true).ToListAsync();

            foreach (var item in workPlace)
            {
                var workPlaceFindById = _workPlaceAllowanceAndPenaltiesRepository.FindAll(x => x.WorkPlaceId == item.Id && x.AllowanceAndPenaltiesId == penalties.Id);
                if (!workPlaceFindById.Any())
                {
                    var workPlaceAllowanceAndPenalties = Domain.Entities.WorkPlaceAllowanceAndPenalties.CreateWorkPlaceAllowanceAndPenalties
                    (
                        Guid.NewGuid(),
                        item.Id,
                        penalties.Id
                    );
                    _workPlaceAllowanceAndPenaltiesRepository.Add(workPlaceAllowanceAndPenalties);
                }
            }
        }

        #endregion

        #region ======phạt cap cho tung noi lam viec======

        if (request.IsAllWorkPlace == false && penalties.IsAllWorkPlace == false && request.WorkPlaceId != null)
        {
            var workPlaceIdInPenalties = await _workPlaceAllowanceAndPenaltiesRepository
                .FindAll().Where(x => x.AllowanceAndPenaltiesId == penalties.Id).AsQueryable().ToListAsync();

            if (workPlaceIdInPenalties.Any(x => x.Status == StatusEnums.Status.Active && x.WorkPlaceId == request.WorkPlaceId))
            {
                foreach (var item in workPlaceIdInPenalties)
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
            else if (workPlaceIdInPenalties.Any(x => x.Status == StatusEnums.Status.InActive && x.WorkPlaceId == request.WorkPlaceId))
            {
                foreach (var item in workPlaceIdInPenalties)
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
                foreach (var item in workPlaceIdInPenalties)
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
                var workPlacePenalties = Domain.Entities.WorkPlaceAllowanceAndPenalties.CreateWorkPlaceAllowanceAndPenalties
                (
                    Guid.NewGuid(),
                    request.WorkPlaceId,
                    penalties.Id
                );
                _workPlaceAllowanceAndPenaltiesRepository.Add(workPlacePenalties);
            }
        }

        #endregion

        penalties.UpdateAllowanceAndPenalties
        (
            null,
            request.TypeOfPenalty,
            request.Money,
            request.EffectiveDate,
            request.Note,
            request.IsAllWorkPlace
        );

        _allowanceAndPenaltiesRepository.Update(penalties);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
