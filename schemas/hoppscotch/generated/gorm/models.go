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

type Team struct {
	ID           string            `gorm:"column:id;primaryKey" json:"id"`
	Name         string            `gorm:"column:name;not null" json:"name"`
	Collections  []TeamCollection  `gorm:"foreignKey:TeamID;constraint:OnDelete:CASCADE"`
	Environments []TeamEnvironment `gorm:"foreignKey:TeamID;constraint:OnDelete:CASCADE"`
	Invitations  []TeamInvitation  `gorm:"foreignKey:TeamID;constraint:OnDelete:CASCADE"`
	Members      []TeamMember      `gorm:"foreignKey:TeamID;constraint:OnDelete:CASCADE"`
	Requests     []TeamRequest     `gorm:"foreignKey:TeamID;constraint:OnDelete:CASCADE"`
}

func (Team) TableName() string {
	return "Team"
}

func (m *Team) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = cuid.New()
	}
	return nil
}

type TeamMember struct {
	ID      string `gorm:"column:id;primaryKey;type:char(36)" json:"id"`
	Role    string `gorm:"column:role;not null" json:"role"`
	UserUID string `gorm:"column:userUid;uniqueIndex:TeamMember_teamID_userUid_key;not null" json:"userUid"`
	TeamID  string `gorm:"column:teamID;uniqueIndex:TeamMember_teamID_userUid_key;not null" json:"teamID"`
	Team    *Team
}

func (TeamMember) TableName() string {
	return "TeamMember"
}

func (m *TeamMember) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = uuid.NewString()
	}
	return nil
}

type TeamInvitation struct {
	ID           string `gorm:"column:id;primaryKey" json:"id"`
	TeamID       string `gorm:"column:teamID;uniqueIndex:TeamInvitation_teamID_inviteeEmail_key;index:TeamInvitation_teamID_idx;not null" json:"teamID"`
	CreatorUID   string `gorm:"column:creatorUid;not null" json:"creatorUid"`
	InviteeEmail string `gorm:"column:inviteeEmail;uniqueIndex:TeamInvitation_teamID_inviteeEmail_key;not null" json:"inviteeEmail"`
	InviteeRole  string `gorm:"column:inviteeRole;not null" json:"inviteeRole"`
	Team         *Team
}

func (TeamInvitation) TableName() string {
	return "TeamInvitation"
}

func (m *TeamInvitation) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = cuid.New()
	}
	return nil
}

type TeamCollection struct {
	ID         string          `gorm:"column:id;primaryKey" json:"id"`
	ParentID   *string         `gorm:"column:parentID;uniqueIndex:TeamCollection_teamID_parentID_orderIndex_key" json:"parentID"`
	TeamID     string          `gorm:"column:teamID;uniqueIndex:TeamCollection_teamID_parentID_orderIndex_key;not null" json:"teamID"`
	Title      string          `gorm:"column:title;not null" json:"title"`
	OrderIndex int             `gorm:"column:orderIndex;uniqueIndex:TeamCollection_teamID_parentID_orderIndex_key;not null" json:"orderIndex"`
	Data       datatypes.JSON  `gorm:"column:data" json:"data"`
	CreatedOn  DateTime        `gorm:"column:createdOn;type:timestamptz(3);not null" json:"createdOn"`
	UpdatedOn  DateTime        `gorm:"column:updatedOn;type:timestamptz(3);not null" json:"updatedOn"`
	Parent     *TeamCollection `gorm:"foreignKey:ParentID"`
	Team       *Team
	Children   []TeamCollection `gorm:"foreignKey:ParentID;constraint:OnDelete:CASCADE"`
	Requests   []TeamRequest    `gorm:"foreignKey:CollectionID;constraint:OnDelete:CASCADE"`
}

func (TeamCollection) TableName() string {
	return "TeamCollection"
}

func (m *TeamCollection) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedOn.IsZero() {
		m.CreatedOn = DateTime{Time: now}
	}
	if m.UpdatedOn.IsZero() {
		m.UpdatedOn = DateTime{Time: now}
	}
	return nil
}

func (*TeamCollection) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedOn") {
		tx.Statement.SetColumn("UpdatedOn", DateTime{Time: now})
	}
	return nil
}

