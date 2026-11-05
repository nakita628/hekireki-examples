#nullable enable

using System;
using System.Collections.Generic;

namespace InboxZero.Models;

public partial class Category
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string EmailAccountId { get; set; } = null!;

    public virtual EmailAccount EmailAccount { get; set; } = null!;

    public virtual ICollection<Newsletter> EmailSenders { get; set; } = new List<Newsletter>();

    public virtual ICollection<Rule> Rules { get; set; } = new List<Rule>();
}
