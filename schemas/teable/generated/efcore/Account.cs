#nullable enable

using System;

namespace Teable.Models;

public partial class Account
{
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string Provider { get; set; } = null!;

    public string ProviderId { get; set; } = null!;

    public string ConnectionId { get; set; } = "";

    public DateTime CreatedTime { get; set; }

    public virtual User User { get; set; } = null!;
}
