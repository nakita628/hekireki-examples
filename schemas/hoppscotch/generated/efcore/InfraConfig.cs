#nullable enable

using System;

namespace Hoppscotch.Models;

public partial class InfraConfig
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Value { get; set; }

    public bool IsEncrypted { get; set; } = false;

    public string? LastSyncedEnvFileValue { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime UpdatedOn { get; set; }
}
