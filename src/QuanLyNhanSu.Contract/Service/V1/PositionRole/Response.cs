namespace QuanLyNhanSu.Contract.Service.V1.PositionRole;
public static class Response
{
    public record PositionRoleResponse(AppRole.Response.GetAppRoleResponse AppRole);
}
