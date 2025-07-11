using BOT.Application;
using BOT.Domain.Infrastructure;
using BOT.Infrastructure;
using BOT.Server.Tools;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices((context, services) =>
{
    
    services.Configure<ApiSettings>(context.Configuration.GetSection("ApiSettings"));
    services.AddTransient<Tools>();
    services.AddMcpServer()
        .WithStdioServerTransport() // We use STDIO communication between server and client
        .WithToolsFromAssembly(); // Here we scan for all the tools available
    services.AddInfrastructure(context.Configuration);
    services.AddApplication(); 

});

var host = builder.Build();


await host.RunAsync();