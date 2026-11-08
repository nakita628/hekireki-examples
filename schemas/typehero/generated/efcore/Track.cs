#nullable enable

using System.Collections.Generic;

namespace TypeHero.Models;

public partial class Track
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string Description { get; set; } = null!;

    public bool Visible { get; set; } = true;

    public bool IsComingSoon { get; set; } = false;

    public virtual ICollection<TrackChallenge> TrackChallenges { get; set; } = new List<TrackChallenge>();

    public virtual ICollection<User> EnrolledUsers { get; set; } = new List<User>();
}
