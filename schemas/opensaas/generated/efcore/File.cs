#nullable enable

using System;

namespace OpenSaas.Models;

public partial class File
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual User User { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string S3Key { get; set; } = null!;
}
