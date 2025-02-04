using Microsoft.VisualBasic;
using QuanLyNhanSu.Domain.Entities.Identity;

namespace QuanLyNhanSu.Contract.Service.V1.Position;
public static class Response
{
    public record PositionResponse(Guid Id, string Name, string Description, 
        List<PositionRole.Response.PositionRoleResponse> PositionRoles, DateTime CreatedDate);
}
