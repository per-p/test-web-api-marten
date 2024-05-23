namespace Data.Model;

public class PageInfo
{
    public long PageNumber { get; set; } = 1;
    public long PageSize { get; set; } = 10;
    public long? Count { get; set; }
}