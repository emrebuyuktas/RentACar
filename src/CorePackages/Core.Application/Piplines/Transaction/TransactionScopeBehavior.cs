using MediatR;
using System.Transactions;

namespace Core.Application.Piplines.Transaction;

public class TransactionScopeBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ITransactiononalRequest
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        using TransactionScope scope = new(TransactionScopeAsyncFlowOption.Enabled);

        TResponse response;

        try
        {
            response = await next();
            scope.Complete();
        }
        catch (Exception)
        {
            scope.Dispose();
            throw;
        }

        return response;
    }
}