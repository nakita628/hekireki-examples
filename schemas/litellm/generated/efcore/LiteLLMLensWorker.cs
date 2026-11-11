#nullable enable

namespace LiteLLM.Models;

public partial class LiteLLMLensWorker
{
    public string Id { get; set; } = null!;

    public string TokenHash { get; set; } = null!;

    public string Data { get; set; } = null!;
}
