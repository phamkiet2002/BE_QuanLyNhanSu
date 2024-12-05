using QuanLyNhanSu.Contract.Abstractions.Message;

namespace QuanLyNhanSu.Contract.Service.V1.AppRole;
public static class Response
{
    public record GetAppRoleResponse(Guid Id, string Name, string Description);
}
