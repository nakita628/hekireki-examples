#nullable enable

using System;

namespace InboxZero.Models;

public partial class FilingFolder
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string FolderId { get; set; } = null!;

    public string FolderName { get; set; } = null!;

    public string FolderPath { get; set; } = null!;

    public string DriveConnectionId { get; set; } = null!;

    public virtual DriveConnection DriveConnection { get; set; } = null!;

    public string EmailAccountId { get; set; } = null!;

    public virtual EmailAccount EmailAccount { get; set; } = null!;
}
