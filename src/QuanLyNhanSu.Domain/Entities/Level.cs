using QuanLyNhanSu.Domain.Abstractions.Entities;

namespace QuanLyNhanSu.Domain.Entities;

public class Level : DomainEntity<Guid>, IAuditEntity
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

    public virtual ICollection<EmployeeLevel> EmployeeLevels { get; set; }

    public Level(Guid id, string name, string description)
    {
        Id = id;
        Name = name;
        Description = description;
        CreatedDate = DateTime.Now;
    }

    public static Level CreateLevel(Guid id, string name, string description)
    {
        return new Level(id, name, description);
    }

    public void UpdateLevel(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public void DeleteLevel()
    {
        IsDeleted = true;
        DeletedDate = DateTime.Now;
    }
}
