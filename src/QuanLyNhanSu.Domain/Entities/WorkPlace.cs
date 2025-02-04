using QuanLyNhanSu.Domain.Abstractions.Entities;
using static QuanLyNhanSu.Domain.Enumerations.StatusEnums;

namespace QuanLyNhanSu.Domain.Entities;

public class WorkPlace : DomainEntity<Guid>, IAuditEntity
{
    #region =====attribute=====
    public string Name { get; private set; }
    public string Phone { get; private set; }
    public string Email { get; private set; }
    public string Address { get; private set; }
    public bool? IsDeleted { get; set; }
    #endregion

    #region =====audit=====
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
    #endregion

    public virtual ICollection<Department> Departments { get; set; }
    public virtual ICollection<WorkPlaceAllowanceAndPenalties> WorkPlaceAndAllowanceAndPenalties { get; set; }
    public virtual ICollection<Employee> Employees { get; set; }
    public virtual WifiConfig? WifiConfig { get; set; }

    public WorkPlace(Guid id, string name, string phone, string email, string address)
    {
        Id = id;
        Name = name;
        Phone = phone;
        Email = email;
        Address = address;
        CreatedDate = DateTime.Now;
    }

    public static WorkPlace CreateWorkPlace(string name, string phone, string email, string address)
    {
        return new WorkPlace(Guid.NewGuid(), name, phone, email, address);
    }

    public void UpdateWorkPlace(string name, string phone, string email, string address)
    {
        Name = name;
        Phone = phone;
        Email = email;
        Address = address;
    }

    public void DeleteWorkPlace()
    {
        IsDeleted = true;
        DeletedDate = DateTime.Now;
    }
}
