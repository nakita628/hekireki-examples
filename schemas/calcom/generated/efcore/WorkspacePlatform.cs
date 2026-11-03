#nullable enable

using System;
using System.Collections.Generic;

namespace Calcom.Models;

public partial class WorkspacePlatform
{
    public int Id { get; set; }

    public string Slug { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string DefaultServiceAccountKey { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool Enabled { get; set; } = false;

    public virtual ICollection<DelegationCredential> DelegationCredentials { get; set; } = new List<DelegationCredential>();
}
