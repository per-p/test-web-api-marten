using System.ComponentModel.DataAnnotations;

namespace Data.Model.Account;

public class Account
{
  // mandatory
    [Required]
    public string Code { get; set; } = string.Empty;
    
    [Required]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    public string organization_id { get; set; } = string.Empty;
    
    [Required]
    public string country { get; set; } = string.Empty;
    
    [Required]
    public bool active_indicator { get; set; }
    
    
    // optional
    public string blocked_indicator { get; set; } = string.Empty;
    
    public string? paccount_former_name { get; set; }
    
    public string? blocked_reason { get; set; }
    
    public string? chain { get; set; }
    
    public string? key_account_manager { get; set; }
    
    public string? legal_status { get; set; }
    
    public string? main_email { get; set; }
    
    public string? main_phone_number { get; set; }
    
    public string? parent_account { get; set; }
    
    public string? primary_subsidiary { get; set; }
    
    public string? vat_number { get; set; }

    public List<string> secondary_subsidiaries { get; set; } = new();

    public List<SourceSystemId> source_system_ids { get; set; } = new();

    public List<Address> addresses { get; set; } = new();
}