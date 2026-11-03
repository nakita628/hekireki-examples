package model

import (
	"database/sql/driver"
	"fmt"
	"strconv"
	"time"

	"github.com/lucsky/cuid"
	"gorm.io/datatypes"
	"gorm.io/gorm"
)

type User struct {
	ID                   int       `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	Name                 *string   `gorm:"column:name" json:"name"`
	Email                string    `gorm:"column:email;uniqueIndex;index:User_email_idx;not null" json:"email"`
	EmailVerified        *DateTime `gorm:"column:emailVerified" json:"emailVerified"`
	Password             *string   `gorm:"column:password" json:"password"`
	Source               *string   `gorm:"column:source" json:"source"`
	Signature            *string   `gorm:"column:signature" json:"signature"`
	CreatedAt            DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt            DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	LastSignedIn         DateTime  `gorm:"column:lastSignedIn;not null" json:"lastSignedIn"`
	Roles                []string  `gorm:"column:roles;serializer:json;not null" json:"roles"`
	IdentityProvider     string    `gorm:"column:identityProvider;default:'DOCUMENSO';not null" json:"identityProvider"`
	AvatarImageID        *string   `gorm:"column:avatarImageId" json:"avatarImageId"`
	Disabled             bool      `gorm:"column:disabled;default:false;not null" json:"disabled"`
	TwoFactorSecret      *string   `gorm:"column:twoFactorSecret" json:"twoFactorSecret"`
	TwoFactorEnabled     bool      `gorm:"column:twoFactorEnabled;default:false;not null" json:"twoFactorEnabled"`
	TwoFactorBackupCodes *string   `gorm:"column:twoFactorBackupCodes" json:"twoFactorBackupCodes"`
	AvatarImage          *AvatarImage
	Accounts             []Account              `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	Sessions             []Session              `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	PasswordResetTokens  []PasswordResetToken   `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	OwnedOrganisations   []Organisation         `gorm:"foreignKey:OwnerUserID;constraint:OnDelete:CASCADE"`
	OrganisationMember   []OrganisationMember   `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	Folders              []Folder               `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	Envelopes            []Envelope             `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	VerificationTokens   []VerificationToken    `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	APITokens            []APIToken             `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	SecurityAuditLogs    []UserSecurityAuditLog `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	Webhooks             []Webhook              `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	SiteSettings         []SiteSettings         `gorm:"foreignKey:LastModifiedByUserID;constraint:OnDelete:SET NULL"`
	Passkeys             []Passkey              `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
}

func (User) TableName() string {
	return "User"
}

func (m *User) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	if m.LastSignedIn.IsZero() {
		m.LastSignedIn = DateTime{Time: now}
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

type TeamProfile struct {
	ID      string  `gorm:"column:id;primaryKey" json:"id"`
	Enabled bool    `gorm:"column:enabled;default:false;not null" json:"enabled"`
	TeamID  int     `gorm:"column:teamId;uniqueIndex;not null" json:"teamId"`
	Bio     *string `gorm:"column:bio" json:"bio"`
	Team    *Team
}

func (TeamProfile) TableName() string {
	return "TeamProfile"
}

func (m *TeamProfile) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = cuid.New()
	}
	return nil
}

type UserSecurityAuditLog struct {
	ID        int      `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	UserID    int      `gorm:"column:userId;not null" json:"userId"`
	CreatedAt DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	Type      string   `gorm:"column:type;not null" json:"type"`
	UserAgent *string  `gorm:"column:userAgent" json:"userAgent"`
	IPAddress *string  `gorm:"column:ipAddress" json:"ipAddress"`
	User      *User
}

func (UserSecurityAuditLog) TableName() string {
	return "UserSecurityAuditLog"
}

func (m *UserSecurityAuditLog) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type PasswordResetToken struct {
	ID        int      `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	Token     string   `gorm:"column:token;uniqueIndex;not null" json:"token"`
	CreatedAt DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	Expiry    DateTime `gorm:"column:expiry;not null" json:"expiry"`
	UserID    int      `gorm:"column:userId;not null" json:"userId"`
	User      *User
}

func (PasswordResetToken) TableName() string {
	return "PasswordResetToken"
}

func (m *PasswordResetToken) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type Passkey struct {
	ID                   string    `gorm:"column:id;primaryKey" json:"id"`
	UserID               int       `gorm:"column:userId;not null" json:"userId"`
	Name                 string    `gorm:"column:name;not null" json:"name"`
	CreatedAt            DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt            DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	LastUsedAt           *DateTime `gorm:"column:lastUsedAt" json:"lastUsedAt"`
	CredentialID         []byte    `gorm:"column:credentialId;not null" json:"credentialId"`
	CredentialPublicKey  []byte    `gorm:"column:credentialPublicKey;not null" json:"credentialPublicKey"`
	Counter              int64     `gorm:"column:counter;not null" json:"counter"`
	CredentialDeviceType string    `gorm:"column:credentialDeviceType;not null" json:"credentialDeviceType"`
	CredentialBackedUp   bool      `gorm:"column:credentialBackedUp;not null" json:"credentialBackedUp"`
	Transports           []string  `gorm:"column:transports;serializer:json;not null" json:"transports"`
	User                 *User
}

func (Passkey) TableName() string {
	return "Passkey"
}

