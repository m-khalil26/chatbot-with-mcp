using BOT.Domain.Entities;

namespace BOT.Domain.Interfaces;

public interface IChatRepository
{
    Task<IAsyncEnumerable<string>> GetStreamingResponseAsync(
        IEnumerable<ChatMessage> messages, 
        IEnumerable<McpTool> availableTools);
    Task<string> GetResponseAsync(
        IEnumerable<ChatMessage> messages, 
        IEnumerable<McpTool> availableTools);
}