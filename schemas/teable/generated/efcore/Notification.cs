#nullable enable

using System;

namespace Teable.Models;

public partial class Notification
{
    public string Id { get; set; } = null!;

    public string FromUserId { get; set; } = null!;

    public string ToUserId { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string Message { get; set; } = null!;

    public string? MessageI18n { get; set; }

    public string Severity { get; set; } = "info";

    public string? UrlPath { get; set; }

    public bool IsRead { get; set; } = false;

    public DateTime CreatedTime { get; set; }

    public string CreatedBy { get; set; } = null!;
}
