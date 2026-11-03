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
	"gorm.io/gorm/schema"
)

var NamingStrategy = schema.NamingStrategy{SingularTable: true, NoLowerCase: true}

type Account struct {
	ID                string  `gorm:"column:id;primaryKey" json:"id"`
	UserID            int     `gorm:"column:userId;not null" json:"userId"`
	Type              string  `gorm:"column:type;not null" json:"type"`
	Provider          string  `gorm:"column:provider;uniqueIndex:Account_provider_providerAccountId_key;not null" json:"provider"`
	ProviderAccountID string  `gorm:"column:providerAccountId;uniqueIndex:Account_provider_providerAccountId_key;not null" json:"providerAccountId"`
	RefreshToken      *string `gorm:"column:refresh_token" json:"refresh_token"`
	AccessToken       *string `gorm:"column:access_token" json:"access_token"`
	ExpiresAt         *int    `gorm:"column:expires_at" json:"expires_at"`
	TokenType         *string `gorm:"column:token_type" json:"token_type"`
	Scope             *string `gorm:"column:scope" json:"scope"`
	IDToken           *string `gorm:"column:id_token" json:"id_token"`
	SessionState      *string `gorm:"column:session_state" json:"session_state"`
	User              *User
}

func (Account) TableName() string {
	return "Account"
}

func (m *Account) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = cuid.New()
	}
	return nil
}

