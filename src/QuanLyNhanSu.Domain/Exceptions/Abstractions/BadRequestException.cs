namespace QuanLyNhanSu.Domain.Exceptions.Abstractions;
public abstract class BadRequestException : DomainException
{
    public BadRequestException(string message) : base("Bad Request", message)
    {
    }
}
