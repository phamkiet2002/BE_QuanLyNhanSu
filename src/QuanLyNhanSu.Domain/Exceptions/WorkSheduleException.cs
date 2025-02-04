using QuanLyNhanSu.Domain.Exceptions.Abstractions;

namespace QuanLyNhanSu.Domain.Exceptions;
public static class WorkSheduleException
{
    public class WorkSheduleNotFoundException : NotFoundException
    {
        public WorkSheduleNotFoundException(Guid workSheduleId)
            : base($"The Work Shedule with the id {workSheduleId} not found.") { }
    }
}
