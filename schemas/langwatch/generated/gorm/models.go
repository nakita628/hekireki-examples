package model

import (
	"database/sql/driver"
	"fmt"
	"strconv"
	"time"

	"github.com/lucsky/cuid"
	gonanoid "github.com/matoous/go-nanoid/v2"
	"gorm.io/datatypes"
	"gorm.io/gorm"
)

type Account struct {
	ID                string    `gorm:"column:id;primaryKey" json:"id"`
	UserID            string    `gorm:"column:userId;index:Account_userId_idx;not null" json:"userId"`
	Type              *string   `gorm:"column:type;default:'oauth';not null" json:"type"`
	Provider          string    `gorm:"column:provider;uniqueIndex:Account_provider_providerAccountId_key;not null" json:"provider"`
	Issuer            *string   `gorm:"column:issuer;index:Account_issuer_providerAccountId_idx" json:"issuer"`
	ProviderAccountID string    `gorm:"column:providerAccountId;uniqueIndex:Account_provider_providerAccountId_key;index:Account_issuer_providerAccountId_idx;not null" json:"providerAccountId"`
	RefreshToken      *string   `gorm:"column:refresh_token;type:text" json:"refresh_token"`
	AccessToken       *string   `gorm:"column:access_token;type:text" json:"access_token"`
	ExpiresAt         *DateTime `gorm:"column:expires_at" json:"expires_at"`
	ExtExpiresIn      *int      `gorm:"column:ext_expires_in" json:"ext_expires_in"`
	TokenType         *string   `gorm:"column:token_type" json:"token_type"`
	Scope             *string   `gorm:"column:scope" json:"scope"`
	IDToken           *string   `gorm:"column:id_token;type:text" json:"id_token"`
	SessionState      *string   `gorm:"column:session_state" json:"session_state"`
	Password          *string   `gorm:"column:password;type:text" json:"password"`
	CreatedAt         DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt         DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	User              *User
}

func (Account) TableName() string {
	return "Account"
}

func (m *Account) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
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

type AccountCredential struct {
	ID                    string    `gorm:"column:id;primaryKey" json:"id"`
	UserID                string    `gorm:"column:userId;index:AccountCredential_userId_idx;not null" json:"userId"`
	Provider              string    `gorm:"column:provider;not null" json:"provider"`
	Password              *string   `gorm:"column:password;type:text" json:"password"`
	AccessToken           *string   `gorm:"column:accessToken;type:text" json:"accessToken"`
	RefreshToken          *string   `gorm:"column:refreshToken;type:text" json:"refreshToken"`
	IDToken               *string   `gorm:"column:idToken;type:text" json:"idToken"`
	AccessTokenExpiresAt  *DateTime `gorm:"column:accessTokenExpiresAt" json:"accessTokenExpiresAt"`
	RefreshTokenExpiresAt *DateTime `gorm:"column:refreshTokenExpiresAt" json:"refreshTokenExpiresAt"`
	Scope                 *string   `gorm:"column:scope" json:"scope"`
	CreatedAt             DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt             DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	User                  *User
}

func (AccountCredential) TableName() string {
	return "AccountCredential"
}

func (m *AccountCredential) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*AccountCredential) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Session struct {
	ID                     string         `gorm:"column:id;primaryKey" json:"id"`
	SessionToken           string         `gorm:"column:sessionToken;uniqueIndex;not null" json:"sessionToken"`
	UserID                 string         `gorm:"column:userId;index:Session_userId_idx;not null" json:"userId"`
	Expires                DateTime       `gorm:"column:expires;not null" json:"expires"`
	IPAddress              *string        `gorm:"column:ipAddress" json:"ipAddress"`
	UserAgent              *string        `gorm:"column:userAgent;type:text" json:"userAgent"`
	IdentifierID           *string        `gorm:"column:identifierId;index:Session_identifierId_idx" json:"identifierId"`
	Amr                    []string       `gorm:"column:amr;serializer:json;not null" json:"amr"`
	StartedBySignUp        bool           `gorm:"column:startedBySignUp;default:false;not null" json:"startedBySignUp"`
	Impersonating          datatypes.JSON `gorm:"column:impersonating" json:"impersonating"`
	ActorUserID            *string        `gorm:"column:actorUserId" json:"actorUserId"`
	SubjectUserID          *string        `gorm:"column:subjectUserId" json:"subjectUserId"`
	ImpersonationReason    *string        `gorm:"column:impersonationReason;type:text" json:"impersonationReason"`
	ImpersonationExpiresAt *DateTime      `gorm:"column:impersonationExpiresAt" json:"impersonationExpiresAt"`
	LastSeenAt             *DateTime      `gorm:"column:lastSeenAt" json:"lastSeenAt"`
	CreatedAt              DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt              DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	User                   *User
}

func (Session) TableName() string {
	return "Session"
}

func (m *Session) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
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

type UserErasure struct {
	ID               string   `gorm:"column:id;primaryKey" json:"id"`
	AccountCreatedAt DateTime `gorm:"column:accountCreatedAt;index:UserErasure_accountCreatedAt_idx;not null" json:"accountCreatedAt"`
	ErasedAt         DateTime `gorm:"column:erasedAt;not null" json:"erasedAt"`
}

func (UserErasure) TableName() string {
	return "UserErasure"
}

func (m *UserErasure) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.ErasedAt.IsZero() {
		m.ErasedAt = DateTime{Time: now}
	}
	return nil
}

type User struct {
	ID                            string                   `gorm:"column:id;primaryKey" json:"id"`
	Name                          *string                  `gorm:"column:name" json:"name"`
	Email                         *string                  `gorm:"column:email;uniqueIndex" json:"email"`
	EmailVerified                 bool                     `gorm:"column:emailVerified;default:false;not null" json:"emailVerified"`
	SignupConfirmationPending     bool                     `gorm:"column:signupConfirmationPending;default:false;not null" json:"signupConfirmationPending"`
	PasskeySignupClaimHash        *string                  `gorm:"column:passkeySignupClaimHash" json:"passkeySignupClaimHash"`
	Image                         *string                  `gorm:"column:image;type:text" json:"image"`
	PendingSsoSetup               bool                     `gorm:"column:pendingSsoSetup;default:false;not null" json:"pendingSsoSetup"`
	UserHashKey                   *string                  `gorm:"column:userHashKey" json:"userHashKey"`
	TwoFactorEnabled              bool                     `gorm:"column:twoFactorEnabled;default:false;not null" json:"twoFactorEnabled"`
	CreatedAt                     DateTime                 `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt                     DateTime                 `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	LastLoginAt                   *DateTime                `gorm:"column:lastLoginAt" json:"lastLoginAt"`
	DeactivatedAt                 *DateTime                `gorm:"column:deactivatedAt" json:"deactivatedAt"`
	LastHomePath                  *string                  `gorm:"column:lastHomePath" json:"lastHomePath"`
	TracesExplorerTourDismissedAt *DateTime                `gorm:"column:tracesExplorerTourDismissedAt" json:"tracesExplorerTourDismissedAt"`
	LangyCodeAccessPreference     *string                  `gorm:"column:langyCodeAccessPreference" json:"langyCodeAccessPreference"`
	PasskeyNudgeDismissedAt       *DateTime                `gorm:"column:passkeyNudgeDismissedAt" json:"passkeyNudgeDismissedAt"`
	JoinOfferDismissedDomains     []string                 `gorm:"column:joinOfferDismissedDomains;serializer:json;not null" json:"joinOfferDismissedDomains"`
	NotificationPreferences       datatypes.JSON           `gorm:"column:notificationPreferences;default:'{}';not null" json:"notificationPreferences"`
	PasswordResetRequiredAt       *DateTime                `gorm:"column:passwordResetRequiredAt" json:"passwordResetRequiredAt"`
	Accounts                      []Account                `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	AccountCredentials            []AccountCredential      `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	Sessions                      []Session                `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	TwoFactors                    []TwoFactor              `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	Passkeys                      []Passkey                `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	TeamMemberships               []TeamUser               `gorm:"foreignKey:UserID"`
	OrgMemberships                []OrganizationUser       `gorm:"foreignKey:UserID"`
	ScimUserResources             []ScimUserResource       `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	Annotation                    []Annotation             `gorm:"foreignKey:UserID"`
	ShareLinks                    []ShareLink              `gorm:"foreignKey:UserID"`
	Workflow                      []Workflow               `gorm:"foreignKey:PublishedByID"`
	WorkflowVersion               []WorkflowVersion        `gorm:"foreignKey:AuthorID"`
	AnnotationQueues              []AnnotationQueueMembers `gorm:"foreignKey:UserID"`
	AssignedQueueItems            []AnnotationQueueItem    `gorm:"foreignKey:UserID"`
	CreatedQueueItems             []AnnotationQueueItem    `gorm:"foreignKey:CreatedByUserID"`
	LlmPromptConfigVersions       []LlmPromptConfigVersion `gorm:"foreignKey:AuthorID"`
	Scenarios                     []Scenario               `gorm:"foreignKey:LastUpdatedByID"`
	InviteRequests                []OrganizationInvite     `gorm:"foreignKey:RequestedBy"`
	SecretsCreated                []ProjectSecret          `gorm:"foreignKey:CreatedByID"`
	SecretsUpdated                []ProjectSecret          `gorm:"foreignKey:UpdatedByID"`
	SavedViews                    []SavedView              `gorm:"foreignKey:UserID"`
	VersionTagsCreated            []PromptTagAssignment    `gorm:"foreignKey:CreatedByID"`
	VersionTagsUpdated            []PromptTagAssignment    `gorm:"foreignKey:UpdatedByID"`
	PromptTagsCreated             []PromptTag              `gorm:"foreignKey:CreatedByID"`
	PromptTagsUpdated             []PromptTag              `gorm:"foreignKey:UpdatedByID"`
	GroupMemberships              []GroupMembership        `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	RoleBindings                  []RoleBinding            `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	ScimExternalIds               []ScimExternalID         `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	APIKeys                       []APIKey                 `gorm:"foreignKey:UserID;constraint:OnDelete:RESTRICT"`
	GatewayBudgetsCreated         []GatewayBudget          `gorm:"foreignKey:CreatedByID"`
	PrincipalVirtualKeys          []VirtualKey             `gorm:"foreignKey:PrincipalUserID;constraint:OnDelete:SET NULL"`
	PersonalTeams                 []Team                   `gorm:"foreignKey:OwnerUserID"`
	PersonalProjects              []Project                `gorm:"foreignKey:OwnerUserID"`
	RoutingPoliciesCreated        []RoutingPolicy          `gorm:"foreignKey:CreatedByID;constraint:OnDelete:SET NULL"`
	RoutingPoliciesUpdated        []RoutingPolicy          `gorm:"foreignKey:UpdatedByID;constraint:OnDelete:SET NULL"`
	GatewayGuardrailsCreated      []GatewayGuardrail       `gorm:"foreignKey:CreatedByID;constraint:OnDelete:SET NULL"`
	GatewayGuardrailsUpdated      []GatewayGuardrail       `gorm:"foreignKey:UpdatedByID;constraint:OnDelete:SET NULL"`
	IngestionSourcesCreated       []IngestionSource        `gorm:"foreignKey:CreatedByID;constraint:OnDelete:SET NULL"`
	AnomalyRulesCreated           []AnomalyRule            `gorm:"foreignKey:CreatedByID;constraint:OnDelete:SET NULL"`
	PinnedTraces                  []PinnedTrace            `gorm:"foreignKey:UserID"`
	TraceEditOverlaysCreated      []TraceEditOverlay       `gorm:"foreignKey:CreatedByID"`
	TraceEditOverlaysUpdated      []TraceEditOverlay       `gorm:"foreignKey:UpdatedByID"`
}

func (User) TableName() string {
	return "User"
}

func (m *User) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
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

type VerificationToken struct {
	ID         string   `gorm:"column:id;primaryKey" json:"id"`
	Identifier string   `gorm:"column:identifier;uniqueIndex:VerificationToken_identifier_token_key;not null" json:"identifier"`
	Token      string   `gorm:"column:token;uniqueIndex:VerificationToken_identifier_token_key;not null" json:"token"`
	Expires    DateTime `gorm:"column:expires;not null" json:"expires"`
	CreatedAt  DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt  DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
}

func (VerificationToken) TableName() string {
	return "VerificationToken"
}

func (m *VerificationToken) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
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

type Identifier struct {
	ID                string    `gorm:"column:id;primaryKey" json:"id"`
	UserID            string    `gorm:"column:userId;index:Identifier_userId_idx;not null" json:"userId"`
	Provider          string    `gorm:"column:provider;index:Identifier_provider_providerAccountId_idx;not null" json:"provider"`
	Value             *string   `gorm:"column:value;index:Identifier_value_idx" json:"value"`
	Domain            *string   `gorm:"column:domain;index:Identifier_domain_idx" json:"domain"`
	IdentifierHash    *string   `gorm:"column:identifierHash;index:Identifier_identifierHash_idx" json:"identifierHash"`
	AccountID         *string   `gorm:"column:accountId;index:Identifier_accountId_idx" json:"accountId"`
	ProviderID        *string   `gorm:"column:providerId" json:"providerId"`
	Issuer            *string   `gorm:"column:issuer;index:Identifier_issuer_providerAccountId_idx" json:"issuer"`
	ProviderAccountID *string   `gorm:"column:providerAccountId;index:Identifier_provider_providerAccountId_idx;index:Identifier_issuer_providerAccountId_idx" json:"providerAccountId"`
	State             string    `gorm:"column:state;not null" json:"state"`
	ConnectionID      *string   `gorm:"column:connectionId" json:"connectionId"`
	VerifiedAt        *DateTime `gorm:"column:verifiedAt" json:"verifiedAt"`
	AttachedAt        DateTime  `gorm:"column:attachedAt;not null" json:"attachedAt"`
	DetachedAt        *DateTime `gorm:"column:detachedAt" json:"detachedAt"`
	LastUsedAt        *DateTime `gorm:"column:lastUsedAt" json:"lastUsedAt"`
	CreatedAt         DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt         DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
}

func (Identifier) TableName() string {
	return "Identifier"
}

func (m *Identifier) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*Identifier) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type IdentitySignInSwitch struct {
	UserID             string   `gorm:"column:userId;primaryKey" json:"userId"`
	OnIdentifiers      bool     `gorm:"column:onIdentifiers;not null" json:"onIdentifiers"`
	Pinned             bool     `gorm:"column:pinned;default:false;not null" json:"pinned"`
	SwitchedAt         DateTime `gorm:"column:switchedAt;not null" json:"switchedAt"`
	LastAppliedEventID *string  `gorm:"column:lastAppliedEventId" json:"lastAppliedEventId"`
	UpdatedAt          DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
}

func (IdentitySignInSwitch) TableName() string {
	return "IdentitySignInSwitch"
}

func (m *IdentitySignInSwitch) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*IdentitySignInSwitch) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type IdentifierReservation struct {
	NormalizedValue string   `gorm:"column:normalizedValue;primaryKey" json:"normalizedValue"`
	UserID          string   `gorm:"column:userId;index:IdentifierReservation_userId_idx;not null" json:"userId"`
	IdentifierID    string   `gorm:"column:identifierId;index:IdentifierReservation_identifierId_idx;not null" json:"identifierId"`
	CommandID       string   `gorm:"column:commandId;not null" json:"commandId"`
	CreatedAt       DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
}

func (IdentifierReservation) TableName() string {
	return "IdentifierReservation"
}

func (m *IdentifierReservation) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type IdentityProjectionCursor struct {
	UserID            string   `gorm:"column:userId;primaryKey" json:"userId"`
	LastEventID       string   `gorm:"column:lastEventId;not null" json:"lastEventId"`
	AcceptedAt        DateTime `gorm:"column:acceptedAt;not null" json:"acceptedAt"`
	OccurredAt        DateTime `gorm:"column:occurredAt;not null" json:"occurredAt"`
	ProjectionVersion string   `gorm:"column:projectionVersion;not null" json:"projectionVersion"`
	CreatedAt         DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt         DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
}

func (IdentityProjectionCursor) TableName() string {
	return "IdentityProjectionCursor"
}

func (m *IdentityProjectionCursor) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*IdentityProjectionCursor) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type TwoFactor struct {
	ID                      string    `gorm:"column:id;primaryKey" json:"id"`
	UserID                  string    `gorm:"column:userId;index:TwoFactor_userId_idx;not null" json:"userId"`
	Secret                  string    `gorm:"column:secret;index:TwoFactor_secret_idx;type:text;not null" json:"secret"`
	BackupCodes             string    `gorm:"column:backupCodes;type:text;not null" json:"backupCodes"`
	Verified                *bool     `gorm:"column:verified;default:true;not null" json:"verified"`
	FailedVerificationCount int       `gorm:"column:failedVerificationCount;default:0;not null" json:"failedVerificationCount"`
	LockedUntil             *DateTime `gorm:"column:lockedUntil" json:"lockedUntil"`
	CreatedAt               DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt               DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	User                    *User
}

func (TwoFactor) TableName() string {
	return "TwoFactor"
}

func (m *TwoFactor) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*TwoFactor) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type SignInAttemptLock struct {
	ID                  string    `gorm:"column:id;primaryKey" json:"id"`
	IdentifierHash      string    `gorm:"column:identifierHash;uniqueIndex;not null" json:"identifierHash"`
	FailedCount         int       `gorm:"column:failedCount;default:0;not null" json:"failedCount"`
	LockedUntil         *DateTime `gorm:"column:lockedUntil;index:SignInAttemptLock_lockedUntil_idx" json:"lockedUntil"`
	ConsecutiveLockouts int       `gorm:"column:consecutiveLockouts;default:0;not null" json:"consecutiveLockouts"`
	HeldForReview       bool      `gorm:"column:heldForReview;default:false;not null" json:"heldForReview"`
	UserID              *string   `gorm:"column:userId;index:SignInAttemptLock_userId_idx" json:"userId"`
	CreatedAt           DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt           DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
}

func (SignInAttemptLock) TableName() string {
	return "SignInAttemptLock"
}

func (m *SignInAttemptLock) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*SignInAttemptLock) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Passkey struct {
	ID           string   `gorm:"column:id;primaryKey" json:"id"`
	Name         *string  `gorm:"column:name" json:"name"`
	PublicKey    string   `gorm:"column:publicKey;type:text;not null" json:"publicKey"`
	UserID       string   `gorm:"column:userId;index:Passkey_userId_idx;not null" json:"userId"`
	CredentialID string   `gorm:"column:credentialID;index:Passkey_credentialID_idx;not null" json:"credentialID"`
	Counter      int      `gorm:"column:counter;default:0;not null" json:"counter"`
	DeviceType   string   `gorm:"column:deviceType;not null" json:"deviceType"`
	BackedUp     bool     `gorm:"column:backedUp;default:false;not null" json:"backedUp"`
	Transports   *string  `gorm:"column:transports" json:"transports"`
	Aaguid       *string  `gorm:"column:aaguid" json:"aaguid"`
	CreatedAt    DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt    DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	User         *User
}

func (Passkey) TableName() string {
	return "Passkey"
}

func (m *Passkey) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*Passkey) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type MfaEnrollment struct {
	UserID                    string    `gorm:"column:userId;primaryKey" json:"userId"`
	EnrollmentID              *string   `gorm:"column:enrollmentId" json:"enrollmentId"`
	Method                    *string   `gorm:"column:method" json:"method"`
	State                     string    `gorm:"column:state;index:MfaEnrollment_state_idx;not null" json:"state"`
	EnrolledAt                *DateTime `gorm:"column:enrolledAt" json:"enrolledAt"`
	ConfirmedAt               *DateTime `gorm:"column:confirmedAt" json:"confirmedAt"`
	ExpiredAt                 *DateTime `gorm:"column:expiredAt" json:"expiredAt"`
	DisabledAt                *DateTime `gorm:"column:disabledAt" json:"disabledAt"`
	DisabledVia               *string   `gorm:"column:disabledVia" json:"disabledVia"`
	BackupCodeCount           int       `gorm:"column:backupCodeCount;default:0;not null" json:"backupCodeCount"`
	ConsumedBackupCodeIndexes []int     `gorm:"column:consumedBackupCodeIndexes;serializer:json;not null" json:"consumedBackupCodeIndexes"`
	FailedCount               int       `gorm:"column:failedCount;default:0;not null" json:"failedCount"`
	OccurredAt                DateTime  `gorm:"column:occurredAt;not null" json:"occurredAt"`
	LastEventID               string    `gorm:"column:lastEventId;not null" json:"lastEventId"`
	AcceptedAt                DateTime  `gorm:"column:acceptedAt;not null" json:"acceptedAt"`
	ProjectionVersion         string    `gorm:"column:projectionVersion;not null" json:"projectionVersion"`
	CreatedAt                 DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt                 DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
}

func (MfaEnrollment) TableName() string {
	return "MfaEnrollment"
}

func (m *MfaEnrollment) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*MfaEnrollment) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type SsoConnection struct {
	ID                      string                      `gorm:"column:id;primaryKey" json:"id"`
	OrganizationID          string                      `gorm:"column:organizationId;index:SsoConnection_organizationId_idx;index:SsoConnection_organizationId_migrationPhase_idx;not null" json:"organizationId"`
	Type                    string                      `gorm:"column:type;not null" json:"type"`
	State                   string                      `gorm:"column:state;index:SsoConnection_state_idx;not null" json:"state"`
	ClaimedDomains          []string                    `gorm:"column:claimedDomains;serializer:json;not null" json:"claimedDomains"`
	DomainClaims            datatypes.JSON              `gorm:"column:domainClaims;default:'[]';not null" json:"domainClaims"`
	ApprovedDomains         []string                    `gorm:"column:approvedDomains;serializer:json;not null" json:"approvedDomains"`
	VerifiedDomains         []string                    `gorm:"column:verifiedDomains;index:SsoConnection_verifiedDomains_idx;serializer:json;not null" json:"verifiedDomains"`
	LapsedDomains           []string                    `gorm:"column:lapsedDomains;index:SsoConnection_lapsedDomains_idx;serializer:json;not null" json:"lapsedDomains"`
	DomainVerifications     datatypes.JSON              `gorm:"column:domainVerifications;default:'[]';not null" json:"domainVerifications"`
	PendingVerification     datatypes.JSON              `gorm:"column:pendingVerification" json:"pendingVerification"`
	IdpMetadata             datatypes.JSON              `gorm:"column:idpMetadata;not null" json:"idpMetadata"`
	ArrivalPolicy           *string                     `gorm:"column:arrivalPolicy;default:'refuse';not null" json:"arrivalPolicy"`
	AllowsJit               bool                        `gorm:"column:allowsJit;default:false;not null" json:"allowsJit"`
	ArrivalPolicyDecidedAt  *DateTime                   `gorm:"column:arrivalPolicyDecidedAt" json:"arrivalPolicyDecidedAt"`
	Source                  string                      `gorm:"column:source;not null" json:"source"`
	TestLoginAccountID      *string                     `gorm:"column:testLoginAccountId" json:"testLoginAccountId"`
	ReplacesConnectionID    *string                     `gorm:"column:replacesConnectionId" json:"replacesConnectionId"`
	MigrationPhase          *string                     `gorm:"column:migrationPhase;index:SsoConnection_organizationId_migrationPhase_idx" json:"migrationPhase"`
	GraceStartedAt          *DateTime                   `gorm:"column:graceStartedAt" json:"graceStartedAt"`
	RouteChangedAt          *DateTime                   `gorm:"column:routeChangedAt" json:"routeChangedAt"`
	FinalizationRequestedAt *DateTime                   `gorm:"column:finalizationRequestedAt" json:"finalizationRequestedAt"`
	FinalizedAt             *DateTime                   `gorm:"column:finalizedAt" json:"finalizedAt"`
	Rejection               datatypes.JSON              `gorm:"column:rejection" json:"rejection"`
	CreatedBy               *string                     `gorm:"column:createdBy" json:"createdBy"`
	TearDownAfter           *DateTime                   `gorm:"column:tearDownAfter" json:"tearDownAfter"`
	OccurredAt              DateTime                    `gorm:"column:occurredAt;not null" json:"occurredAt"`
	LastEventID             string                      `gorm:"column:lastEventId;not null" json:"lastEventId"`
	AcceptedAt              DateTime                    `gorm:"column:acceptedAt;not null" json:"acceptedAt"`
	ProjectionVersion       string                      `gorm:"column:projectionVersion;not null" json:"projectionVersion"`
	CreatedAt               DateTime                    `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt               DateTime                    `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	ReproofCursor           *SsoConnectionReproofCursor `gorm:"foreignKey:ConnectionID;constraint:OnDelete:CASCADE"`
}

func (SsoConnection) TableName() string {
	return "SsoConnection"
}

type SsoConnectionRegistrationSlot struct {
	OrganizationID       string   `gorm:"column:organizationId;primaryKey" json:"organizationId"`
	Kind                 string   `gorm:"column:kind;primaryKey" json:"kind"`
	ConnectionID         string   `gorm:"column:connectionId;uniqueIndex;not null" json:"connectionId"`
	ReplacesConnectionID *string  `gorm:"column:replacesConnectionId" json:"replacesConnectionId"`
	CommandID            string   `gorm:"column:commandId;not null" json:"commandId"`
	CreatedAt            DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt            DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
}

func (SsoConnectionRegistrationSlot) TableName() string {
	return "SsoConnectionRegistrationSlot"
}

func (m *SsoConnectionRegistrationSlot) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*SsoConnectionRegistrationSlot) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type SsoConnectionReproofCursor struct {
	ConnectionID  string         `gorm:"column:connectionId;primaryKey" json:"connectionId"`
	LastReproofAt DateTime       `gorm:"column:lastReproofAt;index:SsoConnectionReproofCursor_lastReproofAt_idx;not null" json:"lastReproofAt"`
	Connection    *SsoConnection `gorm:"foreignKey:ConnectionID"`
}

func (SsoConnectionReproofCursor) TableName() string {
	return "SsoConnectionReproofCursor"
}

type SsoCredential struct {
	ID             string   `gorm:"column:id;primaryKey" json:"id"`
	OrganizationID string   `gorm:"column:organizationId;index:SsoCredential_organizationId_connectionId_idx;not null" json:"organizationId"`
	ConnectionID   string   `gorm:"column:connectionId;index:SsoCredential_organizationId_connectionId_idx;not null" json:"connectionId"`
	Kind           string   `gorm:"column:kind;not null" json:"kind"`
	Ciphertext     string   `gorm:"column:ciphertext;not null" json:"ciphertext"`
	CreatedAt      DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt      DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
}

func (SsoCredential) TableName() string {
	return "SsoCredential"
}

func (m *SsoCredential) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*SsoCredential) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type SsoVerifiedDomain struct {
	Domain         string                    `gorm:"column:domain;primaryKey;uniqueIndex:SsoVerifiedDomain_domain_organizationId_key" json:"domain"`
	OrganizationID string                    `gorm:"column:organizationId;uniqueIndex:SsoVerifiedDomain_domain_organizationId_key;index:SsoVerifiedDomain_organizationId_idx;not null" json:"organizationId"`
	Holders        []SsoVerifiedDomainHolder `gorm:"foreignKey:Domain,OrganizationID;references:Domain,OrganizationID;constraint:OnDelete:CASCADE"`
}

func (SsoVerifiedDomain) TableName() string {
	return "SsoVerifiedDomain"
}

type SsoVerifiedDomainHolder struct {
	Domain         string             `gorm:"column:domain;primaryKey" json:"domain"`
	ConnectionID   string             `gorm:"column:connectionId;primaryKey;index:SsoVerifiedDomainHolder_connectionId_idx" json:"connectionId"`
	OrganizationID string             `gorm:"column:organizationId;index:SsoVerifiedDomainHolder_organizationId_idx;not null" json:"organizationId"`
	Ownership      *SsoVerifiedDomain `gorm:"foreignKey:Domain,OrganizationID;references:Domain,OrganizationID"`
}

func (SsoVerifiedDomainHolder) TableName() string {
	return "SsoVerifiedDomainHolder"
}

type SsoAuthenticationActivity struct {
	ID                string   `gorm:"column:id;primaryKey" json:"id"`
	OrganizationID    string   `gorm:"column:organizationId;index:SsoAuthenticationActivity_organizationId_connectionId_authenticatedAt_idx;index:SsoAuthenticationActivity_organizationId_userId_idx;not null" json:"organizationId"`
	ConnectionID      string   `gorm:"column:connectionId;index:SsoAuthenticationActivity_organizationId_connectionId_authenticatedAt_idx;not null" json:"connectionId"`
	UserID            string   `gorm:"column:userId;index:SsoAuthenticationActivity_organizationId_userId_idx;not null" json:"userId"`
	AuthenticatedAt   DateTime `gorm:"column:authenticatedAt;index:SsoAuthenticationActivity_organizationId_connectionId_authenticatedAt_idx;not null" json:"authenticatedAt"`
	ProviderAccountID *string  `gorm:"column:providerAccountId" json:"providerAccountId"`
}

func (SsoAuthenticationActivity) TableName() string {
	return "SsoAuthenticationActivity"
}

type SsoProvider struct {
	ID             string  `gorm:"column:id;primaryKey" json:"id"`
	Issuer         string  `gorm:"column:issuer;not null" json:"issuer"`
	OidcConfig     *string `gorm:"column:oidcConfig" json:"oidcConfig"`
	SamlConfig     *string `gorm:"column:samlConfig" json:"samlConfig"`
	UserID         *string `gorm:"column:userId" json:"userId"`
	ProviderID     string  `gorm:"column:providerId;uniqueIndex;not null" json:"providerId"`
	OrganizationID *string `gorm:"column:organizationId;index:SsoProvider_organizationId_idx" json:"organizationId"`
	Domain         string  `gorm:"column:domain;index:SsoProvider_domain_idx;not null" json:"domain"`
}

func (SsoProvider) TableName() string {
	return "SsoProvider"
}

type SsoBreakGlassBinding struct {
	ID              string    `gorm:"column:id;primaryKey" json:"id"`
	OrganizationID  string    `gorm:"column:organizationId;index:SsoBreakGlassBinding_organizationId_expiresAt_idx;not null" json:"organizationId"`
	UserID          string    `gorm:"column:userId;index:SsoBreakGlassBinding_userId_idx;not null" json:"userId"`
	GrantedByUserID string    `gorm:"column:grantedByUserId;not null" json:"grantedByUserId"`
	GrantedAt       DateTime  `gorm:"column:grantedAt;not null" json:"grantedAt"`
	ExpiresAt       DateTime  `gorm:"column:expiresAt;index:SsoBreakGlassBinding_organizationId_expiresAt_idx;not null" json:"expiresAt"`
	SupersededAt    *DateTime `gorm:"column:supersededAt" json:"supersededAt"`
	RenewedFromID   *string   `gorm:"column:renewedFromId" json:"renewedFromId"`
	WarnedDays      []int     `gorm:"column:warnedDays;serializer:json;not null" json:"warnedDays"`
	CreatedAt       DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
}

func (SsoBreakGlassBinding) TableName() string {
	return "SsoBreakGlassBinding"
}

func (m *SsoBreakGlassBinding) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type SsoActivationRecoveryReservation struct {
	CommandID      string   `gorm:"column:commandId;primaryKey" json:"commandId"`
	OrganizationID string   `gorm:"column:organizationId;uniqueIndex:SsoActivationRecoveryReservation_organizationId_connectionId_key;index:SsoActivationRecoveryReservation_organizationId_idx;not null" json:"organizationId"`
	ConnectionID   string   `gorm:"column:connectionId;uniqueIndex:SsoActivationRecoveryReservation_organizationId_connectionId_key;not null" json:"connectionId"`
	CreatedAt      DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
}

func (SsoActivationRecoveryReservation) TableName() string {
	return "SsoActivationRecoveryReservation"
}

type JoinRequest struct {
	ID                string    `gorm:"column:id;primaryKey" json:"id"`
	UserID            string    `gorm:"column:userId;index:JoinRequest_userId_state_idx;index:JoinRequest_userId_organizationId_idx;not null" json:"userId"`
	OrganizationID    string    `gorm:"column:organizationId;index:JoinRequest_organizationId_state_idx;index:JoinRequest_userId_organizationId_idx;not null" json:"organizationId"`
	Domain            string    `gorm:"column:domain;index:JoinRequest_domain_idx;not null" json:"domain"`
	State             string    `gorm:"column:state;index:JoinRequest_organizationId_state_idx;index:JoinRequest_userId_state_idx;not null" json:"state"`
	MatchedVia        string    `gorm:"column:matchedVia;not null" json:"matchedVia"`
	Origin            *string   `gorm:"column:origin;default:'web';not null" json:"origin"`
	ExpiresAt         *DateTime `gorm:"column:expiresAt" json:"expiresAt"`
	ResolvedAt        *DateTime `gorm:"column:resolvedAt" json:"resolvedAt"`
	ResolvedByType    *string   `gorm:"column:resolvedByType" json:"resolvedByType"`
	ResolvedByID      *string   `gorm:"column:resolvedById" json:"resolvedById"`
	WithdrawalCause   *string   `gorm:"column:withdrawalCause" json:"withdrawalCause"`
	ConnectionID      *string   `gorm:"column:connectionId" json:"connectionId"`
	OccurredAt        DateTime  `gorm:"column:occurredAt;not null" json:"occurredAt"`
	LastEventID       string    `gorm:"column:lastEventId;not null" json:"lastEventId"`
	AcceptedAt        DateTime  `gorm:"column:acceptedAt;not null" json:"acceptedAt"`
	ProjectionVersion string    `gorm:"column:projectionVersion;not null" json:"projectionVersion"`
	CreatedAt         DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt         DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
}

func (JoinRequest) TableName() string {
	return "JoinRequest"
}

type TeamUser struct {
	UserID         string      `gorm:"column:userId;primaryKey;index:TeamUser_userId_idx" json:"userId"`
	TeamID         string      `gorm:"column:teamId;primaryKey;index:TeamUser_teamId_idx" json:"teamId"`
	Role           string      `gorm:"column:role;not null" json:"role"`
	AssignedRoleID *string     `gorm:"column:assignedRoleId;index:TeamUser_assignedRoleId_idx" json:"assignedRoleId"`
	CreatedAt      DateTime    `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt      DateTime    `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	AssignedRole   *CustomRole `gorm:"foreignKey:AssignedRoleID"`
	User           *User
	Team           *Team
}

func (TeamUser) TableName() string {
	return "TeamUser"
}

func (m *TeamUser) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*TeamUser) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type OrganizationUser struct {
	UserID            string    `gorm:"column:userId;primaryKey;index:OrganizationUser_userId_idx" json:"userId"`
	OrganizationID    string    `gorm:"column:organizationId;primaryKey;index:OrganizationUser_organizationId_idx" json:"organizationId"`
	Role              string    `gorm:"column:role;not null" json:"role"`
	CreatedAt         DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt         DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	DepartmentID      *string   `gorm:"column:departmentId;index:OrganizationUser_departmentId_idx" json:"departmentId"`
	DisabledAt        *DateTime `gorm:"column:disabledAt" json:"disabledAt"`
	PendingSsoGrantID *string   `gorm:"column:pendingSsoGrantId" json:"pendingSsoGrantId"`
	MembershipStamp   string    `gorm:"column:membershipStamp;default:gen_random_uuid()::text;not null" json:"membershipStamp"`
	FullSeatSince     *DateTime `gorm:"column:fullSeatSince" json:"fullSeatSince"`
	SeatPendingSince  *DateTime `gorm:"column:seatPendingSince" json:"seatPendingSince"`
	User              *User
	Organization      *Organization
}

func (OrganizationUser) TableName() string {
	return "OrganizationUser"
}

func (m *OrganizationUser) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*OrganizationUser) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type SeatHold struct {
	ID             string   `gorm:"column:id;primaryKey" json:"id"`
	OrganizationID string   `gorm:"column:organizationId;uniqueIndex:SeatHold_organizationId_userId_key;not null" json:"organizationId"`
	UserID         string   `gorm:"column:userId;uniqueIndex:SeatHold_organizationId_userId_key;not null" json:"userId"`
	Email          *string  `gorm:"column:email" json:"email"`
	Name           *string  `gorm:"column:name" json:"name"`
	SeatTakenAt    DateTime `gorm:"column:seatTakenAt;not null" json:"seatTakenAt"`
	HeldUntil      DateTime `gorm:"column:heldUntil;index:SeatHold_heldUntil_idx;not null" json:"heldUntil"`
	Reason         string   `gorm:"column:reason;not null" json:"reason"`
	CreatedAt      DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
}

func (SeatHold) TableName() string {
	return "SeatHold"
}

func (m *SeatHold) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type SeatRequest struct {
	ID               string    `gorm:"column:id;primaryKey" json:"id"`
	OrganizationID   string    `gorm:"column:organizationId;uniqueIndex:SeatRequest_organizationId_email_key;index:SeatRequest_organizationId_status_idx;not null" json:"organizationId"`
	Email            string    `gorm:"column:email;uniqueIndex:SeatRequest_organizationId_email_key;not null" json:"email"`
	UserID           *string   `gorm:"column:userId;index:SeatRequest_userId_idx" json:"userId"`
	Name             *string   `gorm:"column:name" json:"name"`
	Status           string    `gorm:"column:status;index:SeatRequest_organizationId_status_idx;not null" json:"status"`
	Source           string    `gorm:"column:source;not null" json:"source"`
	IdpRefused       bool      `gorm:"column:idpRefused;default:false;not null" json:"idpRefused"`
	IdpKind          *string   `gorm:"column:idpKind" json:"idpKind"`
	SalesNotifiedAt  *DateTime `gorm:"column:salesNotifiedAt" json:"salesNotifiedAt"`
	OpenedAt         DateTime  `gorm:"column:openedAt;not null" json:"openedAt"`
	ResolvedAt       *DateTime `gorm:"column:resolvedAt" json:"resolvedAt"`
	ResolvedByUserID *string   `gorm:"column:resolvedByUserId" json:"resolvedByUserId"`
	SeatPendingSince *DateTime `gorm:"column:seatPendingSince" json:"seatPendingSince"`
	CreatedAt        DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt        DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
}

func (SeatRequest) TableName() string {
	return "SeatRequest"
}

func (m *SeatRequest) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*SeatRequest) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type OrganizationSeatLease struct {
	OrganizationID string   `gorm:"column:organizationId;primaryKey" json:"organizationId"`
	Holder         string   `gorm:"column:holder;not null" json:"holder"`
	ExpiresAt      DateTime `gorm:"column:expiresAt;not null" json:"expiresAt"`
	CreatedAt      DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
}

func (OrganizationSeatLease) TableName() string {
	return "OrganizationSeatLease"
}

func (m *OrganizationSeatLease) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type OrganizationAccessNotice struct {
	ID             string    `gorm:"column:id;primaryKey" json:"id"`
	OrganizationID string    `gorm:"column:organizationId;uniqueIndex:OrganizationAccessNotice_organizationId_kind_email_dayKey_key;not null" json:"organizationId"`
	Kind           string    `gorm:"column:kind;uniqueIndex:OrganizationAccessNotice_organizationId_kind_email_dayKey_key;not null" json:"kind"`
	Email          string    `gorm:"column:email;uniqueIndex:OrganizationAccessNotice_organizationId_kind_email_dayKey_key;not null" json:"email"`
	Name           *string   `gorm:"column:name" json:"name"`
	Idp            *string   `gorm:"column:idp" json:"idp"`
	DayKey         string    `gorm:"column:dayKey;uniqueIndex:OrganizationAccessNotice_organizationId_kind_email_dayKey_key;not null" json:"dayKey"`
	IdpRefused     bool      `gorm:"column:idpRefused;default:false;not null" json:"idpRefused"`
	CreatedAt      DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	MailedAt       *DateTime `gorm:"column:mailedAt" json:"mailedAt"`
}

func (OrganizationAccessNotice) TableName() string {
	return "OrganizationAccessNotice"
}

func (m *OrganizationAccessNotice) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type Team struct {
	ID               string    `gorm:"column:id;primaryKey" json:"id"`
	Name             string    `gorm:"column:name;not null" json:"name"`
	Slug             string    `gorm:"column:slug;uniqueIndex;not null" json:"slug"`
	OrganizationID   string    `gorm:"column:organizationId;index:Team_organizationId_idx;not null" json:"organizationId"`
	CreatedAt        DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt        DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	ArchivedAt       *DateTime `gorm:"column:archivedAt" json:"archivedAt"`
	IsPersonal       bool      `gorm:"column:isPersonal;index:Team_isPersonal_idx;default:false;not null" json:"isPersonal"`
	OwnerUserID      *string   `gorm:"column:ownerUserId;type:varchar(255)" json:"ownerUserId"`
	DepartmentID     *string   `gorm:"column:departmentId;index:Team_departmentId_idx" json:"departmentId"`
	Organization     *Organization
	OwnerUser        *User             `gorm:"foreignKey:OwnerUserID"`
	Members          []TeamUser        `gorm:"foreignKey:TeamID"`
	Projects         []Project         `gorm:"foreignKey:TeamID"`
	IngestionSources []IngestionSource `gorm:"foreignKey:TeamID;constraint:OnDelete:SET NULL"`
	AiToolEntryTeams []AiToolEntryTeam `gorm:"foreignKey:TeamID;constraint:OnDelete:CASCADE"`
}

func (Team) TableName() string {
	return "Team"
}

func (m *Team) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*Team) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Organization struct {
	ID                          string                         `gorm:"column:id;primaryKey" json:"id"`
	Name                        string                         `gorm:"column:name;not null" json:"name"`
	Kind                        *string                        `gorm:"column:kind;default:'customer';not null" json:"kind"`
	PhoneNumber                 *string                        `gorm:"column:phoneNumber" json:"phoneNumber"`
	Slug                        string                         `gorm:"column:slug;uniqueIndex;not null" json:"slug"`
	CreatedAt                   DateTime                       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt                   DateTime                       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	UsageSpendingMaxLimit       *int                           `gorm:"column:usageSpendingMaxLimit" json:"usageSpendingMaxLimit"`
	DatasetAttachmentMaxMb      *int                           `gorm:"column:datasetAttachmentMaxMb" json:"datasetAttachmentMaxMb"`
	PrivateClickHouseLabel      *string                        `gorm:"column:privateClickHouseLabel" json:"privateClickHouseLabel"`
	PrivateS3Label              *string                        `gorm:"column:privateS3Label" json:"privateS3Label"`
	PrivateDataplaneSeenAt      *DateTime                      `gorm:"column:privateDataplaneSeenAt" json:"privateDataplaneSeenAt"`
	MaxSessionDurationDays      int                            `gorm:"column:maxSessionDurationDays;default:0;not null" json:"maxSessionDurationDays"`
	MfaRequired                 bool                           `gorm:"column:mfaRequired;default:false;not null" json:"mfaRequired"`
	LockoutAfterFailedAttempts  int                            `gorm:"column:lockoutAfterFailedAttempts;default:0;not null" json:"lockoutAfterFailedAttempts"`
	LockoutMinutes              *int                           `gorm:"column:lockoutMinutes;default:30;not null" json:"lockoutMinutes"`
	SessionIdleTimeoutMinutes   *int                           `gorm:"column:sessionIdleTimeoutMinutes;default:1440;not null" json:"sessionIdleTimeoutMinutes"`
	SessionMaxLifetimeMinutes   int                            `gorm:"column:sessionMaxLifetimeMinutes;default:0;not null" json:"sessionMaxLifetimeMinutes"`
	SignupData                  datatypes.JSON                 `gorm:"column:signupData" json:"signupData"`
	SignedDPA                   bool                           `gorm:"column:signedDPA;default:false;not null" json:"signedDPA"`
	ElasticsearchNodeURL        *string                        `gorm:"column:elasticsearchNodeUrl" json:"elasticsearchNodeUrl"`
	ElasticsearchAPIKey         *string                        `gorm:"column:elasticsearchApiKey" json:"elasticsearchApiKey"`
	UseCustomElasticsearch      bool                           `gorm:"column:useCustomElasticsearch;default:false;not null" json:"useCustomElasticsearch"`
	S3Endpoint                  *string                        `gorm:"column:s3Endpoint" json:"s3Endpoint"`
	S3AccessKeyID               *string                        `gorm:"column:s3AccessKeyId" json:"s3AccessKeyId"`
	S3SecretAccessKey           *string                        `gorm:"column:s3SecretAccessKey" json:"s3SecretAccessKey"`
	S3Bucket                    *string                        `gorm:"column:s3Bucket" json:"s3Bucket"`
	UseCustomS3                 bool                           `gorm:"column:useCustomS3;default:false;not null" json:"useCustomS3"`
	SentPlanLimitAlert          *DateTime                      `gorm:"column:sentPlanLimitAlert" json:"sentPlanLimitAlert"`
	SsoDomain                   *string                        `gorm:"column:ssoDomain;uniqueIndex" json:"ssoDomain"`
	SsoProvider                 *string                        `gorm:"column:ssoProvider;index:Organization_ssoProvider_idx" json:"ssoProvider"`
	DomainJoin                  *string                        `gorm:"column:domainJoin;default:'request';not null" json:"domainJoin"`
	JoinerRole                  string                         `gorm:"column:joinerRole;default:'MEMBER';not null" json:"joinerRole"`
	JoinDomains                 []string                       `gorm:"column:joinDomains;serializer:json;not null" json:"joinDomains"`
	PresenceEnabled             *bool                          `gorm:"column:presenceEnabled;default:true;not null" json:"presenceEnabled"`
	TraceSharingEnabled         *bool                          `gorm:"column:traceSharingEnabled;default:true;not null" json:"traceSharingEnabled"`
	SupportContact              *string                        `gorm:"column:supportContact" json:"supportContact"`
	PrimaryIntent               *string                        `gorm:"column:primaryIntent" json:"primaryIntent"`
	PromoCode                   *string                        `gorm:"column:promoCode" json:"promoCode"`
	StripeCustomerID            *string                        `gorm:"column:stripeCustomerId;uniqueIndex" json:"stripeCustomerId"`
	Currency                    string                         `gorm:"column:currency;default:'EUR';not null" json:"currency"`
	PricingModel                string                         `gorm:"column:pricingModel;default:'TIERED';not null" json:"pricingModel"`
	License                     *string                        `gorm:"column:license;type:text" json:"license"`
	LicenseExpiresAt            *DateTime                      `gorm:"column:licenseExpiresAt" json:"licenseExpiresAt"`
	LicenseLastValidatedAt      *DateTime                      `gorm:"column:licenseLastValidatedAt" json:"licenseLastValidatedAt"`
	SelfHostedCustomer          bool                           `gorm:"column:selfHostedCustomer;default:false;not null" json:"selfHostedCustomer"`
	ConnectServicesDisabled     []string                       `gorm:"column:connectServicesDisabled;serializer:json;not null" json:"connectServicesDisabled"`
	InstantEvalsEnabledAt       *DateTime                      `gorm:"column:instantEvalsEnabledAt" json:"instantEvalsEnabledAt"`
	InstantEvalsEnabledByUserID *string                        `gorm:"column:instantEvalsEnabledByUserId" json:"instantEvalsEnabledByUserId"`
	ConnectLastSyncAt           *DateTime                      `gorm:"column:connectLastSyncAt" json:"connectLastSyncAt"`
	ConnectLastSyncError        *string                        `gorm:"column:connectLastSyncError" json:"connectLastSyncError"`
	Members                     []OrganizationUser             `gorm:"foreignKey:OrganizationID"`
	Teams                       []Team                         `gorm:"foreignKey:OrganizationID"`
	WebhookEndpoints            []WebhookEndpoint              `gorm:"foreignKey:OrganizationID;constraint:OnDelete:CASCADE"`
	OrganizationInvite          []OrganizationInvite           `gorm:"foreignKey:OrganizationID"`
	CustomRoles                 []CustomRole                   `gorm:"foreignKey:OrganizationID"`
	Notifications               []Notification                 `gorm:"foreignKey:OrganizationID;constraint:OnDelete:CASCADE"`
	Subscriptions               []Subscription                 `gorm:"foreignKey:OrganizationID"`
	ScimTokens                  []ScimToken                    `gorm:"foreignKey:OrganizationID;constraint:OnDelete:CASCADE"`
	ScimRequestLog              []ScimRequestLog               `gorm:"foreignKey:OrganizationID;constraint:OnDelete:CASCADE"`
	PromptTags                  []PromptTag                    `gorm:"foreignKey:OrganizationID;constraint:OnDelete:CASCADE"`
	Groups                      []Group                        `gorm:"foreignKey:OrganizationID;constraint:OnDelete:CASCADE"`
	RoleBindings                []RoleBinding                  `gorm:"foreignKey:OrganizationID;constraint:OnDelete:CASCADE"`
	APIKeys                     []APIKey                       `gorm:"foreignKey:OrganizationID;constraint:OnDelete:RESTRICT"`
	IssuedLicenses              []IssuedLicense                `gorm:"foreignKey:OrganizationID;constraint:OnDelete:RESTRICT"`
	GatewayChangeEvents         []GatewayChangeEvent           `gorm:"foreignKey:OrganizationID;constraint:OnDelete:CASCADE"`
	RoutingPolicies             []RoutingPolicy                `gorm:"foreignKey:OrganizationID;constraint:OnDelete:CASCADE"`
	IngestionSources            []IngestionSource              `gorm:"foreignKey:OrganizationID;constraint:OnDelete:CASCADE"`
	AnomalyRules                []AnomalyRule                  `gorm:"foreignKey:OrganizationID;constraint:OnDelete:CASCADE"`
	AnomalyAlerts               []AnomalyAlert                 `gorm:"foreignKey:OrganizationID;constraint:OnDelete:CASCADE"`
	AiToolEntries               []AiToolEntry                  `gorm:"foreignKey:OrganizationID;constraint:OnDelete:CASCADE"`
	IngestionTemplates          []IngestionTemplate            `gorm:"foreignKey:OrganizationID;constraint:OnDelete:CASCADE"`
	GithubInstallations         []GithubInstallation           `gorm:"foreignKey:OrganizationID;constraint:OnDelete:CASCADE"`
	GithubPullRequests          []GithubPullRequest            `gorm:"foreignKey:OrganizationID;constraint:OnDelete:CASCADE"`
	GithubBranchPrChecks        []GithubBranchPullRequestCheck `gorm:"foreignKey:OrganizationID;constraint:OnDelete:CASCADE"`
	ConnectedBilling            *ConnectedBillingAccount       `gorm:"foreignKey:OrganizationID;constraint:OnDelete:RESTRICT"`
}

func (Organization) TableName() string {
	return "Organization"
}

func (m *Organization) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
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

type IssuedLicense struct {
	ID                     string    `gorm:"column:id;primaryKey" json:"id"`
	LicenseID              string    `gorm:"column:licenseId;uniqueIndex;not null" json:"licenseId"`
	TokenHash              string    `gorm:"column:tokenHash;uniqueIndex;not null" json:"tokenHash"`
	OrganizationID         *string   `gorm:"column:organizationId;index:IssuedLicense_organizationId_idx" json:"organizationId"`
	OrganizationName       string    `gorm:"column:organizationName;not null" json:"organizationName"`
	Email                  string    `gorm:"column:email;not null" json:"email"`
	PlanType               string    `gorm:"column:planType;not null" json:"planType"`
	MaxMembers             int       `gorm:"column:maxMembers;not null" json:"maxMembers"`
	MaxMembersLite         *int      `gorm:"column:maxMembersLite" json:"maxMembersLite"`
	LiteSeatsUnlimited     bool      `gorm:"column:liteSeatsUnlimited;default:false;not null" json:"liteSeatsUnlimited"`
	MaxMembersDeveloper    *int      `gorm:"column:maxMembersDeveloper" json:"maxMembersDeveloper"`
	IssuedAt               DateTime  `gorm:"column:issuedAt;not null" json:"issuedAt"`
	ExpiresAt              DateTime  `gorm:"column:expiresAt;index:IssuedLicense_expiresAt_idx;not null" json:"expiresAt"`
	Source                 string    `gorm:"column:source;not null" json:"source"`
	IssuedByID             *string   `gorm:"column:issuedById" json:"issuedById"`
	RevokedAt              *DateTime `gorm:"column:revokedAt" json:"revokedAt"`
	RevokedByID            *string   `gorm:"column:revokedById" json:"revokedById"`
	RevokedReason          *string   `gorm:"column:revokedReason" json:"revokedReason"`
	SupersededAt           *DateTime `gorm:"column:supersededAt" json:"supersededAt"`
	ReplacesID             *string   `gorm:"column:replacesId;uniqueIndex" json:"replacesId"`
	PendingDeliveryLicense *string   `gorm:"column:pendingDeliveryLicense;type:text" json:"pendingDeliveryLicense"`
	Services               []string  `gorm:"column:services;serializer:json;not null" json:"services"`
	SeatRateCents          *int      `gorm:"column:seatRateCents" json:"seatRateCents"`
	SeatCurrency           *string   `gorm:"column:seatCurrency" json:"seatCurrency"`
	CommitUsdCents         int       `gorm:"column:commitUsdCents;default:0;not null" json:"commitUsdCents"`
	OverageEnabled         bool      `gorm:"column:overageEnabled;default:false;not null" json:"overageEnabled"`
	OverageMaxUsdCents     *int      `gorm:"column:overageMaxUsdCents" json:"overageMaxUsdCents"`
	InstanceID             *string   `gorm:"column:instanceId" json:"instanceId"`
	InstanceBoundAt        *DateTime `gorm:"column:instanceBoundAt" json:"instanceBoundAt"`
	LastSyncAt             *DateTime `gorm:"column:lastSyncAt" json:"lastSyncAt"`
	LastSyncVersion        *string   `gorm:"column:lastSyncVersion" json:"lastSyncVersion"`
	ReportedMembers        *int      `gorm:"column:reportedMembers" json:"reportedMembers"`
	ReportedMembersLite    *int      `gorm:"column:reportedMembersLite" json:"reportedMembersLite"`
	VirtualKeyID           *string   `gorm:"column:virtualKeyId;uniqueIndex" json:"virtualKeyId"`
	SeatsRaisedFrom        *int      `gorm:"column:seatsRaisedFrom;index:IssuedLicense_seatsRaisedFrom_idx" json:"seatsRaisedFrom"`
	CreatedAt              DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt              DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Organization           *Organization
}

func (IssuedLicense) TableName() string {
	return "IssuedLicense"
}

func (m *IssuedLicense) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*IssuedLicense) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type OrganizationLicense struct {
	OrganizationID string    `gorm:"column:organizationId;primaryKey" json:"organizationId"`
	LicenseKey     *string   `gorm:"column:licenseKey;type:text" json:"licenseKey"`
	ExpiresAt      *DateTime `gorm:"column:expiresAt" json:"expiresAt"`
	ValidatedAt    *DateTime `gorm:"column:validatedAt" json:"validatedAt"`
	UpdatedAt      DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
}

func (OrganizationLicense) TableName() string {
	return "OrganizationLicense"
}

func (m *OrganizationLicense) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*OrganizationLicense) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type ActivationCode struct {
	ID                   string    `gorm:"column:id;primaryKey" json:"id"`
	CodeHash             string    `gorm:"column:codeHash;uniqueIndex;not null" json:"codeHash"`
	CodeHint             string    `gorm:"column:codeHint;not null" json:"codeHint"`
	OrganizationID       string    `gorm:"column:organizationId;index:ActivationCode_organizationId_idx;not null" json:"organizationId"`
	OrganizationName     string    `gorm:"column:organizationName;not null" json:"organizationName"`
	Email                string    `gorm:"column:email;not null" json:"email"`
	PlanType             string    `gorm:"column:planType;not null" json:"planType"`
	MaxMembers           int       `gorm:"column:maxMembers;not null" json:"maxMembers"`
	MaxMembersLite       *int      `gorm:"column:maxMembersLite;default:0" json:"maxMembersLite"`
	LiteSeatsUnlimited   bool      `gorm:"column:liteSeatsUnlimited;default:false;not null" json:"liteSeatsUnlimited"`
	MaxMembersDeveloper  *int      `gorm:"column:maxMembersDeveloper" json:"maxMembersDeveloper"`
	LicenseTermDays      int       `gorm:"column:licenseTermDays;not null" json:"licenseTermDays"`
	Services             []string  `gorm:"column:services;serializer:json;not null" json:"services"`
	ExpiresAt            DateTime  `gorm:"column:expiresAt;index:ActivationCode_expiresAt_idx;not null" json:"expiresAt"`
	Reusable             bool      `gorm:"column:reusable;default:false;not null" json:"reusable"`
	RedeemedAt           *DateTime `gorm:"column:redeemedAt" json:"redeemedAt"`
	RedeemedByInstanceID *string   `gorm:"column:redeemedByInstanceId" json:"redeemedByInstanceId"`
	IssuedLicenseID      *string   `gorm:"column:issuedLicenseId" json:"issuedLicenseId"`
	RedemptionCount      int       `gorm:"column:redemptionCount;default:0;not null" json:"redemptionCount"`
	RevokedAt            *DateTime `gorm:"column:revokedAt" json:"revokedAt"`
	RevokedByID          *string   `gorm:"column:revokedById" json:"revokedById"`
	CreatedByID          *string   `gorm:"column:createdById" json:"createdById"`
	CreatedAt            DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt            DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
}

func (ActivationCode) TableName() string {
	return "ActivationCode"
}

func (m *ActivationCode) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*ActivationCode) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type ConnectedBillingAccount struct {
	ID                      string         `gorm:"column:id;primaryKey" json:"id"`
	OrganizationID          string         `gorm:"column:organizationId;uniqueIndex;not null" json:"organizationId"`
	StripeCustomerID        string         `gorm:"column:stripeCustomerId;uniqueIndex;not null" json:"stripeCustomerId"`
	UsageSubscriptionID     *string        `gorm:"column:usageSubscriptionId" json:"usageSubscriptionId"`
	UsageSubscriptionItemID *string        `gorm:"column:usageSubscriptionItemId" json:"usageSubscriptionItemId"`
	TermStartsAt            DateTime       `gorm:"column:termStartsAt;not null" json:"termStartsAt"`
	TermEndsAt              DateTime       `gorm:"column:termEndsAt;not null" json:"termEndsAt"`
	CommitUsdCents          int            `gorm:"column:commitUsdCents;default:0;not null" json:"commitUsdCents"`
	SeatCurrency            string         `gorm:"column:seatCurrency;not null" json:"seatCurrency"`
	SeatRateCents           int            `gorm:"column:seatRateCents;not null" json:"seatRateCents"`
	Seats                   int            `gorm:"column:seats;not null" json:"seats"`
	BankTransferType        *string        `gorm:"column:bankTransferType" json:"bankTransferType"`
	BankTransferCountry     *string        `gorm:"column:bankTransferCountry" json:"bankTransferCountry"`
	BillingEmail            string         `gorm:"column:billingEmail;not null" json:"billingEmail"`
	PendingRenewal          datatypes.JSON `gorm:"column:pendingRenewal" json:"pendingRenewal"`
	CreatedByID             *string        `gorm:"column:createdById" json:"createdById"`
	CreatedAt               DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt               DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Organization            *Organization
}

func (ConnectedBillingAccount) TableName() string {
	return "ConnectedBillingAccount"
}

func (m *ConnectedBillingAccount) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*ConnectedBillingAccount) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type ConnectedCreditGrant struct {
	ID                  string   `gorm:"column:id;primaryKey" json:"id"`
	AccountID           string   `gorm:"column:accountId;index:ConnectedCreditGrant_accountId_idx;not null" json:"accountId"`
	StripeCreditGrantID string   `gorm:"column:stripeCreditGrantId;uniqueIndex;not null" json:"stripeCreditGrantId"`
	AmountUsdCents      int      `gorm:"column:amountUsdCents;not null" json:"amountUsdCents"`
	Kind                string   `gorm:"column:kind;not null" json:"kind"`
	TermEndsAt          DateTime `gorm:"column:termEndsAt;not null" json:"termEndsAt"`
	ExpiresAt           DateTime `gorm:"column:expiresAt;not null" json:"expiresAt"`
	CreatedAt           DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
}

func (ConnectedCreditGrant) TableName() string {
	return "ConnectedCreditGrant"
}

func (m *ConnectedCreditGrant) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type ConnectedInvoice struct {
	ID              string    `gorm:"column:id;primaryKey" json:"id"`
	AccountID       string    `gorm:"column:accountId;index:ConnectedInvoice_accountId_idx;not null" json:"accountId"`
	StripeInvoiceID string    `gorm:"column:stripeInvoiceId;uniqueIndex;not null" json:"stripeInvoiceId"`
	Kind            string    `gorm:"column:kind;not null" json:"kind"`
	Currency        string    `gorm:"column:currency;not null" json:"currency"`
	AmountCents     int       `gorm:"column:amountCents;not null" json:"amountCents"`
	Status          string    `gorm:"column:status;not null" json:"status"`
	PaidOutOfBandAt *DateTime `gorm:"column:paidOutOfBandAt" json:"paidOutOfBandAt"`
	TermStartsAt    *DateTime `gorm:"column:termStartsAt" json:"termStartsAt"`
	CreatedAt       DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt       DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
}

func (ConnectedInvoice) TableName() string {
	return "ConnectedInvoice"
}

func (m *ConnectedInvoice) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*ConnectedInvoice) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type ScimRequestLog struct {
	ID             string   `gorm:"column:id;primaryKey" json:"id"`
	OrganizationID string   `gorm:"column:organizationId;index:ScimRequestLog_organizationId_occurredAt_idx;not null" json:"organizationId"`
	ConnectionID   *string  `gorm:"column:connectionId;index:ScimRequestLog_connectionId_occurredAt_idx" json:"connectionId"`
	Method         string   `gorm:"column:method;not null" json:"method"`
	Resource       string   `gorm:"column:resource;not null" json:"resource"`
	Status         int      `gorm:"column:status;not null" json:"status"`
	Reason         *string  `gorm:"column:reason" json:"reason"`
	Detail         *string  `gorm:"column:detail" json:"detail"`
	OccurredAt     DateTime `gorm:"column:occurredAt;index:ScimRequestLog_organizationId_occurredAt_idx;index:ScimRequestLog_connectionId_occurredAt_idx;index:ScimRequestLog_occurredAt_idx;not null" json:"occurredAt"`
	Organization   *Organization
}

func (ScimRequestLog) TableName() string {
	return "ScimRequestLog"
}

func (m *ScimRequestLog) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.OccurredAt.IsZero() {
		m.OccurredAt = DateTime{Time: now}
	}
	return nil
}

type ScimToken struct {
	ID             string    `gorm:"column:id;primaryKey" json:"id"`
	OrganizationID string    `gorm:"column:organizationId;index:ScimToken_organizationId_idx;not null" json:"organizationId"`
	ConnectionID   *string   `gorm:"column:connectionId;index:ScimToken_connectionId_idx" json:"connectionId"`
	HashedToken    string    `gorm:"column:hashedToken;uniqueIndex;not null" json:"hashedToken"`
	HashScheme     *string   `gorm:"column:hashScheme;default:'sha256';not null" json:"hashScheme"`
	Description    *string   `gorm:"column:description" json:"description"`
	CreatedAt      DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	LastUsedAt     *DateTime `gorm:"column:lastUsedAt" json:"lastUsedAt"`
	Organization   *Organization
}

func (ScimToken) TableName() string {
	return "ScimToken"
}

func (m *ScimToken) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type ScimExternalID struct {
	OrganizationID string   `gorm:"column:organizationId;index:ScimExternalId_organizationId_userId_idx;not null" json:"organizationId"`
	ID             string   `gorm:"column:id;primaryKey" json:"id"`
	ConnectionID   string   `gorm:"column:connectionId;uniqueIndex:ScimExternalId_connectionId_externalId_key;index:ScimExternalId_connectionId_idx;not null" json:"connectionId"`
	ExternalID     string   `gorm:"column:externalId;uniqueIndex:ScimExternalId_connectionId_externalId_key;not null" json:"externalId"`
	UserID         string   `gorm:"column:userId;index:ScimExternalId_organizationId_userId_idx;index:ScimExternalId_userId_idx;not null" json:"userId"`
	CreatedAt      DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt      DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	User           *User
}

func (ScimExternalID) TableName() string {
	return "ScimExternalId"
}

func (m *ScimExternalID) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*ScimExternalID) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type ScimUserResource struct {
	OrganizationID string    `gorm:"column:organizationId;primaryKey;index:ScimUserResource_organizationId_userName_idx" json:"organizationId"`
	UserID         string    `gorm:"column:userId;primaryKey;index:ScimUserResource_userId_idx" json:"userId"`
	UserName       string    `gorm:"column:userName;index:ScimUserResource_organizationId_userName_idx;not null" json:"userName"`
	Name           *string   `gorm:"column:name" json:"name"`
	Active         *bool     `gorm:"column:active;default:true;not null" json:"active"`
	DeletedAt      *DateTime `gorm:"column:deletedAt" json:"deletedAt"`
	CreatedAt      DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt      DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	User           *User
}

func (ScimUserResource) TableName() string {
	return "ScimUserResource"
}

func (m *ScimUserResource) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*ScimUserResource) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type ScimDirectoryUser struct {
	OrganizationID string `gorm:"column:organizationId;index:ScimDirectoryUser_organizationId_userId_idx;not null" json:"organizationId"`
	ConnectionID   string `gorm:"column:connectionId;primaryKey" json:"connectionId"`
	UserID         string `gorm:"column:userId;primaryKey;index:ScimDirectoryUser_organizationId_userId_idx" json:"userId"`
}

func (ScimDirectoryUser) TableName() string {
	return "ScimDirectoryUser"
}

type ScimSyncState struct {
	ID                string         `gorm:"column:id;primaryKey" json:"id"`
	ConnectionID      string         `gorm:"column:connectionId;index:ScimSyncState_connectionId_idx;not null" json:"connectionId"`
	OrganizationID    string         `gorm:"column:organizationId;index:ScimSyncState_organizationId_idx;not null" json:"organizationId"`
	State             string         `gorm:"column:state;index:ScimSyncState_state_idx;not null" json:"state"`
	LastPushedAt      *DateTime      `gorm:"column:lastPushedAt" json:"lastPushedAt"`
	LastFailure       datatypes.JSON `gorm:"column:lastFailure" json:"lastFailure"`
	DeadLetters       datatypes.JSON `gorm:"column:deadLetters;default:'[]';not null" json:"deadLetters"`
	RevokedCause      *string        `gorm:"column:revokedCause" json:"revokedCause"`
	OccurredAt        DateTime       `gorm:"column:occurredAt;not null" json:"occurredAt"`
	LastEventID       string         `gorm:"column:lastEventId;not null" json:"lastEventId"`
	AcceptedAt        DateTime       `gorm:"column:acceptedAt;not null" json:"acceptedAt"`
	ProjectionVersion string         `gorm:"column:projectionVersion;not null" json:"projectionVersion"`
	CreatedAt         DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt         DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
}

func (ScimSyncState) TableName() string {
	return "ScimSyncState"
}

type ScimSsoConnectionView struct {
	ID                string         `gorm:"column:id;primaryKey" json:"id"`
	OrganizationID    string         `gorm:"column:organizationId;index:ScimSsoConnectionView_organizationId_createdAt_idx;not null" json:"organizationId"`
	Folded            datatypes.JSON `gorm:"column:folded;not null" json:"folded"`
	AppliedEventIds   []string       `gorm:"column:appliedEventIds;serializer:json;not null" json:"appliedEventIds"`
	ProjectionVersion string         `gorm:"column:projectionVersion;not null" json:"projectionVersion"`
	CreatedAt         DateTime       `gorm:"column:createdAt;index:ScimSsoConnectionView_organizationId_createdAt_idx;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt         DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
}

