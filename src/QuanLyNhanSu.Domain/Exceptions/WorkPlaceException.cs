using QuanLyNhanSu.Domain.Exceptions.Abstractions;

namespace QuanLyNhanSu.Domain.Exceptions;
public static class WorkPlaceException
{
    public class WorkPlaceNotFoundException : NotFoundException
    {
        public WorkPlaceNotFoundException(Guid workPlaceId)
            : base($"The Work Place with the id {workPlaceId} not found.") { }
    }

    public class WorkPlaceBadException : BadRequestException
    {
        public WorkPlaceBadException(Guid workPlaceId)
            : base($"Nơi làm việc với Id {workPlaceId} có dữ liệu, không thể xóa.") { }
    }
}
