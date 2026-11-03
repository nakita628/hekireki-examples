#nullable enable

using System;
using System.Collections.Generic;

namespace Postiz.Models;

public partial class Customer
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string OrgId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual Organization Organization { get; set; } = null!;

    public virtual ICollection<Integration> Integrations { get; set; } = new List<Integration>();
}
