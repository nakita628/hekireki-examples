#nullable enable

using System;

namespace Langfuse.Models;

public partial class SkillFile
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string SkillId { get; set; } = null!;

    public virtual Skill Skill { get; set; } = null!;

    public string BlobId { get; set; } = null!;

    public virtual SkillBlob Blob { get; set; } = null!;

    public string Path { get; set; } = null!;
}
