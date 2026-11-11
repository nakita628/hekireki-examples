#nullable enable

using System;
using System.Collections.Generic;

namespace LiteLLM.Models;

public partial class LiteLLMMCPServerTable
{
    public string ServerId { get; set; } = null!;

    public string? ServerName { get; set; }

    public string? Alias { get; set; }

    public string? Description { get; set; }

    public string? Instructions { get; set; }

    public string? Url { get; set; }

    public string? SpecPath { get; set; }

    public string Transport { get; set; } = "sse";

    public string? AuthType { get; set; }

    public string? Credentials { get; set; } = "{}";

    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;

    public string? UpdatedBy { get; set; }

    public string? McpInfo { get; set; } = "{}";

    public List<string>? McpAccessGroups { get; set; }

    public List<string>? AllowedTools { get; set; } = new List<string>();

    public string? ToolNameToDisplayName { get; set; } = "{}";

    public string? ToolNameToDescription { get; set; } = "{}";

    public string? PinnedTools { get; set; } = "{}";

    public List<string>? ExtraHeaders { get; set; } = new List<string>();

    public string? StaticHeaders { get; set; } = "{}";

    public string? EnvVars { get; set; } = "[]";

    public string? Status { get; set; } = "unknown";

    public DateTime? LastHealthCheck { get; set; }

    public string? HealthCheckError { get; set; }

    public string? Command { get; set; }

    public List<string>? Args { get; set; } = new List<string>();

    public string? Env { get; set; } = "{}";

    public string? Issuer { get; set; }

    public string? AuthorizationUrl { get; set; }

    public string? TokenUrl { get; set; }

    public string? RegistrationUrl { get; set; }

    public string? Oauth2Flow { get; set; }

    public string? TokenExchangeEndpoint { get; set; }

    public string? Audience { get; set; }

    public string? SubjectTokenType { get; set; }

    public string? TokenExchangeProfile { get; set; }

    public bool AllowAllKeys { get; set; } = false;

    public bool AvailableOnPublicInternet { get; set; } = true;

    public bool DelegateAuthToUpstream { get; set; } = false;

    public bool OauthPassthrough { get; set; } = false;

    public bool? DcrBridge { get; set; }

    public bool PerServerOauthDiscovery { get; set; } = false;

    public bool IsByok { get; set; } = false;

    public List<string>? ByokDescription { get; set; } = new List<string>();

    public string? ByokApiKeyHelpUrl { get; set; }

    public string? SourceUrl { get; set; }

    public double? Timeout { get; set; }

    public int? MaxConcurrentRequests { get; set; }

    public int? Rpm { get; set; }

    public string? ApprovalStatus { get; set; } = "active";

    public string? SubmittedBy { get; set; }

    public DateTime? SubmittedAt { get; set; }

    public DateTime? ReviewedAt { get; set; }

    public string? ReviewNotes { get; set; }
}
