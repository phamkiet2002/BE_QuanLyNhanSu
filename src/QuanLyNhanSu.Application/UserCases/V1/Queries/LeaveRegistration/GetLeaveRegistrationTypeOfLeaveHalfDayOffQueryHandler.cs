using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.LeaveRegistration;
using QuanLyNhanSu.Domain.Abstractions.Repositories;
using static QuanLyNhanSu.Domain.Enumerations.ApproveEmuns;
using static QuanLyNhanSu.Domain.Enumerations.StatusEnums;

namespace QuanLyNhanSu.Application.UserCases.V1.Queries.LeaveRegistration;
public sealed class GetLeaveRegistrationTypeOfLeaveHalfDayOffQueryHandler : IQueryHandler<Query.GetLeaveRegistrationTypeOfLeaveHalfDayOffsQuery, PagedResult<Response.LeaveRegistrationTypeOfLeaveHalfDayOffResponse>>
{
    private readonly IMapper _mapper;
    private readonly IRepositoryBase<Domain.Entities.LeaveRegistration, Guid> _leaveRegistrationRepository;

    public GetLeaveRegistrationTypeOfLeaveHalfDayOffQueryHandler(IMapper mapper, IRepositoryBase<Domain.Entities.LeaveRegistration, Guid> leaveRegistrationRepository)
    {
        _mapper = mapper;
        _leaveRegistrationRepository = leaveRegistrationRepository;
    }

    public async Task<Result<PagedResult<Response.LeaveRegistrationTypeOfLeaveHalfDayOffResponse>>> Handle(Query.GetLeaveRegistrationTypeOfLeaveHalfDayOffsQuery request, CancellationToken cancellationToken)
    {
        var PageIndex = request.PageIndex <= 0 ? PagedResult<Domain.Entities.LeaveRegistration>.DefaultPageIndex : request.PageIndex;
        var PageSize = request.PageSize <= 0
            ? PagedResult<Domain.Entities.LeaveRegistration>.DefaultPageSize
            : request.PageSize > PagedResult<Domain.Entities.LeaveRegistration>.UpperPageSize
            ? PagedResult<Domain.Entities.LeaveRegistration>.UpperPageSize : request.PageSize;

        var query = _leaveRegistrationRepository.FindAll(x => x.TypeOfLeave == Domain.Enumerations.LeaveRegistrationEnums.TypeOfLeave.Nghibuoi & x.Employee.Status == Status.Active);

        var pendingApprovalFilter = request.PendingApproval == null ? PendingApproval.Chuaduyet : request.PendingApproval;

        if (pendingApprovalFilter != null)
        {
            query = query.Where(x => x.PendingApproval == pendingApprovalFilter);
        }

        var searchTerms = request.SearchTerm?.ToLower() ?? string.Empty;
        if (!string.IsNullOrEmpty(searchTerms))
        {
            query = query.Where(x => x.Employee.Name.Contains(searchTerms));
        }

        // sắp xếp theo ngày có hiện lực mới nhất theo asc
        var sortOrder = (request.SortOrder ?? "des").ToLower();
        query = sortOrder == "des"
           ? query.OrderByDescending(x => x.CreatedDate)
           : sortOrder == "asc" ? query.OrderBy(x => x.CreatedDate)
           : query.OrderByDescending(x => x.CreatedDate);

        var leaveRegistrations = await query
           .Skip((PageIndex - 1) * PageSize)
           .Take(PageSize)
           .ToListAsync(cancellationToken);

        var totalCount = await query.CountAsync(cancellationToken);

        var leaveRegistrationPagedResult = PagedResult<Domain.Entities.LeaveRegistration>.Create
        (
            leaveRegistrations,
            PageIndex,
            PageSize,
            totalCount
        );

        var result = _mapper.Map<PagedResult<Response.LeaveRegistrationTypeOfLeaveHalfDayOffResponse>>(leaveRegistrationPagedResult);

        return Result.Success(result);
    }
}