func (m *Passkey) BeforeCreate(tx *gorm.DB) error {
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

type AnonymousVerificationToken struct {
	ID        string   `gorm:"column:id;primaryKey" json:"id"`
	Token     string   `gorm:"column:token;uniqueIndex;not null" json:"token"`
	ExpiresAt DateTime `gorm:"column:expiresAt;not null" json:"expiresAt"`
	CreatedAt DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
}

func (AnonymousVerificationToken) TableName() string {
	return "AnonymousVerificationToken"
}

func (m *AnonymousVerificationToken) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type VerificationToken struct {
	ID          int            `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	SecondaryID string         `gorm:"column:secondaryId;uniqueIndex;not null" json:"secondaryId"`
	Identifier  string         `gorm:"column:identifier;not null" json:"identifier"`
	Token       string         `gorm:"column:token;uniqueIndex;not null" json:"token"`
	Completed   bool           `gorm:"column:completed;default:false;not null" json:"completed"`
	Expires     DateTime       `gorm:"column:expires;not null" json:"expires"`
	CreatedAt   DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	Metadata    datatypes.JSON `gorm:"column:metadata" json:"metadata"`
	UserID      int            `gorm:"column:userId;not null" json:"userId"`
	User        *User
}

func (VerificationToken) TableName() string {
	return "VerificationToken"
}

func (m *VerificationToken) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.SecondaryID == "" {
		m.SecondaryID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type Webhook struct {
	ID            string   `gorm:"column:id;primaryKey" json:"id"`
	WebhookURL    string   `gorm:"column:webhookUrl;not null" json:"webhookUrl"`
	EventTriggers []string `gorm:"column:eventTriggers;serializer:json;not null" json:"eventTriggers"`
	Secret        *string  `gorm:"column:secret" json:"secret"`
	Enabled       *bool    `gorm:"column:enabled;default:true;not null" json:"enabled"`
	CreatedAt     DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt     DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	UserID        int      `gorm:"column:userId;not null" json:"userId"`
	TeamID        int      `gorm:"column:teamId;not null" json:"teamId"`
	User          *User
	Team          *Team
	WebhookCalls  []WebhookCall `gorm:"foreignKey:WebhookID;constraint:OnDelete:CASCADE"`
}

func (Webhook) TableName() string {
	return "Webhook"
}

func (m *Webhook) BeforeCreate(tx *gorm.DB) error {
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

func (*Webhook) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type WebhookCall struct {
	ID              string         `gorm:"column:id;primaryKey" json:"id"`
	Status          string         `gorm:"column:status;not null" json:"status"`
	URL             string         `gorm:"column:url;not null" json:"url"`
	Event           string         `gorm:"column:event;not null" json:"event"`
	RequestBody     datatypes.JSON `gorm:"column:requestBody;not null" json:"requestBody"`
	ResponseCode    int            `gorm:"column:responseCode;not null" json:"responseCode"`
	ResponseHeaders datatypes.JSON `gorm:"column:responseHeaders" json:"responseHeaders"`
	ResponseBody    datatypes.JSON `gorm:"column:responseBody" json:"responseBody"`
	CreatedAt       DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	WebhookID       string         `gorm:"column:webhookId;not null" json:"webhookId"`
	Webhook         *Webhook
}

func (WebhookCall) TableName() string {
	return "WebhookCall"
}

func (m *WebhookCall) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type APIToken struct {
	ID         int       `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	Name       string    `gorm:"column:name;not null" json:"name"`
	Token      string    `gorm:"column:token;uniqueIndex;not null" json:"token"`
	Algorithm  string    `gorm:"column:algorithm;default:'SHA512';not null" json:"algorithm"`
	Expires    *DateTime `gorm:"column:expires" json:"expires"`
	CreatedAt  DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	LastUsedAt *DateTime `gorm:"column:lastUsedAt" json:"lastUsedAt"`
	UserID     *int      `gorm:"column:userId" json:"userId"`
	TeamID     int       `gorm:"column:teamId;not null" json:"teamId"`
	User       *User
	Team       *Team
}

func (APIToken) TableName() string {
	return "ApiToken"
}

func (m *APIToken) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type Subscription struct {
	ID                int       `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	Status            string    `gorm:"column:status;default:'INACTIVE';not null" json:"status"`
	PlanID            string    `gorm:"column:planId;uniqueIndex;not null" json:"planId"`
	PriceID           string    `gorm:"column:priceId;not null" json:"priceId"`
	PeriodEnd         *DateTime `gorm:"column:periodEnd" json:"periodEnd"`
	CreatedAt         DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt         DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	CancelAtPeriodEnd bool      `gorm:"column:cancelAtPeriodEnd;default:false;not null" json:"cancelAtPeriodEnd"`
	CustomerID        string    `gorm:"column:customerId;not null" json:"customerId"`
	OrganisationID    string    `gorm:"column:organisationId;uniqueIndex;index:Subscription_organisationId_idx;not null" json:"organisationId"`
	Organisation      *Organisation
}

func (Subscription) TableName() string {
	return "Subscription"
}

func (m *Subscription) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*Subscription) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type SubscriptionClaim struct {
	ID                 string         `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt          DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt          DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Name               string         `gorm:"column:name;not null" json:"name"`
	Locked             bool           `gorm:"column:locked;default:false;not null" json:"locked"`
	TeamCount          int            `gorm:"column:teamCount;not null" json:"teamCount"`
	MemberCount        int            `gorm:"column:memberCount;not null" json:"memberCount"`
	EnvelopeItemCount  int            `gorm:"column:envelopeItemCount;not null" json:"envelopeItemCount"`
	RecipientCount     int            `gorm:"column:recipientCount;not null" json:"recipientCount"`
	Flags              datatypes.JSON `gorm:"column:flags;not null" json:"flags"`
	DocumentRateLimits datatypes.JSON `gorm:"column:documentRateLimits;not null" json:"documentRateLimits"`
	DocumentQuota      *int           `gorm:"column:documentQuota" json:"documentQuota"`
	EmailRateLimits    datatypes.JSON `gorm:"column:emailRateLimits;not null" json:"emailRateLimits"`
	EmailQuota         *int           `gorm:"column:emailQuota" json:"emailQuota"`
	APIRateLimits      datatypes.JSON `gorm:"column:apiRateLimits;not null" json:"apiRateLimits"`
	APIQuota           *int           `gorm:"column:apiQuota" json:"apiQuota"`
	EmailTransportID   *string        `gorm:"column:emailTransportId" json:"emailTransportId"`
	EmailTransport     *EmailTransport
}

func (SubscriptionClaim) TableName() string {
	return "SubscriptionClaim"
}

func (m *SubscriptionClaim) BeforeCreate(tx *gorm.DB) error {
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

func (*SubscriptionClaim) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type OrganisationClaim struct {
	ID                          string         `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt                   DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt                   DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	OriginalSubscriptionClaimID *string        `gorm:"column:originalSubscriptionClaimId" json:"originalSubscriptionClaimId"`
	TeamCount                   int            `gorm:"column:teamCount;not null" json:"teamCount"`
	MemberCount                 int            `gorm:"column:memberCount;not null" json:"memberCount"`
	EnvelopeItemCount           int            `gorm:"column:envelopeItemCount;not null" json:"envelopeItemCount"`
	RecipientCount              int            `gorm:"column:recipientCount;not null" json:"recipientCount"`
	Flags                       datatypes.JSON `gorm:"column:flags;not null" json:"flags"`
	DocumentRateLimits          datatypes.JSON `gorm:"column:documentRateLimits;not null" json:"documentRateLimits"`
	DocumentQuota               *int           `gorm:"column:documentQuota" json:"documentQuota"`
	EmailRateLimits             datatypes.JSON `gorm:"column:emailRateLimits;not null" json:"emailRateLimits"`
	EmailQuota                  *int           `gorm:"column:emailQuota" json:"emailQuota"`
	APIRateLimits               datatypes.JSON `gorm:"column:apiRateLimits;not null" json:"apiRateLimits"`
	APIQuota                    *int           `gorm:"column:apiQuota" json:"apiQuota"`
	EmailTransportID            *string        `gorm:"column:emailTransportId" json:"emailTransportId"`
	EmailTransport              *EmailTransport
	Organisation                *Organisation `gorm:"foreignKey:OrganisationClaimID"`
}

func (OrganisationClaim) TableName() string {
	return "OrganisationClaim"
}

func (m *OrganisationClaim) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*OrganisationClaim) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type OrganisationMonthlyStat struct {
	ID             string   `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt      DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt      DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	OrganisationID string   `gorm:"column:organisationId;uniqueIndex:OrganisationMonthlyStat_organisationId_period_key;index:OrganisationMonthlyStat_organisationId_idx;not null" json:"organisationId"`
	Period         string   `gorm:"column:period;uniqueIndex:OrganisationMonthlyStat_organisationId_period_key;not null" json:"period"`
	DocumentCount  int      `gorm:"column:documentCount;default:0;not null" json:"documentCount"`
	EmailCount     int      `gorm:"column:emailCount;default:0;not null" json:"emailCount"`
	APICount       int      `gorm:"column:apiCount;default:0;not null" json:"apiCount"`
	EmailReports   int      `gorm:"column:emailReports;default:0;not null" json:"emailReports"`
	Organisation   *Organisation
}

func (OrganisationMonthlyStat) TableName() string {
	return "OrganisationMonthlyStat"
}

func (m *OrganisationMonthlyStat) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*OrganisationMonthlyStat) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Account struct {
	ID                string   `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt         DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UserID            int      `gorm:"column:userId;not null" json:"userId"`
	Type              string   `gorm:"column:type;not null" json:"type"`
	Provider          string   `gorm:"column:provider;uniqueIndex:Account_provider_providerAccountId_key;not null" json:"provider"`
	ProviderAccountID string   `gorm:"column:providerAccountId;uniqueIndex:Account_provider_providerAccountId_key;not null" json:"providerAccountId"`
	RefreshToken      *string  `gorm:"column:refresh_token;type:text" json:"refresh_token"`
	AccessToken       *string  `gorm:"column:access_token;type:text" json:"access_token"`
	ExpiresAt         *int     `gorm:"column:expires_at" json:"expires_at"`
	CreatedAt         *int     `gorm:"column:created_at" json:"created_at"`
	ExtExpiresIn      *int     `gorm:"column:ext_expires_in" json:"ext_expires_in"`
	TokenType         *string  `gorm:"column:token_type" json:"token_type"`
	Scope             *string  `gorm:"column:scope" json:"scope"`
	IDToken           *string  `gorm:"column:id_token;type:text" json:"id_token"`
	SessionState      *string  `gorm:"column:session_state" json:"session_state"`
	Password          *string  `gorm:"column:password" json:"password"`
	User              *User
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
	return nil
}

type Session struct {
	ID           string   `gorm:"column:id;primaryKey" json:"id"`
	SessionToken string   `gorm:"column:sessionToken;uniqueIndex;index:Session_sessionToken_idx;not null" json:"sessionToken"`
	UserID       int      `gorm:"column:userId;index:Session_userId_idx;not null" json:"userId"`
	IPAddress    *string  `gorm:"column:ipAddress" json:"ipAddress"`
	UserAgent    *string  `gorm:"column:userAgent" json:"userAgent"`
	ExpiresAt    DateTime `gorm:"column:expiresAt;not null" json:"expiresAt"`
	CreatedAt    DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt    DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	IsEmbed      bool     `gorm:"column:isEmbed;default:false;not null" json:"isEmbed"`
	User         *User
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

type AuthHandoff struct {
	ID        string   `gorm:"column:id;primaryKey" json:"id"`
	Type      string   `gorm:"column:type;not null" json:"type"`
	Payload   string   `gorm:"column:payload;not null" json:"payload"`
	CreatedAt DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	ExpiresAt DateTime `gorm:"column:expiresAt;index:AuthHandoff_expiresAt_idx;not null" json:"expiresAt"`
}

func (AuthHandoff) TableName() string {
	return "AuthHandoff"
}

func (m *AuthHandoff) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type Folder struct {
	ID         string   `gorm:"column:id;primaryKey" json:"id"`
	Name       string   `gorm:"column:name;not null" json:"name"`
	UserID     int      `gorm:"column:userId;index:Folder_userId_idx;not null" json:"userId"`
	TeamID     int      `gorm:"column:teamId;index:Folder_teamId_idx;not null" json:"teamId"`
	Pinned     bool     `gorm:"column:pinned;default:false;not null" json:"pinned"`
	ParentID   *string  `gorm:"column:parentId;index:Folder_parentId_idx" json:"parentId"`
	CreatedAt  DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt  DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Visibility string   `gorm:"column:visibility;default:'EVERYONE';not null" json:"visibility"`
	Type       string   `gorm:"column:type;index:Folder_type_idx;not null" json:"type"`
	User       *User
	Team       *Team
	Parent     *Folder    `gorm:"foreignKey:ParentID"`
	Envelopes  []Envelope `gorm:"foreignKey:FolderID;constraint:OnDelete:SET NULL"`
	Subfolders []Folder   `gorm:"foreignKey:ParentID;constraint:OnDelete:CASCADE"`
}

func (Folder) TableName() string {
	return "Folder"
}

func (m *Folder) BeforeCreate(tx *gorm.DB) error {
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

func (*Folder) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Envelope struct {
	ID                      string         `gorm:"column:id;primaryKey" json:"id"`
	SecondaryID             string         `gorm:"column:secondaryId;uniqueIndex;not null" json:"secondaryId"`
	ExternalID              *string        `gorm:"column:externalId" json:"externalId"`
	Type                    string         `gorm:"column:type;index:Envelope_type_idx;not null" json:"type"`
	CreatedAt               DateTime       `gorm:"column:createdAt;index:Envelope_createdAt_idx;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt               DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	CompletedAt             *DateTime      `gorm:"column:completedAt" json:"completedAt"`
	DeletedAt               *DateTime      `gorm:"column:deletedAt" json:"deletedAt"`
	Title                   string         `gorm:"column:title;not null" json:"title"`
	Status                  string         `gorm:"column:status;index:Envelope_status_idx;default:'DRAFT';not null" json:"status"`
	Source                  string         `gorm:"column:source;not null" json:"source"`
	QrToken                 *string        `gorm:"column:qrToken" json:"qrToken"`
	SignatureLevel          string         `gorm:"column:signatureLevel;not null" json:"signatureLevel"`
	InternalVersion         int            `gorm:"column:internalVersion;not null" json:"internalVersion"`
	UseLegacyFieldInsertion bool           `gorm:"column:useLegacyFieldInsertion;default:false;not null" json:"useLegacyFieldInsertion"`
	AuthOptions             datatypes.JSON `gorm:"column:authOptions" json:"authOptions"`
	FormValues              datatypes.JSON `gorm:"column:formValues" json:"formValues"`
	Visibility              string         `gorm:"column:visibility;default:'EVERYONE';not null" json:"visibility"`
	TemplateType            string         `gorm:"column:templateType;default:'PRIVATE';not null" json:"templateType"`
	PublicTitle             string         `gorm:"column:publicTitle;default:'';not null" json:"publicTitle"`
	PublicDescription       string         `gorm:"column:publicDescription;default:'';not null" json:"publicDescription"`
	TemplateID              *int           `gorm:"column:templateId" json:"templateId"`
	UserID                  int            `gorm:"column:userId;index:Envelope_userId_idx;not null" json:"userId"`
	TeamID                  int            `gorm:"column:teamId;index:Envelope_teamId_idx;not null" json:"teamId"`
	FolderID                *string        `gorm:"column:folderId;index:Envelope_folderId_idx" json:"folderId"`
	DocumentMetaID          string         `gorm:"column:documentMetaId;uniqueIndex;not null" json:"documentMetaId"`
	User                    *User
	Team                    *Team
	Folder                  *Folder
	DocumentMeta            *DocumentMeta
	EnvelopeItems           []EnvelopeItem       `gorm:"foreignKey:EnvelopeID;constraint:OnDelete:CASCADE"`
	Recipients              []Recipient          `gorm:"foreignKey:EnvelopeID;constraint:OnDelete:CASCADE"`
	Fields                  []Field              `gorm:"foreignKey:EnvelopeID;constraint:OnDelete:CASCADE"`
	ShareLinks              []DocumentShareLink  `gorm:"foreignKey:EnvelopeID;constraint:OnDelete:CASCADE"`
	AuditLogs               []DocumentAuditLog   `gorm:"foreignKey:EnvelopeID;constraint:OnDelete:SET NULL"`
	EnvelopeAttachments     []EnvelopeAttachment `gorm:"foreignKey:EnvelopeID;constraint:OnDelete:CASCADE"`
	DirectLink              *TemplateDirectLink  `gorm:"foreignKey:EnvelopeID;constraint:OnDelete:CASCADE"`
}

func (Envelope) TableName() string {
	return "Envelope"
}

func (m *Envelope) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*Envelope) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type EnvelopeItem struct {
	ID             string `gorm:"column:id;primaryKey" json:"id"`
	Title          string `gorm:"column:title;not null" json:"title"`
	Order          int    `gorm:"column:order;not null" json:"order"`
	DocumentDataID string `gorm:"column:documentDataId;uniqueIndex;not null" json:"documentDataId"`
	EnvelopeID     string `gorm:"column:envelopeId;index:EnvelopeItem_envelopeId_idx;not null" json:"envelopeId"`
	DocumentData   *DocumentData
	Envelope       *Envelope
	Field          []Field `gorm:"foreignKey:EnvelopeItemID;constraint:OnDelete:CASCADE"`
}

