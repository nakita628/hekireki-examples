#nullable enable

using System;
using System.Collections.Generic;

namespace Papermark.Models;

public partial class Dataroom
{
    public string Id { get; set; } = null!;

    public string PId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? InternalName { get; set; }

    public string? Description { get; set; }

    public string TeamId { get; set; } = null!;

    public virtual Team Team { get; set; } = null!;

    public virtual ICollection<DataroomDocument> Documents { get; set; } = new List<DataroomDocument>();

    public virtual ICollection<DataroomFolder> Folders { get; set; } = new List<DataroomFolder>();

    public virtual ICollection<Link> Links { get; set; } = new List<Link>();

    public virtual ICollection<View> Views { get; set; } = new List<View>();

    public virtual ICollection<Viewer> Viewers { get; set; } = new List<Viewer>();

    public virtual ICollection<ViewerGroup> ViewerGroups { get; set; } = new List<ViewerGroup>();

    public virtual DataroomBrand? Brand { get; set; }

    public string? BrandId { get; set; }

    public virtual Brand? BaseBrand { get; set; }

    public virtual ICollection<UserDataroom> UserDatarooms { get; set; } = new List<UserDataroom>();

    public virtual ICollection<PermissionGroup> PermissionGroups { get; set; } = new List<PermissionGroup>();

    public bool ConversationsEnabled { get; set; } = false;

    public virtual ICollection<Conversation> Conversations { get; set; } = new List<Conversation>();

    public virtual ICollection<DataroomQuestion> Questions { get; set; } = new List<DataroomQuestion>();

    public virtual ICollection<DataroomFaqItem> FaqItems { get; set; } = new List<DataroomFaqItem>();

    public bool AgentsEnabled { get; set; } = false;

    public string? VectorStoreId { get; set; }

    public virtual ICollection<Chat> Chats { get; set; } = new List<Chat>();

    public virtual ICollection<DocumentUpload> UploadedDocuments { get; set; } = new List<DocumentUpload>();

    public virtual ICollection<TagItem> Tags { get; set; } = new List<TagItem>();

    public bool RequestListEnabled { get; set; } = false;

    public virtual ICollection<TaskList> TaskLists { get; set; } = new List<TaskList>();

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();

    public bool EnableChangeNotifications { get; set; } = false;

    public bool EnableVisitorUploadChangeNotifications { get; set; } = false;

    public DefaultPermissionStrategy DefaultPermissionStrategy { get; set; } = DefaultPermissionStrategy.InheritFromParent;

    public bool DefaultShowBanner { get; set; } = true;

    public DefaultPermissionStrategy DefaultGroupPermissionStrategy { get; set; } = DefaultPermissionStrategy.InheritFromParent;

    public RootItemAccess DefaultRootItemAccess { get; set; } = RootItemAccess.ViewOnly;

    public RootItemAccess DefaultGroupRootItemAccess { get; set; } = RootItemAccess.ViewOnly;

    public bool AllowBulkDownload { get; set; } = true;

    public bool ShowLastUpdated { get; set; } = true;

    public bool IntroductionEnabled { get; set; } = false;

    public string? IntroductionContent { get; set; }

    public bool IsFrozen { get; set; } = false;

    public DateTime? FrozenAt { get; set; }

    public string? FrozenBy { get; set; }

    public virtual User? FrozenByUser { get; set; }

    public string? FreezeArchiveUrl { get; set; }

    public string? FreezeArchiveHash { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