func (ScimSsoConnectionView) TableName() string {
	return "ScimSsoConnectionView"
}

type OrganizationSeatPeak struct {
	OrganizationID string  `gorm:"column:organizationId;primaryKey" json:"organizationId"`
	OpenedAt       float64 `gorm:"column:openedAt;not null" json:"openedAt"`
	FullSeats      int     `gorm:"column:fullSeats;not null" json:"fullSeats"`
	LiteSeats      int     `gorm:"column:liteSeats;not null" json:"liteSeats"`
	DeveloperSeats int     `gorm:"column:developerSeats;not null" json:"developerSeats"`
	LatestChangeAt float64 `gorm:"column:latestChangeAt;not null" json:"latestChangeAt"`
	Mismatches     int     `gorm:"column:mismatches;not null" json:"mismatches"`
}

func (OrganizationSeatPeak) TableName() string {
	return "OrganizationSeatPeak"
}

type OrganizationSeatPeakHolder struct {
	OrganizationID string   `gorm:"column:organizationId;primaryKey;index:OrganizationSeatPeakHolder_organizationId_holderKind_seat_expiresAt_idx;index:OrganizationSeatPeakHolder_organizationId_suppressedBy_idx" json:"organizationId"`
	HolderKind     string   `gorm:"column:holderKind;primaryKey;index:OrganizationSeatPeakHolder_organizationId_holderKind_seat_expiresAt_idx" json:"holderKind"`
	HolderID       string   `gorm:"column:holderId;primaryKey" json:"holderId"`
	Seat           string   `gorm:"column:seat;index:OrganizationSeatPeakHolder_organizationId_holderKind_seat_expiresAt_idx;not null" json:"seat"`
	LastChangedAt  float64  `gorm:"column:lastChangedAt;not null" json:"lastChangedAt"`
	LastChangeID   *string  `gorm:"column:lastChangeId" json:"lastChangeId"`
	ExpiresAt      *float64 `gorm:"column:expiresAt;index:OrganizationSeatPeakHolder_organizationId_holderKind_seat_expiresAt_idx" json:"expiresAt"`
	ExpiredByFold  bool     `gorm:"column:expiredByFold;default:false;not null" json:"expiredByFold"`
	SuppressedBy   *string  `gorm:"column:suppressedBy;index:OrganizationSeatPeakHolder_organizationId_suppressedBy_idx" json:"suppressedBy"`
	RestoredByFold bool     `gorm:"column:restoredByFold;default:false;not null" json:"restoredByFold"`
}

func (OrganizationSeatPeakHolder) TableName() string {
	return "OrganizationSeatPeakHolder"
}

type OrganizationSeatPeakMonth struct {
	OrganizationID string  `gorm:"column:organizationId;primaryKey" json:"organizationId"`
	Month          string  `gorm:"column:month;primaryKey" json:"month"`
	FullPeak       int     `gorm:"column:fullPeak;not null" json:"fullPeak"`
	FullPeakAt     float64 `gorm:"column:fullPeakAt;not null" json:"fullPeakAt"`
	LitePeak       int     `gorm:"column:litePeak;not null" json:"litePeak"`
	LitePeakAt     float64 `gorm:"column:litePeakAt;not null" json:"litePeakAt"`
	FullAtEnd      int     `gorm:"column:fullAtEnd;not null" json:"fullAtEnd"`
	LiteAtEnd      int     `gorm:"column:liteAtEnd;not null" json:"liteAtEnd"`
}

func (OrganizationSeatPeakMonth) TableName() string {
	return "OrganizationSeatPeakMonth"
}

