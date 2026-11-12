#nullable enable

using System;
using System.Collections.Generic;

namespace Blinko.Models;

public partial class AiProviders
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Provider { get; set; } = null!;

    public string? BaseURL { get; set; }

    public string? ApiKey { get; set; }

    public string? Config { get; set; }

    public int SortOrder { get; set; } = 0;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<AiModels> Models { get; set; } = new List<AiModels>();
}
