using QuanLyNhanSu.Domain.Abstractions.Entities;
using static QuanLyNhanSu.Domain.Enumerations.ContractEnums;

namespace QuanLyNhanSu.Domain.Entities;

public class Contract : DomainEntity<Guid>, IAuditEntity
{
    #region =====attribute=====
    public int ContracNumber { get; set; }
    public DateTime SignDate { get; set; }
    public DateTime EffectiveDate { get; set; }
    public DateTime ExpirationDate { get; set; }
    public ContractStatus? Status { get; set; }
    public Guid? EmployeeId { get; set; }
    public virtual Employee Employee { get; set; }
    #endregion

    #region =====audit=====
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
    #endregion

    public Contract(Guid id, int contracNumber, DateTime signDate, DateTime effectiveDate, DateTime expirationDate, Guid? employeeId)
    {
        Id = id;
        ContracNumber = contracNumber;
        SignDate = signDate;
        EffectiveDate = effectiveDate;
        ExpirationDate = expirationDate;
        EmployeeId = employeeId;
        CreatedDate = DateTime.Now;
        Status = Enumerations.ContractEnums.ContractStatus.Active;
    }

    public static Contract CreateContract(Guid id, int contracNumber, DateTime signDate, DateTime effectiveDate, DateTime expirationDate, Guid? employeeId)
    {
        return new Contract(id, contracNumber, signDate, effectiveDate, expirationDate, employeeId);
    }

    public void UpdateContract(int contracNumber, DateTime signDate, DateTime effectiveDate, DateTime expirationDate, Guid? employeeId)
    {
        ContracNumber = contracNumber;
        SignDate = signDate;
        EffectiveDate = effectiveDate;
        ExpirationDate = expirationDate;
        EmployeeId = employeeId;
    }

    public void CheckContractNearExpiration()
    {
        Status = Enumerations.ContractEnums.ContractStatus.NearExpiration;
    }
}
