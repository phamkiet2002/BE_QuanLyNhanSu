using QuanLyNhanSu.Contract.Abstractions.Message;

namespace QuanLyNhanSu.Contract.Service.V1.AppRole;
public static class Query
{
    public record GetAppRoleQuery() : IQuery<List<Response.GetAppRoleResponse>>;
}
