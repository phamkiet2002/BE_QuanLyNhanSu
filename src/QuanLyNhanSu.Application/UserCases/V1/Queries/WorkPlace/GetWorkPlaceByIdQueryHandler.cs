using AutoMapper;
using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.WorkPlace;
using QuanLyNhanSu.Domain.Abstractions.Repositories;
using QuanLyNhanSu.Domain.Exceptions;

namespace QuanLyNhanSu.Application.UserCases.V1.Queries.WorkPlace;
public sealed class GetWorkPlaceByIdQueryHandler : IQueryHandler<Query.GetWorkPlaceByIdQuery, Response.WorkPlaceResponse>
{
    private readonly IMapper _mapper;
    private readonly IRepositoryBase<Domain.Entities.WorkPlace, Guid> _workPlaceRepository;

    public GetWorkPlaceByIdQueryHandler(IMapper mapper, IRepositoryBase<Domain.Entities.WorkPlace, Guid> workPlaceRepository)
    {
        _mapper = mapper;
        _workPlaceRepository = workPlaceRepository;
    }

    public async Task<Result<Response.WorkPlaceResponse>> Handle(Query.GetWorkPlaceByIdQuery request, CancellationToken cancellationToken)
    {
        var workPlace = await _workPlaceRepository.FindByIdAsync(request.Id)
            ?? throw new WorkPlaceException.WorkPlaceNotFoundException(request.Id);
        var result = _mapper.Map<Response.WorkPlaceResponse>(workPlace);
        return Result.Success(result);
    }
}
