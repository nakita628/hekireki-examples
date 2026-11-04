#nullable enable

using System;
using System.Collections.Generic;

namespace AnythingLLM.Models;

public partial class Users
{
    public int Id { get; set; }

    public string? Username { get; set; }

    public string Password { get; set; } = null!;

    public string? PfpFilename { get; set; }

    public string Role { get; set; } = "default";

    public int Suspended { get; set; } = 0;

    public bool? SeenRecoveryCodes { get; set; } = false;

    public DateTime CreatedAt { get; set; }

    public DateTime LastUpdatedAt { get; set; }

    public int? DailyMessageLimit { get; set; }

    public string? Bio { get; set; } = "";

    public string? WebPushSubscriptionConfig { get; set; }

    public virtual ICollection<WorkspaceChats> WorkspaceChats { get; set; } = new List<WorkspaceChats>();

    public virtual ICollection<WorkspaceUsers> WorkspaceUsers { get; set; } = new List<WorkspaceUsers>();

    public virtual ICollection<EmbedConfigs> EmbedConfigs { get; set; } = new List<EmbedConfigs>();

    public virtual ICollection<EmbedChats> EmbedChats { get; set; } = new List<EmbedChats>();

    public virtual ICollection<WorkspaceThreads> Threads { get; set; } = new List<WorkspaceThreads>();

    public virtual ICollection<RecoveryCodes> RecoveryCodes { get; set; } = new List<RecoveryCodes>();

    public virtual ICollection<PasswordResetTokens> PasswordResetTokens { get; set; } = new List<PasswordResetTokens>();

    public virtual ICollection<WorkspaceAgentInvocations> WorkspaceAgentInvocations { get; set; } = new List<WorkspaceAgentInvocations>();

    public virtual ICollection<SlashCommandPresets> SlashCommandPresets { get; set; } = new List<SlashCommandPresets>();

    public virtual ICollection<BrowserExtensionApiKeys> BrowserExtensionApiKeys { get; set; } = new List<BrowserExtensionApiKeys>();

    public virtual ICollection<TemporaryAuthTokens> TemporaryAuthTokens { get; set; } = new List<TemporaryAuthTokens>();

    public virtual ICollection<SystemPromptVariables> SystemPromptVariables { get; set; } = new List<SystemPromptVariables>();

    public virtual ICollection<PromptHistory> PromptHistory { get; set; } = new List<PromptHistory>();

    public virtual ICollection<DesktopMobileDevices> DesktopMobileDevices { get; set; } = new List<DesktopMobileDevices>();

    public virtual ICollection<WorkspaceParsedFiles> WorkspaceParsedFiles { get; set; } = new List<WorkspaceParsedFiles>();

    public virtual ICollection<Memories> Memories { get; set; } = new List<Memories>();
}