func (EnvelopeItem) TableName() string {
	return "EnvelopeItem"
}

type DocumentAuditLog struct {
	ID         string         `gorm:"column:id;primaryKey" json:"id"`
	EnvelopeID *string        `gorm:"column:envelopeId;index:DocumentAuditLog_envelopeId_idx" json:"envelopeId"`
	CreatedAt  DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	Type       string         `gorm:"column:type;not null" json:"type"`
	Data       datatypes.JSON `gorm:"column:data;not null" json:"data"`
	Name       *string        `gorm:"column:name" json:"name"`
	Email      *string        `gorm:"column:email" json:"email"`
	UserID     *int           `gorm:"column:userId" json:"userId"`
	UserAgent  *string        `gorm:"column:userAgent" json:"userAgent"`
	IPAddress  *string        `gorm:"column:ipAddress" json:"ipAddress"`
	Envelope   *Envelope
}

func (DocumentAuditLog) TableName() string {
	return "DocumentAuditLog"
}

func (m *DocumentAuditLog) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type DocumentData struct {
	ID           string        `gorm:"column:id;primaryKey" json:"id"`
	Type         string        `gorm:"column:type;not null" json:"type"`
	Data         string        `gorm:"column:data;not null" json:"data"`
	InitialData  string        `gorm:"column:initialData;not null" json:"initialData"`
	EnvelopeItem *EnvelopeItem `gorm:"foreignKey:DocumentDataID;constraint:OnDelete:CASCADE"`
}

