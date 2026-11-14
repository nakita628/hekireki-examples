#nullable enable

namespace LangWatch.Models;

public partial class AnnotationQueueScores
{
    public string AnnotationQueueId { get; set; } = null!;

    public string AnnotationScoreId { get; set; } = null!;

    public virtual AnnotationScore AnnotationScore { get; set; } = null!;

    public virtual AnnotationQueue AnnotationQueue { get; set; } = null!;
}
