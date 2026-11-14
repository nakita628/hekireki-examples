package model

import (
	"database/sql/driver"
	"fmt"
	"strconv"
	"time"

	"github.com/lucsky/cuid"
	"gorm.io/datatypes"
	"gorm.io/gorm"
	"gorm.io/gorm/schema"
)

var NamingStrategy = schema.NamingStrategy{SingularTable: true, NoLowerCase: true}

type Account struct {
	Password              *string   `gorm:"column:password" json:"password"`
	ID                    string    `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt             DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt             DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	UserID                string    `gorm:"column:userId;index:Account_userId_idx;not null" json:"userId"`
	Provider              string    `gorm:"column:provider;uniqueIndex:Account_provider_providerAccountId_key;not null" json:"provider"`
	Type                  *string   `gorm:"column:type;default:'oidc';not null" json:"type"`
	ProviderAccountID     string    `gorm:"column:providerAccountId;uniqueIndex:Account_provider_providerAccountId_key;not null" json:"providerAccountId"`
	RefreshToken          *string   `gorm:"column:refresh_token;type:text" json:"refresh_token"`
	RefreshTokenExpiresAt *DateTime `gorm:"column:refreshTokenExpiresAt" json:"refreshTokenExpiresAt"`
	AccessToken           *string   `gorm:"column:access_token;type:text" json:"access_token"`
	ExpiresAt             *DateTime `gorm:"column:expires_at" json:"expires_at"`
	DisconnectedAt        *DateTime `gorm:"column:disconnectedAt" json:"disconnectedAt"`
	TokenType             *string   `gorm:"column:token_type" json:"token_type"`
	Scope                 *string   `gorm:"column:scope" json:"scope"`
	IDToken               *string   `gorm:"column:id_token;type:text" json:"id_token"`
	SessionState          *string   `gorm:"column:session_state" json:"session_state"`
	User                  *User
	EmailAccount          *EmailAccount `gorm:"foreignKey:AccountID;constraint:OnDelete:CASCADE"`
}

func (Account) TableName() string {
	return "Account"
}

func (m *Account) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	if m.ExpiresAt == nil {
		m.ExpiresAt = &DateTime{Time: now}
	}
	return nil
}

func (*Account) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Session struct {
	ID                       string   `gorm:"column:id;primaryKey" json:"id"`
	SessionToken             string   `gorm:"column:sessionToken;uniqueIndex;not null" json:"sessionToken"`
	UserID                   string   `gorm:"column:userId;index:Session_userId_idx;not null" json:"userId"`
	Expires                  DateTime `gorm:"column:expires;not null" json:"expires"`
	CreatedAt                DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt                DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	EmailOtp                 bool     `gorm:"column:emailOtp;default:false;not null" json:"emailOtp"`
	EmailOtpVersion          int      `gorm:"column:emailOtpVersion;default:0;not null" json:"emailOtpVersion"`
	IPAddress                *string  `gorm:"column:ipAddress" json:"ipAddress"`
	UserAgent                *string  `gorm:"column:userAgent" json:"userAgent"`
	ActiveOrganizationID     *string  `gorm:"column:activeOrganizationId;index:Session_activeOrganizationId_idx" json:"activeOrganizationId"`
	User                     *User
	ActiveOrganization       *Organization       `gorm:"foreignKey:ActiveOrganizationID"`
	OauthRefreshTokenRecords []OauthRefreshToken `gorm:"foreignKey:SessionID;constraint:OnDelete:SET NULL"`
	OauthAccessTokenRecords  []OauthAccessToken  `gorm:"foreignKey:SessionID;constraint:OnDelete:SET NULL"`
}

func (Session) TableName() string {
	return "Session"
}

func (m *Session) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*Session) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type User struct {
	ID                       string                  `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt                DateTime                `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt                DateTime                `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Name                     *string                 `gorm:"column:name" json:"name"`
	Email                    string                  `gorm:"column:email;uniqueIndex;not null" json:"email"`
	EmailVerified            *bool                   `gorm:"column:emailVerified;default:false" json:"emailVerified"`
	Image                    *string                 `gorm:"column:image" json:"image"`
	CompletedOnboardingAt    *DateTime               `gorm:"column:completedOnboardingAt" json:"completedOnboardingAt"`
	SignupIntent             *string                 `gorm:"column:signupIntent" json:"signupIntent"`
	OnboardingAnswers        datatypes.JSON          `gorm:"column:onboardingAnswers" json:"onboardingAnswers"`
	LastLogin                *DateTime               `gorm:"column:lastLogin" json:"lastLogin"`
	Utms                     datatypes.JSON          `gorm:"column:utms" json:"utms"`
	ErrorMessages            datatypes.JSON          `gorm:"column:errorMessages" json:"errorMessages"`
	AnnouncementDismissedAt  *DateTime               `gorm:"column:announcementDismissedAt" json:"announcementDismissedAt"`
	DismissedHints           []string                `gorm:"column:dismissedHints;serializer:json;not null" json:"dismissedHints"`
	SurveyFeatures           []string                `gorm:"column:surveyFeatures;serializer:json;not null" json:"surveyFeatures"`
	SurveyRole               *string                 `gorm:"column:surveyRole" json:"surveyRole"`
	SurveyGoal               *string                 `gorm:"column:surveyGoal" json:"surveyGoal"`
	SurveyCompanySize        *int                    `gorm:"column:surveyCompanySize" json:"surveyCompanySize"`
	SurveySource             *string                 `gorm:"column:surveySource" json:"surveySource"`
	SurveyImprovements       *string                 `gorm:"column:surveyImprovements" json:"surveyImprovements"`
	McpServerEnabled         bool                    `gorm:"column:mcpServerEnabled;default:false;not null" json:"mcpServerEnabled"`
	McpTokenVersion          int                     `gorm:"column:mcpTokenVersion;default:0;not null" json:"mcpTokenVersion"`
	EmailOtpEnabled          bool                    `gorm:"column:emailOtpEnabled;default:false;not null" json:"emailOtpEnabled"`
	EmailOtpVersion          int                     `gorm:"column:emailOtpVersion;default:0;not null" json:"emailOtpVersion"`
	AiProvider               *string                 `gorm:"column:aiProvider" json:"aiProvider"`
	AiModel                  *string                 `gorm:"column:aiModel" json:"aiModel"`
	AiAPIKey                 *string                 `gorm:"column:aiApiKey" json:"aiApiKey"`
	DecisionModelEnabled     *bool                   `gorm:"column:decisionModelEnabled" json:"decisionModelEnabled"`
	WebhookSecret            *string                 `gorm:"column:webhookSecret" json:"webhookSecret"`
	ReferralCode             *string                 `gorm:"column:referralCode;uniqueIndex" json:"referralCode"`
	PremiumID                *string                 `gorm:"column:premiumId" json:"premiumId"`
	PremiumAdminID           *string                 `gorm:"column:premiumAdminId" json:"premiumAdminId"`
	ScimAccessDisabled       bool                    `gorm:"column:scimAccessDisabled;default:false;not null" json:"scimAccessDisabled"`
	Premium                  *Premium                `gorm:"foreignKey:PremiumID"`
	PremiumAdmin             *Premium                `gorm:"foreignKey:PremiumAdminID"`
	Accounts                 []Account               `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	Sessions                 []Session               `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	APIKeys                  []APIKey                `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	EmailAccounts            []EmailAccount          `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	ReferralsMade            []Referral              `gorm:"foreignKey:ReferrerUserID;constraint:OnDelete:CASCADE"`
	MobilePushTokens         []MobilePushToken       `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	OauthClientRecords       []OauthClient           `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	OauthRefreshTokenRecords []OauthRefreshToken     `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	OauthAccessTokenRecords  []OauthAccessToken      `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	OauthConsentRecords      []OauthConsent          `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	SsoProviders             []SsoProvider           `gorm:"foreignKey:UserID;constraint:OnDelete:SET NULL"`
	ScimIdentityTombstones   []ScimIdentityTombstone `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	ScimUsers                []ScimUser              `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	ScimProjectionGrants     []ScimProjectionGrant   `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	ScimIdentityLinks        []ScimIdentityLink      `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	ReferralReceived         *Referral               `gorm:"foreignKey:ReferredUserID;constraint:OnDelete:CASCADE"`
	ScimSubject              *ScimSubject            `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
}

func (User) TableName() string {
	return "User"
}

func (m *User) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*User) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type EmailAccount struct {
	ID                                  string         `gorm:"column:id;primaryKey" json:"id"`
	Email                               string         `gorm:"column:email;uniqueIndex;not null" json:"email"`
	CreatedAt                           DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt                           DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Name                                *string        `gorm:"column:name" json:"name"`
	Image                               *string        `gorm:"column:image" json:"image"`
	SortOrder                           *int           `gorm:"column:sortOrder" json:"sortOrder"`
	About                               *string        `gorm:"column:about" json:"about"`
	WritingStyle                        *string        `gorm:"column:writingStyle" json:"writingStyle"`
	Signature                           *string        `gorm:"column:signature" json:"signature"`
	SentSignatureScan                   datatypes.JSON `gorm:"column:sentSignatureScan" json:"sentSignatureScan"`
	SentSignatureScannedAt              *DateTime      `gorm:"column:sentSignatureScannedAt" json:"sentSignatureScannedAt"`
	IncludeReferralSignature            bool           `gorm:"column:includeReferralSignature;default:false;not null" json:"includeReferralSignature"`
	IncludeSentWithSignature            *bool          `gorm:"column:includeSentWithSignature;default:true;not null" json:"includeSentWithSignature"`
	SentMessageOpenTrackingEnabled      *bool          `gorm:"column:sentMessageOpenTrackingEnabled;default:true;not null" json:"sentMessageOpenTrackingEnabled"`
	WatchEmailsExpirationDate           *DateTime      `gorm:"column:watchEmailsExpirationDate" json:"watchEmailsExpirationDate"`
	WatchEmailsSubscriptionID           *string        `gorm:"column:watchEmailsSubscriptionId;index:EmailAccount_watchEmailsSubscriptionId_idx" json:"watchEmailsSubscriptionId"`
	WatchEmailsSubscriptionHistory      datatypes.JSON `gorm:"column:watchEmailsSubscriptionHistory;index:EmailAccount_watchEmailsSubscriptionHistory_idx" json:"watchEmailsSubscriptionHistory"`
	LastSyncedHistoryID                 *string        `gorm:"column:lastSyncedHistoryId" json:"lastSyncedHistoryId"`
	BehaviorProfile                     datatypes.JSON `gorm:"column:behaviorProfile" json:"behaviorProfile"`
	PersonaAnalysis                     datatypes.JSON `gorm:"column:personaAnalysis" json:"personaAnalysis"`
	Role                                *string        `gorm:"column:role" json:"role"`
	Timezone                            *string        `gorm:"column:timezone" json:"timezone"`
	CalendarBookingLink                 *string        `gorm:"column:calendarBookingLink" json:"calendarBookingLink"`
	DraftDocsUrls                       []string       `gorm:"column:draftDocsUrls;serializer:json;not null" json:"draftDocsUrls"`
	StatsEmailFrequency                 string         `gorm:"column:statsEmailFrequency;default:'WEEKLY';not null" json:"statsEmailFrequency"`
	SummaryEmailFrequency               string         `gorm:"column:summaryEmailFrequency;default:'WEEKLY';not null" json:"summaryEmailFrequency"`
	LastSummaryEmailAt                  *DateTime      `gorm:"column:lastSummaryEmailAt;index:EmailAccount_lastSummaryEmailAt_idx" json:"lastSummaryEmailAt"`
	LastInboxHealthEmailAt              *DateTime      `gorm:"column:lastInboxHealthEmailAt;index:EmailAccount_lastInboxHealthEmailAt_idx" json:"lastInboxHealthEmailAt"`
	ColdEmailBlocker                    *string        `gorm:"column:coldEmailBlocker" json:"coldEmailBlocker"`
	ColdEmailDigest                     bool           `gorm:"column:coldEmailDigest;default:false;not null" json:"coldEmailDigest"`
	ColdEmailPrompt                     *string        `gorm:"column:coldEmailPrompt" json:"coldEmailPrompt"`
	RulesPrompt                         *string        `gorm:"column:rulesPrompt" json:"rulesPrompt"`
	AutoCategorizeSenders               bool           `gorm:"column:autoCategorizeSenders;default:false;not null" json:"autoCategorizeSenders"`
	MultiRuleSelectionEnabled           bool           `gorm:"column:multiRuleSelectionEnabled;default:false;not null" json:"multiRuleSelectionEnabled"`
	SensitiveDataPolicy                 *string        `gorm:"column:sensitiveDataPolicy" json:"sensitiveDataPolicy"`
	DraftReplyConfidence                string         `gorm:"column:draftReplyConfidence;default:'ALL_EMAILS';not null" json:"draftReplyConfidence"`
	DraftCleanupDays                    *int           `gorm:"column:draftCleanupDays;default:14" json:"draftCleanupDays"`
	AllowHiddenAiDraftLinks             bool           `gorm:"column:allowHiddenAiDraftLinks;default:false;not null" json:"allowHiddenAiDraftLinks"`
	RulesRevision                       int            `gorm:"column:rulesRevision;default:0;not null" json:"rulesRevision"`
	MailLayout                          string         `gorm:"column:mailLayout;default:'LIST';not null" json:"mailLayout"`
	MailExpandedPreview                 bool           `gorm:"column:mailExpandedPreview;default:false;not null" json:"mailExpandedPreview"`
	MailBundleInbox                     *bool          `gorm:"column:mailBundleInbox" json:"mailBundleInbox"`
	MailDefaultSplitID                  *string        `gorm:"column:mailDefaultSplitId" json:"mailDefaultSplitId"`
	MailSetup                           string         `gorm:"column:mailSetup;default:'PENDING';not null" json:"mailSetup"`
	IncludeInAllAccounts                *bool          `gorm:"column:includeInAllAccounts;default:true;not null" json:"includeInAllAccounts"`
	MeetingBriefingsEnabled             bool           `gorm:"column:meetingBriefingsEnabled;default:false;not null" json:"meetingBriefingsEnabled"`
	MeetingBriefingsMinutesBefore       *int           `gorm:"column:meetingBriefingsMinutesBefore;default:240;not null" json:"meetingBriefingsMinutesBefore"`
	MeetingBriefsSendEmail              *bool          `gorm:"column:meetingBriefsSendEmail;default:true;not null" json:"meetingBriefsSendEmail"`
	MeetingRecorderEnabled              bool           `gorm:"column:meetingRecorderEnabled;default:false;not null" json:"meetingRecorderEnabled"`
	MeetingRecorderJoinRule             string         `gorm:"column:meetingRecorderJoinRule;default:'ALL';not null" json:"meetingRecorderJoinRule"`
	MeetingRecorderRecapEmailEnabled    *bool          `gorm:"column:meetingRecorderRecapEmailEnabled;default:true;not null" json:"meetingRecorderRecapEmailEnabled"`
	MeetingRecorderFollowUpDraftEnabled *bool          `gorm:"column:meetingRecorderFollowUpDraftEnabled;default:true;not null" json:"meetingRecorderFollowUpDraftEnabled"`
	FilingEnabled                       bool           `gorm:"column:filingEnabled;default:false;not null" json:"filingEnabled"`
	FilingPrompt                        *string        `gorm:"column:filingPrompt" json:"filingPrompt"`
	FilingConfirmationSendEmail         *bool          `gorm:"column:filingConfirmationSendEmail;default:true;not null" json:"filingConfirmationSendEmail"`
	FollowUpAwaitingReplyDays           *float64       `gorm:"column:followUpAwaitingReplyDays" json:"followUpAwaitingReplyDays"`
	FollowUpNeedsReplyDays              *float64       `gorm:"column:followUpNeedsReplyDays" json:"followUpNeedsReplyDays"`
	FollowUpAutoDraftEnabled            bool           `gorm:"column:followUpAutoDraftEnabled;default:false;not null" json:"followUpAutoDraftEnabled"`
	DigestSendEmail                     *bool          `gorm:"column:digestSendEmail;default:true;not null" json:"digestSendEmail"`
	LearnedWritingStyle                 *string        `gorm:"column:learnedWritingStyle" json:"learnedWritingStyle"`
	UserID                              string         `gorm:"column:userId;index:EmailAccount_userId_idx;not null" json:"userId"`
	AccountID                           string         `gorm:"column:accountId;uniqueIndex;not null" json:"accountId"`
	User                                *User
	Account                             *Account
	Labels                              []Label                  `gorm:"foreignKey:EmailAccountID;constraint:OnDelete:CASCADE"`
	Rules                               []Rule                   `gorm:"foreignKey:EmailAccountID;constraint:OnDelete:CASCADE"`
	ExecutedRules                       []ExecutedRule           `gorm:"foreignKey:EmailAccountID;constraint:OnDelete:CASCADE"`
	Newsletters                         []Newsletter             `gorm:"foreignKey:EmailAccountID;constraint:OnDelete:CASCADE"`
	ColdEmails                          []ColdEmail              `gorm:"foreignKey:EmailAccountID;constraint:OnDelete:CASCADE"`
	Groups                              []Group                  `gorm:"foreignKey:EmailAccountID;constraint:OnDelete:CASCADE"`
	Categories                          []Category               `gorm:"foreignKey:EmailAccountID;constraint:OnDelete:CASCADE"`
	ThreadTrackers                      []ThreadTracker          `gorm:"foreignKey:EmailAccountID;constraint:OnDelete:CASCADE"`
	CleanupJobs                         []CleanupJob             `gorm:"foreignKey:EmailAccountID;constraint:OnDelete:CASCADE"`
	BulkInboxActionJobs                 []BulkInboxActionJob     `gorm:"foreignKey:EmailAccountID;constraint:OnDelete:CASCADE"`
	CleanupThreads                      []CleanupThread          `gorm:"foreignKey:EmailAccountID;constraint:OnDelete:CASCADE"`
	EmailMessages                       []EmailMessage           `gorm:"foreignKey:EmailAccountID;constraint:OnDelete:CASCADE"`
	EmailTokens                         []EmailToken             `gorm:"foreignKey:EmailAccountID;constraint:OnDelete:CASCADE"`
	Knowledge                           []Knowledge              `gorm:"foreignKey:EmailAccountID;constraint:OnDelete:CASCADE"`
	Snippets                            []Snippet                `gorm:"foreignKey:EmailAccountID;constraint:OnDelete:CASCADE"`
	EmailSignatures                     []EmailSignature         `gorm:"foreignKey:EmailAccountID;constraint:OnDelete:CASCADE"`
	ReplyMemories                       []ReplyMemory            `gorm:"foreignKey:EmailAccountID;constraint:OnDelete:CASCADE"`
	Chats                               []Chat                   `gorm:"foreignKey:EmailAccountID;constraint:OnDelete:CASCADE"`
	ChatMemories                        []ChatMemory             `gorm:"foreignKey:EmailAccountID;constraint:OnDelete:CASCADE"`
	Digests                             []Digest                 `gorm:"foreignKey:EmailAccountID;constraint:OnDelete:CASCADE"`
	ScheduledActions                    []ScheduledAction        `gorm:"foreignKey:EmailAccountID;constraint:OnDelete:CASCADE"`
	SnoozedThreads                      []SnoozedThread          `gorm:"foreignKey:EmailAccountID;constraint:OnDelete:CASCADE"`
	EmailSendOperations                 []EmailSendOperation     `gorm:"foreignKey:EmailAccountID;constraint:OnDelete:CASCADE"`
	SentMessageOpens                    []SentMessageOpen        `gorm:"foreignKey:EmailAccountID;constraint:OnDelete:CASCADE"`
	ScheduledEmails                     []ScheduledEmail         `gorm:"foreignKey:EmailAccountID;constraint:OnDelete:CASCADE"`
	ResponseTimes                       []ResponseTime           `gorm:"foreignKey:EmailAccountID;constraint:OnDelete:CASCADE"`
	Actions                             []Action                 `gorm:"foreignKey:EmailAccountID;constraint:OnDelete:CASCADE"`
	Members                             []Member                 `gorm:"foreignKey:EmailAccountID;constraint:OnDelete:RESTRICT"`
	Invitations                         []Invitation             `gorm:"foreignKey:InviterID;constraint:OnDelete:CASCADE"`
	Ssoproviders                        []SsoProvider            `gorm:"foreignKey:EmailAccountID;constraint:OnDelete:CASCADE"`
	CalendarConnections                 []CalendarConnection     `gorm:"foreignKey:EmailAccountID;constraint:OnDelete:CASCADE"`
	AvailabilitySchedules               []AvailabilitySchedule   `gorm:"foreignKey:EmailAccountID;constraint:OnDelete:CASCADE"`
	BookingLinks                        []BookingLink            `gorm:"foreignKey:EmailAccountID;constraint:OnDelete:CASCADE"`
	Bookings                            []Booking                `gorm:"foreignKey:EmailAccountID;constraint:OnDelete:CASCADE"`
	McpConnections                      []McpConnection          `gorm:"foreignKey:EmailAccountID;constraint:OnDelete:CASCADE"`
	McpIntegrations                     []McpIntegration         `gorm:"foreignKey:EmailAccountID;constraint:OnDelete:CASCADE"`
	MeetingBriefings                    []MeetingBriefing        `gorm:"foreignKey:EmailAccountID;constraint:OnDelete:CASCADE"`
	Meetings                            []Meeting                `gorm:"foreignKey:EmailAccountID;constraint:OnDelete:CASCADE"`
	MeetingRecordings                   []MeetingRecording       `gorm:"foreignKey:EmailAccountID;constraint:OnDelete:CASCADE"`
	DriveConnections                    []DriveConnection        `gorm:"foreignKey:EmailAccountID;constraint:OnDelete:CASCADE"`
	MessagingChannels                   []MessagingChannel       `gorm:"foreignKey:EmailAccountID;constraint:OnDelete:CASCADE"`
	DocumentFilings                     []DocumentFiling         `gorm:"foreignKey:EmailAccountID;constraint:OnDelete:CASCADE"`
	FilingFolders                       []FilingFolder           `gorm:"foreignKey:EmailAccountID;constraint:OnDelete:CASCADE"`
	APIKeys                             []APIKey                 `gorm:"foreignKey:EmailAccountID;constraint:OnDelete:CASCADE"`
	ClassificationFeedback              []ClassificationFeedback `gorm:"foreignKey:EmailAccountID;constraint:OnDelete:CASCADE"`
	MailSplits                          []MailSplit              `gorm:"foreignKey:EmailAccountID;constraint:OnDelete:CASCADE"`
	DigestSchedule                      *Schedule                `gorm:"foreignKey:EmailAccountID;constraint:OnDelete:CASCADE"`
}

