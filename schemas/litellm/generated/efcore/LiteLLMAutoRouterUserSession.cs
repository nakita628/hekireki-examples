#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMAutoRouterUserSession
{
    public string UserId { get; set; } = null!;

    public string ApiKey { get; set; } = null!;

    public string SessionId { get; set; } = null!;

    public string RouterName { get; set; } = null!;

    public string RouterType { get; set; } = null!;

    public DateTime FirstTurnAt { get; set; }

    public DateTime LastTurnAt { get; set; }

    public string LastModel { get; set; } = null!;

    public string Models { get; set; } = "{}";

    public int Turns { get; set; } = 0;

    public int UnorderedTurns { get; set; } = 0;

    public int CoveredTurns { get; set; } = 0;

    public int CacheHits { get; set; } = 0;

    public int SameModelTurns { get; set; } = 0;

    public int SameModelHits { get; set; } = 0;

    public int FirstVisitTurns { get; set; } = 0;

    public int FirstVisitHits { get; set; } = 0;

    public int ReturnTurns { get; set; } = 0;

    public int ReturnHits { get; set; } = 0;

    public int ReturnExpiredMisses { get; set; } = 0;

    public int ReturnWithinTtlMisses { get; set; } = 0;

    public int Ttl5mTurns { get; set; } = 0;

    public int Ttl1hTurns { get; set; } = 0;

    public long TotalTokens { get; set; } = 0L;

    public double Spend { get; set; } = 0.0;

    public double SavedSpend { get; set; } = 0.0;

    public int SavingsEstimatedTurns { get; set; } = 0;

    public double SavingsEstimatedActualSpend { get; set; } = 0.0;

    public double SavingsEstimatedSavedSpend { get; set; } = 0.0;

    public string SavingsEstimatedBaselineModels { get; set; } = "{}";

    public double ClassifierCost { get; set; } = 0.0;

    public int ClassifierCostRecordedTurns { get; set; } = 0;

    public string TierTurns { get; set; } = "{}";

    public string BaselineModels { get; set; } = "{}";
}
