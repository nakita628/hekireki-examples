#nullable enable

using System;
using System.Collections.Generic;

namespace Trigger.Models;

public partial class InvitationCode
{
    public string Id { get; set; } = null!;

    public string Code { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();

    public DateTime CreatedAt { get; set; }
}
