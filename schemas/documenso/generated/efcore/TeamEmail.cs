#nullable enable

using System;

namespace Documenso.Models;

public partial class TeamEmail
{
    public int TeamId { get; set; }

    public DateTime CreatedAt { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual Team Team { get; set; } = null!;
}
