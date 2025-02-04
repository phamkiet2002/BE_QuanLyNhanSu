using QuanLyNhanSu.Domain.Exceptions.Abstractions;

namespace QuanLyNhanSu.Domain.Exceptions;
public static class AllowanceAndPenaltiesException
{
    public class AllowanceAndPenaltiesNotFoundException : NotFoundException
    {
        public AllowanceAndPenaltiesNotFoundException(Guid Id)
           : base($"The AllowanceAndPenalties with the id {Id} not found.") { }
    }

    public class AllowanceNotFoundException : NotFoundException
    {
        public AllowanceNotFoundException(Guid allowanceId)
           : base($"The Allowance with the id {allowanceId} not found.") { }
    }
    public class PenaltiesNotFoundException : NotFoundException
    {
        public PenaltiesNotFoundException(Guid penaltiesId)
           : base($"The Penalties with the id {penaltiesId} not found.") { }
    }
}
