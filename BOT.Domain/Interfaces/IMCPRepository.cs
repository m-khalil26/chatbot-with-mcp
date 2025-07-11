using BOT.Domain;
using BOT.Domain.Entities;
using BOT.Domain.ValueObjects;

namespace BOT.Domain.Interfaces;

public interface IMCPRepository
{
    Task<IEnumerable<McpTool>> GetAvailableToolsAsync();
    Task<ToolCallResult> CallToolAsync(string toolName, Dictionary<string, object> arguments);
  
}