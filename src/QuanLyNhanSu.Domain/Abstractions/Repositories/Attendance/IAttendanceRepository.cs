using QuanLyNhanSu.Domain.Entities;

namespace QuanLyNhanSu.Domain.Abstractions.Repositories.Attendance;
public interface IAttendanceRepository : IRepositoryBase<Domain.Entities.Attendance, Guid>
{
    //public double CalculateWorkingHours(DateTime? checkIn, DateTime? checkOut, WorkShedule workShedule);
    public double CalculateWorkingHours(Domain.Entities.Attendance attendance, WorkShedule workShedule);
}