type User struct {
	ID                      int                   `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	UUID                    string                `gorm:"column:uuid;uniqueIndex;type:char(36);not null" json:"uuid"`
	Name                    *string               `gorm:"column:name" json:"name"`
	Username                *string               `gorm:"column:username;uniqueIndex" json:"username"`
	Email                   *string               `gorm:"column:email;uniqueIndex" json:"email"`
	EmailVerified           *DateTime             `gorm:"column:emailVerified" json:"emailVerified"`
	UnverifiedNewEmail      *string               `gorm:"column:unverifiedNewEmail" json:"unverifiedNewEmail"`
	Image                   *string               `gorm:"column:image" json:"image"`
	Password                *string               `gorm:"column:password" json:"password"`
	Locale                  *string               `gorm:"column:locale;default:'en';not null" json:"locale"`
	ParentSubscriptionID    *int                  `gorm:"column:parentSubscriptionId" json:"parentSubscriptionId"`
	CollectionOrder         []int                 `gorm:"column:collectionOrder;serializer:json;not null" json:"collectionOrder"`
	LinksRouteTo            string                `gorm:"column:linksRouteTo;default:'ORIGINAL';not null" json:"linksRouteTo"`
	AiTaggingMethod         string                `gorm:"column:aiTaggingMethod;default:'DISABLED';not null" json:"aiTaggingMethod"`
	AiPredefinedTags        []string              `gorm:"column:aiPredefinedTags;serializer:json;not null" json:"aiPredefinedTags"`
	AiTagExistingLinks      bool                  `gorm:"column:aiTagExistingLinks;default:false;not null" json:"aiTagExistingLinks"`
	Theme                   string                `gorm:"column:theme;default:'dark';not null" json:"theme"`
	DismissedAnnouncementID *string               `gorm:"column:dismissedAnnouncementId" json:"dismissedAnnouncementId"`
	ReadableFontFamily      *string               `gorm:"column:readableFontFamily;default:'sans-serif'" json:"readableFontFamily"`
	ReadableFontSize        *string               `gorm:"column:readableFontSize;default:'20px'" json:"readableFontSize"`
	ReadableLineHeight      *string               `gorm:"column:readableLineHeight;default:'1.8'" json:"readableLineHeight"`
	ReadableLineWidth       *string               `gorm:"column:readableLineWidth;default:'normal'" json:"readableLineWidth"`
	PreventDuplicateLinks   bool                  `gorm:"column:preventDuplicateLinks;default:false;not null" json:"preventDuplicateLinks"`
	ArchiveAsScreenshot     *bool                 `gorm:"column:archiveAsScreenshot;default:true;not null" json:"archiveAsScreenshot"`
	ArchiveAsMonolith       *bool                 `gorm:"column:archiveAsMonolith;default:true;not null" json:"archiveAsMonolith"`
	ArchiveAsPDF            *bool                 `gorm:"column:archiveAsPDF;default:true;not null" json:"archiveAsPDF"`
	ArchiveAsReadable       *bool                 `gorm:"column:archiveAsReadable;default:true;not null" json:"archiveAsReadable"`
	ArchiveAsWaybackMachine bool                  `gorm:"column:archiveAsWaybackMachine;default:false;not null" json:"archiveAsWaybackMachine"`
	ReferredBy              *string               `gorm:"column:referredBy" json:"referredBy"`
	LastPickedAt            *DateTime             `gorm:"column:lastPickedAt" json:"lastPickedAt"`
	AcceptPromotionalEmails bool                  `gorm:"column:acceptPromotionalEmails;default:false;not null" json:"acceptPromotionalEmails"`
	TrialEndEmailSent       bool                  `gorm:"column:trialEndEmailSent;default:false;not null" json:"trialEndEmailSent"`
	CreatedAt               DateTime              `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt               DateTime              `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	ParentSubscription      *Subscription         `gorm:"foreignKey:ParentSubscriptionID"`
	Accounts                []Account             `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	Collections             []Collection          `gorm:"foreignKey:OwnerID;constraint:OnDelete:CASCADE"`
	Tags                    []Tag                 `gorm:"foreignKey:OwnerID;constraint:OnDelete:CASCADE"`
	CreatedLinks            []Link                `gorm:"foreignKey:CreatedByID;constraint:OnDelete:CASCADE"`
	CreatedCollections      []Collection          `gorm:"foreignKey:CreatedByID"`
	Highlights              []Highlight           `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	CollectionsJoined       []UsersAndCollections `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	AccessTokens            []AccessToken         `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	DashboardSections       []DashboardSection    `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	Subscriptions           *Subscription         `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	PinnedLinks             []Link                `gorm:"many2many:_PinnedLinks;joinForeignKey:B;joinReferences:A"`
}

func (User) TableName() string {
	return "User"
}

func (m *User) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.UUID == "" {
		m.UUID = uuid.NewString()
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
	Identifier string   `gorm:"column:identifier;uniqueIndex:VerificationToken_identifier_token_key;not null" json:"identifier"`
	Token      string   `gorm:"column:token;primaryKey;uniqueIndex;uniqueIndex:VerificationToken_identifier_token_key" json:"token"`
	Expires    DateTime `gorm:"column:expires;not null" json:"expires"`
	CreatedAt  DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt  DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
}

func (VerificationToken) TableName() string {
	return "VerificationToken"
}

func (m *VerificationToken) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
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

type PasswordResetToken struct {
	Identifier string   `gorm:"column:identifier;not null" json:"identifier"`
	Token      string   `gorm:"column:token;primaryKey;uniqueIndex" json:"token"`
	Expires    DateTime `gorm:"column:expires;not null" json:"expires"`
	CreatedAt  DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt  DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
}

func (PasswordResetToken) TableName() string {
	return "PasswordResetToken"
}

func (m *PasswordResetToken) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*PasswordResetToken) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Collection struct {
	ID               int                   `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	Name             string                `gorm:"column:name;not null" json:"name"`
	Description      string                `gorm:"column:description;default:'';not null" json:"description"`
	Icon             *string               `gorm:"column:icon" json:"icon"`
	IconWeight       *string               `gorm:"column:iconWeight" json:"iconWeight"`
	Color            *string               `gorm:"column:color;default:'#0ea5e9';not null" json:"color"`
	ParentID         *int                  `gorm:"column:parentId" json:"parentId"`
	IsPublic         bool                  `gorm:"column:isPublic;default:false;not null" json:"isPublic"`
	OwnerID          int                   `gorm:"column:ownerId;index:Collection_ownerId_idx;not null" json:"ownerId"`
	CreatedByID      *int                  `gorm:"column:createdById" json:"createdById"`
	CreatedAt        DateTime              `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt        DateTime              `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Parent           *Collection           `gorm:"foreignKey:ParentID"`
	Owner            *User                 `gorm:"foreignKey:OwnerID"`
	CreatedBy        *User                 `gorm:"foreignKey:CreatedByID"`
	SubCollections   []Collection          `gorm:"foreignKey:ParentID;constraint:OnDelete:CASCADE"`
	Members          []UsersAndCollections `gorm:"foreignKey:CollectionID;constraint:OnDelete:CASCADE"`
	Links            []Link                `gorm:"foreignKey:CollectionID;constraint:OnDelete:CASCADE"`
	RssSubscriptions []RssSubscription     `gorm:"foreignKey:CollectionID;constraint:OnDelete:CASCADE"`
	DashboardSection []DashboardSection    `gorm:"foreignKey:CollectionID;constraint:OnDelete:CASCADE"`
}

func (Collection) TableName() string {
	return "Collection"
}

