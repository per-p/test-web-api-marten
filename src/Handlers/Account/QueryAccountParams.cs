namespace Handlers.Account;

public class QueryAccountParams
{
    public string? Code { get; set; }
    public string? OrgNo { get; set; }
    public string? Country { get; set; }
    public string? ExactName { get; set; }
    public string? NameStartsWith { get; set; }
    public string? NameEndsWith { get; set; }
    public string? NameContains { get; set; }
    public int? PageSize { get; set; }
    public int? PageNumber { get; set; }
    public string? SortBy { get; set; }
    public bool? SortDescending { get; set; }
}