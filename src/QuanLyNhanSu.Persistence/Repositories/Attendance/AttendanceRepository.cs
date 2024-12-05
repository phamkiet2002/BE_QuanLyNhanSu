using QuanLyNhanSu.Domain.Abstractions.Repositories.Attendance;
using QuanLyNhanSu.Domain.Entities;

namespace QuanLyNhanSu.Persistence.Repositories.Attendance;
public class AttendanceRepository : RepositoryBase<Domain.Entities.Attendance, Guid>, IAttendanceRepository
{
    public AttendanceRepository(ApplicationDbContext context) : base(context)
    {
    }

    //public double CalculateWorkingHours(DateTime? checkIn, DateTime? checkOut, WorkShedule workShedule)
    //{
    //    if (checkIn == null || checkOut <= checkIn)
    //        return 0;

    //    TimeSpan time = TimeSpan.Zero;
    //    if (checkIn != null && checkOut != null && checkOut > checkIn)
    //    {
    //        var lunchBreak = workShedule.BreakEndTime - workShedule.BreakStartTime;

    //        TimeSpan timeCheckInCheckOut = (TimeSpan)(checkOut - checkIn);
    //        if (checkOut.Value.TimeOfDay > workShedule.BreakStartTime)
    //        {
    //            time = timeCheckInCheckOut;
    //            return time.TotalHours;
    //        }

    //        time = timeCheckInCheckOut - lunchBreak;
    //        return time.TotalHours;
    //    }
    //    return time.TotalHours;
    //}

    public double CalculateWorkingHours(Domain.Entities.Attendance attendance, WorkShedule workShedule)
    {
        if (attendance.CheckIn == null || attendance.CheckOut <= attendance.CheckIn)
            return 0;

        TimeSpan time = TimeSpan.Zero;
        if (attendance.CheckIn != null && attendance.CheckOut != null && attendance.CheckOut > attendance.CheckIn)
        {
            var lunchBreak = workShedule.BreakEndTime - workShedule.BreakStartTime;
            var totalLateTime = attendance.LateTime;
            var totalEarlyLeaveTime = attendance.EarlyLeaveTime;

            TimeSpan timeCheckInCheckOut = attendance.CheckOut - attendance.CheckIn;
            if (attendance.CheckOut.TimeOfDay > workShedule.BreakStartTime)
            {
                return timeCheckInCheckOut.TotalHours;
            }
            time = timeCheckInCheckOut - lunchBreak - totalLateTime.Value.TimeOfDay - totalEarlyLeaveTime.Value.TimeOfDay;
            return time.TotalHours;
        }
        return time.TotalHours;
    }
}
