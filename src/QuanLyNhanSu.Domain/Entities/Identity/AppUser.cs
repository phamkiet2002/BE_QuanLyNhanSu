using Microsoft.AspNetCore.Identity;

namespace QuanLyNhanSu.Domain.Entities.Identity;

public class AppUser : IdentityUser
{
    public Guid? EmployeeId { get; set; }
    public virtual Employee Employee { get; set; }

    //public virtual ICollection<IdentityUserClaim<Guid>> Claims { get; set; }
    //public virtual ICollection<IdentityUserLogin<Guid>> Logins { get; set; }
    //public virtual ICollection<IdentityUserToken<Guid>> Tokens { get; set; }
    //public virtual ICollection<IdentityUserRole<Guid>> UserRoles { get; set; }
}