type Project struct {
	ID                           string         `gorm:"column:id;primaryKey" json:"id"`
	Name                         string         `gorm:"column:name;not null" json:"name"`
	Slug                         string         `gorm:"column:slug;uniqueIndex;not null" json:"slug"`
	APIKey                       string         `gorm:"column:apiKey;uniqueIndex;not null" json:"apiKey"`
	LegacyAPIKeyLastUsedAt       *DateTime      `gorm:"column:legacyApiKeyLastUsedAt" json:"legacyApiKeyLastUsedAt"`
	LwqlKey                      string         `gorm:"column:lwqlKey;uniqueIndex;default:(gen_random_uuid())::text;not null" json:"lwqlKey"`
	TeamID                       string         `gorm:"column:teamId;index:Project_teamId_idx;index:Project_teamId_kind_idx;not null" json:"teamId"`
	Language                     string         `gorm:"column:language;not null" json:"language"`
	Framework                    string         `gorm:"column:framework;not null" json:"framework"`
	Kind                         *string        `gorm:"column:kind;index:Project_teamId_kind_idx;default:'application';not null" json:"kind"`
	AggregateRule                datatypes.JSON `gorm:"column:aggregateRule" json:"aggregateRule"`
	FirstMessage                 bool           `gorm:"column:firstMessage;default:false;not null" json:"firstMessage"`
	Integrated                   bool           `gorm:"column:integrated;default:false;not null" json:"integrated"`
	CreatedAt                    DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt                    DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	UserLinkTemplate             *string        `gorm:"column:userLinkTemplate" json:"userLinkTemplate"`
	TraceSharingEnabled          *bool          `gorm:"column:traceSharingEnabled;default:true;not null" json:"traceSharingEnabled"`
	PresenceEnabled              *bool          `gorm:"column:presenceEnabled;default:true;not null" json:"presenceEnabled"`
	S3Endpoint                   *string        `gorm:"column:s3Endpoint" json:"s3Endpoint"`
	S3AccessKeyID                *string        `gorm:"column:s3AccessKeyId" json:"s3AccessKeyId"`
	S3SecretAccessKey            *string        `gorm:"column:s3SecretAccessKey" json:"s3SecretAccessKey"`
	S3Bucket                     *string        `gorm:"column:s3Bucket" json:"s3Bucket"`
	ArchivedAt                   *DateTime      `gorm:"column:archivedAt" json:"archivedAt"`
	IsPersonal                   bool           `gorm:"column:isPersonal;index:Project_isPersonal_idx;default:false;not null" json:"isPersonal"`
	OwnerUserID                  *string        `gorm:"column:ownerUserId;index:Project_ownerUserId_idx;type:varchar(255)" json:"ownerUserId"`
	PersonalFeatures             datatypes.JSON `gorm:"column:personalFeatures;default:'{}';not null" json:"personalFeatures"`
	DepartmentID                 *string        `gorm:"column:departmentId;index:Project_departmentId_idx" json:"departmentId"`
	LangyEgressAllowlist         datatypes.JSON `gorm:"column:langyEgressAllowlist" json:"langyEgressAllowlist"`
	LastCodingAgentSessionAt     *DateTime      `gorm:"column:lastCodingAgentSessionAt" json:"lastCodingAgentSessionAt"`
	LastCodingAgentPullRequestAt *DateTime      `gorm:"column:lastCodingAgentPullRequestAt" json:"lastCodingAgentPullRequestAt"`
	Team                         *Team
	OwnerUser                    *User                     `gorm:"foreignKey:OwnerUserID"`
	Checks                       []Monitor                 `gorm:"foreignKey:ProjectID"`
	Costs                        []Cost                    `gorm:"foreignKey:ProjectID"`
	Topics                       []Topic                   `gorm:"foreignKey:ProjectID"`
	Datasets                     []Dataset                 `gorm:"foreignKey:ProjectID"`
	DatasetRecords               []DatasetRecord           `gorm:"foreignKey:ProjectID"`
	CustomGraphs                 []CustomGraph             `gorm:"foreignKey:ProjectID"`
	Dashboards                   []Dashboard               `gorm:"foreignKey:ProjectID"`
	BatchEvaluations             []BatchEvaluation         `gorm:"foreignKey:ProjectID"`
	Triggers                     []Trigger                 `gorm:"foreignKey:ProjectID"`
	Experiments                  []Experiment              `gorm:"foreignKey:ProjectID"`
	Annotations                  []Annotation              `gorm:"foreignKey:ProjectID"`
	ProjectSecrets               []ProjectSecret           `gorm:"foreignKey:ProjectID"`
	TriggerSent                  []TriggerSent             `gorm:"foreignKey:ProjectID"`
	WebhookDeliveries            []WebhookEndpointDelivery `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	AnnotationScores             []AnnotationScore         `gorm:"foreignKey:ProjectID"`
	ShareLinks                   []ShareLink               `gorm:"foreignKey:ProjectID"`
	Workflows                    []Workflow                `gorm:"foreignKey:ProjectID"`
	WorkflowVersion              []WorkflowVersion         `gorm:"foreignKey:ProjectID"`
	AnnotationQueue              []AnnotationQueue         `gorm:"foreignKey:ProjectID"`
	AnnotationQueueItem          []AnnotationQueueItem     `gorm:"foreignKey:ProjectID"`
	LlmPromptConfigs             []LlmPromptConfig         `gorm:"foreignKey:ProjectID"`
	Analytics                    []Analytics               `gorm:"foreignKey:ProjectID"`
	Notifications                []Notification            `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	Agents                       []Agent                   `gorm:"foreignKey:ProjectID"`
	Evaluators                   []Evaluator               `gorm:"foreignKey:ProjectID"`
	Scenarios                    []Scenario                `gorm:"foreignKey:ProjectID"`
	SuiteConfigurations          []SimulationSuite         `gorm:"foreignKey:ProjectID"`
	SavedViews                   []SavedView               `gorm:"foreignKey:ProjectID"`
	GatewayGuardrails            []GatewayGuardrail        `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	GatewayChangeEvents          []GatewayChangeEvent      `gorm:"foreignKey:ProjectID;constraint:OnDelete:SET NULL"`
	PinnedTraces                 []PinnedTrace             `gorm:"foreignKey:ProjectID"`
	TraceEditOverlays            []TraceEditOverlay        `gorm:"foreignKey:ProjectID"`
	EmailSuppressions            []EmailSuppression        `gorm:"foreignKey:ProjectID"`
}

func (Project) TableName() string {
	return "Project"
}

func (m *Project) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*Project) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Department struct {
	ID             string    `gorm:"column:id;primaryKey" json:"id"`
	OrganizationID string    `gorm:"column:organizationId;index:Department_organizationId_idx;not null" json:"organizationId"`
	Name           string    `gorm:"column:name;not null" json:"name"`
	CreatedAt      DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt      DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	ArchivedAt     *DateTime `gorm:"column:archivedAt" json:"archivedAt"`
}

func (Department) TableName() string {
	return "Department"
}

func (m *Department) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*Department) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type DepartmentMembershipHistory struct {
	ID             string    `gorm:"column:id;primaryKey" json:"id"`
	OrganizationID string    `gorm:"column:organizationId;index:DepartmentMembershipHistory_organizationId_userId_validFrom_idx;not null" json:"organizationId"`
	UserID         string    `gorm:"column:userId;index:DepartmentMembershipHistory_organizationId_userId_validFrom_idx;not null" json:"userId"`
	DepartmentID   string    `gorm:"column:departmentId;not null" json:"departmentId"`
	ValidFrom      DateTime  `gorm:"column:validFrom;index:DepartmentMembershipHistory_organizationId_userId_validFrom_idx;not null" json:"validFrom"`
	ValidTo        *DateTime `gorm:"column:validTo" json:"validTo"`
	CreatedAt      DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
}

func (DepartmentMembershipHistory) TableName() string {
	return "DepartmentMembershipHistory"
}

func (m *DepartmentMembershipHistory) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type PlatformToolPolicy struct {
	ID              string   `gorm:"column:id;primaryKey" json:"id"`
	OrganizationID  string   `gorm:"column:organizationId;uniqueIndex:PlatformToolPolicy_organizationId_toolSlug_key;index:PlatformToolPolicy_organizationId_idx;not null" json:"organizationId"`
	ToolSlug        string   `gorm:"column:toolSlug;uniqueIndex:PlatformToolPolicy_organizationId_toolSlug_key;not null" json:"toolSlug"`
	AllowVk         *bool    `gorm:"column:allowVk;default:true;not null" json:"allowVk"`
	AllowOtelDirect *bool    `gorm:"column:allowOtelDirect;default:true;not null" json:"allowOtelDirect"`
	CreatedAt       DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt       DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
}

func (PlatformToolPolicy) TableName() string {
	return "PlatformToolPolicy"
}

func (m *PlatformToolPolicy) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*PlatformToolPolicy) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type OrganizationInvite struct {
	ID                      string         `gorm:"column:id;primaryKey" json:"id"`
	Email                   string         `gorm:"column:email;not null" json:"email"`
	InviteCode              string         `gorm:"column:inviteCode;uniqueIndex;not null" json:"inviteCode"`
	Expiration              *DateTime      `gorm:"column:expiration" json:"expiration"`
	Status                  string         `gorm:"column:status;default:'PENDING';not null" json:"status"`
	OrganizationID          string         `gorm:"column:organizationId;index:OrganizationInvite_organizationId_idx;not null" json:"organizationId"`
	TeamIds                 string         `gorm:"column:teamIds;not null" json:"teamIds"`
	TeamAssignments         datatypes.JSON `gorm:"column:teamAssignments" json:"teamAssignments"`
	Role                    string         `gorm:"column:role;not null" json:"role"`
	RequestedBy             *string        `gorm:"column:requestedBy;index:OrganizationInvite_requestedBy_idx" json:"requestedBy"`
	SubscriptionID          *string        `gorm:"column:subscriptionId" json:"subscriptionId"`
	AcceptedByUserID        *string        `gorm:"column:acceptedByUserId" json:"acceptedByUserId"`
	AcceptedViaIdentifierID *string        `gorm:"column:acceptedViaIdentifierId" json:"acceptedViaIdentifierId"`
	CreatedAt               DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt               DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Organization            *Organization
	RequestedByUser         *User `gorm:"foreignKey:RequestedBy"`
}

func (OrganizationInvite) TableName() string {
	return "OrganizationInvite"
}

func (m *OrganizationInvite) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*OrganizationInvite) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Monitor struct {
	ID                string         `gorm:"column:id;primaryKey" json:"id"`
	ProjectID         string         `gorm:"column:projectId;uniqueIndex:Monitor_projectId_slug_key;index:Monitor_projectId_idx;not null" json:"projectId"`
	ExperimentID      *string        `gorm:"column:experimentId;uniqueIndex" json:"experimentId"`
	EvaluatorID       *string        `gorm:"column:evaluatorId;index:Monitor_evaluatorId_idx" json:"evaluatorId"`
	CheckType         string         `gorm:"column:checkType;not null" json:"checkType"`
	Name              string         `gorm:"column:name;not null" json:"name"`
	Slug              string         `gorm:"column:slug;uniqueIndex:Monitor_projectId_slug_key;not null" json:"slug"`
	ExecutionMode     string         `gorm:"column:executionMode;default:'ON_MESSAGE';not null" json:"executionMode"`
	Enabled           *bool          `gorm:"column:enabled;default:true;not null" json:"enabled"`
	Preconditions     datatypes.JSON `gorm:"column:preconditions;not null" json:"preconditions"`
	Parameters        datatypes.JSON `gorm:"column:parameters;not null" json:"parameters"`
	Mappings          datatypes.JSON `gorm:"column:mappings" json:"mappings"`
	Sample            *float64       `gorm:"column:sample;default:1;not null" json:"sample"`
	Level             *string        `gorm:"column:level;default:'trace';not null" json:"level"`
	ThreadIdleTimeout *int           `gorm:"column:threadIdleTimeout" json:"threadIdleTimeout"`
	CreatedAt         DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt         DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	AutoPausedAt      *DateTime      `gorm:"column:autoPausedAt" json:"autoPausedAt"`
	AutoPausedReason  *string        `gorm:"column:autoPausedReason" json:"autoPausedReason"`
	EvaluatorLabel    *string        `gorm:"column:evaluatorLabel" json:"evaluatorLabel"`
	CreatedByID       *string        `gorm:"column:createdById" json:"createdById"`
	UpdatedByID       *string        `gorm:"column:updatedById" json:"updatedById"`
	Project           *Project
	Experiment        *Experiment
	Evaluator         *Evaluator
}

func (Monitor) TableName() string {
	return "Monitor"
}

func (m *Monitor) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*Monitor) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Cost struct {
	ID            string         `gorm:"column:id;primaryKey" json:"id"`
	ProjectID     string         `gorm:"column:projectId;index:Cost_projectId_idx;index:Cost_projectId_createdAt_idx;not null" json:"projectId"`
	CostType      string         `gorm:"column:costType;index:Cost_costType_idx;not null" json:"costType"`
	CostName      *string        `gorm:"column:costName" json:"costName"`
	ReferenceType string         `gorm:"column:referenceType;index:Cost_referenceType_referenceId_idx;not null" json:"referenceType"`
	ReferenceID   string         `gorm:"column:referenceId;index:Cost_referenceType_referenceId_idx;not null" json:"referenceId"`
	Amount        float64        `gorm:"column:amount;not null" json:"amount"`
	Currency      string         `gorm:"column:currency;not null" json:"currency"`
	CreatedAt     DateTime       `gorm:"column:createdAt;index:Cost_projectId_createdAt_idx;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt     DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	ExtraInfo     datatypes.JSON `gorm:"column:extraInfo" json:"extraInfo"`
	Project       *Project
}

func (Cost) TableName() string {
	return "Cost"
}

func (m *Cost) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*Cost) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Topic struct {
	ID                     string         `gorm:"column:id;primaryKey" json:"id"`
	ProjectID              string         `gorm:"column:projectId;index:Topic_projectId_idx;not null" json:"projectId"`
	Name                   string         `gorm:"column:name;not null" json:"name"`
	ParentID               *string        `gorm:"column:parentId;index:Topic_parentId_idx" json:"parentId"`
	EmbeddingsModel        string         `gorm:"column:embeddings_model;not null" json:"embeddings_model"`
	Centroid               datatypes.JSON `gorm:"column:centroid;not null" json:"centroid"`
	P95Distance            float64        `gorm:"column:p95Distance;not null" json:"p95Distance"`
	AutomaticallyGenerated *bool          `gorm:"column:automaticallyGenerated;default:true;not null" json:"automaticallyGenerated"`
	LastEventID            *string        `gorm:"column:lastEventId" json:"lastEventId"`
	CreatedAt              DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt              DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Project                *Project
	Parent                 *Topic  `gorm:"foreignKey:ParentID"`
	Subtopics              []Topic `gorm:"foreignKey:ParentID;constraint:OnUpdate:RESTRICT,OnDelete:RESTRICT"`
}

func (Topic) TableName() string {
	return "Topic"
}

func (m *Topic) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*Topic) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Dataset struct {
	ID                   string         `gorm:"column:id;primaryKey" json:"id"`
	ProjectID            string         `gorm:"column:projectId;uniqueIndex:Dataset_projectId_slug_key;not null" json:"projectId"`
	Name                 string         `gorm:"column:name;not null" json:"name"`
	Slug                 string         `gorm:"column:slug;uniqueIndex:Dataset_projectId_slug_key;not null" json:"slug"`
	ColumnTypes          datatypes.JSON `gorm:"column:columnTypes;not null" json:"columnTypes"`
	CreatedAt            DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt            DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	ArchivedAt           *DateTime      `gorm:"column:archivedAt" json:"archivedAt"`
	Mapping              datatypes.JSON `gorm:"column:mapping" json:"mapping"`
	UseS3                bool           `gorm:"column:useS3;default:false;not null" json:"useS3"`
	S3RecordCount        *int           `gorm:"column:s3RecordCount" json:"s3RecordCount"`
	ContentLayout        *string        `gorm:"column:contentLayout;default:'postgres';not null" json:"contentLayout"`
	Status               *string        `gorm:"column:status;default:'ready';not null" json:"status"`
	StatusError          *string        `gorm:"column:statusError" json:"statusError"`
	StagingKey           *string        `gorm:"column:stagingKey" json:"stagingKey"`
	SourceStoredObjectID *string        `gorm:"column:sourceStoredObjectId" json:"sourceStoredObjectId"`
	UploadFilename       *string        `gorm:"column:uploadFilename" json:"uploadFilename"`
	RowCount             *int           `gorm:"column:rowCount" json:"rowCount"`
	SizeBytes            *int64         `gorm:"column:sizeBytes" json:"sizeBytes"`
	ChunkCount           *int           `gorm:"column:chunkCount" json:"chunkCount"`
	ChunkOffsets         datatypes.JSON `gorm:"column:chunkOffsets" json:"chunkOffsets"`
	Project              *Project
	DatasetRecords       []DatasetRecord   `gorm:"foreignKey:DatasetID"`
	BatchEvaluations     []BatchEvaluation `gorm:"foreignKey:DatasetID"`
}

func (Dataset) TableName() string {
	return "Dataset"
}

func (m *Dataset) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*Dataset) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type DatasetRecord struct {
	ID        string         `gorm:"column:id;primaryKey" json:"id"`
	DatasetID string         `gorm:"column:datasetId;index:DatasetRecord_datasetId_idx;index:DatasetRecord_projectId_datasetId_idx;not null" json:"datasetId"`
	ProjectID string         `gorm:"column:projectId;index:DatasetRecord_projectId_idx;index:DatasetRecord_projectId_datasetId_idx;not null" json:"projectId"`
	Entry     datatypes.JSON `gorm:"column:entry;not null" json:"entry"`
	CreatedAt DateTime       `gorm:"column:createdAt;index:DatasetRecord_createdAt_idx;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Dataset   *Dataset
	Project   *Project
}

func (DatasetRecord) TableName() string {
	return "DatasetRecord"
}

func (m *DatasetRecord) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*DatasetRecord) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type DatasetVersion struct {
	ID                  string         `gorm:"column:id;primaryKey" json:"id"`
	ProjectID           string         `gorm:"column:projectId;index:DatasetVersion_projectId_datasetId_idx;not null" json:"projectId"`
	DatasetID           string         `gorm:"column:datasetId;uniqueIndex:DatasetVersion_datasetId_version_key;index:DatasetVersion_projectId_datasetId_idx;not null" json:"datasetId"`
	Version             int            `gorm:"column:version;uniqueIndex:DatasetVersion_datasetId_version_key;not null" json:"version"`
	Name                *string        `gorm:"column:name" json:"name"`
	Trigger             string         `gorm:"column:trigger;not null" json:"trigger"`
	ContentHash         string         `gorm:"column:contentHash;not null" json:"contentHash"`
	ColumnTypes         datatypes.JSON `gorm:"column:columnTypes;not null" json:"columnTypes"`
	RowCount            int            `gorm:"column:rowCount;not null" json:"rowCount"`
	SizeBytes           int64          `gorm:"column:sizeBytes;not null" json:"sizeBytes"`
	ChunkCount          int            `gorm:"column:chunkCount;not null" json:"chunkCount"`
	RestoredFromVersion *int           `gorm:"column:restoredFromVersion" json:"restoredFromVersion"`
	AuthorID            *string        `gorm:"column:authorId" json:"authorId"`
	AuthorKind          *string        `gorm:"column:authorKind" json:"authorKind"`
	PinnedAt            *DateTime      `gorm:"column:pinnedAt" json:"pinnedAt"`
	CreatedAt           DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
}

func (DatasetVersion) TableName() string {
	return "DatasetVersion"
}

func (m *DatasetVersion) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type DatasetVersionRun struct {
	ID           string   `gorm:"column:id;primaryKey" json:"id"`
	ProjectID    string   `gorm:"column:projectId;uniqueIndex:DatasetVersionRun_projectId_runId_datasetId_key;not null" json:"projectId"`
	DatasetID    string   `gorm:"column:datasetId;uniqueIndex:DatasetVersionRun_projectId_runId_datasetId_key;not null" json:"datasetId"`
	VersionID    string   `gorm:"column:versionId;index:DatasetVersionRun_versionId_idx;not null" json:"versionId"`
	RunID        string   `gorm:"column:runId;uniqueIndex:DatasetVersionRun_projectId_runId_datasetId_key;not null" json:"runId"`
	ExperimentID *string  `gorm:"column:experimentId" json:"experimentId"`
	CreatedAt    DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
}

func (DatasetVersionRun) TableName() string {
	return "DatasetVersionRun"
}

func (m *DatasetVersionRun) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type Dashboard struct {
	ID             string   `gorm:"column:id;primaryKey" json:"id"`
	ProjectID      string   `gorm:"column:projectId;index:Dashboard_projectId_idx;index:Dashboard_projectId_order_idx;not null" json:"projectId"`
	Name           string   `gorm:"column:name;not null" json:"name"`
	Order          int      `gorm:"column:order;index:Dashboard_projectId_order_idx;default:0;not null" json:"order"`
	Description    *string  `gorm:"column:description" json:"description"`
	CreatedByID    *string  `gorm:"column:createdById" json:"createdById"`
	Scope          string   `gorm:"column:scope;index:Dashboard_organizationId_scope_idx;default:'PROJECT';not null" json:"scope"`
	OrganizationID *string  `gorm:"column:organizationId;index:Dashboard_organizationId_scope_idx" json:"organizationId"`
	CreatedAt      DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt      DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Project        *Project
	Graphs         []CustomGraph `gorm:"foreignKey:DashboardID;constraint:OnDelete:CASCADE"`
}

func (Dashboard) TableName() string {
	return "Dashboard"
}

func (m *Dashboard) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*Dashboard) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type DashboardFavourite struct {
	ID          string   `gorm:"column:id;primaryKey" json:"id"`
	UserID      string   `gorm:"column:userId;uniqueIndex:DashboardFavourite_userId_dashboardId_key;uniqueIndex:DashboardFavourite_userId_projectId_templateId_key;index:DashboardFavourite_userId_projectId_position_idx;not null" json:"userId"`
	DashboardID *string  `gorm:"column:dashboardId;uniqueIndex:DashboardFavourite_userId_dashboardId_key;index:DashboardFavourite_dashboardId_idx" json:"dashboardId"`
	TemplateID  *string  `gorm:"column:templateId;uniqueIndex:DashboardFavourite_userId_projectId_templateId_key" json:"templateId"`
	ProjectID   string   `gorm:"column:projectId;uniqueIndex:DashboardFavourite_userId_projectId_templateId_key;index:DashboardFavourite_userId_projectId_position_idx;not null" json:"projectId"`
	Position    int      `gorm:"column:position;index:DashboardFavourite_userId_projectId_position_idx;not null" json:"position"`
	CreatedAt   DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
}

func (DashboardFavourite) TableName() string {
	return "DashboardFavourite"
}

func (m *DashboardFavourite) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type DashboardsV2 struct {
	ID              string         `gorm:"column:id;primaryKey" json:"id"`
	ProjectID       string         `gorm:"column:projectId;index:DashboardsV2_projectId_idx;not null" json:"projectId"`
	Name            string         `gorm:"column:name;not null" json:"name"`
	Description     *string        `gorm:"column:description" json:"description"`
	CreatedByID     *string        `gorm:"column:createdById" json:"createdById"`
	Scope           string         `gorm:"column:scope;index:DashboardsV2_organizationId_scope_idx;default:'PROJECT';not null" json:"scope"`
	OrganizationID  *string        `gorm:"column:organizationId;index:DashboardsV2_organizationId_scope_idx" json:"organizationId"`
	ExcludedOrigins datatypes.JSON `gorm:"column:excludedOrigins" json:"excludedOrigins"`
	CreatedAt       DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt       DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
}

func (DashboardsV2) TableName() string {
	return "DashboardsV2"
}

func (m *DashboardsV2) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*DashboardsV2) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type DashboardWidget struct {
	ID          string         `gorm:"column:id;primaryKey" json:"id"`
	ProjectID   string         `gorm:"column:projectId;index:DashboardWidget_projectId_dashboardId_idx;not null" json:"projectId"`
	DashboardID *string        `gorm:"column:dashboardId;index:DashboardWidget_projectId_dashboardId_idx" json:"dashboardId"`
	Name        string         `gorm:"column:name;not null" json:"name"`
	Definition  datatypes.JSON `gorm:"column:definition;not null" json:"definition"`
	GridColumn  int            `gorm:"column:gridColumn;default:0;not null" json:"gridColumn"`
	GridRow     int            `gorm:"column:gridRow;default:0;not null" json:"gridRow"`
	ColSpan     *int           `gorm:"column:colSpan;default:1;not null" json:"colSpan"`
	RowSpan     *int           `gorm:"column:rowSpan;default:1;not null" json:"rowSpan"`
	CreatedAt   DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt   DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
}

func (DashboardWidget) TableName() string {
	return "DashboardWidget"
}

func (m *DashboardWidget) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*DashboardWidget) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type InsightProjection struct {
	ID                       string         `gorm:"column:id;primaryKey" json:"id"`
	ProjectID                string         `gorm:"column:projectId;index:InsightProjection_projectId_filedAt_idx;index:InsightProjection_projectId_ownerUserId_filedAt_idx;not null" json:"projectId"`
	Title                    string         `gorm:"column:title;not null" json:"title"`
	Body                     string         `gorm:"column:body;not null" json:"body"`
	Tone                     string         `gorm:"column:tone;not null" json:"tone"`
	Topic                    *string        `gorm:"column:topic" json:"topic"`
	ValidDays                int            `gorm:"column:validDays;not null" json:"validDays"`
	Lwql                     *string        `gorm:"column:lwql" json:"lwql"`
	SourceConversationID     *string        `gorm:"column:sourceConversationId" json:"sourceConversationId"`
	SourceMessageID          *string        `gorm:"column:sourceMessageId" json:"sourceMessageId"`
	FiledByUserID            *string        `gorm:"column:filedByUserId" json:"filedByUserId"`
	FiledAt                  float64        `gorm:"column:filedAt;index:InsightProjection_projectId_filedAt_idx;index:InsightProjection_projectId_ownerUserId_filedAt_idx;not null" json:"filedAt"`
	RenewedAt                *float64       `gorm:"column:renewedAt" json:"renewedAt"`
	CreatedAt                float64        `gorm:"column:createdAt;not null" json:"createdAt"`
	UpdatedAt                float64        `gorm:"column:updatedAt;not null" json:"updatedAt"`
	OccurredAt               float64        `gorm:"column:occurredAt;not null" json:"occurredAt"`
	AcceptedAt               float64        `gorm:"column:acceptedAt;not null" json:"acceptedAt"`
	LastEventID              string         `gorm:"column:lastEventId;not null" json:"lastEventId"`
	ProjectionVersion        string         `gorm:"column:projectionVersion;not null" json:"projectionVersion"`
	BoardID                  *string        `gorm:"column:boardId" json:"boardId"`
	BoardName                *string        `gorm:"column:boardName" json:"boardName"`
	WidgetID                 *string        `gorm:"column:widgetId" json:"widgetId"`
	WidgetName               *string        `gorm:"column:widgetName" json:"widgetName"`
	FiledVia                 *string        `gorm:"column:filedVia;default:'chat';not null" json:"filedVia"`
	ReplayStart              *float64       `gorm:"column:replayStart" json:"replayStart"`
	ReplayEnd                *float64       `gorm:"column:replayEnd" json:"replayEnd"`
	ReplayGranularitySeconds *int           `gorm:"column:replayGranularitySeconds" json:"replayGranularitySeconds"`
	ReplayContext            datatypes.JSON `gorm:"column:replayContext" json:"replayContext"`
	OwnerUserID              *string        `gorm:"column:ownerUserId;index:InsightProjection_projectId_ownerUserId_filedAt_idx" json:"ownerUserId"`
	FindingKey               *string        `gorm:"column:findingKey" json:"findingKey"`
}

func (InsightProjection) TableName() string {
	return "InsightProjection"
}

type InsightReaderProjection struct {
	ID                string   `gorm:"column:id;primaryKey" json:"id"`
	ProjectID         string   `gorm:"column:projectId;uniqueIndex:InsightReaderProjection_projectId_insightId_userId_key;index:InsightReaderProjection_projectId_userId_idx;not null" json:"projectId"`
	InsightID         string   `gorm:"column:insightId;uniqueIndex:InsightReaderProjection_projectId_insightId_userId_key;not null" json:"insightId"`
	UserID            string   `gorm:"column:userId;uniqueIndex:InsightReaderProjection_projectId_insightId_userId_key;index:InsightReaderProjection_projectId_userId_idx;not null" json:"userId"`
	SeenAt            *float64 `gorm:"column:seenAt" json:"seenAt"`
	ArchivedAt        *float64 `gorm:"column:archivedAt" json:"archivedAt"`
	KeptAt            *float64 `gorm:"column:keptAt" json:"keptAt"`
	CreatedAt         float64  `gorm:"column:createdAt;not null" json:"createdAt"`
	UpdatedAt         float64  `gorm:"column:updatedAt;not null" json:"updatedAt"`
	OccurredAt        float64  `gorm:"column:occurredAt;not null" json:"occurredAt"`
	AcceptedAt        float64  `gorm:"column:acceptedAt;not null" json:"acceptedAt"`
	LastEventID       string   `gorm:"column:lastEventId;not null" json:"lastEventId"`
	ProjectionVersion string   `gorm:"column:projectionVersion;not null" json:"projectionVersion"`
}

func (InsightReaderProjection) TableName() string {
	return "InsightReaderProjection"
}

type InsightDailyScheduleProjection struct {
	ID                    string   `gorm:"column:id;primaryKey" json:"id"`
	ProjectID             string   `gorm:"column:projectId;uniqueIndex:InsightDailyScheduleProjection_projectId_userId_boardKind_boardId_key;not null" json:"projectId"`
	UserID                string   `gorm:"column:userId;uniqueIndex:InsightDailyScheduleProjection_projectId_userId_boardKind_boardId_key;not null" json:"userId"`
	BoardKind             string   `gorm:"column:boardKind;uniqueIndex:InsightDailyScheduleProjection_projectId_userId_boardKind_boardId_key;not null" json:"boardKind"`
	BoardID               string   `gorm:"column:boardId;uniqueIndex:InsightDailyScheduleProjection_projectId_userId_boardKind_boardId_key;not null" json:"boardId"`
	BoardName             string   `gorm:"column:boardName;not null" json:"boardName"`
	State                 *string  `gorm:"column:state;default:'undecided';not null" json:"state"`
	OffReason             *string  `gorm:"column:offReason" json:"offReason"`
	Hour                  *int     `gorm:"column:hour" json:"hour"`
	Timezone              *string  `gorm:"column:timezone" json:"timezone"`
	MaxInsights           *int     `gorm:"column:maxInsights" json:"maxInsights"`
	LastRunID             *string  `gorm:"column:lastRunId" json:"lastRunId"`
	LastRunAt             *float64 `gorm:"column:lastRunAt" json:"lastRunAt"`
	LastRunOutcome        *string  `gorm:"column:lastRunOutcome" json:"lastRunOutcome"`
	LastRunReason         *string  `gorm:"column:lastRunReason" json:"lastRunReason"`
	LastRunFiled          *int     `gorm:"column:lastRunFiled" json:"lastRunFiled"`
	LastRunRenewed        *int     `gorm:"column:lastRunRenewed" json:"lastRunRenewed"`
	LastRunConversationID *string  `gorm:"column:lastRunConversationId" json:"lastRunConversationId"`
	ConsecutiveSkips      int      `gorm:"column:consecutiveSkips;default:0;not null" json:"consecutiveSkips"`
	SkipStreakSlot        *float64 `gorm:"column:skipStreakSlot" json:"skipStreakSlot"`
	CreatedAt             float64  `gorm:"column:createdAt;not null" json:"createdAt"`
	UpdatedAt             float64  `gorm:"column:updatedAt;not null" json:"updatedAt"`
	OccurredAt            float64  `gorm:"column:occurredAt;not null" json:"occurredAt"`
	AcceptedAt            float64  `gorm:"column:acceptedAt;not null" json:"acceptedAt"`
	LastEventID           string   `gorm:"column:lastEventId;not null" json:"lastEventId"`
	ProjectionVersion     string   `gorm:"column:projectionVersion;not null" json:"projectionVersion"`
}

func (InsightDailyScheduleProjection) TableName() string {
	return "InsightDailyScheduleProjection"
}

type SavedView struct {
	ID        string         `gorm:"column:id;primaryKey" json:"id"`
	ProjectID string         `gorm:"column:projectId;index:SavedView_projectId_idx;index:SavedView_projectId_order_idx;index:SavedView_projectId_kind_order_idx;not null" json:"projectId"`
	UserID    *string        `gorm:"column:userId;index:SavedView_userId_idx" json:"userId"`
	Name      string         `gorm:"column:name;not null" json:"name"`
	Filters   datatypes.JSON `gorm:"column:filters;default:'{}';not null" json:"filters"`
	Query     *string        `gorm:"column:query" json:"query"`
	Period    datatypes.JSON `gorm:"column:period" json:"period"`
	Order     int            `gorm:"column:order;index:SavedView_projectId_order_idx;index:SavedView_projectId_kind_order_idx;default:0;not null" json:"order"`
	Kind      *string        `gorm:"column:kind;index:SavedView_projectId_kind_order_idx;default:'v1-traces-filter';not null" json:"kind"`
	CreatedAt DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Project   *Project
	User      *User
}

func (SavedView) TableName() string {
	return "SavedView"
}

func (m *SavedView) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*SavedView) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type CustomGraph struct {
	ID          string         `gorm:"column:id;primaryKey" json:"id"`
	ProjectID   string         `gorm:"column:projectId;index:CustomGraph_projectId_idx;index:CustomGraph_projectId_kind_idx;not null" json:"projectId"`
	Name        string         `gorm:"column:name;not null" json:"name"`
	Graph       datatypes.JSON `gorm:"column:graph;not null" json:"graph"`
	Filters     datatypes.JSON `gorm:"column:filters" json:"filters"`
	Kind        *string        `gorm:"column:kind;index:CustomGraph_projectId_kind_idx;default:'builder';not null" json:"kind"`
	CreatedAt   DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt   DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	DashboardID *string        `gorm:"column:dashboardId;index:CustomGraph_dashboardId_idx" json:"dashboardId"`
	GridColumn  int            `gorm:"column:gridColumn;default:0;not null" json:"gridColumn"`
	GridRow     int            `gorm:"column:gridRow;default:0;not null" json:"gridRow"`
	ColSpan     *int           `gorm:"column:colSpan;default:1;not null" json:"colSpan"`
	RowSpan     *int           `gorm:"column:rowSpan;default:1;not null" json:"rowSpan"`
	Project     *Project
	Dashboard   *Dashboard
	TriggerSent []TriggerSent `gorm:"foreignKey:CustomGraphID;constraint:OnDelete:CASCADE"`
	Trigger     *Trigger      `gorm:"foreignKey:CustomGraphID;constraint:OnDelete:CASCADE"`
}

func (CustomGraph) TableName() string {
	return "CustomGraph"
}

func (m *CustomGraph) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*CustomGraph) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type BatchEvaluation struct {
	ID           string         `gorm:"column:id;primaryKey" json:"id"`
	ExperimentID string         `gorm:"column:experimentId;index:BatchEvaluation_experimentId_idx;not null" json:"experimentId"`
	ProjectID    string         `gorm:"column:projectId;index:BatchEvaluation_projectId_idx;not null" json:"projectId"`
	Data         datatypes.JSON `gorm:"column:data;default:'{}';not null" json:"data"`
	Status       string         `gorm:"column:status;not null" json:"status"`
	Score        float64        `gorm:"column:score;not null" json:"score"`
	Label        *string        `gorm:"column:label" json:"label"`
	Passed       bool           `gorm:"column:passed;not null" json:"passed"`
	Details      string         `gorm:"column:details;not null" json:"details"`
	Cost         float64        `gorm:"column:cost;not null" json:"cost"`
	DatasetSlug  string         `gorm:"column:datasetSlug;not null" json:"datasetSlug"`
	DatasetID    string         `gorm:"column:datasetId;index:BatchEvaluation_datasetId_idx;not null" json:"datasetId"`
	Evaluation   string         `gorm:"column:evaluation;not null" json:"evaluation"`
	CreatedAt    DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt    DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Experiment   *Experiment
	Project      *Project
	Dataset      *Dataset
}

func (BatchEvaluation) TableName() string {
	return "BatchEvaluation"
}

func (m *BatchEvaluation) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*BatchEvaluation) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Trigger struct {
	ID                   string         `gorm:"column:id;primaryKey" json:"id"`
	Name                 string         `gorm:"column:name;not null" json:"name"`
	ProjectID            string         `gorm:"column:projectId;index:Trigger_projectId_idx;not null" json:"projectId"`
	Action               string         `gorm:"column:action;not null" json:"action"`
	TriggerKind          string         `gorm:"column:triggerKind;default:'AUTOMATION';not null" json:"triggerKind"`
	ActionParams         datatypes.JSON `gorm:"column:actionParams;not null" json:"actionParams"`
	Filters              datatypes.JSON `gorm:"column:filters;not null" json:"filters"`
	FilterQuery          *string        `gorm:"column:filterQuery" json:"filterQuery"`
	LastRunAt            float64        `gorm:"column:lastRunAt;default:0;not null" json:"lastRunAt"`
	CreatedAt            DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt            DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Active               *bool          `gorm:"column:active;default:true;not null" json:"active"`
	PausedReason         *string        `gorm:"column:pausedReason" json:"pausedReason"`
	PausedAt             *DateTime      `gorm:"column:pausedAt;type:timestamptz(3)" json:"pausedAt"`
	Message              *string        `gorm:"column:message" json:"message"`
	Deleted              bool           `gorm:"column:deleted;default:false;not null" json:"deleted"`
	AlertType            *string        `gorm:"column:alertType" json:"alertType"`
	SlackTemplateType    *string        `gorm:"column:slackTemplateType" json:"slackTemplateType"`
	SlackTemplate        *string        `gorm:"column:slackTemplate" json:"slackTemplate"`
	EmailSubjectTemplate *string        `gorm:"column:emailSubjectTemplate" json:"emailSubjectTemplate"`
	EmailBodyTemplate    *string        `gorm:"column:emailBodyTemplate" json:"emailBodyTemplate"`
	NotificationCadence  *string        `gorm:"column:notificationCadence;default:'immediate';not null" json:"notificationCadence"`
	TraceDebounceMs      *int           `gorm:"column:traceDebounceMs;default:30000;not null" json:"traceDebounceMs"`
	CustomGraphID        *string        `gorm:"column:customGraphId;uniqueIndex" json:"customGraphId"`
	Project              *Project
	CustomGraph          *CustomGraph
	TriggerSent          []TriggerSent             `gorm:"foreignKey:TriggerID"`
	WebhookDeliveries    []WebhookEndpointDelivery `gorm:"foreignKey:TriggerID;constraint:OnDelete:CASCADE"`
	LatestEvaluation     *TriggerLatestEvaluation  `gorm:"foreignKey:TriggerID;constraint:OnDelete:CASCADE"`
}

func (Trigger) TableName() string {
	return "Trigger"
}

func (m *Trigger) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*Trigger) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type SlackIntegration struct {
	ID                  string   `gorm:"column:id;primaryKey" json:"id"`
	Name                string   `gorm:"column:name;not null" json:"name"`
	Kind                string   `gorm:"column:kind;not null" json:"kind"`
	ScopeType           string   `gorm:"column:scopeType;uniqueIndex:SlackIntegration_organizationId_scopeType_scopeId_secretFingerprint_key;index:SlackIntegration_scopeType_scopeId_idx;not null" json:"scopeType"`
	ScopeID             string   `gorm:"column:scopeId;uniqueIndex:SlackIntegration_organizationId_scopeType_scopeId_secretFingerprint_key;index:SlackIntegration_scopeType_scopeId_idx;not null" json:"scopeId"`
	OrganizationID      string   `gorm:"column:organizationId;uniqueIndex:SlackIntegration_organizationId_scopeType_scopeId_secretFingerprint_key;not null" json:"organizationId"`
	BotTokenEncrypted   *string  `gorm:"column:botTokenEncrypted" json:"botTokenEncrypted"`
	WebhookURLEncrypted *string  `gorm:"column:webhookUrlEncrypted" json:"webhookUrlEncrypted"`
	SecretFingerprint   string   `gorm:"column:secretFingerprint;uniqueIndex:SlackIntegration_organizationId_scopeType_scopeId_secretFingerprint_key;not null" json:"secretFingerprint"`
	SecretHint          string   `gorm:"column:secretHint;not null" json:"secretHint"`
	SlackTeamID         *string  `gorm:"column:slackTeamId" json:"slackTeamId"`
	SlackTeamName       *string  `gorm:"column:slackTeamName" json:"slackTeamName"`
	CreatedByID         string   `gorm:"column:createdById;not null" json:"createdById"`
	UpdatedByID         string   `gorm:"column:updatedById;not null" json:"updatedById"`
	CreatedAt           DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt           DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
}

func (SlackIntegration) TableName() string {
	return "SlackIntegration"
}

func (m *SlackIntegration) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*SlackIntegration) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type SlackConnectionClaim struct {
	ConnectionID   string   `gorm:"column:connectionId;primaryKey" json:"connectionId"`
	ClaimantID     string   `gorm:"column:claimantId;primaryKey" json:"claimantId"`
	ClaimantLabel  string   `gorm:"column:claimantLabel;not null" json:"claimantLabel"`
	OrganizationID string   `gorm:"column:organizationId;index:slack_connection_claim_organizationId_idx;not null" json:"organizationId"`
	ProjectID      string   `gorm:"column:projectId;not null" json:"projectId"`
	CreatedAt      DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt      DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
}

func (SlackConnectionClaim) TableName() string {
	return "slack_connection_claim"
}

func (m *SlackConnectionClaim) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*SlackConnectionClaim) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type WebhookEndpoint struct {
	ID                          string    `gorm:"column:id;primaryKey" json:"id"`
	OrganizationID              string    `gorm:"column:organizationId;uniqueIndex:WebhookEndpoint_organizationId_idempotencyKey_key;index:WebhookEndpoint_organizationId_status_idx;not null" json:"organizationId"`
	DestinationKind             string    `gorm:"column:destinationKind;default:'http';not null" json:"destinationKind"`
	URL                         *string   `gorm:"column:url" json:"url"`
	SqsQueueURL                 *string   `gorm:"column:sqsQueueUrl" json:"sqsQueueUrl"`
	SqsRoleArn                  *string   `gorm:"column:sqsRoleArn" json:"sqsRoleArn"`
	SqsExternalID               *string   `gorm:"column:sqsExternalId" json:"sqsExternalId"`
	SqsAccessKeyID              *string   `gorm:"column:sqsAccessKeyId" json:"sqsAccessKeyId"`
	SqsSecretAccessKeyEncrypted *string   `gorm:"column:sqsSecretAccessKeyEncrypted" json:"sqsSecretAccessKeyEncrypted"`
	SecretEncrypted             string    `gorm:"column:secretEncrypted;not null" json:"secretEncrypted"`
	SignatureScheme             *string   `gorm:"column:signatureScheme" json:"signatureScheme"`
	IdempotencyKey              *string   `gorm:"column:idempotencyKey;uniqueIndex:WebhookEndpoint_organizationId_idempotencyKey_key" json:"idempotencyKey"`
	PreviousSecretEncrypted     *string   `gorm:"column:previousSecretEncrypted" json:"previousSecretEncrypted"`
	PreviousSecretExpiresAt     *DateTime `gorm:"column:previousSecretExpiresAt" json:"previousSecretExpiresAt"`
	EnabledEvents               []string  `gorm:"column:enabledEvents;serializer:json;not null" json:"enabledEvents"`
	Status                      string    `gorm:"column:status;index:WebhookEndpoint_organizationId_status_idx;default:'ACTIVE';not null" json:"status"`
	DisabledReason              *string   `gorm:"column:disabledReason" json:"disabledReason"`
	DisabledAt                  *DateTime `gorm:"column:disabledAt" json:"disabledAt"`
	FailingSince                *DateTime `gorm:"column:failingSince" json:"failingSince"`
	MaxBatchSize                *int      `gorm:"column:maxBatchSize;default:100;not null" json:"maxBatchSize"`
	MaxBatchDelayMs             *int      `gorm:"column:maxBatchDelayMs;default:250;not null" json:"maxBatchDelayMs"`
	MaxInFlight                 *int      `gorm:"column:maxInFlight;default:4;not null" json:"maxInFlight"`
	AllowSelfSignedCertificate  bool      `gorm:"column:allowSelfSignedCertificate;default:false;not null" json:"allowSelfSignedCertificate"`
	LastSuccessAt               *DateTime `gorm:"column:lastSuccessAt" json:"lastSuccessAt"`
	LastFailureAt               *DateTime `gorm:"column:lastFailureAt" json:"lastFailureAt"`
	ArchivedAt                  *DateTime `gorm:"column:archivedAt" json:"archivedAt"`
	CreatedAt                   DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt                   DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Organization                *Organization
	Deliveries                  []WebhookEndpointDelivery `gorm:"foreignKey:EndpointID;constraint:OnDelete:CASCADE"`
}

func (WebhookEndpoint) TableName() string {
	return "WebhookEndpoint"
}

func (m *WebhookEndpoint) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*WebhookEndpoint) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type WebhookEndpointDelivery struct {
	ID             string           `gorm:"column:id;primaryKey" json:"id"`
	Channel        string           `gorm:"column:channel;default:'platform';not null" json:"channel"`
	OrganizationID *string          `gorm:"column:organizationId;index:WebhookEndpointDelivery_organizationId_firedAt_idx" json:"organizationId"`
	EndpointID     *string          `gorm:"column:endpointId;index:WebhookEndpointDelivery_endpointId_firedAt_idx;index:WebhookEndpointDelivery_endpointId_dispatchId_idx" json:"endpointId"`
	ProjectID      *string          `gorm:"column:projectId;index:WebhookEndpointDelivery_projectId_triggerId_firedAt_idx;index:WebhookEndpointDelivery_projectId_triggerId_dispatchId_idx" json:"projectId"`
	TriggerID      *string          `gorm:"column:triggerId;index:WebhookEndpointDelivery_projectId_triggerId_firedAt_idx;index:WebhookEndpointDelivery_projectId_triggerId_dispatchId_idx" json:"triggerId"`
	DispatchID     string           `gorm:"column:dispatchId;index:WebhookEndpointDelivery_endpointId_dispatchId_idx;index:WebhookEndpointDelivery_projectId_triggerId_dispatchId_idx;not null" json:"dispatchId"`
	Attempt        *int             `gorm:"column:attempt" json:"attempt"`
	EventCount     *int             `gorm:"column:eventCount" json:"eventCount"`
	ResponseStatus *int             `gorm:"column:responseStatus" json:"responseStatus"`
	LatencyMs      *int             `gorm:"column:latencyMs" json:"latencyMs"`
	Error          *string          `gorm:"column:error" json:"error"`
	Response       datatypes.JSON   `gorm:"column:response" json:"response"`
	Outcome        string           `gorm:"column:outcome;not null" json:"outcome"`
	FiredAt        DateTime         `gorm:"column:firedAt;index:WebhookEndpointDelivery_endpointId_firedAt_idx;index:WebhookEndpointDelivery_organizationId_firedAt_idx;index:WebhookEndpointDelivery_firedAt_idx;index:WebhookEndpointDelivery_projectId_triggerId_firedAt_idx;not null" json:"firedAt"`
	CreatedAt      DateTime         `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	Endpoint       *WebhookEndpoint `gorm:"foreignKey:EndpointID"`
	Project        *Project
	Trigger        *Trigger
}

