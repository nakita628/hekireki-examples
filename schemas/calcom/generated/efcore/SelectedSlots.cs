#nullable enable

using System;

namespace Calcom.Models;

public partial class SelectedSlots
{
    public int Id { get; set; }

    public int EventTypeId { get; set; }

    public int UserId { get; set; }

    public DateTime SlotUtcStartDate { get; set; }

    public DateTime SlotUtcEndDate { get; set; }

    public string Uid { get; set; } = null!;

    public DateTime ReleaseAt { get; set; }

    public bool IsSeat { get; set; } = false;
}
