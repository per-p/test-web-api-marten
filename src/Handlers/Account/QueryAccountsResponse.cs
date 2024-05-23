using Data.Model;

namespace Handlers.Account;

public class QueryAccountsResponse
{
    public Data.Model.Account.Account[] Accounts { get; set; } = Array.Empty<Data.Model.Account.Account>();
    public PageInfo? PageInfo { get; set; }
    public ErrorCode? Error { get; set; }
}