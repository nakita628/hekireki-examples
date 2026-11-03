#nullable enable

using System;
using System.Collections.Generic;

namespace Papermark.Models;

public partial class ViewerGroup
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<ViewerGroupMembership> Members { get; set; } = new List<ViewerGroupMembership>();

    public List<string>? Domains { get; set; }

    public virtual ICollection<Link> Links { get; set; } = new List<Link>();

    public virtual ICollection<ViewerGroupAccessControls> AccessControls { get; set; } = new List<ViewerGroupAccessControls>();

    public bool AllowAll { get; set; } = false;

    public string DataroomId { get; set; } = null!;

    public virtual Dataroom Dataroom { get; set; } = null!;

    public string TeamId { get; set; } = null!;

    public virtual Team Team { get; set; } = null!;

    public virtual ICollection<View> Views { get; set; } = new List<View>();

    public virtual ICollection<Conversation> Conversations { get; set; } = new List<Conversation>();

    public virtual ICollection<ViewerInvitation> Invitations { get; set; } = new List<ViewerInvitation>();

    public virtual ICollection<DataroomQuestionAssignment> QuestionAssignments { get; set; } = new List<DataroomQuestionAssignment>();

    public virtual ICollection<TaskAssignment> TaskAssignments { get; set; } = new List<TaskAssignment>();

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
