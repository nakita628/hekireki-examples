#nullable enable

using System;

namespace Umami.Models;

public partial class ErrorRateLimit
{
    public Guid WebsiteId { get; set; }

    public DateTime Bucket { get; set; }

    public int Count { get; set; }
}
