#nullable enable

using System;
using System.Collections.Generic;

namespace InboxZero.Models;

public partial class MailSplit
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Name { get; set; } = null!;

    public int Order { get; set; }

    public bool MatchAll { get; set; } = true;

    public string EmailAccountId { get; set; } = null!;

    public virtual EmailAccount EmailAccount { get; set; } = null!;

    public virtual ICollection<MailSplitFilter> Filters { get; set; } = new List<MailSplitFilter>();
}