func (WebhookEndpointDelivery) TableName() string {
	return "WebhookEndpointDelivery"
}

func (m *WebhookEndpointDelivery) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.FiredAt.IsZero() {
		m.FiredAt = DateTime{Time: now}
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type Experiment struct {
	ID               string         `gorm:"column:id;primaryKey" json:"id"`
	Name             *string        `gorm:"column:name" json:"name"`
	Type             string         `gorm:"column:type;not null" json:"type"`
	Slug             string         `gorm:"column:slug;uniqueIndex:Experiment_projectId_slug_key;not null" json:"slug"`
	ProjectID        string         `gorm:"column:projectId;uniqueIndex:Experiment_projectId_slug_key;index:Experiment_projectId_idx;index:Experiment_projectId_archivedAt_idx;not null" json:"projectId"`
	WorkflowID       *string        `gorm:"column:workflowId;index:Experiment_workflowId_idx" json:"workflowId"`
	CreatedAt        DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt        DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	ArchivedAt       *DateTime      `gorm:"column:archivedAt;index:Experiment_projectId_archivedAt_idx" json:"archivedAt"`
	WorkbenchState   datatypes.JSON `gorm:"column:workbenchState" json:"workbenchState"`
	WorkbenchVersion int            `gorm:"column:workbenchVersion;default:0;not null" json:"workbenchVersion"`
	Project          *Project
	Workflow         *Workflow
	BatchEvaluations []BatchEvaluation `gorm:"foreignKey:ExperimentID"`
	Monitor          *Monitor          `gorm:"foreignKey:ExperimentID;constraint:OnUpdate:RESTRICT,OnDelete:RESTRICT"`
}

func (Experiment) TableName() string {
	return "Experiment"
}

func (m *Experiment) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*Experiment) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type ExperimentVersion struct {
	ID             string         `gorm:"column:id;primaryKey" json:"id"`
	ExperimentID   string         `gorm:"column:experimentId;uniqueIndex:ExperimentVersion_experimentId_version_key;index:ExperimentVersion_projectId_experimentId_autoSaved_idx;index:ExperimentVersion_projectId_experimentId_version_idx;index:ExperimentVersion_projectId_experimentId_counterVersion_idx;not null" json:"experimentId"`
	ProjectID      string         `gorm:"column:projectId;index:ExperimentVersion_projectId_experimentId_autoSaved_idx;index:ExperimentVersion_projectId_experimentId_version_idx;index:ExperimentVersion_projectId_experimentId_counterVersion_idx;not null" json:"projectId"`
	Version        int            `gorm:"column:version;uniqueIndex:ExperimentVersion_experimentId_version_key;index:ExperimentVersion_projectId_experimentId_version_idx;not null" json:"version"`
	CounterVersion int            `gorm:"column:counterVersion;index:ExperimentVersion_projectId_experimentId_counterVersion_idx;not null" json:"counterVersion"`
	AutoSaved      bool           `gorm:"column:autoSaved;index:ExperimentVersion_projectId_experimentId_autoSaved_idx;default:false;not null" json:"autoSaved"`
	CommitMessage  *string        `gorm:"column:commitMessage" json:"commitMessage"`
	AuthorID       *string        `gorm:"column:authorId" json:"authorId"`
	AuthorLabel    string         `gorm:"column:authorLabel;not null" json:"authorLabel"`
	RunID          *string        `gorm:"column:runId" json:"runId"`
	State          datatypes.JSON `gorm:"column:state;not null" json:"state"`
	SchemaVersion  *string        `gorm:"column:schemaVersion;default:'1';not null" json:"schemaVersion"`
	CreatedAt      DateTime       `gorm:"column:createdAt;index:ExperimentVersion_createdAt_idx;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt      DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
}

func (ExperimentVersion) TableName() string {
	return "ExperimentVersion"
}

func (m *ExperimentVersion) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*ExperimentVersion) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Annotation struct {
	ID             string         `gorm:"column:id;primaryKey" json:"id"`
	ProjectID      string         `gorm:"column:projectId;index:Annotation_projectId_idx;index:Annotation_projectId_traceId_idx;index:Annotation_projectId_traceId_anchorKind_idx;not null" json:"projectId"`
	TraceID        string         `gorm:"column:traceId;index:Annotation_traceId_idx;index:Annotation_projectId_traceId_idx;index:Annotation_projectId_traceId_anchorKind_idx;not null" json:"traceId"`
	Comment        *string        `gorm:"column:comment" json:"comment"`
	IsThumbsUp     *bool          `gorm:"column:isThumbsUp" json:"isThumbsUp"`
	UserID         *string        `gorm:"column:userId;index:Annotation_userId_idx" json:"userId"`
	CreatedAt      DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt      DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Email          *string        `gorm:"column:email" json:"email"`
	ScoreOptions   datatypes.JSON `gorm:"column:scoreOptions" json:"scoreOptions"`
	ExpectedOutput *string        `gorm:"column:expectedOutput" json:"expectedOutput"`
	AnchorKind     *string        `gorm:"column:anchorKind;index:Annotation_projectId_traceId_anchorKind_idx" json:"anchorKind"`
	AnchorID       *string        `gorm:"column:anchorId" json:"anchorId"`
	AnchorPath     *string        `gorm:"column:anchorPath" json:"anchorPath"`
	Project        *Project
	User           *User
}

func (Annotation) TableName() string {
	return "Annotation"
}

func (m *Annotation) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*Annotation) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type ModelProvider struct {
	ID                         string               `gorm:"column:id;primaryKey" json:"id"`
	Name                       string               `gorm:"column:name;not null" json:"name"`
	Provider                   string               `gorm:"column:provider;not null" json:"provider"`
	RoutingHandle              *string              `gorm:"column:routingHandle;uniqueIndex:ModelProvider_organizationId_routingHandle_key" json:"routingHandle"`
	Enabled                    bool                 `gorm:"column:enabled;not null" json:"enabled"`
	CustomKeys                 datatypes.JSON       `gorm:"column:customKeys" json:"customKeys"`
	ExtraHeaders               datatypes.JSON       `gorm:"column:extraHeaders" json:"extraHeaders"`
	CustomModels               datatypes.JSON       `gorm:"column:customModels" json:"customModels"`
	CustomEmbeddingsModels     datatypes.JSON       `gorm:"column:customEmbeddingsModels" json:"customEmbeddingsModels"`
	DeploymentMapping          datatypes.JSON       `gorm:"column:deploymentMapping" json:"deploymentMapping"`
	RateLimitRpm               *int                 `gorm:"column:rateLimitRpm" json:"rateLimitRpm"`
	RateLimitTpm               *int                 `gorm:"column:rateLimitTpm" json:"rateLimitTpm"`
	RateLimitRpd               *int                 `gorm:"column:rateLimitRpd" json:"rateLimitRpd"`
	RotationPolicy             string               `gorm:"column:rotationPolicy;default:'MANUAL';not null" json:"rotationPolicy"`
	ProviderConfig             datatypes.JSON       `gorm:"column:providerConfig" json:"providerConfig"`
	FallbackPriorityGlobal     *int                 `gorm:"column:fallbackPriorityGlobal;index:ModelProvider_fallbackPriorityGlobal_idx" json:"fallbackPriorityGlobal"`
	LangySkipPermissionsModels datatypes.JSON       `gorm:"column:langySkipPermissionsModels" json:"langySkipPermissionsModels"`
	HealthStatus               string               `gorm:"column:healthStatus;index:ModelProvider_healthStatus_idx;default:'UNKNOWN';not null" json:"healthStatus"`
	CircuitOpenedAt            *DateTime            `gorm:"column:circuitOpenedAt" json:"circuitOpenedAt"`
	LastHealthCheckAt          *DateTime            `gorm:"column:lastHealthCheckAt" json:"lastHealthCheckAt"`
	DisabledAt                 *DateTime            `gorm:"column:disabledAt" json:"disabledAt"`
	CreatedAt                  DateTime             `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt                  DateTime             `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	OrganizationID             string               `gorm:"column:organizationId;uniqueIndex:ModelProvider_organizationId_routingHandle_key;index:ModelProvider_organizationId_idx;not null" json:"organizationId"`
	Scopes                     []ModelProviderScope `gorm:"foreignKey:ModelProviderID;constraint:OnDelete:CASCADE"`
}

func (ModelProvider) TableName() string {
	return "ModelProvider"
}

func (m *ModelProvider) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*ModelProvider) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type ModelProviderScope struct {
	ID              string   `gorm:"column:id;primaryKey" json:"id"`
	ModelProviderID string   `gorm:"column:modelProviderId;uniqueIndex:ModelProviderScope_modelProviderId_scopeType_scopeId_key;index:ModelProviderScope_modelProviderId_idx;not null" json:"modelProviderId"`
	ScopeType       string   `gorm:"column:scopeType;uniqueIndex:ModelProviderScope_modelProviderId_scopeType_scopeId_key;index:ModelProviderScope_scopeType_scopeId_idx;not null" json:"scopeType"`
	ScopeID         string   `gorm:"column:scopeId;uniqueIndex:ModelProviderScope_modelProviderId_scopeType_scopeId_key;index:ModelProviderScope_scopeType_scopeId_idx;not null" json:"scopeId"`
	CreatedAt       DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	ModelProvider   *ModelProvider
}

func (ModelProviderScope) TableName() string {
	return "ModelProviderScope"
}

func (m *ModelProviderScope) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type ModelDefaultConfig struct {
	ID             string                    `gorm:"column:id;primaryKey" json:"id"`
	Config         datatypes.JSON            `gorm:"column:config;not null" json:"config"`
	AuthorID       *string                   `gorm:"column:authorId" json:"authorId"`
	CreatedAt      DateTime                  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt      DateTime                  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	OrganizationID string                    `gorm:"column:organizationId;index:ModelDefaultConfig_organizationId_idx;not null" json:"organizationId"`
	Scopes         []ModelDefaultConfigScope `gorm:"foreignKey:ConfigID;constraint:OnDelete:CASCADE"`
}

func (ModelDefaultConfig) TableName() string {
	return "ModelDefaultConfig"
}

func (m *ModelDefaultConfig) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*ModelDefaultConfig) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type ModelDefaultConfigScope struct {
	ID        string              `gorm:"column:id;primaryKey" json:"id"`
	ConfigID  string              `gorm:"column:configId;uniqueIndex:ModelDefaultConfigScope_configId_scopeType_scopeId_key;index:ModelDefaultConfigScope_configId_idx;not null" json:"configId"`
	ScopeType string              `gorm:"column:scopeType;uniqueIndex:ModelDefaultConfigScope_configId_scopeType_scopeId_key;index:ModelDefaultConfigScope_scopeType_scopeId_idx;not null" json:"scopeType"`
	ScopeID   string              `gorm:"column:scopeId;uniqueIndex:ModelDefaultConfigScope_configId_scopeType_scopeId_key;index:ModelDefaultConfigScope_scopeType_scopeId_idx;not null" json:"scopeId"`
	CreatedAt DateTime            `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	Config    *ModelDefaultConfig `gorm:"foreignKey:ConfigID"`
}

func (ModelDefaultConfigScope) TableName() string {
	return "ModelDefaultConfigScope"
}

func (m *ModelDefaultConfigScope) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type GithubInstallation struct {
	ID                  string         `gorm:"column:id;primaryKey" json:"id"`
	InstallationID      string         `gorm:"column:installationId;uniqueIndex;not null" json:"installationId"`
	OrganizationID      string         `gorm:"column:organizationId;index:GithubInstallation_organizationId_idx;not null" json:"organizationId"`
	AccountLogin        string         `gorm:"column:accountLogin;not null" json:"accountLogin"`
	AccountType         string         `gorm:"column:accountType;not null" json:"accountType"`
	AccountID           string         `gorm:"column:accountId;not null" json:"accountId"`
	RepositorySelection string         `gorm:"column:repositorySelection;not null" json:"repositorySelection"`
	Repositories        datatypes.JSON `gorm:"column:repositories" json:"repositories"`
	SuspendedAt         *DateTime      `gorm:"column:suspendedAt" json:"suspendedAt"`
	CreatedAt           DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt           DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Organization        *Organization
}

func (GithubInstallation) TableName() string {
	return "GithubInstallation"
}

func (m *GithubInstallation) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*GithubInstallation) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type GithubPullRequest struct {
	ID                 string    `gorm:"column:id;primaryKey" json:"id"`
	OrganizationID     string    `gorm:"column:organizationId;uniqueIndex:GithubPullRequest_organizationId_repositoryHost_repositoryFullName_prNumber_key;index:GithubPullRequest_organizationId_repositoryHost_repositoryFullName_headBranch_idx;not null" json:"organizationId"`
	RepositoryHost     string    `gorm:"column:repositoryHost;uniqueIndex:GithubPullRequest_organizationId_repositoryHost_repositoryFullName_prNumber_key;index:GithubPullRequest_organizationId_repositoryHost_repositoryFullName_headBranch_idx;not null" json:"repositoryHost"`
	RepositoryFullName string    `gorm:"column:repositoryFullName;uniqueIndex:GithubPullRequest_organizationId_repositoryHost_repositoryFullName_prNumber_key;index:GithubPullRequest_organizationId_repositoryHost_repositoryFullName_headBranch_idx;not null" json:"repositoryFullName"`
	HeadBranch         string    `gorm:"column:headBranch;index:GithubPullRequest_organizationId_repositoryHost_repositoryFullName_headBranch_idx;not null" json:"headBranch"`
	PrNumber           int       `gorm:"column:prNumber;uniqueIndex:GithubPullRequest_organizationId_repositoryHost_repositoryFullName_prNumber_key;not null" json:"prNumber"`
	HTMLURL            string    `gorm:"column:htmlUrl;not null" json:"htmlUrl"`
	Title              string    `gorm:"column:title;not null" json:"title"`
	State              string    `gorm:"column:state;not null" json:"state"`
	IsDraft            bool      `gorm:"column:isDraft;default:false;not null" json:"isDraft"`
	AuthorLogin        *string   `gorm:"column:authorLogin" json:"authorLogin"`
	PrCreatedAt        DateTime  `gorm:"column:prCreatedAt;not null" json:"prCreatedAt"`
	PrClosedAt         *DateTime `gorm:"column:prClosedAt" json:"prClosedAt"`
	PrMergedAt         *DateTime `gorm:"column:prMergedAt" json:"prMergedAt"`
	PrUpdatedAt        *DateTime `gorm:"column:prUpdatedAt" json:"prUpdatedAt"`
	MappedAt           DateTime  `gorm:"column:mappedAt;not null" json:"mappedAt"`
	LastCheckedAt      DateTime  `gorm:"column:lastCheckedAt;not null" json:"lastCheckedAt"`
	CreatedAt          DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt          DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Organization       *Organization
}

func (GithubPullRequest) TableName() string {
	return "GithubPullRequest"
}

func (m *GithubPullRequest) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.MappedAt.IsZero() {
		m.MappedAt = DateTime{Time: now}
	}
	if m.LastCheckedAt.IsZero() {
		m.LastCheckedAt = DateTime{Time: now}
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*GithubPullRequest) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type GithubBranchPullRequestCheck struct {
	ID                 string    `gorm:"column:id;primaryKey" json:"id"`
	OrganizationID     string    `gorm:"column:organizationId;uniqueIndex:GithubBranchPullRequestCheck_organizationId_repositoryHost_repositoryFullName_headBranch_key;not null" json:"organizationId"`
	RepositoryHost     string    `gorm:"column:repositoryHost;uniqueIndex:GithubBranchPullRequestCheck_organizationId_repositoryHost_repositoryFullName_headBranch_key;not null" json:"repositoryHost"`
	RepositoryFullName string    `gorm:"column:repositoryFullName;uniqueIndex:GithubBranchPullRequestCheck_organizationId_repositoryHost_repositoryFullName_headBranch_key;not null" json:"repositoryFullName"`
	HeadBranch         string    `gorm:"column:headBranch;uniqueIndex:GithubBranchPullRequestCheck_organizationId_repositoryHost_repositoryFullName_headBranch_key;not null" json:"headBranch"`
	LastCheckedAt      DateTime  `gorm:"column:lastCheckedAt;not null" json:"lastCheckedAt"`
	PrCount            int       `gorm:"column:prCount;default:0;not null" json:"prCount"`
	NotFoundAt         *DateTime `gorm:"column:notFoundAt;index:GithubBranchPullRequestCheck_recheckAfter_lastRequestedAt_notFoundAt_idx" json:"notFoundAt"`
	RecheckAfter       *DateTime `gorm:"column:recheckAfter;index:GithubBranchPullRequestCheck_recheckAfter_lastRequestedAt_notFoundAt_idx" json:"recheckAfter"`
	Attempts           int       `gorm:"column:attempts;default:0;not null" json:"attempts"`
	LastRequestedAt    DateTime  `gorm:"column:lastRequestedAt;index:GithubBranchPullRequestCheck_recheckAfter_lastRequestedAt_notFoundAt_idx;not null" json:"lastRequestedAt"`
	CreatedAt          DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt          DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Organization       *Organization
}

func (GithubBranchPullRequestCheck) TableName() string {
	return "GithubBranchPullRequestCheck"
}

func (m *GithubBranchPullRequestCheck) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*GithubBranchPullRequestCheck) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LangyConversationProjection struct {
	ID                   string   `gorm:"column:id;primaryKey" json:"id"`
	ProjectID            string   `gorm:"column:projectId;uniqueIndex:LangyConversationProjection_projectId_conversationId_key;index:LangyConversationProjection_projectId_userId_conversationId_idx;index:LangyConversationProjection_projectId_userId_lastActivityAt_idx;not null" json:"projectId"`
	ConversationID       string   `gorm:"column:conversationId;uniqueIndex:LangyConversationProjection_projectId_conversationId_key;index:LangyConversationProjection_projectId_userId_conversationId_idx;not null" json:"conversationId"`
	UserID               string   `gorm:"column:userId;index:LangyConversationProjection_projectId_userId_conversationId_idx;index:LangyConversationProjection_projectId_userId_lastActivityAt_idx;not null" json:"userId"`
	Title                *string  `gorm:"column:title" json:"title"`
	TitleSource          string   `gorm:"column:titleSource;not null" json:"titleSource"`
	Origin               *string  `gorm:"column:origin;default:'interactive';not null" json:"origin"`
	Status               string   `gorm:"column:status;not null" json:"status"`
	IsShared             bool     `gorm:"column:isShared;default:false;not null" json:"isShared"`
	SharedAt             *float64 `gorm:"column:sharedAt" json:"sharedAt"`
	SharedByID           *string  `gorm:"column:sharedById" json:"sharedById"`
	MessageCount         int      `gorm:"column:messageCount;default:0;not null" json:"messageCount"`
	LastActivityAt       *float64 `gorm:"column:lastActivityAt;index:LangyConversationProjection_projectId_userId_lastActivityAt_idx" json:"lastActivityAt"`
	CurrentTurnID        *string  `gorm:"column:currentTurnId" json:"currentTurnId"`
	LastError            *string  `gorm:"column:lastError" json:"lastError"`
	LastModel            *string  `gorm:"column:lastModel" json:"lastModel"`
	PendingHandoffToken  *string  `gorm:"column:pendingHandoffToken" json:"pendingHandoffToken"`
	PendingHandoffTurnID *string  `gorm:"column:pendingHandoffTurnId" json:"pendingHandoffTurnId"`
	RunToken             *string  `gorm:"column:runToken" json:"runToken"`
	ArchivedAt           *float64 `gorm:"column:archivedAt" json:"archivedAt"`
	CreatedAt            float64  `gorm:"column:createdAt;not null" json:"createdAt"`
	UpdatedAt            float64  `gorm:"column:updatedAt;not null" json:"updatedAt"`
	OccurredAt           float64  `gorm:"column:occurredAt;not null" json:"occurredAt"`
	AcceptedAt           float64  `gorm:"column:acceptedAt;not null" json:"acceptedAt"`
	LastEventID          string   `gorm:"column:lastEventId;not null" json:"lastEventId"`
	ProjectionVersion    string   `gorm:"column:projectionVersion;not null" json:"projectionVersion"`
}

func (LangyConversationProjection) TableName() string {
	return "LangyConversationProjection"
}

func (m *LangyConversationProjection) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	return nil
}

type LangyTurnRequest struct {
	ID             string   `gorm:"column:id;primaryKey" json:"id"`
	ProjectID      string   `gorm:"column:projectId;uniqueIndex:LangyTurnRequest_projectId_userId_requestId_key;index:LangyTurnRequest_projectId_conversationId_idx;not null" json:"projectId"`
	UserID         string   `gorm:"column:userId;uniqueIndex:LangyTurnRequest_projectId_userId_requestId_key;not null" json:"userId"`
	RequestID      string   `gorm:"column:requestId;uniqueIndex:LangyTurnRequest_projectId_userId_requestId_key;not null" json:"requestId"`
	ConversationID string   `gorm:"column:conversationId;index:LangyTurnRequest_projectId_conversationId_idx;not null" json:"conversationId"`
	TurnID         string   `gorm:"column:turnId;not null" json:"turnId"`
	Status         string   `gorm:"column:status;not null" json:"status"`
	LeaseOwner     string   `gorm:"column:leaseOwner;not null" json:"leaseOwner"`
	LeaseExpiresAt DateTime `gorm:"column:leaseExpiresAt;not null" json:"leaseExpiresAt"`
	CreatedAt      DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt      DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
}

func (LangyTurnRequest) TableName() string {
	return "LangyTurnRequest"
}

func (m *LangyTurnRequest) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*LangyTurnRequest) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LangyActiveTurn struct {
	ID             string   `gorm:"column:id;primaryKey" json:"id"`
	ProjectID      string   `gorm:"column:projectId;uniqueIndex:LangyActiveTurn_projectId_conversationId_key;index:LangyActiveTurn_projectId_userId_idx;not null" json:"projectId"`
	ConversationID string   `gorm:"column:conversationId;uniqueIndex:LangyActiveTurn_projectId_conversationId_key;not null" json:"conversationId"`
	TurnID         string   `gorm:"column:turnId;not null" json:"turnId"`
	RequestID      string   `gorm:"column:requestId;not null" json:"requestId"`
	UserID         string   `gorm:"column:userId;index:LangyActiveTurn_projectId_userId_idx;not null" json:"userId"`
	Status         string   `gorm:"column:status;not null" json:"status"`
	LeaseOwner     string   `gorm:"column:leaseOwner;not null" json:"leaseOwner"`
	LeaseExpiresAt DateTime `gorm:"column:leaseExpiresAt;not null" json:"leaseExpiresAt"`
	CreatedAt      DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt      DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
}

func (LangyActiveTurn) TableName() string {
	return "LangyActiveTurn"
}

func (m *LangyActiveTurn) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*LangyActiveTurn) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LangyConversationTurnProjection struct {
	ID                string         `gorm:"column:id;primaryKey" json:"id"`
	ProjectID         string         `gorm:"column:projectId;uniqueIndex:LangyConversationTurnProjection_projectId_conversationId_turnId_key;index:LangyConversationTurnProjection_projectId_conversationId_updatedAt_idx;not null" json:"projectId"`
	ConversationID    string         `gorm:"column:conversationId;uniqueIndex:LangyConversationTurnProjection_projectId_conversationId_turnId_key;index:LangyConversationTurnProjection_projectId_conversationId_updatedAt_idx;not null" json:"conversationId"`
	TurnID            string         `gorm:"column:turnId;uniqueIndex:LangyConversationTurnProjection_projectId_conversationId_turnId_key;not null" json:"turnId"`
	Status            string         `gorm:"column:status;not null" json:"status"`
	QuestionParts     datatypes.JSON `gorm:"column:questionParts;not null" json:"questionParts"`
	AnswerParts       datatypes.JSON `gorm:"column:answerParts;not null" json:"answerParts"`
	ToolCalls         datatypes.JSON `gorm:"column:toolCalls;not null" json:"toolCalls"`
	Plan              datatypes.JSON `gorm:"column:plan" json:"plan"`
	Error             *string        `gorm:"column:error" json:"error"`
	StartedAt         *float64       `gorm:"column:startedAt" json:"startedAt"`
	EndedAt           *float64       `gorm:"column:endedAt" json:"endedAt"`
	CreatedAt         float64        `gorm:"column:createdAt;not null" json:"createdAt"`
	UpdatedAt         float64        `gorm:"column:updatedAt;index:LangyConversationTurnProjection_projectId_conversationId_updatedAt_idx;not null" json:"updatedAt"`
	OccurredAt        float64        `gorm:"column:occurredAt;not null" json:"occurredAt"`
	AcceptedAt        float64        `gorm:"column:acceptedAt;not null" json:"acceptedAt"`
	LastEventID       string         `gorm:"column:lastEventId;not null" json:"lastEventId"`
	ProjectionVersion string         `gorm:"column:projectionVersion;not null" json:"projectionVersion"`
}

func (LangyConversationTurnProjection) TableName() string {
	return "LangyConversationTurnProjection"
}

func (m *LangyConversationTurnProjection) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	return nil
}

type LangyMessageProjection struct {
	ID             string         `gorm:"column:id;primaryKey" json:"id"`
	ProjectID      string         `gorm:"column:projectId;uniqueIndex:LangyMessageProjection_projectId_conversationId_messageId_key;uniqueIndex:LangyMessageProjection_projectId_sourceEventId_key;index:LangyMessageProjection_projectId_conversationId_createdAt_idx;not null" json:"projectId"`
	ConversationID string         `gorm:"column:conversationId;uniqueIndex:LangyMessageProjection_projectId_conversationId_messageId_key;index:LangyMessageProjection_projectId_conversationId_createdAt_idx;not null" json:"conversationId"`
	MessageID      string         `gorm:"column:messageId;uniqueIndex:LangyMessageProjection_projectId_conversationId_messageId_key;not null" json:"messageId"`
	Role           string         `gorm:"column:role;not null" json:"role"`
	Parts          datatypes.JSON `gorm:"column:parts;not null" json:"parts"`
	SourceEventID  string         `gorm:"column:sourceEventId;uniqueIndex:LangyMessageProjection_projectId_sourceEventId_key;not null" json:"sourceEventId"`
	OccurredAt     float64        `gorm:"column:occurredAt;not null" json:"occurredAt"`
	AcceptedAt     float64        `gorm:"column:acceptedAt;not null" json:"acceptedAt"`
	CreatedAt      float64        `gorm:"column:createdAt;index:LangyMessageProjection_projectId_conversationId_createdAt_idx;not null" json:"createdAt"`
	UpdatedAt      float64        `gorm:"column:updatedAt;not null" json:"updatedAt"`
}

func (LangyMessageProjection) TableName() string {
	return "LangyMessageProjection"
}

func (m *LangyMessageProjection) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	return nil
}

type ProjectSecret struct {
	ID             string   `gorm:"column:id;primaryKey" json:"id"`
	ProjectID      string   `gorm:"column:projectId;uniqueIndex:ProjectSecret_projectId_name_key;index:ProjectSecret_projectId_idx;not null" json:"projectId"`
	Name           string   `gorm:"column:name;uniqueIndex:ProjectSecret_projectId_name_key;not null" json:"name"`
	EncryptedValue string   `gorm:"column:encryptedValue;not null" json:"encryptedValue"`
	CreatedByID    string   `gorm:"column:createdById;not null" json:"createdById"`
	UpdatedByID    string   `gorm:"column:updatedById;not null" json:"updatedById"`
	CreatedAt      DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt      DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Project        *Project
	CreatedBy      *User `gorm:"foreignKey:CreatedByID"`
	UpdatedBy      *User `gorm:"foreignKey:UpdatedByID"`
}

func (ProjectSecret) TableName() string {
	return "ProjectSecret"
}

