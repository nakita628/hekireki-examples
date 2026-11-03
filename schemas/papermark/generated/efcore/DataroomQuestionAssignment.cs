#nullable enable

using System;

namespace Papermark.Models;

public partial class DataroomQuestionAssignment
{
    public string Id { get; set; } = null!;

    public string QuestionId { get; set; } = null!;

    public virtual DataroomQuestion Question { get; set; } = null!;

    public string? LinkId { get; set; }

    public virtual Link? Link { get; set; }

    public string? GroupId { get; set; }

    public virtual ViewerGroup? Group { get; set; }

    public string? ViewerId { get; set; }

    public virtual Viewer? Viewer { get; set; }

    public string? Email { get; set; }

    public DateTime CreatedAt { get; set; }
}