func (DocumentData) TableName() string {
	return "DocumentData"
}

func (m *DocumentData) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = cuid.New()
	}
	return nil
}

type DocumentMeta struct {
	ID                       string         `gorm:"column:id;primaryKey" json:"id"`
	Subject                  *string        `gorm:"column:subject" json:"subject"`
	Message                  *string        `gorm:"column:message" json:"message"`
	Timezone                 *string        `gorm:"column:timezone;type:text;default:'Etc/UTC'" json:"timezone"`
	DateFormat               *string        `gorm:"column:dateFormat;type:text;default:'yyyy-MM-dd hh:mm a'" json:"dateFormat"`
	RedirectURL              *string        `gorm:"column:redirectUrl" json:"redirectUrl"`
	SigningOrder             string         `gorm:"column:signingOrder;default:'PARALLEL';not null" json:"signingOrder"`
	AllowDictateNextSigner   bool           `gorm:"column:allowDictateNextSigner;default:false;not null" json:"allowDictateNextSigner"`
	TypedSignatureEnabled    *bool          `gorm:"column:typedSignatureEnabled;default:true;not null" json:"typedSignatureEnabled"`
	UploadSignatureEnabled   *bool          `gorm:"column:uploadSignatureEnabled;default:true;not null" json:"uploadSignatureEnabled"`
	DrawSignatureEnabled     *bool          `gorm:"column:drawSignatureEnabled;default:true;not null" json:"drawSignatureEnabled"`
	Language                 *string        `gorm:"column:language;default:'en';not null" json:"language"`
	DistributionMethod       string         `gorm:"column:distributionMethod;default:'EMAIL';not null" json:"distributionMethod"`
	EmailSettings            datatypes.JSON `gorm:"column:emailSettings" json:"emailSettings"`
	EmailReplyTo             *string        `gorm:"column:emailReplyTo" json:"emailReplyTo"`
	EmailID                  *string        `gorm:"column:emailId" json:"emailId"`
	EnvelopeExpirationPeriod datatypes.JSON `gorm:"column:envelopeExpirationPeriod" json:"envelopeExpirationPeriod"`
	ReminderSettings         datatypes.JSON `gorm:"column:reminderSettings" json:"reminderSettings"`
	Envelope                 *Envelope      `gorm:"foreignKey:DocumentMetaID"`
}

func (DocumentMeta) TableName() string {
	return "DocumentMeta"
}

func (m *DocumentMeta) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = cuid.New()
	}
	return nil
}

type EnvelopeAttachment struct {
	ID         string   `gorm:"column:id;primaryKey" json:"id"`
	Type       string   `gorm:"column:type;not null" json:"type"`
	Label      string   `gorm:"column:label;not null" json:"label"`
	Data       string   `gorm:"column:data;not null" json:"data"`
	CreatedAt  DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt  DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	EnvelopeID string   `gorm:"column:envelopeId;index:EnvelopeAttachment_envelopeId_idx;not null" json:"envelopeId"`
	Envelope   *Envelope
}

func (EnvelopeAttachment) TableName() string {
	return "EnvelopeAttachment"
}

