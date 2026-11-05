#nullable enable

using System;
using System.Collections.Generic;

namespace InboxZero.Models;

public partial class User
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? Name { get; set; }

    public string Email { get; set; } = null!;

    public bool? EmailVerified { get; set; } = false;

    public string? Image { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();

    public DateTime? CompletedOnboardingAt { get; set; }

    public SignupIntent? SignupIntent { get; set; }

    public string? OnboardingAnswers { get; set; }

    public DateTime? LastLogin { get; set; }

    public string? Utms { get; set; }

    public string? ErrorMessages { get; set; }

    public DateTime? AnnouncementDismissedAt { get; set; }

    public List<string>? DismissedHints { get; set; } = new List<string>();

    public List<string>? SurveyFeatures { get; set; }

    public string? SurveyRole { get; set; }

    public string? SurveyGoal { get; set; }

    public int? SurveyCompanySize { get; set; }

    public string? SurveySource { get; set; }

    public string? SurveyImprovements { get; set; }

    public bool McpServerEnabled { get; set; } = false;

    public int McpTokenVersion { get; set; } = 0;

    public bool EmailOtpEnabled { get; set; } = false;

    public int EmailOtpVersion { get; set; } = 0;

    public string? AiProvider { get; set; }

    public string? AiModel { get; set; }

    public string? AiApiKey { get; set; }

    public bool? DecisionModelEnabled { get; set; }

    public string? WebhookSecret { get; set; }

    public string? ReferralCode { get; set; }

    public string? PremiumId { get; set; }

    public virtual Premium? Premium { get; set; }

    public string? PremiumAdminId { get; set; }

    public virtual Premium? PremiumAdmin { get; set; }

    public virtual ICollection<ApiKey> ApiKeys { get; set; } = new List<ApiKey>();

    public virtual ICollection<EmailAccount> EmailAccounts { get; set; } = new List<EmailAccount>();

    public virtual ICollection<Referral> ReferralsMade { get; set; } = new List<Referral>();

    public virtual Referral? ReferralReceived { get; set; }

    public virtual ICollection<MobilePushToken> MobilePushTokens { get; set; } = new List<MobilePushToken>();

    public virtual ICollection<OauthClient> OauthClientRecords { get; set; } = new List<OauthClient>();

    public virtual ICollection<OauthRefreshToken> OauthRefreshTokenRecords { get; set; } = new List<OauthRefreshToken>();

    public virtual ICollection<OauthAccessToken> OauthAccessTokenRecords { get; set; } = new List<OauthAccessToken>();

    public virtual ICollection<OauthConsent> OauthConsentRecords { get; set; } = new List<OauthConsent>();

    public virtual ICollection<SsoProvider> SsoProviders { get; set; } = new List<SsoProvider>();

    public bool ScimAccessDisabled { get; set; } = false;

    public virtual ICollection<ScimIdentityTombstone> ScimIdentityTombstones { get; set; } = new List<ScimIdentityTombstone>();

    public virtual ScimSubject? ScimSubject { get; set; }

    public virtual ICollection<ScimUser> ScimUsers { get; set; } = new List<ScimUser>();

    public virtual ICollection<ScimProjectionGrant> ScimProjectionGrants { get; set; } = new List<ScimProjectionGrant>();

    public virtual ICollection<ScimIdentityLink> ScimIdentityLinks { get; set; } = new List<ScimIdentityLink>();
}
