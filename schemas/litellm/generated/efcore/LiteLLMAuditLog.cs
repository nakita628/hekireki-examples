#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMAuditLog
{
    public string Id { get; set; } = null!;

    public DateTime UpdatedAt { get; set; }

    public string ChangedBy { get; set; } = "";

    public string ChangedByApiKey { get; set; } = "";

    public string Action { get; set; } = null!;

    public string TableName { get; set; } = null!;

    public string ObjectId { get; set; } = null!;

    public string? BeforeValue { get; set; }

    public string? UpdatedValues { get; set; }
}
