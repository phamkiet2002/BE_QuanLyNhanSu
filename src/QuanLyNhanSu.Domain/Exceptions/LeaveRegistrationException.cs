using QuanLyNhanSu.Domain.Exceptions.Abstractions;

namespace QuanLyNhanSu.Domain.Exceptions;
public static class LeaveRegistrationException
{
    public class LeaveRegistrationNotFound : NotFoundException
    {
        public LeaveRegistrationNotFound(Guid id)
            : base($"The Leave Registration with the id {id} not found.") { }
    }
}
