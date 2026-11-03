#nullable enable

using System;

namespace Hoppscotch.Models;

public partial class Shortcode
{
    public string Id { get; set; } = null!;

    public string Request { get; set; } = null!;

    public string? CreatorUid { get; set; }

    public string? EmbedProperties { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime UpdatedOn { get; set; }

    public virtual User? User { get; set; }
}
