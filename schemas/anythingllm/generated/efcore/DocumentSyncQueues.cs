#nullable enable

using System;
using System.Collections.Generic;

namespace AnythingLLM.Models;

public partial class DocumentSyncQueues
{
    public int Id { get; set; }

    public int StaleAfterMs { get; set; } = 604800000;

    public DateTime NextSyncAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime LastSyncedAt { get; set; }

    public int WorkspaceDocId { get; set; }

    public virtual WorkspaceDocuments? WorkspaceDoc { get; set; }

    public virtual ICollection<DocumentSyncExecutions> Runs { get; set; } = new List<DocumentSyncExecutions>();
}