func (EmailAccount) TableName() string {
	return "EmailAccount"
}

func (m *EmailAccount) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*EmailAccount) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type MailSplit struct {
	ID             string   `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt      DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt      DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Name           string   `gorm:"column:name;uniqueIndex:MailSplit_emailAccountId_name_key;not null" json:"name"`
	Order          int      `gorm:"column:order;index:MailSplit_emailAccountId_order_idx;not null" json:"order"`
	MatchAll       *bool    `gorm:"column:matchAll;default:true;not null" json:"matchAll"`
	BundleInInbox  bool     `gorm:"column:bundleInInbox;default:false;not null" json:"bundleInInbox"`
	HideFromAll    bool     `gorm:"column:hideFromAll;default:false;not null" json:"hideFromAll"`
	EmailAccountID string   `gorm:"column:emailAccountId;uniqueIndex:MailSplit_emailAccountId_name_key;index:MailSplit_emailAccountId_order_idx;not null" json:"emailAccountId"`
	EmailAccount   *EmailAccount
	Filters        []MailSplitFilter `gorm:"foreignKey:MailSplitID;constraint:OnDelete:CASCADE"`
}

func (MailSplit) TableName() string {
	return "MailSplit"
}

func (m *MailSplit) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*MailSplit) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type MailSplitFilter struct {
	ID          string  `gorm:"column:id;primaryKey" json:"id"`
	Kind        string  `gorm:"column:kind;index:MailSplitFilter_kind_value_idx;not null" json:"kind"`
	Value       *string `gorm:"column:value;index:MailSplitFilter_kind_value_idx" json:"value"`
	Order       int     `gorm:"column:order;uniqueIndex:MailSplitFilter_mailSplitId_order_key;not null" json:"order"`
	MailSplitID string  `gorm:"column:mailSplitId;uniqueIndex:MailSplitFilter_mailSplitId_order_key;index:MailSplitFilter_mailSplitId_idx;not null" json:"mailSplitId"`
	MailSplit   *MailSplit
}

func (MailSplitFilter) TableName() string {
	return "MailSplitFilter"
}

func (m *MailSplitFilter) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = cuid.New()
	}
	return nil
}

type Organization struct {
	ID            string             `gorm:"column:id;primaryKey" json:"id"`
	Name          string             `gorm:"column:name;not null" json:"name"`
	Slug          string             `gorm:"column:slug;uniqueIndex;not null" json:"slug"`
	Logo          *string            `gorm:"column:logo" json:"logo"`
	Metadata      datatypes.JSON     `gorm:"column:metadata" json:"metadata"`
	CreatedAt     DateTime           `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt     DateTime           `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Members       []Member           `gorm:"foreignKey:OrganizationID;constraint:OnDelete:CASCADE"`
	Sessions      []Session          `gorm:"foreignKey:ActiveOrganizationID;constraint:OnDelete:SET NULL"`
	Invitations   []Invitation       `gorm:"foreignKey:OrganizationID;constraint:OnDelete:CASCADE"`
	Rules         []OrganizationRule `gorm:"foreignKey:OrganizationID;constraint:OnDelete:CASCADE"`
	Conversations []Conversation     `gorm:"foreignKey:OrganizationID;constraint:OnDelete:CASCADE"`
	SsoProvider   *SsoProvider       `gorm:"foreignKey:OrganizationID;constraint:OnDelete:CASCADE"`
}

func (Organization) TableName() string {
	return "Organization"
}

func (m *Organization) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*Organization) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type OrganizationRule struct {
	ID                  string   `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt           DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt           DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	OrganizationID      string   `gorm:"column:organizationId;uniqueIndex:OrganizationRule_organizationId_name_key;index:OrganizationRule_organizationId_enabled_idx;not null" json:"organizationId"`
	Enabled             *bool    `gorm:"column:enabled;index:OrganizationRule_organizationId_enabled_idx;default:true;not null" json:"enabled"`
	Name                string   `gorm:"column:name;uniqueIndex:OrganizationRule_organizationId_name_key;not null" json:"name"`
	Instructions        *string  `gorm:"column:instructions" json:"instructions"`
	RunOnThreads        bool     `gorm:"column:runOnThreads;default:false;not null" json:"runOnThreads"`
	ConditionalOperator string   `gorm:"column:conditionalOperator;default:'AND';not null" json:"conditionalOperator"`
	From                *string  `gorm:"column:from" json:"from"`
	To                  *string  `gorm:"column:to" json:"to"`
	Subject             *string  `gorm:"column:subject" json:"subject"`
	Body                *string  `gorm:"column:body" json:"body"`
	Organization        *Organization
	Actions             []OrganizationRuleAction `gorm:"foreignKey:OrganizationRuleID;constraint:OnDelete:CASCADE"`
	MemberRules         []Rule                   `gorm:"foreignKey:OrganizationRuleID;constraint:OnDelete:CASCADE"`
}

func (OrganizationRule) TableName() string {
	return "OrganizationRule"
}

func (m *OrganizationRule) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*OrganizationRule) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type OrganizationRuleAction struct {
	ID                 string         `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt          DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt          DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	OrganizationRuleID string         `gorm:"column:organizationRuleId;index:OrganizationRuleAction_organizationRuleId_idx;not null" json:"organizationRuleId"`
	Type               string         `gorm:"column:type;not null" json:"type"`
	Label              *string        `gorm:"column:label" json:"label"`
	Subject            *string        `gorm:"column:subject" json:"subject"`
	Content            *string        `gorm:"column:content" json:"content"`
	To                 *string        `gorm:"column:to" json:"to"`
	Cc                 *string        `gorm:"column:cc" json:"cc"`
	Bcc                *string        `gorm:"column:bcc" json:"bcc"`
	URL                *string        `gorm:"column:url" json:"url"`
	FolderName         *string        `gorm:"column:folderName" json:"folderName"`
	DelayInMinutes     *int           `gorm:"column:delayInMinutes" json:"delayInMinutes"`
	StaticAttachments  datatypes.JSON `gorm:"column:staticAttachments" json:"staticAttachments"`
	OrganizationRule   *OrganizationRule
}

func (OrganizationRuleAction) TableName() string {
	return "OrganizationRuleAction"
}

func (m *OrganizationRuleAction) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*OrganizationRuleAction) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Member struct {
	ID                       string   `gorm:"column:id;primaryKey" json:"id"`
	OrganizationID           string   `gorm:"column:organizationId;uniqueIndex:Member_organizationId_emailAccountId_key;not null" json:"organizationId"`
	EmailAccountID           string   `gorm:"column:emailAccountId;uniqueIndex;uniqueIndex:Member_organizationId_emailAccountId_key;not null" json:"emailAccountId"`
	Role                     *string  `gorm:"column:role;default:'member';not null" json:"role"`
	AllowOrgAdminAnalytics   bool     `gorm:"column:allowOrgAdminAnalytics;default:false;not null" json:"allowOrgAdminAnalytics"`
	CreatedAt                DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt                DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Organization             *Organization
	EmailAccount             *EmailAccount
	Conversations            []Conversation            `gorm:"foreignKey:PublisherID;constraint:OnDelete:CASCADE"`
	ConversationParticipants []ConversationParticipant `gorm:"foreignKey:MemberID;constraint:OnDelete:CASCADE"`
	ConversationComments     []ConversationComment     `gorm:"foreignKey:AuthorMemberID;constraint:OnDelete:SET NULL"`
}

func (Member) TableName() string {
	return "Member"
}

func (m *Member) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*Member) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Conversation struct {
	ID             string   `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt      DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt      DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	OrganizationID string   `gorm:"column:organizationId;index:Conversation_organizationId_idx;not null" json:"organizationId"`
	PublisherID    string   `gorm:"column:publisherId;uniqueIndex:Conversation_publisherId_threadId_key;not null" json:"publisherId"`
	ThreadID       string   `gorm:"column:threadId;uniqueIndex:Conversation_publisherId_threadId_key;not null" json:"threadId"`
	Status         string   `gorm:"column:status;default:'ACTIVE';not null" json:"status"`
	Revision       int      `gorm:"column:revision;default:0;not null" json:"revision"`
	Generation     *int     `gorm:"column:generation;default:1;not null" json:"generation"`
	Organization   *Organization
	Publisher      *Member                   `gorm:"foreignKey:PublisherID"`
	Participants   []ConversationParticipant `gorm:"foreignKey:ConversationID;constraint:OnDelete:CASCADE"`
	Comments       []ConversationComment     `gorm:"foreignKey:ConversationID;constraint:OnDelete:CASCADE"`
}

func (Conversation) TableName() string {
	return "Conversation"
}

func (m *Conversation) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*Conversation) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type ConversationParticipant struct {
	ID             string   `gorm:"column:id;primaryKey" json:"id"`
	ConversationID string   `gorm:"column:conversationId;uniqueIndex:ConversationParticipant_conversationId_memberId_generation_key;not null" json:"conversationId"`
	MemberID       string   `gorm:"column:memberId;uniqueIndex:ConversationParticipant_conversationId_memberId_generation_key;index:ConversationParticipant_memberId_idx;not null" json:"memberId"`
	Generation     int      `gorm:"column:generation;uniqueIndex:ConversationParticipant_conversationId_memberId_generation_key;not null" json:"generation"`
	Active         *bool    `gorm:"column:active;default:true;not null" json:"active"`
	ReadRevision   int      `gorm:"column:readRevision;default:0;not null" json:"readRevision"`
	Muted          bool     `gorm:"column:muted;default:false;not null" json:"muted"`
	CreatedAt      DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	Conversation   *Conversation
	Member         *Member
	Activity       []ConversationActivity `gorm:"foreignKey:ParticipantID;constraint:OnDelete:CASCADE"`
}

func (ConversationParticipant) TableName() string {
	return "ConversationParticipant"
}

func (m *ConversationParticipant) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type ConversationComment struct {
	ID               string    `gorm:"column:id;primaryKey" json:"id"`
	ConversationID   string    `gorm:"column:conversationId;uniqueIndex:ConversationComment_conversationId_revision_key;uniqueIndex:ConversationComment_conversationId_authorMemberId_clientMutationId_key;not null" json:"conversationId"`
	AuthorMemberID   *string   `gorm:"column:authorMemberId;uniqueIndex:ConversationComment_conversationId_authorMemberId_clientMutationId_key" json:"authorMemberId"`
	ClientMutationID string    `gorm:"column:clientMutationId;uniqueIndex:ConversationComment_conversationId_authorMemberId_clientMutationId_key;not null" json:"clientMutationId"`
	Body             string    `gorm:"column:body;type:text;not null" json:"body"`
	Revision         int       `gorm:"column:revision;uniqueIndex:ConversationComment_conversationId_revision_key;not null" json:"revision"`
	DeletedAt        *DateTime `gorm:"column:deletedAt" json:"deletedAt"`
	CreatedAt        DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	Conversation     *Conversation
	Author           *Member                `gorm:"foreignKey:AuthorMemberID"`
	Activity         []ConversationActivity `gorm:"foreignKey:CommentID;constraint:OnDelete:SET NULL"`
}

func (ConversationComment) TableName() string {
	return "ConversationComment"
}

func (m *ConversationComment) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type ConversationActivity struct {
	ID            string                   `gorm:"column:id;primaryKey;index:ConversationActivity_participantId_createdAt_id_idx" json:"id"`
	ParticipantID string                   `gorm:"column:participantId;uniqueIndex:ConversationActivity_participantId_kind_revision_key;index:ConversationActivity_participantId_createdAt_id_idx;not null" json:"participantId"`
	CommentID     *string                  `gorm:"column:commentId" json:"commentId"`
	Kind          string                   `gorm:"column:kind;uniqueIndex:ConversationActivity_participantId_kind_revision_key;not null" json:"kind"`
	Revision      int                      `gorm:"column:revision;uniqueIndex:ConversationActivity_participantId_kind_revision_key;not null" json:"revision"`
	CreatedAt     DateTime                 `gorm:"column:createdAt;index:ConversationActivity_participantId_createdAt_id_idx;autoCreateTime:false;not null" json:"createdAt"`
	Participant   *ConversationParticipant `gorm:"foreignKey:ParticipantID"`
	Comment       *ConversationComment     `gorm:"foreignKey:CommentID"`
}

func (ConversationActivity) TableName() string {
	return "ConversationActivity"
}

func (m *ConversationActivity) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type Invitation struct {
	ID             string   `gorm:"column:id;primaryKey" json:"id"`
	OrganizationID string   `gorm:"column:organizationId;index:invitation_organizationId_idx;not null" json:"organizationId"`
	Email          string   `gorm:"column:email;not null" json:"email"`
	Role           *string  `gorm:"column:role" json:"role"`
	Status         string   `gorm:"column:status;not null" json:"status"`
	ExpiresAt      DateTime `gorm:"column:expiresAt;not null" json:"expiresAt"`
	InviterID      string   `gorm:"column:inviterId;index:invitation_inviterId_idx;not null" json:"inviterId"`
	Organization   *Organization
	Inviter        *EmailAccount `gorm:"foreignKey:InviterID"`
}

func (Invitation) TableName() string {
	return "invitation"
}

func (m *Invitation) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = cuid.New()
	}
	return nil
}

type Verification struct {
	ID         string   `gorm:"column:id;primaryKey" json:"id"`
	Identifier string   `gorm:"column:identifier;not null" json:"identifier"`
	Value      string   `gorm:"column:value;not null" json:"value"`
	ExpiresAt  DateTime `gorm:"column:expiresAt;not null" json:"expiresAt"`
	CreatedAt  DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt  DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
}

func (Verification) TableName() string {
	return "verification"
}

func (m *Verification) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*Verification) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type SsoProvider struct {
	UserID         *string `gorm:"column:userId" json:"userId"`
	ID             string  `gorm:"column:id;primaryKey" json:"id"`
	Issuer         string  `gorm:"column:issuer;not null" json:"issuer"`
	OidcConfig     *string `gorm:"column:oidcConfig" json:"oidcConfig"`
	SamlConfig     *string `gorm:"column:samlConfig" json:"samlConfig"`
	EmailAccountID *string `gorm:"column:emailAccountId;index:ssoProvider_emailAccountId_idx" json:"emailAccountId"`
	ProviderID     string  `gorm:"column:providerId;uniqueIndex;not null" json:"providerId"`
	OrganizationID *string `gorm:"column:organizationId;uniqueIndex" json:"organizationId"`
	Domain         string  `gorm:"column:domain;not null" json:"domain"`
	User           *User
	EmailAccount   *EmailAccount
	Organization   *Organization
}

func (SsoProvider) TableName() string {
	return "ssoProvider"
}

func (m *SsoProvider) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = cuid.New()
	}
	return nil
}

type Digest struct {
	ID             string    `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt      DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt      DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	EmailAccountID string    `gorm:"column:emailAccountId;index:Digest_emailAccountId_idx;not null" json:"emailAccountId"`
	SentAt         *DateTime `gorm:"column:sentAt" json:"sentAt"`
	Status         string    `gorm:"column:status;default:'PENDING';not null" json:"status"`
	EmailAccount   *EmailAccount
	Items          []DigestItem `gorm:"foreignKey:DigestID;constraint:OnDelete:CASCADE"`
}

func (Digest) TableName() string {
	return "Digest"
}

func (m *Digest) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*Digest) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type DigestItem struct {
	ID          string   `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt   DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt   DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	MessageID   string   `gorm:"column:messageId;uniqueIndex:DigestItem_digestId_threadId_messageId_key;not null" json:"messageId"`
	ThreadID    string   `gorm:"column:threadId;uniqueIndex:DigestItem_digestId_threadId_messageId_key;not null" json:"threadId"`
	Content     string   `gorm:"column:content;type:text;not null" json:"content"`
	DigestID    string   `gorm:"column:digestId;uniqueIndex:DigestItem_digestId_threadId_messageId_key;not null" json:"digestId"`
	ActionID    *string  `gorm:"column:actionId;index:DigestItem_actionId_idx" json:"actionId"`
	ColdEmailID *string  `gorm:"column:coldEmailId;index:DigestItem_coldEmailId_idx" json:"coldEmailId"`
	Digest      *Digest
	Action      *ExecutedAction `gorm:"foreignKey:ActionID"`
	ColdEmail   *ColdEmail
}

func (DigestItem) TableName() string {
	return "DigestItem"
}

