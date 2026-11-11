#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMLensRun
{
    public string Id { get; set; } = null!;

    public string LensId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string Data { get; set; } = null!;
}
