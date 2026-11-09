#nullable enable

using System;

namespace Jitsu.Models;

public partial class KvStoreEntry
{
    public string Key { get; set; } = null!;

    public string Value { get; set; } = null!;

    public DateTime? ExpiresAt { get; set; }
}
