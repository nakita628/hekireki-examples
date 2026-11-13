#nullable enable

using System;

namespace Umami.Models;

public partial class ErrorEvent
{
    public Guid WebsiteId { get; set; }

    public Guid Id { get; set; }

    public Guid IssueId { get; set; }

    public Guid SessionId { get; set; }

    public Guid VisitId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime ReceivedAt { get; set; }

    public string Name { get; set; } = null!;

    public string Message { get; set; } = null!;

    public string Stack { get; set; } = null!;

    public string Frames { get; set; } = null!;

    public string Tags { get; set; } = null!;

    public bool Handled { get; set; }

    public string UrlPath { get; set; } = null!;

    public string Release { get; set; } = null!;

    public string Environment { get; set; } = null!;

    public string Browser { get; set; } = null!;

    public string Os { get; set; } = null!;

    public string Device { get; set; } = null!;
}
