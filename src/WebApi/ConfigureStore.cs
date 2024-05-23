using Data.Model.Account;
using Marten;
using Marten.Schema;

namespace WebApi;

public static class Extensions
{
    public static void ConfigureStore(this StoreOptions options, IConfigurationManager c)
    {
        options.Schema.For<Account>()
            .Identity(f => f.Code.ToUpper())
            .Index(x => x.Code, x =>
            {
                x.Name = "idx_account_code";
                x.IsUnique = true;
                x.Casing = ComputedIndex.Casings.Upper;
            })
            .Index(x => x.organization_id, x =>
            {
                x.Name = "idx_account_organization_id";
                x.Casing = ComputedIndex.Casings.Upper;
            })
            .Index(x => x.Name, x =>
            {
                x.Name = "idx_account_name";
                x.Casing = ComputedIndex.Casings.Upper;
            })
            .Index(x => x.country, x =>
            {
                x.Name = "idx_account_country";
                x.Casing = ComputedIndex.Casings.Upper;
            });

        var connecetionString = string.Empty;

        //  = "Host=myserver;Username=mylogin;Password=mypass;Database=mydatabase";
        if (c.GetValue<string>("PG_HOST") is {Length: > 0} host)
            connecetionString += $"Host={host};";
        if (c.GetValue<int>("PG_PORT") is var port and > 1024)
            connecetionString += $"Port={port.ToString()};";
        if (c.GetValue<string>("PG_USER") is {Length: > 0} user)
            connecetionString += $"Username={user};";
        if (c.GetValue<string>("PG_PASSWORD") is {Length: > 0} password)
            connecetionString += $"Password={password};";
        if (c.GetValue<string>("PG_DATABASE") is {Length: > 0} database)
            connecetionString += $"Database={database};";
        options.Connection(connectionString: connecetionString);
    }
}