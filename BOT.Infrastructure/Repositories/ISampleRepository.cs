using System.Net.Http.Json;
using BOT.Domain.Infrastructure;
using BOT.Domain.Interfaces;
using Microsoft.Extensions.Options;

namespace BOT.Infrastructure.Repositories;

public class SampleRepository(HttpClient httpClient, IOptions<ApiSettings> options):ISampleRepository
{
    private static readonly string LogFilePath = Path.Combine("/tmp", "association-repository.log");    
    private void LogToFile(string message)
    {
        try
        {
            var logMessage = $"{DateTime.UtcNow:O} - {message}{Environment.NewLine}";
            File.AppendAllText(LogFilePath, logMessage);
            
        }
        catch
        {
            // Ignorer les erreurs de journalisation
        }
    }
    
    
    public async Task<SampleDto?> SampleOperation(string jwt)
    {

        var sampleUrl = $"{options.Value.SampleUrl}/endpoint";
        httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",jwt);
        var response = await httpClient.GetAsync(sampleUrl);
        try
        {
            if (!response.IsSuccessStatusCode)
            {
                LogToFile("Error while getting all events from Association repository");
                LogToFile($"Response status :"+response.StatusCode);
                return null;
            }
            LogToFile("Association trouvée");
            var value =await response.Content.ReadFromJsonAsync<SampleDto?>();
            return value;
        }
        catch (Exception ex)
        {
            LogToFile("Error while getting all events from PlanningRepository");
            LogToFile($"Error:{ex.StackTrace}");
            return null;
        }
        
    }
}