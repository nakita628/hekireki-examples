#nullable enable

namespace LiteLLM.Models;

public partial class LiteLLMAutoRouterDailySpend
{
    public string Date { get; set; } = null!;

    public string ApiKey { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string RouterName { get; set; } = null!;

    public string RouterType { get; set; } = null!;

    public int Turns { get; set; } = 0;

    public long TotalTokens { get; set; } = 0L;

    public int TokenRecordedTurns { get; set; } = 0;

    public double Spend { get; set; } = 0.0;

    public double SavedSpend { get; set; } = 0.0;

    public int SavingsEstimatedTurns { get; set; } = 0;

    public double SavingsEstimatedActualSpend { get; set; } = 0.0;

    public double SavingsEstimatedSavedSpend { get; set; } = 0.0;

    public double ClassifierCost { get; set; } = 0.0;

    public int ClassifierCostRecordedTurns { get; set; } = 0;
}
