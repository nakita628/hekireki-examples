#nullable enable

using System;
using System.Collections.Generic;

namespace AnythingLLM.Models;

public partial class Workspaces
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string? VectorTag { get; set; }

    public DateTime CreatedAt { get; set; }

    public double? OpenAiTemp { get; set; }

    public int OpenAiHistory { get; set; } = 20;

    public DateTime LastUpdatedAt { get; set; }

    public string? OpenAiPrompt { get; set; }

    public double? SimilarityThreshold { get; set; } = 0.25;

    public string? ChatProvider { get; set; }

    public string? ChatModel { get; set; }

    public int? TopN { get; set; } = 4;

    public string? ChatMode { get; set; } = "chat";

    public string? PfpFilename { get; set; }

    public string? AgentProvider { get; set; }

    public string? AgentModel { get; set; }

    public string? QueryRefusalResponse { get; set; }

    public string? VectorSearchMode { get; set; } = "default";

    public int? RouterId { get; set; }

    public virtual ICollection<WorkspaceUsers> WorkspaceUsers { get; set; } = new List<WorkspaceUsers>();

    public virtual ICollection<WorkspaceDocuments> Documents { get; set; } = new List<WorkspaceDocuments>();

    public virtual ICollection<WorkspaceSuggestedMessages> WorkspaceSuggestedMessages { get; set; } = new List<WorkspaceSuggestedMessages>();

    public virtual ICollection<EmbedConfigs> EmbedConfigs { get; set; } = new List<EmbedConfigs>();

    public virtual ICollection<WorkspaceThreads> Threads { get; set; } = new List<WorkspaceThreads>();

    public virtual ICollection<WorkspaceAgentInvocations> WorkspaceAgentInvocations { get; set; } = new List<WorkspaceAgentInvocations>();

    public virtual ICollection<PromptHistory> PromptHistory { get; set; } = new List<PromptHistory>();

    public virtual ICollection<WorkspaceParsedFiles> WorkspaceParsedFiles { get; set; } = new List<WorkspaceParsedFiles>();

    public virtual ICollection<Memories> Memories { get; set; } = new List<Memories>();
}