type TeamRequest struct {
	ID           string          `gorm:"column:id;primaryKey" json:"id"`
	CollectionID string          `gorm:"column:collectionID;uniqueIndex:TeamRequest_teamID_collectionID_orderIndex_key;not null" json:"collectionID"`
	TeamID       string          `gorm:"column:teamID;uniqueIndex:TeamRequest_teamID_collectionID_orderIndex_key;not null" json:"teamID"`
	Title        string          `gorm:"column:title;not null" json:"title"`
	Request      datatypes.JSON  `gorm:"column:request;not null" json:"request"`
	MockExamples datatypes.JSON  `gorm:"column:mockExamples" json:"mockExamples"`
	OrderIndex   int             `gorm:"column:orderIndex;uniqueIndex:TeamRequest_teamID_collectionID_orderIndex_key;not null" json:"orderIndex"`
	CreatedOn    DateTime        `gorm:"column:createdOn;type:timestamptz(3);not null" json:"createdOn"`
	UpdatedOn    DateTime        `gorm:"column:updatedOn;type:timestamptz(3);not null" json:"updatedOn"`
	Collection   *TeamCollection `gorm:"foreignKey:CollectionID"`
	Team         *Team
}

func (TeamRequest) TableName() string {
	return "TeamRequest"
}

func (m *TeamRequest) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedOn.IsZero() {
		m.CreatedOn = DateTime{Time: now}
	}
	if m.UpdatedOn.IsZero() {
		m.UpdatedOn = DateTime{Time: now}
	}
	return nil
}

func (*TeamRequest) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedOn") {
		tx.Statement.SetColumn("UpdatedOn", DateTime{Time: now})
	}
	return nil
}

type TeamEnvironment struct {
	ID        string         `gorm:"column:id;primaryKey" json:"id"`
	TeamID    string         `gorm:"column:teamID;not null" json:"teamID"`
	Name      string         `gorm:"column:name;not null" json:"name"`
	Variables datatypes.JSON `gorm:"column:variables;not null" json:"variables"`
	Team      *Team
}

func (TeamEnvironment) TableName() string {
	return "TeamEnvironment"
}

func (m *TeamEnvironment) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = cuid.New()
	}
	return nil
}

type User struct {
	UID                  string                `gorm:"column:uid;primaryKey" json:"uid"`
	DisplayName          *string               `gorm:"column:displayName" json:"displayName"`
	Email                *string               `gorm:"column:email;uniqueIndex" json:"email"`
	PhotoURL             *string               `gorm:"column:photoURL" json:"photoURL"`
	IsAdmin              bool                  `gorm:"column:isAdmin;default:false;not null" json:"isAdmin"`
	RefreshToken         *string               `gorm:"column:refreshToken" json:"refreshToken"`
	CurrentRESTSession   datatypes.JSON        `gorm:"column:currentRESTSession" json:"currentRESTSession"`
	CurrentGQLSession    datatypes.JSON        `gorm:"column:currentGQLSession" json:"currentGQLSession"`
	CreatedOn            DateTime              `gorm:"column:createdOn;type:timestamptz(3);not null" json:"createdOn"`
	LastLoggedOn         *DateTime             `gorm:"column:lastLoggedOn;type:timestamptz(3)" json:"lastLoggedOn"`
	LastActiveOn         *DateTime             `gorm:"column:lastActiveOn;type:timestamptz(3)" json:"lastActiveOn"`
	ProviderAccounts     []Account             `gorm:"foreignKey:UserID;references:UID;constraint:OnDelete:CASCADE"`
	InvitedUsers         []InvitedUsers        `gorm:"foreignKey:AdminUID;references:UID;constraint:OnDelete:CASCADE"`
	MockServers          []MockServer          `gorm:"foreignKey:CreatorUID;references:UID;constraint:OnDelete:SET NULL"`
	PersonalAccessTokens []PersonalAccessToken `gorm:"foreignKey:UserUID;references:UID;constraint:OnDelete:CASCADE"`
	Shortcodes           []Shortcode           `gorm:"foreignKey:CreatorUID;references:UID"`
	UserCollections      []UserCollection      `gorm:"foreignKey:UserUID;references:UID;constraint:OnDelete:CASCADE"`
	Environments         []UserEnvironment     `gorm:"foreignKey:UserUID;references:UID;constraint:OnDelete:CASCADE"`
	History              []UserHistory         `gorm:"foreignKey:UserUID;references:UID;constraint:OnDelete:CASCADE"`
	UserRequests         []UserRequest         `gorm:"foreignKey:UserUID;references:UID;constraint:OnDelete:CASCADE"`
	VerificationTokens   []VerificationToken   `gorm:"foreignKey:UserUID;references:UID;constraint:OnDelete:CASCADE"`
	Settings             *UserSettings         `gorm:"foreignKey:UserUID;references:UID;constraint:OnDelete:CASCADE"`
}