func (m *DigestItem) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*DigestItem) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Schedule struct {
	ID               string    `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt        DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt        DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	IntervalDays     *int      `gorm:"column:intervalDays" json:"intervalDays"`
	Occurrences      *int      `gorm:"column:occurrences" json:"occurrences"`
	DaysOfWeek       *int      `gorm:"column:daysOfWeek" json:"daysOfWeek"`
	TimeOfDay        *DateTime `gorm:"column:timeOfDay" json:"timeOfDay"`
	EmailAccountID   string    `gorm:"column:emailAccountId;uniqueIndex;not null" json:"emailAccountId"`
	LastOccurrenceAt *DateTime `gorm:"column:lastOccurrenceAt" json:"lastOccurrenceAt"`
	NextOccurrenceAt *DateTime `gorm:"column:nextOccurrenceAt" json:"nextOccurrenceAt"`
	EmailAccount     *EmailAccount
}

func (Schedule) TableName() string {
	return "Schedule"
}

func (m *Schedule) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*Schedule) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Premium struct {
	ID                               string    `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt                        DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt                        DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	PendingInvites                   []string  `gorm:"column:pendingInvites;index:Premium_pendingInvites_idx;serializer:json;not null" json:"pendingInvites"`
	LemonSqueezyRenewsAt             *DateTime `gorm:"column:lemonSqueezyRenewsAt" json:"lemonSqueezyRenewsAt"`
	LemonSqueezyCustomerID           *int      `gorm:"column:lemonSqueezyCustomerId" json:"lemonSqueezyCustomerId"`
	LemonSqueezySubscriptionID       *int      `gorm:"column:lemonSqueezySubscriptionId" json:"lemonSqueezySubscriptionId"`
	LemonSqueezySubscriptionItemID   *int      `gorm:"column:lemonSqueezySubscriptionItemId" json:"lemonSqueezySubscriptionItemId"`
	LemonSqueezyOrderID              *int      `gorm:"column:lemonSqueezyOrderId" json:"lemonSqueezyOrderId"`
	LemonSqueezyProductID            *int      `gorm:"column:lemonSqueezyProductId" json:"lemonSqueezyProductId"`
	LemonSqueezyVariantID            *int      `gorm:"column:lemonSqueezyVariantId" json:"lemonSqueezyVariantId"`
	LemonLicenseKey                  *string   `gorm:"column:lemonLicenseKey" json:"lemonLicenseKey"`
	LemonLicenseInstanceID           *string   `gorm:"column:lemonLicenseInstanceId" json:"lemonLicenseInstanceId"`
	LemonSubscriptionStatus          *string   `gorm:"column:lemonSubscriptionStatus" json:"lemonSubscriptionStatus"`
	StripeCustomerID                 *string   `gorm:"column:stripeCustomerId;uniqueIndex" json:"stripeCustomerId"`
	StripeSubscriptionID             *string   `gorm:"column:stripeSubscriptionId;uniqueIndex" json:"stripeSubscriptionId"`
	StripeSubscriptionItemID         *string   `gorm:"column:stripeSubscriptionItemId;uniqueIndex" json:"stripeSubscriptionItemId"`
	StripePriceID                    *string   `gorm:"column:stripePriceId" json:"stripePriceId"`
	StripeProductID                  *string   `gorm:"column:stripeProductId" json:"stripeProductId"`
	StripeSubscriptionStatus         *string   `gorm:"column:stripeSubscriptionStatus" json:"stripeSubscriptionStatus"`
	StripeCancelAt                   *DateTime `gorm:"column:stripeCancelAt" json:"stripeCancelAt"`
	StripeCancelAtPeriodEnd          *bool     `gorm:"column:stripeCancelAtPeriodEnd" json:"stripeCancelAtPeriodEnd"`
	StripeRenewsAt                   *DateTime `gorm:"column:stripeRenewsAt" json:"stripeRenewsAt"`
	StripeTrialEnd                   *DateTime `gorm:"column:stripeTrialEnd" json:"stripeTrialEnd"`
	StripeTrialConversionInvoiceID   *string   `gorm:"column:stripeTrialConversionInvoiceId" json:"stripeTrialConversionInvoiceId"`
	StripeTrialConvertedAt           *DateTime `gorm:"column:stripeTrialConvertedAt" json:"stripeTrialConvertedAt"`
	StripeCancellationInitiatedAt    *DateTime `gorm:"column:stripeCancellationInitiatedAt" json:"stripeCancellationInitiatedAt"`
	StripeCanceledAt                 *DateTime `gorm:"column:stripeCanceledAt" json:"stripeCanceledAt"`
	StripeEndedAt                    *DateTime `gorm:"column:stripeEndedAt" json:"stripeEndedAt"`
	StripeCancellationFeedback       *string   `gorm:"column:stripeCancellationFeedback" json:"stripeCancellationFeedback"`
	StripeCancellationComment        *string   `gorm:"column:stripeCancellationComment" json:"stripeCancellationComment"`
	StripeAiOverageLastInvoiceID     *string   `gorm:"column:stripeAiOverageLastInvoiceId" json:"stripeAiOverageLastInvoiceId"`
	StripeAiOverageLastPeriodEnd     *DateTime `gorm:"column:stripeAiOverageLastPeriodEnd" json:"stripeAiOverageLastPeriodEnd"`
	StripeAiOverageLastUnits         *int      `gorm:"column:stripeAiOverageLastUnits" json:"stripeAiOverageLastUnits"`
	StripeInvoiceEmailsEnabled       bool      `gorm:"column:stripeInvoiceEmailsEnabled;default:false;not null" json:"stripeInvoiceEmailsEnabled"`
	AppleOriginalTransactionID       *string   `gorm:"column:appleOriginalTransactionId;uniqueIndex" json:"appleOriginalTransactionId"`
	AppleLatestTransactionID         *string   `gorm:"column:appleLatestTransactionId;uniqueIndex" json:"appleLatestTransactionId"`
	AppleProductID                   *string   `gorm:"column:appleProductId" json:"appleProductId"`
	AppleSubscriptionGroupIdentifier *string   `gorm:"column:appleSubscriptionGroupIdentifier" json:"appleSubscriptionGroupIdentifier"`
	AppleAppAccountToken             *string   `gorm:"column:appleAppAccountToken" json:"appleAppAccountToken"`
	AppleSubscriptionStatus          *string   `gorm:"column:appleSubscriptionStatus" json:"appleSubscriptionStatus"`
	AppleEnvironment                 *string   `gorm:"column:appleEnvironment" json:"appleEnvironment"`
	ApplePurchaseDate                *DateTime `gorm:"column:applePurchaseDate" json:"applePurchaseDate"`
	AppleExpiresAt                   *DateTime `gorm:"column:appleExpiresAt" json:"appleExpiresAt"`
	AppleRevokedAt                   *DateTime `gorm:"column:appleRevokedAt" json:"appleRevokedAt"`
	AppleOfferDiscountType           *string   `gorm:"column:appleOfferDiscountType" json:"appleOfferDiscountType"`
	AdminGrantExpiresAt              *DateTime `gorm:"column:adminGrantExpiresAt" json:"adminGrantExpiresAt"`
	AdminGrantTier                   *string   `gorm:"column:adminGrantTier" json:"adminGrantTier"`
	Tier                             *string   `gorm:"column:tier" json:"tier"`
	EmailAccountsAccess              *int      `gorm:"column:emailAccountsAccess" json:"emailAccountsAccess"`
	UnsubscribeMonth                 *int      `gorm:"column:unsubscribeMonth" json:"unsubscribeMonth"`
	UnsubscribeCredits               *int      `gorm:"column:unsubscribeCredits" json:"unsubscribeCredits"`
	AiMonth                          *int      `gorm:"column:aiMonth" json:"aiMonth"`
	AiCredits                        *int      `gorm:"column:aiCredits" json:"aiCredits"`
	Users                            []User    `gorm:"foreignKey:PremiumID"`
	Admins                           []User    `gorm:"foreignKey:PremiumAdminID"`
	Payments                         []Payment `gorm:"foreignKey:PremiumID;constraint:OnDelete:SET NULL"`
}

func (Premium) TableName() string {
	return "Premium"
}

func (m *Premium) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*Premium) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type PendingPremiumGrant struct {
	ID                  string   `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt           DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt           DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Email               string   `gorm:"column:email;uniqueIndex;not null" json:"email"`
	Tier                string   `gorm:"column:tier;not null" json:"tier"`
	Count               *int     `gorm:"column:count;default:1;not null" json:"count"`
	EmailAccountsAccess *int     `gorm:"column:emailAccountsAccess" json:"emailAccountsAccess"`
}

func (PendingPremiumGrant) TableName() string {
	return "PendingPremiumGrant"
}

func (m *PendingPremiumGrant) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*PendingPremiumGrant) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type VerificationToken struct {
	ID         string   `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt  DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt  DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Identifier string   `gorm:"column:identifier;uniqueIndex:VerificationToken_identifier_token_key;not null" json:"identifier"`
	Token      string   `gorm:"column:token;uniqueIndex;uniqueIndex:VerificationToken_identifier_token_key;not null" json:"token"`
	Expires    DateTime `gorm:"column:expires;not null" json:"expires"`
}

func (VerificationToken) TableName() string {
	return "VerificationToken"
}

func (m *VerificationToken) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*VerificationToken) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Label struct {
	ID             string   `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt      DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt      DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	GmailLabelID   string   `gorm:"column:gmailLabelId;uniqueIndex:Label_gmailLabelId_emailAccountId_key;not null" json:"gmailLabelId"`
	Name           string   `gorm:"column:name;uniqueIndex:Label_name_emailAccountId_key;not null" json:"name"`
	Description    *string  `gorm:"column:description" json:"description"`
	Enabled        *bool    `gorm:"column:enabled;default:true;not null" json:"enabled"`
	EmailAccountID string   `gorm:"column:emailAccountId;uniqueIndex:Label_gmailLabelId_emailAccountId_key;uniqueIndex:Label_name_emailAccountId_key;index:Label_emailAccountId_idx;not null" json:"emailAccountId"`
	EmailAccount   *EmailAccount
}

func (Label) TableName() string {
	return "Label"
}

func (m *Label) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*Label) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Rule struct {
	ID                            string   `gorm:"column:id;primaryKey;uniqueIndex:Rule_id_emailAccountId_key" json:"id"`
	CreatedAt                     DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt                     DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Name                          string   `gorm:"column:name;uniqueIndex:Rule_name_emailAccountId_key;not null" json:"name"`
	Enabled                       *bool    `gorm:"column:enabled;index:Rule_emailAccountId_enabled_idx;default:true;not null" json:"enabled"`
	Automate                      *bool    `gorm:"column:automate;default:true;not null" json:"automate"`
	RunOnThreads                  bool     `gorm:"column:runOnThreads;default:false;not null" json:"runOnThreads"`
	EmailAccountID                string   `gorm:"column:emailAccountId;uniqueIndex:Rule_id_emailAccountId_key;uniqueIndex:Rule_name_emailAccountId_key;uniqueIndex:Rule_emailAccountId_systemType_key;uniqueIndex:Rule_emailAccountId_organizationRuleId_key;index:Rule_emailAccountId_enabled_idx;not null" json:"emailAccountId"`
	OrganizationRuleID            *string  `gorm:"column:organizationRuleId;uniqueIndex:Rule_emailAccountId_organizationRuleId_key;index:Rule_organizationRuleId_idx" json:"organizationRuleId"`
	OrganizationRuleMemberEnabled *bool    `gorm:"column:organizationRuleMemberEnabled" json:"organizationRuleMemberEnabled"`
	ConditionalOperator           string   `gorm:"column:conditionalOperator;default:'AND';not null" json:"conditionalOperator"`
	Instructions                  *string  `gorm:"column:instructions" json:"instructions"`
	GroupID                       *string  `gorm:"column:groupId;uniqueIndex" json:"groupId"`
	From                          *string  `gorm:"column:from" json:"from"`
	To                            *string  `gorm:"column:to" json:"to"`
	Subject                       *string  `gorm:"column:subject" json:"subject"`
	Body                          *string  `gorm:"column:body" json:"body"`
	CategoryFilterType            *string  `gorm:"column:categoryFilterType" json:"categoryFilterType"`
	SystemType                    *string  `gorm:"column:systemType;uniqueIndex:Rule_emailAccountId_systemType_key" json:"systemType"`
	PromptText                    *string  `gorm:"column:promptText" json:"promptText"`
	EmailAccount                  *EmailAccount
	OrganizationRule              *OrganizationRule
	Group                         *Group
	Actions                       []Action                 `gorm:"foreignKey:RuleID,EmailAccountID;references:ID,EmailAccountID;constraint:OnDelete:CASCADE"`
	AttachmentSources             []AttachmentSource       `gorm:"foreignKey:RuleID;constraint:OnDelete:CASCADE"`
	ExecutedRules                 []ExecutedRule           `gorm:"foreignKey:RuleID"`
	ClassificationFeedback        []ClassificationFeedback `gorm:"foreignKey:RuleID;constraint:OnDelete:CASCADE"`
	History                       []RuleHistory            `gorm:"foreignKey:RuleID;constraint:OnDelete:CASCADE"`
	CategoryFilters               []Category               `gorm:"many2many:_CategoryToRule;joinForeignKey:B;joinReferences:A"`
}

func (Rule) TableName() string {
	return "Rule"
}

func (m *Rule) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*Rule) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Action struct {
	ID                             string         `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt                      DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt                      DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Type                           string         `gorm:"column:type;not null" json:"type"`
	RuleID                         string         `gorm:"column:ruleId;index:Action_ruleId_idx;not null" json:"ruleId"`
	EmailAccountID                 string         `gorm:"column:emailAccountId;index:Action_emailAccountId_idx;not null" json:"emailAccountId"`
	MessagingChannelID             *string        `gorm:"column:messagingChannelId;index:Action_messagingChannelId_messagingChannelEmailAccountId_idx" json:"messagingChannelId"`
	MessagingChannelEmailAccountID *string        `gorm:"column:messagingChannelEmailAccountId;index:Action_messagingChannelId_messagingChannelEmailAccountId_idx" json:"messagingChannelEmailAccountId"`
	Label                          *string        `gorm:"column:label" json:"label"`
	LabelID                        *string        `gorm:"column:labelId" json:"labelId"`
	Subject                        *string        `gorm:"column:subject" json:"subject"`
	Content                        *string        `gorm:"column:content" json:"content"`
	To                             *string        `gorm:"column:to" json:"to"`
	Cc                             *string        `gorm:"column:cc" json:"cc"`
	Bcc                            *string        `gorm:"column:bcc" json:"bcc"`
	URL                            *string        `gorm:"column:url" json:"url"`
	FolderName                     *string        `gorm:"column:folderName" json:"folderName"`
	FolderID                       *string        `gorm:"column:folderId" json:"folderId"`
	DelayInMinutes                 *int           `gorm:"column:delayInMinutes" json:"delayInMinutes"`
	StaticAttachments              datatypes.JSON `gorm:"column:staticAttachments" json:"staticAttachments"`
	IntegrationName                *string        `gorm:"column:integrationName" json:"integrationName"`
	IntegrationToolName            *string        `gorm:"column:integrationToolName" json:"integrationToolName"`
	IntegrationArgs                datatypes.JSON `gorm:"column:integrationArgs" json:"integrationArgs"`
	Rule                           *Rule          `gorm:"foreignKey:RuleID,EmailAccountID;references:ID,EmailAccountID"`
	EmailAccount                   *EmailAccount
	MessagingChannel               *MessagingChannel `gorm:"foreignKey:MessagingChannelID,MessagingChannelEmailAccountID;references:ID,EmailAccountID"`
}

func (Action) TableName() string {
	return "Action"
}

func (m *Action) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*Action) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type RuleHistory struct {
	ID                  string         `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt           DateTime       `gorm:"column:createdAt;index:RuleHistory_ruleId_createdAt_idx;autoCreateTime:false;not null" json:"createdAt"`
	RuleID              string         `gorm:"column:ruleId;uniqueIndex:RuleHistory_ruleId_version_key;index:RuleHistory_ruleId_createdAt_idx;not null" json:"ruleId"`
	Version             int            `gorm:"column:version;uniqueIndex:RuleHistory_ruleId_version_key;not null" json:"version"`
	TriggerType         string         `gorm:"column:triggerType;not null" json:"triggerType"`
	PromptText          *string        `gorm:"column:promptText" json:"promptText"`
	Name                string         `gorm:"column:name;not null" json:"name"`
	Instructions        *string        `gorm:"column:instructions" json:"instructions"`
	Enabled             bool           `gorm:"column:enabled;not null" json:"enabled"`
	Automate            bool           `gorm:"column:automate;not null" json:"automate"`
	RunOnThreads        bool           `gorm:"column:runOnThreads;not null" json:"runOnThreads"`
	ConditionalOperator string         `gorm:"column:conditionalOperator;not null" json:"conditionalOperator"`
	From                *string        `gorm:"column:from" json:"from"`
	To                  *string        `gorm:"column:to" json:"to"`
	Subject             *string        `gorm:"column:subject" json:"subject"`
	Body                *string        `gorm:"column:body" json:"body"`
	CategoryFilterType  *string        `gorm:"column:categoryFilterType" json:"categoryFilterType"`
	SystemType          *string        `gorm:"column:systemType" json:"systemType"`
	Actions             datatypes.JSON `gorm:"column:actions;not null" json:"actions"`
	CategoryFilters     datatypes.JSON `gorm:"column:categoryFilters" json:"categoryFilters"`
	Rule                *Rule
}

func (RuleHistory) TableName() string {
	return "RuleHistory"
}

func (m *RuleHistory) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type ExecutedRule struct {
	ID               string         `gorm:"column:id;primaryKey;index:ExecutedRule_emailAccountId_id_idx;index:ExecutedRule_emailAccountId_updatedAt_id_idx" json:"id"`
	CreatedAt        DateTime       `gorm:"column:createdAt;index:ExecutedRule_emailAccountId_status_createdAt_idx;index:ExecutedRule_emailAccountId_createdAt_idx;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt        DateTime       `gorm:"column:updatedAt;index:ExecutedRule_emailAccountId_updatedAt_id_idx;autoUpdateTime:false;not null" json:"updatedAt"`
	ThreadID         string         `gorm:"column:threadId;index:ExecutedRule_emailAccountId_threadId_messageId_ruleId_idx;not null" json:"threadId"`
	MessageID        string         `gorm:"column:messageId;index:ExecutedRule_emailAccountId_threadId_messageId_ruleId_idx;index:ExecutedRule_emailAccountId_messageId_idx;not null" json:"messageId"`
	Status           string         `gorm:"column:status;index:ExecutedRule_emailAccountId_status_createdAt_idx;not null" json:"status"`
	Automated        bool           `gorm:"column:automated;not null" json:"automated"`
	Reason           *string        `gorm:"column:reason" json:"reason"`
	MatchMetadata    datatypes.JSON `gorm:"column:matchMetadata" json:"matchMetadata"`
	RuleID           *string        `gorm:"column:ruleId;index:ExecutedRule_emailAccountId_threadId_messageId_ruleId_idx;index:ExecutedRule_ruleId_idx" json:"ruleId"`
	EmailAccountID   string         `gorm:"column:emailAccountId;index:ExecutedRule_emailAccountId_threadId_messageId_ruleId_idx;index:ExecutedRule_emailAccountId_messageId_idx;index:ExecutedRule_emailAccountId_status_createdAt_idx;index:ExecutedRule_emailAccountId_createdAt_idx;index:ExecutedRule_emailAccountId_id_idx;index:ExecutedRule_emailAccountId_updatedAt_id_idx;not null" json:"emailAccountId"`
	Rule             *Rule
	EmailAccount     *EmailAccount
	ActionItems      []ExecutedAction  `gorm:"foreignKey:ExecutedRuleID;constraint:OnDelete:CASCADE"`
	ScheduledActions []ScheduledAction `gorm:"foreignKey:ExecutedRuleID;constraint:OnDelete:CASCADE"`
}

func (ExecutedRule) TableName() string {
	return "ExecutedRule"
}

func (m *ExecutedRule) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*ExecutedRule) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type ExecutedAction struct {
	ID                     string         `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt              DateTime       `gorm:"column:createdAt;index:ExecutedAction_executionStatus_createdAt_idx;index:ExecutedAction_messagingChannelId_createdAt_idx;index:ExecutedAction_draftStatus_createdAt_draft_idx;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt              DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Type                   string         `gorm:"column:type;not null" json:"type"`
	ExecutionStatus        *string        `gorm:"column:executionStatus;index:ExecutedAction_executionStatus_createdAt_idx" json:"executionStatus"`
	ExecutionStartedAt     *DateTime      `gorm:"column:executionStartedAt" json:"executionStartedAt"`
	ExecutedAt             *DateTime      `gorm:"column:executedAt" json:"executedAt"`
	ExecutionError         datatypes.JSON `gorm:"column:executionError" json:"executionError"`
	SentMessageIds         []string       `gorm:"column:sentMessageIds;index:ExecutedAction_sentMessageIds_idx;serializer:json;not null" json:"sentMessageIds"`
	ExecutedRuleID         string         `gorm:"column:executedRuleId;index:ExecutedAction_executedRuleId_idx;index:ExecutedAction_executedRuleId_draft_idx;not null" json:"executedRuleId"`
	MessagingChannelID     *string        `gorm:"column:messagingChannelId;index:ExecutedAction_messagingChannelId_createdAt_idx" json:"messagingChannelId"`
	MessagingMessageID     *string        `gorm:"column:messagingMessageId" json:"messagingMessageId"`
	MessagingMessageSentAt *DateTime      `gorm:"column:messagingMessageSentAt" json:"messagingMessageSentAt"`
	MessagingMessageStatus *string        `gorm:"column:messagingMessageStatus" json:"messagingMessageStatus"`
	Label                  *string        `gorm:"column:label" json:"label"`
	LabelID                *string        `gorm:"column:labelId" json:"labelId"`
	Subject                *string        `gorm:"column:subject" json:"subject"`
	Content                *string        `gorm:"column:content" json:"content"`
	To                     *string        `gorm:"column:to" json:"to"`
	Cc                     *string        `gorm:"column:cc" json:"cc"`
	Bcc                    *string        `gorm:"column:bcc" json:"bcc"`
	URL                    *string        `gorm:"column:url" json:"url"`
	FolderName             *string        `gorm:"column:folderName" json:"folderName"`
	FolderID               *string        `gorm:"column:folderId" json:"folderId"`
	StaticAttachments      datatypes.JSON `gorm:"column:staticAttachments" json:"staticAttachments"`
	SelectedAttachments    datatypes.JSON `gorm:"column:selectedAttachments" json:"selectedAttachments"`
	IntegrationName        *string        `gorm:"column:integrationName" json:"integrationName"`
	IntegrationToolName    *string        `gorm:"column:integrationToolName" json:"integrationToolName"`
	IntegrationArgs        datatypes.JSON `gorm:"column:integrationArgs" json:"integrationArgs"`
	DraftID                *string        `gorm:"column:draftId" json:"draftId"`
	DraftStatus            *string        `gorm:"column:draftStatus;index:ExecutedAction_draftStatus_createdAt_draft_idx" json:"draftStatus"`
	WasDraftSent           *bool          `gorm:"column:wasDraftSent" json:"wasDraftSent"`
	DraftModelProvider     *string        `gorm:"column:draftModelProvider" json:"draftModelProvider"`
	DraftModelName         *string        `gorm:"column:draftModelName" json:"draftModelName"`
	DraftPipelineVersion   *int           `gorm:"column:draftPipelineVersion" json:"draftPipelineVersion"`
	DraftContextMetadata   datatypes.JSON `gorm:"column:draftContextMetadata" json:"draftContextMetadata"`
	ExecutedRule           *ExecutedRule
	MessagingChannel       *MessagingChannel
	DigestItems            []DigestItem     `gorm:"foreignKey:ActionID;constraint:OnDelete:CASCADE"`
	DraftSendLog           *DraftSendLog    `gorm:"foreignKey:ExecutedActionID;constraint:OnDelete:CASCADE"`
	ScheduledAction        *ScheduledAction `gorm:"foreignKey:ExecutedActionID;constraint:OnDelete:CASCADE"`
}

func (ExecutedAction) TableName() string {
	return "ExecutedAction"
}

