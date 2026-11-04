#nullable enable

using System;

namespace AnythingLLM.Models;

public partial class BrowserExtensionApiKeys
{
    public int Id { get; set; }

    public string Key { get; set; } = null!;

    public int? UserId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime LastUpdatedAt { get; set; }

    public virtual Users? User { get; set; }
}
