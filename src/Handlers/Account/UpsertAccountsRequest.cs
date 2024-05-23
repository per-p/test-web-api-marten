using MediatR;

namespace Handlers.Account;

public class UpsertAccountsRequest:IRequest<UpsertAccountsResponse>
{
    public Data.Model.Account.Account[] Payload { get; }

    public UpsertAccountsRequest(Data.Model.Account.Account[] payload)
    {
        Payload = payload;
    }
}