func (m *ExecutedAction) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*ExecutedAction) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type ScheduledAction struct {
	ID                  string         `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt           DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt           DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	ExecutedRuleID      string         `gorm:"column:executedRuleId;index:ScheduledAction_executedRuleId_idx;not null" json:"executedRuleId"`
	ActionType          string         `gorm:"column:actionType;not null" json:"actionType"`
	MessageID           string         `gorm:"column:messageId;index:ScheduledAction_emailAccountId_messageId_idx;not null" json:"messageId"`
	ThreadID            string         `gorm:"column:threadId;not null" json:"threadId"`
	ScheduledFor        DateTime       `gorm:"column:scheduledFor;index:ScheduledAction_status_scheduledFor_idx;not null" json:"scheduledFor"`
	EmailAccountID      string         `gorm:"column:emailAccountId;index:ScheduledAction_emailAccountId_messageId_idx;not null" json:"emailAccountId"`
	Status              string         `gorm:"column:status;index:ScheduledAction_status_scheduledFor_idx;default:'PENDING';not null" json:"status"`
	SchedulingStatus    string         `gorm:"column:schedulingStatus;default:'PENDING';not null" json:"schedulingStatus"`
	Label               *string        `gorm:"column:label" json:"label"`
	LabelID             *string        `gorm:"column:labelId" json:"labelId"`
	Subject             *string        `gorm:"column:subject" json:"subject"`
	Content             *string        `gorm:"column:content" json:"content"`
	To                  *string        `gorm:"column:to" json:"to"`
	Cc                  *string        `gorm:"column:cc" json:"cc"`
	Bcc                 *string        `gorm:"column:bcc" json:"bcc"`
	URL                 *string        `gorm:"column:url" json:"url"`
	FolderName          *string        `gorm:"column:folderName" json:"folderName"`
	FolderID            *string        `gorm:"column:folderId" json:"folderId"`
	ScheduledID         *string        `gorm:"column:scheduledId" json:"scheduledId"`
	StaticAttachments   datatypes.JSON `gorm:"column:staticAttachments" json:"staticAttachments"`
	SelectedAttachments datatypes.JSON `gorm:"column:selectedAttachments" json:"selectedAttachments"`
	ExecutedAt          *DateTime      `gorm:"column:executedAt" json:"executedAt"`
	ExecutedActionID    *string        `gorm:"column:executedActionId;uniqueIndex" json:"executedActionId"`
	ExecutedRule        *ExecutedRule
	ExecutedAction      *ExecutedAction
	EmailAccount        *EmailAccount
}

func (ScheduledAction) TableName() string {
	return "ScheduledAction"
}

func (m *ScheduledAction) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*ScheduledAction) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type SnoozedThread struct {
	ID               string   `gorm:"column:id;primaryKey" json:"id"`
	UpdatedAt        DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	ThreadID         string   `gorm:"column:threadId;index:SnoozedThread_emailAccountId_threadId_status_idx;not null" json:"threadId"`
	ScheduledFor     DateTime `gorm:"column:scheduledFor;index:SnoozedThread_status_scheduledFor_idx;not null" json:"scheduledFor"`
	Status           string   `gorm:"column:status;index:SnoozedThread_status_scheduledFor_idx;index:SnoozedThread_emailAccountId_threadId_status_idx;default:'PENDING';not null" json:"status"`
	ClientMutationID *string  `gorm:"column:clientMutationId;uniqueIndex:SnoozedThread_emailAccountId_clientMutationId_key" json:"clientMutationId"`
	EmailAccountID   string   `gorm:"column:emailAccountId;uniqueIndex:SnoozedThread_emailAccountId_clientMutationId_key;index:SnoozedThread_emailAccountId_threadId_status_idx;not null" json:"emailAccountId"`
	EmailAccount     *EmailAccount
}

func (SnoozedThread) TableName() string {
	return "SnoozedThread"
}

func (m *SnoozedThread) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*SnoozedThread) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type ScheduledEmail struct {
	ID                  string         `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt           DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt           DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	EmailAccountID      string         `gorm:"column:emailAccountId;uniqueIndex:ScheduledEmail_emailAccountId_clientMutationId_key;index:ScheduledEmail_emailAccountId_threadId_idx;not null" json:"emailAccountId"`
	ClientMutationID    string         `gorm:"column:clientMutationId;uniqueIndex:ScheduledEmail_emailAccountId_clientMutationId_key;not null" json:"clientMutationId"`
	PayloadHash         string         `gorm:"column:payloadHash;not null" json:"payloadHash"`
	Payload             datatypes.JSON `gorm:"column:payload;not null" json:"payload"`
	ThreadID            *string        `gorm:"column:threadId;index:ScheduledEmail_emailAccountId_threadId_idx" json:"threadId"`
	SendAt              DateTime       `gorm:"column:sendAt;index:ScheduledEmail_status_sendAt_idx;not null" json:"sendAt"`
	Status              string         `gorm:"column:status;index:ScheduledEmail_status_sendAt_idx;index:ScheduledEmail_status_processingStartedAt_idx;default:'PENDING';not null" json:"status"`
	ProcessingStartedAt *DateTime      `gorm:"column:processingStartedAt;index:ScheduledEmail_status_processingStartedAt_idx" json:"processingStartedAt"`
	ExecutionQueuedAt   *DateTime      `gorm:"column:executionQueuedAt" json:"executionQueuedAt"`
	SentAt              *DateTime      `gorm:"column:sentAt" json:"sentAt"`
	Error               *string        `gorm:"column:error" json:"error"`
	RemindAt            *DateTime      `gorm:"column:remindAt;index:ScheduledEmail_reminderStatus_remindAt_idx" json:"remindAt"`
	ReminderStatus      string         `gorm:"column:reminderStatus;index:ScheduledEmail_reminderStatus_remindAt_idx;index:ScheduledEmail_reminderStatus_reminderStartedAt_idx;default:'NONE';not null" json:"reminderStatus"`
	ReminderStartedAt   *DateTime      `gorm:"column:reminderStartedAt;index:ScheduledEmail_reminderStatus_reminderStartedAt_idx" json:"reminderStartedAt"`
	HeldForUndo         bool           `gorm:"column:heldForUndo;default:false;not null" json:"heldForUndo"`
	DraftMessageIds     []string       `gorm:"column:draftMessageIds;serializer:json;not null" json:"draftMessageIds"`
	EmailAccount        *EmailAccount
}

func (ScheduledEmail) TableName() string {
	return "ScheduledEmail"
}

func (m *ScheduledEmail) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*ScheduledEmail) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type EmailSendOperation struct {
	ID                  string         `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt           DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt           DateTime       `gorm:"column:updatedAt;index:EmailSendOperation_status_updatedAt_idx;autoUpdateTime:false;not null" json:"updatedAt"`
	ClientMutationID    string         `gorm:"column:clientMutationId;uniqueIndex:EmailSendOperation_emailAccountId_clientMutationId_key;not null" json:"clientMutationId"`
	PayloadHash         string         `gorm:"column:payloadHash;not null" json:"payloadHash"`
	Status              string         `gorm:"column:status;index:EmailSendOperation_status_updatedAt_idx;default:'PROCESSING';not null" json:"status"`
	ProcessingStartedAt DateTime       `gorm:"column:processingStartedAt;not null" json:"processingStartedAt"`
	Result              datatypes.JSON `gorm:"column:result" json:"result"`
	EmailAccountID      string         `gorm:"column:emailAccountId;uniqueIndex:EmailSendOperation_emailAccountId_clientMutationId_key;not null" json:"emailAccountId"`
	EmailAccount        *EmailAccount
}

func (EmailSendOperation) TableName() string {
	return "EmailSendOperation"
}

func (m *EmailSendOperation) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	if m.ProcessingStartedAt.IsZero() {
		m.ProcessingStartedAt = DateTime{Time: now}
	}
	return nil
}

func (*EmailSendOperation) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type SentMessageOpen struct {
	ID             string    `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt      DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt      DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Token          string    `gorm:"column:token;uniqueIndex;not null" json:"token"`
	EmailAccountID string    `gorm:"column:emailAccountId;uniqueIndex:SentMessageOpen_emailAccountId_messageId_key;index:SentMessageOpen_emailAccountId_threadId_idx;not null" json:"emailAccountId"`
	ThreadID       *string   `gorm:"column:threadId;index:SentMessageOpen_emailAccountId_threadId_idx" json:"threadId"`
	MessageID      *string   `gorm:"column:messageId;uniqueIndex:SentMessageOpen_emailAccountId_messageId_key" json:"messageId"`
	FirstOpenedAt  *DateTime `gorm:"column:firstOpenedAt" json:"firstOpenedAt"`
	LastOpenedAt   *DateTime `gorm:"column:lastOpenedAt" json:"lastOpenedAt"`
	OpenCount      int       `gorm:"column:openCount;default:0;not null" json:"openCount"`
	EmailAccount   *EmailAccount
}

func (SentMessageOpen) TableName() string {
	return "SentMessageOpen"
}

func (m *SentMessageOpen) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*SentMessageOpen) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Group struct {
	ID             string   `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt      DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt      DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Name           string   `gorm:"column:name;uniqueIndex:Group_name_emailAccountId_key;not null" json:"name"`
	Prompt         *string  `gorm:"column:prompt" json:"prompt"`
	EmailAccountID string   `gorm:"column:emailAccountId;uniqueIndex:Group_name_emailAccountId_key;index:Group_emailAccountId_idx;not null" json:"emailAccountId"`
	EmailAccount   *EmailAccount
	Items          []GroupItem `gorm:"foreignKey:GroupID;constraint:OnDelete:CASCADE"`
	Rule           *Rule       `gorm:"foreignKey:GroupID;constraint:OnDelete:CASCADE"`
}

func (Group) TableName() string {
	return "Group"
}

func (m *Group) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*Group) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type GroupItem struct {
	ID        string   `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	GroupID   *string  `gorm:"column:groupId;uniqueIndex:GroupItem_groupId_type_value_key" json:"groupId"`
	Type      string   `gorm:"column:type;uniqueIndex:GroupItem_groupId_type_value_key;not null" json:"type"`
	Value     string   `gorm:"column:value;uniqueIndex:GroupItem_groupId_type_value_key;not null" json:"value"`
	Exclude   bool     `gorm:"column:exclude;default:false;not null" json:"exclude"`
	Reason    *string  `gorm:"column:reason" json:"reason"`
	ThreadID  *string  `gorm:"column:threadId" json:"threadId"`
	MessageID *string  `gorm:"column:messageId" json:"messageId"`
	Source    *string  `gorm:"column:source" json:"source"`
	Group     *Group
}

func (GroupItem) TableName() string {
	return "GroupItem"
}

func (m *GroupItem) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*GroupItem) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type ClassificationFeedback struct {
	ID             string   `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt      DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	Sender         string   `gorm:"column:sender;uniqueIndex:ClassificationFeedback_emailAccountId_sender_ruleId_messageId_eventType_key;not null" json:"sender"`
	EventType      string   `gorm:"column:eventType;uniqueIndex:ClassificationFeedback_emailAccountId_sender_ruleId_messageId_eventType_key;not null" json:"eventType"`
	RuleID         string   `gorm:"column:ruleId;uniqueIndex:ClassificationFeedback_emailAccountId_sender_ruleId_messageId_eventType_key;index:ClassificationFeedback_ruleId_idx;not null" json:"ruleId"`
	EmailAccountID string   `gorm:"column:emailAccountId;uniqueIndex:ClassificationFeedback_emailAccountId_sender_ruleId_messageId_eventType_key;not null" json:"emailAccountId"`
	ThreadID       string   `gorm:"column:threadId;not null" json:"threadId"`
	MessageID      string   `gorm:"column:messageId;uniqueIndex:ClassificationFeedback_emailAccountId_sender_ruleId_messageId_eventType_key;not null" json:"messageId"`
	Rule           *Rule
	EmailAccount   *EmailAccount
}

func (ClassificationFeedback) TableName() string {
	return "ClassificationFeedback"
}

func (m *ClassificationFeedback) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type Category struct {
	ID             string   `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt      DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt      DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Name           string   `gorm:"column:name;uniqueIndex:Category_name_emailAccountId_key;not null" json:"name"`
	Description    *string  `gorm:"column:description" json:"description"`
	EmailAccountID string   `gorm:"column:emailAccountId;uniqueIndex:Category_name_emailAccountId_key;index:Category_emailAccountId_idx;not null" json:"emailAccountId"`
	EmailAccount   *EmailAccount
	EmailSenders   []Newsletter `gorm:"foreignKey:CategoryID"`
	Rules          []Rule       `gorm:"many2many:_CategoryToRule;joinForeignKey:A;joinReferences:B"`
}

func (Category) TableName() string {
	return "Category"
}

func (m *Category) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*Category) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Newsletter struct {
	ID              string    `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt       DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt       DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Email           string    `gorm:"column:email;uniqueIndex:Newsletter_email_emailAccountId_key;not null" json:"email"`
	Name            *string   `gorm:"column:name" json:"name"`
	Status          *string   `gorm:"column:status;index:Newsletter_emailAccountId_status_idx" json:"status"`
	PatternAnalyzed bool      `gorm:"column:patternAnalyzed;default:false;not null" json:"patternAnalyzed"`
	LastAnalyzedAt  *DateTime `gorm:"column:lastAnalyzedAt" json:"lastAnalyzedAt"`
	EmailAccountID  string    `gorm:"column:emailAccountId;uniqueIndex:Newsletter_email_emailAccountId_key;index:Newsletter_emailAccountId_status_idx;not null" json:"emailAccountId"`
	CategoryID      *string   `gorm:"column:categoryId;index:Newsletter_categoryId_idx" json:"categoryId"`
	EmailAccount    *EmailAccount
	Category        *Category
}

func (Newsletter) TableName() string {
	return "Newsletter"
}

func (m *Newsletter) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*Newsletter) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type ColdEmail struct {
	ID             string   `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt      DateTime `gorm:"column:createdAt;index:ColdEmail_emailAccountId_createdAt_idx;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt      DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	FromEmail      string   `gorm:"column:fromEmail;uniqueIndex:ColdEmail_emailAccountId_fromEmail_key;not null" json:"fromEmail"`
	MessageID      *string  `gorm:"column:messageId" json:"messageId"`
	ThreadID       *string  `gorm:"column:threadId" json:"threadId"`
	Status         *string  `gorm:"column:status;index:ColdEmail_emailAccountId_status_idx" json:"status"`
	Reason         *string  `gorm:"column:reason" json:"reason"`
	EmailAccountID string   `gorm:"column:emailAccountId;uniqueIndex:ColdEmail_emailAccountId_fromEmail_key;index:ColdEmail_emailAccountId_status_idx;index:ColdEmail_emailAccountId_createdAt_idx;not null" json:"emailAccountId"`
	EmailAccount   *EmailAccount
	DigestItems    []DigestItem `gorm:"foreignKey:ColdEmailID"`
}

func (ColdEmail) TableName() string {
	return "ColdEmail"
}

func (m *ColdEmail) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*ColdEmail) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type EmailMessage struct {
	ID              string   `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt       DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt       DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	ThreadID        string   `gorm:"column:threadId;uniqueIndex:EmailMessage_emailAccountId_threadId_messageId_key;index:EmailMessage_emailAccountId_threadId_idx;not null" json:"threadId"`
	MessageID       string   `gorm:"column:messageId;uniqueIndex:EmailMessage_emailAccountId_threadId_messageId_key;not null" json:"messageId"`
	Date            DateTime `gorm:"column:date;index:EmailMessage_emailAccountId_date_idx;not null" json:"date"`
	From            string   `gorm:"column:from;index:EmailMessage_emailAccountId_from_idx;not null" json:"from"`
	FromName        *string  `gorm:"column:fromName;index:EmailMessage_emailAccountId_fromName_idx" json:"fromName"`
	FromDomain      string   `gorm:"column:fromDomain;not null" json:"fromDomain"`
	To              string   `gorm:"column:to;not null" json:"to"`
	UnsubscribeLink *string  `gorm:"column:unsubscribeLink" json:"unsubscribeLink"`
	Read            bool     `gorm:"column:read;not null" json:"read"`
	Sent            bool     `gorm:"column:sent;not null" json:"sent"`
	Draft           bool     `gorm:"column:draft;not null" json:"draft"`
	Inbox           bool     `gorm:"column:inbox;not null" json:"inbox"`
	EmailAccountID  string   `gorm:"column:emailAccountId;uniqueIndex:EmailMessage_emailAccountId_threadId_messageId_key;index:EmailMessage_emailAccountId_threadId_idx;index:EmailMessage_emailAccountId_date_idx;index:EmailMessage_emailAccountId_from_idx;index:EmailMessage_emailAccountId_fromName_idx;not null" json:"emailAccountId"`
	EmailAccount    *EmailAccount
}

func (EmailMessage) TableName() string {
	return "EmailMessage"
}

func (m *EmailMessage) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*EmailMessage) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type ResponseTime struct {
	ID                string   `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt         DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	ThreadID          string   `gorm:"column:threadId;not null" json:"threadId"`
	SentMessageID     string   `gorm:"column:sentMessageId;uniqueIndex:ResponseTime_emailAccountId_sentMessageId_key;not null" json:"sentMessageId"`
	ReceivedMessageID string   `gorm:"column:receivedMessageId;not null" json:"receivedMessageId"`
	ResponseTimeMins  int      `gorm:"column:responseTimeMins;not null" json:"responseTimeMins"`
	ReceivedAt        DateTime `gorm:"column:receivedAt;not null" json:"receivedAt"`
	SentAt            DateTime `gorm:"column:sentAt;index:ResponseTime_emailAccountId_sentAt_idx;not null" json:"sentAt"`
	EmailAccountID    string   `gorm:"column:emailAccountId;uniqueIndex:ResponseTime_emailAccountId_sentMessageId_key;index:ResponseTime_emailAccountId_sentAt_idx;not null" json:"emailAccountId"`
	EmailAccount      *EmailAccount
}

func (ResponseTime) TableName() string {
	return "ResponseTime"
}

func (m *ResponseTime) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type ThreadTracker struct {
	ID                     string         `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt              DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt              DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	SentAt                 DateTime       `gorm:"column:sentAt;index:ThreadTracker_emailAccountId_resolved_sentAt_type_idx;index:ThreadTracker_emailAccountId_type_resolved_sentAt_idx;index:ThreadTracker_emailAccountId_type_resolved_followUpAppliedAt_sentAt_idx;not null" json:"sentAt"`
	ThreadID               string         `gorm:"column:threadId;uniqueIndex:ThreadTracker_emailAccountId_threadId_messageId_key;not null" json:"threadId"`
	MessageID              string         `gorm:"column:messageId;uniqueIndex:ThreadTracker_emailAccountId_threadId_messageId_key;not null" json:"messageId"`
	Resolved               bool           `gorm:"column:resolved;index:ThreadTracker_emailAccountId_resolved_idx;index:ThreadTracker_emailAccountId_resolved_sentAt_type_idx;index:ThreadTracker_emailAccountId_type_resolved_sentAt_idx;index:ThreadTracker_emailAccountId_type_resolved_followUpAppliedAt_sentAt_idx;default:false;not null" json:"resolved"`
	Type                   string         `gorm:"column:type;index:ThreadTracker_emailAccountId_resolved_sentAt_type_idx;index:ThreadTracker_emailAccountId_type_resolved_sentAt_idx;index:ThreadTracker_emailAccountId_type_resolved_followUpAppliedAt_sentAt_idx;not null" json:"type"`
	FollowUpAppliedAt      *DateTime      `gorm:"column:followUpAppliedAt;index:ThreadTracker_emailAccountId_type_resolved_followUpAppliedAt_sentAt_idx" json:"followUpAppliedAt"`
	FollowUpDraftID        *string        `gorm:"column:followUpDraftId" json:"followUpDraftId"`
	FollowUpDraftContent   *string        `gorm:"column:followUpDraftContent" json:"followUpDraftContent"`
	FollowUpDraftCreatedAt *DateTime      `gorm:"column:followUpDraftCreatedAt;index:ThreadTracker_followUpDraft_cleanup_idx" json:"followUpDraftCreatedAt"`
	FollowUpNotifications  datatypes.JSON `gorm:"column:followUpNotifications" json:"followUpNotifications"`
	EmailAccountID         string         `gorm:"column:emailAccountId;uniqueIndex:ThreadTracker_emailAccountId_threadId_messageId_key;index:ThreadTracker_emailAccountId_resolved_idx;index:ThreadTracker_emailAccountId_resolved_sentAt_type_idx;index:ThreadTracker_emailAccountId_type_resolved_sentAt_idx;index:ThreadTracker_emailAccountId_type_resolved_followUpAppliedAt_sentAt_idx;index:ThreadTracker_followUpDraft_cleanup_idx;not null" json:"emailAccountId"`
	EmailAccount           *EmailAccount
}

func (ThreadTracker) TableName() string {
	return "ThreadTracker"
}

func (m *ThreadTracker) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*ThreadTracker) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type CleanupJob struct {
	ID               string   `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt        DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt        DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Action           string   `gorm:"column:action;default:'ARCHIVE';not null" json:"action"`
	DaysOld          *int     `gorm:"column:daysOld;default:7;not null" json:"daysOld"`
	Instructions     *string  `gorm:"column:instructions" json:"instructions"`
	SkipReply        *bool    `gorm:"column:skipReply" json:"skipReply"`
	SkipStarred      *bool    `gorm:"column:skipStarred" json:"skipStarred"`
	SkipCalendar     *bool    `gorm:"column:skipCalendar" json:"skipCalendar"`
	SkipReceipt      *bool    `gorm:"column:skipReceipt" json:"skipReceipt"`
	SkipAttachment   *bool    `gorm:"column:skipAttachment" json:"skipAttachment"`
	SkipConversation *bool    `gorm:"column:skipConversation" json:"skipConversation"`
	EmailAccountID   string   `gorm:"column:emailAccountId;index:CleanupJob_emailAccountId_idx;not null" json:"emailAccountId"`
	EmailAccount     *EmailAccount
	Threads          []CleanupThread `gorm:"foreignKey:JobID;constraint:OnDelete:CASCADE"`
}

func (CleanupJob) TableName() string {
	return "CleanupJob"
}

func (m *CleanupJob) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*CleanupJob) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type CleanupThread struct {
	ID             string      `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt      DateTime    `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt      DateTime    `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	ThreadID       string      `gorm:"column:threadId;not null" json:"threadId"`
	Archived       bool        `gorm:"column:archived;not null" json:"archived"`
	JobID          string      `gorm:"column:jobId;index:CleanupThread_jobId_idx;not null" json:"jobId"`
	EmailAccountID string      `gorm:"column:emailAccountId;index:CleanupThread_emailAccountId_idx;not null" json:"emailAccountId"`
	Job            *CleanupJob `gorm:"foreignKey:JobID"`
	EmailAccount   *EmailAccount
}

