#nullable enable

using System;

namespace Papermark.Models;

public partial class ViewerInvitation
{
    public string Id { get; set; } = null!;

    public string ViewerId { get; set; } = null!;

    public virtual Viewer Viewer { get; set; } = null!;

    public string LinkId { get; set; } = null!;

    public virtual Link Link { get; set; } = null!;

    public string? GroupId { get; set; }

    public virtual ViewerGroup? Group { get; set; }

    public string InvitedBy { get; set; } = null!;

    public string? CustomMessage { get; set; }

    public DateTime SentAt { get; set; }

    public InvitationStatus Status { get; set; } = InvitationStatus.Sent;

    public DateTime CreatedAt { get; set; }
}
