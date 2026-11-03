#nullable enable

using System.Collections.Generic;

namespace Trigger.Models;

public partial class LlmPricingTier
{
    public string Id { get; set; } = null!;

    public string ModelId { get; set; } = null!;

    public virtual LlmModel Model { get; set; } = null!;

    public string Name { get; set; } = null!;

    public bool IsDefault { get; set; } = true;

    public int Priority { get; set; } = 0;

    public string Conditions { get; set; } = "[]";

    public virtual ICollection<LlmPrice> Prices { get; set; } = new List<LlmPrice>();
}