func (CleanupThread) TableName() string {
	return "CleanupThread"
}

func (m *CleanupThread) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*CleanupThread) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type BulkInboxActionJob struct {
	ID               string         `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt        DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt        DateTime       `gorm:"column:updatedAt;index:BulkInboxActionJob_chatId_updatedAt_idx;autoUpdateTime:false;not null" json:"updatedAt"`
	Status           string         `gorm:"column:status;default:'RUNNING';not null" json:"status"`
	Search           datatypes.JSON `gorm:"column:search;not null" json:"search"`
	Action           string         `gorm:"column:action;not null" json:"action"`
	LabelID          *string        `gorm:"column:labelId" json:"labelId"`
	LabelName        *string        `gorm:"column:labelName" json:"labelName"`
	Instruction      *string        `gorm:"column:instruction" json:"instruction"`
	PageToken        *string        `gorm:"column:pageToken" json:"pageToken"`
	RateLimitedUntil *DateTime      `gorm:"column:rateLimitedUntil" json:"rateLimitedUntil"`
	LeaseExpiresAt   *DateTime      `gorm:"column:leaseExpiresAt" json:"leaseExpiresAt"`
	ChunkToken       *string        `gorm:"column:chunkToken" json:"chunkToken"`
	RecoveryCount    int            `gorm:"column:recoveryCount;default:0;not null" json:"recoveryCount"`
	ProcessedCount   int            `gorm:"column:processedCount;default:0;not null" json:"processedCount"`
	MatchedCount     int            `gorm:"column:matchedCount;default:0;not null" json:"matchedCount"`
	ActedCount       int            `gorm:"column:actedCount;default:0;not null" json:"actedCount"`
	SkippedCount     int            `gorm:"column:skippedCount;default:0;not null" json:"skippedCount"`
	FailedCount      int            `gorm:"column:failedCount;default:0;not null" json:"failedCount"`
	LimitReached     bool           `gorm:"column:limitReached;default:false;not null" json:"limitReached"`
	Error            *string        `gorm:"column:error" json:"error"`
	CompletedAt      *DateTime      `gorm:"column:completedAt" json:"completedAt"`
	ConfirmationKey  string         `gorm:"column:confirmationKey;uniqueIndex:BulkInboxActionJob_emailAccountId_confirmationKey_key;not null" json:"confirmationKey"`
	ChatID           *string        `gorm:"column:chatId;index:BulkInboxActionJob_chatId_updatedAt_idx" json:"chatId"`
	EmailAccountID   string         `gorm:"column:emailAccountId;uniqueIndex:BulkInboxActionJob_emailAccountId_confirmationKey_key;not null" json:"emailAccountId"`
	Chat             *Chat
	EmailAccount     *EmailAccount
	HandledThreads   []BulkInboxActionThread `gorm:"foreignKey:JobID;constraint:OnDelete:CASCADE"`
}

func (BulkInboxActionJob) TableName() string {
	return "BulkInboxActionJob"
}

func (m *BulkInboxActionJob) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*BulkInboxActionJob) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type BulkInboxActionThread struct {
	JobID    string              `gorm:"column:jobId;primaryKey" json:"jobId"`
	ThreadID string              `gorm:"column:threadId;primaryKey" json:"threadId"`
	Job      *BulkInboxActionJob `gorm:"foreignKey:JobID"`
}

func (BulkInboxActionThread) TableName() string {
	return "BulkInboxActionThread"
}

type Knowledge struct {
	ID             string   `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt      DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt      DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Title          string   `gorm:"column:title;uniqueIndex:Knowledge_emailAccountId_title_key;not null" json:"title"`
	Content        string   `gorm:"column:content;not null" json:"content"`
	EmailAccountID string   `gorm:"column:emailAccountId;uniqueIndex:Knowledge_emailAccountId_title_key;not null" json:"emailAccountId"`
	EmailAccount   *EmailAccount
}

func (Knowledge) TableName() string {
	return "Knowledge"
}

func (m *Knowledge) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*Knowledge) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Snippet struct {
	ID             string   `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt      DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt      DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Shortcut       string   `gorm:"column:shortcut;uniqueIndex:Snippet_emailAccountId_shortcut_key;not null" json:"shortcut"`
	Content        string   `gorm:"column:content;not null" json:"content"`
	EmailAccountID string   `gorm:"column:emailAccountId;uniqueIndex:Snippet_emailAccountId_shortcut_key;index:Snippet_emailAccountId_idx;not null" json:"emailAccountId"`
	EmailAccount   *EmailAccount
}

func (Snippet) TableName() string {
	return "Snippet"
}

func (m *Snippet) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*Snippet) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type EmailSignature struct {
	ID             string   `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt      DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt      DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Name           string   `gorm:"column:name;uniqueIndex:EmailSignature_emailAccountId_name_key;not null" json:"name"`
	HTML           string   `gorm:"column:html;not null" json:"html"`
	EmailAccountID string   `gorm:"column:emailAccountId;uniqueIndex:EmailSignature_emailAccountId_name_key;not null" json:"emailAccountId"`
	EmailAccount   *EmailAccount
}

func (EmailSignature) TableName() string {
	return "EmailSignature"
}

func (m *EmailSignature) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*EmailSignature) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type ReplyMemory struct {
	ID                     string   `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt              DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt              DateTime `gorm:"column:updatedAt;index:ReplyMemory_emailAccountId_updatedAt_idx;autoUpdateTime:false;not null" json:"updatedAt"`
	Content                string   `gorm:"column:content;uniqueIndex:ReplyMemory_emailAccountId_kind_scopeType_scopeValue_content_key;not null" json:"content"`
	Kind                   string   `gorm:"column:kind;uniqueIndex:ReplyMemory_emailAccountId_kind_scopeType_scopeValue_content_key;not null" json:"kind"`
	ScopeType              string   `gorm:"column:scopeType;uniqueIndex:ReplyMemory_emailAccountId_kind_scopeType_scopeValue_content_key;index:ReplyMemory_emailAccountId_scopeType_scopeValue_idx;not null" json:"scopeType"`
	ScopeValue             string   `gorm:"column:scopeValue;uniqueIndex:ReplyMemory_emailAccountId_kind_scopeType_scopeValue_content_key;index:ReplyMemory_emailAccountId_scopeType_scopeValue_idx;not null" json:"scopeValue"`
	IsLearnedStyleEvidence bool     `gorm:"column:isLearnedStyleEvidence;default:false;not null" json:"isLearnedStyleEvidence"`
	EmailAccountID         string   `gorm:"column:emailAccountId;uniqueIndex:ReplyMemory_emailAccountId_kind_scopeType_scopeValue_content_key;index:ReplyMemory_emailAccountId_updatedAt_idx;index:ReplyMemory_emailAccountId_scopeType_scopeValue_idx;not null" json:"emailAccountId"`
	EmailAccount           *EmailAccount
	Sources                []ReplyMemorySource `gorm:"foreignKey:ReplyMemoryID;constraint:OnDelete:CASCADE"`
}

func (ReplyMemory) TableName() string {
	return "ReplyMemory"
}

func (m *ReplyMemory) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*ReplyMemory) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type ReplyMemorySource struct {
	CreatedAt                     DateTime  `gorm:"column:createdAt;index:ReplyMemorySource_replyMemoryId_learnedWritingStyleAnalyzedAt_createdAt_idx;autoCreateTime:false;not null" json:"createdAt"`
	LearnedWritingStyleAnalyzedAt *DateTime `gorm:"column:learnedWritingStyleAnalyzedAt;index:ReplyMemorySource_replyMemoryId_learnedWritingStyleAnalyzedAt_createdAt_idx" json:"learnedWritingStyleAnalyzedAt"`
	ReplyMemoryID                 string    `gorm:"column:replyMemoryId;primaryKey;index:ReplyMemorySource_replyMemoryId_learnedWritingStyleAnalyzedAt_createdAt_idx" json:"replyMemoryId"`
	DraftSendLogID                string    `gorm:"column:draftSendLogId;primaryKey;index:ReplyMemorySource_draftSendLogId_idx" json:"draftSendLogId"`
	ReplyMemory                   *ReplyMemory
	DraftSendLog                  *DraftSendLog
}

func (ReplyMemorySource) TableName() string {
	return "ReplyMemorySource"
}

func (m *ReplyMemorySource) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type APIKey struct {
	ID             string    `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt      DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt      DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Name           *string   `gorm:"column:name" json:"name"`
	HashedKey      string    `gorm:"column:hashedKey;uniqueIndex;not null" json:"hashedKey"`
	IsActive       *bool     `gorm:"column:isActive;index:ApiKey_userId_isActive_idx;index:ApiKey_emailAccountId_isActive_idx;default:true;not null" json:"isActive"`
	ExpiresAt      *DateTime `gorm:"column:expiresAt" json:"expiresAt"`
	LastUsedAt     *DateTime `gorm:"column:lastUsedAt" json:"lastUsedAt"`
	Scopes         []string  `gorm:"column:scopes;serializer:json;not null" json:"scopes"`
	UserID         string    `gorm:"column:userId;index:ApiKey_userId_isActive_idx;not null" json:"userId"`
	EmailAccountID *string   `gorm:"column:emailAccountId;index:ApiKey_emailAccountId_isActive_idx" json:"emailAccountId"`
	User           *User
	EmailAccount   *EmailAccount
}

func (APIKey) TableName() string {
	return "ApiKey"
}

func (m *APIKey) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*APIKey) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type EmailToken struct {
	ID             string   `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt      DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	Token          string   `gorm:"column:token;uniqueIndex;not null" json:"token"`
	ExpiresAt      DateTime `gorm:"column:expiresAt;not null" json:"expiresAt"`
	EmailAccountID string   `gorm:"column:emailAccountId;index:EmailToken_emailAccountId_idx;not null" json:"emailAccountId"`
	EmailAccount   *EmailAccount
}

func (EmailToken) TableName() string {
	return "EmailToken"
}

func (m *EmailToken) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type Payment struct {
	ID                      string    `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt               DateTime  `gorm:"column:createdAt;index:Payment_premiumId_createdAt_idx;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt               DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	PremiumID               *string   `gorm:"column:premiumId;index:Payment_premiumId_createdAt_idx" json:"premiumId"`
	ProcessorType           string    `gorm:"column:processorType;default:'LEMON_SQUEEZY';not null" json:"processorType"`
	ProcessorID             *string   `gorm:"column:processorId;uniqueIndex" json:"processorId"`
	ProcessorSubscriptionID *string   `gorm:"column:processorSubscriptionId" json:"processorSubscriptionId"`
	ProcessorCustomerID     *string   `gorm:"column:processorCustomerId" json:"processorCustomerId"`
	Amount                  int       `gorm:"column:amount;not null" json:"amount"`
	Currency                string    `gorm:"column:currency;not null" json:"currency"`
	Status                  string    `gorm:"column:status;not null" json:"status"`
	Tax                     int       `gorm:"column:tax;not null" json:"tax"`
	TaxInclusive            bool      `gorm:"column:taxInclusive;not null" json:"taxInclusive"`
	Refunded                bool      `gorm:"column:refunded;default:false;not null" json:"refunded"`
	RefundedAt              *DateTime `gorm:"column:refundedAt" json:"refundedAt"`
	RefundedAmount          *int      `gorm:"column:refundedAmount" json:"refundedAmount"`
	BillingReason           *string   `gorm:"column:billingReason" json:"billingReason"`
	InvoiceEmailSentAt      *DateTime `gorm:"column:invoiceEmailSentAt" json:"invoiceEmailSentAt"`
	Premium                 *Premium
}

func (Payment) TableName() string {
	return "Payment"
}

func (m *Payment) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = cuid.New()
	}
	return nil
}

type DraftSendLog struct {
	ID                      string         `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt               DateTime       `gorm:"column:createdAt;index:DraftSendLog_replyMemoryProcessedAt_replyMemoryAttemptCount_createdAt_idx;autoCreateTime:false;not null" json:"createdAt"`
	ExecutedActionID        string         `gorm:"column:executedActionId;uniqueIndex;index:DraftSendLog_executedActionId_idx;not null" json:"executedActionId"`
	SentMessageID           string         `gorm:"column:sentMessageId;not null" json:"sentMessageId"`
	SimilarityScore         float64        `gorm:"column:similarityScore;not null" json:"similarityScore"`
	BodySimilarityScore     *float64       `gorm:"column:bodySimilarityScore" json:"bodySimilarityScore"`
	BodySimilarityStatus    *string        `gorm:"column:bodySimilarityStatus" json:"bodySimilarityStatus"`
	SimilarityMetadata      datatypes.JSON `gorm:"column:similarityMetadata" json:"similarityMetadata"`
	SentText                *string        `gorm:"column:sentText" json:"sentText"`
	ReplyMemorySentText     *string        `gorm:"column:replyMemorySentText" json:"replyMemorySentText"`
	ReplyMemoryAttemptCount int            `gorm:"column:replyMemoryAttemptCount;index:DraftSendLog_replyMemoryProcessedAt_replyMemoryAttemptCount_createdAt_idx;default:0;not null" json:"replyMemoryAttemptCount"`
	ReplyMemoryProcessedAt  *DateTime      `gorm:"column:replyMemoryProcessedAt;index:DraftSendLog_replyMemoryProcessedAt_replyMemoryAttemptCount_createdAt_idx" json:"replyMemoryProcessedAt"`
	ExecutedAction          *ExecutedAction
	ReplyMemorySources      []ReplyMemorySource `gorm:"foreignKey:DraftSendLogID;constraint:OnDelete:CASCADE"`
}

func (DraftSendLog) TableName() string {
	return "DraftSendLog"
}

func (m *DraftSendLog) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type Chat struct {
	ID                    string    `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt             DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt             DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	DeletedAt             *DateTime `gorm:"column:deletedAt;index:Chat_deletedAt_idx" json:"deletedAt"`
	Name                  *string   `gorm:"column:name" json:"name"`
	CompactionCount       int       `gorm:"column:compactionCount;default:0;not null" json:"compactionCount"`
	LastSeenRulesRevision *int      `gorm:"column:lastSeenRulesRevision" json:"lastSeenRulesRevision"`
	ActiveStreamID        *string   `gorm:"column:activeStreamId" json:"activeStreamId"`
	ActiveStreamStartedAt *DateTime `gorm:"column:activeStreamStartedAt" json:"activeStreamStartedAt"`
	EmailAccountID        string    `gorm:"column:emailAccountId;index:Chat_emailAccountId_idx;not null" json:"emailAccountId"`
	EmailAccount          *EmailAccount
	Messages              []ChatMessage        `gorm:"foreignKey:ChatID;constraint:OnDelete:CASCADE"`
	Compactions           []ChatCompaction     `gorm:"foreignKey:ChatID;constraint:OnDelete:CASCADE"`
	Memories              []ChatMemory         `gorm:"foreignKey:ChatID;constraint:OnDelete:SET NULL"`
	ActionTotals          []ChatActionTotal    `gorm:"foreignKey:ChatID;constraint:OnDelete:CASCADE"`
	BulkInboxActionJobs   []BulkInboxActionJob `gorm:"foreignKey:ChatID;constraint:OnDelete:SET NULL"`
}

func (Chat) TableName() string {
	return "Chat"
}

func (m *Chat) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*Chat) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type ChatMessage struct {
	ID        string         `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Role      string         `gorm:"column:role;not null" json:"role"`
	Parts     datatypes.JSON `gorm:"column:parts;not null" json:"parts"`
	Metadata  datatypes.JSON `gorm:"column:metadata" json:"metadata"`
	ChatID    string         `gorm:"column:chatId;index:ChatMessage_chatId_idx;not null" json:"chatId"`
	Chat      *Chat
}

func (ChatMessage) TableName() string {
	return "ChatMessage"
}

func (m *ChatMessage) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*ChatMessage) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type ChatCompaction struct {
	ID                       string   `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt                DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	Summary                  string   `gorm:"column:summary;type:text;not null" json:"summary"`
	MessageCount             int      `gorm:"column:messageCount;not null" json:"messageCount"`
	CompactedBeforeCreatedAt DateTime `gorm:"column:compactedBeforeCreatedAt;not null" json:"compactedBeforeCreatedAt"`
	ChatID                   string   `gorm:"column:chatId;index:ChatCompaction_chatId_idx;not null" json:"chatId"`
	Chat                     *Chat
}

func (ChatCompaction) TableName() string {
	return "ChatCompaction"
}

func (m *ChatCompaction) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type ChatActionTotal struct {
	ID        string   `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Action    string   `gorm:"column:action;uniqueIndex:ChatActionTotal_chatId_action_key;not null" json:"action"`
	Count     int      `gorm:"column:count;default:0;not null" json:"count"`
	ChatID    string   `gorm:"column:chatId;uniqueIndex:ChatActionTotal_chatId_action_key;not null" json:"chatId"`
	Chat      *Chat
}

func (ChatActionTotal) TableName() string {
	return "ChatActionTotal"
}

func (m *ChatActionTotal) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*ChatActionTotal) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type ChatMemory struct {
	ID             string   `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt      DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt      DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Content        string   `gorm:"column:content;not null" json:"content"`
	ChatID         *string  `gorm:"column:chatId" json:"chatId"`
	EmailAccountID string   `gorm:"column:emailAccountId;index:ChatMemory_emailAccountId_idx;not null" json:"emailAccountId"`
	Chat           *Chat
	EmailAccount   *EmailAccount
}

func (ChatMemory) TableName() string {
	return "ChatMemory"
}

func (m *ChatMemory) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*ChatMemory) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type CalendarConnection struct {
	ID             string    `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt      DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt      DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Provider       string    `gorm:"column:provider;uniqueIndex:CalendarConnection_emailAccountId_provider_email_key;not null" json:"provider"`
	Email          string    `gorm:"column:email;uniqueIndex:CalendarConnection_emailAccountId_provider_email_key;not null" json:"email"`
	AccessToken    *string   `gorm:"column:accessToken" json:"accessToken"`
	RefreshToken   *string   `gorm:"column:refreshToken" json:"refreshToken"`
	ExpiresAt      *DateTime `gorm:"column:expiresAt" json:"expiresAt"`
	IsConnected    *bool     `gorm:"column:isConnected;default:true;not null" json:"isConnected"`
	EmailAccountID string    `gorm:"column:emailAccountId;uniqueIndex:CalendarConnection_emailAccountId_provider_email_key;not null" json:"emailAccountId"`
	EmailAccount   *EmailAccount
	Calendars      []Calendar `gorm:"foreignKey:ConnectionID;constraint:OnDelete:CASCADE"`
}

func (CalendarConnection) TableName() string {
	return "CalendarConnection"
}

func (m *CalendarConnection) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*CalendarConnection) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Calendar struct {
	ID           string              `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt    DateTime            `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt    DateTime            `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	CalendarID   string              `gorm:"column:calendarId;uniqueIndex:Calendar_connectionId_calendarId_key;not null" json:"calendarId"`
	Name         string              `gorm:"column:name;not null" json:"name"`
	Description  *string             `gorm:"column:description" json:"description"`
	Primary      bool                `gorm:"column:primary;default:false;not null" json:"primary"`
	IsEnabled    *bool               `gorm:"column:isEnabled;default:true;not null" json:"isEnabled"`
	Timezone     *string             `gorm:"column:timezone" json:"timezone"`
	ConnectionID string              `gorm:"column:connectionId;uniqueIndex:Calendar_connectionId_calendarId_key;not null" json:"connectionId"`
	Connection   *CalendarConnection `gorm:"foreignKey:ConnectionID"`
	BookingLinks []BookingLink       `gorm:"foreignKey:DestinationCalendarID;constraint:OnDelete:SET NULL"`
}

func (Calendar) TableName() string {
	return "Calendar"
}

func (m *Calendar) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*Calendar) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type BookingLink struct {
	ID                     string   `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt              DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt              DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Slug                   string   `gorm:"column:slug;uniqueIndex;not null" json:"slug"`
	Title                  string   `gorm:"column:title;not null" json:"title"`
	Description            *string  `gorm:"column:description" json:"description"`
	IsActive               *bool    `gorm:"column:isActive;default:true;not null" json:"isActive"`
	DurationMinutes        int      `gorm:"column:durationMinutes;not null" json:"durationMinutes"`
	SlotIntervalMinutes    int      `gorm:"column:slotIntervalMinutes;not null" json:"slotIntervalMinutes"`
	LocationType           string   `gorm:"column:locationType;default:'CUSTOM';not null" json:"locationType"`
	LocationValue          *string  `gorm:"column:locationValue" json:"locationValue"`
	MinimumNoticeMinutes   *int     `gorm:"column:minimumNoticeMinutes;default:120;not null" json:"minimumNoticeMinutes"`
	MaxDaysAhead           *int     `gorm:"column:maxDaysAhead;default:90;not null" json:"maxDaysAhead"`
	EmailAccountID         string   `gorm:"column:emailAccountId;uniqueIndex;not null" json:"emailAccountId"`
	DestinationCalendarID  *string  `gorm:"column:destinationCalendarId;index:BookingLink_destinationCalendarId_idx" json:"destinationCalendarId"`
	AvailabilityScheduleID string   `gorm:"column:availabilityScheduleId;index:BookingLink_availabilityScheduleId_idx;not null" json:"availabilityScheduleId"`
	EmailAccount           *EmailAccount
	DestinationCalendar    *Calendar             `gorm:"foreignKey:DestinationCalendarID"`
	AvailabilitySchedule   *AvailabilitySchedule `gorm:"foreignKey:AvailabilityScheduleID,EmailAccountID;references:ID,EmailAccountID"`
	Bookings               []Booking             `gorm:"foreignKey:BookingLinkID;constraint:OnDelete:CASCADE"`
}

