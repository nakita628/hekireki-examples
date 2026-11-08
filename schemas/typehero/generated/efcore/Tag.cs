#nullable enable

using System.Collections.Generic;

namespace TypeHero.Models;

public partial class Tag
{
    public string Id { get; set; } = null!;

    public Tags Tag1 { get; set; }

    public virtual ICollection<Challenge> Challenges { get; set; } = new List<Challenge>();
}
