#nullable enable

using System;

namespace Trigger.Models;

public partial class AuthorizationCode
{
    public string Id { get; set; } = null!;

    public string Code { get; set; } = null!;

    public virtual PersonalAccessToken? PersonalAccessToken { get; set; }

    public string? PersonalAccessTokenId { get; set; }

    public string? RequesterIp { get; set; }

    public string? CodeChallenge { get; set; }

    public int? CallbackPort { get; set; }

    public string? ApprovalCodeHash { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
