using BOT.Application.Dtos;

namespace BOT.Application.Services.Interfaces;
public interface IChatService
{
    Task<ChatResponseDto> ProcessChatAsync(ChatRequestDto request,string jwt);
    Task<IAsyncEnumerable<string>> ProcessStreamingChatAsync(ChatRequestDto request,string jwt);
}