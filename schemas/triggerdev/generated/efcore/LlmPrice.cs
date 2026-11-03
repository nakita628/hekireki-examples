#nullable enable

namespace Trigger.Models;

public partial class LlmPrice
{
    public string Id { get; set; } = null!;

    public string ModelId { get; set; } = null!;

    public virtual LlmModel Model { get; set; } = null!;

    public string PricingTierId { get; set; } = null!;

    public virtual LlmPricingTier PricingTier { get; set; } = null!;

    public string UsageType { get; set; } = null!;

    public decimal Price { get; set; }
}
