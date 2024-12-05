using QuanLyNhanSu.Domain.Exceptions.Abstractions;

namespace QuanLyNhanSu.Domain.Exceptions;
public static class EmployeeException
{
    public class EmployeeNotFoundException : NotFoundException
    {
        public EmployeeNotFoundException(string MaNv)
            : base($"The Employee with the id {MaNv} not found.") { }

        public EmployeeNotFoundException(Guid id)
            : base($"The Employee with the id {id} not found.") { }
    }
}
