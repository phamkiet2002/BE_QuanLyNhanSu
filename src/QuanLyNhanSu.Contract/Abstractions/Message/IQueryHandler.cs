using MediatR;
using QuanLyNhanSu.Contract.Abstractions.Shared;

namespace QuanLyNhanSu.Contract.Abstractions.Message;
public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{

}
