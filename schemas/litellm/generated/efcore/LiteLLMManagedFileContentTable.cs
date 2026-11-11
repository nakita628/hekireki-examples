#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMManagedFileContentTable
{
    public string Id { get; set; } = null!;

    public byte[] Content { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}
