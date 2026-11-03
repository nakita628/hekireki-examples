#nullable enable

using System;
using System.Collections.Generic;

namespace Ghostfolio.Models;

public partial class SymbolProfile
{
    public virtual ICollection<Order> Activities { get; set; } = new List<Order>();

    public AssetClass? AssetClass { get; set; }

    public virtual AssetProfileOverrides? AssetProfileOverrides { get; set; }

    public virtual ICollection<AssetProfileSplit> AssetProfileSplits { get; set; } = new List<AssetProfileSplit>();

    public AssetSubClass? AssetSubClass { get; set; }

    public string? Comment { get; set; }

    public string? Countries { get; set; }

    public DateTime CreatedAt { get; set; }

    public string Currency { get; set; } = null!;

    public string? Cusip { get; set; }

    public DataGatheringFrequency DataGatheringFrequency { get; set; } = DataGatheringFrequency.Daily;

    public DataSource DataSource { get; set; }

    public string? Figi { get; set; }

    public string? FigiComposite { get; set; }

    public string? FigiShareClass { get; set; }

    public string? Holdings { get; set; } = "[]";

    public string Id { get; set; } = null!;

    public bool IsActive { get; set; } = true;

    public string? Isin { get; set; }

    public string? Name { get; set; }

    public string? ScraperConfiguration { get; set; }

    public string? Sectors { get; set; }

    public string Symbol { get; set; } = null!;

    public string? SymbolMapping { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? Url { get; set; }

    public virtual User? User { get; set; }

    public string? UserId { get; set; }

    public virtual ICollection<User> WatchedBy { get; set; } = new List<User>();
}
