#nullable enable

using System;

namespace AnythingLLM.Models;

public partial class RecoveryCodes
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string CodeHash { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual Users User { get; set; } = null!;
}
