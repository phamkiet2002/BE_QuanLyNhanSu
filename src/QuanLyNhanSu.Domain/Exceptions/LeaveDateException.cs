using QuanLyNhanSu.Domain.Exceptions.Abstractions;

namespace QuanLyNhanSu.Domain.Exceptions;
public static class LeaveDateException
{
    public class LeaveDateNotFoundException : NotFoundException
    {
        public LeaveDateNotFoundException(Guid Id)
            : base($"The LeaveDate with the id {Id} not found.") { }
    }
}
