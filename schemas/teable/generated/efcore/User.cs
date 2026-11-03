#nullable enable

using System;
using System.Collections.Generic;

namespace Teable.Models;

public partial class User
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Password { get; set; }

    public string? Salt { get; set; }

    public string? Phone { get; set; }

    public string Email { get; set; } = null!;

    public string? Avatar { get; set; }

    public bool? IsSystem { get; set; }

    public bool? IsAdmin { get; set; }

    public bool? IsTrialUsed { get; set; }

    public string? Lang { get; set; }

    public string? NotifyMeta { get; set; }

    public DateTime? LastSignTime { get; set; }

    public DateTime? DeactivatedTime { get; set; }

    public DateTime CreatedTime { get; set; }

    public DateTime? DeletedTime { get; set; }

    public DateTime? LastModifiedTime { get; set; }

    public DateTime? PermanentDeletedTime { get; set; }

    public string? RefMeta { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual ICollection<UserBaseOrder> BaseOrders { get; set; } = new List<UserBaseOrder>();
}
