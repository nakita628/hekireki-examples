#nullable enable

using System;
using System.Collections.Generic;

namespace LiteLLM.Models;

public partial class LiteLLMPolicyAttachmentTable
{
    public string AttachmentId { get; set; } = null!;

    public string PolicyName { get; set; } = null!;

    public string? Scope { get; set; }

    public List<string>? Teams { get; set; } = new List<string>();

    public List<string>? Keys { get; set; } = new List<string>();

    public List<string>? Models { get; set; } = new List<string>();

    public List<string>? Tags { get; set; } = new List<string>();

    public int? Priority { get; set; }

    public bool IsDefault { get; set; } = false;

    public DateTime CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }
}
