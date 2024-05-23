using Marten;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Handlers.Account;

public class UpsertAccountsHandler: IRequestHandler<UpsertAccountsRequest,UpsertAccountsResponse>
{
    private readonly IDocumentStore _store;
    private readonly ILogger<UpsertAccountsHandler> _logger;

    public UpsertAccountsHandler(IDocumentStore store,  ILogger<UpsertAccountsHandler> logger)
    {
        _store = store;
        _logger = logger;
    }
    
    public async Task<UpsertAccountsResponse> Handle(UpsertAccountsRequest request, CancellationToken cancellationToken)
    {
        var response = new UpsertAccountsResponse();
        using (var session = _store.LightweightSession())
        {
            try
            {
                session.StoreObjects(request.Payload);
                await session.SaveChangesAsync(cancellationToken);
                response.ResponseCode = 0;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Unexpected error while upserting accounts. Check logs");
                response.ResponseCode = 1;
            }
        }

        return response;
    }
}