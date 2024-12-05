using QuanLyNhanSu.Domain.Exceptions.Abstractions;

namespace QuanLyNhanSu.Domain.Exceptions;
public static class ContractException
{
    public class ContractNotFoundException : NotFoundException
    {
        public ContractNotFoundException(Guid contractId)
            : base($"The Contract with the id {contractId} not found.") { }
    }
}
