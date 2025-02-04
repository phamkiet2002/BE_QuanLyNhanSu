using QuanLyNhanSu.Domain.Abstractions.Entities;
using static QuanLyNhanSu.Domain.Enumerations.AllowanceAndPenaltyEnums;

namespace QuanLyNhanSu.Domain.Entities;

public class AllowanceAndPenalties : DomainEntity<Guid>, IAuditEntity
{
    #region =====attribute=====
    public TypeAllowanceAndPenalties? Type { get; set; }
    public TypeOfAllowance? TypeOfAllowance { get; set; }
    public TypeOfPenalty? TypeOfPenalty { get; set; }
    public decimal Money { get; set; }
    public DateTime EffectiveDate { get; set; }
    public string Note { get; set; }
    public bool? IsAllWorkPlace { get; set; }
    #endregion

    #region =====audit=====
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public bool? IsDeleted { get; set; }
    public DateTime? DeletedDate { get; set; }
    #endregion

    public virtual ICollection<WorkPlaceAllowanceAndPenalties> WorkPlaceAndAllowanceAndPenalties { get; set; }

    public AllowanceAndPenalties() { }

    public AllowanceAndPenalties(Guid id, TypeOfAllowance? typeOfAllowance, decimal money, DateTime effectiveDate, string note, bool? isAllWorkPlace)
    {
        Id = id;
        Type = TypeAllowanceAndPenalties.Phucap;
        TypeOfAllowance = typeOfAllowance;
        Money = money;
        EffectiveDate = effectiveDate;
        Note = note;
        IsAllWorkPlace = isAllWorkPlace;
        CreatedDate = DateTime.Now;
    }

    public static AllowanceAndPenalties CreateAllowance(Guid id, TypeOfAllowance? typeOfAllowance, decimal money, DateTime effectiveDate, string note, bool? isAllWorkPlace)
    {
        return new AllowanceAndPenalties(id, typeOfAllowance, money, effectiveDate, note, isAllWorkPlace);
    }

    public AllowanceAndPenalties(Guid id, TypeOfPenalty? typeOfPenalty, decimal money, DateTime effectiveDate, string note, bool? isAllWorkPlace)
    {
        Id = id;
        Type = TypeAllowanceAndPenalties.Phat;
        TypeOfPenalty = typeOfPenalty;
        Money = money;
        EffectiveDate = effectiveDate;
        Note = note;
        IsAllWorkPlace = isAllWorkPlace;
        CreatedDate = DateTime.Now;
    }

    public static AllowanceAndPenalties CreatePenalty(Guid id, TypeOfPenalty? typeOfPenalty, decimal money, DateTime effectiveDate, string note, bool? isAllWorkPlace)
    {
        return new AllowanceAndPenalties(id, typeOfPenalty, money, effectiveDate, note, isAllWorkPlace);
    }

    public void UpdateAllowanceAndPenalties(TypeOfAllowance? typeOfAllowance, TypeOfPenalty? typeOfPenalty, decimal money, DateTime effectiveDate, string note, bool? isAllWorkPlace)
    {
        TypeOfAllowance = typeOfAllowance;
        TypeOfPenalty = typeOfPenalty;
        Money = money;
        EffectiveDate = effectiveDate;
        Note = note;
        IsAllWorkPlace = isAllWorkPlace;
    }

    public void DeleteAllowanceAndPenalties()
    {
        IsDeleted = true;
        DeletedDate = DateTime.Now;
    }
}
