#nullable enable

using System;
using System.Collections.Generic;

namespace OpenPanel.Models;

public partial class UserTotp
{
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public string Secret { get; set; } = null!;

    public List<string>? RecoveryCodes { get; set; }

    public DateTime? EnabledAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
