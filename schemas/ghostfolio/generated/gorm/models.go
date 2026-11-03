package model

import (
	"database/sql/driver"
	"fmt"
	"strconv"
	"time"

	"github.com/google/uuid"
	"gorm.io/datatypes"
	"gorm.io/gorm"
	"gorm.io/gorm/schema"
)

var NamingStrategy = schema.NamingStrategy{SingularTable: true, NoLowerCase: true}

type Access struct {
	Alias         *string        `gorm:"column:alias;index:Access_alias_idx" json:"alias"`
	CreatedAt     DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	ExpiresAt     DateTime       `gorm:"column:expiresAt;not null" json:"expiresAt"`
	GranteeUserID *string        `gorm:"column:granteeUserId;index:Access_granteeUserId_idx" json:"granteeUserId"`
	ID            string         `gorm:"column:id;primaryKey;type:char(36)" json:"id"`
	LastUsedAt    *DateTime      `gorm:"column:lastUsedAt" json:"lastUsedAt"`
	Scopes        []string       `gorm:"column:scopes;serializer:json;not null" json:"scopes"`
	Settings      datatypes.JSON `gorm:"column:settings;default:'{}';not null" json:"settings"`
	Type          string         `gorm:"column:type;default:'PRIVATE';not null" json:"type"`
	UpdatedAt     DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	UserID        string         `gorm:"column:userId;index:Access_userId_idx;not null" json:"userId"`
	GranteeUser   User           `gorm:"foreignKey:GranteeUserID"`
	User          User           `gorm:"foreignKey:UserID"`
}

func (Access) TableName() string {
	return "Access"
}

func (m *Access) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.ID == "" {
		m.ID = uuid.NewString()
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*Access) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Account struct {
	Comment    *string  `gorm:"column:comment" json:"comment"`
	CreatedAt  DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	Currency   *string  `gorm:"column:currency;index:Account_currency_idx" json:"currency"`
	ID         string   `gorm:"column:id;primaryKey;type:char(36);index:Account_id_idx" json:"id"`
	Name       *string  `gorm:"column:name;index:Account_name_idx" json:"name"`
	PlatformID *string  `gorm:"column:platformId" json:"platformId"`
	UpdatedAt  DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	UserID     string   `gorm:"column:userId;primaryKey;index:Account_userId_idx" json:"userId"`
	Platform   Platform
	User       User
	Activities []Order          `gorm:"foreignKey:AccountID,AccountUserID;references:ID,UserID"`
	Balances   []AccountBalance `gorm:"foreignKey:AccountID,UserID;references:ID,UserID;constraint:OnDelete:CASCADE"`
	Tags       []TagsOnAccounts `gorm:"foreignKey:AccountID,UserID;references:ID,UserID;constraint:OnDelete:CASCADE"`
}

func (Account) TableName() string {
	return "Account"
}

