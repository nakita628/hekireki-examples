#nullable enable

using System;
using System.Collections.Generic;

namespace Calcom.Models;

public partial class SecondaryEmail
{
    public int Id { get; set; }

    public virtual User User { get; set; } = null!;

    public int UserId { get; set; }

    public string Email { get; set; } = null!;

    public DateTime? EmailVerified { get; set; }

    public virtual ICollection<VerificationToken> VerificationTokens { get; set; } = new List<VerificationToken>();

    public virtual ICollection<EventType> EventTypes { get; set; } = new List<EventType>();
}