func (m *ProjectSecret) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*ProjectSecret) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type TriggerSent struct {
	ID              string    `gorm:"column:id;primaryKey;index:TriggerSent_projectId_triggerId_createdAt_id_idx" json:"id"`
	TriggerID       string    `gorm:"column:triggerId;uniqueIndex:TriggerSent_triggerId_traceId_key;index:TriggerSent_triggerId_idx;index:TriggerSent_projectId_triggerId_createdAt_id_idx;not null" json:"triggerId"`
	TraceID         *string   `gorm:"column:traceId;uniqueIndex:TriggerSent_triggerId_traceId_key" json:"traceId"`
	CustomGraphID   *string   `gorm:"column:customGraphId;index:TriggerSent_customGraphId_idx" json:"customGraphId"`
	ProjectID       string    `gorm:"column:projectId;index:TriggerSent_projectId_idx;index:TriggerSent_projectId_triggerId_createdAt_id_idx;not null" json:"projectId"`
	ResolvedAt      *DateTime `gorm:"column:resolvedAt;index:TriggerSent_resolvedAt_idx" json:"resolvedAt"`
	OpenIncidentKey *string   `gorm:"column:openIncidentKey;uniqueIndex" json:"openIncidentKey"`
	CreatedAt       DateTime  `gorm:"column:createdAt;index:TriggerSent_projectId_triggerId_createdAt_id_idx;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt       DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	CustomGraph     *CustomGraph
	Project         *Project
	Trigger         *Trigger
}

func (TriggerSent) TableName() string {
	return "TriggerSent"
}

func (m *TriggerSent) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*TriggerSent) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type TriggerLatestEvaluation struct {
	TriggerID         string   `gorm:"column:triggerId;primaryKey" json:"triggerId"`
	ProjectID         string   `gorm:"column:projectId;index:TriggerLatestEvaluation_projectId_idx;not null" json:"projectId"`
	EvaluatedAt       DateTime `gorm:"column:evaluatedAt;type:timestamptz(3);not null" json:"evaluatedAt"`
	Verdict           string   `gorm:"column:verdict;not null" json:"verdict"`
	ObservedValue     *float64 `gorm:"column:observedValue" json:"observedValue"`
	Threshold         *float64 `gorm:"column:threshold" json:"threshold"`
	Operator          *string  `gorm:"column:operator" json:"operator"`
	TimePeriodMinutes *int     `gorm:"column:timePeriodMinutes" json:"timePeriodMinutes"`
	SkipCode          *string  `gorm:"column:skipCode" json:"skipCode"`
	UpdatedAt         DateTime `gorm:"column:updatedAt;type:timestamptz(3);autoUpdateTime:false;not null" json:"updatedAt"`
	Trigger           *Trigger
}

func (TriggerLatestEvaluation) TableName() string {
	return "TriggerLatestEvaluation"
}

func (m *TriggerLatestEvaluation) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*TriggerLatestEvaluation) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type EmailSuppression struct {
	ID        string   `gorm:"column:id;primaryKey" json:"id"`
	ProjectID string   `gorm:"column:projectId;uniqueIndex:EmailSuppression_projectId_email_triggerId_key;index:EmailSuppression_projectId_idx;not null" json:"projectId"`
	Email     string   `gorm:"column:email;uniqueIndex:EmailSuppression_projectId_email_triggerId_key;not null" json:"email"`
	TriggerID *string  `gorm:"column:triggerId;uniqueIndex:EmailSuppression_projectId_email_triggerId_key" json:"triggerId"`
	Reason    *string  `gorm:"column:reason;default:'unsubscribe';not null" json:"reason"`
	CreatedAt DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	Project   *Project
}

func (EmailSuppression) TableName() string {
	return "EmailSuppression"
}

func (m *EmailSuppression) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type AnnotationScore struct {
	ID                    string         `gorm:"column:id;primaryKey" json:"id"`
	Name                  string         `gorm:"column:name;not null" json:"name"`
	ProjectID             string         `gorm:"column:projectId;index:AnnotationScore_projectId_idx;not null" json:"projectId"`
	CreatedAt             DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt             DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	DeletedAt             *DateTime      `gorm:"column:deletedAt" json:"deletedAt"`
	Description           *string        `gorm:"column:description" json:"description"`
	Active                *bool          `gorm:"column:active;default:true;not null" json:"active"`
	DataType              *string        `gorm:"column:dataType" json:"dataType"`
	Options               datatypes.JSON `gorm:"column:options" json:"options"`
	DefaultValue          datatypes.JSON `gorm:"column:defaultValue" json:"defaultValue"`
	Global                bool           `gorm:"column:global;default:false;not null" json:"global"`
	Project               *Project
	AnnotationQueueScores []AnnotationQueueScores `gorm:"foreignKey:AnnotationScoreID"`
}

func (AnnotationScore) TableName() string {
	return "AnnotationScore"
}

func (m *AnnotationScore) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*AnnotationScore) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type AnnotationQueue struct {
	ID                    string   `gorm:"column:id;primaryKey" json:"id"`
	Name                  string   `gorm:"column:name;not null" json:"name"`
	Slug                  string   `gorm:"column:slug;uniqueIndex:AnnotationQueue_projectId_slug_key;not null" json:"slug"`
	ProjectID             string   `gorm:"column:projectId;uniqueIndex:AnnotationQueue_projectId_slug_key;index:AnnotationQueue_projectId_idx;not null" json:"projectId"`
	Description           *string  `gorm:"column:description" json:"description"`
	CreatedAt             DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt             DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Project               *Project
	Members               []AnnotationQueueMembers `gorm:"foreignKey:AnnotationQueueID"`
	AnnotationQueueScores []AnnotationQueueScores  `gorm:"foreignKey:AnnotationQueueID"`
	AnnotationQueueItems  []AnnotationQueueItem    `gorm:"foreignKey:AnnotationQueueID"`
}

func (AnnotationQueue) TableName() string {
	return "AnnotationQueue"
}

func (m *AnnotationQueue) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*AnnotationQueue) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type AnnotationQueueMembers struct {
	AnnotationQueueID string `gorm:"column:annotationQueueId;primaryKey;index:AnnotationQueueMembers_annotationQueueId_idx" json:"annotationQueueId"`
	UserID            string `gorm:"column:userId;primaryKey;index:AnnotationQueueMembers_userId_idx" json:"userId"`
	User              *User
	AnnotationQueue   *AnnotationQueue
}

func (AnnotationQueueMembers) TableName() string {
	return "AnnotationQueueMembers"
}

type AnnotationQueueScores struct {
	AnnotationQueueID string `gorm:"column:annotationQueueId;primaryKey;index:AnnotationQueueScores_annotationQueueId_idx" json:"annotationQueueId"`
	AnnotationScoreID string `gorm:"column:annotationScoreId;primaryKey;index:AnnotationQueueScores_annotationScoreId_idx" json:"annotationScoreId"`
	AnnotationScore   *AnnotationScore
	AnnotationQueue   *AnnotationQueue
}

func (AnnotationQueueScores) TableName() string {
	return "AnnotationQueueScores"
}

type AnnotationQueueItem struct {
	ID                 string    `gorm:"column:id;primaryKey" json:"id"`
	AnnotationQueueID  *string   `gorm:"column:annotationQueueId;uniqueIndex:AnnotationQueueItem_traceId_annotationQueueId_projectId_key;index:AnnotationQueueItem_annotationQueueId_idx;index:AnnotationQueueItem_annotationQueueId_doneAt_idx" json:"annotationQueueId"`
	UserID             *string   `gorm:"column:userId;uniqueIndex:AnnotationQueueItem_traceId_userId_projectId_key;index:AnnotationQueueItem_userId_idx;index:AnnotationQueueItem_projectId_userId_doneAt_idx" json:"userId"`
	CreatedAt          DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt          DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	CreatedByUserID    *string   `gorm:"column:createdByUserId" json:"createdByUserId"`
	TraceID            string    `gorm:"column:traceId;uniqueIndex:AnnotationQueueItem_traceId_userId_projectId_key;uniqueIndex:AnnotationQueueItem_traceId_annotationQueueId_projectId_key;not null" json:"traceId"`
	ProjectID          string    `gorm:"column:projectId;uniqueIndex:AnnotationQueueItem_traceId_userId_projectId_key;uniqueIndex:AnnotationQueueItem_traceId_annotationQueueId_projectId_key;index:AnnotationQueueItem_projectId_idx;index:AnnotationQueueItem_projectId_doneAt_idx;index:AnnotationQueueItem_projectId_userId_doneAt_idx;not null" json:"projectId"`
	DoneAt             *DateTime `gorm:"column:doneAt;index:AnnotationQueueItem_projectId_doneAt_idx;index:AnnotationQueueItem_projectId_userId_doneAt_idx;index:AnnotationQueueItem_annotationQueueId_doneAt_idx" json:"doneAt"`
	MarkedForDatasetAt *DateTime `gorm:"column:markedForDatasetAt" json:"markedForDatasetAt"`
	AnnotationQueue    *AnnotationQueue
	User               *User `gorm:"foreignKey:UserID"`
	CreatedByUser      *User `gorm:"foreignKey:CreatedByUserID"`
	Project            *Project
}

func (AnnotationQueueItem) TableName() string {
	return "AnnotationQueueItem"
}

func (m *AnnotationQueueItem) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*AnnotationQueueItem) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type ShareLink struct {
	ID           string    `gorm:"column:id;primaryKey" json:"id"`
	Token        string    `gorm:"column:token;uniqueIndex;not null" json:"token"`
	ResourceType string    `gorm:"column:resourceType;index:ShareLink_projectId_resourceType_resourceId_idx;not null" json:"resourceType"`
	ResourceID   string    `gorm:"column:resourceId;index:ShareLink_projectId_resourceType_resourceId_idx;not null" json:"resourceId"`
	ThreadID     *string   `gorm:"column:threadId" json:"threadId"`
	ProjectID    string    `gorm:"column:projectId;index:ShareLink_projectId_resourceType_resourceId_idx;not null" json:"projectId"`
	UserID       *string   `gorm:"column:userId;index:ShareLink_userId_idx" json:"userId"`
	Visibility   string    `gorm:"column:visibility;default:'PUBLIC';not null" json:"visibility"`
	ExpiresAt    *DateTime `gorm:"column:expiresAt" json:"expiresAt"`
	MaxViews     *int      `gorm:"column:maxViews" json:"maxViews"`
	ViewCount    int       `gorm:"column:viewCount;default:0;not null" json:"viewCount"`
	CreatedAt    DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt    DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Project      *Project
	User         *User
}

func (ShareLink) TableName() string {
	return "ShareLink"
}

func (m *ShareLink) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*ShareLink) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type PinnedTrace struct {
	ID        string   `gorm:"column:id;primaryKey" json:"id"`
	ProjectID string   `gorm:"column:projectId;uniqueIndex:PinnedTrace_projectId_traceId_key;index:PinnedTrace_projectId_idx;not null" json:"projectId"`
	TraceID   string   `gorm:"column:traceId;uniqueIndex:PinnedTrace_projectId_traceId_key;not null" json:"traceId"`
	UserID    *string  `gorm:"column:userId" json:"userId"`
	Source    string   `gorm:"column:source;default:'manual';not null" json:"source"`
	Reason    *string  `gorm:"column:reason" json:"reason"`
	CreatedAt DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	Project   *Project
	User      *User
}

func (PinnedTrace) TableName() string {
	return "PinnedTrace"
}

func (m *PinnedTrace) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type TraceEditOverlay struct {
	ID          string         `gorm:"column:id;primaryKey" json:"id"`
	ProjectID   string         `gorm:"column:projectId;uniqueIndex:TraceEditOverlay_projectId_traceId_key;index:TraceEditOverlay_projectId_idx;not null" json:"projectId"`
	TraceID     string         `gorm:"column:traceId;uniqueIndex:TraceEditOverlay_projectId_traceId_key;not null" json:"traceId"`
	Patch       datatypes.JSON `gorm:"column:patch;not null" json:"patch"`
	CreatedByID *string        `gorm:"column:createdById" json:"createdById"`
	UpdatedByID *string        `gorm:"column:updatedById" json:"updatedById"`
	CreatedAt   DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt   DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Project     *Project
	CreatedBy   *User `gorm:"foreignKey:CreatedByID"`
	UpdatedBy   *User `gorm:"foreignKey:UpdatedByID"`
}

func (TraceEditOverlay) TableName() string {
	return "TraceEditOverlay"
}

func (m *TraceEditOverlay) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*TraceEditOverlay) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type TraceIngestSourceBilling struct {
	OrganizationID string   `gorm:"column:organizationId;primaryKey" json:"organizationId"`
	SourceType     string   `gorm:"column:sourceType;primaryKey" json:"sourceType"`
	Billed         bool     `gorm:"column:billed;not null" json:"billed"`
	RecordedAt     DateTime `gorm:"column:recordedAt;not null" json:"recordedAt"`
	UpdatedAt      DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
}

func (TraceIngestSourceBilling) TableName() string {
	return "TraceIngestSourceBilling"
}

func (m *TraceIngestSourceBilling) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*TraceIngestSourceBilling) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type RetentionPolicy struct {
	ID             string   `gorm:"column:id;primaryKey" json:"id"`
	OrganizationID string   `gorm:"column:organizationId;index:RetentionPolicy_organizationId_idx;not null" json:"organizationId"`
	ScopeType      string   `gorm:"column:scopeType;uniqueIndex:RetentionPolicy_scopeType_scopeId_category_key;index:RetentionPolicy_scopeType_scopeId_idx;not null" json:"scopeType"`
	ScopeID        string   `gorm:"column:scopeId;uniqueIndex:RetentionPolicy_scopeType_scopeId_category_key;index:RetentionPolicy_scopeType_scopeId_idx;not null" json:"scopeId"`
	Category       string   `gorm:"column:category;uniqueIndex:RetentionPolicy_scopeType_scopeId_category_key;not null" json:"category"`
	RetentionDays  int      `gorm:"column:retentionDays;not null" json:"retentionDays"`
	CreatedAt      DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt      DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
}

func (RetentionPolicy) TableName() string {
	return "RetentionPolicy"
}

func (m *RetentionPolicy) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*RetentionPolicy) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type DataPrivacyPolicy struct {
	ID             string         `gorm:"column:id;primaryKey" json:"id"`
	OrganizationID string         `gorm:"column:organizationId;index:DataPrivacyPolicy_organizationId_idx;not null" json:"organizationId"`
	ScopeType      string         `gorm:"column:scopeType;uniqueIndex:DataPrivacyPolicy_scopeType_scopeId_personalOnly_key;index:DataPrivacyPolicy_scopeType_scopeId_idx;not null" json:"scopeType"`
	ScopeID        string         `gorm:"column:scopeId;uniqueIndex:DataPrivacyPolicy_scopeType_scopeId_personalOnly_key;index:DataPrivacyPolicy_scopeType_scopeId_idx;not null" json:"scopeId"`
	PersonalOnly   bool           `gorm:"column:personalOnly;uniqueIndex:DataPrivacyPolicy_scopeType_scopeId_personalOnly_key;default:false;not null" json:"personalOnly"`
	Config         datatypes.JSON `gorm:"column:config;not null" json:"config"`
	CreatedAt      DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt      DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
}

func (DataPrivacyPolicy) TableName() string {
	return "DataPrivacyPolicy"
}

func (m *DataPrivacyPolicy) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*DataPrivacyPolicy) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type InstantEvalJudgeProject struct {
	ProjectID      string   `gorm:"column:projectId;primaryKey" json:"projectId"`
	OrganizationID string   `gorm:"column:organizationId;not null" json:"organizationId"`
	CreatedAt      DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
}

func (InstantEvalJudgeProject) TableName() string {
	return "InstantEvalJudgeProject"
}

type InstantEvalJudgeUsageBilling struct {
	OrganizationID string   `gorm:"column:organizationId;primaryKey" json:"organizationId"`
	UsageBilled    bool     `gorm:"column:usageBilled;not null" json:"usageBilled"`
	OccurredAt     DateTime `gorm:"column:occurredAt;not null" json:"occurredAt"`
	FromCatchUp    bool     `gorm:"column:fromCatchUp;not null" json:"fromCatchUp"`
}

func (InstantEvalJudgeUsageBilling) TableName() string {
	return "InstantEvalJudgeUsageBilling"
}

type InstantEvalJudgeSpend struct {
	OrganizationID string   `gorm:"column:organizationId;primaryKey" json:"organizationId"`
	RequestID      string   `gorm:"column:requestId;primaryKey" json:"requestId"`
	SpendNanoUsd   int64    `gorm:"column:spendNanoUsd;not null" json:"spendNanoUsd"`
	OccurredAt     DateTime `gorm:"column:occurredAt;not null" json:"occurredAt"`
}

func (InstantEvalJudgeSpend) TableName() string {
	return "InstantEvalJudgeSpend"
}

type CustomLLMModelCost struct {
	ID                          string   `gorm:"column:id;primaryKey" json:"id"`
	OrganizationID              string   `gorm:"column:organizationId;index:CustomLLMModelCost_organizationId_idx;not null" json:"organizationId"`
	ScopeType                   string   `gorm:"column:scopeType;index:CustomLLMModelCost_scopeType_scopeId_idx;default:'PROJECT';not null" json:"scopeType"`
	ScopeID                     string   `gorm:"column:scopeId;index:CustomLLMModelCost_scopeType_scopeId_idx;not null" json:"scopeId"`
	ProjectID                   *string  `gorm:"column:projectId;index:CustomLLMModelCost_projectId_idx" json:"projectId"`
	Model                       string   `gorm:"column:model;not null" json:"model"`
	Regex                       string   `gorm:"column:regex;not null" json:"regex"`
	InputCostPerToken           *float64 `gorm:"column:inputCostPerToken" json:"inputCostPerToken"`
	OutputCostPerToken          *float64 `gorm:"column:outputCostPerToken" json:"outputCostPerToken"`
	CacheReadCostPerToken       *float64 `gorm:"column:cacheReadCostPerToken" json:"cacheReadCostPerToken"`
	CacheCreationCostPerToken   *float64 `gorm:"column:cacheCreationCostPerToken" json:"cacheCreationCostPerToken"`
	CacheCreation1hCostPerToken *float64 `gorm:"column:cacheCreation1hCostPerToken" json:"cacheCreation1hCostPerToken"`
	CreatedAt                   DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt                   DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
}

func (CustomLLMModelCost) TableName() string {
	return "CustomLLMModelCost"
}

func (m *CustomLLMModelCost) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*CustomLLMModelCost) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Workflow struct {
	ID                   string    `gorm:"column:id;primaryKey" json:"id"`
	ProjectID            string    `gorm:"column:projectId;index:Workflow_projectId_idx;not null" json:"projectId"`
	Name                 string    `gorm:"column:name;not null" json:"name"`
	Icon                 string    `gorm:"column:icon;not null" json:"icon"`
	Description          string    `gorm:"column:description;not null" json:"description"`
	CreatedAt            DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt            DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	LatestVersionID      *string   `gorm:"column:latestVersionId;index:Workflow_latestVersionId_idx" json:"latestVersionId"`
	CurrentVersionID     *string   `gorm:"column:currentVersionId;index:Workflow_currentVersionId_idx" json:"currentVersionId"`
	PublishedID          *string   `gorm:"column:publishedId" json:"publishedId"`
	PublishedByID        *string   `gorm:"column:publishedById;index:Workflow_publishedById_idx" json:"publishedById"`
	ArchivedAt           *DateTime `gorm:"column:archivedAt" json:"archivedAt"`
	IsEvaluator          bool      `gorm:"column:isEvaluator;default:false;not null" json:"isEvaluator"`
	IsComponent          bool      `gorm:"column:isComponent;default:false;not null" json:"isComponent"`
	CopiedFromWorkflowID *string   `gorm:"column:copiedFromWorkflowId;index:Workflow_copiedFromWorkflowId_idx" json:"copiedFromWorkflowId"`
	Project              *Project
	LatestVersion        *WorkflowVersion  `gorm:"foreignKey:LatestVersionID"`
	CurrentVersion       *WorkflowVersion  `gorm:"foreignKey:CurrentVersionID"`
	PublishedBy          *User             `gorm:"foreignKey:PublishedByID"`
	CopiedFrom           *Workflow         `gorm:"foreignKey:CopiedFromWorkflowID"`
	Versions             []WorkflowVersion `gorm:"foreignKey:WorkflowID"`
	Experiments          []Experiment      `gorm:"foreignKey:WorkflowID"`
	CopiedWorkflows      []Workflow        `gorm:"foreignKey:CopiedFromWorkflowID;constraint:OnUpdate:RESTRICT,OnDelete:RESTRICT"`
	Agents               []Agent           `gorm:"foreignKey:WorkflowID"`
	Evaluators           []Evaluator       `gorm:"foreignKey:WorkflowID"`
}

func (Workflow) TableName() string {
	return "Workflow"
}

func (m *Workflow) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*Workflow) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type WorkflowVersion struct {
	ID                       string         `gorm:"column:id;primaryKey" json:"id"`
	Version                  string         `gorm:"column:version;uniqueIndex:WorkflowVersion_workflowId_version_key;not null" json:"version"`
	CommitMessage            string         `gorm:"column:commitMessage;not null" json:"commitMessage"`
	AuthorID                 string         `gorm:"column:authorId;index:WorkflowVersion_authorId_idx;not null" json:"authorId"`
	ProjectID                string         `gorm:"column:projectId;index:WorkflowVersion_projectId_idx;not null" json:"projectId"`
	WorkflowID               string         `gorm:"column:workflowId;uniqueIndex:WorkflowVersion_workflowId_version_key;index:WorkflowVersion_workflowId_idx;not null" json:"workflowId"`
	ParentID                 *string        `gorm:"column:parentId;index:WorkflowVersion_parentId_idx" json:"parentId"`
	AutoSaved                bool           `gorm:"column:autoSaved;default:false;not null" json:"autoSaved"`
	CreatedAt                DateTime       `gorm:"column:createdAt;index:WorkflowVersion_createdAt_idx;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt                DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Dsl                      datatypes.JSON `gorm:"column:dsl;not null" json:"dsl"`
	Author                   *User          `gorm:"foreignKey:AuthorID"`
	Project                  *Project
	Workflow                 *Workflow
	Parent                   *WorkflowVersion  `gorm:"foreignKey:ParentID"`
	Children                 []WorkflowVersion `gorm:"foreignKey:ParentID;constraint:OnUpdate:RESTRICT,OnDelete:RESTRICT"`
	WorkflowAsLatestVersion  []Workflow        `gorm:"foreignKey:LatestVersionID;constraint:OnUpdate:RESTRICT,OnDelete:RESTRICT"`
	WorkflowAsCurrentVersion []Workflow        `gorm:"foreignKey:CurrentVersionID;constraint:OnUpdate:RESTRICT,OnDelete:RESTRICT"`
}

func (WorkflowVersion) TableName() string {
	return "WorkflowVersion"
}

func (m *WorkflowVersion) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*WorkflowVersion) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type AuditLog struct {
	ID             string         `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt      DateTime       `gorm:"column:createdAt;index:AuditLog_createdAt_idx;index:AuditLog_organizationId_createdAt_idx;autoCreateTime:false;not null" json:"createdAt"`
	UserID         *string        `gorm:"column:userId;index:AuditLog_userId_idx" json:"userId"`
	ProjectID      *string        `gorm:"column:projectId;index:AuditLog_projectId_idx" json:"projectId"`
	OrganizationID *string        `gorm:"column:organizationId;index:AuditLog_organizationId_createdAt_idx" json:"organizationId"`
	Action         string         `gorm:"column:action;index:AuditLog_action_idx;not null" json:"action"`
	Args           datatypes.JSON `gorm:"column:args" json:"args"`
	Error          *string        `gorm:"column:error" json:"error"`
	IPAddress      *string        `gorm:"column:ipAddress;index:AuditLog_ipAddress_idx" json:"ipAddress"`
	UserAgent      *string        `gorm:"column:userAgent" json:"userAgent"`
	Metadata       datatypes.JSON `gorm:"column:metadata" json:"metadata"`
	ActorUserID    *string        `gorm:"column:actorUserId;index:AuditLog_actorUserId_idx" json:"actorUserId"`
	TargetKind     *string        `gorm:"column:targetKind;index:AuditLog_targetKind_targetId_idx" json:"targetKind"`
	TargetID       *string        `gorm:"column:targetId;index:AuditLog_targetKind_targetId_idx" json:"targetId"`
	Before         datatypes.JSON `gorm:"column:before" json:"before"`
	After          datatypes.JSON `gorm:"column:after" json:"after"`
	IdempotencyKey *string        `gorm:"column:idempotencyKey;uniqueIndex" json:"idempotencyKey"`
}

func (AuditLog) TableName() string {
	return "AuditLog"
}

func (m *AuditLog) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type LlmPromptConfig struct {
	ID                 string    `gorm:"column:id;primaryKey" json:"id"`
	Handle             *string   `gorm:"column:handle;uniqueIndex" json:"handle"`
	Name               string    `gorm:"column:name;not null" json:"name"`
	ProjectID          string    `gorm:"column:projectId;index:LlmPromptConfig_projectId_idx;not null" json:"projectId"`
	CreatedAt          DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt          DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	DeletedAt          *DateTime `gorm:"column:deletedAt" json:"deletedAt"`
	OrganizationID     string    `gorm:"column:organizationId;index:LlmPromptConfig_organizationId_scope_idx;not null" json:"organizationId"`
	Scope              string    `gorm:"column:scope;index:LlmPromptConfig_organizationId_scope_idx;default:'PROJECT';not null" json:"scope"`
	CopiedFromPromptID *string   `gorm:"column:copiedFromPromptId;index:LlmPromptConfig_copiedFromPromptId_idx" json:"copiedFromPromptId"`
	Project            *Project
	CopiedFrom         *LlmPromptConfig         `gorm:"foreignKey:CopiedFromPromptID"`
	CopiedPrompts      []LlmPromptConfig        `gorm:"foreignKey:CopiedFromPromptID;constraint:OnUpdate:RESTRICT,OnDelete:RESTRICT"`
	Versions           []LlmPromptConfigVersion `gorm:"foreignKey:ConfigID;constraint:OnDelete:CASCADE"`
	VersionTags        []PromptTagAssignment    `gorm:"foreignKey:ConfigID;constraint:OnDelete:CASCADE"`
}

func (LlmPromptConfig) TableName() string {
	return "LlmPromptConfig"
}

func (m *LlmPromptConfig) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*LlmPromptConfig) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LlmPromptConfigVersion struct {
	ID                string                `gorm:"column:id;primaryKey" json:"id"`
	Version           int                   `gorm:"column:version;uniqueIndex:LlmPromptConfigVersion_configId_version_key;not null" json:"version"`
	CommitMessage     *string               `gorm:"column:commitMessage" json:"commitMessage"`
	AuthorID          *string               `gorm:"column:authorId;index:LlmPromptConfigVersion_authorId_idx" json:"authorId"`
	ConfigID          string                `gorm:"column:configId;uniqueIndex:LlmPromptConfigVersion_configId_version_key;index:LlmPromptConfigVersion_configId_idx;not null" json:"configId"`
	ConfigData        datatypes.JSON        `gorm:"column:config;not null" json:"config"`
	SchemaVersion     string                `gorm:"column:schemaVersion;not null" json:"schemaVersion"`
	RuntimeParameters datatypes.JSON        `gorm:"column:runtimeParameters;default:'{}';not null" json:"runtimeParameters"`
	CreatedAt         DateTime              `gorm:"column:createdAt;index:LlmPromptConfigVersion_createdAt_idx;autoCreateTime:false;not null" json:"createdAt"`
	ProjectID         string                `gorm:"column:projectId;not null" json:"projectId"`
	Author            *User                 `gorm:"foreignKey:AuthorID"`
	Config            *LlmPromptConfig      `gorm:"foreignKey:ConfigID"`
	VersionTags       []PromptTagAssignment `gorm:"foreignKey:VersionID;constraint:OnDelete:CASCADE"`
}

func (LlmPromptConfigVersion) TableName() string {
	return "LlmPromptConfigVersion"
}

func (m *LlmPromptConfigVersion) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type PromptTagAssignment struct {
	ID          string                  `gorm:"column:id;primaryKey" json:"id"`
	ConfigID    string                  `gorm:"column:configId;uniqueIndex:PromptTagAssignment_configId_tagId_key;index:PromptTagAssignment_configId_idx;not null" json:"configId"`
	VersionID   string                  `gorm:"column:versionId;index:PromptTagAssignment_versionId_idx;not null" json:"versionId"`
	TagID       string                  `gorm:"column:tagId;uniqueIndex:PromptTagAssignment_configId_tagId_key;index:PromptTagAssignment_tagId_idx;not null" json:"tagId"`
	ProjectID   string                  `gorm:"column:projectId;index:PromptTagAssignment_projectId_idx;not null" json:"projectId"`
	CreatedAt   DateTime                `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt   DateTime                `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	CreatedByID *string                 `gorm:"column:createdById" json:"createdById"`
	UpdatedByID *string                 `gorm:"column:updatedById" json:"updatedById"`
	Config      *LlmPromptConfig        `gorm:"foreignKey:ConfigID"`
	Version     *LlmPromptConfigVersion `gorm:"foreignKey:VersionID"`
	PromptTag   *PromptTag              `gorm:"foreignKey:TagID"`
	CreatedBy   *User                   `gorm:"foreignKey:CreatedByID"`
	UpdatedBy   *User                   `gorm:"foreignKey:UpdatedByID"`
}

func (PromptTagAssignment) TableName() string {
	return "PromptTagAssignment"
}

func (m *PromptTagAssignment) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*PromptTagAssignment) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type PromptTag struct {
	ID             string   `gorm:"column:id;primaryKey" json:"id"`
	OrganizationID string   `gorm:"column:organizationId;uniqueIndex:PromptTag_organizationId_name_key;index:PromptTag_organizationId_idx;not null" json:"organizationId"`
	Name           string   `gorm:"column:name;uniqueIndex:PromptTag_organizationId_name_key;not null" json:"name"`
	CreatedAt      DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt      DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	CreatedByID    *string  `gorm:"column:createdById" json:"createdById"`
	UpdatedByID    *string  `gorm:"column:updatedById" json:"updatedById"`
	Organization   *Organization
	CreatedBy      *User                 `gorm:"foreignKey:CreatedByID"`
	UpdatedBy      *User                 `gorm:"foreignKey:UpdatedByID"`
	Assignments    []PromptTagAssignment `gorm:"foreignKey:TagID;constraint:OnDelete:CASCADE"`
}

func (PromptTag) TableName() string {
	return "PromptTag"
}

func (m *PromptTag) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*PromptTag) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Analytics struct {
	ID           string         `gorm:"column:id;primaryKey" json:"id"`
	ProjectID    string         `gorm:"column:projectId;index:Analytics_createdAt_key_projectId_numericValue_idx;index:Analytics_projectId_idx;not null" json:"projectId"`
	Key          string         `gorm:"column:key;index:Analytics_createdAt_key_projectId_numericValue_idx;not null" json:"key"`
	Value        datatypes.JSON `gorm:"column:value" json:"value"`
	NumericValue *float64       `gorm:"column:numericValue;index:Analytics_createdAt_key_projectId_numericValue_idx" json:"numericValue"`
	StringValue  *string        `gorm:"column:stringValue" json:"stringValue"`
	BoolValue    *bool          `gorm:"column:boolValue" json:"boolValue"`
	CreatedAt    DateTime       `gorm:"column:createdAt;index:Analytics_createdAt_key_projectId_numericValue_idx;autoCreateTime:false;not null" json:"createdAt"`
	Project      *Project
}

func (Analytics) TableName() string {
	return "Analytics"
}

func (m *Analytics) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type CustomRole struct {
	ID             string         `gorm:"column:id;primaryKey" json:"id"`
	OrganizationID string         `gorm:"column:organizationId;uniqueIndex:CustomRole_organizationId_name_key;index:CustomRole_organizationId_idx;not null" json:"organizationId"`
	Name           string         `gorm:"column:name;uniqueIndex:CustomRole_organizationId_name_key;not null" json:"name"`
	Description    *string        `gorm:"column:description" json:"description"`
	Permissions    datatypes.JSON `gorm:"column:permissions;not null" json:"permissions"`
	Kind           *string        `gorm:"column:kind;default:'custom';not null" json:"kind"`
	CreatedAt      DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt      DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Organization   *Organization
	AssignedUsers  []TeamUser    `gorm:"foreignKey:AssignedRoleID"`
	RoleBindings   []RoleBinding `gorm:"foreignKey:CustomRoleID"`
}

func (CustomRole) TableName() string {
	return "CustomRole"
}

func (m *CustomRole) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*CustomRole) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Notification struct {
	ID             string         `gorm:"column:id;primaryKey" json:"id"`
	OrganizationID *string        `gorm:"column:organizationId;index:Notification_organizationId_idx" json:"organizationId"`
	ProjectID      *string        `gorm:"column:projectId;index:Notification_projectId_idx" json:"projectId"`
	Metadata       datatypes.JSON `gorm:"column:metadata;not null" json:"metadata"`
	CreatedAt      DateTime       `gorm:"column:createdAt;index:Notification_createdAt_idx;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt      DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	SentAt         DateTime       `gorm:"column:sentAt;index:Notification_sentAt_idx;not null" json:"sentAt"`
	Organization   *Organization
	Project        *Project
}

func (Notification) TableName() string {
	return "Notification"
}

func (m *Notification) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*Notification) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Agent struct {
	ID                string         `gorm:"column:id;primaryKey" json:"id"`
	ProjectID         string         `gorm:"column:projectId;uniqueIndex:Agent_projectId_identityKey_key;index:Agent_projectId_idx;index:Agent_projectId_lastSeenAt_idx;not null" json:"projectId"`
	Name              string         `gorm:"column:name;not null" json:"name"`
	Type              string         `gorm:"column:type;not null" json:"type"`
	Config            datatypes.JSON `gorm:"column:config;not null" json:"config"`
	WorkflowID        *string        `gorm:"column:workflowId;index:Agent_workflowId_idx" json:"workflowId"`
	CopiedFromAgentID *string        `gorm:"column:copiedFromAgentId;index:Agent_copiedFromAgentId_idx" json:"copiedFromAgentId"`
	Environment       *string        `gorm:"column:environment" json:"environment"`
	OwnerUserID       *string        `gorm:"column:ownerUserId" json:"ownerUserId"`
	HostLabel         *string        `gorm:"column:hostLabel" json:"hostLabel"`
	IdentityKey       *string        `gorm:"column:identityKey;uniqueIndex:Agent_projectId_identityKey_key" json:"identityKey"`
	LastSeenAt        *DateTime      `gorm:"column:lastSeenAt;index:Agent_projectId_lastSeenAt_idx" json:"lastSeenAt"`
	ArchivedAt        *DateTime      `gorm:"column:archivedAt" json:"archivedAt"`
	CreatedAt         DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt         DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	CreatedByID       *string        `gorm:"column:createdById" json:"createdById"`
	UpdatedByID       *string        `gorm:"column:updatedById" json:"updatedById"`
	Project           *Project
	Workflow          *Workflow
	CopiedFrom        *Agent  `gorm:"foreignKey:CopiedFromAgentID"`
	CopiedAgents      []Agent `gorm:"foreignKey:CopiedFromAgentID;constraint:OnUpdate:RESTRICT,OnDelete:RESTRICT"`
}

func (Agent) TableName() string {
	return "Agent"
}

func (m *Agent) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*Agent) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type AgentVersion struct {
	ID            string         `gorm:"column:id;primaryKey" json:"id"`
	ProjectID     string         `gorm:"column:projectId;index:AgentVersion_projectId_idx;not null" json:"projectId"`
	AgentID       string         `gorm:"column:agentId;uniqueIndex:AgentVersion_agentId_version_key;not null" json:"agentId"`
	Version       int            `gorm:"column:version;uniqueIndex:AgentVersion_agentId_version_key;not null" json:"version"`
	Name          string         `gorm:"column:name;not null" json:"name"`
	Type          string         `gorm:"column:type;not null" json:"type"`
	Config        datatypes.JSON `gorm:"column:config;not null" json:"config"`
	WorkflowID    *string        `gorm:"column:workflowId" json:"workflowId"`
	CommitMessage *string        `gorm:"column:commitMessage" json:"commitMessage"`
	AuthorID      *string        `gorm:"column:authorId" json:"authorId"`
	AuthorKind    *string        `gorm:"column:authorKind" json:"authorKind"`
	CreatedAt     DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
}

func (AgentVersion) TableName() string {
	return "AgentVersion"
}

func (m *AgentVersion) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type AgentVersionLabel struct {
	ID          string   `gorm:"column:id;primaryKey" json:"id"`
	ProjectID   string   `gorm:"column:projectId;index:AgentVersionLabel_projectId_idx;not null" json:"projectId"`
	AgentID     string   `gorm:"column:agentId;uniqueIndex:AgentVersionLabel_agentId_label_key;not null" json:"agentId"`
	Label       string   `gorm:"column:label;uniqueIndex:AgentVersionLabel_agentId_label_key;not null" json:"label"`
	VersionID   string   `gorm:"column:versionId;index:AgentVersionLabel_versionId_idx;not null" json:"versionId"`
	CreatedByID *string  `gorm:"column:createdById" json:"createdById"`
	UpdatedByID *string  `gorm:"column:updatedById" json:"updatedById"`
	CreatedAt   DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt   DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
}

func (AgentVersionLabel) TableName() string {
	return "AgentVersionLabel"
}

func (m *AgentVersionLabel) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*AgentVersionLabel) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Evaluator struct {
	ID                    string         `gorm:"column:id;primaryKey" json:"id"`
	ProjectID             string         `gorm:"column:projectId;uniqueIndex:Evaluator_projectId_slug_key;index:Evaluator_projectId_idx;not null" json:"projectId"`
	Name                  string         `gorm:"column:name;not null" json:"name"`
	Slug                  *string        `gorm:"column:slug;uniqueIndex:Evaluator_projectId_slug_key" json:"slug"`
	Type                  string         `gorm:"column:type;not null" json:"type"`
	Config                datatypes.JSON `gorm:"column:config;not null" json:"config"`
	WorkflowID            *string        `gorm:"column:workflowId;index:Evaluator_workflowId_idx" json:"workflowId"`
	CopiedFromEvaluatorID *string        `gorm:"column:copiedFromEvaluatorId;index:Evaluator_copiedFromEvaluatorId_idx" json:"copiedFromEvaluatorId"`
	ArchivedAt            *DateTime      `gorm:"column:archivedAt" json:"archivedAt"`
	CreatedAt             DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt             DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	CreatedByID           *string        `gorm:"column:createdById" json:"createdById"`
	UpdatedByID           *string        `gorm:"column:updatedById" json:"updatedById"`
	Project               *Project
	Workflow              *Workflow
	CopiedFrom            *Evaluator         `gorm:"foreignKey:CopiedFromEvaluatorID"`
	CopiedEvaluators      []Evaluator        `gorm:"foreignKey:CopiedFromEvaluatorID;constraint:OnUpdate:RESTRICT,OnDelete:RESTRICT"`
	Monitors              []Monitor          `gorm:"foreignKey:EvaluatorID"`
	GatewayGuardrails     []GatewayGuardrail `gorm:"foreignKey:EvaluatorID;constraint:OnDelete:RESTRICT"`
}

func (Evaluator) TableName() string {
	return "Evaluator"
}

func (m *Evaluator) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*Evaluator) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type EvaluatorVersion struct {
	ID            string         `gorm:"column:id;primaryKey" json:"id"`
	ProjectID     string         `gorm:"column:projectId;index:EvaluatorVersion_projectId_idx;not null" json:"projectId"`
	EvaluatorID   string         `gorm:"column:evaluatorId;uniqueIndex:EvaluatorVersion_evaluatorId_version_key;not null" json:"evaluatorId"`
	Version       int            `gorm:"column:version;uniqueIndex:EvaluatorVersion_evaluatorId_version_key;not null" json:"version"`
	Name          string         `gorm:"column:name;not null" json:"name"`
	Type          string         `gorm:"column:type;not null" json:"type"`
	Config        datatypes.JSON `gorm:"column:config;not null" json:"config"`
	WorkflowID    *string        `gorm:"column:workflowId" json:"workflowId"`
	CommitMessage *string        `gorm:"column:commitMessage" json:"commitMessage"`
	AuthorID      *string        `gorm:"column:authorId" json:"authorId"`
	AuthorKind    *string        `gorm:"column:authorKind" json:"authorKind"`
	CreatedAt     DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
}

func (EvaluatorVersion) TableName() string {
	return "EvaluatorVersion"
}

