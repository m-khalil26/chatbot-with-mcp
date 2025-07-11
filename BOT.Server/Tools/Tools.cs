using BOT.Domain.Interfaces;

namespace BOT.Server.Tools;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using ModelContextProtocol.Server;


[McpServerToolType]
public class Tools(ISampleRepository sampleRepository)
{
    private static readonly string LogFilePath = Path.Combine("/tmp", "mcp_server_tools.log");
    /// <summary>
    /// Logs exceptions to a local file to avoid writing to stdout.
    /// </summary>
    private static void LogError(Exception ex, string methodName)
    {
        try
        {
            var logMessage = $"{DateTime.UtcNow:O} - ERROR in {methodName}:{Environment.NewLine}" +
                             $"Message: {ex.Message}{Environment.NewLine}" +
                             $"StackTrace: {ex.StackTrace}{Environment.NewLine}" +
                             "--------------------------------------------------"+ Environment.NewLine;
            File.AppendAllText(LogFilePath, logMessage);
        }
        catch
        {
            // Ignorer pour ne pas planter le serveur
        }
    }
    private static void LogMessage(string message)
    {
        try
        {
            var logMessage = $"{DateTime.UtcNow:O} - INFO: {message}{Environment.NewLine}";
            File.AppendAllText(LogFilePath, logMessage);
        }
        catch
        {
            // Ignorer pour ne pas planter le serveur
        }
    }
    

    [McpServerTool, Description("Here write the definition of your tool, this description will be used by the MCP SDK to know which tool to call when scanning")]
    public  async Task<SampleDto?> Tool(string name, string jwt)
    {
        LogMessage($"Start of treatment with name ={name}");
    
        try
        {
            // THE JWT IS PASSED TO EACH REPOSITORY OPERATION
            // THE JWT IS INJECTED BY THE MCP SDK TO THE TOOL AS ASKED IN INITIAL CONFIG PROMPT
            var sampleResult = await sampleRepository.SampleOperation(jwt);
            
            LogMessage($"Operation done"); 
            
            return sampleResult;
        }
        catch (Exception e)
        {
            LogMessage($"An error occured while calling data sources : {e.Message}\nStackTrace: {e.StackTrace}");
            return null;
        }
    }
    

}