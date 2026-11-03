#nullable enable

using System;

namespace Typebot.Models;

public partial class Credentials
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string WorkspaceId { get; set; } = null!;

    public string Data { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string Iv { get; set; } = null!;

    public virtual Workspace Workspace { get; set; } = null!;
}
