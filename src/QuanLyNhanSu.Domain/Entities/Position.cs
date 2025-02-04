using QuanLyNhanSu.Domain.Abstractions.Entities;
using QuanLyNhanSu.Domain.Entities.Identity;

namespace QuanLyNhanSu.Domain.Entities;

public class Position : DomainEntity<Guid>, IAuditEntity
{
    #region =====attribute=====
    public string Name { get; private set; }
    public string Description { get; private set; }
    public bool? IsDeleted { get; set; }
    #endregion

    #region =====audit=====
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
    #endregion

    public virtual ICollection<EmployeePosition> EmployeePositions { get; set; }
    public virtual ICollection<PositionRole> PositionRoles { get; set; }

    public Position(Guid id, string name, string description)
    {
        Id = id;
        Name = name;
        Description = description;
        CreatedDate = DateTime.Now;
    }

    public static Position CreatePosition(Guid id, string name, string description)
    {
        return new Position(id, name, description);
    }

    public void UpdatePosition(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public void DeletePosition()
    {
        IsDeleted = true;
        DeletedDate = DateTime.Now;
    }
}
