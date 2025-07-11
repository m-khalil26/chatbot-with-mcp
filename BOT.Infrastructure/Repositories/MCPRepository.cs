using BOT.Domain.Entities;
using BOT.Domain.Interfaces;
using BOT.Domain.ValueObjects;
using ModelContextProtocol.Client;

namespace BOT.Infrastructure.Repositories;

public class MCPRepository(IMcpClient mcpClient) : IMCPRepository
{
    public async Task<IEnumerable<McpTool>> GetAvailableToolsAsync()
    {
        var tools = await mcpClient.ListToolsAsync();
        
        return tools.Select(t => new McpTool(
            t.Name,
            t.Description ?? "",
            new Dictionary<string, object>() 
        ));
    }

    public async Task<ToolCallResult> CallToolAsync(string toolName, Dictionary<string, object> arguments)
    {
        try
        {
            var result = await mcpClient.CallToolAsync(toolName, arguments);
            
            return new ToolCallResult(toolName, arguments, result, true);
        }
        catch (Exception ex)
        {
            return new ToolCallResult(toolName, arguments, null!, false, ex.Message);
        }
    }

    public Task<bool> IsConnectedAsync()
    {
        // La connexion est gérée par McpClientService
        return Task.FromResult(mcpClient != null);
    }


}