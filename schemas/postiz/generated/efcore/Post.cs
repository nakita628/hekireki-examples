#nullable enable

using System;
using System.Collections.Generic;

namespace Postiz.Models;

public partial class Post
{
    public string Id { get; set; } = null!;

    public State State { get; set; } = State.Queue;

    public DateTime PublishDate { get; set; }

    public string OrganizationId { get; set; } = null!;

    public string IntegrationId { get; set; } = null!;

    public string Content { get; set; } = null!;

    public int Delay { get; set; } = 0;

    public string Group { get; set; } = null!;

    public string? BatchId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? ParentPostId { get; set; }

    public string? ReleaseId { get; set; }

    public string? ReleaseURL { get; set; }

    public string? Settings { get; set; }

    public string? Image { get; set; }

    public string? SubmittedForOrderId { get; set; }

    public string? SubmittedForOrganizationId { get; set; }

    public ApprovedSubmitForOrder ApprovedSubmitForOrder { get; set; } = ApprovedSubmitForOrder.No;

    public CreationMethod CreationMethod { get; set; } = CreationMethod.Unknown;

    public string? LastMessageId { get; set; }

    public int? IntervalInDays { get; set; }

    public string? Error { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<Comments> Comments { get; set; } = new List<Comments>();

    public virtual ICollection<Errors> Errors { get; set; } = new List<Errors>();

    public virtual ICollection<PayoutProblems> PayoutProblems { get; set; } = new List<PayoutProblems>();

    public virtual Integration Integration { get; set; } = null!;

    public virtual Messages? LastMessage { get; set; }

    public virtual Organization Organization { get; set; } = null!;

    public virtual Post? ParentPost { get; set; }

    public virtual ICollection<Post> ChildrenPost { get; set; } = new List<Post>();

    public virtual Orders? SubmittedForOrder { get; set; }

    public virtual Organization? SubmittedForOrganization { get; set; }

    public virtual ICollection<TagsPosts> Tags { get; set; } = new List<TagsPosts>();
}