func (BookingLink) TableName() string {
	return "BookingLink"
}

func (m *BookingLink) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*BookingLink) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type AvailabilitySchedule struct {
	ID             string   `gorm:"column:id;primaryKey;uniqueIndex:AvailabilitySchedule_id_emailAccountId_key" json:"id"`
	CreatedAt      DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt      DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Name           string   `gorm:"column:name;not null" json:"name"`
	IsDefault      bool     `gorm:"column:isDefault;index:AvailabilitySchedule_emailAccountId_isDefault_idx;default:false;not null" json:"isDefault"`
	Timezone       *string  `gorm:"column:timezone;default:'UTC';not null" json:"timezone"`
	EmailAccountID string   `gorm:"column:emailAccountId;uniqueIndex:AvailabilitySchedule_id_emailAccountId_key;index:AvailabilitySchedule_emailAccountId_idx;index:AvailabilitySchedule_emailAccountId_isDefault_idx;not null" json:"emailAccountId"`
	EmailAccount   *EmailAccount
	Windows        []AvailabilityWindow `gorm:"foreignKey:AvailabilityScheduleID;constraint:OnDelete:CASCADE"`
	BookingLinks   []BookingLink        `gorm:"foreignKey:AvailabilityScheduleID,EmailAccountID;references:ID,EmailAccountID;constraint:OnDelete:NO ACTION"`
}

func (AvailabilitySchedule) TableName() string {
	return "AvailabilitySchedule"
}

func (m *AvailabilitySchedule) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*AvailabilitySchedule) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type AvailabilityWindow struct {
	ID                     string   `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt              DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt              DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Weekday                int      `gorm:"column:weekday;not null" json:"weekday"`
	StartMinutes           int      `gorm:"column:startMinutes;not null" json:"startMinutes"`
	EndMinutes             int      `gorm:"column:endMinutes;not null" json:"endMinutes"`
	AvailabilityScheduleID string   `gorm:"column:availabilityScheduleId;index:AvailabilityWindow_availabilityScheduleId_idx;not null" json:"availabilityScheduleId"`
	AvailabilitySchedule   *AvailabilitySchedule
}

func (AvailabilityWindow) TableName() string {
	return "AvailabilityWindow"
}

func (m *AvailabilityWindow) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*AvailabilityWindow) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Booking struct {
	ID                   string   `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt            DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt            DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	GuestName            string   `gorm:"column:guestName;not null" json:"guestName"`
	GuestEmail           string   `gorm:"column:guestEmail;not null" json:"guestEmail"`
	GuestNote            *string  `gorm:"column:guestNote" json:"guestNote"`
	StartTime            DateTime `gorm:"column:startTime;index:Booking_bookingLinkId_startTime_endTime_idx;not null" json:"startTime"`
	EndTime              DateTime `gorm:"column:endTime;index:Booking_bookingLinkId_startTime_endTime_idx;not null" json:"endTime"`
	Status               string   `gorm:"column:status;default:'PENDING_PROVIDER_EVENT';not null" json:"status"`
	Provider             *string  `gorm:"column:provider" json:"provider"`
	ProviderConnectionID *string  `gorm:"column:providerConnectionId" json:"providerConnectionId"`
	ProviderCalendarID   *string  `gorm:"column:providerCalendarId" json:"providerCalendarId"`
	ProviderEventID      *string  `gorm:"column:providerEventId" json:"providerEventId"`
	VideoConferenceLink  *string  `gorm:"column:videoConferenceLink" json:"videoConferenceLink"`
	CancelTokenHash      string   `gorm:"column:cancelTokenHash;not null" json:"cancelTokenHash"`
	CancellationReason   *string  `gorm:"column:cancellationReason" json:"cancellationReason"`
	IdempotencyToken     *string  `gorm:"column:idempotencyToken;uniqueIndex:Booking_bookingLinkId_idempotencyToken_key" json:"idempotencyToken"`
	BookingLinkID        string   `gorm:"column:bookingLinkId;uniqueIndex:Booking_bookingLinkId_idempotencyToken_key;index:Booking_bookingLinkId_startTime_endTime_idx;not null" json:"bookingLinkId"`
	EmailAccountID       string   `gorm:"column:emailAccountId;index:Booking_emailAccountId_idx;not null" json:"emailAccountId"`
	BookingLink          *BookingLink
	EmailAccount         *EmailAccount
}

func (Booking) TableName() string {
	return "Booking"
}

func (m *Booking) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*Booking) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type MeetingBriefing struct {
	ID              string   `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt       DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	CalendarEventID string   `gorm:"column:calendarEventId;uniqueIndex:MeetingBriefing_emailAccountId_calendarEventId_key;not null" json:"calendarEventId"`
	EventTitle      string   `gorm:"column:eventTitle;not null" json:"eventTitle"`
	EventStartTime  DateTime `gorm:"column:eventStartTime;not null" json:"eventStartTime"`
	GuestCount      int      `gorm:"column:guestCount;not null" json:"guestCount"`
	Status          string   `gorm:"column:status;not null" json:"status"`
	EmailAccountID  string   `gorm:"column:emailAccountId;uniqueIndex:MeetingBriefing_emailAccountId_calendarEventId_key;index:MeetingBriefing_emailAccountId_idx;not null" json:"emailAccountId"`
	EmailAccount    *EmailAccount
}

func (MeetingBriefing) TableName() string {
	return "MeetingBriefing"
}

func (m *MeetingBriefing) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type ContactResearch struct {
	ID                string         `gorm:"column:id;primaryKey;index:ContactResearch_latest_idx" json:"id"`
	CreatedAt         DateTime       `gorm:"column:createdAt;index:ContactResearch_latest_idx;autoCreateTime:false;not null" json:"createdAt"`
	Email             string         `gorm:"column:email;index:ContactResearch_latest_idx;not null" json:"email"`
	ResearchStartedAt DateTime       `gorm:"column:researchStartedAt;index:ContactResearch_latest_idx;not null" json:"researchStartedAt"`
	Found             bool           `gorm:"column:found;not null" json:"found"`
	Role              *string        `gorm:"column:role" json:"role"`
	Confidence        *string        `gorm:"column:confidence" json:"confidence"`
	Company           datatypes.JSON `gorm:"column:company" json:"company"`
	Sources           []string       `gorm:"column:sources;serializer:json;not null" json:"sources"`
}

func (ContactResearch) TableName() string {
	return "ContactResearch"
}

func (m *ContactResearch) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type MeetingRecording struct {
	ID                    string         `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt             DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt             DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	BotProvider           *string        `gorm:"column:botProvider;uniqueIndex:MeetingRecording_botProvider_externalBotId_key;default:'recall';not null" json:"botProvider"`
	ExternalBotID         *string        `gorm:"column:externalBotId;uniqueIndex:MeetingRecording_botProvider_externalBotId_key" json:"externalBotId"`
	BotName               *string        `gorm:"column:botName" json:"botName"`
	ExternalRecordingID   *string        `gorm:"column:externalRecordingId" json:"externalRecordingId"`
	TranscriptRequestedAt *DateTime      `gorm:"column:transcriptRequestedAt" json:"transcriptRequestedAt"`
	ExternalTranscriptID  *string        `gorm:"column:externalTranscriptId" json:"externalTranscriptId"`
	MeetingURL            string         `gorm:"column:meetingUrl;not null" json:"meetingUrl"`
	NormalizedMeetingURL  string         `gorm:"column:normalizedMeetingUrl;index:MeetingRecording_normalizedMeetingUrl_meetingStartTime_idx;not null" json:"normalizedMeetingUrl"`
	ActiveKey             *string        `gorm:"column:activeKey;uniqueIndex:MeetingRecording_activeKey_meetingStartTime_key" json:"activeKey"`
	MeetingStartTime      DateTime       `gorm:"column:meetingStartTime;uniqueIndex:MeetingRecording_activeKey_meetingStartTime_key;index:MeetingRecording_normalizedMeetingUrl_meetingStartTime_idx;index:MeetingRecording_status_meetingStartTime_idx;not null" json:"meetingStartTime"`
	Status                string         `gorm:"column:status;index:MeetingRecording_status_meetingStartTime_idx;index:MeetingRecording_status_mediaDeletedAt_idx;default:'PENDING';not null" json:"status"`
	FailureReason         *string        `gorm:"column:failureReason" json:"failureReason"`
	Transcript            datatypes.JSON `gorm:"column:transcript" json:"transcript"`
	TranscriptFetchedAt   *DateTime      `gorm:"column:transcriptFetchedAt" json:"transcriptFetchedAt"`
	MediaDeletedAt        *DateTime      `gorm:"column:mediaDeletedAt;index:MeetingRecording_status_mediaDeletedAt_idx" json:"mediaDeletedAt"`
	EmailAccountID        string         `gorm:"column:emailAccountId;index:MeetingRecording_emailAccountId_idx;not null" json:"emailAccountId"`
	EmailAccount          *EmailAccount
	Meetings              []Meeting `gorm:"foreignKey:RecordingID;constraint:OnDelete:SET NULL"`
}

func (MeetingRecording) TableName() string {
	return "MeetingRecording"
}

func (m *MeetingRecording) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*MeetingRecording) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Meeting struct {
	ID                     string            `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt              DateTime          `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt              DateTime          `gorm:"column:updatedAt;index:Meeting_processingStatus_updatedAt_idx;autoUpdateTime:false;not null" json:"updatedAt"`
	CalendarEventID        string            `gorm:"column:calendarEventId;uniqueIndex:Meeting_emailAccountId_calendarEventId_key;not null" json:"calendarEventId"`
	EventTitle             string            `gorm:"column:eventTitle;not null" json:"eventTitle"`
	StartTime              DateTime          `gorm:"column:startTime;index:Meeting_emailAccountId_startTime_idx;not null" json:"startTime"`
	EndTime                DateTime          `gorm:"column:endTime;not null" json:"endTime"`
	Attendees              datatypes.JSON    `gorm:"column:attendees;not null" json:"attendees"`
	OrganizerEmail         *string           `gorm:"column:organizerEmail" json:"organizerEmail"`
	JoinOverride           *bool             `gorm:"column:joinOverride" json:"joinOverride"`
	ProcessingStatus       string            `gorm:"column:processingStatus;index:Meeting_processingStatus_updatedAt_idx;default:'PENDING';not null" json:"processingStatus"`
	ProcessingAttempts     int               `gorm:"column:processingAttempts;default:0;not null" json:"processingAttempts"`
	Summary                datatypes.JSON    `gorm:"column:summary" json:"summary"`
	FollowUpDraftStartedAt *DateTime         `gorm:"column:followUpDraftStartedAt" json:"followUpDraftStartedAt"`
	FollowUpDraftID        *string           `gorm:"column:followUpDraftId" json:"followUpDraftId"`
	RecapSentAt            *DateTime         `gorm:"column:recapSentAt" json:"recapSentAt"`
	RecordingID            *string           `gorm:"column:recordingId;uniqueIndex:Meeting_emailAccountId_recordingId_key;index:Meeting_recordingId_idx" json:"recordingId"`
	EmailAccountID         string            `gorm:"column:emailAccountId;uniqueIndex:Meeting_emailAccountId_calendarEventId_key;uniqueIndex:Meeting_emailAccountId_recordingId_key;index:Meeting_emailAccountId_startTime_idx;not null" json:"emailAccountId"`
	Recording              *MeetingRecording `gorm:"foreignKey:RecordingID"`
	EmailAccount           *EmailAccount
}

func (Meeting) TableName() string {
	return "Meeting"
}

func (m *Meeting) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*Meeting) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type MessagingChannel struct {
	ID              string    `gorm:"column:id;primaryKey;uniqueIndex:MessagingChannel_id_emailAccountId_key" json:"id"`
	CreatedAt       DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt       DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Provider        string    `gorm:"column:provider;uniqueIndex:MessagingChannel_emailAccountId_provider_teamId_key;index:MessagingChannel_provider_teamId_idx;not null" json:"provider"`
	IsConnected     *bool     `gorm:"column:isConnected;default:true;not null" json:"isConnected"`
	TeamID          string    `gorm:"column:teamId;uniqueIndex:MessagingChannel_emailAccountId_provider_teamId_key;index:MessagingChannel_provider_teamId_idx;not null" json:"teamId"`
	TeamName        *string   `gorm:"column:teamName" json:"teamName"`
	ProviderUserID  *string   `gorm:"column:providerUserId" json:"providerUserId"`
	BotUserID       *string   `gorm:"column:botUserId" json:"botUserId"`
	AccessToken     *string   `gorm:"column:accessToken" json:"accessToken"`
	RefreshToken    *string   `gorm:"column:refreshToken" json:"refreshToken"`
	ExpiresAt       *DateTime `gorm:"column:expiresAt" json:"expiresAt"`
	EmailAccountID  string    `gorm:"column:emailAccountId;uniqueIndex:MessagingChannel_emailAccountId_provider_teamId_key;uniqueIndex:MessagingChannel_id_emailAccountId_key;index:MessagingChannel_emailAccountId_idx;not null" json:"emailAccountId"`
	EmailAccount    *EmailAccount
	Routes          []MessagingRoute `gorm:"foreignKey:MessagingChannelID;constraint:OnDelete:CASCADE"`
	AutomationJobs  []AutomationJob  `gorm:"foreignKey:MessagingChannelID,EmailAccountID;references:ID,EmailAccountID;constraint:OnDelete:CASCADE"`
	Actions         []Action         `gorm:"foreignKey:MessagingChannelID,MessagingChannelEmailAccountID;references:ID,EmailAccountID;constraint:OnDelete:SET NULL"`
	ExecutedActions []ExecutedAction `gorm:"foreignKey:MessagingChannelID;constraint:OnDelete:SET NULL"`
}

func (MessagingChannel) TableName() string {
	return "MessagingChannel"
}

func (m *MessagingChannel) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*MessagingChannel) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type MessagingRoute struct {
	ID                 string   `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt          DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt          DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Purpose            string   `gorm:"column:purpose;uniqueIndex:MessagingRoute_messagingChannelId_purpose_key;not null" json:"purpose"`
	TargetType         string   `gorm:"column:targetType;not null" json:"targetType"`
	TargetID           string   `gorm:"column:targetId;not null" json:"targetId"`
	MessagingChannelID string   `gorm:"column:messagingChannelId;uniqueIndex:MessagingRoute_messagingChannelId_purpose_key;not null" json:"messagingChannelId"`
	MessagingChannel   *MessagingChannel
}

func (MessagingRoute) TableName() string {
	return "MessagingRoute"
}

func (m *MessagingRoute) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*MessagingRoute) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type AutomationJob struct {
	ID                 string             `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt          DateTime           `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt          DateTime           `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Name               string             `gorm:"column:name;not null" json:"name"`
	Enabled            *bool              `gorm:"column:enabled;index:AutomationJob_emailAccountId_enabled_nextRunAt_idx;index:AutomationJob_enabled_nextRunAt_idx;default:true;not null" json:"enabled"`
	Prompt             *string            `gorm:"column:prompt;type:text" json:"prompt"`
	CronExpression     string             `gorm:"column:cronExpression;not null" json:"cronExpression"`
	NextRunAt          DateTime           `gorm:"column:nextRunAt;index:AutomationJob_emailAccountId_enabled_nextRunAt_idx;index:AutomationJob_enabled_nextRunAt_idx;not null" json:"nextRunAt"`
	MessagingChannelID string             `gorm:"column:messagingChannelId;index:AutomationJob_messagingChannelId_emailAccountId_idx;not null" json:"messagingChannelId"`
	EmailAccountID     string             `gorm:"column:emailAccountId;uniqueIndex;index:AutomationJob_emailAccountId_enabled_nextRunAt_idx;index:AutomationJob_messagingChannelId_emailAccountId_idx;not null" json:"emailAccountId"`
	MessagingChannel   *MessagingChannel  `gorm:"foreignKey:MessagingChannelID,EmailAccountID;references:ID,EmailAccountID"`
	Runs               []AutomationJobRun `gorm:"foreignKey:AutomationJobID;constraint:OnDelete:CASCADE"`
}

func (AutomationJob) TableName() string {
	return "AutomationJob"
}

func (m *AutomationJob) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*AutomationJob) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type AutomationJobRun struct {
	ID                string    `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt         DateTime  `gorm:"column:createdAt;index:AutomationJobRun_automationJobId_createdAt_idx;index:AutomationJobRun_status_createdAt_idx;autoCreateTime:false;not null" json:"createdAt"`
	Status            string    `gorm:"column:status;index:AutomationJobRun_status_createdAt_idx;default:'PENDING';not null" json:"status"`
	ScheduledFor      DateTime  `gorm:"column:scheduledFor;uniqueIndex:AutomationJobRun_automationJobId_scheduledFor_key;not null" json:"scheduledFor"`
	ProcessedAt       *DateTime `gorm:"column:processedAt" json:"processedAt"`
	OutboundMessage   *string   `gorm:"column:outboundMessage;type:text" json:"outboundMessage"`
	ProviderMessageID *string   `gorm:"column:providerMessageId" json:"providerMessageId"`
	Error             *string   `gorm:"column:error;type:text" json:"error"`
	AutomationJobID   string    `gorm:"column:automationJobId;uniqueIndex:AutomationJobRun_automationJobId_scheduledFor_key;index:AutomationJobRun_automationJobId_createdAt_idx;not null" json:"automationJobId"`
	AutomationJob     *AutomationJob
}

func (AutomationJobRun) TableName() string {
	return "AutomationJobRun"
}

func (m *AutomationJobRun) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type DriveConnection struct {
	ID                string    `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt         DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt         DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Provider          string    `gorm:"column:provider;uniqueIndex:DriveConnection_emailAccountId_provider_key;not null" json:"provider"`
	Email             string    `gorm:"column:email;not null" json:"email"`
	AccessToken       *string   `gorm:"column:accessToken" json:"accessToken"`
	RefreshToken      *string   `gorm:"column:refreshToken" json:"refreshToken"`
	ExpiresAt         *DateTime `gorm:"column:expiresAt" json:"expiresAt"`
	IsConnected       *bool     `gorm:"column:isConnected;default:true;not null" json:"isConnected"`
	EmailAccountID    string    `gorm:"column:emailAccountId;uniqueIndex:DriveConnection_emailAccountId_provider_key;index:DriveConnection_emailAccountId_idx;not null" json:"emailAccountId"`
	EmailAccount      *EmailAccount
	DocumentFilings   []DocumentFiling   `gorm:"foreignKey:DriveConnectionID;constraint:OnDelete:CASCADE"`
	FilingFolders     []FilingFolder     `gorm:"foreignKey:DriveConnectionID;constraint:OnDelete:CASCADE"`
	AttachmentSources []AttachmentSource `gorm:"foreignKey:DriveConnectionID;constraint:OnDelete:CASCADE"`
}

func (DriveConnection) TableName() string {
	return "DriveConnection"
}

