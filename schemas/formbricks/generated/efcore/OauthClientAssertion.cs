#nullable enable

using System;

namespace Formbricks.Models;

public partial class OauthClientAssertion
{
    public string Id { get; set; } = null!;

    public DateTime ExpiresAt { get; set; }
}
