#nullable enable

using System;
using System.Collections.Generic;

namespace Papermark.Models;

public partial class Link
{
    public string Id { get; set; } = null!;

    public virtual Document? Document { get; set; }

    public string? DocumentId { get; set; }

    public virtual Dataroom? Dataroom { get; set; }

    public string? DataroomId { get; set; }

    public LinkType LinkType { get; set; } = LinkType.DocumentLink;

    public string? Url { get; set; }

    public string? Name { get; set; }

    public string? Slug { get; set; }

    public DateTime? ExpiresAt { get; set; }

    public string? Password { get; set; }

    public List<string>? AllowList { get; set; }

    public List<string>? DenyList { get; set; }

    public bool EmailProtected { get; set; } = true;

    public bool EmailAuthenticated { get; set; } = false;

    public bool? AllowDownload { get; set; } = false;

    public bool IsArchived { get; set; } = false;

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<View> Views { get; set; } = new List<View>();

    public virtual Domain? Domain { get; set; }

    public string? DomainId { get; set; }

    public string? DomainSlug { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool? EnableNotification { get; set; } = true;

    public bool? EnableFeedback { get; set; } = false;

    public bool? EnableQuestion { get; set; } = false;

    public bool? EnableScreenshotProtection { get; set; } = false;

    public bool? EnableConfidentialView { get; set; } = false;

    public virtual Feedback? Feedback { get; set; }

    public bool? EnableAgreement { get; set; } = false;

    public virtual Agreement? Agreement { get; set; }

    public string? AgreementId { get; set; }

    public bool? ShowBanner { get; set; } = false;

    public bool? EnableWatermark { get; set; } = false;

    public string? WatermarkConfig { get; set; }

    public LinkAudienceType AudienceType { get; set; } = LinkAudienceType.General;

    public string? GroupId { get; set; }

    public virtual ViewerGroup? Group { get; set; }

    public string? PermissionGroupId { get; set; }

    public virtual PermissionGroup? PermissionGroup { get; set; }

    public string? MetaTitle { get; set; }

    public string? MetaDescription { get; set; }

    public string? MetaImage { get; set; }

    public string? MetaFavicon { get; set; }

    public bool? EnableCustomMetatag { get; set; } = false;

    public string? WelcomeMessage { get; set; }

    public bool EnableConversation { get; set; } = false;

    public virtual ICollection<Conversation> Conversations { get; set; } = new List<Conversation>();

    public virtual ICollection<DataroomQuestionAssignment> QuestionAssignments { get; set; } = new List<DataroomQuestionAssignment>();

    public virtual ICollection<DataroomFaqItem> DataroomFaqItems { get; set; } = new List<DataroomFaqItem>();

    public bool? EnableAIAgents { get; set; } = false;

    public virtual ICollection<Chat> Chats { get; set; } = new List<Chat>();

    public bool? EnableUpload { get; set; } = false;

    public bool? IsFileRequestOnly { get; set; } = false;

    public string? UploadFolderId { get; set; }

    public List<string>? UploadFolderIds { get; set; } = new List<string>();

    public bool? EnableIndexFile { get; set; } = false;

    public virtual ICollection<DocumentUpload> UploadedDocuments { get; set; } = new List<DocumentUpload>();

    public string? TeamId { get; set; }

    public virtual Team? Team { get; set; }

    public string? BrandId { get; set; }

    public virtual Brand? Brand { get; set; }

    public string? OwnerId { get; set; }

    public virtual User? Owner { get; set; }

    public virtual ICollection<CustomField> CustomFields { get; set; } = new List<CustomField>();

    public virtual ICollection<TagItem> Tags { get; set; } = new List<TagItem>();

    public virtual ICollection<ViewerInvitation> ViewerInvitations { get; set; } = new List<ViewerInvitation>();

    public virtual ICollection<LinkVisitorGroup> VisitorGroups { get; set; } = new List<LinkVisitorGroup>();

    public virtual ICollection<TaskAssignment> TaskAssignments { get; set; } = new List<TaskAssignment>();

    public virtual Workflow? Workflow { get; set; }
}
