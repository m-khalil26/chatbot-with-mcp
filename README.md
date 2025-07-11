# Sample chatbot using MCP


This a sample template for building a chatbot application that uses an LLM to interact with you datasets and APIS via Anthropic's Model Context Protocol (MCP).



## REST API 

Two controllers are exposed, one for the tools and the other for the chat functionalities

### ChatController

```
POST /api/chat - Send a prompt and receive an answer
POST /api/chat/stream - Send a prompt and receive a streaming answer
```

### ToolsController

```
GET /api/tools - Available tools
POST /api/tools/call - Directly call a tool
```

## Configuration


```

### McpServer

The server is launched on API launch

```json
"McpClient": {
  "Command": "dotnet",
  "Arguments": ["run", "--project", "../Maraudr.MCP.Server"],
  "Name": "Business MCP Server",
  "WorkingDirectory": null
}
```
And it uses STDIO to interact with the client ( API Application)

### Your LLM configuration

```json
"YourLLM": {
  "ApiKey": "key",
  "BaseUrl": "https://your-llm.ai/api/v1",
  "ModelName": ""
}
```



### Requirements

- .NET 9.0 SDK
- Either a local LLM compatible with tool calling, or an API key for online LLMS.

## Write your own tools

To add a new tool

1. Create a class in `BOT.Server.Tools`
2. Decorate it with `[McpServerToolType]`
3. Write your tools with `[McpServerTool]` and `[Description]`
4. Write your repositories for data access


