#nullable enable

using System;
using System.Collections.Generic;

namespace Calcom.Models;

public partial class User
{
    public int Id { get; set; }

    public Guid Uuid { get; set; }

    public string? Username { get; set; }

    public string? Name { get; set; }

    public string Email { get; set; } = null!;

    public DateTime? EmailVerified { get; set; }

    public virtual UserPassword? Password { get; set; }

    public string? Bio { get; set; }

    public string? AvatarUrl { get; set; }

    public string TimeZone { get; set; } = "Europe/London";

    public virtual ICollection<TravelSchedule> TravelSchedules { get; set; } = new List<TravelSchedule>();

    public string WeekStart { get; set; } = "Sunday";

    public int BufferTime { get; set; } = 0;

    public bool HideBranding { get; set; } = false;

    public string? Theme { get; set; }

    public string? AppTheme { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? TrialEndsAt { get; set; }

    public DateTime? LastActiveAt { get; set; }

    public virtual ICollection<EventType> EventTypes { get; set; } = new List<EventType>();

    public virtual ICollection<Credential> Credentials { get; set; } = new List<Credential>();

    public virtual ICollection<Membership> Teams { get; set; } = new List<Membership>();

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    public int? DefaultScheduleId { get; set; }

    public virtual ICollection<SelectedCalendar> SelectedCalendars { get; set; } = new List<SelectedCalendar>();

    public bool CompletedOnboarding { get; set; } = false;

    public string? Locale { get; set; }

    public int? TimeFormat { get; set; } = 12;

    public string? TwoFactorSecret { get; set; }

    public bool TwoFactorEnabled { get; set; } = false;

    public string? BackupCodes { get; set; }

    public IdentityProvider IdentityProvider { get; set; } = IdentityProvider.Cal;

    public string? IdentityProviderId { get; set; }

    public virtual ICollection<Availability> Availability { get; set; } = new List<Availability>();

    public int? InvitedTo { get; set; }

    public virtual ICollection<Webhook> Webhooks { get; set; } = new List<Webhook>();

    public string? BrandColor { get; set; }

    public string? DarkBrandColor { get; set; }

    public virtual DestinationCalendar? DestinationCalendar { get; set; }

    public bool? AllowDynamicBooking { get; set; } = true;

    public bool? AllowSEOIndexing { get; set; } = true;

    public bool? ReceiveMonthlyDigestEmail { get; set; } = true;

    public bool? RequiresBookerEmailVerification { get; set; } = false;

    public string? Metadata { get; set; }

    public bool? Verified { get; set; } = false;

    public UserPermissionRole Role { get; set; } = UserPermissionRole.User;

    public virtual ICollection<ApiKey> ApiKeys { get; set; } = new List<ApiKey>();

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();

    public virtual ICollection<Feedback> Feedback { get; set; } = new List<Feedback>();

    public virtual ICollection<EventType> OwnedEventTypes { get; set; } = new List<EventType>();

    public virtual ICollection<VerifiedNumber> VerifiedNumbers { get; set; } = new List<VerifiedNumber>();

    public virtual ICollection<VerifiedEmail> VerifiedEmails { get; set; } = new List<VerifiedEmail>();

    public virtual ICollection<Host> Hosts { get; set; } = new List<Host>();

    public int? OrganizationId { get; set; }

    public virtual Team? Organization { get; set; }

    public virtual ICollection<AccessCode> AccessCodes { get; set; } = new List<AccessCode>();

    public virtual ICollection<OutOfOfficeEntry> BookingRedirects { get; set; } = new List<OutOfOfficeEntry>();

    public virtual ICollection<OutOfOfficeEntry> BookingRedirectsTo { get; set; } = new List<OutOfOfficeEntry>();

    public bool Locked { get; set; } = false;

    public virtual ICollection<PlatformOAuthClient> PlatformOAuthClients { get; set; } = new List<PlatformOAuthClient>();

    public virtual ICollection<AccessToken> AccessToken { get; set; } = new List<AccessToken>();

    public virtual ICollection<RefreshToken> RefreshToken { get; set; } = new List<RefreshToken>();

    public virtual ICollection<PlatformAuthorizationToken> PlatformAuthorizationToken { get; set; } = new List<PlatformAuthorizationToken>();

    public virtual ICollection<Profile> Profiles { get; set; } = new List<Profile>();

    public int? MovedToProfileId { get; set; }

    public virtual Profile? MovedToProfile { get; set; }

    public virtual ICollection<SecondaryEmail> SecondaryEmails { get; set; } = new List<SecondaryEmail>();

    public bool IsPlatformManaged { get; set; } = false;

    public virtual ICollection<OutOfOfficeReason> OutOfOfficeReasons { get; set; } = new List<OutOfOfficeReason>();

    public SMSLockState SmsLockState { get; set; } = SMSLockState.Unlocked;

    public bool SmsLockReviewedByAdmin { get; set; } = false;

    public virtual ICollection<NotificationsSubscriptions> NotificationsSubscriptions { get; set; } = new List<NotificationsSubscriptions>();

    public string? ReferralLinkId { get; set; }

    public virtual ICollection<UserFeatures> Features { get; set; } = new List<UserFeatures>();

    public virtual ICollection<Booking> ReassignedBookings { get; set; } = new List<Booking>();

    public virtual ICollection<AttributeToUser> CreatedAttributeToUsers { get; set; } = new List<AttributeToUser>();

    public virtual ICollection<AttributeToUser> UpdatedAttributeToUsers { get; set; } = new List<AttributeToUser>();

    public virtual ICollection<EventTypeTranslation> CreatedTranslations { get; set; } = new List<EventTypeTranslation>();

    public virtual ICollection<EventTypeTranslation> UpdatedTranslations { get; set; } = new List<EventTypeTranslation>();

    public virtual ICollection<BookingReport> ReportedBookings { get; set; } = new List<BookingReport>();

    public virtual ICollection<WrongAssignmentReport> WrongAssignmentReports { get; set; } = new List<WrongAssignmentReport>();

    public virtual ICollection<WrongAssignmentReport> ReviewedWrongAssignmentReports { get; set; } = new List<WrongAssignmentReport>();

    public virtual ICollection<BookingInternalNote> BookingInternalNote { get; set; } = new List<BookingInternalNote>();

    public CreationSource? CreationSource { get; set; }

    public virtual ICollection<OrganizationOnboarding> CreatedOrganizationOnboardings { get; set; } = new List<OrganizationOnboarding>();

    public virtual ICollection<FilterSegment> FilterSegments { get; set; } = new List<FilterSegment>();

    public virtual ICollection<UserFilterSegmentPreference> FilterSegmentPreferences { get; set; } = new List<UserFilterSegmentPreference>();

    public virtual CreditBalance? CreditBalance { get; set; }

    public virtual ICollection<CalAiPhoneNumber> CalAiPhoneNumbers { get; set; } = new List<CalAiPhoneNumber>();

    public virtual ICollection<Agent> Agents { get; set; } = new List<Agent>();

    public virtual ICollection<OAuthClient> OAuthClients { get; set; } = new List<OAuthClient>();

    public virtual UserHolidaySettings? HolidaySettings { get; set; }

    public bool AutoOptInFeatures { get; set; } = false;
}