func (m *Account) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.ID == "" {
		m.ID = uuid.NewString()
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

type AccountBalance struct {
	AccountID string   `gorm:"column:accountId;uniqueIndex:AccountBalance_accountId_userId_date_key;index:AccountBalance_accountId_idx;not null" json:"accountId"`
	CreatedAt DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	Date      DateTime `gorm:"column:date;uniqueIndex:AccountBalance_accountId_userId_date_key;index:AccountBalance_date_idx;not null" json:"date"`
	ID        string   `gorm:"column:id;primaryKey;type:char(36)" json:"id"`
	UpdatedAt DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	UserID    string   `gorm:"column:userId;uniqueIndex:AccountBalance_accountId_userId_date_key;not null" json:"userId"`
	Value     float64  `gorm:"column:value;not null" json:"value"`
	Account   Account  `gorm:"foreignKey:AccountID,UserID;references:ID,UserID"`
}

func (AccountBalance) TableName() string {
	return "AccountBalance"
}

func (m *AccountBalance) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.Date.IsZero() {
		m.Date = DateTime{Time: now}
	}
	if m.ID == "" {
		m.ID = uuid.NewString()
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*AccountBalance) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Analytics struct {
	ActivityCount                       int      `gorm:"column:activityCount;default:0;not null" json:"activityCount"`
	Country                             *string  `gorm:"column:country" json:"country"`
	DataProviderGhostfolioDailyRequests int      `gorm:"column:dataProviderGhostfolioDailyRequests;default:0;not null" json:"dataProviderGhostfolioDailyRequests"`
	LastRequestAt                       DateTime `gorm:"column:lastRequestAt;index:Analytics_lastRequestAt_idx;not null" json:"lastRequestAt"`
	LastRequestClient                   *string  `gorm:"column:lastRequestClient" json:"lastRequestClient"`
	UpdatedAt                           DateTime `gorm:"column:updatedAt;index:Analytics_updatedAt_idx;autoUpdateTime:false;not null" json:"updatedAt"`
	UserID                              string   `gorm:"column:userId;primaryKey" json:"userId"`
	User                                User
}

func (Analytics) TableName() string {
	return "Analytics"
}

func (m *Analytics) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.LastRequestAt.IsZero() {
		m.LastRequestAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*Analytics) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type APIKey struct {
	CreatedAt DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	HashedKey string   `gorm:"column:hashedKey;uniqueIndex;not null" json:"hashedKey"`
	ID        string   `gorm:"column:id;primaryKey;type:char(36)" json:"id"`
	UpdatedAt DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	UserID    string   `gorm:"column:userId;index:ApiKey_userId_idx;not null" json:"userId"`
	User      User
}

func (APIKey) TableName() string {
	return "ApiKey"
}

func (m *APIKey) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.ID == "" {
		m.ID = uuid.NewString()
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

type AssetProfileOverrides struct {
	AssetClass      *string        `gorm:"column:assetClass" json:"assetClass"`
	AssetSubClass   *string        `gorm:"column:assetSubClass" json:"assetSubClass"`
	Countries       datatypes.JSON `gorm:"column:countries;default:'[]'" json:"countries"`
	Holdings        datatypes.JSON `gorm:"column:holdings;default:'[]'" json:"holdings"`
	Name            *string        `gorm:"column:name" json:"name"`
	Sectors         datatypes.JSON `gorm:"column:sectors;default:'[]'" json:"sectors"`
	SymbolProfileID string         `gorm:"column:symbolProfileId;primaryKey" json:"symbolProfileId"`
	UpdatedAt       DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	URL             *string        `gorm:"column:url" json:"url"`
	SymbolProfile   SymbolProfile
}

func (AssetProfileOverrides) TableName() string {
	return "SymbolProfileOverrides"
}

func (m *AssetProfileOverrides) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*AssetProfileOverrides) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type AssetProfileResolution struct {
	CreatedAt        DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	Currency         string   `gorm:"column:currency;not null" json:"currency"`
	DataSourceOrigin string   `gorm:"column:dataSourceOrigin;uniqueIndex:AssetProfileResolution_dataSourceOrigin_symbolOrigin_key;not null" json:"dataSourceOrigin"`
	DataSourceTarget string   `gorm:"column:dataSourceTarget;not null" json:"dataSourceTarget"`
	ID               string   `gorm:"column:id;primaryKey;type:char(36)" json:"id"`
	RequestCount     *int     `gorm:"column:requestCount;default:1;not null" json:"requestCount"`
	SymbolOrigin     string   `gorm:"column:symbolOrigin;uniqueIndex:AssetProfileResolution_dataSourceOrigin_symbolOrigin_key;not null" json:"symbolOrigin"`
	SymbolTarget     string   `gorm:"column:symbolTarget;not null" json:"symbolTarget"`
	UpdatedAt        DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
}

func (AssetProfileResolution) TableName() string {
	return "AssetProfileResolution"
}

func (m *AssetProfileResolution) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.ID == "" {
		m.ID = uuid.NewString()
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*AssetProfileResolution) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type AssetProfileSplit struct {
	CreatedAt       DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	Date            DateTime `gorm:"column:date;uniqueIndex:AssetProfileSplit_symbolProfileId_date_key;index:AssetProfileSplit_date_idx;not null" json:"date"`
	Denominator     int      `gorm:"column:denominator;not null" json:"denominator"`
	ID              string   `gorm:"column:id;primaryKey;type:char(36)" json:"id"`
	Numerator       int      `gorm:"column:numerator;not null" json:"numerator"`
	SymbolProfileID string   `gorm:"column:symbolProfileId;uniqueIndex:AssetProfileSplit_symbolProfileId_date_key;not null" json:"symbolProfileId"`
	UpdatedAt       DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	SymbolProfile   SymbolProfile
}

func (AssetProfileSplit) TableName() string {
	return "AssetProfileSplit"
}

func (m *AssetProfileSplit) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.ID == "" {
		m.ID = uuid.NewString()
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*AssetProfileSplit) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type AuthDevice struct {
	CreatedAt           DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	CredentialID        []byte   `gorm:"column:credentialId;not null" json:"credentialId"`
	CredentialPublicKey []byte   `gorm:"column:credentialPublicKey;not null" json:"credentialPublicKey"`
	Counter             int      `gorm:"column:counter;not null" json:"counter"`
	ID                  string   `gorm:"column:id;primaryKey;type:char(36)" json:"id"`
	UpdatedAt           DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	UserID              string   `gorm:"column:userId;index:AuthDevice_userId_idx;not null" json:"userId"`
	User                User
}

func (AuthDevice) TableName() string {
	return "AuthDevice"
}

func (m *AuthDevice) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.ID == "" {
		m.ID = uuid.NewString()
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*AuthDevice) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type MarketData struct {
	CreatedAt        DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	DataSource       string   `gorm:"column:dataSource;uniqueIndex:MarketData_dataSource_symbol_date_key;not null" json:"dataSource"`
	Date             DateTime `gorm:"column:date;uniqueIndex:MarketData_dataSource_symbol_date_key;index:MarketData_date_idx;not null" json:"date"`
	ID               string   `gorm:"column:id;primaryKey;type:char(36)" json:"id"`
	IsCarriedForward bool     `gorm:"column:isCarriedForward;default:false;not null" json:"isCarriedForward"`
	MarketPrice      float64  `gorm:"column:marketPrice;not null" json:"marketPrice"`
	State            string   `gorm:"column:state;index:MarketData_state_idx;default:'CLOSE';not null" json:"state"`
	Symbol           string   `gorm:"column:symbol;uniqueIndex:MarketData_dataSource_symbol_date_key;index:MarketData_symbol_idx;not null" json:"symbol"`
}

func (MarketData) TableName() string {
	return "MarketData"
}

func (m *MarketData) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.ID == "" {
		m.ID = uuid.NewString()
	}
	return nil
}