func (m *EnvelopeAttachment) BeforeCreate(tx *gorm.DB) error {
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

func (*EnvelopeAttachment) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Recipient struct {
	ID                   int            `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	EnvelopeID           string         `gorm:"column:envelopeId;index:Recipient_envelopeId_idx;index:Recipient_email_documentDeletedAt_envelopeId_idx;index:Recipient_email_envelopeId_idx;index:Recipient_email_signingStatus_envelopeId_role_idx;not null" json:"envelopeId"`
	Email                string         `gorm:"column:email;index:Recipient_email_idx;index:Recipient_email_documentDeletedAt_envelopeId_idx;index:Recipient_email_envelopeId_idx;index:Recipient_email_signingStatus_envelopeId_role_idx;index:Recipient_email_trgm_idx;type:varchar(255);not null" json:"email"`
	Name                 string         `gorm:"column:name;index:Recipient_name_trgm_idx;type:varchar(255);default:'';not null" json:"name"`
	Token                string         `gorm:"column:token;index:Recipient_token_idx;not null" json:"token"`
	DocumentDeletedAt    *DateTime      `gorm:"column:documentDeletedAt;index:Recipient_email_documentDeletedAt_envelopeId_idx" json:"documentDeletedAt"`
	Expired              *DateTime      `gorm:"column:expired" json:"expired"`
	ExpiresAt            *DateTime      `gorm:"column:expiresAt;index:Recipient_expiresAt_idx" json:"expiresAt"`
	ExpirationNotifiedAt *DateTime      `gorm:"column:expirationNotifiedAt" json:"expirationNotifiedAt"`
	SentAt               *DateTime      `gorm:"column:sentAt" json:"sentAt"`
	SignedAt             *DateTime      `gorm:"column:signedAt;index:Recipient_signedAt_idx" json:"signedAt"`
	LastReminderSentAt   *DateTime      `gorm:"column:lastReminderSentAt" json:"lastReminderSentAt"`
	NextReminderAt       *DateTime      `gorm:"column:nextReminderAt;index:Recipient_nextReminderAt_idx" json:"nextReminderAt"`
	ReminderCount        int            `gorm:"column:reminderCount;default:0;not null" json:"reminderCount"`
	AuthOptions          datatypes.JSON `gorm:"column:authOptions" json:"authOptions"`
	SigningOrder         *int           `gorm:"column:signingOrder" json:"signingOrder"`
	RejectionReason      *string        `gorm:"column:rejectionReason" json:"rejectionReason"`
	Role                 string         `gorm:"column:role;index:Recipient_email_signingStatus_envelopeId_role_idx;default:'SIGNER';not null" json:"role"`
	ReadStatus           string         `gorm:"column:readStatus;default:'NOT_OPENED';not null" json:"readStatus"`
	SigningStatus        string         `gorm:"column:signingStatus;index:Recipient_email_signingStatus_envelopeId_role_idx;default:'NOT_SIGNED';not null" json:"signingStatus"`
	SendStatus           string         `gorm:"column:sendStatus;default:'NOT_SENT';not null" json:"sendStatus"`
	Envelope             *Envelope
	Fields               []Field        `gorm:"foreignKey:RecipientID;constraint:OnDelete:CASCADE"`
	Signatures           []Signature    `gorm:"foreignKey:RecipientID;constraint:OnDelete:CASCADE"`
	CscCredential        *CscCredential `gorm:"foreignKey:RecipientID;constraint:OnDelete:CASCADE"`
	CscSession           *CscSession    `gorm:"foreignKey:RecipientID;constraint:OnDelete:CASCADE"`
}

func (Recipient) TableName() string {
	return "Recipient"
}

type Field struct {
	ID             int            `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	SecondaryID    string         `gorm:"column:secondaryId;uniqueIndex;not null" json:"secondaryId"`
	EnvelopeID     string         `gorm:"column:envelopeId;index:Field_envelopeId_idx;not null" json:"envelopeId"`
	EnvelopeItemID string         `gorm:"column:envelopeItemId;index:Field_envelopeItemId_idx;not null" json:"envelopeItemId"`
	RecipientID    int            `gorm:"column:recipientId;index:Field_recipientId_idx;not null" json:"recipientId"`
	Type           string         `gorm:"column:type;not null" json:"type"`
	Page           int            `gorm:"column:page;not null" json:"page"`
	PositionX      float64        `gorm:"column:positionX;default:0;not null" json:"positionX"`
	PositionY      float64        `gorm:"column:positionY;default:0;not null" json:"positionY"`
	Width          *float64       `gorm:"column:width;default:-1;not null" json:"width"`
	Height         *float64       `gorm:"column:height;default:-1;not null" json:"height"`
	CustomText     string         `gorm:"column:customText;not null" json:"customText"`
	Inserted       bool           `gorm:"column:inserted;not null" json:"inserted"`
	FieldMeta      datatypes.JSON `gorm:"column:fieldMeta" json:"fieldMeta"`
	EnvelopeItem   *EnvelopeItem
	Envelope       *Envelope
	Recipient      *Recipient
	Signature      *Signature `gorm:"foreignKey:FieldID;constraint:OnDelete:CASCADE"`
}

func (Field) TableName() string {
	return "Field"
}

func (m *Field) BeforeCreate(_ *gorm.DB) error {
	if m.SecondaryID == "" {
		m.SecondaryID = cuid.New()
	}
	return nil
}

type Signature struct {
	ID                     int      `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	Created                DateTime `gorm:"column:created;not null" json:"created"`
	RecipientID            int      `gorm:"column:recipientId;index:Signature_recipientId_idx;not null" json:"recipientId"`
	FieldID                int      `gorm:"column:fieldId;uniqueIndex;not null" json:"fieldId"`
	SignatureImageAsBase64 *string  `gorm:"column:signatureImageAsBase64" json:"signatureImageAsBase64"`
	TypedSignature         *string  `gorm:"column:typedSignature" json:"typedSignature"`
	Recipient              *Recipient
	Field                  *Field
}

func (Signature) TableName() string {
	return "Signature"
}

func (m *Signature) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.Created.IsZero() {
		m.Created = DateTime{Time: now}
	}
	return nil
}

type CscCredential struct {
	ID                     string    `gorm:"column:id;primaryKey" json:"id"`
	ProviderID             string    `gorm:"column:providerId;not null" json:"providerId"`
	CredentialID           string    `gorm:"column:credentialId;not null" json:"credentialId"`
	CertCache              []byte    `gorm:"column:certCache" json:"certCache"`
	SignatureAlgorithm     string    `gorm:"column:signatureAlgorithm;not null" json:"signatureAlgorithm"`
	KeyType                string    `gorm:"column:keyType;not null" json:"keyType"`
	DigestAlgorithm        string    `gorm:"column:digestAlgorithm;not null" json:"digestAlgorithm"`
	KeyLenBits             *int      `gorm:"column:keyLenBits" json:"keyLenBits"`
	SignAlgoParams         *string   `gorm:"column:signAlgoParams" json:"signAlgoParams"`
	ServiceTokenCiphertext []byte    `gorm:"column:serviceTokenCiphertext" json:"serviceTokenCiphertext"`
	ServiceTokenExpiresAt  *DateTime `gorm:"column:serviceTokenExpiresAt" json:"serviceTokenExpiresAt"`
	RecipientID            int       `gorm:"column:recipientId;uniqueIndex;not null" json:"recipientId"`
	CreatedAt              DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt              DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Recipient              *Recipient
}

func (CscCredential) TableName() string {
	return "CscCredential"
}

func (m *CscCredential) BeforeCreate(tx *gorm.DB) error {
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

func (*CscCredential) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type CscSession struct {
	ID           string         `gorm:"column:id;primaryKey" json:"id"`
	EnvelopeID   string         `gorm:"column:envelopeId;not null" json:"envelopeId"`
	SigningTime  DateTime       `gorm:"column:signingTime;not null" json:"signingTime"`
	ItemsJSON    datatypes.JSON `gorm:"column:itemsJson;not null" json:"itemsJson"`
	EncryptedSad []byte         `gorm:"column:encryptedSad" json:"encryptedSad"`
	SadExpiresAt *DateTime      `gorm:"column:sadExpiresAt" json:"sadExpiresAt"`
	CreatedAt    DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	RecipientID  int            `gorm:"column:recipientId;uniqueIndex;not null" json:"recipientId"`
	Recipient    *Recipient
}

func (CscSession) TableName() string {
	return "CscSession"
}

func (m *CscSession) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type DocumentShareLink struct {
	ID         int      `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	Email      string   `gorm:"column:email;uniqueIndex:DocumentShareLink_envelopeId_email_key;not null" json:"email"`
	Slug       string   `gorm:"column:slug;uniqueIndex;not null" json:"slug"`
	EnvelopeID string   `gorm:"column:envelopeId;uniqueIndex:DocumentShareLink_envelopeId_email_key;not null" json:"envelopeId"`
	CreatedAt  DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt  DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Envelope   *Envelope
}

func (DocumentShareLink) TableName() string {
	return "DocumentShareLink"
}

func (m *DocumentShareLink) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*DocumentShareLink) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Organisation struct {
	ID                                 string   `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt                          DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt                          DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Type                               string   `gorm:"column:type;not null" json:"type"`
	Name                               string   `gorm:"column:name;index:Organisation_name_idx;not null" json:"name"`
	URL                                string   `gorm:"column:url;uniqueIndex;not null" json:"url"`
	AvatarImageID                      *string  `gorm:"column:avatarImageId" json:"avatarImageId"`
	CustomerID                         *string  `gorm:"column:customerId;uniqueIndex" json:"customerId"`
	OrganisationClaimID                string   `gorm:"column:organisationClaimId;uniqueIndex;not null" json:"organisationClaimId"`
	OwnerUserID                        int      `gorm:"column:ownerUserId;index:Organisation_ownerUserId_idx;not null" json:"ownerUserId"`
	OrganisationGlobalSettingsID       string   `gorm:"column:organisationGlobalSettingsId;uniqueIndex;not null" json:"organisationGlobalSettingsId"`
	OrganisationAuthenticationPortalID string   `gorm:"column:organisationAuthenticationPortalId;uniqueIndex;not null" json:"organisationAuthenticationPortalId"`
	OrganisationClaim                  *OrganisationClaim
	AvatarImage                        *AvatarImage
	Owner                              *User `gorm:"foreignKey:OwnerUserID"`
	OrganisationGlobalSettings         *OrganisationGlobalSettings
	OrganisationAuthenticationPortal   *OrganisationAuthenticationPortal
	Members                            []OrganisationMember       `gorm:"foreignKey:OrganisationID;constraint:OnDelete:CASCADE"`
	Invites                            []OrganisationMemberInvite `gorm:"foreignKey:OrganisationID;constraint:OnDelete:CASCADE"`
	Groups                             []OrganisationGroup        `gorm:"foreignKey:OrganisationID;constraint:OnDelete:CASCADE"`
	Teams                              []Team                     `gorm:"foreignKey:OrganisationID;constraint:OnDelete:CASCADE"`
	EmailDomains                       []EmailDomain              `gorm:"foreignKey:OrganisationID;constraint:OnDelete:CASCADE"`
	OrganisationEmails                 []OrganisationEmail        `gorm:"foreignKey:OrganisationID;constraint:OnDelete:CASCADE"`
	MonthlyStats                       []OrganisationMonthlyStat  `gorm:"foreignKey:OrganisationID;constraint:OnDelete:CASCADE"`
	EmbedSignatures                    []EmbedSignature           `gorm:"foreignKey:OrganisationID;constraint:OnDelete:CASCADE"`
	Subscription                       *Subscription              `gorm:"foreignKey:OrganisationID;constraint:OnDelete:CASCADE"`
}

func (Organisation) TableName() string {
	return "Organisation"
}

func (m *Organisation) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*Organisation) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type OrganisationMember struct {
	ID                       string   `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt                DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt                DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	UserID                   int      `gorm:"column:userId;uniqueIndex:OrganisationMember_userId_organisationId_key;not null" json:"userId"`
	OrganisationID           string   `gorm:"column:organisationId;uniqueIndex:OrganisationMember_userId_organisationId_key;index:OrganisationMember_organisationId_idx;not null" json:"organisationId"`
	User                     *User
	Organisation             *Organisation
	OrganisationGroupMembers []OrganisationGroupMember `gorm:"foreignKey:OrganisationMemberID;constraint:OnDelete:CASCADE"`
}

