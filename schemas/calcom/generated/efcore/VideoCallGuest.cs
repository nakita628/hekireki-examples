#nullable enable

using System;

namespace Calcom.Models;

public partial class VideoCallGuest
{
    public string Id { get; set; } = null!;

    public string BookingUid { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Name { get; set; } = null!;

    public DateTime? JoinedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
