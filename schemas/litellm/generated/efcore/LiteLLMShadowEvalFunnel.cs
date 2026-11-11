#nullable enable

namespace LiteLLM.Models;

public partial class LiteLLMShadowEvalFunnel
{
    public string JobId { get; set; } = null!;

    public int NotSampled { get; set; } = 0;

    public int Unjudgeable { get; set; } = 0;

    public int Shed { get; set; } = 0;

    public int Withheld { get; set; } = 0;
}