func (m *Collection) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*Collection) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type UsersAndCollections struct {
	UserID       int      `gorm:"column:userId;primaryKey;index:UsersAndCollections_userId_idx" json:"userId"`
	CollectionID int      `gorm:"column:collectionId;primaryKey" json:"collectionId"`
	CanCreate    bool     `gorm:"column:canCreate;not null" json:"canCreate"`
	CanUpdate    bool     `gorm:"column:canUpdate;not null" json:"canUpdate"`
	CanDelete    bool     `gorm:"column:canDelete;not null" json:"canDelete"`
	CreatedAt    DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt    DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	User         *User
	Collection   *Collection
}

func (UsersAndCollections) TableName() string {
	return "UsersAndCollections"
}

func (m *UsersAndCollections) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*UsersAndCollections) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Link struct {
	ID              int       `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	Name            string    `gorm:"column:name;default:'';not null" json:"name"`
	Type            *string   `gorm:"column:type;default:'url';not null" json:"type"`
	Description     string    `gorm:"column:description;default:'';not null" json:"description"`
	CreatedByID     *int      `gorm:"column:createdById" json:"createdById"`
	CollectionID    int       `gorm:"column:collectionId;index:Link_collectionId_idx;not null" json:"collectionId"`
	Icon            *string   `gorm:"column:icon" json:"icon"`
	IconWeight      *string   `gorm:"column:iconWeight" json:"iconWeight"`
	Color           *string   `gorm:"column:color" json:"color"`
	URL             *string   `gorm:"column:url" json:"url"`
	TextContent     *string   `gorm:"column:textContent" json:"textContent"`
	Preview         *string   `gorm:"column:preview" json:"preview"`
	Image           *string   `gorm:"column:image" json:"image"`
	Pdf             *string   `gorm:"column:pdf" json:"pdf"`
	Readable        *string   `gorm:"column:readable" json:"readable"`
	Monolith        *string   `gorm:"column:monolith" json:"monolith"`
	ClientSide      bool      `gorm:"column:clientSide;default:false;not null" json:"clientSide"`
	AiTagged        bool      `gorm:"column:aiTagged;default:false;not null" json:"aiTagged"`
	MetaDescription *string   `gorm:"column:metaDescription" json:"metaDescription"`
	IndexVersion    *int      `gorm:"column:indexVersion" json:"indexVersion"`
	LastPreserved   *DateTime `gorm:"column:lastPreserved" json:"lastPreserved"`
	ImportDate      *DateTime `gorm:"column:importDate" json:"importDate"`
	CreatedAt       DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt       DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	CreatedBy       *User     `gorm:"foreignKey:CreatedByID"`
	Collection      *Collection
	Highlight       []Highlight `gorm:"foreignKey:LinkID;constraint:OnDelete:CASCADE"`
	PinnedBy        []User      `gorm:"many2many:_PinnedLinks;joinForeignKey:A;joinReferences:B"`
	Tags            []Tag       `gorm:"many2many:_LinkToTag;joinForeignKey:A;joinReferences:B"`
}

func (Link) TableName() string {
	return "Link"
}

func (m *Link) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*Link) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Tag struct {
	ID                      int      `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	Name                    string   `gorm:"column:name;uniqueIndex:Tag_name_ownerId_key;not null" json:"name"`
	OwnerID                 int      `gorm:"column:ownerId;uniqueIndex:Tag_name_ownerId_key;index:Tag_ownerId_idx;not null" json:"ownerId"`
	ArchiveAsScreenshot     *bool    `gorm:"column:archiveAsScreenshot" json:"archiveAsScreenshot"`
	ArchiveAsMonolith       *bool    `gorm:"column:archiveAsMonolith" json:"archiveAsMonolith"`
	ArchiveAsPDF            *bool    `gorm:"column:archiveAsPDF" json:"archiveAsPDF"`
	ArchiveAsReadable       *bool    `gorm:"column:archiveAsReadable" json:"archiveAsReadable"`
	ArchiveAsWaybackMachine *bool    `gorm:"column:archiveAsWaybackMachine" json:"archiveAsWaybackMachine"`
	AiTag                   *bool    `gorm:"column:aiTag" json:"aiTag"`
	AiGenerated             bool     `gorm:"column:aiGenerated;default:false;not null" json:"aiGenerated"`
	CreatedAt               DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt               DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Owner                   *User    `gorm:"foreignKey:OwnerID"`
	Links                   []Link   `gorm:"many2many:_LinkToTag;joinForeignKey:B;joinReferences:A"`
}

func (Tag) TableName() string {
	return "Tag"
}