func (OrganisationMember) TableName() string {
	return "OrganisationMember"
}

func (m *OrganisationMember) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*OrganisationMember) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type OrganisationMemberInvite struct {
	ID               string   `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt        DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	Email            string   `gorm:"column:email;not null" json:"email"`
	Token            string   `gorm:"column:token;uniqueIndex;not null" json:"token"`
	Status           string   `gorm:"column:status;default:'PENDING';not null" json:"status"`
	OrganisationID   string   `gorm:"column:organisationId;not null" json:"organisationId"`
	OrganisationRole string   `gorm:"column:organisationRole;not null" json:"organisationRole"`
	Organisation     *Organisation
}

func (OrganisationMemberInvite) TableName() string {
	return "OrganisationMemberInvite"
}

func (m *OrganisationMemberInvite) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type OrganisationGroup struct {
	ID                       string  `gorm:"column:id;primaryKey" json:"id"`
	Name                     *string `gorm:"column:name" json:"name"`
	Type                     string  `gorm:"column:type;not null" json:"type"`
	OrganisationRole         string  `gorm:"column:organisationRole;not null" json:"organisationRole"`
	OrganisationID           string  `gorm:"column:organisationId;index:OrganisationGroup_organisationId_idx;not null" json:"organisationId"`
	Organisation             *Organisation
	OrganisationGroupMembers []OrganisationGroupMember `gorm:"foreignKey:GroupID;constraint:OnDelete:CASCADE"`
	TeamGroups               []TeamGroup               `gorm:"foreignKey:OrganisationGroupID;constraint:OnDelete:CASCADE"`
}

func (OrganisationGroup) TableName() string {
	return "OrganisationGroup"
}

type OrganisationGroupMember struct {
	ID                   string             `gorm:"column:id;primaryKey" json:"id"`
	GroupID              string             `gorm:"column:groupId;uniqueIndex:OrganisationGroupMember_organisationMemberId_groupId_key;index:OrganisationGroupMember_groupId_idx;not null" json:"groupId"`
	OrganisationMemberID string             `gorm:"column:organisationMemberId;uniqueIndex:OrganisationGroupMember_organisationMemberId_groupId_key;index:OrganisationGroupMember_organisationMemberId_idx;not null" json:"organisationMemberId"`
	Group                *OrganisationGroup `gorm:"foreignKey:GroupID"`
	OrganisationMember   *OrganisationMember
}

func (OrganisationGroupMember) TableName() string {
	return "OrganisationGroupMember"
}

type TeamGroup struct {
	ID                  string `gorm:"column:id;primaryKey" json:"id"`
	OrganisationGroupID string `gorm:"column:organisationGroupId;uniqueIndex:TeamGroup_teamId_organisationGroupId_key;index:TeamGroup_organisationGroupId_idx;not null" json:"organisationGroupId"`
	TeamRole            string `gorm:"column:teamRole;not null" json:"teamRole"`
	TeamID              int    `gorm:"column:teamId;uniqueIndex:TeamGroup_teamId_organisationGroupId_key;index:TeamGroup_teamId_idx;not null" json:"teamId"`
	OrganisationGroup   *OrganisationGroup
	Team                *Team
}

func (TeamGroup) TableName() string {
	return "TeamGroup"
}

type OrganisationGlobalSettings struct {
	ID                        string             `gorm:"column:id;primaryKey" json:"id"`
	DocumentVisibility        string             `gorm:"column:documentVisibility;default:'EVERYONE';not null" json:"documentVisibility"`
	DocumentLanguage          *string            `gorm:"column:documentLanguage;default:'en';not null" json:"documentLanguage"`
	IncludeSenderDetails      *bool              `gorm:"column:includeSenderDetails;default:true;not null" json:"includeSenderDetails"`
	IncludeSigningCertificate *bool              `gorm:"column:includeSigningCertificate;default:true;not null" json:"includeSigningCertificate"`
	IncludeAuditLog           bool               `gorm:"column:includeAuditLog;default:false;not null" json:"includeAuditLog"`
	DocumentTimezone          *string            `gorm:"column:documentTimezone" json:"documentTimezone"`
	DocumentDateFormat        *string            `gorm:"column:documentDateFormat;default:'yyyy-MM-dd hh:mm a';not null" json:"documentDateFormat"`
	DelegateDocumentOwnership bool               `gorm:"column:delegateDocumentOwnership;default:false;not null" json:"delegateDocumentOwnership"`
	TypedSignatureEnabled     *bool              `gorm:"column:typedSignatureEnabled;default:true;not null" json:"typedSignatureEnabled"`
	UploadSignatureEnabled    *bool              `gorm:"column:uploadSignatureEnabled;default:true;not null" json:"uploadSignatureEnabled"`
	DrawSignatureEnabled      *bool              `gorm:"column:drawSignatureEnabled;default:true;not null" json:"drawSignatureEnabled"`
	DefaultRecipients         datatypes.JSON     `gorm:"column:defaultRecipients" json:"defaultRecipients"`
	EmailID                   *string            `gorm:"column:emailId" json:"emailId"`
	EmailReplyTo              *string            `gorm:"column:emailReplyTo" json:"emailReplyTo"`
	EmailDocumentSettings     datatypes.JSON     `gorm:"column:emailDocumentSettings;not null" json:"emailDocumentSettings"`
	BrandingEnabled           bool               `gorm:"column:brandingEnabled;default:false;not null" json:"brandingEnabled"`
	BrandingLogo              string             `gorm:"column:brandingLogo;default:'';not null" json:"brandingLogo"`
	BrandingURL               string             `gorm:"column:brandingUrl;default:'';not null" json:"brandingUrl"`
	BrandingCompanyDetails    string             `gorm:"column:brandingCompanyDetails;default:'';not null" json:"brandingCompanyDetails"`
	BrandingColors            datatypes.JSON     `gorm:"column:brandingColors" json:"brandingColors"`
	BrandingCSS               string             `gorm:"column:brandingCss;default:'';not null" json:"brandingCss"`
	EnvelopeExpirationPeriod  datatypes.JSON     `gorm:"column:envelopeExpirationPeriod" json:"envelopeExpirationPeriod"`
	ReminderSettings          datatypes.JSON     `gorm:"column:reminderSettings" json:"reminderSettings"`
	AiFeaturesEnabled         bool               `gorm:"column:aiFeaturesEnabled;default:false;not null" json:"aiFeaturesEnabled"`
	Email                     *OrganisationEmail `gorm:"foreignKey:EmailID"`
	Organisation              *Organisation      `gorm:"foreignKey:OrganisationGlobalSettingsID"`
}

func (OrganisationGlobalSettings) TableName() string {
	return "OrganisationGlobalSettings"
}

type TeamGlobalSettings struct {
	ID                        string             `gorm:"column:id;primaryKey" json:"id"`
	DocumentVisibility        *string            `gorm:"column:documentVisibility" json:"documentVisibility"`
	DocumentLanguage          *string            `gorm:"column:documentLanguage" json:"documentLanguage"`
	DocumentTimezone          *string            `gorm:"column:documentTimezone" json:"documentTimezone"`
	DocumentDateFormat        *string            `gorm:"column:documentDateFormat" json:"documentDateFormat"`
	DelegateDocumentOwnership *bool              `gorm:"column:delegateDocumentOwnership" json:"delegateDocumentOwnership"`
	IncludeSenderDetails      *bool              `gorm:"column:includeSenderDetails" json:"includeSenderDetails"`
	IncludeSigningCertificate *bool              `gorm:"column:includeSigningCertificate" json:"includeSigningCertificate"`
	IncludeAuditLog           *bool              `gorm:"column:includeAuditLog" json:"includeAuditLog"`
	TypedSignatureEnabled     *bool              `gorm:"column:typedSignatureEnabled" json:"typedSignatureEnabled"`
	UploadSignatureEnabled    *bool              `gorm:"column:uploadSignatureEnabled" json:"uploadSignatureEnabled"`
	DrawSignatureEnabled      *bool              `gorm:"column:drawSignatureEnabled" json:"drawSignatureEnabled"`
	DefaultRecipients         datatypes.JSON     `gorm:"column:defaultRecipients" json:"defaultRecipients"`
	EmailID                   *string            `gorm:"column:emailId" json:"emailId"`
	EmailReplyTo              *string            `gorm:"column:emailReplyTo" json:"emailReplyTo"`
	EmailDocumentSettings     datatypes.JSON     `gorm:"column:emailDocumentSettings" json:"emailDocumentSettings"`
	BrandingEnabled           *bool              `gorm:"column:brandingEnabled" json:"brandingEnabled"`
	BrandingLogo              *string            `gorm:"column:brandingLogo" json:"brandingLogo"`
	BrandingURL               *string            `gorm:"column:brandingUrl" json:"brandingUrl"`
	BrandingCompanyDetails    *string            `gorm:"column:brandingCompanyDetails" json:"brandingCompanyDetails"`
	BrandingColors            datatypes.JSON     `gorm:"column:brandingColors" json:"brandingColors"`
	BrandingCSS               *string            `gorm:"column:brandingCss" json:"brandingCss"`
	EnvelopeExpirationPeriod  datatypes.JSON     `gorm:"column:envelopeExpirationPeriod" json:"envelopeExpirationPeriod"`
	ReminderSettings          datatypes.JSON     `gorm:"column:reminderSettings" json:"reminderSettings"`
	AiFeaturesEnabled         *bool              `gorm:"column:aiFeaturesEnabled" json:"aiFeaturesEnabled"`
	Email                     *OrganisationEmail `gorm:"foreignKey:EmailID"`
	Team                      *Team              `gorm:"foreignKey:TeamGlobalSettingsID;constraint:OnDelete:CASCADE"`
}

func (TeamGlobalSettings) TableName() string {
	return "TeamGlobalSettings"
}

type Team struct {
	ID                   int      `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	Name                 string   `gorm:"column:name;index:Team_name_idx;not null" json:"name"`
	URL                  string   `gorm:"column:url;uniqueIndex;not null" json:"url"`
	CreatedAt            DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	AvatarImageID        *string  `gorm:"column:avatarImageId" json:"avatarImageId"`
	OrganisationID       string   `gorm:"column:organisationId;index:Team_organisationId_idx;not null" json:"organisationId"`
	TeamGlobalSettingsID string   `gorm:"column:teamGlobalSettingsId;uniqueIndex;not null" json:"teamGlobalSettingsId"`
	AvatarImage          *AvatarImage
	Organisation         *Organisation
	TeamGlobalSettings   *TeamGlobalSettings
	Envelopes            []Envelope             `gorm:"foreignKey:TeamID;constraint:OnDelete:CASCADE"`
	Folders              []Folder               `gorm:"foreignKey:TeamID;constraint:OnDelete:CASCADE"`
	APITokens            []APIToken             `gorm:"foreignKey:TeamID;constraint:OnDelete:CASCADE"`
	Webhooks             []Webhook              `gorm:"foreignKey:TeamID;constraint:OnDelete:CASCADE"`
	TeamGroups           []TeamGroup            `gorm:"foreignKey:TeamID;constraint:OnDelete:CASCADE"`
	TeamEmail            *TeamEmail             `gorm:"foreignKey:TeamID;constraint:OnDelete:CASCADE"`
	EmailVerification    *TeamEmailVerification `gorm:"foreignKey:TeamID;constraint:OnDelete:CASCADE"`
	Profile              *TeamProfile           `gorm:"foreignKey:TeamID;constraint:OnDelete:CASCADE"`
}

