namespace QuanLyNhanSu.Domain.Exceptions;
public static class AttendanceSettingException
{
    public class AttendanceSettingNotFoundException : Exception
    {
        public AttendanceSettingNotFoundException(Guid id) : base($"Attendance setting with id {id} is not found")
        {
        }
    }
}
