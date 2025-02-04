using QuanLyNhanSu.Domain.Abstractions.Entities;
using QuanLyNhanSu.Domain.Enumerations;
using static QuanLyNhanSu.Domain.Enumerations.PayRollEnums;

namespace QuanLyNhanSu.Domain.Entities;
public class PayRoll : DomainEntity<Guid>, IAuditEntity
{
    #region =====attribute=====
    public Guid? EmployeeId { get; set; }
    public virtual Employee Employee { get; set; }

    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }

    public double? ActualWorkingDay { get; set; }
    public decimal? TotalAllowance { get; set; }
    public decimal? TotalPenalties { get; set; }

    public decimal? SalaryGross { get; set; }
    public decimal? SalaryNet { get; set; }

    public PayRollStatus? PayRollStatus { get; set; }

    public DateTime? PaymentDate { get; set; }

    public string? ReasonNote { get; set; }
    #endregion

    #region =====audit=====
    public DateTime? CreatedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    #endregion


    public PayRoll(Guid id, Guid? employeeId)
    {
        Id = id;
        EmployeeId = employeeId;
        FromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        ToDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
        SalaryGross = 0;
        SalaryNet = 0;
        PayRollStatus = PayRollEnums.PayRollStatus.UNPAID;
        CreatedDate = DateTime.Now;
    }

    public static PayRoll CreatePayRoll(Guid id, Guid? employeeId)
    {
        return new PayRoll(id, employeeId);
    }

    public void UpdateSalary(double actualWorkingDay, decimal totalAllowance, decimal totalPenalties, decimal salaryGross, decimal salaryNet)
    {
        ActualWorkingDay = actualWorkingDay;
        TotalAllowance = totalAllowance;
        TotalPenalties = totalPenalties;
        SalaryGross = salaryGross;
        SalaryNet = salaryNet;
        //PayRollStatus = PayRollEnums.PayRollStatus.PAID;
        UpdatedDate = DateTime.Now;
    }

    public void PaidAndUnPaidPaymentDate(PayRollStatus payRollStatus, string reasonNote, DateTime? paymentDate)
    {
        PayRollStatus = payRollStatus;
        ReasonNote = reasonNote;
        PaymentDate = paymentDate;
    }
}