func (m *DriveConnection) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*DriveConnection) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type FilingFolder struct {
	ID                string   `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt         DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt         DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	FolderID          string   `gorm:"column:folderId;uniqueIndex:FilingFolder_emailAccountId_folderId_key;not null" json:"folderId"`
	FolderName        string   `gorm:"column:folderName;not null" json:"folderName"`
	FolderPath        string   `gorm:"column:folderPath;not null" json:"folderPath"`
	DriveConnectionID string   `gorm:"column:driveConnectionId;index:FilingFolder_driveConnectionId_idx;not null" json:"driveConnectionId"`
	EmailAccountID    string   `gorm:"column:emailAccountId;uniqueIndex:FilingFolder_emailAccountId_folderId_key;not null" json:"emailAccountId"`
	DriveConnection   *DriveConnection
	EmailAccount      *EmailAccount
}

func (FilingFolder) TableName() string {
	return "FilingFolder"
}

func (m *FilingFolder) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*FilingFolder) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type DocumentFiling struct {
	ID                    string    `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt             DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt             DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	MessageID             string    `gorm:"column:messageId;uniqueIndex:DocumentFiling_emailAccountId_messageId_attachmentId_key;index:DocumentFiling_messageId_idx;not null" json:"messageId"`
	AttachmentID          string    `gorm:"column:attachmentId;uniqueIndex:DocumentFiling_emailAccountId_messageId_attachmentId_key;not null" json:"attachmentId"`
	Filename              string    `gorm:"column:filename;not null" json:"filename"`
	FolderID              *string   `gorm:"column:folderId" json:"folderId"`
	FolderPath            string    `gorm:"column:folderPath;not null" json:"folderPath"`
	FileID                *string   `gorm:"column:fileId" json:"fileId"`
	Reasoning             *string   `gorm:"column:reasoning" json:"reasoning"`
	Confidence            *float64  `gorm:"column:confidence" json:"confidence"`
	Status                string    `gorm:"column:status;index:DocumentFiling_emailAccountId_status_idx;default:'FILED';not null" json:"status"`
	WasAsked              bool      `gorm:"column:wasAsked;default:false;not null" json:"wasAsked"`
	WasCorrected          bool      `gorm:"column:wasCorrected;default:false;not null" json:"wasCorrected"`
	OriginalPath          *string   `gorm:"column:originalPath" json:"originalPath"`
	CorrectedAt           *DateTime `gorm:"column:correctedAt" json:"correctedAt"`
	FeedbackPositive      *bool     `gorm:"column:feedbackPositive" json:"feedbackPositive"`
	FeedbackAt            *DateTime `gorm:"column:feedbackAt" json:"feedbackAt"`
	NotificationMessageID *string   `gorm:"column:notificationMessageId;uniqueIndex" json:"notificationMessageId"`
	NotificationSentAt    *DateTime `gorm:"column:notificationSentAt" json:"notificationSentAt"`
	NotificationBatchID   *string   `gorm:"column:notificationBatchId;index:DocumentFiling_emailAccountId_notificationBatchId_idx" json:"notificationBatchId"`
	DriveConnectionID     string    `gorm:"column:driveConnectionId;index:DocumentFiling_driveConnectionId_idx;not null" json:"driveConnectionId"`
	EmailAccountID        string    `gorm:"column:emailAccountId;uniqueIndex:DocumentFiling_emailAccountId_messageId_attachmentId_key;index:DocumentFiling_emailAccountId_status_idx;index:DocumentFiling_emailAccountId_notificationBatchId_idx;not null" json:"emailAccountId"`
	DriveConnection       *DriveConnection
	EmailAccount          *EmailAccount
}

func (DocumentFiling) TableName() string {
	return "DocumentFiling"
}

func (m *DocumentFiling) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*DocumentFiling) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type AttachmentSource struct {
	ID                string   `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt         DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt         DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Name              string   `gorm:"column:name;not null" json:"name"`
	Type              string   `gorm:"column:type;uniqueIndex:AttachmentSource_ruleId_driveConnectionId_type_sourceId_key;not null" json:"type"`
	SourceID          string   `gorm:"column:sourceId;uniqueIndex:AttachmentSource_ruleId_driveConnectionId_type_sourceId_key;not null" json:"sourceId"`
	SourcePath        *string  `gorm:"column:sourcePath" json:"sourcePath"`
	RuleID            string   `gorm:"column:ruleId;uniqueIndex:AttachmentSource_ruleId_driveConnectionId_type_sourceId_key;index:AttachmentSource_ruleId_idx;not null" json:"ruleId"`
	DriveConnectionID string   `gorm:"column:driveConnectionId;uniqueIndex:AttachmentSource_ruleId_driveConnectionId_type_sourceId_key;index:AttachmentSource_driveConnectionId_idx;not null" json:"driveConnectionId"`
	Rule              *Rule
	DriveConnection   *DriveConnection
	Documents         []AttachmentDocument `gorm:"foreignKey:AttachmentSourceID;constraint:OnDelete:CASCADE"`
}

func (AttachmentSource) TableName() string {
	return "AttachmentSource"
}

func (m *AttachmentSource) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*AttachmentSource) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type AttachmentDocument struct {
	ID                 string         `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt          DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt          DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	AttachmentSourceID string         `gorm:"column:attachmentSourceId;uniqueIndex:AttachmentDocument_attachmentSourceId_fileId_key;index:AttachmentDocument_attachmentSourceId_modifiedAt_idx;not null" json:"attachmentSourceId"`
	FileID             string         `gorm:"column:fileId;uniqueIndex:AttachmentDocument_attachmentSourceId_fileId_key;not null" json:"fileId"`
	Name               string         `gorm:"column:name;not null" json:"name"`
	MimeType           string         `gorm:"column:mimeType;not null" json:"mimeType"`
	ModifiedAt         *DateTime      `gorm:"column:modifiedAt;index:AttachmentDocument_attachmentSourceId_modifiedAt_idx" json:"modifiedAt"`
	Summary            *string        `gorm:"column:summary;type:text" json:"summary"`
	Content            *string        `gorm:"column:content;type:text" json:"content"`
	Metadata           datatypes.JSON `gorm:"column:metadata" json:"metadata"`
	IndexedAt          *DateTime      `gorm:"column:indexedAt" json:"indexedAt"`
	Error              *string        `gorm:"column:error;type:text" json:"error"`
	AttachmentSource   *AttachmentSource
}

func (AttachmentDocument) TableName() string {
	return "AttachmentDocument"
}

func (m *AttachmentDocument) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*AttachmentDocument) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Referral struct {
	ID                         string    `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt                  DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt                  DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	ReferrerUserID             string    `gorm:"column:referrerUserId;index:Referral_referrerUserId_idx;not null" json:"referrerUserId"`
	ReferredUserID             string    `gorm:"column:referredUserId;uniqueIndex;not null" json:"referredUserId"`
	ReferralCodeUsed           string    `gorm:"column:referralCodeUsed;not null" json:"referralCodeUsed"`
	Status                     string    `gorm:"column:status;index:Referral_status_idx;default:'PENDING';not null" json:"status"`
	RewardGrantedAt            *DateTime `gorm:"column:rewardGrantedAt" json:"rewardGrantedAt"`
	StripeBalanceTransactionID *string   `gorm:"column:stripeBalanceTransactionId" json:"stripeBalanceTransactionId"`
	RewardAmount               *int      `gorm:"column:rewardAmount" json:"rewardAmount"`
	ReferrerUser               *User     `gorm:"foreignKey:ReferrerUserID"`
	ReferredUser               *User     `gorm:"foreignKey:ReferredUserID"`
}

func (Referral) TableName() string {
	return "Referral"
}

func (m *Referral) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*Referral) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type McpIntegration struct {
	ID                         string   `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt                  DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt                  DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Name                       string   `gorm:"column:name;uniqueIndex;not null" json:"name"`
	RegisteredServerURL        *string  `gorm:"column:registeredServerUrl" json:"registeredServerUrl"`
	RegisteredAuthorizationURL *string  `gorm:"column:registeredAuthorizationUrl" json:"registeredAuthorizationUrl"`
	RegisteredTokenURL         *string  `gorm:"column:registeredTokenUrl" json:"registeredTokenUrl"`
	OauthClientID              *string  `gorm:"column:oauthClientId" json:"oauthClientId"`
	OauthClientSecret          *string  `gorm:"column:oauthClientSecret" json:"oauthClientSecret"`
	DisplayName                *string  `gorm:"column:displayName" json:"displayName"`
	ServerURL                  *string  `gorm:"column:serverUrl" json:"serverUrl"`
	AuthType                   *string  `gorm:"column:authType" json:"authType"`
	EmailAccountID             *string  `gorm:"column:emailAccountId;index:McpIntegration_emailAccountId_idx" json:"emailAccountId"`
	EmailAccount               *EmailAccount
	Connections                []McpConnection `gorm:"foreignKey:IntegrationID;constraint:OnDelete:CASCADE"`
}

func (McpIntegration) TableName() string {
	return "McpIntegration"
}

func (m *McpIntegration) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*McpIntegration) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type McpConnection struct {
	ID             string          `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt      DateTime        `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt      DateTime        `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Name           string          `gorm:"column:name;not null" json:"name"`
	IsActive       *bool           `gorm:"column:isActive;default:true;not null" json:"isActive"`
	AccessToken    *string         `gorm:"column:accessToken" json:"accessToken"`
	RefreshToken   *string         `gorm:"column:refreshToken" json:"refreshToken"`
	APIKey         *string         `gorm:"column:apiKey" json:"apiKey"`
	ExpiresAt      *DateTime       `gorm:"column:expiresAt" json:"expiresAt"`
	IntegrationID  string          `gorm:"column:integrationId;uniqueIndex:McpConnection_emailAccountId_integrationId_key;not null" json:"integrationId"`
	EmailAccountID *string         `gorm:"column:emailAccountId;uniqueIndex:McpConnection_emailAccountId_integrationId_key" json:"emailAccountId"`
	Integration    *McpIntegration `gorm:"foreignKey:IntegrationID"`
	EmailAccount   *EmailAccount
	Tools          []McpTool `gorm:"foreignKey:ConnectionID;constraint:OnDelete:CASCADE"`
}

func (McpConnection) TableName() string {
	return "McpConnection"
}

func (m *McpConnection) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*McpConnection) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type McpTool struct {
	ID           string         `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt    DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt    DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Name         string         `gorm:"column:name;uniqueIndex:McpTool_connectionId_name_key;not null" json:"name"`
	Description  *string        `gorm:"column:description" json:"description"`
	Schema       datatypes.JSON `gorm:"column:schema" json:"schema"`
	IsEnabled    *bool          `gorm:"column:isEnabled;default:true;not null" json:"isEnabled"`
	IsWrite      bool           `gorm:"column:isWrite;default:false;not null" json:"isWrite"`
	ConnectionID string         `gorm:"column:connectionId;uniqueIndex:McpTool_connectionId_name_key;not null" json:"connectionId"`
	Connection   *McpConnection `gorm:"foreignKey:ConnectionID"`
}

func (McpTool) TableName() string {
	return "McpTool"
}

func (m *McpTool) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*McpTool) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type MobilePushToken struct {
	ID                   string   `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt            DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt            DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Token                string   `gorm:"column:token;uniqueIndex;not null" json:"token"`
	Platform             string   `gorm:"column:platform;not null" json:"platform"`
	TokenType            string   `gorm:"column:tokenType;default:'expo';not null" json:"tokenType"`
	AppVersion           *string  `gorm:"column:appVersion" json:"appVersion"`
	UserID               string   `gorm:"column:userId;index:MobilePushToken_userId_idx;not null" json:"userId"`
	User                 *User
	MobilePushDeliveries []MobilePushDelivery `gorm:"foreignKey:MobilePushTokenID;constraint:OnDelete:CASCADE"`
}

func (MobilePushToken) TableName() string {
	return "MobilePushToken"
}

func (m *MobilePushToken) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*MobilePushToken) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type MobilePushDelivery struct {
	ID                string   `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt         DateTime `gorm:"column:createdAt;index:MobilePushDelivery_createdAt_idx;autoCreateTime:false;not null" json:"createdAt"`
	DeduplicationKey  string   `gorm:"column:deduplicationKey;uniqueIndex:MobilePushDelivery_deduplicationKey_mobilePushTokenId_key;not null" json:"deduplicationKey"`
	MobilePushTokenID string   `gorm:"column:mobilePushTokenId;uniqueIndex:MobilePushDelivery_deduplicationKey_mobilePushTokenId_key;index:MobilePushDelivery_mobilePushTokenId_idx;not null" json:"mobilePushTokenId"`
	MobilePushToken   *MobilePushToken
}

func (MobilePushDelivery) TableName() string {
	return "MobilePushDelivery"
}

func (m *MobilePushDelivery) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type OauthClient struct {
	ID                               string         `gorm:"column:id;primaryKey" json:"id"`
	ClientID                         string         `gorm:"column:clientId;uniqueIndex;not null" json:"clientId"`
	ClientSecret                     *string        `gorm:"column:clientSecret" json:"clientSecret"`
	ClientDiscoveryID                *string        `gorm:"column:clientDiscoveryId" json:"clientDiscoveryId"`
	Disabled                         *bool          `gorm:"column:disabled;default:false" json:"disabled"`
	SkipConsent                      *bool          `gorm:"column:skipConsent" json:"skipConsent"`
	EnableEndSession                 *bool          `gorm:"column:enableEndSession" json:"enableEndSession"`
	SubjectType                      *string        `gorm:"column:subjectType" json:"subjectType"`
	Scopes                           []string       `gorm:"column:scopes;serializer:json;not null" json:"scopes"`
	ClientCredentialsScopes          []string       `gorm:"column:clientCredentialsScopes;serializer:json;not null" json:"clientCredentialsScopes"`
	UserID                           *string        `gorm:"column:userId;index:OauthClient_userId_idx" json:"userId"`
	CreatedAt                        *DateTime      `gorm:"column:createdAt;autoCreateTime:false" json:"createdAt"`
	UpdatedAt                        *DateTime      `gorm:"column:updatedAt;autoUpdateTime:false" json:"updatedAt"`
	Name                             *string        `gorm:"column:name" json:"name"`
	URI                              *string        `gorm:"column:uri" json:"uri"`
	Icon                             *string        `gorm:"column:icon" json:"icon"`
	Contacts                         []string       `gorm:"column:contacts;serializer:json;not null" json:"contacts"`
	Tos                              *string        `gorm:"column:tos" json:"tos"`
	Policy                           *string        `gorm:"column:policy" json:"policy"`
	SoftwareID                       *string        `gorm:"column:softwareId" json:"softwareId"`
	SoftwareVersion                  *string        `gorm:"column:softwareVersion" json:"softwareVersion"`
	SoftwareStatement                *string        `gorm:"column:softwareStatement" json:"softwareStatement"`
	RedirectUris                     []string       `gorm:"column:redirectUris;serializer:json;not null" json:"redirectUris"`
	PostLogoutRedirectUris           []string       `gorm:"column:postLogoutRedirectUris;serializer:json;not null" json:"postLogoutRedirectUris"`
	BackchannelLogoutURI             *string        `gorm:"column:backchannelLogoutUri" json:"backchannelLogoutUri"`
	BackchannelLogoutSessionRequired *bool          `gorm:"column:backchannelLogoutSessionRequired" json:"backchannelLogoutSessionRequired"`
	TokenEndpointAuthMethod          *string        `gorm:"column:tokenEndpointAuthMethod" json:"tokenEndpointAuthMethod"`
	ApplicationType                  *string        `gorm:"column:applicationType" json:"applicationType"`
	Jwks                             *string        `gorm:"column:jwks" json:"jwks"`
	JwksURI                          *string        `gorm:"column:jwksUri" json:"jwksUri"`
	GrantTypes                       []string       `gorm:"column:grantTypes;serializer:json;not null" json:"grantTypes"`
	ResponseTypes                    []string       `gorm:"column:responseTypes;serializer:json;not null" json:"responseTypes"`
	RequirePKCE                      *bool          `gorm:"column:requirePKCE" json:"requirePKCE"`
	DpopBoundAccessTokens            *bool          `gorm:"column:dpopBoundAccessTokens;default:false" json:"dpopBoundAccessTokens"`
	ReferenceID                      *string        `gorm:"column:referenceId" json:"referenceId"`
	Metadata                         datatypes.JSON `gorm:"column:metadata" json:"metadata"`
	User                             *User
	OauthClientResources             []OauthClientResource `gorm:"foreignKey:ClientID;references:ClientID;constraint:OnDelete:CASCADE"`
	Oauthrefreshtokens               []OauthRefreshToken   `gorm:"foreignKey:ClientID;references:ClientID;constraint:OnDelete:CASCADE"`
	Oauthaccesstokens                []OauthAccessToken    `gorm:"foreignKey:ClientID;references:ClientID;constraint:OnDelete:CASCADE"`
	Oauthconsents                    []OauthConsent        `gorm:"foreignKey:ClientID;references:ClientID;constraint:OnDelete:CASCADE"`
}

func (OauthClient) TableName() string {
	return "OauthClient"
}

func (m *OauthClient) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = cuid.New()
	}
	return nil
}

type OauthRefreshToken struct {
	ID                      string         `gorm:"column:id;primaryKey" json:"id"`
	Token                   string         `gorm:"column:token;uniqueIndex;not null" json:"token"`
	ClientID                string         `gorm:"column:clientId;index:OauthRefreshToken_clientId_idx;not null" json:"clientId"`
	SessionID               *string        `gorm:"column:sessionId;index:OauthRefreshToken_sessionId_idx" json:"sessionId"`
	UserID                  string         `gorm:"column:userId;index:OauthRefreshToken_userId_idx;not null" json:"userId"`
	ReferenceID             *string        `gorm:"column:referenceId" json:"referenceId"`
	AuthorizationCodeID     *string        `gorm:"column:authorizationCodeId;index:OauthRefreshToken_authorizationCodeId_idx" json:"authorizationCodeId"`
	Resources               []string       `gorm:"column:resources;serializer:json;not null" json:"resources"`
	RequestedUserInfoClaims []string       `gorm:"column:requestedUserInfoClaims;serializer:json;not null" json:"requestedUserInfoClaims"`
	ExpiresAt               DateTime       `gorm:"column:expiresAt;not null" json:"expiresAt"`
	CreatedAt               DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	Revoked                 *DateTime      `gorm:"column:revoked" json:"revoked"`
	RotatedAt               *DateTime      `gorm:"column:rotatedAt" json:"rotatedAt"`
	RotationReplayResponse  *string        `gorm:"column:rotationReplayResponse" json:"rotationReplayResponse"`
	RotationReplayExpiresAt *DateTime      `gorm:"column:rotationReplayExpiresAt" json:"rotationReplayExpiresAt"`
	AuthTime                *DateTime      `gorm:"column:authTime" json:"authTime"`
	Confirmation            datatypes.JSON `gorm:"column:confirmation" json:"confirmation"`
	Scopes                  []string       `gorm:"column:scopes;serializer:json;not null" json:"scopes"`
	Client                  *OauthClient   `gorm:"foreignKey:ClientID;references:ClientID"`
	Session                 *Session
	User                    *User
	Oauthaccesstokens       []OauthAccessToken `gorm:"foreignKey:RefreshID;constraint:OnDelete:CASCADE"`
}

func (OauthRefreshToken) TableName() string {
	return "OauthRefreshToken"
}

func (m *OauthRefreshToken) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = cuid.New()
	}
	return nil
}

type OauthAccessToken struct {
	ID                      string         `gorm:"column:id;primaryKey" json:"id"`
	Token                   string         `gorm:"column:token;uniqueIndex;not null" json:"token"`
	ClientID                string         `gorm:"column:clientId;index:OauthAccessToken_clientId_idx;not null" json:"clientId"`
	SessionID               *string        `gorm:"column:sessionId;index:OauthAccessToken_sessionId_idx" json:"sessionId"`
	UserID                  *string        `gorm:"column:userId;index:OauthAccessToken_userId_idx" json:"userId"`
	ReferenceID             *string        `gorm:"column:referenceId" json:"referenceId"`
	AuthorizationCodeID     *string        `gorm:"column:authorizationCodeId;index:OauthAccessToken_authorizationCodeId_idx" json:"authorizationCodeId"`
	Resources               []string       `gorm:"column:resources;serializer:json;not null" json:"resources"`
	RequestedUserInfoClaims []string       `gorm:"column:requestedUserInfoClaims;serializer:json;not null" json:"requestedUserInfoClaims"`
	RefreshID               *string        `gorm:"column:refreshId;index:OauthAccessToken_refreshId_idx" json:"refreshId"`
	ExpiresAt               DateTime       `gorm:"column:expiresAt;not null" json:"expiresAt"`
	CreatedAt               DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	Revoked                 *DateTime      `gorm:"column:revoked" json:"revoked"`
	Confirmation            datatypes.JSON `gorm:"column:confirmation" json:"confirmation"`
	Scopes                  []string       `gorm:"column:scopes;serializer:json;not null" json:"scopes"`
	Client                  *OauthClient   `gorm:"foreignKey:ClientID;references:ClientID"`
	Session                 *Session
	User                    *User
	Refresh                 *OauthRefreshToken `gorm:"foreignKey:RefreshID"`
}

func (OauthAccessToken) TableName() string {
	return "OauthAccessToken"
}

func (m *OauthAccessToken) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = cuid.New()
	}
	return nil
}

type OauthConsent struct {
	ID                      string       `gorm:"column:id;primaryKey" json:"id"`
	ClientID                string       `gorm:"column:clientId;uniqueIndex:OauthConsent_clientId_userId_key;not null" json:"clientId"`
	UserID                  *string      `gorm:"column:userId;uniqueIndex:OauthConsent_clientId_userId_key;index:OauthConsent_userId_idx" json:"userId"`
	ReferenceID             *string      `gorm:"column:referenceId" json:"referenceId"`
	Resources               []string     `gorm:"column:resources;serializer:json;not null" json:"resources"`
	RequestedUserInfoClaims []string     `gorm:"column:requestedUserInfoClaims;serializer:json;not null" json:"requestedUserInfoClaims"`
	Scopes                  []string     `gorm:"column:scopes;serializer:json;not null" json:"scopes"`
	CreatedAt               DateTime     `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt               DateTime     `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Client                  *OauthClient `gorm:"foreignKey:ClientID;references:ClientID"`
	User                    *User
}

func (OauthConsent) TableName() string {
	return "OauthConsent"
}

func (m *OauthConsent) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = cuid.New()
	}
	return nil
}

