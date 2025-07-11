
using BOT.Application.Services;
using BOT.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BOT.Application;
public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IChatService,ChatService>();
        services.AddScoped<IToolService,ToolService>();
        
    }
}