func (Team) TableName() string {
	return "Team"
}

func (m *Team) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type TeamEmail struct {
	TeamID    int      `gorm:"column:teamId;primaryKey" json:"teamId"`
	CreatedAt DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	Name      string   `gorm:"column:name;not null" json:"name"`
	Email     string   `gorm:"column:email;uniqueIndex;not null" json:"email"`
	Team      *Team
}

func (TeamEmail) TableName() string {
	return "TeamEmail"
}

func (m *TeamEmail) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type TeamEmailVerification struct {
	TeamID    int      `gorm:"column:teamId;primaryKey" json:"teamId"`
	Name      string   `gorm:"column:name;not null" json:"name"`
	Email     string   `gorm:"column:email;not null" json:"email"`
	Token     string   `gorm:"column:token;uniqueIndex;not null" json:"token"`
	Completed bool     `gorm:"column:completed;default:false;not null" json:"completed"`
	ExpiresAt DateTime `gorm:"column:expiresAt;not null" json:"expiresAt"`
	CreatedAt DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	Team      *Team
}

func (TeamEmailVerification) TableName() string {
	return "TeamEmailVerification"
}

func (m *TeamEmailVerification) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type TemplateDirectLink struct {
	ID                        string   `gorm:"column:id;primaryKey" json:"id"`
	EnvelopeID                string   `gorm:"column:envelopeId;uniqueIndex;not null" json:"envelopeId"`
	Token                     string   `gorm:"column:token;uniqueIndex;not null" json:"token"`
	CreatedAt                 DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	Enabled                   bool     `gorm:"column:enabled;not null" json:"enabled"`
	DirectTemplateRecipientID int      `gorm:"column:directTemplateRecipientId;not null" json:"directTemplateRecipientId"`
	Envelope                  *Envelope
}

func (TemplateDirectLink) TableName() string {
	return "TemplateDirectLink"
}

func (m *TemplateDirectLink) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type SiteSettings struct {
	ID                   string         `gorm:"column:id;primaryKey" json:"id"`
	Enabled              bool           `gorm:"column:enabled;default:false;not null" json:"enabled"`
	Data                 datatypes.JSON `gorm:"column:data;not null" json:"data"`
	LastModifiedByUserID *int           `gorm:"column:lastModifiedByUserId" json:"lastModifiedByUserId"`
	LastModifiedAt       DateTime       `gorm:"column:lastModifiedAt;not null" json:"lastModifiedAt"`
	LastModifiedByUser   *User          `gorm:"foreignKey:LastModifiedByUserID"`
}

func (SiteSettings) TableName() string {
	return "SiteSettings"
}

func (m *SiteSettings) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.LastModifiedAt.IsZero() {
		m.LastModifiedAt = DateTime{Time: now}
	}
	return nil
}

