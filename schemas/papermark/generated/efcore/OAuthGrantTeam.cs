#nullable enable

using System;

namespace Papermark.Models;

public partial class OAuthGrantTeam
{
    public string Id { get; set; } = null!;

    public string GrantId { get; set; } = null!;

    public string TeamId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}
