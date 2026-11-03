#nullable enable

using System;

namespace Typebot.Models;

public partial class RuntimeMediaIdCache
{
    public ChatProvider Provider { get; set; }

    public string Url { get; set; } = null!;

    public string MediaId { get; set; } = null!;

    public DateTime? ExpiresAt { get; set; }

    public virtual PublicTypebot? PublicTypebot { get; set; }

    public string PublicTypebotId { get; set; } = null!;
}
