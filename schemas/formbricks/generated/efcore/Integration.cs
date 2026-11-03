#nullable enable

namespace Formbricks.Models;

public partial class Integration
{
    public string Id { get; set; } = null!;

    public IntegrationType Type { get; set; }

    public string Config { get; set; } = null!;

    public virtual Workspace Workspace { get; set; } = null!;

    public string WorkspaceId { get; set; } = null!;
}
