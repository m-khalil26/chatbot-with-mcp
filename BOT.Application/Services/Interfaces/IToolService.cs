using BOT.Application.Dtos;

namespace BOT.Application.Services.Interfaces;


    public interface IToolService
    {
        Task<ToolListResponseDto> GetAvailableToolsAsync();
        Task<ToolCallResponseDto> CallToolAsync(ToolCallRequestDto request);
    }