func (User) TableName() string {
	return "User"
}

func (m *User) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.UID == "" {
		m.UID = cuid.New()
	}
	if m.CreatedOn.IsZero() {
		m.CreatedOn = DateTime{Time: now}
	}
	return nil
}

type Account struct {
	ID                   string   `gorm:"column:id;primaryKey" json:"id"`
	UserID               string   `gorm:"column:userId;not null" json:"userId"`
	Provider             string   `gorm:"column:provider;uniqueIndex:Account_provider_providerAccountId_key;not null" json:"provider"`
	ProviderAccountID    string   `gorm:"column:providerAccountId;uniqueIndex:Account_provider_providerAccountId_key;not null" json:"providerAccountId"`
	ProviderRefreshToken *string  `gorm:"column:providerRefreshToken" json:"providerRefreshToken"`
	ProviderAccessToken  *string  `gorm:"column:providerAccessToken" json:"providerAccessToken"`
	ProviderScope        *string  `gorm:"column:providerScope" json:"providerScope"`
	LoggedIn             DateTime `gorm:"column:loggedIn;type:timestamptz(3);not null" json:"loggedIn"`
	User                 *User    `gorm:"foreignKey:UserID;references:UID"`
}

func (Account) TableName() string {
	return "Account"
}

func (m *Account) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.LoggedIn.IsZero() {
		m.LoggedIn = DateTime{Time: now}
	}
	return nil
}

type VerificationToken struct {
	DeviceIdentifier string   `gorm:"column:deviceIdentifier;uniqueIndex:VerificationToken_deviceIdentifier_token_key;not null" json:"deviceIdentifier"`
	Token            string   `gorm:"column:token;primaryKey;uniqueIndex;uniqueIndex:VerificationToken_deviceIdentifier_token_key" json:"token"`
	UserUID          string   `gorm:"column:userUid;not null" json:"userUid"`
	ExpiresOn        DateTime `gorm:"column:expiresOn;type:timestamptz(3);not null" json:"expiresOn"`
	User             *User    `gorm:"references:UID"`
}

func (VerificationToken) TableName() string {
	return "VerificationToken"
}

func (m *VerificationToken) BeforeCreate(_ *gorm.DB) error {
	if m.Token == "" {
		m.Token = cuid.New()
	}
	return nil
}

type UserSettings struct {
	ID         string         `gorm:"column:id;primaryKey" json:"id"`
	UserUID    string         `gorm:"column:userUid;uniqueIndex;not null" json:"userUid"`
	Properties datatypes.JSON `gorm:"column:properties;not null" json:"properties"`
	UpdatedOn  DateTime       `gorm:"column:updatedOn;type:timestamptz(3);not null" json:"updatedOn"`
	User       *User          `gorm:"references:UID"`
}

func (UserSettings) TableName() string {
	return "UserSettings"
}

func (m *UserSettings) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.UpdatedOn.IsZero() {
		m.UpdatedOn = DateTime{Time: now}
	}
	return nil
}

func (*UserSettings) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedOn") {
		tx.Statement.SetColumn("UpdatedOn", DateTime{Time: now})
	}
	return nil
}

type UserHistory struct {
	ID               string         `gorm:"column:id;primaryKey" json:"id"`
	UserUID          string         `gorm:"column:userUid;not null" json:"userUid"`
	ReqType          string         `gorm:"column:reqType;not null" json:"reqType"`
	Request          datatypes.JSON `gorm:"column:request;not null" json:"request"`
	ResponseMetadata datatypes.JSON `gorm:"column:responseMetadata;not null" json:"responseMetadata"`
	IsStarred        bool           `gorm:"column:isStarred;not null" json:"isStarred"`
	ExecutedOn       DateTime       `gorm:"column:executedOn;type:timestamptz(3);not null" json:"executedOn"`
	User             *User          `gorm:"references:UID"`
}

