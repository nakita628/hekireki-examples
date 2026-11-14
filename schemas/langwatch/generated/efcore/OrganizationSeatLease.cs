#nullable enable

using System;

namespace LangWatch.Models;

public partial class OrganizationSeatLease
{
    public string OrganizationId { get; set; } = null!;

    public string Holder { get; set; } = null!;

    public DateTime ExpiresAt { get; set; }

    public DateTime CreatedAt { get; set; }
}
