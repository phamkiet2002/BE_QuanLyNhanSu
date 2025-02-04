namespace QuanLyNhanSu.Domain.Exceptions.Abstractions;
public abstract class NotFoundException : DomainException
{
    public NotFoundException(string message) : base("Not Found", message)
    {
    }
}
