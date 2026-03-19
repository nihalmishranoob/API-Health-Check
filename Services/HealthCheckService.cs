using System.Diagnostics;
using System.Text.Json;
using ApiHealthDashboard.Models;

namespace ApiHealthDashboard.Services;

public class HealthCheckService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<HealthCheckService> _logger;

    public HealthCheckService(IHttpClientFactory httpClientFactory, ILogger<HealthCheckService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public async Task<ApiHealthResult> CheckAsync(HealthCheckRequest request)
    {
        var result = new ApiHealthResult
        {
            Url = request.Url,
            Label = string.IsNullOrWhiteSpace(request.Label) ? request.Url : request.Label,
            CheckedAt = DateTime.UtcNow
        };

        try
        {
            var client = _httpClientFactory.CreateClient("HealthCheck");
            var stopwatch = Stopwatch.StartNew();

            HttpResponseMessage response;
            try
            {
                response = await client.GetAsync(request.Url);
            }
            finally
            {
                stopwatch.Stop();
            }

            result.StatusCode = (int)response.StatusCode;
            result.ResponseTimeMs = stopwatch.ElapsedMilliseconds;

            // JSON Validation — only if response is JSON content type
            var contentType = response.Content.Headers.ContentType?.MediaType ?? "";
            if (contentType.Contains("json", StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    var body = await response.Content.ReadAsStringAsync();
                    JsonDocument.Parse(body);
                    result.IsValidJson = true;
                }
                catch
                {
                    result.IsValidJson = false;
                }
            }
            else
            {
                result.IsValidJson = null;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Health check failed for {Url}", request.Url);
            result.ErrorMessage = ex.Message;
            result.StatusCode = null;
            result.ResponseTimeMs = -1;
        }

        return result;
    }
}
