#nullable enable

using System;
using System.Collections.Generic;

namespace Ghostfolio.Models;

public partial class Account
{
    public virtual ICollection<Order> Activities { get; set; } = new List<Order>();

    public virtual ICollection<AccountBalance> Balances { get; set; } = new List<AccountBalance>();

    public string? Comment { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? Currency { get; set; }

    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public virtual Platform? Platform { get; set; }

    public string? PlatformId { get; set; }

    public virtual ICollection<TagsOnAccounts> Tags { get; set; } = new List<TagsOnAccounts>();

    public DateTime UpdatedAt { get; set; }

    public virtual User User { get; set; } = null!;

    public string UserId { get; set; } = null!;
}