func (m *EvaluatorVersion) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type EvaluatorVersionLabel struct {
	ID          string   `gorm:"column:id;primaryKey" json:"id"`
	ProjectID   string   `gorm:"column:projectId;index:EvaluatorVersionLabel_projectId_idx;not null" json:"projectId"`
	EvaluatorID string   `gorm:"column:evaluatorId;uniqueIndex:EvaluatorVersionLabel_evaluatorId_label_key;not null" json:"evaluatorId"`
	Label       string   `gorm:"column:label;uniqueIndex:EvaluatorVersionLabel_evaluatorId_label_key;not null" json:"label"`
	VersionID   string   `gorm:"column:versionId;index:EvaluatorVersionLabel_versionId_idx;not null" json:"versionId"`
	CreatedByID *string  `gorm:"column:createdById" json:"createdById"`
	UpdatedByID *string  `gorm:"column:updatedById" json:"updatedById"`
	CreatedAt   DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt   DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
}

func (EvaluatorVersionLabel) TableName() string {
	return "EvaluatorVersionLabel"
}

func (m *EvaluatorVersionLabel) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*EvaluatorVersionLabel) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Scenario struct {
	ID              string         `gorm:"column:id;primaryKey" json:"id"`
	ProjectID       string         `gorm:"column:projectId;index:Scenario_projectId_idx;index:Scenario_projectId_testSuiteId_idx;not null" json:"projectId"`
	Name            string         `gorm:"column:name;not null" json:"name"`
	Situation       string         `gorm:"column:situation;not null" json:"situation"`
	Criteria        []string       `gorm:"column:criteria;serializer:json;not null" json:"criteria"`
	Labels          []string       `gorm:"column:labels;serializer:json;not null" json:"labels"`
	Parameters      datatypes.JSON `gorm:"column:parameters" json:"parameters"`
	SimulatorModel  *string        `gorm:"column:simulatorModel" json:"simulatorModel"`
	JudgeModel      *string        `gorm:"column:judgeModel" json:"judgeModel"`
	MaxTurns        *int           `gorm:"column:maxTurns" json:"maxTurns"`
	MinTurns        *int           `gorm:"column:minTurns" json:"minTurns"`
	Fields          datatypes.JSON `gorm:"column:fields" json:"fields"`
	CallerVoice     datatypes.JSON `gorm:"column:callerVoice" json:"callerVoice"`
	TestSuiteID     *string        `gorm:"column:testSuiteId;index:Scenario_projectId_testSuiteId_idx" json:"testSuiteId"`
	Version         *int           `gorm:"column:version;default:1;not null" json:"version"`
	LastUpdatedByID *string        `gorm:"column:lastUpdatedById" json:"lastUpdatedById"`
	ArchivedAt      *DateTime      `gorm:"column:archivedAt" json:"archivedAt"`
	CreatedAt       DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt       DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Project         *Project
	TestSuite       *SimulationSuite  `gorm:"foreignKey:TestSuiteID"`
	LastUpdatedBy   *User             `gorm:"foreignKey:LastUpdatedByID"`
	Versions        []ScenarioVersion `gorm:"foreignKey:ScenarioID;constraint:OnDelete:CASCADE"`
}

func (Scenario) TableName() string {
	return "Scenario"
}

func (m *Scenario) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*Scenario) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type ScenarioVersion struct {
	ID                string         `gorm:"column:id;primaryKey" json:"id"`
	ScenarioID        string         `gorm:"column:scenarioId;uniqueIndex:ScenarioVersion_scenarioId_version_key;index:ScenarioVersion_projectId_scenarioId_version_idx;not null" json:"scenarioId"`
	ProjectID         string         `gorm:"column:projectId;index:ScenarioVersion_projectId_scenarioId_version_idx;not null" json:"projectId"`
	Version           int            `gorm:"column:version;uniqueIndex:ScenarioVersion_scenarioId_version_key;index:ScenarioVersion_projectId_scenarioId_version_idx;not null" json:"version"`
	AuthorID          *string        `gorm:"column:authorId" json:"authorId"`
	AuthorLabel       string         `gorm:"column:authorLabel;not null" json:"authorLabel"`
	ChangeDescription *string        `gorm:"column:changeDescription" json:"changeDescription"`
	Snapshot          datatypes.JSON `gorm:"column:snapshot;not null" json:"snapshot"`
	SchemaVersion     *int           `gorm:"column:schemaVersion;default:1;not null" json:"schemaVersion"`
	CreatedAt         DateTime       `gorm:"column:createdAt;index:ScenarioVersion_createdAt_idx;autoCreateTime:false;not null" json:"createdAt"`
	Scenario          *Scenario
}

func (ScenarioVersion) TableName() string {
	return "ScenarioVersion"
}

func (m *ScenarioVersion) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type SimulationSuite struct {
	ID                 string         `gorm:"column:id;primaryKey" json:"id"`
	ProjectID          string         `gorm:"column:projectId;uniqueIndex:SimulationSuite_projectId_slug_key;index:SimulationSuite_projectId_idx;index:SimulationSuite_projectId_kind_idx;not null" json:"projectId"`
	Name               string         `gorm:"column:name;not null" json:"name"`
	Slug               string         `gorm:"column:slug;uniqueIndex:SimulationSuite_projectId_slug_key;not null" json:"slug"`
	Description        *string        `gorm:"column:description" json:"description"`
	ScenarioIds        []string       `gorm:"column:scenarioIds;serializer:json;not null" json:"scenarioIds"`
	Targets            datatypes.JSON `gorm:"column:targets;not null" json:"targets"`
	RepeatCount        *int           `gorm:"column:repeatCount;default:1;not null" json:"repeatCount"`
	Labels             []string       `gorm:"column:labels;serializer:json;not null" json:"labels"`
	SimulatorModel     *string        `gorm:"column:simulatorModel" json:"simulatorModel"`
	JudgeModel         *string        `gorm:"column:judgeModel" json:"judgeModel"`
	Kind               *string        `gorm:"column:kind;index:SimulationSuite_projectId_kind_idx;default:'run_plan';not null" json:"kind"`
	Scope              datatypes.JSON `gorm:"column:scope" json:"scope"`
	Fields             datatypes.JSON `gorm:"column:fields" json:"fields"`
	Evaluators         datatypes.JSON `gorm:"column:evaluators" json:"evaluators"`
	ArchivedAt         *DateTime      `gorm:"column:archivedAt" json:"archivedAt"`
	CreatedAt          DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt          DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Project            *Project
	TestSuiteScenarios []Scenario `gorm:"foreignKey:TestSuiteID;constraint:OnDelete:SET NULL"`
}

func (SimulationSuite) TableName() string {
	return "SimulationSuite"
}

func (m *SimulationSuite) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*SimulationSuite) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Subscription struct {
	ID                       string    `gorm:"column:id;primaryKey" json:"id"`
	OrganizationID           string    `gorm:"column:organizationId;index:Subscription_organizationId_idx;not null" json:"organizationId"`
	Plan                     string    `gorm:"column:plan;not null" json:"plan"`
	StripeSubscriptionID     *string   `gorm:"column:stripeSubscriptionId;uniqueIndex" json:"stripeSubscriptionId"`
	Status                   string    `gorm:"column:status;not null" json:"status"`
	StartDate                *DateTime `gorm:"column:startDate" json:"startDate"`
	EndDate                  *DateTime `gorm:"column:endDate" json:"endDate"`
	LastPaymentFailedDate    *DateTime `gorm:"column:lastPaymentFailedDate" json:"lastPaymentFailedDate"`
	CreatedAt                DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt                DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	MaxMembers               *int      `gorm:"column:maxMembers" json:"maxMembers"`
	MaxProjects              *int      `gorm:"column:maxProjects" json:"maxProjects"`
	MaxMessagesPerMonth      *int      `gorm:"column:maxMessagesPerMonth" json:"maxMessagesPerMonth"`
	MaxWorkflows             *int      `gorm:"column:maxWorkflows" json:"maxWorkflows"`
	MaxRetentionDays         *int      `gorm:"column:maxRetentionDays" json:"maxRetentionDays"`
	MaxMembersLite           *int      `gorm:"column:maxMembersLite" json:"maxMembersLite"`
	MaxMembersDeveloper      *int      `gorm:"column:maxMembersDeveloper" json:"maxMembersDeveloper"`
	MaxTeams                 *int      `gorm:"column:maxTeams" json:"maxTeams"`
	MaxPrompts               *int      `gorm:"column:maxPrompts" json:"maxPrompts"`
	MaxEvaluators            *int      `gorm:"column:maxEvaluators" json:"maxEvaluators"`
	MaxScenarios             *int      `gorm:"column:maxScenarios" json:"maxScenarios"`
	MaxAgents                *int      `gorm:"column:maxAgents" json:"maxAgents"`
	MaxExperiments           *int      `gorm:"column:maxExperiments" json:"maxExperiments"`
	MaxOnlineEvaluations     *int      `gorm:"column:maxOnlineEvaluations" json:"maxOnlineEvaluations"`
	MaxDatasets              *int      `gorm:"column:maxDatasets" json:"maxDatasets"`
	MaxDashboards            *int      `gorm:"column:maxDashboards" json:"maxDashboards"`
	MaxCustomGraphs          *int      `gorm:"column:maxCustomGraphs" json:"maxCustomGraphs"`
	MaxAutomations           *int      `gorm:"column:maxAutomations" json:"maxAutomations"`
	SeatsKept                *int      `gorm:"column:seatsKept" json:"seatsKept"`
	SeatsKeptUntil           *DateTime `gorm:"column:seatsKeptUntil" json:"seatsKeptUntil"`
	SeatLineQuantity         *int      `gorm:"column:seatLineQuantity" json:"seatLineQuantity"`
	SeatLineQuantityAt       *DateTime `gorm:"column:seatLineQuantityAt" json:"seatLineQuantityAt"`
	SeatLineWriteQuantities  []int     `gorm:"column:seatLineWriteQuantities;serializer:json;not null" json:"seatLineWriteQuantities"`
	SeatLineWriteSince       *DateTime `gorm:"column:seatLineWriteSince" json:"seatLineWriteSince"`
	SeatLineWriteAt          *DateTime `gorm:"column:seatLineWriteAt" json:"seatLineWriteAt"`
	SeatLineWriteAttempts    *int      `gorm:"column:seatLineWriteAttempts" json:"seatLineWriteAttempts"`
	SeatLineWriteFlaggedAt   *DateTime `gorm:"column:seatLineWriteFlaggedAt" json:"seatLineWriteFlaggedAt"`
	SeatRepairQuantity       *int      `gorm:"column:seatRepairQuantity" json:"seatRepairQuantity"`
	SeatRepairLiftedQuantity *int      `gorm:"column:seatRepairLiftedQuantity" json:"seatRepairLiftedQuantity"`
	SeatRepairSince          *DateTime `gorm:"column:seatRepairSince" json:"seatRepairSince"`
	SeatRepairAttempts       *int      `gorm:"column:seatRepairAttempts" json:"seatRepairAttempts"`
	SeatRepairFlaggedAt      *DateTime `gorm:"column:seatRepairFlaggedAt" json:"seatRepairFlaggedAt"`
	SeatLineLeaseToken       *string   `gorm:"column:seatLineLeaseToken" json:"seatLineLeaseToken"`
	SeatLineLeaseUntil       *DateTime `gorm:"column:seatLineLeaseUntil" json:"seatLineLeaseUntil"`
	Organization             *Organization
	Invoices                 []Invoice `gorm:"foreignKey:SubscriptionID"`
}

func (Subscription) TableName() string {
	return "Subscription"
}

func (m *Subscription) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
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

type Invoice struct {
	ID             string   `gorm:"column:id;primaryKey" json:"id"`
	SubscriptionID string   `gorm:"column:subscriptionId;index:Invoice_subscriptionId_idx;not null" json:"subscriptionId"`
	TotalAmount    float64  `gorm:"column:totalAmount;not null" json:"totalAmount"`
	Currency       string   `gorm:"column:currency;not null" json:"currency"`
	DueDate        DateTime `gorm:"column:dueDate;not null" json:"dueDate"`
	Paid           bool     `gorm:"column:paid;default:false;not null" json:"paid"`
	CreatedAt      DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt      DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Subscription   *Subscription
	LineItems      []InvoiceItem `gorm:"foreignKey:InvoiceID"`
}

func (Invoice) TableName() string {
	return "Invoice"
}

func (m *Invoice) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*Invoice) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type InvoiceItem struct {
	ID          string   `gorm:"column:id;primaryKey" json:"id"`
	InvoiceID   string   `gorm:"column:invoiceId;index:InvoiceItem_invoiceId_idx;not null" json:"invoiceId"`
	Description string   `gorm:"column:description;not null" json:"description"`
	Amount      float64  `gorm:"column:amount;not null" json:"amount"`
	CreatedAt   DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt   DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Invoice     *Invoice
}

func (InvoiceItem) TableName() string {
	return "InvoiceItem"
}

func (m *InvoiceItem) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*InvoiceItem) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Group struct {
	ID               string   `gorm:"column:id;primaryKey" json:"id"`
	OrganizationID   string   `gorm:"column:organizationId;uniqueIndex:Group_organizationId_slug_key;index:Group_organizationId_idx;not null" json:"organizationId"`
	Name             string   `gorm:"column:name;not null" json:"name"`
	Slug             string   `gorm:"column:slug;uniqueIndex:Group_organizationId_slug_key;not null" json:"slug"`
	ExternalID       *string  `gorm:"column:externalId;uniqueIndex:Group_scimConnectionId_externalId_key" json:"externalId"`
	ScimSource       *string  `gorm:"column:scimSource" json:"scimSource"`
	ScimConnectionID *string  `gorm:"column:scimConnectionId;uniqueIndex:Group_scimConnectionId_externalId_key;index:Group_scimConnectionId_idx" json:"scimConnectionId"`
	CreatedAt        DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt        DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Organization     *Organization
	Members          []GroupMembership `gorm:"foreignKey:GroupID;constraint:OnDelete:CASCADE"`
	RoleBindings     []RoleBinding     `gorm:"foreignKey:GroupID;constraint:OnDelete:CASCADE"`
}

func (Group) TableName() string {
	return "Group"
}

func (m *Group) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
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

type GroupMembership struct {
	UserID    string   `gorm:"column:userId;primaryKey;index:GroupMembership_userId_idx" json:"userId"`
	GroupID   string   `gorm:"column:groupId;primaryKey;index:GroupMembership_groupId_idx" json:"groupId"`
	CreatedAt DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	User      *User
	Group     *Group
}

func (GroupMembership) TableName() string {
	return "GroupMembership"
}

func (m *GroupMembership) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type AuthzUserStanding struct {
	UserID            string    `gorm:"column:userId;primaryKey" json:"userId"`
	DeactivatedAt     *DateTime `gorm:"column:deactivatedAt" json:"deactivatedAt"`
	ErasedAt          *DateTime `gorm:"column:erasedAt" json:"erasedAt"`
	StandingChangedAt DateTime  `gorm:"column:standingChangedAt;not null" json:"standingChangedAt"`
}

func (AuthzUserStanding) TableName() string {
	return "AuthzUserStanding"
}

type AuthzEngineSwitch struct {
	OrganizationID     string   `gorm:"column:organizationId;primaryKey" json:"organizationId"`
	OnEngine           bool     `gorm:"column:onEngine;not null" json:"onEngine"`
	Pinned             bool     `gorm:"column:pinned;default:false;not null" json:"pinned"`
	SwitchedAt         DateTime `gorm:"column:switchedAt;not null" json:"switchedAt"`
	LastAppliedEventID *string  `gorm:"column:lastAppliedEventId" json:"lastAppliedEventId"`
	UpdatedAt          DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
}

func (AuthzEngineSwitch) TableName() string {
	return "AuthzEngineSwitch"
}

func (m *AuthzEngineSwitch) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*AuthzEngineSwitch) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type AuthzOrganizationPolicy struct {
	OrganizationID               string   `gorm:"column:organizationId;primaryKey" json:"organizationId"`
	BraidsOrganizationAdminsOnly bool     `gorm:"column:braidsOrganizationAdminsOnly;default:false;not null" json:"braidsOrganizationAdminsOnly"`
	UpdatedByUserID              string   `gorm:"column:updatedByUserId;not null" json:"updatedByUserId"`
	UpdatedAt                    DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
}

func (AuthzOrganizationPolicy) TableName() string {
	return "AuthzOrganizationPolicy"
}

func (m *AuthzOrganizationPolicy) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*AuthzOrganizationPolicy) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type RoleBinding struct {
	ID             string   `gorm:"column:id;primaryKey" json:"id"`
	OrganizationID string   `gorm:"column:organizationId;index:RoleBinding_organizationId_idx;not null" json:"organizationId"`
	UserID         *string  `gorm:"column:userId;index:RoleBinding_userId_idx" json:"userId"`
	GroupID        *string  `gorm:"column:groupId;index:RoleBinding_groupId_idx" json:"groupId"`
	APIKeyID       *string  `gorm:"column:apiKeyId;index:RoleBinding_apiKeyId_idx" json:"apiKeyId"`
	Role           string   `gorm:"column:role;not null" json:"role"`
	CustomRoleID   *string  `gorm:"column:customRoleId" json:"customRoleId"`
	ScopeType      string   `gorm:"column:scopeType;index:RoleBinding_scopeType_scopeId_idx;not null" json:"scopeType"`
	ScopeID        string   `gorm:"column:scopeId;index:RoleBinding_scopeType_scopeId_idx;not null" json:"scopeId"`
	CreatedAt      DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt      DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Organization   *Organization
	User           *User
	Group          *Group
	APIKey         *APIKey
	CustomRole     *CustomRole
}

func (RoleBinding) TableName() string {
	return "RoleBinding"
}

func (m *RoleBinding) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*RoleBinding) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Grant struct {
	ID              string         `gorm:"column:id;primaryKey" json:"id"`
	OrganizationID  string         `gorm:"column:organizationId;index:Grant_organizationId_scopeType_scopeId_idx;not null" json:"organizationId"`
	PrincipalType   string         `gorm:"column:principalType;not null" json:"principalType"`
	PrincipalID     *string        `gorm:"column:principalId" json:"principalId"`
	RoleKey         *string        `gorm:"column:roleKey" json:"roleKey"`
	LegacyRole      *string        `gorm:"column:legacyRole" json:"legacyRole"`
	Source          string         `gorm:"column:source;not null" json:"source"`
	ScopeType       string         `gorm:"column:scopeType;index:Grant_organizationId_scopeType_scopeId_idx;not null" json:"scopeType"`
	ScopeID         string         `gorm:"column:scopeId;index:Grant_organizationId_scopeType_scopeId_idx;index:Grant_projectId_resourceKind_scopeId_idx;not null" json:"scopeId"`
	Token           *string        `gorm:"column:token;uniqueIndex" json:"token"`
	Permission      *string        `gorm:"column:permission" json:"permission"`
	ResourceKind    *string        `gorm:"column:resourceKind;index:Grant_projectId_resourceKind_scopeId_idx" json:"resourceKind"`
	ProjectID       *string        `gorm:"column:projectId;index:Grant_projectId_resourceKind_scopeId_idx" json:"projectId"`
	CreatedByUserID *string        `gorm:"column:createdByUserId" json:"createdByUserId"`
	ExpiresAt       *DateTime      `gorm:"column:expiresAt" json:"expiresAt"`
	MaxViews        *int           `gorm:"column:maxViews" json:"maxViews"`
	Condition       datatypes.JSON `gorm:"column:condition" json:"condition"`
	ReplacesGrantID *string        `gorm:"column:replacesGrantId" json:"replacesGrantId"`
	OccurredAt      DateTime       `gorm:"column:occurredAt;not null" json:"occurredAt"`
	CreatedAt       DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt       DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	RevokedAt       *DateTime      `gorm:"column:revokedAt" json:"revokedAt"`
	RevokedReason   *string        `gorm:"column:revokedReason" json:"revokedReason"`
}

func (Grant) TableName() string {
	return "Grant"
}

func (m *Grant) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*Grant) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type GrantUsage struct {
	GrantID        string    `gorm:"column:grantId;primaryKey" json:"grantId"`
	OrganizationID string    `gorm:"column:organizationId;index:GrantUsage_organizationId_idx;not null" json:"organizationId"`
	ProjectID      string    `gorm:"column:projectId;index:GrantUsage_projectId_idx;not null" json:"projectId"`
	ViewCount      int       `gorm:"column:viewCount;default:0;not null" json:"viewCount"`
	LastViewedAt   *DateTime `gorm:"column:lastViewedAt" json:"lastViewedAt"`
	CreatedAt      DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt      DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
}

func (GrantUsage) TableName() string {
	return "GrantUsage"
}

func (m *GrantUsage) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*GrantUsage) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Role struct {
	ID             string         `gorm:"column:id;primaryKey" json:"id"`
	OrganizationID string         `gorm:"column:organizationId;index:Role_organizationId_idx;not null" json:"organizationId"`
	Name           string         `gorm:"column:name;not null" json:"name"`
	Description    *string        `gorm:"column:description" json:"description"`
	Permissions    datatypes.JSON `gorm:"column:permissions;not null" json:"permissions"`
	Kind           *string        `gorm:"column:kind;default:'custom';not null" json:"kind"`
	OccurredAt     DateTime       `gorm:"column:occurredAt;not null" json:"occurredAt"`
	CreatedAt      DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt      DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	DeletedAt      *DateTime      `gorm:"column:deletedAt" json:"deletedAt"`
}

func (Role) TableName() string {
	return "Role"
}

func (m *Role) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*Role) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type APIKey struct {
	ID                        string    `gorm:"column:id;primaryKey" json:"id"`
	Name                      string    `gorm:"column:name;not null" json:"name"`
	Description               *string   `gorm:"column:description" json:"description"`
	LookupID                  string    `gorm:"column:lookupId;uniqueIndex;index:ApiKey_lookupId_idx;not null" json:"lookupId"`
	HashedSecret              string    `gorm:"column:hashedSecret;not null" json:"hashedSecret"`
	PermissionMode            *string   `gorm:"column:permissionMode;default:'all';not null" json:"permissionMode"`
	UserID                    *string   `gorm:"column:userId;index:ApiKey_userId_idx" json:"userId"`
	CreatedByUserID           *string   `gorm:"column:createdByUserId" json:"createdByUserId"`
	OrganizationID            string    `gorm:"column:organizationId;index:ApiKey_organizationId_idx;not null" json:"organizationId"`
	ExpiresAt                 *DateTime `gorm:"column:expiresAt" json:"expiresAt"`
	LastUsedAt                *DateTime `gorm:"column:lastUsedAt" json:"lastUsedAt"`
	RevokedAt                 *DateTime `gorm:"column:revokedAt" json:"revokedAt"`
	RevocationCause           *string   `gorm:"column:revocationCause" json:"revocationCause"`
	ParentAPIKeyID            *string   `gorm:"column:parentApiKeyId" json:"parentApiKeyId"`
	IngestSourceType          *string   `gorm:"column:ingestSourceType;index:ApiKey_ingestSourceType_idx" json:"ingestSourceType"`
	IngestionTemplateID       *string   `gorm:"column:ingestionTemplateId;index:ApiKey_ingestionTemplateId_idx" json:"ingestionTemplateId"`
	CreatedByDeviceLabel      *string   `gorm:"column:createdByDeviceLabel" json:"createdByDeviceLabel"`
	IsSystemManaged           bool      `gorm:"column:isSystemManaged;default:false;not null" json:"isSystemManaged"`
	CreatedByBrowserSessionID *string   `gorm:"column:createdByBrowserSessionId" json:"createdByBrowserSessionId"`
	CreatedAt                 DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt                 DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	User                      *User
	Organization              *Organization
	RoleBindings              []RoleBinding `gorm:"foreignKey:APIKeyID;constraint:OnDelete:RESTRICT"`
}

func (APIKey) TableName() string {
	return "ApiKey"
}

func (m *APIKey) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
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

type SigningKey struct {
	ID               string    `gorm:"column:id;primaryKey" json:"id"`
	OrganizationID   string    `gorm:"column:organizationId;index:SigningKey_organizationId_idx;not null" json:"organizationId"`
	ProjectID        string    `gorm:"column:projectId;index:SigningKey_projectId_idx;not null" json:"projectId"`
	Name             string    `gorm:"column:name;not null" json:"name"`
	PublicID         string    `gorm:"column:publicId;uniqueIndex;not null" json:"publicId"`
	SecretCiphertext string    `gorm:"column:secretCiphertext;not null" json:"secretCiphertext"`
	CreatedByUserID  string    `gorm:"column:createdByUserId;not null" json:"createdByUserId"`
	CreatedAt        DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	RotatedAt        *DateTime `gorm:"column:rotatedAt" json:"rotatedAt"`
	VerifiesUntil    *DateTime `gorm:"column:verifiesUntil" json:"verifiesUntil"`
	RevokedAt        *DateTime `gorm:"column:revokedAt" json:"revokedAt"`
	LastUsedAt       *DateTime `gorm:"column:lastUsedAt" json:"lastUsedAt"`
}

func (SigningKey) TableName() string {
	return "SigningKey"
}

func (m *SigningKey) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type ProjectActiveDay struct {
	ID        string   `gorm:"column:id;primaryKey" json:"id"`
	ProjectID string   `gorm:"column:projectId;uniqueIndex:ProjectActiveDay_projectId_day_key;not null" json:"projectId"`
	Day       string   `gorm:"column:day;uniqueIndex:ProjectActiveDay_projectId_day_key;not null" json:"day"`
	CreatedAt DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
}

func (ProjectActiveDay) TableName() string {
	return "ProjectActiveDay"
}

func (m *ProjectActiveDay) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type NurturingProject struct {
	ProjectID      string `gorm:"column:projectId;primaryKey" json:"projectId"`
	OrganizationID string `gorm:"column:organizationId;index:NurturingProject_organizationId_idx;not null" json:"organizationId"`
}

func (NurturingProject) TableName() string {
	return "NurturingProject"
}

type NurturingUncopiedProject struct {
	ProjectID   string   `gorm:"column:projectId;primaryKey" json:"projectId"`
	FirstSeenAt DateTime `gorm:"column:firstSeenAt;not null" json:"firstSeenAt"`
}

func (NurturingUncopiedProject) TableName() string {
	return "NurturingUncopiedProject"
}

func (m *NurturingUncopiedProject) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.FirstSeenAt.IsZero() {
		m.FirstSeenAt = DateTime{Time: now}
	}
	return nil
}

type NurturingOrganizationMember struct {
	OrganizationID string    `gorm:"column:organizationId;primaryKey" json:"organizationId"`
	UserID         string    `gorm:"column:userId;primaryKey;index:NurturingOrganizationMember_userId_idx" json:"userId"`
	JoinedAt       *DateTime `gorm:"column:joinedAt" json:"joinedAt"`
	LeftAt         *DateTime `gorm:"column:leftAt" json:"leftAt"`
	ReturnedAt     *DateTime `gorm:"column:returnedAt" json:"returnedAt"`
}

func (NurturingOrganizationMember) TableName() string {
	return "NurturingOrganizationMember"
}

type NurturingOrganizationCreated struct {
	OrganizationID string   `gorm:"column:organizationId;primaryKey" json:"organizationId"`
	CreatedAt      DateTime `gorm:"column:createdAt;index:NurturingOrganizationCreated_createdAt_idx;autoCreateTime:false;not null" json:"createdAt"`
}

func (NurturingOrganizationCreated) TableName() string {
	return "NurturingOrganizationCreated"
}

type NurturingOrganization struct {
	OrganizationID       string    `gorm:"column:organizationId;primaryKey" json:"organizationId"`
	AdminUserID          *string   `gorm:"column:adminUserId" json:"adminUserId"`
	Seeded               bool      `gorm:"column:seeded;default:false;not null" json:"seeded"`
	EvaluationCount      int       `gorm:"column:evaluationCount;default:0;not null" json:"evaluationCount"`
	SimulationRunCount   int       `gorm:"column:simulationRunCount;default:0;not null" json:"simulationRunCount"`
	CodingFirstSessionAt *DateTime `gorm:"column:codingFirstSessionAt" json:"codingFirstSessionAt"`
	CodingAgentTypes     []string  `gorm:"column:codingAgentTypes;serializer:json;not null" json:"codingAgentTypes"`
	CodingActiveDays     int       `gorm:"column:codingActiveDays;default:0;not null" json:"codingActiveDays"`
	CodingLastActiveDay  *string   `gorm:"column:codingLastActiveDay" json:"codingLastActiveDay"`
	CodingTerminalTypes  []string  `gorm:"column:codingTerminalTypes;serializer:json;not null" json:"codingTerminalTypes"`
	CodingRepositoryKeys []string  `gorm:"column:codingRepositoryKeys;serializer:json;not null" json:"codingRepositoryKeys"`
	CodingFirstDays      []int     `gorm:"column:codingFirstDays;serializer:json;not null" json:"codingFirstDays"`
	LastTraceAt          *DateTime `gorm:"column:lastTraceAt" json:"lastTraceAt"`
	UpdatedAt            DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
}

func (NurturingOrganization) TableName() string {
	return "NurturingOrganization"
}

func (m *NurturingOrganization) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*NurturingOrganization) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type NurturingProjectUsageView struct {
	ProjectID         string         `gorm:"column:projectId;primaryKey" json:"projectId"`
	Owner             string         `gorm:"column:owner;primaryKey" json:"owner"`
	Folded            datatypes.JSON `gorm:"column:folded;not null" json:"folded"`
	AppliedEventIds   []string       `gorm:"column:appliedEventIds;serializer:json;not null" json:"appliedEventIds"`
	ProjectionVersion string         `gorm:"column:projectionVersion;not null" json:"projectionVersion"`
	UpdatedAt         DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
}

func (NurturingProjectUsageView) TableName() string {
	return "NurturingProjectUsageView"
}

type NurturingProjectTraceDay struct {
	ProjectID         string    `gorm:"column:projectId;primaryKey" json:"projectId"`
	Day               string    `gorm:"column:day;primaryKey" json:"day"`
	TraceCount        int       `gorm:"column:traceCount;default:0;not null" json:"traceCount"`
	ErrorCount        int       `gorm:"column:errorCount;default:0;not null" json:"errorCount"`
	LastTraceAt       *DateTime `gorm:"column:lastTraceAt" json:"lastTraceAt"`
	LastFailedTraceAt *DateTime `gorm:"column:lastFailedTraceAt" json:"lastFailedTraceAt"`
}

func (NurturingProjectTraceDay) TableName() string {
	return "NurturingProjectTraceDay"
}

type NurturingPersonalProject struct {
	ProjectID      string `gorm:"column:projectId;primaryKey" json:"projectId"`
	OrganizationID string `gorm:"column:organizationId;not null" json:"organizationId"`
	OwnerUserID    string `gorm:"column:ownerUserId;not null" json:"ownerUserId"`
}

func (NurturingPersonalProject) TableName() string {
	return "NurturingPersonalProject"
}

type NurturingTracedProject struct {
	ProjectID      string `gorm:"column:projectId;primaryKey" json:"projectId"`
	OrganizationID string `gorm:"column:organizationId;index:NurturingTracedProject_organizationId_idx;not null" json:"organizationId"`
}

func (NurturingTracedProject) TableName() string {
	return "NurturingTracedProject"
}

type BillingMeterCheckpoint struct {
	ID                   string   `gorm:"column:id;primaryKey" json:"id"`
	OrganizationID       string   `gorm:"column:organizationId;uniqueIndex:BillingMeterCheckpoint_organizationId_billingMonth_meter_key;not null" json:"organizationId"`
	BillingMonth         string   `gorm:"column:billingMonth;uniqueIndex:BillingMeterCheckpoint_organizationId_billingMonth_meter_key;not null" json:"billingMonth"`
	Meter                *string  `gorm:"column:meter;uniqueIndex:BillingMeterCheckpoint_organizationId_billingMonth_meter_key;default:'langwatch_billable_events';not null" json:"meter"`
	LastReportedTotal    int      `gorm:"column:lastReportedTotal;default:0;not null" json:"lastReportedTotal"`
	PendingReportedTotal *int     `gorm:"column:pendingReportedTotal" json:"pendingReportedTotal"`
	ConsecutiveFailures  int      `gorm:"column:consecutiveFailures;default:0;not null" json:"consecutiveFailures"`
	LastCountedEventID   *string  `gorm:"column:lastCountedEventId" json:"lastCountedEventId"`
	UpdatedAt            DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
}

func (BillingMeterCheckpoint) TableName() string {
	return "BillingMeterCheckpoint"
}

func (m *BillingMeterCheckpoint) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*BillingMeterCheckpoint) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type VirtualKey struct {
	ID                       string         `gorm:"column:id;primaryKey" json:"id"`
	OrganizationID           string         `gorm:"column:organizationId;uniqueIndex:VirtualKey_organizationId_externalId_key;index:VirtualKey_organizationId_idx;index:VirtualKey_organizationId_purpose_idx;not null" json:"organizationId"`
	Name                     string         `gorm:"column:name;not null" json:"name"`
	Description              *string        `gorm:"column:description" json:"description"`
	Status                   string         `gorm:"column:status;index:VirtualKey_status_idx;default:'ACTIVE';not null" json:"status"`
	Purpose                  string         `gorm:"column:purpose;index:VirtualKey_organizationId_purpose_idx;default:'USER';not null" json:"purpose"`
	ExternalID               *string        `gorm:"column:externalId;uniqueIndex:VirtualKey_organizationId_externalId_key" json:"externalId"`
	Metadata                 datatypes.JSON `gorm:"column:metadata;default:'{}';not null" json:"metadata"`
	DisabledAt               *DateTime      `gorm:"column:disabledAt" json:"disabledAt"`
	DisabledReason           *string        `gorm:"column:disabledReason" json:"disabledReason"`
	ExpiresAt                *DateTime      `gorm:"column:expiresAt" json:"expiresAt"`
	HashedSecret             string         `gorm:"column:hashedSecret;uniqueIndex;not null" json:"hashedSecret"`
	DisplayPrefix            string         `gorm:"column:displayPrefix;index:VirtualKey_displayPrefix_idx;not null" json:"displayPrefix"`
	PrincipalUserID          *string        `gorm:"column:principalUserId;index:VirtualKey_principalUserId_idx" json:"principalUserId"`
	TraceProjectID           *string        `gorm:"column:traceProjectId" json:"traceProjectId"`
	Config                   datatypes.JSON `gorm:"column:config;default:'{}';not null" json:"config"`
	Revision                 int64          `gorm:"column:revision;index:VirtualKey_revision_idx;default:0;not null" json:"revision"`
	PreviousHashedSecret     *string        `gorm:"column:previousHashedSecret" json:"previousHashedSecret"`
	PreviousSecretValidUntil *DateTime      `gorm:"column:previousSecretValidUntil" json:"previousSecretValidUntil"`
	RevokedAt                *DateTime      `gorm:"column:revokedAt" json:"revokedAt"`
	RevokedByID              *string        `gorm:"column:revokedById" json:"revokedById"`
	CreatedAt                DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt                DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	CreatedByID              string         `gorm:"column:createdById;not null" json:"createdById"`
	LastUsedAt               *DateTime      `gorm:"column:lastUsedAt" json:"lastUsedAt"`
	RoutingPolicyID          *string        `gorm:"column:routingPolicyId;index:VirtualKey_routingPolicyId_idx" json:"routingPolicyId"`
	RoutingMode              string         `gorm:"column:routingMode;default:'NONE';not null" json:"routingMode"`
	ConnectServices          []string       `gorm:"column:connectServices;serializer:json;not null" json:"connectServices"`
	LicenseTokenHash         *string        `gorm:"column:licenseTokenHash;uniqueIndex" json:"licenseTokenHash"`
	LicenseInstanceID        *string        `gorm:"column:licenseInstanceId" json:"licenseInstanceId"`
	LicenseExpiresAt         *DateTime      `gorm:"column:licenseExpiresAt" json:"licenseExpiresAt"`
	LicenseID                *string        `gorm:"column:licenseId" json:"licenseId"`
	PrincipalUser            *User          `gorm:"foreignKey:PrincipalUserID"`
	RoutingPolicy            *RoutingPolicy
	Scopes                   []VirtualKeyScope     `gorm:"foreignKey:VirtualKeyID;constraint:OnDelete:CASCADE"`
	LedgerEntries            []GatewayBudgetLedger `gorm:"foreignKey:VirtualKeyID;constraint:OnDelete:CASCADE"`
}

func (VirtualKey) TableName() string {
	return "VirtualKey"
}

func (m *VirtualKey) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*VirtualKey) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type VirtualKeyScope struct {
	ID           string   `gorm:"column:id;primaryKey" json:"id"`
	VirtualKeyID string   `gorm:"column:virtualKeyId;uniqueIndex:VirtualKeyScope_virtualKeyId_scopeType_scopeId_key;index:VirtualKeyScope_virtualKeyId_idx;not null" json:"virtualKeyId"`
	ScopeType    string   `gorm:"column:scopeType;uniqueIndex:VirtualKeyScope_virtualKeyId_scopeType_scopeId_key;index:VirtualKeyScope_scopeType_scopeId_idx;not null" json:"scopeType"`
	ScopeID      string   `gorm:"column:scopeId;uniqueIndex:VirtualKeyScope_virtualKeyId_scopeType_scopeId_key;index:VirtualKeyScope_scopeType_scopeId_idx;not null" json:"scopeId"`
	CreatedAt    DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	VirtualKey   *VirtualKey
}

func (VirtualKeyScope) TableName() string {
	return "VirtualKeyScope"
}

