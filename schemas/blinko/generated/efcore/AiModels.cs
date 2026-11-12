#nullable enable

using System;

namespace Blinko.Models;

public partial class AiModels
{
    public int Id { get; set; }

    public int ProviderId { get; set; }

    public string Title { get; set; } = null!;

    public string ModelKey { get; set; } = null!;

    public string Capabilities { get; set; } = null!;

    public string? Config { get; set; }

    public int SortOrder { get; set; } = 0;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual AiProviders Provider { get; set; } = null!;
}