func (UserHistory) TableName() string {
	return "UserHistory"
}

func (m *UserHistory) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.ExecutedOn.IsZero() {
		m.ExecutedOn = DateTime{Time: now}
	}
	return nil
}

type UserEnvironment struct {
	ID        string         `gorm:"column:id;primaryKey" json:"id"`
	UserUID   string         `gorm:"column:userUid;not null" json:"userUid"`
	Name      *string        `gorm:"column:name" json:"name"`
	Variables datatypes.JSON `gorm:"column:variables;not null" json:"variables"`
	IsGlobal  bool           `gorm:"column:isGlobal;not null" json:"isGlobal"`
	User      *User          `gorm:"references:UID"`
}

func (UserEnvironment) TableName() string {
	return "UserEnvironment"
}

func (m *UserEnvironment) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = cuid.New()
	}
	return nil
}

type InvitedUsers struct {
	AdminUID     string   `gorm:"column:adminUid;not null" json:"adminUid"`
	AdminEmail   string   `gorm:"column:adminEmail;not null" json:"adminEmail"`
	InviteeEmail string   `gorm:"column:inviteeEmail;primaryKey;uniqueIndex" json:"inviteeEmail"`
	InvitedOn    DateTime `gorm:"column:invitedOn;type:timestamptz(3);not null" json:"invitedOn"`
	User         *User    `gorm:"foreignKey:AdminUID;references:UID"`
}

func (InvitedUsers) TableName() string {
	return "InvitedUsers"
}

func (m *InvitedUsers) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.InvitedOn.IsZero() {
		m.InvitedOn = DateTime{Time: now}
	}
	return nil
}

type UserRequest struct {
	ID             string          `gorm:"column:id;primaryKey" json:"id"`
	CollectionID   string          `gorm:"column:collectionID;uniqueIndex:UserRequest_userUid_collectionID_orderIndex_key;not null" json:"collectionID"`
	UserUID        string          `gorm:"column:userUid;uniqueIndex:UserRequest_userUid_collectionID_orderIndex_key;not null" json:"userUid"`
	Title          string          `gorm:"column:title;not null" json:"title"`
	Request        datatypes.JSON  `gorm:"column:request;not null" json:"request"`
	Type           string          `gorm:"column:type;not null" json:"type"`
	MockExamples   datatypes.JSON  `gorm:"column:mockExamples" json:"mockExamples"`
	OrderIndex     int             `gorm:"column:orderIndex;uniqueIndex:UserRequest_userUid_collectionID_orderIndex_key;not null" json:"orderIndex"`
	CreatedOn      DateTime        `gorm:"column:createdOn;type:timestamptz(3);not null" json:"createdOn"`
	UpdatedOn      DateTime        `gorm:"column:updatedOn;type:timestamptz(3);not null" json:"updatedOn"`
	UserCollection *UserCollection `gorm:"foreignKey:CollectionID"`
	User           *User           `gorm:"references:UID"`
}

func (UserRequest) TableName() string {
	return "UserRequest"
}

func (m *UserRequest) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedOn.IsZero() {
		m.CreatedOn = DateTime{Time: now}
	}
	if m.UpdatedOn.IsZero() {
		m.UpdatedOn = DateTime{Time: now}
	}
	return nil
}

func (*UserRequest) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedOn") {
		tx.Statement.SetColumn("UpdatedOn", DateTime{Time: now})
	}
	return nil
}

type UserCollection struct {
	ID         string           `gorm:"column:id;primaryKey" json:"id"`
	ParentID   *string          `gorm:"column:parentID;uniqueIndex:UserCollection_userUid_parentID_orderIndex_key" json:"parentID"`
	UserUID    string           `gorm:"column:userUid;uniqueIndex:UserCollection_userUid_parentID_orderIndex_key;not null" json:"userUid"`
	Title      string           `gorm:"column:title;not null" json:"title"`
	Type       string           `gorm:"column:type;not null" json:"type"`
	OrderIndex int              `gorm:"column:orderIndex;uniqueIndex:UserCollection_userUid_parentID_orderIndex_key;not null" json:"orderIndex"`
	Data       datatypes.JSON   `gorm:"column:data" json:"data"`
	CreatedOn  DateTime         `gorm:"column:createdOn;type:timestamptz(3);not null" json:"createdOn"`
	UpdatedOn  DateTime         `gorm:"column:updatedOn;type:timestamptz(3);not null" json:"updatedOn"`
	Parent     *UserCollection  `gorm:"foreignKey:ParentID"`
	User       *User            `gorm:"references:UID"`
	Children   []UserCollection `gorm:"foreignKey:ParentID;constraint:OnDelete:CASCADE"`
	Requests   []UserRequest    `gorm:"foreignKey:CollectionID;constraint:OnDelete:CASCADE"`
}

