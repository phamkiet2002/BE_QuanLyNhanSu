using System.Transactions;
using MediatR;
using QuanLyNhanSu.Domain.Abstractions;
//using QuanLyNhanSu.Persistence;

namespace QuanLyNhanSu.Application.Behaviors;

public sealed class TransactionPipelineBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IUnitOfWork _unitOfWork; // SQL-SERVER-STRATEGY-2
    //private readonly ApplicationDbContext _context; // SQL-SERVER-STRATEGY-1

    //, ApplicationDbContext context
    public TransactionPipelineBehavior(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        //_context = context;
    }

    public async Task<TResponse> Handle(TRequest request,
        RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!IsCommand()) // In case TRequest is QueryRequest just ignore
            return await next();

        //IMPORTANT: passing "TransactionScopeAsyncFlowOption.Enabled" to the TransactionScope constructor. This is necessary to be able to use it with async/await.
        using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            var response = await next();
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            transaction.Complete();
            await _unitOfWork.DisposeAsync();
            return response;
        }

    }

    private bool IsCommand()
        => typeof(TRequest).Name.EndsWith("Command");
}
