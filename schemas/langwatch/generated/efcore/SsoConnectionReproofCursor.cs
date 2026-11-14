#nullable enable

using System;

namespace LangWatch.Models;

public partial class SsoConnectionReproofCursor
{
    public string ConnectionId { get; set; } = null!;

    public DateTime LastReproofAt { get; set; }

    public virtual SsoConnection Connection { get; set; } = null!;
}
