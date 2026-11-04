#nullable enable

using System;
using System.Collections.Generic;

namespace AnythingLLM.Models;

public partial class EmbedConfigs
{
    public int Id { get; set; }

    public string Uuid { get; set; } = null!;

    public bool Enabled { get; set; } = false;

    public string ChatMode { get; set; } = "query";

    public string? AllowlistDomains { get; set; }

    public bool AllowModelOverride { get; set; } = false;

    public bool AllowTemperatureOverride { get; set; } = false;

    public bool AllowPromptOverride { get; set; } = false;

    public int? MaxChatsPerDay { get; set; }

    public int? MaxChatsPerSession { get; set; }

    public int? MessageLimit { get; set; } = 20;

    public int WorkspaceId { get; set; }

    public int? CreatedBy { get; set; }

    public int? UsersId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Workspaces Workspace { get; set; } = null!;

    public virtual ICollection<EmbedChats> EmbedChats { get; set; } = new List<EmbedChats>();

    public virtual Users? Users { get; set; }
}