type Order struct {
	AccountID       *string  `gorm:"column:accountId;index:Order_accountId_idx" json:"accountId"`
	AccountUserID   *string  `gorm:"column:accountUserId" json:"accountUserId"`
	Comment         *string  `gorm:"column:comment" json:"comment"`
	CreatedAt       DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	Currency        *string  `gorm:"column:currency" json:"currency"`
	Date            DateTime `gorm:"column:date;index:Order_date_idx;index:Order_symbolProfileId_date_idx;index:Order_userId_date_idx;index:Order_userId_type_date_idx;not null" json:"date"`
	Fee             float64  `gorm:"column:fee;not null" json:"fee"`
	ID              string   `gorm:"column:id;primaryKey;type:char(36)" json:"id"`
	Quantity        float64  `gorm:"column:quantity;not null" json:"quantity"`
	SymbolProfileID string   `gorm:"column:symbolProfileId;index:Order_symbolProfileId_date_idx;not null" json:"symbolProfileId"`
	Type            string   `gorm:"column:type;index:Order_userId_type_date_idx;not null" json:"type"`
	UnitPrice       float64  `gorm:"column:unitPrice;not null" json:"unitPrice"`
	UpdatedAt       DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	UserID          string   `gorm:"column:userId;index:Order_userId_date_idx;index:Order_userId_type_date_idx;not null" json:"userId"`
	Account         Account  `gorm:"foreignKey:AccountID,AccountUserID;references:ID,UserID"`
	User            User
	SymbolProfile   SymbolProfile
	Tags            []Tag `gorm:"many2many:_OrderToTag;joinForeignKey:A;joinReferences:B"`
}

