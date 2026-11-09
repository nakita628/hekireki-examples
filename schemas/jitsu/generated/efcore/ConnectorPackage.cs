#nullable enable

using System;

namespace Jitsu.Models;

public partial class ConnectorPackage
{
    public string Id { get; set; } = null!;

    public string PackageId { get; set; } = null!;

    public string PackageType { get; set; } = "airbyte";

    public string? Meta { get; set; }

    public byte[]? LogoSvg { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
