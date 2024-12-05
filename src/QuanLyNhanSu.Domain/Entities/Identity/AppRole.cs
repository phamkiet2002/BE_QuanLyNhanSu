using Microsoft.AspNetCore.Identity;
using QuanLyNhanSu.Domain.Abstractions.Entities;

namespace QuanLyNhanSu.Domain.Entities.Identity;
public class AppRole : IdentityRole
{
    public string Description { get; set; }
    public virtual ICollection<PositionRole> PositionRoles { get; set; }
}
