#nullable enable

namespace TypeHero.Models;

public partial class TrackChallenge
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int ChallengeId { get; set; }

    public virtual Challenge Challenge { get; set; } = null!;

    public virtual Track? Track { get; set; }

    public int? TrackId { get; set; }
}
