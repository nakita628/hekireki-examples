#nullable enable

namespace Formbricks.Models;

public partial class SurveyLanguage
{
    public virtual Language Language { get; set; } = null!;

    public string LanguageId { get; set; } = null!;

    public string SurveyId { get; set; } = null!;

    public virtual Survey Survey { get; set; } = null!;

    public bool Default { get; set; } = false;

    public bool Enabled { get; set; } = true;
}
