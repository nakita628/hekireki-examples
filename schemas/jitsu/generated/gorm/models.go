package model

import (
	"database/sql/driver"
	"fmt"
	"strconv"
	"time"

	"github.com/google/uuid"
	"github.com/lucsky/cuid"
	"gorm.io/datatypes"
	"gorm.io/gorm"
)

type EmailLog struct {
	ID         string         `gorm:"column:id;primaryKey" json:"id"`
	Time       DateTime       `gorm:"column:time;not null" json:"time"`
	Email      datatypes.JSON `gorm:"column:email;not null" json:"email"`
	Error      *string        `gorm:"column:error" json:"error"`
	PreviewURL *string        `gorm:"column:previewUrl" json:"previewUrl"`
	MessageID  *string        `gorm:"column:messageId" json:"messageId"`
	Status     string         `gorm:"column:status;not null" json:"status"`
}

func (EmailLog) TableName() string {
	return "EmailLog"
}

func (m *EmailLog) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.Time.IsZero() {
		m.Time = DateTime{Time: now}
	}
	return nil
}

type UserPreferences struct {
	ID          string         `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt   DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt   DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	UserID      string         `gorm:"column:userId;not null" json:"userId"`
	WorkspaceID *string        `gorm:"column:workspaceId" json:"workspaceId"`
	Scope       *string        `gorm:"column:scope" json:"scope"`
	Preferences datatypes.JSON `gorm:"column:preferences;not null" json:"preferences"`
	User        *UserProfile   `gorm:"foreignKey:UserID"`
	Workspace   *Workspace
}

func (UserPreferences) TableName() string {
	return "UserPreferences"
}

func (m *UserPreferences) BeforeCreate(tx *gorm.DB) error {
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

func (*UserPreferences) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type UserProfile struct {
	ID                      string                    `gorm:"column:id;primaryKey" json:"id"`
	Name                    string                    `gorm:"column:name;not null" json:"name"`
	Email                   string                    `gorm:"column:email;not null" json:"email"`
	Admin                   *bool                     `gorm:"column:admin;default:false" json:"admin"`
	CreatedAt               DateTime                  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt               DateTime                  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	LoginProvider           string                    `gorm:"column:loginProvider;uniqueIndex:UserProfile_loginProvider_externalId_key;not null" json:"loginProvider"`
	ExternalUsername        *string                   `gorm:"column:externalUsername" json:"externalUsername"`
	ExternalID              string                    `gorm:"column:externalId;uniqueIndex:UserProfile_loginProvider_externalId_key;index:UserProfile_externalId_idx;not null" json:"externalId"`
	Tokens                  []UserAPIToken            `gorm:"foreignKey:UserID"`
	WorkspaceAccess         []WorkspaceAccess         `gorm:"foreignKey:UserID"`
	UserPreferences         []UserPreferences         `gorm:"foreignKey:UserID"`
	WorkspaceUserProperties []WorkspaceUserProperties `gorm:"foreignKey:UserID"`
	Password                *UserPassword             `gorm:"foreignKey:UserID"`
}

func (UserProfile) TableName() string {
	return "UserProfile"
}

func (m *UserProfile) BeforeCreate(tx *gorm.DB) error {
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

func (*UserProfile) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type UserPassword struct {
	ID                string       `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt         DateTime     `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt         DateTime     `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	UserID            string       `gorm:"column:userId;uniqueIndex;not null" json:"userId"`
	Hash              string       `gorm:"column:hash;not null" json:"hash"`
	ChangeAtNextLogin bool         `gorm:"column:changeAtNextLogin;default:false;not null" json:"changeAtNextLogin"`
	User              *UserProfile `gorm:"foreignKey:UserID"`
}

func (UserPassword) TableName() string {
	return "UserPassword"
}

