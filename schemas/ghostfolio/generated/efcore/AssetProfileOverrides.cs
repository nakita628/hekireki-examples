#nullable enable

using System;

namespace Ghostfolio.Models;

public partial class AssetProfileOverrides
{
    public AssetClass? AssetClass { get; set; }

    public AssetSubClass? AssetSubClass { get; set; }

    public string? Countries { get; set; } = "[]";

    public string? Holdings { get; set; } = "[]";

    public string? Name { get; set; }

    public string? Sectors { get; set; } = "[]";

    public virtual SymbolProfile SymbolProfile { get; set; } = null!;

    public string SymbolProfileId { get; set; } = null!;

    public DateTime UpdatedAt { get; set; }

    public string? Url { get; set; }
}
