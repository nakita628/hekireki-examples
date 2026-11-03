#nullable enable

using System;

namespace Postiz.Models;

public partial class MastraSkillBlobs
{
    public string Hash { get; set; } = null!;

    public string Content { get; set; } = null!;

    public int Size { get; set; }

    public string? MimeType { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? CreatedAtZ { get; set; } = DateTime.UtcNow;
}