func (Order) TableName() string {
	return "Order"
}

func (m *Order) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.ID == "" {
		m.ID = uuid.NewString()
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*Order) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Platform struct {
	ID       string    `gorm:"column:id;primaryKey;type:char(36)" json:"id"`
	Name     *string   `gorm:"column:name;index:Platform_name_idx" json:"name"`
	URL      string    `gorm:"column:url;uniqueIndex;not null" json:"url"`
	Accounts []Account `gorm:"foreignKey:PlatformID"`
}

func (Platform) TableName() string {
	return "Platform"
}

func (m *Platform) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = uuid.NewString()
	}
	return nil
}

type Property struct {
	Key   string `gorm:"column:key;primaryKey" json:"key"`
	Value string `gorm:"column:value;not null" json:"value"`
}

func (Property) TableName() string {
	return "Property"
}

type Settings struct {
	Settings  datatypes.JSON `gorm:"column:settings" json:"settings"`
	UpdatedAt DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	UserID    string         `gorm:"column:userId;primaryKey" json:"userId"`
	User      User
}

func (Settings) TableName() string {
	return "Settings"
}

func (m *Settings) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*Settings) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type SymbolProfile struct {
	AssetClass             *string        `gorm:"column:assetClass;index:SymbolProfile_assetClass_idx" json:"assetClass"`
	AssetSubClass          *string        `gorm:"column:assetSubClass" json:"assetSubClass"`
	Comment                *string        `gorm:"column:comment" json:"comment"`
	Countries              datatypes.JSON `gorm:"column:countries" json:"countries"`
	CreatedAt              DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	Currency               string         `gorm:"column:currency;index:SymbolProfile_currency_idx;not null" json:"currency"`
	Cusip                  *string        `gorm:"column:cusip;index:SymbolProfile_cusip_idx" json:"cusip"`
	DataGatheringFrequency string         `gorm:"column:dataGatheringFrequency;index:SymbolProfile_dataGatheringFrequency_idx;default:'DAILY';not null" json:"dataGatheringFrequency"`
	DataSource             string         `gorm:"column:dataSource;uniqueIndex:SymbolProfile_dataSource_symbol_key;index:SymbolProfile_dataSource_idx;not null" json:"dataSource"`
	Figi                   *string        `gorm:"column:figi" json:"figi"`
	FigiComposite          *string        `gorm:"column:figiComposite" json:"figiComposite"`
	FigiShareClass         *string        `gorm:"column:figiShareClass" json:"figiShareClass"`
	Holdings               datatypes.JSON `gorm:"column:holdings;default:'[]'" json:"holdings"`
	ID                     string         `gorm:"column:id;primaryKey;type:char(36)" json:"id"`
	IsActive               *bool          `gorm:"column:isActive;index:SymbolProfile_isActive_idx;default:true;not null" json:"isActive"`
	Isin                   *string        `gorm:"column:isin;index:SymbolProfile_isin_idx" json:"isin"`
	Name                   *string        `gorm:"column:name;index:SymbolProfile_name_idx" json:"name"`
	ScraperConfiguration   datatypes.JSON `gorm:"column:scraperConfiguration" json:"scraperConfiguration"`
	Sectors                datatypes.JSON `gorm:"column:sectors" json:"sectors"`
	Symbol                 string         `gorm:"column:symbol;uniqueIndex:SymbolProfile_dataSource_symbol_key;index:SymbolProfile_symbol_idx;not null" json:"symbol"`
	SymbolMapping          datatypes.JSON `gorm:"column:symbolMapping" json:"symbolMapping"`
	UpdatedAt              DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	URL                    *string        `gorm:"column:url" json:"url"`
	UserID                 *string        `gorm:"column:userId" json:"userId"`
	User                   User
	Activities             []Order                `gorm:"foreignKey:SymbolProfileID"`
	AssetProfileSplits     []AssetProfileSplit    `gorm:"foreignKey:SymbolProfileID;constraint:OnDelete:CASCADE"`
	AssetProfileOverrides  *AssetProfileOverrides `gorm:"foreignKey:SymbolProfileID;constraint:OnDelete:CASCADE"`
	WatchedBy              []User                 `gorm:"many2many:_UserWatchlist;joinForeignKey:A;joinReferences:B"`
}

