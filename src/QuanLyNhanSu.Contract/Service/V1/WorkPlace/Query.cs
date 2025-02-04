using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;

namespace QuanLyNhanSu.Contract.Service.V1.WorkPlace;
public static class Query
{
    public record GetWorkPlacesQuery(string SearchTerm, string? SortOrder, int PageIndex, int PageSize) : IQuery<PagedResult<Response.WorkPlaceResponse>>;
    public record GetWorkPlaceByIdQuery(Guid Id) : IQuery<Response.WorkPlaceResponse>;
}
