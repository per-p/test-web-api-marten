namespace Data.Model.Account;

public class Subsidiary
{
    public string Value { get; set; } = string.Empty;
    public static implicit operator string(Subsidiary tag) => tag.Value;
    public static implicit operator Subsidiary (string tag) => new() { Value = tag };
}