func (UserCollection) TableName() string {
	return "UserCollection"
}

func (m *UserCollection) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedOn.IsZero() {
		m.CreatedOn = DateTime{Time: now}
	}
	if m.UpdatedOn.IsZero() {
		m.UpdatedOn = DateTime{Time: now}
	}
	return nil
}

func (*UserCollection) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedOn") {
		tx.Statement.SetColumn("UpdatedOn", DateTime{Time: now})
	}
	return nil
}

type MockServer struct {
	ID              string               `gorm:"column:id;primaryKey" json:"id"`
	Name            string               `gorm:"column:name;not null" json:"name"`
	Subdomain       string               `gorm:"column:subdomain;uniqueIndex;not null" json:"subdomain"`
	CreatorUID      *string              `gorm:"column:creatorUid" json:"creatorUid"`
	CollectionID    string               `gorm:"column:collectionID;not null" json:"collectionID"`
	WorkspaceType   string               `gorm:"column:workspaceType;not null" json:"workspaceType"`
	WorkspaceID     string               `gorm:"column:workspaceID;not null" json:"workspaceID"`
	DelayInMs       int                  `gorm:"column:delayInMs;default:0;not null" json:"delayInMs"`
	IsPublic        *bool                `gorm:"column:isPublic;default:true;not null" json:"isPublic"`
	IsActive        *bool                `gorm:"column:isActive;default:true;not null" json:"isActive"`
	HitCount        int                  `gorm:"column:hitCount;default:0;not null" json:"hitCount"`
	LastHitAt       *DateTime            `gorm:"column:lastHitAt;type:timestamptz(3)" json:"lastHitAt"`
	CreatedOn       DateTime             `gorm:"column:createdOn;type:timestamptz(3);not null" json:"createdOn"`
	UpdatedOn       DateTime             `gorm:"column:updatedOn;type:timestamptz(3);not null" json:"updatedOn"`
	DeletedAt       *DateTime            `gorm:"column:deletedAt;type:timestamptz(3)" json:"deletedAt"`
	User            *User                `gorm:"foreignKey:CreatorUID;references:UID"`
	RequestLogs     []MockServerLog      `gorm:"foreignKey:MockServerID;constraint:OnDelete:CASCADE"`
	ActivityHistory []MockServerActivity `gorm:"foreignKey:MockServerID;constraint:OnDelete:CASCADE"`
}

func (MockServer) TableName() string {
	return "MockServer"
}

func (m *MockServer) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedOn.IsZero() {
		m.CreatedOn = DateTime{Time: now}
	}
	if m.UpdatedOn.IsZero() {
		m.UpdatedOn = DateTime{Time: now}
	}
	return nil
}

func (*MockServer) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedOn") {
		tx.Statement.SetColumn("UpdatedOn", DateTime{Time: now})
	}
	return nil
}

type MockServerLog struct {
	ID              string         `gorm:"column:id;primaryKey" json:"id"`
	MockServerID    string         `gorm:"column:mockServerID;index:MockServerLog_mockServerID_idx;index:MockServerLog_mockServerID_executedAt_idx;not null" json:"mockServerID"`
	RequestMethod   string         `gorm:"column:requestMethod;not null" json:"requestMethod"`
	RequestPath     string         `gorm:"column:requestPath;not null" json:"requestPath"`
	RequestHeaders  datatypes.JSON `gorm:"column:requestHeaders;not null" json:"requestHeaders"`
	RequestBody     datatypes.JSON `gorm:"column:requestBody" json:"requestBody"`
	RequestQuery    datatypes.JSON `gorm:"column:requestQuery" json:"requestQuery"`
	ResponseStatus  int            `gorm:"column:responseStatus;not null" json:"responseStatus"`
	ResponseHeaders datatypes.JSON `gorm:"column:responseHeaders;not null" json:"responseHeaders"`
	ResponseBody    datatypes.JSON `gorm:"column:responseBody" json:"responseBody"`
	ResponseTime    int            `gorm:"column:responseTime;not null" json:"responseTime"`
	IPAddress       *string        `gorm:"column:ipAddress" json:"ipAddress"`
	UserAgent       *string        `gorm:"column:userAgent" json:"userAgent"`
	ExecutedAt      DateTime       `gorm:"column:executedAt;index:MockServerLog_mockServerID_executedAt_idx;type:timestamptz(3);not null" json:"executedAt"`
	MockServer      *MockServer
}

