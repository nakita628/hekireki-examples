#nullable enable

using System;
using System.Collections.Generic;

namespace TypeHero.Models;

public partial class Submission
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public bool IsSuccessful { get; set; }

    public virtual User User { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public virtual Challenge Challenge { get; set; } = null!;

    public int ChallengeId { get; set; }

    public virtual ICollection<Vote> Vote { get; set; } = new List<Vote>();
}
