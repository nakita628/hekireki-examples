#nullable enable

using System;
using System.Collections.Generic;

namespace Langfuse.Models;

public partial class SkillBlob
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string Sha256Hash { get; set; } = null!;

    public string ContentType { get; set; } = null!;

    public int ContentLength { get; set; }

    public string Content { get; set; } = null!;

    public virtual ICollection<SkillFile> Files { get; set; } = new List<SkillFile>();
}
