using Marten;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Handlers.Account;

public class GetAccountByCodeHandler : IRequestHandler<GetAccountByCodeRequest, Data.Model.Account.Account?>
{
    private readonly IDocumentStore _store;
    private readonly ILogger<GetAccountByCodeHandler> _logger;

    public GetAccountByCodeHandler(IDocumentStore store, ILogger<GetAccountByCodeHandler> logger)
    {
        _store = store;
        _logger = logger;
    }
    
    public async Task<Data.Model.Account.Account?> Handle(GetAccountByCodeRequest request, CancellationToken cancellationToken)
    {
        using (var s = _store.QuerySession())
        {
           return await s.LoadAsync<Data.Model.Account.Account>(request.Code, cancellationToken);
        }
    }
}