using MediatR;

namespace Handlers.Account;

public class GetAccountByCodeRequest:  IRequest<Data.Model.Account.Account?>
{
    public string Code { get; set; } = string.Empty;
}