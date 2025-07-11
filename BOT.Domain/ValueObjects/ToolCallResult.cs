namespace BOT.Domain.ValueObjects;

public record ToolCallResult(
    string ToolName,
    Dictionary<string, object> Arguments,
    object Result,
    bool IsSuccess,
    string? ErrorMessage = null
);