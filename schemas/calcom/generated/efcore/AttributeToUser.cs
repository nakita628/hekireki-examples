#nullable enable

using System;

namespace Calcom.Models;

public partial class AttributeToUser
{
    public string Id { get; set; } = null!;

    public virtual Membership Member { get; set; } = null!;

    public int MemberId { get; set; }

    public virtual AttributeOption AttributeOption { get; set; } = null!;

    public string AttributeOptionId { get; set; } = null!;

    public int? Weight { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? CreatedById { get; set; }

    public virtual User? CreatedBy { get; set; }

    public string? CreatedByDSyncId { get; set; }

    public virtual DSyncData? CreatedByDSync { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User? UpdatedBy { get; set; }

    public int? UpdatedById { get; set; }

    public string? UpdatedByDSyncId { get; set; }

    public virtual DSyncData? UpdatedByDSync { get; set; }
}
