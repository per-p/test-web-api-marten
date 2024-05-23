using Data.Model;
using MediatR;

namespace Handlers.Account;

public class QueryAccountsRequest : IRequest<QueryAccountsResponse>
{
    public QueryAccountParams? Params { get; set; }

}