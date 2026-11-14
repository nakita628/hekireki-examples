#nullable enable

using System;

namespace LangWatch.Models;

public partial class SeatHold
{
    public string Id { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string? Email { get; set; }

    public string? Name { get; set; }

    public DateTime SeatTakenAt { get; set; }

    public DateTime HeldUntil { get; set; }

    public string Reason { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}
