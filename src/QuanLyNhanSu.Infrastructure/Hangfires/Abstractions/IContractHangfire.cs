namespace QuanLyNhanSu.Infrastructure.Hangfires.Abstractions;
public interface IContractHangfire
{
    public Task CheckAndUpdateContractStatusAsync();
}
