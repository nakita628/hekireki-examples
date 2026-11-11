#nullable enable

namespace LiteLLM.Models;

public partial class LiteLLMLensReview
{
    public string LensId { get; set; } = null!;

    public string CriteriaKey { get; set; } = null!;

    public string ExecutionId { get; set; } = null!;

    public string Data { get; set; } = null!;

    public virtual LiteLLMLens Lens { get; set; } = null!;
}
