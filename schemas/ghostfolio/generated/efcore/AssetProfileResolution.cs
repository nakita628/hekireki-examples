#nullable enable

using System;

namespace Ghostfolio.Models;

public partial class AssetProfileResolution
{
    public DateTime CreatedAt { get; set; }

    public string Currency { get; set; } = null!;

    public DataSource DataSourceOrigin { get; set; }

    public DataSource DataSourceTarget { get; set; }

    public string Id { get; set; } = null!;

    public int RequestCount { get; set; } = 1;

    public string SymbolOrigin { get; set; } = null!;

    public string SymbolTarget { get; set; } = null!;

    public DateTime UpdatedAt { get; set; }
}
