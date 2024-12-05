using MediatR;
using QuanLyNhanSu.Contract.Abstractions.Shared;

namespace QuanLyNhanSu.Contract.Abstractions.Message;
public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
