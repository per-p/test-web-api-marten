using MediatR;

namespace Handlers.Account;

public class GetAccountAddressesByAccountCodeRequest:  IRequest<Data.Model.Account.Address[]?>
{
    public string Code { get; set; } = string.Empty;
}