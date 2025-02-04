using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.Level;
using QuanLyNhanSu.Domain.Abstractions.Repositories;
using QuanLyNhanSu.Domain.Abstractions;
using QuanLyNhanSu.Domain.Exceptions;

namespace QuanLyNhanSu.Application.UserCases.V1.Commands.Level;
public sealed class UpdateLevelCommandHandler : ICommandHandler<Command.UpdateLevelCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryBase<Domain.Entities.Level, Guid> _levelRepository;

    public UpdateLevelCommandHandler(IUnitOfWork unitOfWork, IRepositoryBase<Domain.Entities.Level, Guid> levelRepository)
    {
        _unitOfWork = unitOfWork;
        _levelRepository = levelRepository;
    }

    public async Task<Result> Handle(Command.UpdateLevelCommand request, CancellationToken cancellationToken)
    {
        var level = await _levelRepository.FindByIdAsync(request.Id)
            ?? throw new LevelException.LevelNotFoundException(request.Id);
        level.UpdateLevel(request.Name, request.Description);
        _levelRepository.Update(level);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