func (m *UserPassword) BeforeCreate(tx *gorm.DB) error {
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

func (*UserPassword) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Workspace struct {
	ID                      string                    `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt               DateTime                  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt               DateTime                  `gorm:"column:updatedAt;index:Workspace_updatedAt_idx;autoUpdateTime:false;not null" json:"updatedAt"`
	Name                    string                    `gorm:"column:name;not null" json:"name"`
	Deleted                 bool                      `gorm:"column:deleted;default:false;not null" json:"deleted"`
	Slug                    *string                   `gorm:"column:slug;uniqueIndex;index:Workspace_slug_idx" json:"slug"`
	FeaturesEnabled         []string                  `gorm:"column:featuresEnabled;serializer:json;not null" json:"featuresEnabled"`
	WorkspaceAccess         []WorkspaceAccess         `gorm:"foreignKey:WorkspaceID"`
	WorkspaceOptions        []WorkspaceOptions        `gorm:"foreignKey:WorkspaceID"`
	ConfigurationObject     []ConfigurationObject     `gorm:"foreignKey:WorkspaceID"`
	ConfigurationObjectLink []ConfigurationObjectLink `gorm:"foreignKey:WorkspaceID"`
	Preferences             []UserPreferences         `gorm:"foreignKey:WorkspaceID"`
	WorkspaceUserProperties []WorkspaceUserProperties `gorm:"foreignKey:WorkspaceID"`
	ProfileBuilders         []ProfileBuilder          `gorm:"foreignKey:WorkspaceID"`
	OidcLoginGroups         []OidcLoginGroup          `gorm:"foreignKey:WorkspaceID"`
}

func (Workspace) TableName() string {
	return "Workspace"
}

func (m *Workspace) BeforeCreate(tx *gorm.DB) error {
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

func (*Workspace) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type WorkspaceOptions struct {
	ID          string         `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt   DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt   DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	WorkspaceID string         `gorm:"column:workspaceId;index:WorkspaceOptions_workspaceId_namespace_idx;not null" json:"workspaceId"`
	Namespace   string         `gorm:"column:namespace;index:WorkspaceOptions_workspaceId_namespace_idx;not null" json:"namespace"`
	Value       datatypes.JSON `gorm:"column:value;not null" json:"value"`
	Workspace   *Workspace
}

func (WorkspaceOptions) TableName() string {
	return "WorkspaceOptions"
}

func (m *WorkspaceOptions) BeforeCreate(tx *gorm.DB) error {
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

func (*WorkspaceOptions) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type WorkspaceAccess struct {
	WorkspaceID string   `gorm:"column:workspaceId;primaryKey" json:"workspaceId"`
	CreatedAt   DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt   DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	UserID      string   `gorm:"column:userId;primaryKey" json:"userId"`
	Role        string   `gorm:"column:role;default:'owner';not null" json:"role"`
	Workspace   *Workspace
	User        *UserProfile `gorm:"foreignKey:UserID"`
}

func (WorkspaceAccess) TableName() string {
	return "WorkspaceAccess"
}

func (m *WorkspaceAccess) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*WorkspaceAccess) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type WorkspaceUserProperties struct {
	WorkspaceID string   `gorm:"column:workspaceId;primaryKey" json:"workspaceId"`
	UserID      string   `gorm:"column:userId;primaryKey" json:"userId"`
	LastUsed    DateTime `gorm:"column:lastUsed;not null" json:"lastUsed"`
	Workspace   *Workspace
	User        *UserProfile `gorm:"foreignKey:UserID"`
}

func (WorkspaceUserProperties) TableName() string {
	return "WorkspaceUserProperties"
}

func (m *WorkspaceUserProperties) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.LastUsed.IsZero() {
		m.LastUsed = DateTime{Time: now}
	}
	return nil
}

type UserAPIToken struct {
	ID            string             `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt     DateTime           `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt     DateTime           `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	LastUsed      *DateTime          `gorm:"column:lastUsed" json:"lastUsed"`
	Hint          string             `gorm:"column:hint;not null" json:"hint"`
	Hash          string             `gorm:"column:hash;not null" json:"hash"`
	UserID        string             `gorm:"column:userId;not null" json:"userId"`
	Type          *string            `gorm:"column:type" json:"type"`
	Name          *string            `gorm:"column:name" json:"name"`
	ExpiresAt     *DateTime          `gorm:"column:expiresAt" json:"expiresAt"`
	OauthClientID *string            `gorm:"column:oauthClientId" json:"oauthClientId"`
	User          *UserProfile       `gorm:"foreignKey:UserID"`
	OauthClient   *OAuthClient       `gorm:"foreignKey:OauthClientID"`
	AccessTokens  []OAuthAccessToken `gorm:"foreignKey:RefreshTokenID"`
}

func (UserAPIToken) TableName() string {
	return "UserApiToken"
}

func (m *UserAPIToken) BeforeCreate(tx *gorm.DB) error {
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

func (*UserAPIToken) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type OAuthClient struct {
	ID               string         `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt        DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt        DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	ClientSecretHash string         `gorm:"column:clientSecretHash;not null" json:"clientSecretHash"`
	Name             string         `gorm:"column:name;not null" json:"name"`
	RedirectUris     []string       `gorm:"column:redirectUris;serializer:json;not null" json:"redirectUris"`
	Tokens           []UserAPIToken `gorm:"foreignKey:OauthClientID"`
}

func (OAuthClient) TableName() string {
	return "OAuthClient"
}