func (m *VirtualKeyScope) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type RoutingPolicy struct {
	ID               string         `gorm:"column:id;primaryKey" json:"id"`
	OrganizationID   string         `gorm:"column:organizationId;index:RoutingPolicy_organizationId_idx;not null" json:"organizationId"`
	Name             string         `gorm:"column:name;not null" json:"name"`
	Description      *string        `gorm:"column:description" json:"description"`
	ModelProviderIds datatypes.JSON `gorm:"column:modelProviderIds;default:'[]';not null" json:"modelProviderIds"`
	ModelAliases     datatypes.JSON `gorm:"column:modelAliases;default:'{}';not null" json:"modelAliases"`
	DefaultModel     *string        `gorm:"column:defaultModel" json:"defaultModel"`
	PolicyRules      datatypes.JSON `gorm:"column:policyRules;default:'{}';not null" json:"policyRules"`
	IsDefault        bool           `gorm:"column:isDefault;default:false;not null" json:"isDefault"`
	CreatedAt        DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt        DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	CreatedByID      *string        `gorm:"column:createdById" json:"createdById"`
	UpdatedByID      *string        `gorm:"column:updatedById" json:"updatedById"`
	Organization     *Organization
	CreatedBy        *User                `gorm:"foreignKey:CreatedByID"`
	UpdatedBy        *User                `gorm:"foreignKey:UpdatedByID"`
	VirtualKeys      []VirtualKey         `gorm:"foreignKey:RoutingPolicyID;constraint:OnDelete:SET NULL"`
	Scopes           []RoutingPolicyScope `gorm:"foreignKey:RoutingPolicyID;constraint:OnDelete:CASCADE"`
}

func (RoutingPolicy) TableName() string {
	return "RoutingPolicy"
}

func (m *RoutingPolicy) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*RoutingPolicy) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type RoutingPolicyScope struct {
	ID              string   `gorm:"column:id;primaryKey" json:"id"`
	RoutingPolicyID string   `gorm:"column:routingPolicyId;uniqueIndex:RoutingPolicyScope_routingPolicyId_scopeType_scopeId_key;index:RoutingPolicyScope_routingPolicyId_idx;not null" json:"routingPolicyId"`
	ScopeType       string   `gorm:"column:scopeType;uniqueIndex:RoutingPolicyScope_routingPolicyId_scopeType_scopeId_key;index:RoutingPolicyScope_scopeType_scopeId_idx;not null" json:"scopeType"`
	ScopeID         string   `gorm:"column:scopeId;uniqueIndex:RoutingPolicyScope_routingPolicyId_scopeType_scopeId_key;index:RoutingPolicyScope_scopeType_scopeId_idx;not null" json:"scopeId"`
	CreatedAt       DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	RoutingPolicy   *RoutingPolicy
}

func (RoutingPolicyScope) TableName() string {
	return "RoutingPolicyScope"
}

func (m *RoutingPolicyScope) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type GatewayGuardrail struct {
	ID          string    `gorm:"column:id;primaryKey" json:"id"`
	ProjectID   string    `gorm:"column:projectId;index:GatewayGuardrail_projectId_idx;index:GatewayGuardrail_projectId_archivedAt_idx;not null" json:"projectId"`
	Name        string    `gorm:"column:name;not null" json:"name"`
	Description *string   `gorm:"column:description" json:"description"`
	EvaluatorID string    `gorm:"column:evaluatorId;index:GatewayGuardrail_evaluatorId_idx;not null" json:"evaluatorId"`
	Direction   string    `gorm:"column:direction;not null" json:"direction"`
	FailureMode string    `gorm:"column:failureMode;default:'FAIL_CLOSED';not null" json:"failureMode"`
	ArchivedAt  *DateTime `gorm:"column:archivedAt;index:GatewayGuardrail_projectId_archivedAt_idx" json:"archivedAt"`
	CreatedAt   DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt   DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	CreatedByID *string   `gorm:"column:createdById" json:"createdById"`
	UpdatedByID *string   `gorm:"column:updatedById" json:"updatedById"`
	Project     *Project
	Evaluator   *Evaluator
	CreatedBy   *User `gorm:"foreignKey:CreatedByID"`
	UpdatedBy   *User `gorm:"foreignKey:UpdatedByID"`
}

func (GatewayGuardrail) TableName() string {
	return "GatewayGuardrail"
}

func (m *GatewayGuardrail) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*GatewayGuardrail) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type IngestionSource struct {
	ID                   string         `gorm:"column:id;primaryKey" json:"id"`
	OrganizationID       string         `gorm:"column:organizationId;uniqueIndex:IngestionSource_organizationId_name_key;index:IngestionSource_organizationId_idx;not null" json:"organizationId"`
	TeamID               *string        `gorm:"column:teamId;index:IngestionSource_teamId_idx" json:"teamId"`
	SourceType           string         `gorm:"column:sourceType;index:IngestionSource_sourceType_idx;not null" json:"sourceType"`
	Name                 string         `gorm:"column:name;uniqueIndex:IngestionSource_organizationId_name_key;not null" json:"name"`
	Description          *string        `gorm:"column:description" json:"description"`
	IngestSecretHash     string         `gorm:"column:ingestSecretHash;not null" json:"ingestSecretHash"`
	ParserConfig         datatypes.JSON `gorm:"column:parserConfig;default:'{}';not null" json:"parserConfig"`
	PollerCursor         datatypes.JSON `gorm:"column:pollerCursor" json:"pollerCursor"`
	ErrorCount           int            `gorm:"column:errorCount;default:0;not null" json:"errorCount"`
	PullSchedule         *string        `gorm:"column:pullSchedule" json:"pullSchedule"`
	Status               *string        `gorm:"column:status;default:'awaiting_first_event';not null" json:"status"`
	TraceProjectID       *string        `gorm:"column:traceProjectId" json:"traceProjectId"`
	ProviderAccountID    *string        `gorm:"column:providerAccountId" json:"providerAccountId"`
	LastEventAt          *DateTime      `gorm:"column:lastEventAt" json:"lastEventAt"`
	LastSuccessAt        *DateTime      `gorm:"column:lastSuccessAt" json:"lastSuccessAt"`
	LastReadThroughAt    *DateTime      `gorm:"column:lastReadThroughAt" json:"lastReadThroughAt"`
	LastRunCompleteness  *string        `gorm:"column:lastRunCompleteness" json:"lastRunCompleteness"`
	UnpricedUsageSince   *DateTime      `gorm:"column:unpricedUsageSince" json:"unpricedUsageSince"`
	UnpricedUsageThrough *DateTime      `gorm:"column:unpricedUsageThrough" json:"unpricedUsageThrough"`
	ArchivedAt           *DateTime      `gorm:"column:archivedAt" json:"archivedAt"`
	CreatedAt            DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt            DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	CreatedByID          *string        `gorm:"column:createdById" json:"createdById"`
	Organization         *Organization
	Team                 *Team
	CreatedBy            *User `gorm:"foreignKey:CreatedByID"`
}

func (IngestionSource) TableName() string {
	return "IngestionSource"
}

func (m *IngestionSource) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*IngestionSource) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type AnomalyRule struct {
	ID                string         `gorm:"column:id;primaryKey" json:"id"`
	OrganizationID    string         `gorm:"column:organizationId;uniqueIndex:AnomalyRule_organizationId_name_key;index:AnomalyRule_organizationId_idx;not null" json:"organizationId"`
	Scope             string         `gorm:"column:scope;index:AnomalyRule_scope_scopeId_idx;not null" json:"scope"`
	ScopeID           string         `gorm:"column:scopeId;index:AnomalyRule_scope_scopeId_idx;not null" json:"scopeId"`
	Name              string         `gorm:"column:name;uniqueIndex:AnomalyRule_organizationId_name_key;not null" json:"name"`
	Description       *string        `gorm:"column:description" json:"description"`
	Severity          string         `gorm:"column:severity;not null" json:"severity"`
	RuleType          string         `gorm:"column:ruleType;index:AnomalyRule_ruleType_idx;not null" json:"ruleType"`
	ThresholdConfig   datatypes.JSON `gorm:"column:thresholdConfig;default:'{}';not null" json:"thresholdConfig"`
	DestinationConfig datatypes.JSON `gorm:"column:destinationConfig;default:'{}';not null" json:"destinationConfig"`
	Status            *string        `gorm:"column:status;index:AnomalyRule_status_idx;default:'active';not null" json:"status"`
	ArchivedAt        *DateTime      `gorm:"column:archivedAt" json:"archivedAt"`
	CreatedAt         DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt         DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	CreatedByID       *string        `gorm:"column:createdById" json:"createdById"`
	Organization      *Organization
	CreatedBy         *User          `gorm:"foreignKey:CreatedByID"`
	Alerts            []AnomalyAlert `gorm:"foreignKey:RuleID;constraint:OnDelete:CASCADE"`
}

func (AnomalyRule) TableName() string {
	return "AnomalyRule"
}

func (m *AnomalyRule) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*AnomalyRule) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type AnomalyAlert struct {
	ID                 string         `gorm:"column:id;primaryKey" json:"id"`
	OrganizationID     string         `gorm:"column:organizationId;index:AnomalyAlert_organizationId_detectedAt_idx;not null" json:"organizationId"`
	RuleID             string         `gorm:"column:ruleId;uniqueIndex:AnomalyAlert_ruleId_triggerWindowStart_key;index:AnomalyAlert_ruleId_idx;not null" json:"ruleId"`
	Severity           string         `gorm:"column:severity;not null" json:"severity"`
	RuleName           string         `gorm:"column:ruleName;not null" json:"ruleName"`
	RuleType           string         `gorm:"column:ruleType;not null" json:"ruleType"`
	TriggerWindowStart DateTime       `gorm:"column:triggerWindowStart;uniqueIndex:AnomalyAlert_ruleId_triggerWindowStart_key;not null" json:"triggerWindowStart"`
	TriggerWindowEnd   DateTime       `gorm:"column:triggerWindowEnd;not null" json:"triggerWindowEnd"`
	TriggerSpendUsd    *float64       `gorm:"column:triggerSpendUsd;type:decimal(18,6)" json:"triggerSpendUsd"`
	TriggerEventCount  *int           `gorm:"column:triggerEventCount" json:"triggerEventCount"`
	Detail             datatypes.JSON `gorm:"column:detail;default:'{}';not null" json:"detail"`
	State              *string        `gorm:"column:state;index:AnomalyAlert_state_idx;default:'open';not null" json:"state"`
	DestinationStatus  datatypes.JSON `gorm:"column:destinationStatus;default:'{}';not null" json:"destinationStatus"`
	DetectedAt         DateTime       `gorm:"column:detectedAt;index:AnomalyAlert_organizationId_detectedAt_idx;not null" json:"detectedAt"`
	AcknowledgedAt     *DateTime      `gorm:"column:acknowledgedAt" json:"acknowledgedAt"`
	ResolvedAt         *DateTime      `gorm:"column:resolvedAt" json:"resolvedAt"`
	Organization       *Organization
	Rule               *AnomalyRule `gorm:"foreignKey:RuleID"`
}

func (AnomalyAlert) TableName() string {
	return "AnomalyAlert"
}

func (m *AnomalyAlert) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.DetectedAt.IsZero() {
		m.DetectedAt = DateTime{Time: now}
	}
	return nil
}

type AiToolEntry struct {
	ID             string         `gorm:"column:id;primaryKey" json:"id"`
	OrganizationID string         `gorm:"column:organizationId;index:AiToolEntry_organizationId_scope_scopeId_idx;index:AiToolEntry_organizationId_enabled_archivedAt_idx;index:AiToolEntry_organizationId_type_idx;not null" json:"organizationId"`
	Scope          string         `gorm:"column:scope;index:AiToolEntry_organizationId_scope_scopeId_idx;not null" json:"scope"`
	ScopeID        string         `gorm:"column:scopeId;index:AiToolEntry_organizationId_scope_scopeId_idx;not null" json:"scopeId"`
	Type           string         `gorm:"column:type;index:AiToolEntry_organizationId_type_idx;not null" json:"type"`
	DisplayName    string         `gorm:"column:displayName;not null" json:"displayName"`
	Slug           string         `gorm:"column:slug;not null" json:"slug"`
	IconKey        *string        `gorm:"column:iconKey" json:"iconKey"`
	IconAsset      *string        `gorm:"column:iconAsset;type:text" json:"iconAsset"`
	Order          int            `gorm:"column:order;default:0;not null" json:"order"`
	Enabled        *bool          `gorm:"column:enabled;index:AiToolEntry_organizationId_enabled_archivedAt_idx;default:true;not null" json:"enabled"`
	Config         datatypes.JSON `gorm:"column:config;default:'{}';not null" json:"config"`
	ArchivedAt     *DateTime      `gorm:"column:archivedAt;index:AiToolEntry_organizationId_enabled_archivedAt_idx" json:"archivedAt"`
	CreatedAt      DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt      DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	CreatedByID    *string        `gorm:"column:createdById" json:"createdById"`
	UpdatedByID    *string        `gorm:"column:updatedById" json:"updatedById"`
	Organization   *Organization
	Teams          []AiToolEntryTeam       `gorm:"foreignKey:EntryID;constraint:OnDelete:CASCADE"`
	Departments    []AiToolEntryDepartment `gorm:"foreignKey:EntryID;constraint:OnDelete:CASCADE"`
}

func (AiToolEntry) TableName() string {
	return "AiToolEntry"
}

func (m *AiToolEntry) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*AiToolEntry) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type AiToolEntryTeam struct {
	ID      string       `gorm:"column:id;primaryKey" json:"id"`
	EntryID string       `gorm:"column:entryId;uniqueIndex:AiToolEntryTeam_entryId_teamId_key;not null" json:"entryId"`
	TeamID  string       `gorm:"column:teamId;uniqueIndex:AiToolEntryTeam_entryId_teamId_key;index:AiToolEntryTeam_teamId_idx;not null" json:"teamId"`
	Entry   *AiToolEntry `gorm:"foreignKey:EntryID"`
	Team    *Team
}

func (AiToolEntryTeam) TableName() string {
	return "AiToolEntryTeam"
}

func (m *AiToolEntryTeam) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	return nil
}

type AiToolEntryDepartment struct {
	ID           string       `gorm:"column:id;primaryKey" json:"id"`
	EntryID      string       `gorm:"column:entryId;uniqueIndex:AiToolEntryDepartment_entryId_departmentId_key;not null" json:"entryId"`
	DepartmentID string       `gorm:"column:departmentId;uniqueIndex:AiToolEntryDepartment_entryId_departmentId_key;index:AiToolEntryDepartment_departmentId_idx;not null" json:"departmentId"`
	Entry        *AiToolEntry `gorm:"foreignKey:EntryID"`
}

func (AiToolEntryDepartment) TableName() string {
	return "AiToolEntryDepartment"
}

func (m *AiToolEntryDepartment) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	return nil
}

type IngestionTemplate struct {
	ID                string    `gorm:"column:id;primaryKey" json:"id"`
	OrganizationID    *string   `gorm:"column:organizationId;uniqueIndex:IngestionTemplate_organizationId_slug_key;index:IngestionTemplate_organizationId_archivedAt_idx" json:"organizationId"`
	Slug              string    `gorm:"column:slug;uniqueIndex:IngestionTemplate_organizationId_slug_key;not null" json:"slug"`
	SourceType        string    `gorm:"column:sourceType;index:IngestionTemplate_sourceType_idx;not null" json:"sourceType"`
	DisplayName       string    `gorm:"column:displayName;not null" json:"displayName"`
	Description       *string   `gorm:"column:description;type:text" json:"description"`
	IconAsset         *string   `gorm:"column:iconAsset;type:text" json:"iconAsset"`
	CredentialSchema  *string   `gorm:"column:credentialSchema" json:"credentialSchema"`
	OttlRules         string    `gorm:"column:ottlRules;type:text;default:'';not null" json:"ottlRules"`
	PlatformPublished bool      `gorm:"column:platformPublished;default:false;not null" json:"platformPublished"`
	Enabled           *bool     `gorm:"column:enabled;default:true;not null" json:"enabled"`
	ArchivedAt        *DateTime `gorm:"column:archivedAt;index:IngestionTemplate_organizationId_archivedAt_idx" json:"archivedAt"`
	CreatedAt         DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt         DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	CreatedByID       *string   `gorm:"column:createdById" json:"createdById"`
	UpdatedByID       *string   `gorm:"column:updatedById" json:"updatedById"`
	Organization      *Organization
}

func (IngestionTemplate) TableName() string {
	return "IngestionTemplate"
}

func (m *IngestionTemplate) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*IngestionTemplate) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type GatewayConnectUpstream struct {
	OrganizationID string   `gorm:"column:organizationId;primaryKey" json:"organizationId"`
	BaseURL        string   `gorm:"column:baseUrl;not null" json:"baseUrl"`
	EncryptedToken string   `gorm:"column:encryptedToken;not null" json:"encryptedToken"`
	InstanceID     string   `gorm:"column:instanceId;not null" json:"instanceId"`
	UpdatedAt      DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
}

func (GatewayConnectUpstream) TableName() string {
	return "GatewayConnectUpstream"
}

func (m *GatewayConnectUpstream) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*GatewayConnectUpstream) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type GatewayTraceExportKey struct {
	ProjectID      string   `gorm:"column:projectId;primaryKey" json:"projectId"`
	APIKeyID       string   `gorm:"column:apiKeyId;not null" json:"apiKeyId"`
	EncryptedToken string   `gorm:"column:encryptedToken;not null" json:"encryptedToken"`
	CreatedAt      DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
}

func (GatewayTraceExportKey) TableName() string {
	return "GatewayTraceExportKey"
}

func (m *GatewayTraceExportKey) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type GatewayBudget struct {
	ID                     string                        `gorm:"column:id;primaryKey" json:"id"`
	OrganizationID         string                        `gorm:"column:organizationId;uniqueIndex:GatewayBudget_organizationId_externalId_key;index:GatewayBudget_organizationId_idx;not null" json:"organizationId"`
	ScopeType              string                        `gorm:"column:scopeType;index:GatewayBudget_scopeType_scopeId_providerKey_idx;not null" json:"scopeType"`
	ScopeID                string                        `gorm:"column:scopeId;index:GatewayBudget_scopeType_scopeId_providerKey_idx;not null" json:"scopeId"`
	ProviderKey            *string                       `gorm:"column:providerKey;index:GatewayBudget_scopeType_scopeId_providerKey_idx" json:"providerKey"`
	Name                   string                        `gorm:"column:name;not null" json:"name"`
	Description            *string                       `gorm:"column:description" json:"description"`
	Window                 string                        `gorm:"column:window;not null" json:"window"`
	LimitUsd               float64                       `gorm:"column:limitUsd;type:decimal(18,6);not null" json:"limitUsd"`
	OnBreach               string                        `gorm:"column:onBreach;default:'BLOCK';not null" json:"onBreach"`
	Timezone               *string                       `gorm:"column:timezone" json:"timezone"`
	ExternalID             *string                       `gorm:"column:externalId;uniqueIndex:GatewayBudget_organizationId_externalId_key" json:"externalId"`
	Metadata               datatypes.JSON                `gorm:"column:metadata;default:'{}';not null" json:"metadata"`
	SpentUsd               float64                       `gorm:"column:spentUsd;type:decimal(18,6);default:0;not null" json:"spentUsd"`
	CurrentPeriodStartedAt DateTime                      `gorm:"column:currentPeriodStartedAt;not null" json:"currentPeriodStartedAt"`
	ResetsAt               DateTime                      `gorm:"column:resetsAt;index:GatewayBudget_resetsAt_idx;not null" json:"resetsAt"`
	LastResetAt            *DateTime                     `gorm:"column:lastResetAt" json:"lastResetAt"`
	CycleAnchorAt          *DateTime                     `gorm:"column:cycleAnchorAt" json:"cycleAnchorAt"`
	ArchivedAt             *DateTime                     `gorm:"column:archivedAt" json:"archivedAt"`
	CreatedAt              DateTime                      `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt              DateTime                      `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	CreatedByID            string                        `gorm:"column:createdById;not null" json:"createdById"`
	ManagedByVirtualKeyID  *string                       `gorm:"column:managedByVirtualKeyId;index:GatewayBudget_managedByVirtualKeyId_idx" json:"managedByVirtualKeyId"`
	CreatedBy              *User                         `gorm:"foreignKey:CreatedByID"`
	LedgerEntries          []GatewayBudgetLedger         `gorm:"foreignKey:BudgetID;constraint:OnDelete:CASCADE"`
	BucketBoundaries       []GatewayBudgetBucketBoundary `gorm:"foreignKey:BudgetID;constraint:OnDelete:CASCADE"`
}

func (GatewayBudget) TableName() string {
	return "GatewayBudget"
}

func (m *GatewayBudget) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CurrentPeriodStartedAt.IsZero() {
		m.CurrentPeriodStartedAt = DateTime{Time: now}
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*GatewayBudget) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type GatewayBudgetBucketBoundary struct {
	ID              string         `gorm:"column:id;primaryKey" json:"id"`
	OrganizationID  string         `gorm:"column:organizationId;index:GatewayBudgetBucketBoundary_organizationId_idx;not null" json:"organizationId"`
	BudgetID        string         `gorm:"column:budgetId;uniqueIndex:GatewayBudgetBucketBoundary_budgetId_bucketScopeId_key;not null" json:"budgetId"`
	BucketScopeID   string         `gorm:"column:bucketScopeId;uniqueIndex:GatewayBudgetBucketBoundary_budgetId_bucketScopeId_key;not null" json:"bucketScopeId"`
	PeriodStartedAt DateTime       `gorm:"column:periodStartedAt;not null" json:"periodStartedAt"`
	CreatedAt       DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt       DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Budget          *GatewayBudget `gorm:"foreignKey:BudgetID"`
}

func (GatewayBudgetBucketBoundary) TableName() string {
	return "GatewayBudgetBucketBoundary"
}

func (m *GatewayBudgetBucketBoundary) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*GatewayBudgetBucketBoundary) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type GatewayBudgetLedger struct {
	ID               string         `gorm:"column:id;primaryKey" json:"id"`
	BudgetID         string         `gorm:"column:budgetId;uniqueIndex:GatewayBudgetLedger_budgetId_gatewayRequestId_key;not null" json:"budgetId"`
	VirtualKeyID     string         `gorm:"column:virtualKeyId;index:GatewayBudgetLedger_virtualKeyId_idx;not null" json:"virtualKeyId"`
	ModelProviderID  *string        `gorm:"column:modelProviderId" json:"modelProviderId"`
	ProviderKey      *string        `gorm:"column:providerKey" json:"providerKey"`
	GatewayRequestID string         `gorm:"column:gatewayRequestId;uniqueIndex:GatewayBudgetLedger_budgetId_gatewayRequestId_key;index:GatewayBudgetLedger_gatewayRequestId_idx;not null" json:"gatewayRequestId"`
	AmountUsd        float64        `gorm:"column:amountUsd;type:decimal(18,6);not null" json:"amountUsd"`
	TokensInput      int            `gorm:"column:tokensInput;default:0;not null" json:"tokensInput"`
	TokensOutput     int            `gorm:"column:tokensOutput;default:0;not null" json:"tokensOutput"`
	TokensCacheRead  int            `gorm:"column:tokensCacheRead;default:0;not null" json:"tokensCacheRead"`
	TokensCacheWrite int            `gorm:"column:tokensCacheWrite;default:0;not null" json:"tokensCacheWrite"`
	Model            string         `gorm:"column:model;not null" json:"model"`
	DurationMs       *int           `gorm:"column:durationMs" json:"durationMs"`
	Status           string         `gorm:"column:status;default:'SUCCESS';not null" json:"status"`
	OccurredAt       DateTime       `gorm:"column:occurredAt;index:GatewayBudgetLedger_occurredAt_idx;not null" json:"occurredAt"`
	Budget           *GatewayBudget `gorm:"foreignKey:BudgetID"`
	VirtualKey       *VirtualKey
}

func (GatewayBudgetLedger) TableName() string {
	return "GatewayBudgetLedger"
}

func (m *GatewayBudgetLedger) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.OccurredAt.IsZero() {
		m.OccurredAt = DateTime{Time: now}
	}
	return nil
}

type GatewayChangeEvent struct {
	Revision        int64          `gorm:"column:revision;primaryKey;autoIncrement;index:GatewayChangeEvent_organizationId_revision_idx;index:GatewayChangeEvent_projectId_revision_idx" json:"revision"`
	OrganizationID  string         `gorm:"column:organizationId;index:GatewayChangeEvent_organizationId_revision_idx;not null" json:"organizationId"`
	Kind            string         `gorm:"column:kind;not null" json:"kind"`
	VirtualKeyID    *string        `gorm:"column:virtualKeyId;index:GatewayChangeEvent_virtualKeyId_idx" json:"virtualKeyId"`
	BudgetID        *string        `gorm:"column:budgetId;index:GatewayChangeEvent_budgetId_idx" json:"budgetId"`
	ModelProviderID *string        `gorm:"column:modelProviderId" json:"modelProviderId"`
	ProjectID       *string        `gorm:"column:projectId;index:GatewayChangeEvent_projectId_revision_idx" json:"projectId"`
	Payload         datatypes.JSON `gorm:"column:payload" json:"payload"`
	CreatedAt       DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	Organization    *Organization
	Project         *Project
}

func (GatewayChangeEvent) TableName() string {
	return "GatewayChangeEvent"
}

func (m *GatewayChangeEvent) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type GatewayCacheRule struct {
	ID             string         `gorm:"column:id;primaryKey" json:"id"`
	OrganizationID string         `gorm:"column:organizationId;index:GatewayCacheRule_organizationId_archivedAt_priority_idx;index:GatewayCacheRule_organizationId_enabled_priority_idx;not null" json:"organizationId"`
	Name           string         `gorm:"column:name;not null" json:"name"`
	Description    *string        `gorm:"column:description" json:"description"`
	Priority       *int           `gorm:"column:priority;index:GatewayCacheRule_organizationId_archivedAt_priority_idx;index:GatewayCacheRule_organizationId_enabled_priority_idx;default:100;not null" json:"priority"`
	Enabled        *bool          `gorm:"column:enabled;index:GatewayCacheRule_organizationId_enabled_priority_idx;default:true;not null" json:"enabled"`
	Matchers       datatypes.JSON `gorm:"column:matchers;not null" json:"matchers"`
	Action         datatypes.JSON `gorm:"column:action;not null" json:"action"`
	ModeEnum       string         `gorm:"column:modeEnum;not null" json:"modeEnum"`
	ArchivedAt     *DateTime      `gorm:"column:archivedAt;index:GatewayCacheRule_organizationId_archivedAt_priority_idx" json:"archivedAt"`
	CreatedAt      DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt      DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	CreatedByID    string         `gorm:"column:createdById;not null" json:"createdById"`
}

func (GatewayCacheRule) TableName() string {
	return "GatewayCacheRule"
}

func (m *GatewayCacheRule) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*GatewayCacheRule) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type FeatureFlag struct {
	Key          string         `gorm:"column:key;primaryKey" json:"key"`
	Enabled      bool           `gorm:"column:enabled;not null" json:"enabled"`
	Rules        datatypes.JSON `gorm:"column:rules" json:"rules"`
	LastEditedBy *string        `gorm:"column:lastEditedBy" json:"lastEditedBy"`
	CreatedAt    DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt    DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
}

func (FeatureFlag) TableName() string {
	return "FeatureFlag"
}

func (m *FeatureFlag) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*FeatureFlag) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type FeatureFlagExperimentSetting struct {
	FlagKey         string   `gorm:"column:flagKey;primaryKey" json:"flagKey"`
	SubjectType     string   `gorm:"column:subjectType;primaryKey;index:FeatureFlagExperimentSetting_subjectType_subjectId_idx" json:"subjectType"`
	SubjectID       string   `gorm:"column:subjectId;primaryKey;index:FeatureFlagExperimentSetting_subjectType_subjectId_idx" json:"subjectId"`
	Enabled         bool     `gorm:"column:enabled;not null" json:"enabled"`
	ChangedByUserID *string  `gorm:"column:changedByUserId" json:"changedByUserId"`
	CreatedAt       DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt       DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
}

func (FeatureFlagExperimentSetting) TableName() string {
	return "FeatureFlagExperimentSetting"
}

func (m *FeatureFlagExperimentSetting) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*FeatureFlagExperimentSetting) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type ProcessManagerInstance struct {
	ID          string         `gorm:"column:id;primaryKey" json:"id"`
	ProcessName string         `gorm:"column:processName;uniqueIndex:ProcessManagerInstance_processName_projectId_processKey_key;not null" json:"processName"`
	ProjectID   string         `gorm:"column:projectId;uniqueIndex:ProcessManagerInstance_processName_projectId_processKey_key;not null" json:"projectId"`
	ProcessKey  string         `gorm:"column:processKey;uniqueIndex:ProcessManagerInstance_processName_projectId_processKey_key;not null" json:"processKey"`
	TenantID    string         `gorm:"column:tenantId;not null" json:"tenantId"`
	UserID      *string        `gorm:"column:userId" json:"userId"`
	State       datatypes.JSON `gorm:"column:state;not null" json:"state"`
	Revision    int            `gorm:"column:revision;not null" json:"revision"`
	NextWakeAt  *DateTime      `gorm:"column:nextWakeAt;index:ProcessManagerInstance_nextWakeAt_idx;type:timestamptz(3)" json:"nextWakeAt"`
	UpdatedAt   DateTime       `gorm:"column:updatedAt;type:timestamptz(3);autoUpdateTime:false;not null" json:"updatedAt"`
}

func (ProcessManagerInstance) TableName() string {
	return "ProcessManagerInstance"
}

func (m *ProcessManagerInstance) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	return nil
}

type ProcessManagerInbox struct {
	ID             string   `gorm:"column:id;primaryKey" json:"id"`
	ProcessName    string   `gorm:"column:processName;uniqueIndex:ProcessManagerInbox_processName_projectId_sourceEventKey_key;index:ProcessManagerInbox_processName_projectId_processKey_consumedAt_idx;not null" json:"processName"`
	ProjectID      string   `gorm:"column:projectId;uniqueIndex:ProcessManagerInbox_processName_projectId_sourceEventKey_key;index:ProcessManagerInbox_processName_projectId_processKey_consumedAt_idx;not null" json:"projectId"`
	ProcessKey     string   `gorm:"column:processKey;index:ProcessManagerInbox_processName_projectId_processKey_consumedAt_idx;not null" json:"processKey"`
	TenantID       string   `gorm:"column:tenantId;not null" json:"tenantId"`
	SourceEventID  string   `gorm:"column:sourceEventId;not null" json:"sourceEventId"`
	SourceEventKey *string  `gorm:"column:sourceEventKey;uniqueIndex:ProcessManagerInbox_processName_projectId_sourceEventKey_key" json:"sourceEventKey"`
	ConsumedAt     DateTime `gorm:"column:consumedAt;index:ProcessManagerInbox_processName_projectId_processKey_consumedAt_idx;index:ProcessManagerInbox_consumedAt_idx;type:timestamptz(3);not null" json:"consumedAt"`
}

func (ProcessManagerInbox) TableName() string {
	return "ProcessManagerInbox"
}

func (m *ProcessManagerInbox) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	return nil
}

