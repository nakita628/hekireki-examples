#nullable enable

using System;

namespace Documenso.Models;

public partial class AuthHandoff
{
    public string Id { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string Payload { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime ExpiresAt { get; set; }
}
