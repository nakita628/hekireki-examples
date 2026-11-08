#nullable enable

using System;

namespace Plunk.Models;

public partial class ApiRequest
{
    public string Id { get; set; } = null!;

    public string Method { get; set; } = null!;

    public string Path { get; set; } = null!;

    public int StatusCode { get; set; }

    public int Duration { get; set; }

    public string? ProjectId { get; set; }

    public virtual Project? Project { get; set; }

    public string? UserId { get; set; }

    public string? AuthType { get; set; }

    public string? Ip { get; set; }

    public string? UserAgent { get; set; }

    public string? ErrorCode { get; set; }

    public string? ErrorMessage { get; set; }

    public int? RequestSize { get; set; }

    public int? ResponseSize { get; set; }

    public DateTime CreatedAt { get; set; }
}
