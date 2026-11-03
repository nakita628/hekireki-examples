#nullable enable

using System;

namespace Typebot.Models;

public partial class ApiToken
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string Token { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string OwnerId { get; set; } = null!;

    public virtual User Owner { get; set; } = null!;
}
