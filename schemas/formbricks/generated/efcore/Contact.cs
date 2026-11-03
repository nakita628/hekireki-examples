#nullable enable

using System;
using System.Collections.Generic;

namespace Formbricks.Models;

public partial class Contact
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Workspace Workspace { get; set; } = null!;

    public string WorkspaceId { get; set; } = null!;

    public virtual ICollection<Response> Responses { get; set; } = new List<Response>();

    public virtual ICollection<ContactAttribute> Attributes { get; set; } = new List<ContactAttribute>();

    public virtual ICollection<Display> Displays { get; set; } = new List<Display>();
}
