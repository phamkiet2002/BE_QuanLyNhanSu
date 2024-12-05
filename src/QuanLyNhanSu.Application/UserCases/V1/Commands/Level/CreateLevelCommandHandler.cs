using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.Level;
using QuanLyNhanSu.Domain.Abstractions;
using QuanLyNhanSu.Domain.Abstractions.Repositories;

namespace QuanLyNhanSu.Application.UserCases.V1.Commands.Level;
public sealed class CreateLevelCommandHandler : ICommandHandler<Command.CreateLevelCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryBase<Domain.Entities.Level, Guid> _levelRepository;

    public CreateLevelCommandHandler(IUnitOfWork unitOfWork, IRepositoryBase<Domain.Entities.Level, Guid> levelRepository)
    {
        _unitOfWork = unitOfWork;
        _levelRepository = levelRepository;
    }

    public async Task<Result> Handle(Command.CreateLevelCommand request, CancellationToken cancellationToken)
    {
        var level = Domain.Entities.Level.CreateLevel
        (
            Guid.NewGuid(),
            request.Name,
            request.Description
        );
        _levelRepository.Add(level);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
