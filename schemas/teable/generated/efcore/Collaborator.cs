#nullable enable

using System;

namespace Teable.Models;

public partial class Collaborator
{
    public string Id { get; set; } = null!;

    public string RoleName { get; set; } = null!;

    public string ResourceType { get; set; } = null!;

    public string ResourceId { get; set; } = null!;

    public string PrincipalId { get; set; } = null!;

    public string PrincipalType { get; set; } = null!;

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedTime { get; set; }

    public DateTime? LastModifiedTime { get; set; }

    public string? LastModifiedBy { get; set; }
}