func (MockServerLog) TableName() string {
	return "MockServerLog"
}

func (m *MockServerLog) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.ExecutedAt.IsZero() {
		m.ExecutedAt = DateTime{Time: now}
	}
	return nil
}

type MockServerActivity struct {
	ID           string   `gorm:"column:id;primaryKey" json:"id"`
	MockServerID string   `gorm:"column:mockServerID;index:MockServerActivity_mockServerID_idx;not null" json:"mockServerID"`
	Action       string   `gorm:"column:action;not null" json:"action"`
	PerformedBy  *string  `gorm:"column:performedBy" json:"performedBy"`
	PerformedAt  DateTime `gorm:"column:performedAt;type:timestamptz(3);not null" json:"performedAt"`
	MockServer   *MockServer
}

func (MockServerActivity) TableName() string {
	return "MockServerActivity"
}

func (m *MockServerActivity) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.PerformedAt.IsZero() {
		m.PerformedAt = DateTime{Time: now}
	}
	return nil
}

type Shortcode struct {
	ID              string         `gorm:"column:id;primaryKey;uniqueIndex:Shortcode_id_creatorUid_key" json:"id"`
	Request         datatypes.JSON `gorm:"column:request;not null" json:"request"`
	CreatorUID      *string        `gorm:"column:creatorUid;uniqueIndex:Shortcode_id_creatorUid_key" json:"creatorUid"`
	EmbedProperties datatypes.JSON `gorm:"column:embedProperties" json:"embedProperties"`
	CreatedOn       DateTime       `gorm:"column:createdOn;type:timestamptz(3);not null" json:"createdOn"`
	UpdatedOn       DateTime       `gorm:"column:updatedOn;type:timestamptz(3);not null" json:"updatedOn"`
	User            *User          `gorm:"foreignKey:CreatorUID;references:UID"`
}

func (Shortcode) TableName() string {
	return "Shortcode"
}

func (m *Shortcode) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedOn.IsZero() {
		m.CreatedOn = DateTime{Time: now}
	}
	if m.UpdatedOn.IsZero() {
		m.UpdatedOn = DateTime{Time: now}
	}
	return nil
}

func (*Shortcode) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedOn") {
		tx.Statement.SetColumn("UpdatedOn", DateTime{Time: now})
	}
	return nil
}

type InfraConfig struct {
	ID                     string   `gorm:"column:id;primaryKey" json:"id"`
	Name                   string   `gorm:"column:name;uniqueIndex;not null" json:"name"`
	Value                  *string  `gorm:"column:value" json:"value"`
	IsEncrypted            bool     `gorm:"column:isEncrypted;default:false;not null" json:"isEncrypted"`
	LastSyncedEnvFileValue *string  `gorm:"column:lastSyncedEnvFileValue" json:"lastSyncedEnvFileValue"`
	CreatedOn              DateTime `gorm:"column:createdOn;type:timestamptz(3);not null" json:"createdOn"`
	UpdatedOn              DateTime `gorm:"column:updatedOn;type:timestamptz(3);not null" json:"updatedOn"`
}

func (InfraConfig) TableName() string {
	return "InfraConfig"
}

func (m *InfraConfig) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedOn.IsZero() {
		m.CreatedOn = DateTime{Time: now}
	}
	if m.UpdatedOn.IsZero() {
		m.UpdatedOn = DateTime{Time: now}
	}
	return nil
}

func (*InfraConfig) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedOn") {
		tx.Statement.SetColumn("UpdatedOn", DateTime{Time: now})
	}
	return nil
}

