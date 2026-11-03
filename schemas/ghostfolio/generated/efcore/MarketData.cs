#nullable enable

using System;

namespace Ghostfolio.Models;

public partial class MarketData
{
    public DateTime CreatedAt { get; set; }

    public DataSource DataSource { get; set; }

    public DateTime Date { get; set; }

    public string Id { get; set; } = null!;

    public bool IsCarriedForward { get; set; } = false;

    public double MarketPrice { get; set; }

    public MarketDataState State { get; set; } = MarketDataState.Close;

    public string Symbol { get; set; } = null!;
}
