#nullable enable

using System;
using System.Collections.Generic;

namespace InboxZero.Models;

public partial class DriveConnection
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Provider { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? AccessToken { get; set; }

    public string? RefreshToken { get; set; }

    public DateTime? ExpiresAt { get; set; }

    public bool IsConnected { get; set; } = true;

    public string EmailAccountId { get; set; } = null!;

    public virtual EmailAccount EmailAccount { get; set; } = null!;

    public virtual ICollection<DocumentFiling> DocumentFilings { get; set; } = new List<DocumentFiling>();

    public virtual ICollection<FilingFolder> FilingFolders { get; set; } = new List<FilingFolder>();

    public virtual ICollection<AttachmentSource> AttachmentSources { get; set; } = new List<AttachmentSource>();
}
