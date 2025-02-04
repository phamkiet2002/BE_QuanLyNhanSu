using System.Reflection.Metadata.Ecma335;
using QuanLyNhanSu.Domain.Abstractions.Entities;
using QuanLyNhanSu.Domain.Entities.Identity;
using static QuanLyNhanSu.Domain.Enumerations.EmployeeEnums;
using static QuanLyNhanSu.Domain.Enumerations.StatusEnums;

namespace QuanLyNhanSu.Domain.Entities;

public class Employee : DomainEntity<Guid>, IAuditEntity
{
    #region =====attribute=====
    public string? MaNV { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string IdentityCard { get; set; }
    public Gender Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Address { get; set; }
    public DateTime JoinDate { get; set; }
    public string BankName { get; set; }
    public string BankAccountNumber { get; set; }
    public Status? Status { get; set; }
    #endregion

    #region =====audit=====
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
    #endregion
    public Guid? WorkPlaceId { get; set; }
    public virtual WorkPlace WorkPlace { get; set; }

    public virtual ICollection<EmployeeDepartment> EmployeeDepartments { get; set; }

    public virtual ICollection<EmployeePosition> EmployeePositions { get; set; }

    public virtual ICollection<Salary> Salarys { get; set; }

    public virtual ICollection<EmployeeLevel> EmployeeLevels { get; set; }

    public virtual ICollection<Contract> Contracts { get; set; }

    public virtual ICollection<LeaveRegistration> LeaveRegistrations { get; set; }
    public virtual ICollection<LeaveRegistration> LeaveRegistrationApproves { get; set; }
    public virtual ICollection<Attendance> Attendances { get; set; }
    public virtual ICollection<Attendance> AttendanceApproves { get; set; }
    public virtual ICollection<PayRoll> PayRolls { get; set; }
    public virtual AppUser AppUser { get; set; }

    public Employee
    (
        Guid id,
        string name, string email, string phone, string identityCard, Gender gender, DateTime dateOfBirth, string address,
        DateTime joinDate, string bankName, string bankAccountNumber, Guid? workPlaceId
    )
    {
        Id = id;
        MaNV = EmployeeCodeGenerator.GenerateCode();
        Name = name;
        Email = email;
        Phone = phone;
        IdentityCard = identityCard;
        Gender = gender;
        DateOfBirth = dateOfBirth;
        Address = address;
        JoinDate = joinDate;
        BankName = bankName;
        BankAccountNumber = bankAccountNumber;
        WorkPlaceId = workPlaceId;
        CreatedDate = DateTime.Now;
        Status = Enumerations.StatusEnums.Status.Active;
    }

    public static Employee CreateEmployee
    (
        Guid id,
        string name, string email, string phone, string identityCard, Gender gender, DateTime dateOfBirth, string address,
        DateTime joinDate, string bankName, string bankAccountNumber, Guid? workPlaceId
    )
    {
        return new Employee(id, name, email, phone, identityCard, gender, dateOfBirth, address, joinDate, bankName, bankAccountNumber, workPlaceId);
    }

    public void UpdateEmployee
    (
        string name, string email, string phone, string identityCard, Gender gender, DateTime dateOfBirth, string address, string bankName, string bankAccountNumber
    )
    {
        Name = name;
        Email = email;
        Phone = phone;
        IdentityCard = identityCard;
        Gender = gender;
        DateOfBirth = dateOfBirth;
        Address = address;
        BankName = bankName;
        BankAccountNumber = bankAccountNumber;
    }

    public void UpdateEmployeeWorkPlace(Guid? workPlaceId)
    {
        WorkPlaceId = workPlaceId;
    }

    public void LeaveWorkEmployee()
    {
        Status = Enumerations.StatusEnums.Status.InActive;
        DeletedDate = DateTime.Now;
    }
}

public static class EmployeeCodeGenerator
{
    private static readonly object LockObj = new object();
    public static int Counter = 1;
    public static int CurrentYearSuffix = DateTime.Now.Year % 100;

    public static string GenerateCode()
    {
        lock (LockObj)
        {
            var yearSuffix = DateTime.Now.Year % 100;
            if (CurrentYearSuffix != yearSuffix)
            {
                CurrentYearSuffix = yearSuffix;
                Counter = 1;
            }
            var code = $"NV{CurrentYearSuffix}{Counter:000}";
            Counter++;
            return code;
        }
    }
}
