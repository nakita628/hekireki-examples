#nullable enable

using System;

namespace Ghostfolio.Models;

public partial class TagsOnAccounts
{
    public virtual Account Account { get; set; } = null!;

    public string AccountId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual Tag Tag { get; set; } = null!;

    public string TagId { get; set; } = null!;

    public DateTime UpdatedAt { get; set; }

    public string UserId { get; set; } = null!;
}
