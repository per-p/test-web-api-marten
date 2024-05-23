namespace Data.Model.Account;

public class Address
{
    public string? Code { get; set; }
    public bool? active_indicator { get; set; }
    public string? addressee { get; set; }
    public string? attention { get; set; }
    public string? country { get; set; }
    public string? postal_code { get; set; }
    public string? postal_district { get; set; }
    public string? relation_type { get; set; }
    public string? street { get; set; }
    public string? subregion { get; set; }
    public string? netsuite_id { get; set; }
    public bool? billto_default_indicator { get; set; }
    public bool? shipto_default_indicator { get; set; }
}