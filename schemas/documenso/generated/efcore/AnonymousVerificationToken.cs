#nullable enable

using System;

namespace Documenso.Models;

public partial class AnonymousVerificationToken
{
    public string Id { get; set; } = null!;

    public string Token { get; set; } = null!;

    public DateTime ExpiresAt { get; set; }

    public DateTime CreatedAt { get; set; }
}
