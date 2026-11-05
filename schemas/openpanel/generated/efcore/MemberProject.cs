#nullable enable

using System;

namespace OpenPanel.Models;

public partial class MemberProject
{
    public Guid MemberId { get; set; }

    public virtual Member Member { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public AccessLevel? Level { get; set; }

    public DateTime CreatedAt { get; set; }
}
