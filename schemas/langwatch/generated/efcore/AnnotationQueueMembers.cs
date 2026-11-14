#nullable enable

namespace LangWatch.Models;

public partial class AnnotationQueueMembers
{
    public string AnnotationQueueId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public virtual AnnotationQueue AnnotationQueue { get; set; } = null!;
}
