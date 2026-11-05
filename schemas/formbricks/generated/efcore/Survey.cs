#nullable enable

using System;
using System.Collections.Generic;

namespace Formbricks.Models;

public partial class Survey
{
    public string HiddenFields { get; set; } = "{\"enabled\": false}";

    public string Variables { get; set; } = "[]";

    public bool OwnershipInitialized { get; set; } = false;

    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Name { get; set; } = null!;

    public string? RedirectUrl { get; set; }

    public SurveyType Type { get; set; } = SurveyType.App;

    public virtual Workspace Workspace { get; set; } = null!;

    public string WorkspaceId { get; set; } = null!;

    public virtual User? Creator { get; set; }

    public string? CreatedBy { get; set; }

    public SurveyVisibility Visibility { get; set; } = SurveyVisibility.Workspace;

    public virtual User? Owner { get; set; }

    public string? OwnerId { get; set; }

    public int VisibilityVersion { get; set; } = 0;

    public int VisibilityProjectedVersion { get; set; } = 0;

    public bool VisibilityPending { get; set; } = false;

    public DateTime? VisibilityChangedAt { get; set; }

    public string? VisibilityChangedById { get; set; }

    public SurveyStatus Status { get; set; } = SurveyStatus.Draft;

    public string WelcomeCard { get; set; } = "{\"enabled\": false}";

    public string Questions { get; set; } = "[]";

    public List<string>? Blocks { get; set; } = new List<string>();

    public List<string>? Endings { get; set; } = new List<string>();

    public virtual ICollection<Response> Responses { get; set; } = new List<Response>();

    public virtual ICollection<SurveyQuota> Quotas { get; set; } = new List<SurveyQuota>();

    public DisplayOptions DisplayOption { get; set; } = DisplayOptions.DisplayOnce;

    public int? RecontactDays { get; set; }

    public int? DisplayLimit { get; set; }

    public virtual ICollection<SurveyTrigger> Triggers { get; set; } = new List<SurveyTrigger>();

    public string? InlineTriggers { get; set; }

    public virtual ICollection<SurveyAttributeFilter> AttributeFilters { get; set; } = new List<SurveyAttributeFilter>();

    public virtual ICollection<Display> Displays { get; set; } = new List<Display>();

    public int? AutoClose { get; set; }

    public bool KeepEndingCardOpen { get; set; } = false;

    public int? AutoComplete { get; set; }

    public int Delay { get; set; } = 0;

    public DateTime? PublishOn { get; set; }

    public DateTime? CloseOn { get; set; }

    public DateTime? ArchivedAt { get; set; }

    public string? SurveyClosedMessage { get; set; }

    public string? SegmentId { get; set; }

    public virtual Segment? Segment { get; set; }

    public string? WorkspaceOverwrites { get; set; }

    public string? Styling { get; set; }

    public string? SingleUse { get; set; } = "{\"enabled\": false, \"isEncrypted\": true}";

    public bool IsVerifyEmailEnabled { get; set; } = false;

    public bool IsBackButtonHidden { get; set; } = false;

    public bool IsAutoProgressingEnabled { get; set; } = false;

    public bool IsCaptureIpEnabled { get; set; } = false;

    public bool IsAnonymizeResponsesEnabled { get; set; } = false;

    public string? Pin { get; set; }

    public decimal? DisplayPercentage { get; set; }

    public virtual ICollection<SurveyLanguage> Languages { get; set; } = new List<SurveyLanguage>();

    public virtual ICollection<EmbeddedData> EmbeddedData { get; set; } = new List<EmbeddedData>();

    public virtual ICollection<SurveyEmbeddedData> EmbeddedDataLinks { get; set; } = new List<SurveyEmbeddedData>();

    public bool? ShowLanguageSwitch { get; set; }

    public bool? AutoSelectLanguage { get; set; }

    public virtual ICollection<SurveyFollowUp> FollowUps { get; set; } = new List<SurveyFollowUp>();

    public string? Recaptcha { get; set; } = "{\"enabled\": false, \"threshold\":0.1}";

    public string Metadata { get; set; } = "{}";

    public virtual ICollection<FeedbackSourceFormbricksMapping> FeedbackSourceMappings { get; set; } = new List<FeedbackSourceFormbricksMapping>();

    public virtual ICollection<RetentionExemption> RetentionExemptions { get; set; } = new List<RetentionExemption>();

    public virtual ICollection<RetentionNotice> RetentionNotices { get; set; } = new List<RetentionNotice>();

    public virtual RetentionSurveyRestore? RetentionRestore { get; set; }

    public string? Slug { get; set; }

    public string? CustomHeadScripts { get; set; }

    public SurveyScriptMode? CustomHeadScriptsMode { get; set; } = SurveyScriptMode.Add;

    public string? CustomCss { get; set; }

    public virtual ICollection<Creative> Creatives { get; set; } = new List<Creative>();

    public virtual ICollection<AttachmentExport> AttachmentExports { get; set; } = new List<AttachmentExport>();

    public string? ShowConditions { get; set; }
}
