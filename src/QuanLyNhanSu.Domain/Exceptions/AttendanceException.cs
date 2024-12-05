namespace QuanLyNhanSu.Domain.Exceptions;
public static class AttendanceException
{
    public class AttendanceNotFoundException : Exception
    {
        public AttendanceNotFoundException(Guid id) : base($"Attendance with id {id} is not found")
        {
        }
    }
}
