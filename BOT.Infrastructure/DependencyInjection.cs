using BOT.Domain.Interfaces;
using BOT.Infrastructure.McpClient;
using BOT.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModelContextProtocol.Client;

namespace BOT.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMcpClient(configuration);
        
        services.AddScoped<ISampleRepository, SampleRepository>();
        
        services.AddScoped<IChatRepository, ChatRepository>();
        services.AddScoped<IMCPRepository, MCPRepository>();
        
        services.AddHttpClient<SampleRepository>(client =>
        {
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        });
    }

    private static void AddMcpClient(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<McpClient.McpClientOptions>(
            configuration.GetSection(McpClient.McpClientOptions.SectionName));

        services.AddSingleton<McpClientService>();

        services.AddSingleton<IMcpClient>(provider =>
        {
            var clientService = provider.GetRequiredService<McpClientService>();
            return clientService.GetClientAsync().GetAwaiter().GetResult();
        });
    }
}