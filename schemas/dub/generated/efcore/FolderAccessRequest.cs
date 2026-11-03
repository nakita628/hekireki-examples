#nullable enable

using System;

namespace Dub.Models;

public partial class FolderAccessRequest
{
    public string Id { get; set; } = null!;

    public string FolderId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual Folder Folder { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
