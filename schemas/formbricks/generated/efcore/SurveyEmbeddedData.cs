#nullable enable

namespace Formbricks.Models;

public partial class SurveyEmbeddedData
{
    public string Id { get; set; } = null!;

    public string WorkspaceId { get; set; } = null!;

    public string SurveyId { get; set; } = null!;

    public string EmbeddedDataId { get; set; } = null!;

    public string StorageKey { get; set; } = null!;

    public int Order { get; set; }

    public virtual Survey Survey { get; set; } = null!;

    public virtual EmbeddedData EmbeddedData { get; set; } = null!;
}