type ProcessManagerOutbox struct {
	ID            string                        `gorm:"column:id;primaryKey" json:"id"`
	ProcessName   string                        `gorm:"column:processName;uniqueIndex:ProcessManagerOutbox_processName_projectId_messageKey_key;index:ProcessManagerOutbox_processName_projectId_processKey_createdAt_idx;index:ProcessManagerOutbox_status_processName_nextAttemptAt_idx;not null" json:"processName"`
	ProjectID     string                        `gorm:"column:projectId;uniqueIndex:ProcessManagerOutbox_processName_projectId_messageKey_key;index:ProcessManagerOutbox_processName_projectId_processKey_createdAt_idx;not null" json:"projectId"`
	ProcessKey    string                        `gorm:"column:processKey;index:ProcessManagerOutbox_processName_projectId_processKey_createdAt_idx;not null" json:"processKey"`
	TenantID      string                        `gorm:"column:tenantId;not null" json:"tenantId"`
	UserID        *string                       `gorm:"column:userId" json:"userId"`
	MessageKey    string                        `gorm:"column:messageKey;uniqueIndex:ProcessManagerOutbox_processName_projectId_messageKey_key;not null" json:"messageKey"`
	IntentType    string                        `gorm:"column:intentType;not null" json:"intentType"`
	Payload       datatypes.JSON                `gorm:"column:payload;not null" json:"payload"`
	TraceCarrier  datatypes.JSON                `gorm:"column:traceCarrier;not null" json:"traceCarrier"`
	SourceEventID *string                       `gorm:"column:sourceEventId" json:"sourceEventId"`
	Status        string                        `gorm:"column:status;index:ProcessManagerOutbox_status_nextAttemptAt_leasedUntil_idx;index:ProcessManagerOutbox_status_processName_nextAttemptAt_idx;default:'pending';not null" json:"status"`
	Attempts      int                           `gorm:"column:attempts;default:0;not null" json:"attempts"`
	NextAttemptAt DateTime                      `gorm:"column:nextAttemptAt;index:ProcessManagerOutbox_status_nextAttemptAt_leasedUntil_idx;index:ProcessManagerOutbox_status_processName_nextAttemptAt_idx;type:timestamptz(3);not null" json:"nextAttemptAt"`
	LeasedUntil   *DateTime                     `gorm:"column:leasedUntil;index:ProcessManagerOutbox_status_nextAttemptAt_leasedUntil_idx;type:timestamptz(3)" json:"leasedUntil"`
	LeaseToken    *string                       `gorm:"column:leaseToken;uniqueIndex" json:"leaseToken"`
	DispatchedAt  *DateTime                     `gorm:"column:dispatchedAt;type:timestamptz(3)" json:"dispatchedAt"`
	CreatedAt     DateTime                      `gorm:"column:createdAt;index:ProcessManagerOutbox_processName_projectId_processKey_createdAt_idx;type:timestamptz(3);autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt     DateTime                      `gorm:"column:updatedAt;type:timestamptz(3);autoUpdateTime:false;not null" json:"updatedAt"`
	AttemptLog    []ProcessManagerOutboxAttempt `gorm:"foreignKey:OutboxID;constraint:OnDelete:CASCADE"`
}

func (ProcessManagerOutbox) TableName() string {
	return "ProcessManagerOutbox"
}

func (m *ProcessManagerOutbox) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	return nil
}

type ProcessManagerOutboxAttempt struct {
	ID           string                `gorm:"column:id;primaryKey" json:"id"`
	OutboxID     string                `gorm:"column:outboxId;index:ProcessManagerOutboxAttempt_outboxId_attempt_idx;not null" json:"outboxId"`
	ProjectID    string                `gorm:"column:projectId;not null" json:"projectId"`
	Attempt      int                   `gorm:"column:attempt;index:ProcessManagerOutboxAttempt_outboxId_attempt_idx;not null" json:"attempt"`
	OccurredAt   DateTime              `gorm:"column:occurredAt;type:timestamptz(3);not null" json:"occurredAt"`
	Outcome      string                `gorm:"column:outcome;not null" json:"outcome"`
	ErrorType    string                `gorm:"column:errorType;not null" json:"errorType"`
	ErrorMessage string                `gorm:"column:errorMessage;not null" json:"errorMessage"`
	RetryAfterMs *int                  `gorm:"column:retryAfterMs" json:"retryAfterMs"`
	Outbox       *ProcessManagerOutbox `gorm:"foreignKey:OutboxID"`
}

func (ProcessManagerOutboxAttempt) TableName() string {
	return "ProcessManagerOutboxAttempt"
}

func (m *ProcessManagerOutboxAttempt) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	return nil
}

type TopicClusteringRunProjection struct {
	ID                         string   `gorm:"column:id;primaryKey" json:"id"`
	ProjectID                  string   `gorm:"column:projectId;uniqueIndex;not null" json:"projectId"`
	LastRequestedAt            *float64 `gorm:"column:lastRequestedAt" json:"lastRequestedAt"`
	LastRequestTrigger         *string  `gorm:"column:lastRequestTrigger" json:"lastRequestTrigger"`
	LastRunAt                  *float64 `gorm:"column:lastRunAt" json:"lastRunAt"`
	LastRunOutcome             *string  `gorm:"column:lastRunOutcome" json:"lastRunOutcome"`
	LastRunMode                *string  `gorm:"column:lastRunMode" json:"lastRunMode"`
	LastRunSkippedReason       *string  `gorm:"column:lastRunSkippedReason" json:"lastRunSkippedReason"`
	LastRunError               *string  `gorm:"column:lastRunError" json:"lastRunError"`
	LastRunErrorCode           *string  `gorm:"column:lastRunErrorCode" json:"lastRunErrorCode"`
	LastRunErrorUserActionable bool     `gorm:"column:lastRunErrorUserActionable;default:false;not null" json:"lastRunErrorUserActionable"`
	LastRunTracesProcessed     int      `gorm:"column:lastRunTracesProcessed;default:0;not null" json:"lastRunTracesProcessed"`
	LastRunTopicsCount         int      `gorm:"column:lastRunTopicsCount;default:0;not null" json:"lastRunTopicsCount"`
	LastRunSubtopicsCount      int      `gorm:"column:lastRunSubtopicsCount;default:0;not null" json:"lastRunSubtopicsCount"`
	LastRunPages               int      `gorm:"column:lastRunPages;default:0;not null" json:"lastRunPages"`
	InProgressRunID            *string  `gorm:"column:inProgressRunId" json:"inProgressRunId"`
	InProgressTraces           int      `gorm:"column:inProgressTraces;default:0;not null" json:"inProgressTraces"`
	InProgressPages            int      `gorm:"column:inProgressPages;default:0;not null" json:"inProgressPages"`
	InProgressStartedAt        *float64 `gorm:"column:inProgressStartedAt" json:"inProgressStartedAt"`
	CreatedAt                  float64  `gorm:"column:createdAt;not null" json:"createdAt"`
	UpdatedAt                  float64  `gorm:"column:updatedAt;not null" json:"updatedAt"`
	OccurredAt                 float64  `gorm:"column:occurredAt;not null" json:"occurredAt"`
	AcceptedAt                 float64  `gorm:"column:acceptedAt;not null" json:"acceptedAt"`
	LastEventID                string   `gorm:"column:lastEventId;not null" json:"lastEventId"`
	ProjectionVersion          string   `gorm:"column:projectionVersion;not null" json:"projectionVersion"`
}

func (TopicClusteringRunProjection) TableName() string {
	return "TopicClusteringRunProjection"
}

type TopicModelProjection struct {
	ID                string  `gorm:"column:id;primaryKey" json:"id"`
	ProjectID         string  `gorm:"column:projectId;uniqueIndex;not null" json:"projectId"`
	CreatedAt         float64 `gorm:"column:createdAt;not null" json:"createdAt"`
	UpdatedAt         float64 `gorm:"column:updatedAt;not null" json:"updatedAt"`
	OccurredAt        float64 `gorm:"column:occurredAt;not null" json:"occurredAt"`
	AcceptedAt        float64 `gorm:"column:acceptedAt;not null" json:"acceptedAt"`
	LastEventID       string  `gorm:"column:lastEventId;not null" json:"lastEventId"`
	ProjectionVersion string  `gorm:"column:projectionVersion;not null" json:"projectionVersion"`
}

func (TopicModelProjection) TableName() string {
	return "TopicModelProjection"
}

type TopicClusteringRunHistoryProjection struct {
	ID                string         `gorm:"column:id;primaryKey" json:"id"`
	ProjectID         string         `gorm:"column:projectId;uniqueIndex;not null" json:"projectId"`
	Runs              datatypes.JSON `gorm:"column:runs;not null" json:"runs"`
	CreatedAt         float64        `gorm:"column:createdAt;not null" json:"createdAt"`
	UpdatedAt         float64        `gorm:"column:updatedAt;not null" json:"updatedAt"`
	OccurredAt        float64        `gorm:"column:occurredAt;not null" json:"occurredAt"`
	AcceptedAt        float64        `gorm:"column:acceptedAt;not null" json:"acceptedAt"`
	LastEventID       string         `gorm:"column:lastEventId;not null" json:"lastEventId"`
	ProjectionVersion string         `gorm:"column:projectionVersion;not null" json:"projectionVersion"`
}

func (TopicClusteringRunHistoryProjection) TableName() string {
	return "TopicClusteringRunHistoryProjection"
}

type IngestionPullRunProjection struct {
	ID                       string   `gorm:"column:id;primaryKey" json:"id"`
	SourceID                 string   `gorm:"column:sourceId;uniqueIndex;not null" json:"sourceId"`
	ProjectID                string   `gorm:"column:projectId;index:IngestionPullRunProjection_projectId_idx;not null" json:"projectId"`
	Enabled                  bool     `gorm:"column:enabled;default:false;not null" json:"enabled"`
	Cron                     *string  `gorm:"column:cron" json:"cron"`
	Cursor                   *string  `gorm:"column:cursor" json:"cursor"`
	LastRunAt                *float64 `gorm:"column:lastRunAt" json:"lastRunAt"`
	LastRunOutcome           *string  `gorm:"column:lastRunOutcome" json:"lastRunOutcome"`
	LastRunEventCount        int      `gorm:"column:lastRunEventCount;default:0;not null" json:"lastRunEventCount"`
	LastRunError             *string  `gorm:"column:lastRunError" json:"lastRunError"`
	LastRunErrorCode         *string  `gorm:"column:lastRunErrorCode" json:"lastRunErrorCode"`
	ConsecutiveErrors        int      `gorm:"column:consecutiveErrors;default:0;not null" json:"consecutiveErrors"`
	LastSuccessAt            *float64 `gorm:"column:lastSuccessAt" json:"lastSuccessAt"`
	LastRunScheduledFor      *float64 `gorm:"column:lastRunScheduledFor" json:"lastRunScheduledFor"`
	LastReadThroughAt        *float64 `gorm:"column:lastReadThroughAt" json:"lastReadThroughAt"`
	LastRunCompleteness      *string  `gorm:"column:lastRunCompleteness" json:"lastRunCompleteness"`
	LastAgentsListingAt      *float64 `gorm:"column:lastAgentsListingAt" json:"lastAgentsListingAt"`
	LastAgentsListingOutcome *string  `gorm:"column:lastAgentsListingOutcome" json:"lastAgentsListingOutcome"`
	LastAgentsListingCount   *int     `gorm:"column:lastAgentsListingCount" json:"lastAgentsListingCount"`
	LastAgentsListingReason  *string  `gorm:"column:lastAgentsListingReason" json:"lastAgentsListingReason"`
	LastAgentsListingStatus  *int     `gorm:"column:lastAgentsListingStatus" json:"lastAgentsListingStatus"`
	LastPeopleListingAt      *float64 `gorm:"column:lastPeopleListingAt" json:"lastPeopleListingAt"`
	LastPeopleListingOutcome *string  `gorm:"column:lastPeopleListingOutcome" json:"lastPeopleListingOutcome"`
	LastPeopleDirectoryCount *int     `gorm:"column:lastPeopleDirectoryCount" json:"lastPeopleDirectoryCount"`
	LastPeopleWithheldCount  *int     `gorm:"column:lastPeopleWithheldCount" json:"lastPeopleWithheldCount"`
	LastPeopleListingReason  *string  `gorm:"column:lastPeopleListingReason" json:"lastPeopleListingReason"`
	LastPeopleListingStatus  *int     `gorm:"column:lastPeopleListingStatus" json:"lastPeopleListingStatus"`
	CreatedAt                float64  `gorm:"column:createdAt;not null" json:"createdAt"`
	UpdatedAt                float64  `gorm:"column:updatedAt;not null" json:"updatedAt"`
	OccurredAt               float64  `gorm:"column:occurredAt;not null" json:"occurredAt"`
	AcceptedAt               float64  `gorm:"column:acceptedAt;not null" json:"acceptedAt"`
	LastEventID              string   `gorm:"column:lastEventId;not null" json:"lastEventId"`
	ProjectionVersion        string   `gorm:"column:projectionVersion;not null" json:"projectionVersion"`
}

func (IngestionPullRunProjection) TableName() string {
	return "IngestionPullRunProjection"
}

type ScheduledJob struct {
	ID          string    `gorm:"column:id;primaryKey" json:"id"`
	ProjectID   string    `gorm:"column:projectId;index:ScheduledJob_projectId_idx;not null" json:"projectId"`
	TargetType  string    `gorm:"column:targetType;uniqueIndex:ScheduledJob_targetType_targetId_key;not null" json:"targetType"`
	TargetID    string    `gorm:"column:targetId;uniqueIndex:ScheduledJob_targetType_targetId_key;not null" json:"targetId"`
	Cron        string    `gorm:"column:cron;not null" json:"cron"`
	Timezone    string    `gorm:"column:timezone;not null" json:"timezone"`
	NextRunAt   DateTime  `gorm:"column:nextRunAt;index:ScheduledJob_active_nextRunAt_idx;not null" json:"nextRunAt"`
	LastSlot    *DateTime `gorm:"column:lastSlot" json:"lastSlot"`
	CurrentSlot *DateTime `gorm:"column:currentSlot" json:"currentSlot"`
	Attempts    int       `gorm:"column:attempts;default:0;not null" json:"attempts"`
	LastError   *string   `gorm:"column:lastError" json:"lastError"`
	Active      *bool     `gorm:"column:active;index:ScheduledJob_active_nextRunAt_idx;default:true;not null" json:"active"`
	CreatedAt   DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt   DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
}

func (ScheduledJob) TableName() string {
	return "ScheduledJob"
}

func (m *ScheduledJob) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*ScheduledJob) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type BugReport struct {
	ID               string         `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt        DateTime       `gorm:"column:createdAt;index:BugReport_createdAt_idx;type:timestamptz(3);autoCreateTime:false;not null" json:"createdAt"`
	Source           string         `gorm:"column:source;not null" json:"source"`
	Kind             string         `gorm:"column:kind;not null" json:"kind"`
	Title            string         `gorm:"column:title;not null" json:"title"`
	Summary          *string        `gorm:"column:summary" json:"summary"`
	SessionData      *string        `gorm:"column:sessionData" json:"sessionData"`
	SessionTruncated bool           `gorm:"column:sessionTruncated;default:false;not null" json:"sessionTruncated"`
	Agent            *string        `gorm:"column:agent" json:"agent"`
	ContactEmail     *string        `gorm:"column:contactEmail" json:"contactEmail"`
	CliVersion       *string        `gorm:"column:cliVersion" json:"cliVersion"`
	LinkedProjectID  *string        `gorm:"column:linkedProjectId" json:"linkedProjectId"`
	Metadata         datatypes.JSON `gorm:"column:metadata" json:"metadata"`
}

func (BugReport) TableName() string {
	return "BugReport"
}

func (m *BugReport) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type IdempotencyReceipt struct {
	ID                 string   `gorm:"column:id;primaryKey" json:"id"`
	ScopeID            string   `gorm:"column:scopeId;uniqueIndex:IdempotencyReceipt_scopeId_key_key;not null" json:"scopeId"`
	Key                string   `gorm:"column:key;uniqueIndex:IdempotencyReceipt_scopeId_key_key;not null" json:"key"`
	ClaimID            string   `gorm:"column:claimId;not null" json:"claimId"`
	RequestFingerprint string   `gorm:"column:requestFingerprint;not null" json:"requestFingerprint"`
	ResponseStatus     *int     `gorm:"column:responseStatus" json:"responseStatus"`
	ResponseBody       *string  `gorm:"column:responseBody;type:text" json:"responseBody"`
	CreatedAt          DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	HeartbeatAt        DateTime `gorm:"column:heartbeatAt;not null" json:"heartbeatAt"`
	ExpiresAt          DateTime `gorm:"column:expiresAt;index:IdempotencyReceipt_expiresAt_idx;not null" json:"expiresAt"`
}

func (IdempotencyReceipt) TableName() string {
	return "IdempotencyReceipt"
}

func (m *IdempotencyReceipt) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type GatewayRealtimeSession struct {
	ID                   string         `gorm:"column:id;primaryKey" json:"id"`
	ProjectID            string         `gorm:"column:projectId;index:GatewayRealtimeSession_projectId_status_mintedAt_idx;not null" json:"projectId"`
	OrganizationID       string         `gorm:"column:organizationId;not null" json:"organizationId"`
	VirtualKeyID         string         `gorm:"column:virtualKeyId;index:GatewayRealtimeSession_virtualKeyId_status_mintedAt_idx;not null" json:"virtualKeyId"`
	ModelProviderID      string         `gorm:"column:modelProviderId;not null" json:"modelProviderId"`
	Vendor               string         `gorm:"column:vendor;uniqueIndex:GatewayRealtimeSession_vendor_vendorConversationId_key;not null" json:"vendor"`
	AgentID              *string        `gorm:"column:agentId" json:"agentId"`
	Model                string         `gorm:"column:model;not null" json:"model"`
	RequestedModel       *string        `gorm:"column:requestedModel" json:"requestedModel"`
	VendorConversationID *string        `gorm:"column:vendorConversationId;uniqueIndex:GatewayRealtimeSession_vendor_vendorConversationId_key" json:"vendorConversationId"`
	Status               string         `gorm:"column:status;index:GatewayRealtimeSession_virtualKeyId_status_mintedAt_idx;index:GatewayRealtimeSession_projectId_status_mintedAt_idx;index:GatewayRealtimeSession_status_metering_mintedAt_idx;default:'OPEN';not null" json:"status"`
	MintedAt             DateTime       `gorm:"column:mintedAt;index:GatewayRealtimeSession_virtualKeyId_status_mintedAt_idx;index:GatewayRealtimeSession_projectId_status_mintedAt_idx;index:GatewayRealtimeSession_status_metering_mintedAt_idx;not null" json:"mintedAt"`
	ClosedAt             *DateTime      `gorm:"column:closedAt" json:"closedAt"`
	CloseReason          *string        `gorm:"column:closeReason" json:"closeReason"`
	TraceID              *string        `gorm:"column:traceId" json:"traceId"`
	VendorCostRaw        datatypes.JSON `gorm:"column:vendorCostRaw" json:"vendorCostRaw"`
	Kind                 *string        `gorm:"column:kind" json:"kind"`
	Metering             *string        `gorm:"column:metering;index:GatewayRealtimeSession_status_metering_mintedAt_idx" json:"metering"`
	CredentialExpiresAt  *DateTime      `gorm:"column:credentialExpiresAt" json:"credentialExpiresAt"`
	TranscriptionModel   *string        `gorm:"column:transcriptionModel" json:"transcriptionModel"`
	EndUserID            *string        `gorm:"column:endUserId" json:"endUserId"`
	LastReportAt         *DateTime      `gorm:"column:lastReportAt" json:"lastReportAt"`
	ReportedCostNanoUsd  int64          `gorm:"column:reportedCostNanoUsd;default:0;not null" json:"reportedCostNanoUsd"`
	ReportCount          int            `gorm:"column:reportCount;default:0;not null" json:"reportCount"`
	CreatedAt            DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt            DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
}

func (GatewayRealtimeSession) TableName() string {
	return "GatewayRealtimeSession"
}

func (m *GatewayRealtimeSession) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.MintedAt.IsZero() {
		m.MintedAt = DateTime{Time: now}
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*GatewayRealtimeSession) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type GatewayRealtimeSessionReport struct {
	ID          string         `gorm:"column:id;primaryKey" json:"id"`
	SessionID   string         `gorm:"column:sessionId;uniqueIndex:GatewayRealtimeSessionReport_sessionId_reportKey_key;not null" json:"sessionId"`
	ReportKey   string         `gorm:"column:reportKey;uniqueIndex:GatewayRealtimeSessionReport_sessionId_reportKey_key;not null" json:"reportKey"`
	ProjectID   string         `gorm:"column:projectId;index:GatewayRealtimeSessionReport_projectId_idx;not null" json:"projectId"`
	Model       string         `gorm:"column:model;not null" json:"model"`
	Usage       datatypes.JSON `gorm:"column:usage;not null" json:"usage"`
	CostNanoUsd int64          `gorm:"column:costNanoUsd;not null" json:"costNanoUsd"`
	CreatedAt   DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
}

func (GatewayRealtimeSessionReport) TableName() string {
	return "GatewayRealtimeSessionReport"
}

func (m *GatewayRealtimeSessionReport) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type SystemMigrationTenantState struct {
	MigrationName string         `gorm:"column:migrationName;primaryKey;index:SystemMigrationTenantState_migrationName_status_idx" json:"migrationName"`
	TenantID      string         `gorm:"column:tenantId;primaryKey" json:"tenantId"`
	Status        string         `gorm:"column:status;index:SystemMigrationTenantState_migrationName_status_idx;not null" json:"status"`
	Report        datatypes.JSON `gorm:"column:report" json:"report"`
	OccurredAt    DateTime       `gorm:"column:occurredAt;not null" json:"occurredAt"`
	CreatedAt     DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt     DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
}

func (SystemMigrationTenantState) TableName() string {
	return "SystemMigrationTenantState"
}

func (m *SystemMigrationTenantState) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.OccurredAt.IsZero() {
		m.OccurredAt = DateTime{Time: now}
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*SystemMigrationTenantState) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type SystemMigrationEnrollment struct {
	OrganizationID   string   `gorm:"column:organizationId;primaryKey" json:"organizationId"`
	MigrationName    string   `gorm:"column:migrationName;primaryKey;index:SystemMigrationEnrollment_migrationName_idx" json:"migrationName"`
	EnrolledByUserID string   `gorm:"column:enrolledByUserId;not null" json:"enrolledByUserId"`
	CreatedAt        DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
}

func (SystemMigrationEnrollment) TableName() string {
	return "SystemMigrationEnrollment"
}

func (m *SystemMigrationEnrollment) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type StoredObject struct {
	TenantID                  string         `gorm:"column:tenantId;primaryKey;index:StoredObject_tenantId_status_id_idx" json:"tenantId"`
	ID                        string         `gorm:"column:id;primaryKey;index:StoredObject_tenantId_status_id_idx" json:"id"`
	Status                    string         `gorm:"column:status;index:StoredObject_tenantId_status_id_idx;index:StoredObject_ownerKind_ownerId_status_idx;index:StoredObject_status_expiresAt_idx;not null" json:"status"`
	Purpose                   string         `gorm:"column:purpose;not null" json:"purpose"`
	OwnerKind                 string         `gorm:"column:ownerKind;index:StoredObject_ownerKind_ownerId_status_idx;not null" json:"ownerKind"`
	OwnerID                   string         `gorm:"column:ownerId;index:StoredObject_ownerKind_ownerId_status_idx;not null" json:"ownerId"`
	Filename                  string         `gorm:"column:filename;not null" json:"filename"`
	Sha256                    string         `gorm:"column:sha256;not null" json:"sha256"`
	SizeBytes                 int64          `gorm:"column:sizeBytes;not null" json:"sizeBytes"`
	MediaType                 string         `gorm:"column:mediaType;not null" json:"mediaType"`
	MediaTypeVerified         bool           `gorm:"column:mediaTypeVerified;default:false;not null" json:"mediaTypeVerified"`
	StorageProvider           *string        `gorm:"column:storageProvider" json:"storageProvider"`
	StorageDestinationID      *string        `gorm:"column:storageDestinationId" json:"storageDestinationId"`
	StorageProviderRelativeID *string        `gorm:"column:storageProviderRelativeId;type:text" json:"storageProviderRelativeId"`
	Generation                int            `gorm:"column:generation;default:0;not null" json:"generation"`
	Audiences                 datatypes.JSON `gorm:"column:audiences;not null" json:"audiences"`
	ExpiresAt                 *DateTime      `gorm:"column:expiresAt;index:StoredObject_status_expiresAt_idx" json:"expiresAt"`
	AvailableAt               *DateTime      `gorm:"column:availableAt" json:"availableAt"`
	DeletedAt                 *DateTime      `gorm:"column:deletedAt" json:"deletedAt"`
	Source                    string         `gorm:"column:source;not null" json:"source"`
	LegacyFingerprint         *string        `gorm:"column:legacyFingerprint" json:"legacyFingerprint"`
	CreatedAt                 DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt                 DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
}

func (StoredObject) TableName() string {
	return "StoredObject"
}

func (m *StoredObject) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*StoredObject) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type DiscoveredPerson struct {
	ID                 string    `gorm:"column:id;primaryKey" json:"id"`
	OrganizationID     string    `gorm:"column:organizationId;uniqueIndex:DiscoveredPerson_organizationId_provider_rawActorId_key;index:DiscoveredPerson_organizationId_lastSeenAt_idx;not null" json:"organizationId"`
	Provider           string    `gorm:"column:provider;uniqueIndex:DiscoveredPerson_organizationId_provider_rawActorId_key;not null" json:"provider"`
	RawActorID         string    `gorm:"column:rawActorId;uniqueIndex:DiscoveredPerson_organizationId_provider_rawActorId_key;not null" json:"rawActorId"`
	DisplayText        string    `gorm:"column:displayText;not null" json:"displayText"`
	Kind               string    `gorm:"column:kind;not null" json:"kind"`
	Department         *string   `gorm:"column:department" json:"department"`
	FirstSeenAt        DateTime  `gorm:"column:firstSeenAt;not null" json:"firstSeenAt"`
	LastSeenAt         DateTime  `gorm:"column:lastSeenAt;index:DiscoveredPerson_organizationId_lastSeenAt_idx;not null" json:"lastSeenAt"`
	ErasedAt           *DateTime `gorm:"column:erasedAt" json:"erasedAt"`
	MoneyRowsPendingAt *DateTime `gorm:"column:moneyRowsPendingAt" json:"moneyRowsPendingAt"`
	MoneyRebuildSince  *string   `gorm:"column:moneyRebuildSince" json:"moneyRebuildSince"`
	SuspendedAt        *DateTime `gorm:"column:suspendedAt" json:"suspendedAt"`
	SuspendedReason    *string   `gorm:"column:suspendedReason" json:"suspendedReason"`
	CreatedAt          DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt          DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
}

func (DiscoveredPerson) TableName() string {
	return "DiscoveredPerson"
}

func (m *DiscoveredPerson) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*DiscoveredPerson) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type DiscoveredAgent struct {
	ID             string         `gorm:"column:id;primaryKey" json:"id"`
	OrganizationID string         `gorm:"column:organizationId;uniqueIndex:DiscoveredAgent_organizationId_provider_rawAgentId_key;index:DiscoveredAgent_organizationId_lastSeenAt_idx;not null" json:"organizationId"`
	Provider       string         `gorm:"column:provider;uniqueIndex:DiscoveredAgent_organizationId_provider_rawAgentId_key;not null" json:"provider"`
	RawAgentID     string         `gorm:"column:rawAgentId;uniqueIndex:DiscoveredAgent_organizationId_provider_rawAgentId_key;not null" json:"rawAgentId"`
	DisplayText    string         `gorm:"column:displayText;not null" json:"displayText"`
	Metadata       datatypes.JSON `gorm:"column:metadata;not null" json:"metadata"`
	FirstSeenAt    DateTime       `gorm:"column:firstSeenAt;not null" json:"firstSeenAt"`
	LastSeenAt     DateTime       `gorm:"column:lastSeenAt;index:DiscoveredAgent_organizationId_lastSeenAt_idx;not null" json:"lastSeenAt"`
	CreatedAt      DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt      DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
}

func (DiscoveredAgent) TableName() string {
	return "DiscoveredAgent"
}

func (m *DiscoveredAgent) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*DiscoveredAgent) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type IdentityMatch struct {
	ID                 string    `gorm:"column:id;primaryKey" json:"id"`
	OrganizationID     string    `gorm:"column:organizationId;index:IdentityMatch_organizationId_discoveredPersonId_idx;index:IdentityMatch_organizationId_userId_idx;not null" json:"organizationId"`
	DiscoveredPersonID string    `gorm:"column:discoveredPersonId;index:IdentityMatch_organizationId_discoveredPersonId_idx;not null" json:"discoveredPersonId"`
	UserID             *string   `gorm:"column:userId;index:IdentityMatch_organizationId_userId_idx" json:"userId"`
	EvidenceKind       string    `gorm:"column:evidenceKind;not null" json:"evidenceKind"`
	ValidFrom          DateTime  `gorm:"column:validFrom;not null" json:"validFrom"`
	ValidTo            *DateTime `gorm:"column:validTo" json:"validTo"`
	CreatedAt          DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
}

func (IdentityMatch) TableName() string {
	return "IdentityMatch"
}

func (m *IdentityMatch) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type GovernanceTenantHistory struct {
	ID             string   `gorm:"column:id;primaryKey" json:"id"`
	OrganizationID string   `gorm:"column:organizationId;uniqueIndex:GovernanceTenantHistory_organizationId_tenantId_key;index:GovernanceTenantHistory_organizationId_idx;not null" json:"organizationId"`
	TenantID       string   `gorm:"column:tenantId;uniqueIndex:GovernanceTenantHistory_organizationId_tenantId_key;not null" json:"tenantId"`
	FirstUsedAt    DateTime `gorm:"column:firstUsedAt;not null" json:"firstUsedAt"`
	LastUsedAt     DateTime `gorm:"column:lastUsedAt;not null" json:"lastUsedAt"`
}

func (GovernanceTenantHistory) TableName() string {
	return "GovernanceTenantHistory"
}

func (m *GovernanceTenantHistory) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	return nil
}

type ErasedIdentifierSuppression struct {
	ID             string   `gorm:"column:id;primaryKey" json:"id"`
	OrganizationID string   `gorm:"column:organizationId;uniqueIndex:ErasedIdentifierSuppression_organizationId_provider_identifierHash_key;index:ErasedIdentifierSuppression_organizationId_idx;not null" json:"organizationId"`
	Provider       string   `gorm:"column:provider;uniqueIndex:ErasedIdentifierSuppression_organizationId_provider_identifierHash_key;not null" json:"provider"`
	IdentifierHash string   `gorm:"column:identifierHash;uniqueIndex:ErasedIdentifierSuppression_organizationId_provider_identifierHash_key;not null" json:"identifierHash"`
	ErasedAt       DateTime `gorm:"column:erasedAt;not null" json:"erasedAt"`
}

func (ErasedIdentifierSuppression) TableName() string {
	return "ErasedIdentifierSuppression"
}

func (m *ErasedIdentifierSuppression) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	return nil
}

type IdentityMatchSuggestion struct {
	ID                 string   `gorm:"column:id;primaryKey" json:"id"`
	OrganizationID     string   `gorm:"column:organizationId;uniqueIndex:IdentityMatchSuggestion_organizationId_discoveredPersonId_userId_key;index:IdentityMatchSuggestion_organizationId_score_idx;not null" json:"organizationId"`
	DiscoveredPersonID string   `gorm:"column:discoveredPersonId;uniqueIndex:IdentityMatchSuggestion_organizationId_discoveredPersonId_userId_key;not null" json:"discoveredPersonId"`
	UserID             string   `gorm:"column:userId;uniqueIndex:IdentityMatchSuggestion_organizationId_discoveredPersonId_userId_key;not null" json:"userId"`
	Score              float64  `gorm:"column:score;index:IdentityMatchSuggestion_organizationId_score_idx;not null" json:"score"`
	ComputedAt         DateTime `gorm:"column:computedAt;not null" json:"computedAt"`
}

func (IdentityMatchSuggestion) TableName() string {
	return "IdentityMatchSuggestion"
}

func (m *IdentityMatchSuggestion) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	return nil
}

type ConnectedSeatChange struct {
	ID              string   `gorm:"column:id;primaryKey" json:"id"`
	LicenseID       string   `gorm:"column:licenseId;uniqueIndex;not null" json:"licenseId"`
	AccountID       *string  `gorm:"column:accountId;index:ConnectedSeatChange_accountId_idx" json:"accountId"`
	OrganizationID  *string  `gorm:"column:organizationId;index:ConnectedSeatChange_organizationId_idx" json:"organizationId"`
	ChangedAt       DateTime `gorm:"column:changedAt;not null" json:"changedAt"`
	AddedSeats      int      `gorm:"column:addedSeats;not null" json:"addedSeats"`
	UnitAmountCents int      `gorm:"column:unitAmountCents;not null" json:"unitAmountCents"`
	AmountCents     int      `gorm:"column:amountCents;not null" json:"amountCents"`
	Currency        *string  `gorm:"column:currency" json:"currency"`
	StripeInvoiceID *string  `gorm:"column:stripeInvoiceId" json:"stripeInvoiceId"`
	State           string   `gorm:"column:state;index:ConnectedSeatChange_state_idx;not null" json:"state"`
	CreatedAt       DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt       DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
}

func (ConnectedSeatChange) TableName() string {
	return "ConnectedSeatChange"
}

func (m *ConnectedSeatChange) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*ConnectedSeatChange) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type ConnectedStatement struct {
	ID        string   `gorm:"column:id;primaryKey" json:"id"`
	AccountID string   `gorm:"column:accountId;uniqueIndex:ConnectedStatement_accountId_month_key;index:ConnectedStatement_accountId_idx;not null" json:"accountId"`
	Month     DateTime `gorm:"column:month;uniqueIndex:ConnectedStatement_accountId_month_key;not null" json:"month"`
	SentAt    DateTime `gorm:"column:sentAt;not null" json:"sentAt"`
}

func (ConnectedStatement) TableName() string {
	return "ConnectedStatement"
}

func (m *ConnectedStatement) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	return nil
}

type LicenseRenewalClaim struct {
	StripeInvoiceID      string         `gorm:"column:stripeInvoiceId;primaryKey" json:"stripeInvoiceId"`
	StripeSubscriptionID string         `gorm:"column:stripeSubscriptionId;not null" json:"stripeSubscriptionId"`
	ClaimToken           string         `gorm:"column:claimToken;not null" json:"claimToken"`
	ClaimedAt            DateTime       `gorm:"column:claimedAt;not null" json:"claimedAt"`
	IssuedLicense        datatypes.JSON `gorm:"column:issuedLicense" json:"issuedLicense"`
	DeliveredAt          *DateTime      `gorm:"column:deliveredAt" json:"deliveredAt"`
	LicenseID            *string        `gorm:"column:licenseId" json:"licenseId"`
}

func (LicenseRenewalClaim) TableName() string {
	return "LicenseRenewalClaim"
}

type InstanceIdentity struct {
	ID                       string    `gorm:"column:id;primaryKey" json:"id"`
	InstanceID               string    `gorm:"column:instanceId;uniqueIndex;not null" json:"instanceId"`
	CreatedAt                DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	LastReportAt             *DateTime `gorm:"column:lastReportAt" json:"lastReportAt"`
	LastReportError          *string   `gorm:"column:lastReportError" json:"lastReportError"`
	OptionalMetricsOptOut    bool      `gorm:"column:optionalMetricsOptOut;default:false;not null" json:"optionalMetricsOptOut"`
	HostnameOptOut           bool      `gorm:"column:hostnameOptOut;default:false;not null" json:"hostnameOptOut"`
	UpgradeDiagnosticsOptOut bool      `gorm:"column:upgradeDiagnosticsOptOut;default:false;not null" json:"upgradeDiagnosticsOptOut"`
}

func (InstanceIdentity) TableName() string {
	return "InstanceIdentity"
}

func (m *InstanceIdentity) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type SelfHostedInstance struct {
	ID                      string         `gorm:"column:id;primaryKey" json:"id"`
	InstanceID              string         `gorm:"column:instanceId;uniqueIndex;not null" json:"instanceId"`
	FirstSeenAt             DateTime       `gorm:"column:firstSeenAt;not null" json:"firstSeenAt"`
	LastSeenAt              DateTime       `gorm:"column:lastSeenAt;index:SelfHostedInstance_lastSeenAt_idx;not null" json:"lastSeenAt"`
	Version                 *string        `gorm:"column:version" json:"version"`
	InstallMethod           *string        `gorm:"column:installMethod" json:"installMethod"`
	ChartVersion            *string        `gorm:"column:chartVersion" json:"chartVersion"`
	Hostname                *string        `gorm:"column:hostname" json:"hostname"`
	Environment             *string        `gorm:"column:environment" json:"environment"`
	InstalledAt             *DateTime      `gorm:"column:installedAt" json:"installedAt"`
	ReportSchemaVersion     *int           `gorm:"column:reportSchemaVersion" json:"reportSchemaVersion"`
	OrganizationID          *string        `gorm:"column:organizationId;index:SelfHostedInstance_organizationId_idx" json:"organizationId"`
	IssuedLicenseID         *string        `gorm:"column:issuedLicenseId;index:SelfHostedInstance_issuedLicenseId_idx" json:"issuedLicenseId"`
	UserEmailDomains        datatypes.JSON `gorm:"column:userEmailDomains" json:"userEmailDomains"`
	UserDomains             []string       `gorm:"column:userDomains;serializer:json;not null" json:"userDomains"`
	LatestReport            datatypes.JSON `gorm:"column:latestReport" json:"latestReport"`
	OptionalMetricsReported *bool          `gorm:"column:optionalMetricsReported;default:true;not null" json:"optionalMetricsReported"`
	HostnameReported        *bool          `gorm:"column:hostnameReported;default:true;not null" json:"hostnameReported"`
	ReportCount             int            `gorm:"column:reportCount;default:0;not null" json:"reportCount"`
	LastUnknownFields       int            `gorm:"column:lastUnknownFields;default:0;not null" json:"lastUnknownFields"`
	RaisedSignals           []string       `gorm:"column:raisedSignals;serializer:json;not null" json:"raisedSignals"`
	CreatedAt               DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt               DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
}

func (SelfHostedInstance) TableName() string {
	return "SelfHostedInstance"
}

func (m *SelfHostedInstance) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.FirstSeenAt.IsZero() {
		m.FirstSeenAt = DateTime{Time: now}
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*SelfHostedInstance) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type SelfHostedInstanceDiagnostic struct {
	ID               string    `gorm:"column:id;primaryKey" json:"id"`
	InstanceID       string    `gorm:"column:instanceId;index:SelfHostedInstanceDiagnostic_instanceId_receivedAt_idx;not null" json:"instanceId"`
	StepID           string    `gorm:"column:stepId;not null" json:"stepId"`
	StepDescription  *string   `gorm:"column:stepDescription" json:"stepDescription"`
	Release          *string   `gorm:"column:release" json:"release"`
	Attempt          int       `gorm:"column:attempt;not null" json:"attempt"`
	Trigger          string    `gorm:"column:trigger;not null" json:"trigger"`
	ByteLength       int       `gorm:"column:byteLength;not null" json:"byteLength"`
	ObjectKey        string    `gorm:"column:objectKey;not null" json:"objectKey"`
	ReceivedAt       DateTime  `gorm:"column:receivedAt;index:SelfHostedInstanceDiagnostic_instanceId_receivedAt_idx;not null" json:"receivedAt"`
	ExpiresAt        DateTime  `gorm:"column:expiresAt;not null" json:"expiresAt"`
	SlackAnnouncedAt *DateTime `gorm:"column:slackAnnouncedAt" json:"slackAnnouncedAt"`
}

func (SelfHostedInstanceDiagnostic) TableName() string {
	return "SelfHostedInstanceDiagnostic"
}

func (m *SelfHostedInstanceDiagnostic) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ReceivedAt.IsZero() {
		m.ReceivedAt = DateTime{Time: now}
	}
	return nil
}

type SelfHostedNotice struct {
	ID          string    `gorm:"column:id;primaryKey" json:"id"`
	Severity    string    `gorm:"column:severity;not null" json:"severity"`
	Title       string    `gorm:"column:title;not null" json:"title"`
	Body        string    `gorm:"column:body;not null" json:"body"`
	URL         *string   `gorm:"column:url" json:"url"`
	FixedIn     *string   `gorm:"column:fixedIn" json:"fixedIn"`
	Status      string    `gorm:"column:status;not null" json:"status"`
	ExpiresAt   *DateTime `gorm:"column:expiresAt" json:"expiresAt"`
	PublishedAt *DateTime `gorm:"column:publishedAt" json:"publishedAt"`
	PublishedBy *string   `gorm:"column:publishedBy" json:"publishedBy"`
	CreatedAt   DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt   DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
}

func (SelfHostedNotice) TableName() string {
	return "SelfHostedNotice"
}

func (m *SelfHostedNotice) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*SelfHostedNotice) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type SelfHostedInstanceReport struct {
	ID                  string         `gorm:"column:id;primaryKey" json:"id"`
	InstanceID          string         `gorm:"column:instanceId;index:SelfHostedInstanceReport_instanceId_receivedAt_idx;not null" json:"instanceId"`
	ReceivedAt          DateTime       `gorm:"column:receivedAt;index:SelfHostedInstanceReport_instanceId_receivedAt_idx;index:SelfHostedInstanceReport_receivedAt_idx;not null" json:"receivedAt"`
	Version             *string        `gorm:"column:version" json:"version"`
	ReportSchemaVersion *int           `gorm:"column:reportSchemaVersion" json:"reportSchemaVersion"`
	UnknownFields       int            `gorm:"column:unknownFields;default:0;not null" json:"unknownFields"`
	Payload             datatypes.JSON `gorm:"column:payload;not null" json:"payload"`
}

func (SelfHostedInstanceReport) TableName() string {
	return "SelfHostedInstanceReport"
}

func (m *SelfHostedInstanceReport) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.ReceivedAt.IsZero() {
		m.ReceivedAt = DateTime{Time: now}
	}
	return nil
}

type WebPushSubscription struct {
	ID            string    `gorm:"column:id;primaryKey" json:"id"`
	UserID        string    `gorm:"column:userId;index:WebPushSubscription_userId_idx;not null" json:"userId"`
	Endpoint      string    `gorm:"column:endpoint;uniqueIndex;not null" json:"endpoint"`
	P256dh        string    `gorm:"column:p256dh;not null" json:"p256dh"`
	Auth          string    `gorm:"column:auth;not null" json:"auth"`
	UserAgent     *string   `gorm:"column:userAgent" json:"userAgent"`
	CreatedAt     DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	LastSuccessAt *DateTime `gorm:"column:lastSuccessAt" json:"lastSuccessAt"`
}

func (WebPushSubscription) TableName() string {
	return "WebPushSubscription"
}

func (m *WebPushSubscription) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = gonanoid.Must()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type WebPushVapidKey struct {
	ID                  string   `gorm:"column:id;primaryKey" json:"id"`
	PublicKey           string   `gorm:"column:publicKey;not null" json:"publicKey"`
	PrivateKeyEncrypted string   `gorm:"column:privateKeyEncrypted;not null" json:"privateKeyEncrypted"`
	CreatedAt           DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
}

func (WebPushVapidKey) TableName() string {
	return "WebPushVapidKey"
}

func (m *WebPushVapidKey) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type SignInProviderMount struct {
	ID                 string   `gorm:"column:id;primaryKey" json:"id"`
	Provider           *string  `gorm:"column:provider" json:"provider"`
	Mounted            bool     `gorm:"column:mounted;not null" json:"mounted"`
	LastAppliedEventID string   `gorm:"column:lastAppliedEventId;not null" json:"lastAppliedEventId"`
	UpdatedAt          DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
}

func (SignInProviderMount) TableName() string {
	return "SignInProviderMount"
}

func (m *SignInProviderMount) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*SignInProviderMount) BeforeUpdate(tx *gorm.DB) error {
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
