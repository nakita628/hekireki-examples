#nullable enable

using System;

namespace LangWatch.Models;

public partial class IdentifierReservation
{
    public string NormalizedValue { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string IdentifierId { get; set; } = null!;

    public string CommandId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}
