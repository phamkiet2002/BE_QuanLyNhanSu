using System.Data;
using Microsoft.AspNetCore.Identity;
using QuanLyNhanSu.Domain.Abstractions.Entities;

namespace QuanLyNhanSu.Domain.Entities.Identity;
public class PositionRole : DomainEntity<Guid>
{
    public Guid? PositionId { get; set; }
    public virtual Position Position { get; set; }

    public string? RoleId { get; set; }
    public virtual AppRole AppRole { get; set; }

    public PositionRole() { }

    public PositionRole(Guid? positionId, string? roleId)
    {
        PositionId = positionId;
        RoleId = roleId;
    }

    public static PositionRole CreatePositionRole(Guid? positionId, string? roleId)
    {
        return new PositionRole(positionId, roleId);
    }

    public void UpdatePositionRole(Guid? positionId, string? roleId)
    {
        PositionId = positionId;
        RoleId = roleId;
    }
}
