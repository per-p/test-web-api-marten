using System.Collections;
using System.Linq.Expressions;
using Data.Model;
using JasperFx.Core;
using Marten;
using Marten.Pagination;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Handlers.Account;

public class QueryAccountsHandler : IRequestHandler<QueryAccountsRequest, QueryAccountsResponse>
{
    private readonly IDocumentStore _store;
    private readonly ILogger<QueryAccountsHandler> _logger;

    public QueryAccountsHandler(IDocumentStore store, ILogger<QueryAccountsHandler> logger)
    {
        _store = store;
        _logger = logger;
    }

    public async Task<QueryAccountsResponse> Handle(QueryAccountsRequest request, CancellationToken cancellationToken)
    {
        var response = new QueryAccountsResponse();
        if (request.Params is null)
        {
            response.Error = new ErrorCode {Code = 99, Description = "Bad request, no parameters"};
            return response;
        }

        if (!(!string.IsNullOrWhiteSpace(request.Params.Code) ^
            !string.IsNullOrWhiteSpace(request.Params.Country) ^
            !string.IsNullOrWhiteSpace(request.Params.ExactName) ^
            !string.IsNullOrWhiteSpace(request.Params.NameEndsWith) ^
            !string.IsNullOrWhiteSpace(request.Params.NameStartsWith) ^
            !string.IsNullOrWhiteSpace(request.Params.NameContains) ^
            !string.IsNullOrWhiteSpace(request.Params.OrgNo)))
            response.Error = new ErrorCode
            {
                Code = 100,
                Description = "One and only one query parameter must be set"
            };

        if (request.Params is {PageNumber: < 1})
        {
            response.Error = new ErrorCode
            {
                Code = 101,
                Description = "Page number must be greater than 0"
            };
        }

        if (request.Params is {PageSize: < 1})
        {
            response.Error = new ErrorCode
            {
                Code = 102,
                Description = "Page size must be greater than 0"
            };
        }

        if (response.Error is not null)
            return response;

        var p = request.Params!;
        
        Expression<Func<Data.Model.Account.Account, bool>> predicate = a => false;
        Expression<Func<Data.Model.Account.Account, string>> sortKey = a => a.Name;
        
        if (p.Code is {Length: > 0} code)
        {
            predicate = a => a.Code.Equals(code);
            sortKey = a => a.Code;
        }

        if (p.OrgNo is {Length: > 0} orgNo)
        {
            predicate = a => a.organization_id.Equals(orgNo);
            sortKey = a => a.organization_id;
        }

        if (p.Country is {Length: > 0} country)
            predicate = a => a.country.Equals(country);

        if (p.ExactName is {Length: > 0} name)
        {
            predicate = a => a.Name.Equals(name);
            sortKey = a => a.Name;
        }

        if (p.NameStartsWith is {Length: > 0} nameStart)
        {
            predicate = a => a.Name.StartsWith(nameStart.ToUpper());
            sortKey = a => a.Name;
        }

        if (p.NameEndsWith is {Length: > 0} nameEnd)
        {
            predicate = a => a.Name.EndsWith(nameEnd.ToUpper());
            sortKey = a => a.Name;
        }

        if (p.NameContains is {Length: > 0} nameContains)
        {
            predicate = a => a.Name.Contains(nameContains.ToUpper());
            sortKey = a => a.Name;
        }
        using (var s = _store.QuerySession())
        {
            
                
        
            var query = s.Query<Data.Model.Account.Account>().Where(predicate!);
            query = p.SortDescending ?? false
                ? query.OrderByDescending(sortKey)
                : query.OrderBy(sortKey);

            var pagedList = await query.ToPagedListAsync(
                p.PageNumber ?? 1,
                p.PageSize ?? 100,
                cancellationToken);

            return new QueryAccountsResponse
            {
                Accounts = pagedList.ToArray(),
                PageInfo = new PageInfo
                    {PageNumber = pagedList.PageNumber, PageSize = pagedList.PageSize, Count = pagedList.Count}
            };
        }
    }
}