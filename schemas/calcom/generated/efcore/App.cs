#nullable enable

using System;
using System.Collections.Generic;

namespace Calcom.Models;

public partial class App
{
    public string Slug { get; set; } = null!;

    public string DirName { get; set; } = null!;

    public string? Keys { get; set; }

    public List<AppCategories>? Categories { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Credential> Credentials { get; set; } = new List<Credential>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Webhook> Webhook { get; set; } = new List<Webhook>();

    public virtual ICollection<ApiKey> ApiKey { get; set; } = new List<ApiKey>();

    public bool Enabled { get; set; } = false;
}
