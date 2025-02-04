namespace QuanLyNhanSu.Domain.Abstractions.Entities;
public interface IAuditEntity
{
    public DateTime? CreatedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
}
