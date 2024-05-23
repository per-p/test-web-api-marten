using Data.Model.Account;
using Handlers.Account;
using Marten;
using MediatR;
using Weasel.Core;
using WebApi;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables(prefix: "Marten_");
builder.Services.AddMarten(options =>
{
    options.ConfigureStore(builder.Configuration);
    if (builder.Environment.IsDevelopment())
    {
        options.AutoCreateSchemaObjects = AutoCreate.All;
    }
});

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<UpsertAccountsHandler>());


var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapPost("/account",
    async (IMediator mediator, Account[] accounts, CancellationToken cancellationToken) =>
        await mediator.Send(new UpsertAccountsRequest(accounts), cancellationToken));

app.MapGet("/account/{code}", async (IMediator mediator, string code, CancellationToken cancellationToken) => 
    await mediator.Send(new GetAccountByCodeRequest {Code = code})
);

app.MapGet("/account", async (IMediator mediator, [AsParameters] QueryAccountParams @params,
        CancellationToken cancellationToken) =>
    await mediator.Send(new QueryAccountsRequest { Params =  @params}, cancellationToken)
);

app.MapGet("/account/addresses", async (IMediator mediator, string code, CancellationToken cancellationToken) =>
    await mediator.Send(new GetAccountAddressesByAccountCodeRequest {Code = code}, cancellationToken));

app.MapGet("/account/{code}/addresses", async (IMediator mediator, string code, CancellationToken cancellationToken) =>
    await mediator.Send(new GetAccountAddressesByAccountCodeRequest {Code = code}, cancellationToken)
);

app.Run();