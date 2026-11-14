#nullable enable

using System;

namespace LangWatch.Models;

public partial class DataPrivacyPolicy
{
    public string Id { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public DataPrivacyScopeType ScopeType { get; set; }

    public string ScopeId { get; set; } = null!;

    public bool PersonalOnly { get; set; } = false;

    public string Config { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
