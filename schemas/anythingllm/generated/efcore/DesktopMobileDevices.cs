#nullable enable

using System;

namespace AnythingLLM.Models;

public partial class DesktopMobileDevices
{
    public int Id { get; set; }

    public string DeviceOs { get; set; } = null!;

    public string DeviceName { get; set; } = null!;

    public string Token { get; set; } = null!;

    public bool Approved { get; set; } = false;

    public int? UserId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Users? User { get; set; }
}
