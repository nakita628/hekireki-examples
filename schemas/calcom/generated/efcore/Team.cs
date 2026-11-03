#nullable enable

using System;
using System.Collections.Generic;

namespace Calcom.Models;

public partial class Team
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Slug { get; set; }

    public string? LogoUrl { get; set; }

    public string? CalVideoLogo { get; set; }

    public string? AppLogo { get; set; }

    public string? AppIconLogo { get; set; }

    public string? Bio { get; set; }

    public bool HideBranding { get; set; } = false;

    public bool HideTeamProfileLink { get; set; } = false;

    public bool IsPrivate { get; set; } = false;

    public bool HideBookATeamMember { get; set; } = false;

    public virtual ICollection<Membership> Members { get; set; } = new List<Membership>();

    public virtual ICollection<EventType> EventTypes { get; set; } = new List<EventType>();

    public DateTime CreatedAt { get; set; }

    public string? Metadata { get; set; }

    public string? Theme { get; set; }

    public RRResetInterval? RrResetInterval { get; set; } = RRResetInterval.Month;

    public RRTimestampBasis RrTimestampBasis { get; set; } = RRTimestampBasis.CreatedAt;

    public string? BrandColor { get; set; }

    public string? DarkBrandColor { get; set; }

    public virtual ICollection<VerifiedNumber> VerifiedNumbers { get; set; } = new List<VerifiedNumber>();

    public virtual ICollection<VerifiedEmail> VerifiedEmails { get; set; } = new List<VerifiedEmail>();

    public string? BannerUrl { get; set; }

    public int? ParentId { get; set; }

    public virtual Team? Parent { get; set; }

    public virtual ICollection<Team> Children { get; set; } = new List<Team>();

    public virtual ICollection<User> OrgUsers { get; set; } = new List<User>();

    public virtual ICollection<VerificationToken> InviteTokens { get; set; } = new List<VerificationToken>();

    public virtual ICollection<Webhook> Webhooks { get; set; } = new List<Webhook>();

    public int? TimeFormat { get; set; }

    public string TimeZone { get; set; } = "Europe/London";

    public string WeekStart { get; set; } = "Sunday";

    public virtual ICollection<ApiKey> ApiKeys { get; set; } = new List<ApiKey>();

    public virtual ICollection<Credential> Credentials { get; set; } = new List<Credential>();

    public virtual ICollection<AccessCode> AccessCodes { get; set; } = new List<AccessCode>();

    public bool IsOrganization { get; set; } = false;

    public virtual OrganizationSettings? OrganizationSettings { get; set; }

    public virtual ICollection<InstantMeetingToken> InstantMeetingTokens { get; set; } = new List<InstantMeetingToken>();

    public virtual ICollection<Profile> OrgProfiles { get; set; } = new List<Profile>();

    public bool PendingPayment { get; set; } = false;

    public virtual ICollection<DSyncTeamGroupMapping> DsyncTeamGroupMapping { get; set; } = new List<DSyncTeamGroupMapping>();

    public bool IsPlatform { get; set; } = false;

    public virtual ICollection<PlatformOAuthClient> PlatformOAuthClient { get; set; } = new List<PlatformOAuthClient>();

    public virtual PlatformOAuthClient? CreatedByOAuthClient { get; set; }

    public string? CreatedByOAuthClientId { get; set; }

    public SMSLockState SmsLockState { get; set; } = SMSLockState.Unlocked;

    public virtual PlatformBilling? PlatformBilling { get; set; }

    public virtual ICollection<Attribute> Attributes { get; set; } = new List<Attribute>();

    public bool SmsLockReviewedByAdmin { get; set; } = false;

    public virtual ICollection<DelegationCredential> DelegationCredentials { get; set; } = new List<DelegationCredential>();

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();

    public virtual ICollection<CalAiPhoneNumber> CalAiPhoneNumbers { get; set; } = new List<CalAiPhoneNumber>();

    public virtual ICollection<Agent> Agents { get; set; } = new List<Agent>();

    public virtual ICollection<BookingReport> BookingReports { get; set; } = new List<BookingReport>();

    public virtual ICollection<WrongAssignmentReport> WrongAssignmentReports { get; set; } = new List<WrongAssignmentReport>();

    public virtual ICollection<TeamFeatures> Features { get; set; } = new List<TeamFeatures>();

    public string? BookingLimits { get; set; }

    public bool IncludeManagedEventsInLimits { get; set; } = false;

    public virtual ICollection<InternalNotePreset> InternalNotePresets { get; set; } = new List<InternalNotePreset>();

    public virtual CreditBalance? CreditBalance { get; set; }

    public virtual OrganizationOnboarding? OrganizationOnboarding { get; set; }

    public virtual ManagedOrganization? ManagedOrganization { get; set; }

    public virtual ICollection<ManagedOrganization> ManagedOrganizations { get; set; } = new List<ManagedOrganization>();

    public virtual ICollection<FilterSegment> FilterSegments { get; set; } = new List<FilterSegment>();

    public virtual TeamBilling? TeamBilling { get; set; }

    public virtual OrganizationBilling? OrganizationBilling { get; set; }

    public virtual ICollection<IntegrationAttributeSync> IntegrationAttributeSyncs { get; set; } = new List<IntegrationAttributeSync>();

    public bool AutoOptInFeatures { get; set; } = false;

    public virtual ICollection<SeatChangeLog> SeatChangeLogs { get; set; } = new List<SeatChangeLog>();

    public virtual ICollection<MonthlyProration> MonthlyProrations { get; set; } = new List<MonthlyProration>();
}
