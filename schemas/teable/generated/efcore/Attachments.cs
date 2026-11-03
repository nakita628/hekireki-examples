#nullable enable

using System;

namespace Teable.Models;

public partial class Attachments
{
    public string Id { get; set; } = null!;

    public string Token { get; set; } = null!;

    public string Hash { get; set; } = null!;

    public long Size { get; set; }

    public string Mimetype { get; set; } = null!;

    public string Path { get; set; } = null!;

    public int? Width { get; set; }

    public int? Height { get; set; }

    public DateTime? DeletedTime { get; set; }

    public DateTime CreatedTime { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string? LastModifiedBy { get; set; }

    public string? ThumbnailPath { get; set; }
}
