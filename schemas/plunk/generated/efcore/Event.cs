#nullable enable

using System;

namespace Plunk.Models;

public partial class Event
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Data { get; set; }

    public virtual Project Project { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual Contact? Contact { get; set; }

    public string? ContactId { get; set; }

    public virtual Email? Email { get; set; }

    public string? EmailId { get; set; }

    public DateTime CreatedAt { get; set; }
}
