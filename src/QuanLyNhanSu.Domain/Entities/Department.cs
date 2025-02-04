using QuanLyNhanSu.Domain.Abstractions.Entities;

namespace QuanLyNhanSu.Domain.Entities;

public class Department : DomainEntity<Guid>, IAuditEntity
{
    #region =====attribute=====
    public string Name { get; private set; }
    public string Description { get; private set; }
    public bool? IsDeleted { get; set; }

    public Guid? WorkPlaceId { get; private set; }
    public virtual WorkPlace WorkPlace { get; private set; }

    public Guid? WorkSheduleId { get; private set; }
    public virtual WorkShedule WorkShedule { get; set; }
    #endregion

    #region =====audit=====
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
    #endregion

    public virtual ICollection<EmployeeDepartment> EmployeeDepartments { get; set; }

    public Department(Guid id, string name, string description, Guid? workPlaceId, Guid? workSheduleId)
    {
        Id = id;
        Name = name;
        Description = description;
        WorkPlaceId = workPlaceId;
        WorkSheduleId = workSheduleId;
        CreatedDate = DateTime.Now;
    }

    public static Department CreateDepartment(Guid id, string name, string description, Guid? workPlaceId, Guid? workSheduleId)
    {
        return new Department(id, name, description, workPlaceId, workSheduleId);
    }

    public void UpdateDepartment(string name, string description, Guid? workPlaceId, Guid? workSheduleId)
    {
        Name = name;
        Description = description;
        WorkPlaceId = workPlaceId;
        WorkSheduleId = workSheduleId;
    }

    public void DeleteDepartment()
    {
        IsDeleted = true;
        DeletedDate = DateTime.Now;
    }
}
