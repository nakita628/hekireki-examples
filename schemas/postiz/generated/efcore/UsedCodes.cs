#nullable enable

using System;

namespace Postiz.Models;

public partial class UsedCodes
{
    public string Id { get; set; } = null!;

    public string Code { get; set; } = null!;

    public string OrgId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Organization Organization { get; set; } = null!;
}
