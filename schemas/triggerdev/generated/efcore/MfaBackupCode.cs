#nullable enable

using System;

namespace Trigger.Models;

public partial class MfaBackupCode
{
    public string Id { get; set; } = null!;

    public string Code { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public DateTime? UsedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
