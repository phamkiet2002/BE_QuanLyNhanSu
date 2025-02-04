using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.WorkPlace;
using QuanLyNhanSu.Domain.Abstractions.Repositories;
using QuanLyNhanSu.Domain.Abstractions;
using QuanLyNhanSu.Domain.Exceptions;

namespace QuanLyNhanSu.Application.UserCases.V1.Commands.WorkPlace;
public sealed class UpdateWorkPlaceCommandHandler : ICommandHandler<Command.UpdateWorkPlaceCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryBase<Domain.Entities.WorkPlace, Guid> _workPlaceRepository;

    public UpdateWorkPlaceCommandHandler(IUnitOfWork unitOfWork, IRepositoryBase<Domain.Entities.WorkPlace, Guid> workPlaceRepository)
    {
        _unitOfWork = unitOfWork;
        _workPlaceRepository = workPlaceRepository;
    }

    public async Task<Result> Handle(Command.UpdateWorkPlaceCommand request, CancellationToken cancellationToken)
    {
        var workPlace = await _workPlaceRepository.FindByIdAsync(request.Id)
            ?? throw new WorkPlaceException.WorkPlaceNotFoundException(request.Id);
        workPlace.UpdateWorkPlace(request.Name, request.Phone, request.Email, request.Address);
        _workPlaceRepository.Update(workPlace);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
