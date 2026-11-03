#nullable enable

using System;

namespace Papermark.Models;

public partial class CustomFieldResponse
{
    public string Id { get; set; } = null!;

    public string Data { get; set; } = null!;

    public string ViewId { get; set; } = null!;

    public virtual View View { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
