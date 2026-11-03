#nullable enable

using System;
using System.Collections.Generic;

namespace Calcom.Models;

public partial class AuditActor
{
    public Guid Id { get; set; }

    public AuditActorType Type { get; set; }

    public Guid? UserUuid { get; set; }

    public int? AttendeeId { get; set; }

    public int? CredentialId { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Name { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<BookingAudit> BookingAudits { get; set; } = new List<BookingAudit>();
}
