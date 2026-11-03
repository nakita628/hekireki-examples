#nullable enable

using System;

namespace Postiz.Models;

public partial class Errors
{
    public string Id { get; set; } = null!;

    public string Message { get; set; } = null!;

    public string Platform { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string PostId { get; set; } = null!;

    public string Body { get; set; } = "{}";

    public virtual Organization Organization { get; set; } = null!;

    public virtual Post Post { get; set; } = null!;
}
