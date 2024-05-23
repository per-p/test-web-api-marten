using Data.Model.Account;
using Marten;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Handlers.Account;

public class
    GetAccountAddressesByAccountCodeHandler : IRequestHandler<GetAccountAddressesByAccountCodeRequest,
    Address[]?>
{
    private readonly IDocumentStore _store;
    private readonly ILogger<GetAccountAddressesByAccountCodeHandler> _logger;

    public GetAccountAddressesByAccountCodeHandler(IDocumentStore store,
        ILogger<GetAccountAddressesByAccountCodeHandler> logger)
    {
        _store = store;
        _logger = logger;
    }

    public async Task<Address[]?> Handle(GetAccountAddressesByAccountCodeRequest request,
        CancellationToken cancellationToken)
    {
        using (var s = _store.QuerySession())
        {
            return (await s.Query<Data.Model.Account.Account>()
                    .Where(w => w.Code == request.Code)
                    .SelectMany(s => s.addresses)
                    .ToListAsync(cancellationToken))
                ?.ToArray();
        }
    }

}