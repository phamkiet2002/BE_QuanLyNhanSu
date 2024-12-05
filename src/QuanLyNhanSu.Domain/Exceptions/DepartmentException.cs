using QuanLyNhanSu.Domain.Exceptions.Abstractions;

namespace QuanLyNhanSu.Domain.Exceptions;
public static class DepartmentException
{
    public class DepartmentNotFoundException : NotFoundException
    {
        public DepartmentNotFoundException(Guid departmentId)
            : base($"The Department with the id {departmentId} not found.") { }
    }

}
