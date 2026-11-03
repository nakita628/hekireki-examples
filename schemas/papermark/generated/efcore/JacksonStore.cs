#nullable enable

using System;

namespace Papermark.Models;

public partial class JacksonStore
{
    public string Key { get; set; } = null!;

    public string Value { get; set; } = null!;

    public string? Iv { get; set; }

    public string? Tag { get; set; }

    public string? Namespace { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }
}
