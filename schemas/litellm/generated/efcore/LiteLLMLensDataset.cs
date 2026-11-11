#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMLensDataset
{
    public string Id { get; set; } = null!;

    public int Revision { get; set; }

    public DateTime CreatedAt { get; set; }

    public string Data { get; set; } = null!;
}
