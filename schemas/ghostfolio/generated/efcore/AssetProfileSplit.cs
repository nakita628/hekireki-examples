#nullable enable

using System;

namespace Ghostfolio.Models;

public partial class AssetProfileSplit
{
    public DateTime CreatedAt { get; set; }

    public DateTime Date { get; set; }

    public int Denominator { get; set; }

    public string Id { get; set; } = null!;

    public int Numerator { get; set; }

    public virtual SymbolProfile SymbolProfile { get; set; } = null!;

    public string SymbolProfileId { get; set; } = null!;

    public DateTime UpdatedAt { get; set; }
}