type PersonalAccessToken struct {
	ID        string    `gorm:"column:id;primaryKey" json:"id"`
	UserUID   string    `gorm:"column:userUid;not null" json:"userUid"`
	Label     string    `gorm:"column:label;not null" json:"label"`
	Token     string    `gorm:"column:token;uniqueIndex;not null" json:"token"`
	ExpiresOn *DateTime `gorm:"column:expiresOn;type:timestamptz(3)" json:"expiresOn"`
	CreatedOn DateTime  `gorm:"column:createdOn;type:timestamptz(3);not null" json:"createdOn"`
	UpdatedOn DateTime  `gorm:"column:updatedOn;type:timestamptz(3);not null" json:"updatedOn"`
	User      *User     `gorm:"references:UID"`
}

func (PersonalAccessToken) TableName() string {
	return "PersonalAccessToken"
}

func (m *PersonalAccessToken) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.Token == "" {
		m.Token = uuid.NewString()
	}
	if m.CreatedOn.IsZero() {
		m.CreatedOn = DateTime{Time: now}
	}
	if m.UpdatedOn.IsZero() {
		m.UpdatedOn = DateTime{Time: now}
	}
	return nil
}

func (*PersonalAccessToken) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedOn") {
		tx.Statement.SetColumn("UpdatedOn", DateTime{Time: now})
	}
	return nil
}

type InfraToken struct {
	ID         string    `gorm:"column:id;primaryKey" json:"id"`
	CreatorUID string    `gorm:"column:creatorUid;not null" json:"creatorUid"`
	Label      string    `gorm:"column:label;not null" json:"label"`
	Token      string    `gorm:"column:token;uniqueIndex;not null" json:"token"`
	ExpiresOn  *DateTime `gorm:"column:expiresOn;type:timestamptz(3)" json:"expiresOn"`
	CreatedOn  DateTime  `gorm:"column:createdOn;type:timestamptz(3);not null" json:"createdOn"`
	UpdatedOn  DateTime  `gorm:"column:updatedOn;type:timestamptz(3);not null" json:"updatedOn"`
}

func (InfraToken) TableName() string {
	return "InfraToken"
}

func (m *InfraToken) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.Token == "" {
		m.Token = uuid.NewString()
	}
	if m.CreatedOn.IsZero() {
		m.CreatedOn = DateTime{Time: now}
	}
	if m.UpdatedOn.IsZero() {
		m.UpdatedOn = DateTime{Time: now}
	}
	return nil
}

type PublishedDocs struct {
	ID                   string         `gorm:"column:id;primaryKey" json:"id"`
	Slug                 string         `gorm:"column:slug;uniqueIndex:PublishedDocs_slug_version_key;not null" json:"slug"`
	Title                string         `gorm:"column:title;not null" json:"title"`
	CollectionID         string         `gorm:"column:collectionID;index:PublishedDocs_collectionID_idx;not null" json:"collectionID"`
	CreatorUID           string         `gorm:"column:creatorUid;not null" json:"creatorUid"`
	Version              string         `gorm:"column:version;uniqueIndex:PublishedDocs_slug_version_key;not null" json:"version"`
	AutoSync             bool           `gorm:"column:autoSync;not null" json:"autoSync"`
	DocumentTree         datatypes.JSON `gorm:"column:documentTree" json:"documentTree"`
	WorkspaceType        string         `gorm:"column:workspaceType;not null" json:"workspaceType"`
	WorkspaceID          string         `gorm:"column:workspaceID;not null" json:"workspaceID"`
	EnvironmentID        *string        `gorm:"column:environmentID" json:"environmentID"`
	EnvironmentName      *string        `gorm:"column:environmentName" json:"environmentName"`
	EnvironmentVariables datatypes.JSON `gorm:"column:environmentVariables" json:"environmentVariables"`
	Metadata             datatypes.JSON `gorm:"column:metadata" json:"metadata"`
	CreatedOn            DateTime       `gorm:"column:createdOn;type:timestamptz(3);not null" json:"createdOn"`
	UpdatedOn            DateTime       `gorm:"column:updatedOn;type:timestamptz(3);not null" json:"updatedOn"`
}

func (PublishedDocs) TableName() string {
	return "PublishedDocs"
}

func (m *PublishedDocs) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedOn.IsZero() {
		m.CreatedOn = DateTime{Time: now}
	}
	if m.UpdatedOn.IsZero() {
		m.UpdatedOn = DateTime{Time: now}
	}
	return nil
}

func (*PublishedDocs) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedOn") {
		tx.Statement.SetColumn("UpdatedOn", DateTime{Time: now})
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