type BackgroundJob struct {
	ID            string              `gorm:"column:id;primaryKey" json:"id"`
	Status        string              `gorm:"column:status;default:'PENDING';not null" json:"status"`
	Payload       datatypes.JSON      `gorm:"column:payload" json:"payload"`
	Retried       int                 `gorm:"column:retried;default:0;not null" json:"retried"`
	MaxRetries    *int                `gorm:"column:maxRetries;default:3;not null" json:"maxRetries"`
	JobID         string              `gorm:"column:jobId;not null" json:"jobId"`
	Name          string              `gorm:"column:name;not null" json:"name"`
	Version       string              `gorm:"column:version;not null" json:"version"`
	SubmittedAt   DateTime            `gorm:"column:submittedAt;not null" json:"submittedAt"`
	UpdatedAt     DateTime            `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	CompletedAt   *DateTime           `gorm:"column:completedAt" json:"completedAt"`
	LastRetriedAt *DateTime           `gorm:"column:lastRetriedAt" json:"lastRetriedAt"`
	Tasks         []BackgroundJobTask `gorm:"foreignKey:JobID;constraint:OnDelete:CASCADE"`
}

func (BackgroundJob) TableName() string {
	return "BackgroundJob"
}

func (m *BackgroundJob) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.SubmittedAt.IsZero() {
		m.SubmittedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*BackgroundJob) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type BackgroundJobTask struct {
	ID            string         `gorm:"column:id;primaryKey" json:"id"`
	Name          string         `gorm:"column:name;not null" json:"name"`
	Status        string         `gorm:"column:status;default:'PENDING';not null" json:"status"`
	Result        datatypes.JSON `gorm:"column:result" json:"result"`
	Retried       int            `gorm:"column:retried;default:0;not null" json:"retried"`
	MaxRetries    *int           `gorm:"column:maxRetries;default:3;not null" json:"maxRetries"`
	CreatedAt     DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt     DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	CompletedAt   *DateTime      `gorm:"column:completedAt" json:"completedAt"`
	JobID         string         `gorm:"column:jobId;not null" json:"jobId"`
	BackgroundJob *BackgroundJob `gorm:"foreignKey:JobID"`
}

func (BackgroundJobTask) TableName() string {
	return "BackgroundJobTask"
}

func (m *BackgroundJobTask) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*BackgroundJobTask) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type AvatarImage struct {
	ID           string         `gorm:"column:id;primaryKey" json:"id"`
	Bytes        string         `gorm:"column:bytes;not null" json:"bytes"`
	Team         []Team         `gorm:"foreignKey:AvatarImageID;constraint:OnDelete:SET NULL"`
	User         []User         `gorm:"foreignKey:AvatarImageID;constraint:OnDelete:SET NULL"`
	Organisation []Organisation `gorm:"foreignKey:AvatarImageID;constraint:OnDelete:SET NULL"`
}

func (AvatarImage) TableName() string {
	return "AvatarImage"
}

func (m *AvatarImage) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = cuid.New()
	}
	return nil
}

type EmailDomain struct {
	ID             string    `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt      DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt      DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Status         string    `gorm:"column:status;default:'PENDING';not null" json:"status"`
	Selector       string    `gorm:"column:selector;uniqueIndex;not null" json:"selector"`
	Domain         string    `gorm:"column:domain;uniqueIndex;not null" json:"domain"`
	PublicKey      string    `gorm:"column:publicKey;not null" json:"publicKey"`
	PrivateKey     string    `gorm:"column:privateKey;not null" json:"privateKey"`
	LastVerifiedAt *DateTime `gorm:"column:lastVerifiedAt" json:"lastVerifiedAt"`
	OrganisationID string    `gorm:"column:organisationId;not null" json:"organisationId"`
	Organisation   *Organisation
	Emails         []OrganisationEmail `gorm:"foreignKey:EmailDomainID;constraint:OnDelete:CASCADE"`
}

func (EmailDomain) TableName() string {
	return "EmailDomain"
}

func (m *EmailDomain) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*EmailDomain) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type OrganisationEmail struct {
	ID                         string   `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt                  DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt                  DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Email                      string   `gorm:"column:email;uniqueIndex;not null" json:"email"`
	EmailName                  string   `gorm:"column:emailName;not null" json:"emailName"`
	EmailDomainID              string   `gorm:"column:emailDomainId;not null" json:"emailDomainId"`
	OrganisationID             string   `gorm:"column:organisationId;not null" json:"organisationId"`
	EmailDomain                *EmailDomain
	Organisation               *Organisation
	OrganisationGlobalSettings []OrganisationGlobalSettings `gorm:"foreignKey:EmailID"`
	TeamGlobalSettings         []TeamGlobalSettings         `gorm:"foreignKey:EmailID"`
}

func (OrganisationEmail) TableName() string {
	return "OrganisationEmail"
}

func (m *OrganisationEmail) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*OrganisationEmail) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type EmailTransport struct {
	ID                 string              `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt          DateTime            `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt          DateTime            `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Name               string              `gorm:"column:name;not null" json:"name"`
	Type               string              `gorm:"column:type;not null" json:"type"`
	FromName           string              `gorm:"column:fromName;not null" json:"fromName"`
	FromAddress        string              `gorm:"column:fromAddress;not null" json:"fromAddress"`
	Config             string              `gorm:"column:config;not null" json:"config"`
	SubscriptionClaims []SubscriptionClaim `gorm:"foreignKey:EmailTransportID;constraint:OnDelete:SET NULL"`
	OrganisationClaims []OrganisationClaim `gorm:"foreignKey:EmailTransportID;constraint:OnDelete:SET NULL"`
}

func (EmailTransport) TableName() string {
	return "EmailTransport"
}

func (m *EmailTransport) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*EmailTransport) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type OrganisationAuthenticationPortal struct {
	ID                         string        `gorm:"column:id;primaryKey" json:"id"`
	Enabled                    bool          `gorm:"column:enabled;default:false;not null" json:"enabled"`
	ClientID                   string        `gorm:"column:clientId;default:'';not null" json:"clientId"`
	ClientSecret               string        `gorm:"column:clientSecret;default:'';not null" json:"clientSecret"`
	WellKnownURL               string        `gorm:"column:wellKnownUrl;default:'';not null" json:"wellKnownUrl"`
	DefaultOrganisationRole    string        `gorm:"column:defaultOrganisationRole;default:'MEMBER';not null" json:"defaultOrganisationRole"`
	AutoProvisionUsers         *bool         `gorm:"column:autoProvisionUsers;default:true;not null" json:"autoProvisionUsers"`
	AllowedDomains             []string      `gorm:"column:allowedDomains;serializer:json;not null" json:"allowedDomains"`
	AllowPersonalOrganisations bool          `gorm:"column:allowPersonalOrganisations;default:false;not null" json:"allowPersonalOrganisations"`
	Organisation               *Organisation `gorm:"foreignKey:OrganisationAuthenticationPortalID"`
}

func (OrganisationAuthenticationPortal) TableName() string {
	return "OrganisationAuthenticationPortal"
}

type Counter struct {
	ID    string `gorm:"column:id;primaryKey" json:"id"`
	Value int    `gorm:"column:value;not null" json:"value"`
}

func (Counter) TableName() string {
	return "Counter"
}

type EmbedSignature struct {
	Key            string   `gorm:"column:key;primaryKey" json:"key"`
	CreatedAt      DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt      DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	OrganisationID string   `gorm:"column:organisationId;index:EmbedSignature_organisationId_idx;not null" json:"organisationId"`
	RecipientID    int      `gorm:"column:recipientId;index:EmbedSignature_recipientId_idx;not null" json:"recipientId"`
	Signature      string   `gorm:"column:signature;not null" json:"signature"`
	Organisation   *Organisation
}

func (EmbedSignature) TableName() string {
	return "EmbedSignature"
}

func (m *EmbedSignature) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*EmbedSignature) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type RateLimit struct {
	Key       string   `gorm:"column:key;primaryKey" json:"key"`
	Action    string   `gorm:"column:action;primaryKey" json:"action"`
	Bucket    DateTime `gorm:"column:bucket;primaryKey" json:"bucket"`
	Count     *int     `gorm:"column:count;default:1;not null" json:"count"`
	CreatedAt DateTime `gorm:"column:createdAt;index:RateLimit_createdAt_idx;autoCreateTime:false;not null" json:"createdAt"`
}

func (RateLimit) TableName() string {
	return "RateLimit"
}

func (m *RateLimit) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
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