type Jwks struct {
	ID         string    `gorm:"column:id;primaryKey" json:"id"`
	PublicKey  string    `gorm:"column:publicKey;not null" json:"publicKey"`
	PrivateKey string    `gorm:"column:privateKey;not null" json:"privateKey"`
	CreatedAt  DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	ExpiresAt  *DateTime `gorm:"column:expiresAt" json:"expiresAt"`
	Alg        *string   `gorm:"column:alg" json:"alg"`
	Crv        *string   `gorm:"column:crv" json:"crv"`
}

func (Jwks) TableName() string {
	return "Jwks"
}

func (m *Jwks) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = cuid.New()
	}
	return nil
}

type OauthResource struct {
	ID                            string                `gorm:"column:id;primaryKey" json:"id"`
	Identifier                    string                `gorm:"column:identifier;uniqueIndex;not null" json:"identifier"`
	Name                          string                `gorm:"column:name;not null" json:"name"`
	AccessTokenTTL                *int                  `gorm:"column:accessTokenTtl" json:"accessTokenTtl"`
	RefreshTokenTTL               *int                  `gorm:"column:refreshTokenTtl" json:"refreshTokenTtl"`
	SigningAlgorithm              *string               `gorm:"column:signingAlgorithm" json:"signingAlgorithm"`
	SigningKeyID                  *string               `gorm:"column:signingKeyId" json:"signingKeyId"`
	AllowedScopes                 []string              `gorm:"column:allowedScopes;serializer:json;not null" json:"allowedScopes"`
	CustomClaims                  datatypes.JSON        `gorm:"column:customClaims" json:"customClaims"`
	DpopBoundAccessTokensRequired *bool                 `gorm:"column:dpopBoundAccessTokensRequired;default:false" json:"dpopBoundAccessTokensRequired"`
	Disabled                      *bool                 `gorm:"column:disabled;default:false" json:"disabled"`
	CreatedAt                     *DateTime             `gorm:"column:createdAt;autoCreateTime:false" json:"createdAt"`
	UpdatedAt                     *DateTime             `gorm:"column:updatedAt;autoUpdateTime:false" json:"updatedAt"`
	PolicyVersion                 *int                  `gorm:"column:policyVersion;default:1" json:"policyVersion"`
	Metadata                      datatypes.JSON        `gorm:"column:metadata" json:"metadata"`
	OauthClientResources          []OauthClientResource `gorm:"foreignKey:ResourceID;references:Identifier;constraint:OnDelete:CASCADE"`
}

func (OauthResource) TableName() string {
	return "OauthResource"
}

func (m *OauthResource) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = cuid.New()
	}
	return nil
}

type OauthClientResource struct {
	ID            string         `gorm:"column:id;primaryKey" json:"id"`
	ClientID      string         `gorm:"column:clientId;uniqueIndex:OauthClientResource_clientId_resourceId_key;index:OauthClientResource_clientId_idx;not null" json:"clientId"`
	ResourceID    string         `gorm:"column:resourceId;uniqueIndex:OauthClientResource_clientId_resourceId_key;index:OauthClientResource_resourceId_idx;not null" json:"resourceId"`
	Metadata      datatypes.JSON `gorm:"column:metadata" json:"metadata"`
	CreatedAt     *DateTime      `gorm:"column:createdAt;autoCreateTime:false" json:"createdAt"`
	Client        *OauthClient   `gorm:"foreignKey:ClientID;references:ClientID"`
	OauthResource *OauthResource `gorm:"foreignKey:ResourceID;references:Identifier"`
}

func (OauthClientResource) TableName() string {
	return "OauthClientResource"
}

func (m *OauthClientResource) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = cuid.New()
	}
	return nil
}

type OauthClientAssertion struct {
	ID        string   `gorm:"column:id;primaryKey" json:"id"`
	ExpiresAt DateTime `gorm:"column:expiresAt;not null" json:"expiresAt"`
}

func (OauthClientAssertion) TableName() string {
	return "OauthClientAssertion"
}

func (m *OauthClientAssertion) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = cuid.New()
	}
	return nil
}

type ScimManagedConnection struct {
	ID                          string                       `gorm:"column:id;primaryKey" json:"id"`
	CreationRequestID           string                       `gorm:"column:creationRequestId;uniqueIndex;not null" json:"creationRequestId"`
	ConnectionID                string                       `gorm:"column:connectionId;uniqueIndex;not null" json:"connectionId"`
	ProvisioningDomainID        string                       `gorm:"column:provisioningDomainId;index:scimManagedConnection_provisioningDomainId_idx;not null" json:"provisioningDomainId"`
	Status                      string                       `gorm:"column:status;not null" json:"status"`
	Revision                    int                          `gorm:"column:revision;not null" json:"revision"`
	CreatedAt                   DateTime                     `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	CreatedBy                   string                       `gorm:"column:createdBy;not null" json:"createdBy"`
	DecommissionStartedAt       *DateTime                    `gorm:"column:decommissionStartedAt" json:"decommissionStartedAt"`
	DecommissionStartedBy       *string                      `gorm:"column:decommissionStartedBy" json:"decommissionStartedBy"`
	DecommissionedAt            *DateTime                    `gorm:"column:decommissionedAt" json:"decommissionedAt"`
	DecommissionedBy            *string                      `gorm:"column:decommissionedBy" json:"decommissionedBy"`
	Scimmanagedcredentials      []ScimManagedCredential      `gorm:"foreignKey:ConnectionRecordID;constraint:OnDelete:CASCADE"`
	Scimmanagedconnectionevents []ScimManagedConnectionEvent `gorm:"foreignKey:ConnectionRecordID;constraint:OnDelete:CASCADE"`
}

func (ScimManagedConnection) TableName() string {
	return "scimManagedConnection"
}

func (m *ScimManagedConnection) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = cuid.New()
	}
	return nil
}

type ScimManagedCredential struct {
	ID                    string                 `gorm:"column:id;primaryKey" json:"id"`
	ConnectionRecordID    string                 `gorm:"column:connectionRecordId;index:scimManagedCredential_connectionRecordId_idx;not null" json:"connectionRecordId"`
	CredentialID          string                 `gorm:"column:credentialId;uniqueIndex;not null" json:"credentialId"`
	TokenDigest           string                 `gorm:"column:tokenDigest;not null" json:"tokenDigest"`
	HashVersion           string                 `gorm:"column:hashVersion;not null" json:"hashVersion"`
	ActiveSlotKey         string                 `gorm:"column:activeSlotKey;uniqueIndex;not null" json:"activeSlotKey"`
	Status                string                 `gorm:"column:status;not null" json:"status"`
	SerializedScopes      string                 `gorm:"column:serializedScopes;not null" json:"serializedScopes"`
	ExpiresAt             DateTime               `gorm:"column:expiresAt;not null" json:"expiresAt"`
	CreatedAt             DateTime               `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	CreatedBy             string                 `gorm:"column:createdBy;not null" json:"createdBy"`
	LastUsedAt            *DateTime              `gorm:"column:lastUsedAt" json:"lastUsedAt"`
	RevokedAt             *DateTime              `gorm:"column:revokedAt" json:"revokedAt"`
	RevokedBy             *string                `gorm:"column:revokedBy" json:"revokedBy"`
	DecommissionedAt      *DateTime              `gorm:"column:decommissionedAt" json:"decommissionedAt"`
	Scimmanagedconnection *ScimManagedConnection `gorm:"foreignKey:ConnectionRecordID"`
}

func (ScimManagedCredential) TableName() string {
	return "scimManagedCredential"
}

func (m *ScimManagedCredential) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = cuid.New()
	}
	return nil
}

type ScimManagedConnectionEvent struct {
	ID                    string                 `gorm:"column:id;primaryKey" json:"id"`
	ConnectionRecordID    string                 `gorm:"column:connectionRecordId;index:scimManagedConnectionEvent_connectionRecordId_idx;not null" json:"connectionRecordId"`
	EventKey              string                 `gorm:"column:eventKey;uniqueIndex;not null" json:"eventKey"`
	Sequence              int                    `gorm:"column:sequence;not null" json:"sequence"`
	Type                  string                 `gorm:"column:type;not null" json:"type"`
	ActorID               string                 `gorm:"column:actorId;not null" json:"actorId"`
	CredentialID          *string                `gorm:"column:credentialId" json:"credentialId"`
	CreatedAt             DateTime               `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	Scimmanagedconnection *ScimManagedConnection `gorm:"foreignKey:ConnectionRecordID"`
}

func (ScimManagedConnectionEvent) TableName() string {
	return "scimManagedConnectionEvent"
}

func (m *ScimManagedConnectionEvent) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = cuid.New()
	}
	return nil
}

type ScimConnectionBinding struct {
	ID                              string    `gorm:"column:id;primaryKey" json:"id"`
	ConnectionID                    string    `gorm:"column:connectionId;index:scimConnectionBinding_connectionId_idx;not null" json:"connectionId"`
	ConnectionKey                   string    `gorm:"column:connectionKey;uniqueIndex;not null" json:"connectionKey"`
	ProvisioningDomainID            string    `gorm:"column:provisioningDomainId;not null" json:"provisioningDomainId"`
	CreatedAt                       DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	DecommissionedAt                *DateTime `gorm:"column:decommissionedAt" json:"decommissionedAt"`
	DecommissionStatus              *string   `gorm:"column:decommissionStatus;default:'active';not null" json:"decommissionStatus"`
	DecommissionCursorUserID        *string   `gorm:"column:decommissionCursorUserId" json:"decommissionCursorUserId"`
	DecommissionReconciledUserCount int       `gorm:"column:decommissionReconciledUserCount;default:0;not null" json:"decommissionReconciledUserCount"`
	DecommissionBatchCount          int       `gorm:"column:decommissionBatchCount;default:0;not null" json:"decommissionBatchCount"`
	DecommissionRevision            int       `gorm:"column:decommissionRevision;default:0;not null" json:"decommissionRevision"`
	DecommissionCompletedAt         *DateTime `gorm:"column:decommissionCompletedAt" json:"decommissionCompletedAt"`
	DecommissionLeaseID             *string   `gorm:"column:decommissionLeaseId" json:"decommissionLeaseId"`
	DecommissionLeaseExpiresAt      *DateTime `gorm:"column:decommissionLeaseExpiresAt" json:"decommissionLeaseExpiresAt"`
}

func (ScimConnectionBinding) TableName() string {
	return "scimConnectionBinding"
}

func (m *ScimConnectionBinding) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = cuid.New()
	}
	return nil
}

type ScimIdentityTombstone struct {
	ID                   string   `gorm:"column:id;primaryKey" json:"id"`
	ConnectionID         string   `gorm:"column:connectionId;index:scimIdentityTombstone_connectionId_idx;not null" json:"connectionId"`
	ProvisioningDomainID string   `gorm:"column:provisioningDomainId;index:scimIdentityTombstone_provisioningDomainId_idx;not null" json:"provisioningDomainId"`
	ExternalID           string   `gorm:"column:externalId;not null" json:"externalId"`
	ExternalIDKey        string   `gorm:"column:externalIdKey;uniqueIndex;not null" json:"externalIdKey"`
	UserID               string   `gorm:"column:userId;index:scimIdentityTombstone_userId_idx;not null" json:"userId"`
	Profile              string   `gorm:"column:profile;not null" json:"profile"`
	DeletedAt            DateTime `gorm:"column:deletedAt;not null" json:"deletedAt"`
	User                 *User
}

func (ScimIdentityTombstone) TableName() string {
	return "scimIdentityTombstone"
}

func (m *ScimIdentityTombstone) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = cuid.New()
	}
	return nil
}

type ScimSubject struct {
	ID              string   `gorm:"column:id;primaryKey" json:"id"`
	UserID          string   `gorm:"column:userId;uniqueIndex;not null" json:"userId"`
	ProfileSourceID *string  `gorm:"column:profileSourceId;index:scimSubject_profileSourceId_idx" json:"profileSourceId"`
	Revision        int      `gorm:"column:revision;not null" json:"revision"`
	CreatedAt       DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt       DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	User            *User
}

func (ScimSubject) TableName() string {
	return "scimSubject"
}

func (m *ScimSubject) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = cuid.New()
	}
	return nil
}

type ScimUser struct {
	ID                   string   `gorm:"column:id;primaryKey" json:"id"`
	ConnectionID         string   `gorm:"column:connectionId;index:scimUser_connectionId_idx;not null" json:"connectionId"`
	ProvisioningDomainID string   `gorm:"column:provisioningDomainId;index:scimUser_provisioningDomainId_idx;not null" json:"provisioningDomainId"`
	UserID               string   `gorm:"column:userId;index:scimUser_userId_idx;not null" json:"userId"`
	ConnectionUserKey    string   `gorm:"column:connectionUserKey;uniqueIndex;not null" json:"connectionUserKey"`
	UserName             string   `gorm:"column:userName;not null" json:"userName"`
	UserNameKey          string   `gorm:"column:userNameKey;uniqueIndex;not null" json:"userNameKey"`
	PrimaryEmail         string   `gorm:"column:primaryEmail;not null" json:"primaryEmail"`
	WorkEmailValueIndex  string   `gorm:"column:workEmailValueIndex;not null" json:"workEmailValueIndex"`
	EmailValueIndex      string   `gorm:"column:emailValueIndex;not null" json:"emailValueIndex"`
	DisplayName          string   `gorm:"column:displayName;not null" json:"displayName"`
	FormattedName        string   `gorm:"column:formattedName;not null" json:"formattedName"`
	GivenName            *string  `gorm:"column:givenName" json:"givenName"`
	FamilyName           *string  `gorm:"column:familyName" json:"familyName"`
	SerializedEmails     string   `gorm:"column:serializedEmails;not null" json:"serializedEmails"`
	SerializedAttributes *string  `gorm:"column:serializedAttributes" json:"serializedAttributes"`
	ExternalID           *string  `gorm:"column:externalId" json:"externalId"`
	ExternalIDKey        *string  `gorm:"column:externalIdKey;uniqueIndex" json:"externalIdKey"`
	Active               bool     `gorm:"column:active;not null" json:"active"`
	OrderKey             string   `gorm:"column:orderKey;uniqueIndex;not null" json:"orderKey"`
	CreatedAt            DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt            DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	User                 *User
	ScimProjectionGrants []ScimProjectionGrant `gorm:"foreignKey:ScimUserID;constraint:OnDelete:CASCADE"`
	ScimGroupMembers     []ScimGroupMember     `gorm:"foreignKey:ScimUserID;constraint:OnDelete:CASCADE"`
}

func (ScimUser) TableName() string {
	return "scimUser"
}

func (m *ScimUser) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = cuid.New()
	}
	return nil
}

type ScimProjectionGrant struct {
	ID                   string   `gorm:"column:id;primaryKey" json:"id"`
	ConnectionID         string   `gorm:"column:connectionId;index:scimProjectionGrant_connectionId_idx;not null" json:"connectionId"`
	ProvisioningDomainID string   `gorm:"column:provisioningDomainId;index:scimProjectionGrant_provisioningDomainId_idx;not null" json:"provisioningDomainId"`
	ScimUserID           string   `gorm:"column:scimUserId;index:scimProjectionGrant_scimUserId_idx;not null" json:"scimUserId"`
	UserID               string   `gorm:"column:userId;index:scimProjectionGrant_userId_idx;not null" json:"userId"`
	SourceKind           string   `gorm:"column:sourceKind;not null" json:"sourceKind"`
	SourceID             string   `gorm:"column:sourceId;not null" json:"sourceId"`
	SourceValue          *string  `gorm:"column:sourceValue" json:"sourceValue"`
	Role                 string   `gorm:"column:role;not null" json:"role"`
	GrantKey             string   `gorm:"column:grantKey;uniqueIndex;not null" json:"grantKey"`
	CreatedAt            DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt            DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	ScimUser             *ScimUser
	User                 *User
}

func (ScimProjectionGrant) TableName() string {
	return "scimProjectionGrant"
}

func (m *ScimProjectionGrant) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = cuid.New()
	}
	return nil
}

type ScimGroup struct {
	ID                   string            `gorm:"column:id;primaryKey" json:"id"`
	ConnectionID         string            `gorm:"column:connectionId;index:scimGroup_connectionId_idx;not null" json:"connectionId"`
	ProvisioningDomainID string            `gorm:"column:provisioningDomainId;index:scimGroup_provisioningDomainId_idx;not null" json:"provisioningDomainId"`
	Revision             int               `gorm:"column:revision;default:0;not null" json:"revision"`
	DisplayName          string            `gorm:"column:displayName;not null" json:"displayName"`
	DisplayNameKey       string            `gorm:"column:displayNameKey;uniqueIndex;not null" json:"displayNameKey"`
	ExternalID           *string           `gorm:"column:externalId" json:"externalId"`
	ExternalIDKey        *string           `gorm:"column:externalIdKey;uniqueIndex" json:"externalIdKey"`
	OrderKey             string            `gorm:"column:orderKey;uniqueIndex;not null" json:"orderKey"`
	CreatedAt            DateTime          `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt            DateTime          `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	ScimGroupMembers     []ScimGroupMember `gorm:"foreignKey:GroupID;constraint:OnDelete:CASCADE"`
}

func (ScimGroup) TableName() string {
	return "scimGroup"
}

func (m *ScimGroup) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = cuid.New()
	}
	return nil
}

type ScimGroupMember struct {
	ID            string     `gorm:"column:id;primaryKey" json:"id"`
	ConnectionID  string     `gorm:"column:connectionId;index:scimGroupMember_connectionId_idx;not null" json:"connectionId"`
	GroupID       string     `gorm:"column:groupId;index:scimGroupMember_groupId_idx;not null" json:"groupId"`
	ScimUserID    string     `gorm:"column:scimUserId;index:scimGroupMember_scimUserId_idx;not null" json:"scimUserId"`
	MembershipKey string     `gorm:"column:membershipKey;uniqueIndex;not null" json:"membershipKey"`
	CreatedAt     DateTime   `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	ScimGroup     *ScimGroup `gorm:"foreignKey:GroupID"`
	ScimUser      *ScimUser
}

func (ScimGroupMember) TableName() string {
	return "scimGroupMember"
}

func (m *ScimGroupMember) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = cuid.New()
	}
	return nil
}

type ScimIdentityLink struct {
	ID           string `gorm:"column:id;primaryKey" json:"id"`
	ConnectionID string `gorm:"column:connectionId;uniqueIndex:ScimIdentityLink_connectionId_externalId_key;not null" json:"connectionId"`
	ExternalID   string `gorm:"column:externalId;uniqueIndex:ScimIdentityLink_connectionId_externalId_key;not null" json:"externalId"`
	UserID       string `gorm:"column:userId;index:ScimIdentityLink_userId_idx;not null" json:"userId"`
	User         *User
}

func (ScimIdentityLink) TableName() string {
	return "ScimIdentityLink"
}

func (m *ScimIdentityLink) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = cuid.New()
	}
	return nil
}

type LoopsEmailSource struct {
	ID             string    `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt      DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt      DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	EmailMessageID *string   `gorm:"column:emailMessageId;uniqueIndex" json:"emailMessageId"`
	LoopID         *string   `gorm:"column:loopId;index:LoopsEmailSource_loopId_idx" json:"loopId"`
	LoopName       *string   `gorm:"column:loopName" json:"loopName"`
	CampaignID     *string   `gorm:"column:campaignId;index:LoopsEmailSource_campaignId_idx" json:"campaignId"`
	CampaignName   *string   `gorm:"column:campaignName" json:"campaignName"`
	SourceType     *string   `gorm:"column:sourceType" json:"sourceType"`
	EventTime      *DateTime `gorm:"column:eventTime" json:"eventTime"`
}

func (LoopsEmailSource) TableName() string {
	return "LoopsEmailSource"
}

func (m *LoopsEmailSource) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*LoopsEmailSource) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type DateTime struct{ time.Time }

func (DateTime) GormDataType() string {
	return "time"
}

func (dateTime DateTime) Value() (driver.Value, error) {
	return dateTime.UTC().Truncate(time.Millisecond), nil
}

func (dateTime *DateTime) Scan(src any) error {
	read, err := readPrismaTime(src)
	dateTime.Time = read
	return err
}

var prismaTimeLayouts = []string{
	"2006-01-02T15:04:05.999999999Z07:00",
	"2006-01-02 15:04:05.999999999Z07:00",
	"2006-01-02 15:04:05.999999999Z07",
	"2006-01-02T15:04:05.999999999",
	"2006-01-02 15:04:05.999999999",
	"2006-01-02",
}

func readPrismaTime(src any) (time.Time, error) {
	switch value := src.(type) {
	case nil:
		return time.Time{}, nil
	case time.Time:
		return value.UTC().Truncate(time.Millisecond), nil
	case int64:
		return time.UnixMilli(value).UTC(), nil
	case []byte:
		return readPrismaTime(string(value))
	case string:
		if millis, err := strconv.ParseInt(value, 10, 64); err == nil {
			return time.UnixMilli(millis).UTC(), nil
		}
		for _, layout := range prismaTimeLayouts {
			if read, err := time.Parse(layout, value); err == nil {
				return read.UTC().Truncate(time.Millisecond), nil
			}
		}
		return time.Time{}, fmt.Errorf("a DateTime column held %q", value)
	}
	return time.Time{}, fmt.Errorf("a DateTime column held %T", src)
}
