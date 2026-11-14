#nullable enable

using System;

namespace LangWatch.Models;

public partial class ErasedIdentifierSuppression
{
    public string Id { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public string Provider { get; set; } = null!;

    public string IdentifierHash { get; set; } = null!;

    public DateTime ErasedAt { get; set; }
}
