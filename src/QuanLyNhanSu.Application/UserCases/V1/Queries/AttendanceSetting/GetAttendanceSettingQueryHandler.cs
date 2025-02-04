using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.AttendanceSetting;
using QuanLyNhanSu.Domain.Abstractions.Repositories;

namespace QuanLyNhanSu.Application.UserCases.V1.Queries.AttendanceSetting;
public sealed class GetAttendanceSettingQueryHandler : IQueryHandler<Query.GetAttendanceSettingQuery, PagedResult<Response.AttendanceSettingResponse>>
{
    private readonly IRepositoryBase<Domain.Entities.AttendanceSetting, Guid> _attendanceSettingRepository;
    private readonly IMapper _mapper;
    public GetAttendanceSettingQueryHandler(IRepositoryBase<Domain.Entities.AttendanceSetting, Guid> attendanceSettingRepository, IMapper mapper)
    {
        _attendanceSettingRepository = attendanceSettingRepository;
        _mapper = mapper;
    }

    public async Task<Result<PagedResult<Response.AttendanceSettingResponse>>> Handle(Query.GetAttendanceSettingQuery request, CancellationToken cancellationToken)
    {
        var PageIndex = request.PageIndex <= 0 ? PagedResult<Domain.Entities.AttendanceSetting>.DefaultPageIndex : request.PageIndex;
        var PageSize = request.PageSize <= 0
            ? PagedResult<Domain.Entities.AttendanceSetting>.DefaultPageSize
            : request.PageSize > PagedResult<Domain.Entities.AttendanceSetting>.UpperPageSize
            ? PagedResult<Domain.Entities.AttendanceSetting>.UpperPageSize : request.PageSize;

        var query = _attendanceSettingRepository.FindAll(x=> x.IsDeleted != true);

        var status = request.Status; //== null ? Domain.Enumerations.StatusEnums.Status.Active : request.Status;

        if (status != null)
        {
            query = query.Where(x => x.Status == status);
        }

        // sắp xếp theo ngày có hiện lực mới nhất theo asc
        var sortOrder = (request.SortOrder ?? "des").ToLower();
        query = sortOrder == "des"
           ? query.OrderByDescending(x => x.CreatedDate)
           : sortOrder == "asc" ? query.OrderBy(x => x.CreatedDate)
           : query.OrderByDescending(x => x.CreatedDate);

        var attendanceSetting = await query
            .Skip((PageIndex - 1) * PageSize)
            .Take(PageSize)
            .ToListAsync(cancellationToken);

        var totalCount = await query.CountAsync(cancellationToken);

        var contractPagedResult = PagedResult<Domain.Entities.AttendanceSetting>.Create(
            attendanceSetting,
            PageIndex,
            PageSize,
            totalCount);

        var result = _mapper.Map<PagedResult<Response.AttendanceSettingResponse>>(contractPagedResult);

        return Result.Success(result);
    }
}