func (m *OAuthClient) BeforeCreate(tx *gorm.DB) error {
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

func (*OAuthClient) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type KvStoreEntry struct {
	Key       string         `gorm:"column:key;primaryKey;index:kv_key_pattern_idx" json:"key"`
	Value     datatypes.JSON `gorm:"column:value;not null" json:"value"`
	ExpiresAt *DateTime      `gorm:"column:expires_at;index:kv_expires_at_idx" json:"expires_at"`
}

func (KvStoreEntry) TableName() string {
	return "kv"
}

type OAuthAccessToken struct {
	ID             string        `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt      DateTime      `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	LastUsed       *DateTime     `gorm:"column:lastUsed" json:"lastUsed"`
	Hash           string        `gorm:"column:hash;not null" json:"hash"`
	ExpiresAt      DateTime      `gorm:"column:expiresAt;index:OAuthAccessToken_expiresAt_idx;not null" json:"expiresAt"`
	RefreshTokenID string        `gorm:"column:refreshTokenId;index:OAuthAccessToken_refreshTokenId_idx;not null" json:"refreshTokenId"`
	RefreshToken   *UserAPIToken `gorm:"foreignKey:RefreshTokenID"`
}

func (OAuthAccessToken) TableName() string {
	return "OAuthAccessToken"
}

func (m *OAuthAccessToken) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type InvitationToken struct {
	ID          string   `gorm:"column:id;primaryKey" json:"id"`
	WorkspaceID string   `gorm:"column:workspaceId;not null" json:"workspaceId"`
	Email       string   `gorm:"column:email;not null" json:"email"`
	CreatedAt   DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt   DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Token       string   `gorm:"column:token;not null" json:"token"`
	UsedBy      *string  `gorm:"column:usedBy" json:"usedBy"`
	Role        string   `gorm:"column:role;default:'owner';not null" json:"role"`
}

func (InvitationToken) TableName() string {
	return "InvitationToken"
}

func (m *InvitationToken) BeforeCreate(tx *gorm.DB) error {
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

func (*InvitationToken) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type ConfigurationObject struct {
	ID                     string         `gorm:"column:id;primaryKey" json:"id"`
	Deleted                *bool          `gorm:"column:deleted;default:false" json:"deleted"`
	CreatedAt              DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt              DateTime       `gorm:"column:updatedAt;index:ConfigurationObject_updatedAt_idx;autoUpdateTime:false;not null" json:"updatedAt"`
	Type                   string         `gorm:"column:type;not null" json:"type"`
	WorkspaceID            string         `gorm:"column:workspaceId;not null" json:"workspaceId"`
	Config                 datatypes.JSON `gorm:"column:config" json:"config"`
	Workspace              *Workspace
	FromLinks              []ConfigurationObjectLink `gorm:"foreignKey:ToID"`
	ToLinks                []ConfigurationObjectLink `gorm:"foreignKey:FromID"`
	ProfileBuilder         []ProfileBuilder          `gorm:"foreignKey:DestinationID"`
	ProfileBuilderFunction []ProfileBuilderFunction  `gorm:"foreignKey:FunctionID"`
}

func (ConfigurationObject) TableName() string {
	return "ConfigurationObject"
}

func (m *ConfigurationObject) BeforeCreate(tx *gorm.DB) error {
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

func (*ConfigurationObject) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type ConfigurationObjectLink struct {
	ID          string         `gorm:"column:id;primaryKey" json:"id"`
	Deleted     *bool          `gorm:"column:deleted;default:false" json:"deleted"`
	CreatedAt   DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt   DateTime       `gorm:"column:updatedAt;index:ConfigurationObjectLink_updatedAt_idx;autoUpdateTime:false;not null" json:"updatedAt"`
	Type        *string        `gorm:"column:type;default:'push'" json:"type"`
	Data        datatypes.JSON `gorm:"column:data" json:"data"`
	WorkspaceID string         `gorm:"column:workspaceId;index:ConfigurationObjectLink_workspaceId_idx;not null" json:"workspaceId"`
	FromID      string         `gorm:"column:fromId;not null" json:"fromId"`
	ToID        string         `gorm:"column:toId;not null" json:"toId"`
	Workspace   *Workspace
	From        *ConfigurationObject `gorm:"foreignKey:FromID"`
	To          *ConfigurationObject `gorm:"foreignKey:ToID"`
}

func (ConfigurationObjectLink) TableName() string {
	return "ConfigurationObjectLink"
}

func (m *ConfigurationObjectLink) BeforeCreate(tx *gorm.DB) error {
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

func (*ConfigurationObjectLink) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type ConnectorPackage struct {
	ID          string         `gorm:"column:id;primaryKey" json:"id"`
	PackageID   string         `gorm:"column:packageId;not null" json:"packageId"`
	PackageType *string        `gorm:"column:packageType;default:'airbyte';not null" json:"packageType"`
	Meta        datatypes.JSON `gorm:"column:meta" json:"meta"`
	LogoSvg     []byte         `gorm:"column:logoSvg" json:"logoSvg"`
	CreatedAt   DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt   DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
}

func (ConnectorPackage) TableName() string {
	return "ConnectorPackage"
}

func (m *ConnectorPackage) BeforeCreate(tx *gorm.DB) error {
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

func (*ConnectorPackage) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type AuditLog struct {
	ID             string         `gorm:"column:id;primaryKey" json:"id"`
	Timestamp      DateTime       `gorm:"column:timestamp;index:AuditLog_timestamp_idx;index:AuditLog_workspaceId_timestamp_idx;not null" json:"timestamp"`
	Type           string         `gorm:"column:type;index:AuditLog_type_idx;not null" json:"type"`
	Severity       *string        `gorm:"column:severity;default:'info'" json:"severity"`
	UserID         *string        `gorm:"column:userId" json:"userId"`
	WorkspaceID    *string        `gorm:"column:workspaceId;index:AuditLog_workspaceId_timestamp_idx" json:"workspaceId"`
	ObjectID       *string        `gorm:"column:objectId" json:"objectId"`
	Changes        datatypes.JSON `gorm:"column:changes" json:"changes"`
	AuthType       *string        `gorm:"column:authType" json:"authType"`
	TokenID        *string        `gorm:"column:tokenId" json:"tokenId"`
	IP             *string        `gorm:"column:ip" json:"ip"`
	RequestHeaders datatypes.JSON `gorm:"column:requestHeaders" json:"requestHeaders"`
}

func (AuditLog) TableName() string {
	return "AuditLog"
}

func (m *AuditLog) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.Timestamp.IsZero() {
		m.Timestamp = DateTime{Time: now}
	}
	return nil
}

type GlobalProps struct {
	ID        string         `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Name      string         `gorm:"column:name;not null" json:"name"`
	Value     datatypes.JSON `gorm:"column:value" json:"value"`
}

func (GlobalProps) TableName() string {
	return "GlobalProps"
}

func (m *GlobalProps) BeforeCreate(tx *gorm.DB) error {
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

func (*GlobalProps) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type SourceSpec struct {
	Package   string         `gorm:"column:package;primaryKey" json:"package"`
	Version   string         `gorm:"column:version;primaryKey" json:"version"`
	Specs     datatypes.JSON `gorm:"column:specs" json:"specs"`
	Timestamp DateTime       `gorm:"column:timestamp;type:timestamptz(3);not null" json:"timestamp"`
	Error     *string        `gorm:"column:error" json:"error"`
}

func (SourceSpec) TableName() string {
	return "source_spec"
}

func (m *SourceSpec) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.Timestamp.IsZero() {
		m.Timestamp = DateTime{Time: now}
	}
	return nil
}

type SourceCheck struct {
	Package     string   `gorm:"column:package;not null" json:"package"`
	Version     string   `gorm:"column:version;not null" json:"version"`
	Key         string   `gorm:"column:key;primaryKey" json:"key"`
	Status      string   `gorm:"column:status;not null" json:"status"`
	Description *string  `gorm:"column:description" json:"description"`
	Timestamp   DateTime `gorm:"column:timestamp;type:timestamptz(3);not null" json:"timestamp"`
}

func (SourceCheck) TableName() string {
	return "source_check"
}

func (m *SourceCheck) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.Timestamp.IsZero() {
		m.Timestamp = DateTime{Time: now}
	}
	return nil
}

type SourceCatalog struct {
	Package     string         `gorm:"column:package;primaryKey" json:"package"`
	Version     string         `gorm:"column:version;primaryKey" json:"version"`
	Key         string         `gorm:"column:key;primaryKey" json:"key"`
	Catalog     datatypes.JSON `gorm:"column:catalog" json:"catalog"`
	Timestamp   DateTime       `gorm:"column:timestamp;type:timestamptz(3);not null" json:"timestamp"`
	Status      string         `gorm:"column:status;not null" json:"status"`
	Description *string        `gorm:"column:description" json:"description"`
}

func (SourceCatalog) TableName() string {
	return "source_catalog"
}

func (m *SourceCatalog) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.Timestamp.IsZero() {
		m.Timestamp = DateTime{Time: now}
	}
	return nil
}

type ReverseSyncControl struct {
	WorkspaceID         string  `gorm:"column:workspace_id;primaryKey;uniqueIndex:reverse_sync_control_workspace_id_sync_id_run_order_key" json:"workspace_id"`
	SyncID              string  `gorm:"column:sync_id;primaryKey;uniqueIndex:reverse_sync_control_workspace_id_sync_id_run_order_key;index:reverse_sync_control_sync_id_idx" json:"sync_id"`
	RunID               string  `gorm:"column:run_id;primaryKey" json:"run_id"`
	RunOrder            int64   `gorm:"column:run_order;uniqueIndex:reverse_sync_control_workspace_id_sync_id_run_order_key;default:0;not null" json:"run_order"`
	Detached            bool    `gorm:"column:detached;default:false;not null" json:"detached"`
	Revision            string  `gorm:"column:revision;not null" json:"revision"`
	TargetHash          string  `gorm:"column:target_hash;not null" json:"target_hash"`
	Phase               *string `gorm:"column:phase;default:'new';not null" json:"phase"`
	Mode                string  `gorm:"column:mode;not null" json:"mode"`
	Extraction          string  `gorm:"column:extraction;not null" json:"extraction"`
	ArtifactHead        []byte  `gorm:"column:artifact_head" json:"artifact_head"`
	BaseSequence        int64   `gorm:"column:base_sequence;default:0;not null" json:"base_sequence"`
	NextSequence        int64   `gorm:"column:next_sequence;default:0;not null" json:"next_sequence"`
	CheckpointSequence  int64   `gorm:"column:checkpoint_sequence;default:0;not null" json:"checkpoint_sequence"`
	FinishSequence      *int64  `gorm:"column:finish_sequence" json:"finish_sequence"`
	Store               []byte  `gorm:"column:store" json:"store"`
	ProviderState       []byte  `gorm:"column:provider_state" json:"provider_state"`
	FinishResult        []byte  `gorm:"column:finish_result" json:"finish_result"`
	CommittedGeneration *string `gorm:"column:committed_generation" json:"committed_generation"`
}

func (ReverseSyncControl) TableName() string {
	return "reverse_sync_control"
}

type ReverseSyncTargetOwner struct {
	TargetHash  string `gorm:"column:target_hash;primaryKey" json:"target_hash"`
	WorkspaceID string `gorm:"column:workspace_id;uniqueIndex:reverse_sync_target_owner_workspace_id_sync_id_key;not null" json:"workspace_id"`
	SyncID      string `gorm:"column:sync_id;uniqueIndex:reverse_sync_target_owner_workspace_id_sync_id_key;not null" json:"sync_id"`
}

func (ReverseSyncTargetOwner) TableName() string {
	return "reverse_sync_target_owner"
}

type SourceState struct {
	SyncID    string         `gorm:"column:sync_id;primaryKey" json:"sync_id"`
	Stream    string         `gorm:"column:stream;primaryKey" json:"stream"`
	State     datatypes.JSON `gorm:"column:state;not null" json:"state"`
	Timestamp DateTime       `gorm:"column:timestamp;type:timestamptz(3);not null" json:"timestamp"`
}

func (SourceState) TableName() string {
	return "source_state"
}

func (m *SourceState) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.Timestamp.IsZero() {
		m.Timestamp = DateTime{Time: now}
	}
	return nil
}

type SourceTask struct {
	SyncID      string         `gorm:"column:sync_id;index:source_task_sync_id_idx;index:source_task_sync_id_started_at_idx;not null" json:"sync_id"`
	TaskID      string         `gorm:"column:task_id;primaryKey" json:"task_id"`
	Package     string         `gorm:"column:package;not null" json:"package"`
	Version     string         `gorm:"column:version;not null" json:"version"`
	StartedAt   DateTime       `gorm:"column:started_at;index:source_task_started_at_idx;index:source_task_sync_id_started_at_idx;type:timestamptz(3);not null" json:"started_at"`
	UpdatedAt   DateTime       `gorm:"column:updated_at;type:timestamptz(3);autoUpdateTime:false;not null" json:"updated_at"`
	StartedBy   datatypes.JSON `gorm:"column:started_by" json:"started_by"`
	Status      string         `gorm:"column:status;not null" json:"status"`
	Description *string        `gorm:"column:description" json:"description"`
	Metrics     datatypes.JSON `gorm:"column:metrics" json:"metrics"`
	Error       *string        `gorm:"column:error" json:"error"`
}

func (SourceTask) TableName() string {
	return "source_task"
}

func (m *SourceTask) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.StartedAt.IsZero() {
		m.StartedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

type TaskLog struct {
	ID        string   `gorm:"column:id;primaryKey;type:char(36)" json:"id"`
	Level     string   `gorm:"column:level;not null" json:"level"`
	Logger    string   `gorm:"column:logger;not null" json:"logger"`
	Message   string   `gorm:"column:message;not null" json:"message"`
	SyncID    string   `gorm:"column:sync_id;index:task_log_sync_id_idx;not null" json:"sync_id"`
	TaskID    string   `gorm:"column:task_id;index:task_log_task_id_idx;not null" json:"task_id"`
	Timestamp DateTime `gorm:"column:timestamp;index:task_log_timestamp_idx;type:timestamptz(3);not null" json:"timestamp"`
}

func (TaskLog) TableName() string {
	return "task_log"
}

func (m *TaskLog) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = uuid.NewString()
	}
	if m.Timestamp.IsZero() {
		m.Timestamp = DateTime{Time: now}
	}
	return nil
}

type ProfileBuilder struct {
	ID                             string         `gorm:"column:id;primaryKey" json:"id"`
	Name                           string         `gorm:"column:name;not null" json:"name"`
	Deleted                        *bool          `gorm:"column:deleted;default:false" json:"deleted"`
	CreatedAt                      DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt                      DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	WorkspaceID                    string         `gorm:"column:workspaceId;not null" json:"workspaceId"`
	IntermediateStorageCredentials datatypes.JSON `gorm:"column:intermediateStorageCredentials;not null" json:"intermediateStorageCredentials"`
	DestinationID                  *string        `gorm:"column:destinationId" json:"destinationId"`
	ConnectionOptions              datatypes.JSON `gorm:"column:connectionOptions" json:"connectionOptions"`
	Version                        int            `gorm:"column:version;default:0;not null" json:"version"`
	Workspace                      *Workspace
	Destination                    *ConfigurationObject     `gorm:"foreignKey:DestinationID"`
	Functions                      []ProfileBuilderFunction `gorm:"foreignKey:ProfileBuilderID"`
}

func (ProfileBuilder) TableName() string {
	return "ProfileBuilder"
}

func (m *ProfileBuilder) BeforeCreate(tx *gorm.DB) error {
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

func (*ProfileBuilder) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type ProfileBuilderFunction struct {
	ID               string   `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt        DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt        DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	ProfileBuilderID string   `gorm:"column:profileBuilderId;not null" json:"profileBuilderId"`
	FunctionID       string   `gorm:"column:functionId;not null" json:"functionId"`
	ProfileBuilder   *ProfileBuilder
	Function         *ConfigurationObject `gorm:"foreignKey:FunctionID"`
}

func (ProfileBuilderFunction) TableName() string {
	return "ProfileBuilderFunction"
}

func (m *ProfileBuilderFunction) BeforeCreate(tx *gorm.DB) error {
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

func (*ProfileBuilderFunction) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type ProfileBuilderState struct {
	ProfileBuilderID      string    `gorm:"column:profileBuilderId;primaryKey" json:"profileBuilderId"`
	ProfileBuilderVersion int       `gorm:"column:profileBuilderVersion;primaryKey" json:"profileBuilderVersion"`
	InstanceIndex         int       `gorm:"column:instanceIndex;primaryKey" json:"instanceIndex"`
	TotalInstances        int       `gorm:"column:totalInstances;primaryKey" json:"totalInstances"`
	StartedAt             DateTime  `gorm:"column:startedAt;not null" json:"startedAt"`
	UpdatedAt             DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	LastTimestamp         *DateTime `gorm:"column:lastTimestamp" json:"lastTimestamp"`
	ProcessedUsers        int       `gorm:"column:processedUsers;not null" json:"processedUsers"`
	TotalUsers            int       `gorm:"column:totalUsers;not null" json:"totalUsers"`
	ErrorUsers            int       `gorm:"column:errorUsers;not null" json:"errorUsers"`
	Speed                 float64   `gorm:"column:speed;not null" json:"speed"`
}

func (ProfileBuilderState) TableName() string {
	return "ProfileBuilderState"
}

type ProfileBuilderState2 struct {
	ProfileBuilderID string         `gorm:"column:profileBuilderId;primaryKey" json:"profileBuilderId"`
	UpdatedAt        DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	FullRebuildInfo  datatypes.JSON `gorm:"column:fullRebuildInfo" json:"fullRebuildInfo"`
	QueuesInfo       datatypes.JSON `gorm:"column:queuesInfo" json:"queuesInfo"`
	Metrics          datatypes.JSON `gorm:"column:metrics" json:"metrics"`
}

func (ProfileBuilderState2) TableName() string {
	return "ProfileBuilderState2"
}

type StatusChange struct {
	ID          int64    `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	WorkspaceID string   `gorm:"column:workspaceId;not null" json:"workspaceId"`
	ActorID     string   `gorm:"column:actorId;index:StatusChange_actorId_type_tableName_idx;not null" json:"actorId"`
	Type        string   `gorm:"column:type;index:StatusChange_actorId_type_tableName_idx;not null" json:"type"`
	TableName_  string   `gorm:"column:tableName;index:StatusChange_actorId_type_tableName_idx;not null" json:"tableName"`
	Timestamp   DateTime `gorm:"column:timestamp;index:StatusChange_timestamp_idx;type:timestamptz(3);not null" json:"timestamp"`
	Status      string   `gorm:"column:status;not null" json:"status"`
	StartedAt   DateTime `gorm:"column:startedAt;index:StatusChange_startedAt_idx;not null" json:"startedAt"`
	Counts      int      `gorm:"column:counts;default:0;not null" json:"counts"`
	Description *string  `gorm:"column:description" json:"description"`
	QueueSize   int      `gorm:"column:queueSize;default:0;not null" json:"queueSize"`
}

func (StatusChange) TableName() string {
	return "StatusChange"
}

func (m *StatusChange) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.Timestamp.IsZero() {
		m.Timestamp = DateTime{Time: now}
	}
	return nil
}

type NotificationState struct {
	WorkspaceID      string    `gorm:"column:workspaceId;not null" json:"workspaceId"`
	ActorID          string    `gorm:"column:actorId;primaryKey" json:"actorId"`
	Type             string    `gorm:"column:type;primaryKey" json:"type"`
	TableName_       string    `gorm:"column:tableName;primaryKey" json:"tableName"`
	ChannelID        string    `gorm:"column:channelId;primaryKey" json:"channelId"`
	FlappingSince    *DateTime `gorm:"column:flappingSince;type:timestamptz(3)" json:"flappingSince"`
	LastNotification DateTime  `gorm:"column:lastNotification;type:timestamptz(3);not null" json:"lastNotification"`
	StatusChangeID   int64     `gorm:"column:statusChangeId;not null" json:"statusChangeId"`
	Error            *string   `gorm:"column:error" json:"error"`
}

func (NotificationState) TableName() string {
	return "NotificationState"
}

type Notification struct {
	ID             string   `gorm:"column:id;primaryKey;type:char(36)" json:"id"`
	WorkspaceID    string   `gorm:"column:workspaceId;not null" json:"workspaceId"`
	ActorID        string   `gorm:"column:actorId;index:Notification_actorId_type_tableName_idx;not null" json:"actorId"`
	Type           string   `gorm:"column:type;index:Notification_actorId_type_tableName_idx;not null" json:"type"`
	TableName_     string   `gorm:"column:tableName;index:Notification_actorId_type_tableName_idx;not null" json:"tableName"`
	ChannelID      string   `gorm:"column:channelId;not null" json:"channelId"`
	StatusChangeID int64    `gorm:"column:statusChangeId;not null" json:"statusChangeId"`
	Timestamp      DateTime `gorm:"column:timestamp;index:Notification_timestamp_idx;type:timestamptz(3);not null" json:"timestamp"`
	Status         string   `gorm:"column:status;not null" json:"status"`
	Error          *string  `gorm:"column:error" json:"error"`
}

func (Notification) TableName() string {
	return "Notification"
}

func (m *Notification) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = uuid.NewString()
	}
	if m.Timestamp.IsZero() {
		m.Timestamp = DateTime{Time: now}
	}
	return nil
}

type OidcProvider struct {
	ID                    string           `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt             DateTime         `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt             DateTime         `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Name                  string           `gorm:"column:name;not null" json:"name"`
	Issuer                string           `gorm:"column:issuer;uniqueIndex:OidcProvider_issuer_clientId_key;not null" json:"issuer"`
	ClientID              string           `gorm:"column:clientId;uniqueIndex:OidcProvider_issuer_clientId_key;not null" json:"clientId"`
	ClientSecret          string           `gorm:"column:clientSecret;not null" json:"clientSecret"`
	AuthorizationEndpoint *string          `gorm:"column:authorizationEndpoint" json:"authorizationEndpoint"`
	TokenEndpoint         *string          `gorm:"column:tokenEndpoint" json:"tokenEndpoint"`
	UserinfoEndpoint      *string          `gorm:"column:userinfoEndpoint" json:"userinfoEndpoint"`
	JwksURI               *string          `gorm:"column:jwksUri" json:"jwksUri"`
	IntrospectionEndpoint *string          `gorm:"column:introspectionEndpoint" json:"introspectionEndpoint"`
	Scopes                []string         `gorm:"column:scopes;serializer:json;not null" json:"scopes"`
	Audience              *string          `gorm:"column:audience" json:"audience"`
	Prompt                *string          `gorm:"column:prompt" json:"prompt"`
	UsernameClaim         *string          `gorm:"column:usernameClaim;default:'email';not null" json:"usernameClaim"`
	EmailClaim            *string          `gorm:"column:emailClaim;default:'email';not null" json:"emailClaim"`
	NameClaim             *string          `gorm:"column:nameClaim;default:'name';not null" json:"nameClaim"`
	GroupClaim            *string          `gorm:"column:groupClaim;default:'groups';not null" json:"groupClaim"`
	Enabled               *bool            `gorm:"column:enabled;default:true;not null" json:"enabled"`
	AutoDiscovery         *bool            `gorm:"column:autoDiscovery;default:true;not null" json:"autoDiscovery"`
	OidcLoginGroups       []OidcLoginGroup `gorm:"foreignKey:OidcProviderID"`
}

func (OidcProvider) TableName() string {
	return "OidcProvider"
}

func (m *OidcProvider) BeforeCreate(tx *gorm.DB) error {
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

func (*OidcProvider) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type OidcLoginGroup struct {
	ID             string   `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt      DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt      DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	WorkspaceID    string   `gorm:"column:workspaceId;uniqueIndex:OidcLoginGroup_workspaceId_oidcProviderId_key;index:OidcLoginGroup_workspaceId_idx;not null" json:"workspaceId"`
	OidcProviderID string   `gorm:"column:oidcProviderId;uniqueIndex:OidcLoginGroup_workspaceId_oidcProviderId_key;index:OidcLoginGroup_oidcProviderId_idx;not null" json:"oidcProviderId"`
	GroupValue     *string  `gorm:"column:groupValue" json:"groupValue"`
	AllowAllUsers  bool     `gorm:"column:allowAllUsers;default:false;not null" json:"allowAllUsers"`
	Workspace      *Workspace
	OidcProvider   *OidcProvider
}

func (OidcLoginGroup) TableName() string {
	return "OidcLoginGroup"
}

func (m *OidcLoginGroup) BeforeCreate(tx *gorm.DB) error {
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

func (*OidcLoginGroup) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type ReprocessingJobs struct {
	ID                  string                `gorm:"column:id;primaryKey;type:varchar(64)" json:"id"`
	Config              datatypes.JSON        `gorm:"column:config;not null" json:"config"`
	Status              string                `gorm:"column:status;type:varchar(32);not null" json:"status"`
	CreatedAt           DateTime              `gorm:"column:created_at;index:reprocessing_jobs_created_at_idx;type:timestamptz(6);autoCreateTime:false;not null" json:"created_at"`
	StartedAt           *DateTime             `gorm:"column:started_at;type:timestamptz(6)" json:"started_at"`
	CompletedAt         *DateTime             `gorm:"column:completed_at;type:timestamptz(6)" json:"completed_at"`
	K8sJobName          *string               `gorm:"column:k8s_job_name;type:varchar(255)" json:"k8s_job_name"`
	TotalFiles          *int                  `gorm:"column:total_files;default:0" json:"total_files"`
	TotalWorkers        *int                  `gorm:"column:total_workers;default:0" json:"total_workers"`
	Error               *string               `gorm:"column:error" json:"error"`
	ReprocessingWorkers []ReprocessingWorkers `gorm:"foreignKey:JobID;constraint:OnUpdate:NO ACTION,OnDelete:CASCADE"`
}

func (ReprocessingJobs) TableName() string {
	return "reprocessing_jobs"
}

func (m *ReprocessingJobs) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = cuid.New()
	}
	return nil
}

type ReprocessingWorkers struct {
	JobID          string            `gorm:"column:job_id;primaryKey;index:reprocessing_workers_job_id_idx;index:reprocessing_workers_status_idx;type:varchar(64)" json:"job_id"`
	WorkerIndex    int               `gorm:"column:worker_index;primaryKey" json:"worker_index"`
	Status         string            `gorm:"column:status;index:reprocessing_workers_status_idx;type:varchar(32);not null" json:"status"`
	StartedAt      *DateTime         `gorm:"column:started_at;type:timestamptz(6)" json:"started_at"`
	UpdatedAt      DateTime          `gorm:"column:updated_at;type:timestamptz(6);autoUpdateTime:false;not null" json:"updated_at"`
	CompletedAt    *DateTime         `gorm:"column:completed_at;type:timestamptz(6)" json:"completed_at"`
	CurrentFile    *string           `gorm:"column:current_file" json:"current_file"`
	CurrentLine    *int64            `gorm:"column:current_line;default:0" json:"current_line"`
	CurrentTs      *string           `gorm:"column:current_ts" json:"current_ts"`
	AssignedFiles  *int              `gorm:"column:assigned_files;default:0" json:"assigned_files"`
	ProcessedFiles *int              `gorm:"column:processed_files;default:0" json:"processed_files"`
	TotalLines     *int64            `gorm:"column:total_lines;default:0" json:"total_lines"`
	SuccessCount   *int64            `gorm:"column:success_count;default:0" json:"success_count"`
	ErrorCount     *int64            `gorm:"column:error_count;default:0" json:"error_count"`
	SkippedCount   *int64            `gorm:"column:skipped_count;default:0" json:"skipped_count"`
	TotalBytes     *int64            `gorm:"column:total_bytes;default:0" json:"total_bytes"`
	ProcessedBytes *int64            `gorm:"column:processed_bytes;default:0" json:"processed_bytes"`
	Error          *string           `gorm:"column:error" json:"error"`
	Job            *ReprocessingJobs `gorm:"foreignKey:JobID"`
}

func (ReprocessingWorkers) TableName() string {
	return "reprocessing_workers"
}

func (m *ReprocessingWorkers) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

type FunctionsServer struct {
	WorkspaceID      string    `gorm:"column:workspaceId;primaryKey" json:"workspaceId"`
	Class            string    `gorm:"column:class;primaryKey" json:"class"`
	DeploymentID     string    `gorm:"column:deploymentId;not null" json:"deploymentId"`
	Connections      []string  `gorm:"column:connections;serializer:json;not null" json:"connections"`
	EmptyConnections []string  `gorm:"column:emptyConnections;serializer:json;not null" json:"emptyConnections"`
	ProfileBuilders  []string  `gorm:"column:profileBuilders;serializer:json;not null" json:"profileBuilders"`
	CreatedAt        DateTime  `gorm:"column:createdAt;type:timestamptz(3);autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt        *DateTime `gorm:"column:updatedAt;type:timestamptz(3);autoUpdateTime:false" json:"updatedAt"`
}

func (FunctionsServer) TableName() string {
	return "FunctionsServer"
}

func (m *FunctionsServer) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type RateLimitCounter struct {
	BucketKey   string   `gorm:"column:bucketKey;primaryKey" json:"bucketKey"`
	WindowStart DateTime `gorm:"column:windowStart;primaryKey;type:timestamptz(3)" json:"windowStart"`
	Count       int      `gorm:"column:count;default:0;not null" json:"count"`
	ExpiresAt   DateTime `gorm:"column:expiresAt;index:RateLimitCounter_expiresAt_idx;type:timestamptz(3);not null" json:"expiresAt"`
}

func (RateLimitCounter) TableName() string {
	return "RateLimitCounter"
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
