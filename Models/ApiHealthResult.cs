namespace ApiHealthDashboard.Models;

public class ApiHealthResult
{
    public string Url { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;

    // HTTP Status
    public int? StatusCode { get; set; }
    public bool IsStatusHealthy => StatusCode.HasValue && StatusCode >= 200 && StatusCode < 300;

    // Response Time
    public long ResponseTimeMs { get; set; }
    public bool IsResponseTimeFast => ResponseTimeMs < 500;

    // JSON Validity (null = not a JSON endpoint)
    public bool? IsValidJson { get; set; }

    // Overall
    public bool IsHealthy => IsStatusHealthy && IsResponseTimeFast && (IsValidJson ?? true);

    // Error
    public string? ErrorMessage { get; set; }

    public DateTime CheckedAt { get; set; } = DateTime.UtcNow;
}

public class HealthCheckRequest
{
    public string Url { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
}

public class IndexViewModel
{
    public HealthCheckRequest Request { get; set; } = new();
    public ApiHealthResult? Result { get; set; }
    public string? ErrorMessage { get; set; }
}