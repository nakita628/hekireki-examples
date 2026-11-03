#nullable enable

using System;
using System.Collections.Generic;

namespace Formbricks.Models;

public partial class OauthConsent
{
    public string Id { get; set; } = null!;

    public string ClientId { get; set; } = null!;

    public virtual OauthClient Client { get; set; } = null!;

    public string? UserId { get; set; }

    public virtual User? User { get; set; }

    public string? ReferenceId { get; set; }

    public List<string>? Scopes { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public List<string>? Resources { get; set; } = new List<string>();

    public List<string>? RequestedUserInfoClaims { get; set; } = new List<string>();
}
