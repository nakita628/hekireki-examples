#nullable enable

using System;
using System.Collections.Generic;

namespace Formbricks.Models;

public partial class Language
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Code { get; set; } = null!;

    public string? Alias { get; set; }

    public virtual Workspace Workspace { get; set; } = null!;

    public string WorkspaceId { get; set; } = null!;

    public virtual ICollection<SurveyLanguage> SurveyLanguages { get; set; } = new List<SurveyLanguage>();

    public virtual ICollection<CreativeLanguage> CreativeLanguages { get; set; } = new List<CreativeLanguage>();
}