func (m *Tag) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*Tag) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Subscription struct {
	ID                         int            `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	Active                     bool           `gorm:"column:active;not null" json:"active"`
	Provider                   string         `gorm:"column:provider;not null" json:"provider"`
	CurrentPeriodStart         DateTime       `gorm:"column:currentPeriodStart;not null" json:"currentPeriodStart"`
	CurrentPeriodEnd           DateTime       `gorm:"column:currentPeriodEnd;not null" json:"currentPeriodEnd"`
	Quantity                   *int           `gorm:"column:quantity;default:1;not null" json:"quantity"`
	StoreOriginalTransactionID *string        `gorm:"column:storeOriginalTransactionId;uniqueIndex" json:"storeOriginalTransactionId"`
	StoreProductID             *string        `gorm:"column:storeProductId" json:"storeProductId"`
	GooglePurchaseToken        *string        `gorm:"column:googlePurchaseToken;uniqueIndex" json:"googlePurchaseToken"`
	StoreMetadata              datatypes.JSON `gorm:"column:storeMetadata" json:"storeMetadata"`
	StripeSubscriptionID       *string        `gorm:"column:stripeSubscriptionId;uniqueIndex" json:"stripeSubscriptionId"`
	UserID                     int            `gorm:"column:userId;uniqueIndex;not null" json:"userId"`
	CreatedAt                  DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt                  DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	User                       *User
	ChildUsers                 []User `gorm:"foreignKey:ParentSubscriptionID"`
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

type AccessToken struct {
	ID         int       `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	Name       string    `gorm:"column:name;not null" json:"name"`
	UserID     int       `gorm:"column:userId;not null" json:"userId"`
	Token      string    `gorm:"column:token;uniqueIndex;not null" json:"token"`
	Revoked    bool      `gorm:"column:revoked;default:false;not null" json:"revoked"`
	IsSession  bool      `gorm:"column:isSession;default:false;not null" json:"isSession"`
	Expires    DateTime  `gorm:"column:expires;not null" json:"expires"`
	LastUsedAt *DateTime `gorm:"column:lastUsedAt" json:"lastUsedAt"`
	CreatedAt  DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt  DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	User       *User
}

func (AccessToken) TableName() string {
	return "AccessToken"
}

func (m *AccessToken) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*AccessToken) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type RssSubscription struct {
	ID            int       `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	URL           string    `gorm:"column:url;not null" json:"url"`
	Name          string    `gorm:"column:name;not null" json:"name"`
	LastBuildDate *DateTime `gorm:"column:lastBuildDate" json:"lastBuildDate"`
	CollectionID  int       `gorm:"column:collectionId;not null" json:"collectionId"`
	OwnerID       int       `gorm:"column:ownerId;not null" json:"ownerId"`
	CreatedAt     DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt     DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Collection    *Collection
}

func (RssSubscription) TableName() string {
	return "RssSubscription"
}

func (m *RssSubscription) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*RssSubscription) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Highlight struct {
	ID          int      `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	Color       string   `gorm:"column:color;not null" json:"color"`
	Comment     *string  `gorm:"column:comment" json:"comment"`
	LinkID      int      `gorm:"column:linkId;not null" json:"linkId"`
	UserID      int      `gorm:"column:userId;not null" json:"userId"`
	StartOffset int      `gorm:"column:startOffset;not null" json:"startOffset"`
	EndOffset   int      `gorm:"column:endOffset;not null" json:"endOffset"`
	Text        string   `gorm:"column:text;not null" json:"text"`
	CreatedAt   DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt   DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Link        *Link
	User        *User
}

func (Highlight) TableName() string {
	return "Highlight"
}

func (m *Highlight) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*Highlight) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type DashboardSection struct {
	ID           int      `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	UserID       int      `gorm:"column:userId;uniqueIndex:DashboardSection_userId_collectionId_key;not null" json:"userId"`
	CollectionID *int     `gorm:"column:collectionId;uniqueIndex:DashboardSection_userId_collectionId_key" json:"collectionId"`
	Type         string   `gorm:"column:type;not null" json:"type"`
	Order        int      `gorm:"column:order;not null" json:"order"`
	CreatedAt    DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt    DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	User         *User
	Collection   *Collection
}

func (DashboardSection) TableName() string {
	return "DashboardSection"
}

func (m *DashboardSection) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*DashboardSection) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type AppMigration struct {
	ID         int       `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	Name       string    `gorm:"column:name;uniqueIndex;not null" json:"name"`
	Status     string    `gorm:"column:status;not null" json:"status"`
	FinishedAt *DateTime `gorm:"column:finishedAt" json:"finishedAt"`
	CreatedAt  DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
}

func (AppMigration) TableName() string {
	return "AppMigration"
}

func (m *AppMigration) BeforeCreate(tx *gorm.DB) error {
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