func (SymbolProfile) TableName() string {
	return "SymbolProfile"
}

func (m *SymbolProfile) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.ID == "" {
		m.ID = uuid.NewString()
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*SymbolProfile) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Subscription struct {
	CreatedAt               DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	ExpiresAt               DateTime `gorm:"column:expiresAt;not null" json:"expiresAt"`
	ID                      string   `gorm:"column:id;primaryKey;type:char(36)" json:"id"`
	Price                   *float64 `gorm:"column:price" json:"price"`
	StripeCheckoutSessionID *string  `gorm:"column:stripeCheckoutSessionId;uniqueIndex" json:"stripeCheckoutSessionId"`
	UpdatedAt               DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	UserID                  string   `gorm:"column:userId;index:Subscription_userId_idx;not null" json:"userId"`
	User                    User
}

func (Subscription) TableName() string {
	return "Subscription"
}

func (m *Subscription) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.ID == "" {
		m.ID = uuid.NewString()
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

type Tag struct {
	ID         string  `gorm:"column:id;primaryKey;type:char(36)" json:"id"`
	Name       string  `gorm:"column:name;uniqueIndex:Tag_name_userId_key;index:Tag_name_idx;not null" json:"name"`
	UserID     *string `gorm:"column:userId;uniqueIndex:Tag_name_userId_key" json:"userId"`
	User       User
	Accounts   []TagsOnAccounts `gorm:"foreignKey:TagID;constraint:OnDelete:CASCADE"`
	Activities []Order          `gorm:"many2many:_OrderToTag;joinForeignKey:B;joinReferences:A"`
}

func (Tag) TableName() string {
	return "Tag"
}

func (m *Tag) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = uuid.NewString()
	}
	return nil
}

type TagsOnAccounts struct {
	AccountID string   `gorm:"column:accountId;primaryKey" json:"accountId"`
	CreatedAt DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	TagID     string   `gorm:"column:tagId;primaryKey;index:TagsOnAccounts_tagId_idx" json:"tagId"`
	UpdatedAt DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	UserID    string   `gorm:"column:userId;primaryKey" json:"userId"`
	Account   Account  `gorm:"foreignKey:AccountID,UserID;references:ID,UserID"`
	Tag       Tag
}

func (TagsOnAccounts) TableName() string {
	return "TagsOnAccounts"
}

func (m *TagsOnAccounts) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*TagsOnAccounts) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type User struct {
	AccessToken   *string         `gorm:"column:accessToken;index:User_accessToken_idx" json:"accessToken"`
	AuthChallenge *string         `gorm:"column:authChallenge" json:"authChallenge"`
	CreatedAt     DateTime        `gorm:"column:createdAt;index:User_createdAt_idx;autoCreateTime:false;not null" json:"createdAt"`
	ID            string          `gorm:"column:id;primaryKey;type:char(36)" json:"id"`
	Provider      string          `gorm:"column:provider;index:User_provider_idx;default:'ANONYMOUS';not null" json:"provider"`
	Role          string          `gorm:"column:role;index:User_role_idx;default:'USER';not null" json:"role"`
	ThirdPartyID  *string         `gorm:"column:thirdPartyId;index:User_thirdPartyId_idx" json:"thirdPartyId"`
	UpdatedAt     DateTime        `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	AccessesGet   []Access        `gorm:"foreignKey:GranteeUserID;constraint:OnDelete:CASCADE"`
	AccessesGive  []Access        `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	Accounts      []Account       `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	Activities    []Order         `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	APIKeys       []APIKey        `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	AuthDevices   []AuthDevice    `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	Subscriptions []Subscription  `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	Tags          []Tag           `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	SymbolProfile []SymbolProfile `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	Analytics     *Analytics      `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	Settings      *Settings       `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	Watchlist     []SymbolProfile `gorm:"many2many:_UserWatchlist;joinForeignKey:B;joinReferences:A"`
}

func (User) TableName() string {
	return "User"
}

func (m *User) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.ID == "" {
		m.ID = uuid.NewString()
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
