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
	ID                   string                `gorm:"column:user_id;primaryKey;type:char(36)" json:"user_id"`
	Username             string                `gorm:"column:username;uniqueIndex;type:varchar(255);not null" json:"username"`
	Password             string                `gorm:"column:password;type:varchar(60);not null" json:"password"`
	Role                 string                `gorm:"column:role;type:varchar(50);not null" json:"role"`
	LogoURL              *string               `gorm:"column:logo_url;type:varchar(2183)" json:"logo_url"`
	DisplayName          *string               `gorm:"column:display_name;type:varchar(255)" json:"display_name"`
	TwoFactorRequired    bool                  `gorm:"column:two_factor_required;default:false;not null" json:"two_factor_required"`
	CreatedAt            *DateTime             `gorm:"column:created_at;type:timestamptz(6);autoCreateTime:false" json:"created_at"`
	UpdatedAt            *DateTime             `gorm:"column:updated_at;type:timestamptz(6);autoUpdateTime:false" json:"updated_at"`
	DeletedAt            *DateTime             `gorm:"column:deleted_at;type:timestamptz(6)" json:"deleted_at"`
	Websites             []Website             `gorm:"foreignKey:UserID"`
	CreatedBy            []Website             `gorm:"foreignKey:CreatedBy"`
	Links                []Link                `gorm:"foreignKey:UserID"`
	Pixels               []Pixel               `gorm:"foreignKey:UserID"`
	Teams                []TeamUser            `gorm:"foreignKey:UserID"`
	Reports              []Report              `gorm:"foreignKey:UserID"`
	Boards               []Board               `gorm:"foreignKey:UserID"`
	TwoFactorBackupCodes []TwoFactorBackupCode `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	TwoFactorOtpUseds    []TwoFactorOtpUsed    `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	APIKeys              []APIKey              `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	TwoFactorAuth        *TwoFactorAuth        `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	TwoFactorRateLimit   *TwoFactorRateLimit   `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
}

func (User) TableName() string {
	return "user"
}

func (m *User) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt == nil {
		m.CreatedAt = &DateTime{Time: now}
	}
	if m.UpdatedAt == nil {
		m.UpdatedAt = &DateTime{Time: now}
	}
	return nil
}

func (*User) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", &DateTime{Time: now})
	}
	return nil
}

type APIKey struct {
	ID         string    `gorm:"column:api_key_id;primaryKey;type:char(36)" json:"api_key_id"`
	UserID     string    `gorm:"column:user_id;index:api_key_user_id_idx;type:char(36);not null" json:"user_id"`
	Name       string    `gorm:"column:name;type:varchar(255);not null" json:"name"`
	KeyHash    string    `gorm:"column:key_hash;uniqueIndex;type:varchar(128);not null" json:"key_hash"`
	KeyPrefix  string    `gorm:"column:key_prefix;type:varchar(20);not null" json:"key_prefix"`
	LastUsedAt *DateTime `gorm:"column:last_used_at;type:timestamptz(6)" json:"last_used_at"`
	CreatedAt  *DateTime `gorm:"column:created_at;type:timestamptz(6);autoCreateTime:false" json:"created_at"`
	User       User
}

func (APIKey) TableName() string {
	return "api_key"
}

func (m *APIKey) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt == nil {
		m.CreatedAt = &DateTime{Time: now}
	}
	return nil
}

type Session struct {
	ID            string         `gorm:"column:session_id;primaryKey;type:char(36)" json:"session_id"`
	WebsiteID     string         `gorm:"column:website_id;index:session_website_id_idx;index:session_website_id_created_at_idx;index:session_website_id_created_at_browser_idx;index:session_website_id_created_at_os_idx;index:session_website_id_created_at_device_idx;index:session_website_id_created_at_screen_idx;index:session_website_id_created_at_language_idx;index:session_website_id_created_at_country_idx;index:session_website_id_created_at_region_idx;index:session_website_id_created_at_city_idx;type:char(36);not null" json:"website_id"`
	Browser       *string        `gorm:"column:browser;index:session_website_id_created_at_browser_idx;type:varchar(20)" json:"browser"`
	Os            *string        `gorm:"column:os;index:session_website_id_created_at_os_idx;type:varchar(20)" json:"os"`
	Device        *string        `gorm:"column:device;index:session_website_id_created_at_device_idx;type:varchar(20)" json:"device"`
	Screen        *string        `gorm:"column:screen;index:session_website_id_created_at_screen_idx;type:varchar(11)" json:"screen"`
	Language      *string        `gorm:"column:language;index:session_website_id_created_at_language_idx;type:varchar(35)" json:"language"`
	Country       *string        `gorm:"column:country;index:session_website_id_created_at_country_idx;type:varchar(2)" json:"country"`
	Region        *string        `gorm:"column:region;index:session_website_id_created_at_region_idx;type:varchar(20)" json:"region"`
	City          *string        `gorm:"column:city;index:session_website_id_created_at_city_idx;type:varchar(50)" json:"city"`
	DistinctID    *string        `gorm:"column:distinct_id;type:varchar(50)" json:"distinct_id"`
	CreatedAt     *DateTime      `gorm:"column:created_at;index:session_created_at_idx;index:session_website_id_created_at_idx;index:session_website_id_created_at_browser_idx;index:session_website_id_created_at_os_idx;index:session_website_id_created_at_device_idx;index:session_website_id_created_at_screen_idx;index:session_website_id_created_at_language_idx;index:session_website_id_created_at_country_idx;index:session_website_id_created_at_region_idx;index:session_website_id_created_at_city_idx;type:timestamptz(6);autoCreateTime:false" json:"created_at"`
	WebsiteEvents []WebsiteEvent `gorm:"foreignKey:SessionID"`
	SessionData   []SessionData  `gorm:"foreignKey:SessionID"`
	Revenue       []Revenue      `gorm:"foreignKey:SessionID"`
}

func (Session) TableName() string {
	return "session"
}

func (m *Session) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt == nil {
		m.CreatedAt = &DateTime{Time: now}
	}
	return nil
}

type SessionLink struct {
	WebsiteID  string    `gorm:"column:website_id;primaryKey;index:session_link_website_id_session_id_idx;type:char(36)" json:"website_id"`
	SessionID  string    `gorm:"column:session_id;primaryKey;index:session_link_website_id_session_id_idx;type:char(36)" json:"session_id"`
	DistinctID string    `gorm:"column:distinct_id;primaryKey;type:varchar(50)" json:"distinct_id"`
	CreatedAt  *DateTime `gorm:"column:created_at;type:timestamptz(6);autoCreateTime:false" json:"created_at"`
}

func (SessionLink) TableName() string {
	return "session_link"
}

func (m *SessionLink) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt == nil {
		m.CreatedAt = &DateTime{Time: now}
	}
	return nil
}

type Website struct {
	ID                  string         `gorm:"column:website_id;primaryKey;type:char(36)" json:"website_id"`
	Name                string         `gorm:"column:name;type:varchar(100);not null" json:"name"`
	Domain              *string        `gorm:"column:domain;type:varchar(500)" json:"domain"`
	ResetAt             *DateTime      `gorm:"column:reset_at;type:timestamptz(6)" json:"reset_at"`
	UserID              *string        `gorm:"column:user_id;index:website_user_id_idx;type:char(36)" json:"user_id"`
	TeamID              *string        `gorm:"column:team_id;index:website_team_id_idx;type:char(36)" json:"team_id"`
	CreatedBy           *string        `gorm:"column:created_by;index:website_created_by_idx;type:char(36)" json:"created_by"`
	CreatedAt           *DateTime      `gorm:"column:created_at;index:website_created_at_idx;type:timestamptz(6);autoCreateTime:false" json:"created_at"`
	UpdatedAt           *DateTime      `gorm:"column:updated_at;type:timestamptz(6);autoUpdateTime:false" json:"updated_at"`
	DeletedAt           *DateTime      `gorm:"column:deleted_at;type:timestamptz(6)" json:"deleted_at"`
	RecorderEnabled     bool           `gorm:"column:recorder_enabled;default:false;not null" json:"recorder_enabled"`
	ReplayConfig        datatypes.JSON `gorm:"column:replay_config" json:"replay_config"`
	User                User           `gorm:"foreignKey:UserID"`
	CreateUser          User           `gorm:"foreignKey:CreatedBy"`
	Team                Team
	Annotations         []Annotation         `gorm:"foreignKey:WebsiteID"`
	EventData           []EventData          `gorm:"foreignKey:WebsiteID"`
	Reports             []Report             `gorm:"foreignKey:WebsiteID"`
	Revenue             []Revenue            `gorm:"foreignKey:WebsiteID"`
	Segments            []Segment            `gorm:"foreignKey:WebsiteID"`
	SessionData         []SessionData        `gorm:"foreignKey:WebsiteID"`
	SessionReplays      []SessionReplay      `gorm:"foreignKey:WebsiteID"`
	SessionReplaysSaved []SessionReplaySaved `gorm:"foreignKey:WebsiteID"`
	HeatmapEvents       []HeatmapEvent       `gorm:"foreignKey:WebsiteID"`
}

func (Website) TableName() string {
	return "website"
}

func (m *Website) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt == nil {
		m.CreatedAt = &DateTime{Time: now}
	}
	if m.UpdatedAt == nil {
		m.UpdatedAt = &DateTime{Time: now}
	}
	return nil
}

func (*Website) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", &DateTime{Time: now})
	}
	return nil
}

type WebsiteEvent struct {
	ID             string    `gorm:"column:event_id;primaryKey;type:char(36)" json:"event_id"`
	WebsiteID      string    `gorm:"column:website_id;index:website_event_website_id_idx;index:website_event_website_id_created_at_idx;index:website_event_website_id_created_at_url_path_idx;index:website_event_website_id_created_at_url_query_idx;index:website_event_website_id_created_at_referrer_domain_idx;index:website_event_website_id_created_at_page_title_idx;index:website_event_website_id_created_at_event_name_idx;index:website_event_website_id_created_at_tag_idx;index:website_event_website_id_session_id_created_at_idx;index:website_event_website_id_visit_id_created_at_idx;index:website_event_website_id_created_at_hostname_idx;type:char(36);not null" json:"website_id"`
	SessionID      string    `gorm:"column:session_id;index:website_event_session_id_idx;index:website_event_website_id_session_id_created_at_idx;type:char(36);not null" json:"session_id"`
	VisitID        string    `gorm:"column:visit_id;index:website_event_visit_id_idx;index:website_event_website_id_visit_id_created_at_idx;type:char(36);not null" json:"visit_id"`
	CreatedAt      *DateTime `gorm:"column:created_at;index:website_event_created_at_idx;index:website_event_website_id_created_at_idx;index:website_event_website_id_created_at_url_path_idx;index:website_event_website_id_created_at_url_query_idx;index:website_event_website_id_created_at_referrer_domain_idx;index:website_event_website_id_created_at_page_title_idx;index:website_event_website_id_created_at_event_name_idx;index:website_event_website_id_created_at_tag_idx;index:website_event_website_id_session_id_created_at_idx;index:website_event_website_id_visit_id_created_at_idx;index:website_event_website_id_created_at_hostname_idx;type:timestamptz(6);autoCreateTime:false" json:"created_at"`
	URLPath        string    `gorm:"column:url_path;index:website_event_website_id_created_at_url_path_idx;type:varchar(500);not null" json:"url_path"`
	URLQuery       *string   `gorm:"column:url_query;index:website_event_website_id_created_at_url_query_idx;type:varchar(500)" json:"url_query"`
	UtmSource      *string   `gorm:"column:utm_source;type:varchar(255)" json:"utm_source"`
	UtmMedium      *string   `gorm:"column:utm_medium;type:varchar(255)" json:"utm_medium"`
	UtmCampaign    *string   `gorm:"column:utm_campaign;type:varchar(255)" json:"utm_campaign"`
	UtmContent     *string   `gorm:"column:utm_content;type:varchar(255)" json:"utm_content"`
	UtmTerm        *string   `gorm:"column:utm_term;type:varchar(255)" json:"utm_term"`
	ReferrerPath   *string   `gorm:"column:referrer_path;type:varchar(500)" json:"referrer_path"`
	ReferrerQuery  *string   `gorm:"column:referrer_query;type:varchar(500)" json:"referrer_query"`
	ReferrerDomain *string   `gorm:"column:referrer_domain;index:website_event_website_id_created_at_referrer_domain_idx;type:varchar(500)" json:"referrer_domain"`
	PageTitle      *string   `gorm:"column:page_title;index:website_event_website_id_created_at_page_title_idx;type:varchar(500)" json:"page_title"`
	Gclid          *string   `gorm:"column:gclid;type:varchar(255)" json:"gclid"`
	Fbclid         *string   `gorm:"column:fbclid;type:varchar(255)" json:"fbclid"`
	Msclkid        *string   `gorm:"column:msclkid;type:varchar(255)" json:"msclkid"`
	Ttclid         *string   `gorm:"column:ttclid;type:varchar(255)" json:"ttclid"`
	Lifatid        *string   `gorm:"column:li_fat_id;type:varchar(255)" json:"li_fat_id"`
	Twclid         *string   `gorm:"column:twclid;type:varchar(255)" json:"twclid"`
	EventType      *int      `gorm:"column:event_type;default:1;not null" json:"event_type"`
	EventName      *string   `gorm:"column:event_name;index:website_event_website_id_created_at_event_name_idx;type:varchar(50)" json:"event_name"`
	Tag            *string   `gorm:"column:tag;index:website_event_website_id_created_at_tag_idx;type:varchar(50)" json:"tag"`
	Hostname       *string   `gorm:"column:hostname;index:website_event_website_id_created_at_hostname_idx;type:varchar(100)" json:"hostname"`
	Lcp            *float64  `gorm:"column:lcp;type:decimal(10,1)" json:"lcp"`
	Inp            *float64  `gorm:"column:inp;type:decimal(10,1)" json:"inp"`
	Cls            *float64  `gorm:"column:cls;type:decimal(10,4)" json:"cls"`
	Fcp            *float64  `gorm:"column:fcp;type:decimal(10,1)" json:"fcp"`
	Ttfb           *float64  `gorm:"column:ttfb;type:decimal(10,1)" json:"ttfb"`
	Session        Session
	EventData      []EventData `gorm:"foreignKey:WebsiteEventID"`
}

func (WebsiteEvent) TableName() string {
	return "website_event"
}

func (m *WebsiteEvent) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt == nil {
		m.CreatedAt = &DateTime{Time: now}
	}
	return nil
}

type EventData struct {
	ID             string    `gorm:"column:event_data_id;primaryKey;type:char(36)" json:"event_data_id"`
	WebsiteID      string    `gorm:"column:website_id;index:event_data_website_id_idx;index:event_data_website_id_created_at_idx;index:event_data_website_id_created_at_data_key_idx;type:char(36);not null" json:"website_id"`
	WebsiteEventID string    `gorm:"column:website_event_id;index:event_data_website_event_id_idx;type:char(36);not null" json:"website_event_id"`
	DataKey        string    `gorm:"column:data_key;index:event_data_website_id_created_at_data_key_idx;type:varchar(500);not null" json:"data_key"`
	StringValue    *string   `gorm:"column:string_value;type:varchar(500)" json:"string_value"`
	NumberValue    *float64  `gorm:"column:number_value;type:decimal(19,4)" json:"number_value"`
	DateValue      *DateTime `gorm:"column:date_value;type:timestamptz(6)" json:"date_value"`
	DataType       int       `gorm:"column:data_type;not null" json:"data_type"`
	CreatedAt      *DateTime `gorm:"column:created_at;index:event_data_created_at_idx;index:event_data_website_id_created_at_idx;index:event_data_website_id_created_at_data_key_idx;type:timestamptz(6);autoCreateTime:false" json:"created_at"`
	Website        Website
	WebsiteEvent   WebsiteEvent
}

func (EventData) TableName() string {
	return "event_data"
}

func (m *EventData) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt == nil {
		m.CreatedAt = &DateTime{Time: now}
	}
	return nil
}

type SessionData struct {
	ID          string    `gorm:"column:session_data_id;primaryKey;type:char(36)" json:"session_data_id"`
	WebsiteID   string    `gorm:"column:website_id;index:session_data_website_id_idx;index:session_data_website_id_created_at_data_key_idx;type:char(36);not null" json:"website_id"`
	SessionID   string    `gorm:"column:session_id;uniqueIndex:session_data_session_id_data_key_key;index:session_data_session_id_idx;index:session_data_session_id_created_at_idx;type:char(36);not null" json:"session_id"`
	DataKey     string    `gorm:"column:data_key;uniqueIndex:session_data_session_id_data_key_key;index:session_data_website_id_created_at_data_key_idx;type:varchar(500);not null" json:"data_key"`
	StringValue *string   `gorm:"column:string_value;type:varchar(500)" json:"string_value"`
	NumberValue *float64  `gorm:"column:number_value;type:decimal(19,4)" json:"number_value"`
	DateValue   *DateTime `gorm:"column:date_value;type:timestamptz(6)" json:"date_value"`
	DataType    int       `gorm:"column:data_type;not null" json:"data_type"`
	DistinctID  *string   `gorm:"column:distinct_id;type:varchar(50)" json:"distinct_id"`
	CreatedAt   *DateTime `gorm:"column:created_at;index:session_data_created_at_idx;index:session_data_session_id_created_at_idx;index:session_data_website_id_created_at_data_key_idx;type:timestamptz(6);autoCreateTime:false" json:"created_at"`
	Website     Website
	Session     Session
}

func (SessionData) TableName() string {
	return "session_data"
}

func (m *SessionData) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt == nil {
		m.CreatedAt = &DateTime{Time: now}
	}
	return nil
}

type Team struct {
	ID                string     `gorm:"column:team_id;primaryKey;type:char(36)" json:"team_id"`
	Name              string     `gorm:"column:name;type:varchar(50);not null" json:"name"`
	AccessCode        *string    `gorm:"column:access_code;uniqueIndex;index:team_access_code_idx;type:varchar(50)" json:"access_code"`
	LogoURL           *string    `gorm:"column:logo_url;type:varchar(2183)" json:"logo_url"`
	TwoFactorRequired bool       `gorm:"column:two_factor_required;default:false;not null" json:"two_factor_required"`
	CreatedAt         *DateTime  `gorm:"column:created_at;type:timestamptz(6);autoCreateTime:false" json:"created_at"`
	UpdatedAt         *DateTime  `gorm:"column:updated_at;type:timestamptz(6);autoUpdateTime:false" json:"updated_at"`
	DeletedAt         *DateTime  `gorm:"column:deleted_at;type:timestamptz(6)" json:"deleted_at"`
	Websites          []Website  `gorm:"foreignKey:TeamID"`
	Members           []TeamUser `gorm:"foreignKey:TeamID"`
	Links             []Link     `gorm:"foreignKey:TeamID"`
	Pixels            []Pixel    `gorm:"foreignKey:TeamID"`
	Boards            []Board    `gorm:"foreignKey:TeamID"`
}

func (Team) TableName() string {
	return "team"
}

func (m *Team) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt == nil {
		m.CreatedAt = &DateTime{Time: now}
	}
	if m.UpdatedAt == nil {
		m.UpdatedAt = &DateTime{Time: now}
	}
	return nil
}

func (*Team) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", &DateTime{Time: now})
	}
	return nil
}

type TeamUser struct {
	ID        string    `gorm:"column:team_user_id;primaryKey;type:char(36)" json:"team_user_id"`
	TeamID    string    `gorm:"column:team_id;index:team_user_team_id_idx;type:char(36);not null" json:"team_id"`
	UserID    string    `gorm:"column:user_id;index:team_user_user_id_idx;type:char(36);not null" json:"user_id"`
	Role      string    `gorm:"column:role;type:varchar(50);not null" json:"role"`
	CreatedAt *DateTime `gorm:"column:created_at;type:timestamptz(6);autoCreateTime:false" json:"created_at"`
	UpdatedAt *DateTime `gorm:"column:updated_at;type:timestamptz(6);autoUpdateTime:false" json:"updated_at"`
	Team      Team
	User      User
}

func (TeamUser) TableName() string {
	return "team_user"
}

func (m *TeamUser) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt == nil {
		m.CreatedAt = &DateTime{Time: now}
	}
	if m.UpdatedAt == nil {
		m.UpdatedAt = &DateTime{Time: now}
	}
	return nil
}

func (*TeamUser) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", &DateTime{Time: now})
	}
	return nil
}

type Report struct {
	ID          string         `gorm:"column:report_id;primaryKey;type:char(36)" json:"report_id"`
	UserID      string         `gorm:"column:user_id;index:report_user_id_idx;type:char(36);not null" json:"user_id"`
	WebsiteID   string         `gorm:"column:website_id;index:report_website_id_idx;type:char(36);not null" json:"website_id"`
	Type        string         `gorm:"column:type;index:report_type_idx;type:varchar(50);not null" json:"type"`
	Name        string         `gorm:"column:name;index:report_name_idx;type:varchar(200);not null" json:"name"`
	Description string         `gorm:"column:description;type:varchar(500);not null" json:"description"`
	Parameters  datatypes.JSON `gorm:"column:parameters;not null" json:"parameters"`
	CreatedAt   *DateTime      `gorm:"column:created_at;type:timestamptz(6);autoCreateTime:false" json:"created_at"`
	UpdatedAt   *DateTime      `gorm:"column:updated_at;type:timestamptz(6);autoUpdateTime:false" json:"updated_at"`
	User        User
	Website     Website
}

func (Report) TableName() string {
	return "report"
}

func (m *Report) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt == nil {
		m.CreatedAt = &DateTime{Time: now}
	}
	if m.UpdatedAt == nil {
		m.UpdatedAt = &DateTime{Time: now}
	}
	return nil
}

func (*Report) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", &DateTime{Time: now})
	}
	return nil
}

type Annotation struct {
	ID        string    `gorm:"column:annotation_id;primaryKey;type:char(36)" json:"annotation_id"`
	WebsiteID string    `gorm:"column:website_id;index:annotation_website_id_date_idx;type:char(36);not null" json:"website_id"`
	UserID    *string   `gorm:"column:user_id;type:char(36)" json:"user_id"`
	Date      DateTime  `gorm:"column:date;index:annotation_website_id_date_idx;type:timestamptz(6);not null" json:"date"`
	AllDay    *bool     `gorm:"column:all_day;default:true;not null" json:"all_day"`
	Note      string    `gorm:"column:note;type:varchar(500);not null" json:"note"`
	CreatedAt *DateTime `gorm:"column:created_at;type:timestamptz(6);autoCreateTime:false" json:"created_at"`
	UpdatedAt *DateTime `gorm:"column:updated_at;type:timestamptz(6);autoUpdateTime:false" json:"updated_at"`
	Website   Website
}

func (Annotation) TableName() string {
	return "annotation"
}

func (m *Annotation) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt == nil {
		m.CreatedAt = &DateTime{Time: now}
	}
	if m.UpdatedAt == nil {
		m.UpdatedAt = &DateTime{Time: now}
	}
	return nil
}

func (*Annotation) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", &DateTime{Time: now})
	}
	return nil
}

type Segment struct {
	ID         string         `gorm:"column:segment_id;primaryKey;type:char(36)" json:"segment_id"`
	WebsiteID  string         `gorm:"column:website_id;index:segment_website_id_idx;type:char(36);not null" json:"website_id"`
	Type       string         `gorm:"column:type;type:varchar(50);not null" json:"type"`
	Name       string         `gorm:"column:name;type:varchar(200);not null" json:"name"`
	Parameters datatypes.JSON `gorm:"column:parameters;not null" json:"parameters"`
	CreatedAt  *DateTime      `gorm:"column:created_at;type:timestamptz(6);autoCreateTime:false" json:"created_at"`
	UpdatedAt  *DateTime      `gorm:"column:updated_at;type:timestamptz(6);autoUpdateTime:false" json:"updated_at"`
	Website    Website
}

func (Segment) TableName() string {
	return "segment"
}

func (m *Segment) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt == nil {
		m.CreatedAt = &DateTime{Time: now}
	}
	if m.UpdatedAt == nil {
		m.UpdatedAt = &DateTime{Time: now}
	}
	return nil
}

func (*Segment) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", &DateTime{Time: now})
	}
	return nil
}

type Revenue struct {
	ID        string    `gorm:"column:revenue_id;primaryKey;type:char(36)" json:"revenue_id"`
	WebsiteID string    `gorm:"column:website_id;index:revenue_website_id_idx;index:revenue_website_id_created_at_idx;index:revenue_website_id_session_id_created_at_idx;type:char(36);not null" json:"website_id"`
	SessionID string    `gorm:"column:session_id;index:revenue_session_id_idx;index:revenue_website_id_session_id_created_at_idx;type:char(36);not null" json:"session_id"`
	EventID   string    `gorm:"column:event_id;type:char(36);not null" json:"event_id"`
	EventName string    `gorm:"column:event_name;type:varchar(50);not null" json:"event_name"`
	Currency  string    `gorm:"column:currency;type:varchar(10);not null" json:"currency"`
	Revenue   *float64  `gorm:"column:revenue;type:decimal(19,4)" json:"revenue"`
	CreatedAt *DateTime `gorm:"column:created_at;index:revenue_website_id_created_at_idx;index:revenue_website_id_session_id_created_at_idx;type:timestamptz(6);autoCreateTime:false" json:"created_at"`
	Website   Website
	Session   Session
}

func (Revenue) TableName() string {
	return "revenue"
}

func (m *Revenue) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt == nil {
		m.CreatedAt = &DateTime{Time: now}
	}
	return nil
}

type Link struct {
	ID        string    `gorm:"column:link_id;primaryKey;type:char(36)" json:"link_id"`
	Name      string    `gorm:"column:name;type:varchar(100);not null" json:"name"`
	URL       string    `gorm:"column:url;type:varchar(500);not null" json:"url"`
	Slug      string    `gorm:"column:slug;uniqueIndex;index:link_slug_idx;type:varchar(100);not null" json:"slug"`
	UserID    *string   `gorm:"column:user_id;index:link_user_id_idx;type:char(36)" json:"user_id"`
	TeamID    *string   `gorm:"column:team_id;index:link_team_id_idx;type:char(36)" json:"team_id"`
	CreatedAt *DateTime `gorm:"column:created_at;index:link_created_at_idx;type:timestamptz(6);autoCreateTime:false" json:"created_at"`
	UpdatedAt *DateTime `gorm:"column:updated_at;type:timestamptz(6);autoUpdateTime:false" json:"updated_at"`
	DeletedAt *DateTime `gorm:"column:deleted_at;type:timestamptz(6)" json:"deleted_at"`
	User      User
	Team      Team
}

func (Link) TableName() string {
	return "link"
}

func (m *Link) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt == nil {
		m.CreatedAt = &DateTime{Time: now}
	}
	if m.UpdatedAt == nil {
		m.UpdatedAt = &DateTime{Time: now}
	}
	return nil
}

func (*Link) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", &DateTime{Time: now})
	}
	return nil
}

type Pixel struct {
	ID        string    `gorm:"column:pixel_id;primaryKey;type:char(36)" json:"pixel_id"`
	Name      string    `gorm:"column:name;type:varchar(100);not null" json:"name"`
	Slug      string    `gorm:"column:slug;uniqueIndex;index:pixel_slug_idx;type:varchar(100);not null" json:"slug"`
	UserID    *string   `gorm:"column:user_id;index:pixel_user_id_idx;type:char(36)" json:"user_id"`
	TeamID    *string   `gorm:"column:team_id;index:pixel_team_id_idx;type:char(36)" json:"team_id"`
	CreatedAt *DateTime `gorm:"column:created_at;index:pixel_created_at_idx;type:timestamptz(6);autoCreateTime:false" json:"created_at"`
	UpdatedAt *DateTime `gorm:"column:updated_at;type:timestamptz(6);autoUpdateTime:false" json:"updated_at"`
	DeletedAt *DateTime `gorm:"column:deleted_at;type:timestamptz(6)" json:"deleted_at"`
	User      User
	Team      Team
}

func (Pixel) TableName() string {
	return "pixel"
}

func (m *Pixel) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt == nil {
		m.CreatedAt = &DateTime{Time: now}
	}
	if m.UpdatedAt == nil {
		m.UpdatedAt = &DateTime{Time: now}
	}
	return nil
}

func (*Pixel) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", &DateTime{Time: now})
	}
	return nil
}

type Board struct {
	ID          string         `gorm:"column:board_id;primaryKey;type:char(36)" json:"board_id"`
	Type        string         `gorm:"column:type;type:varchar(50);not null" json:"type"`
	Name        string         `gorm:"column:name;type:varchar(200);not null" json:"name"`
	Description string         `gorm:"column:description;type:varchar(500);not null" json:"description"`
	Parameters  datatypes.JSON `gorm:"column:parameters;not null" json:"parameters"`
	UserID      *string        `gorm:"column:user_id;index:board_user_id_idx;type:char(36)" json:"user_id"`
	TeamID      *string        `gorm:"column:team_id;index:board_team_id_idx;type:char(36)" json:"team_id"`
	CreatedAt   *DateTime      `gorm:"column:created_at;index:board_created_at_idx;type:timestamptz(6);autoCreateTime:false" json:"created_at"`
	UpdatedAt   *DateTime      `gorm:"column:updated_at;type:timestamptz(6);autoUpdateTime:false" json:"updated_at"`
	User        User
	Team        Team
}

func (Board) TableName() string {
	return "board"
}

func (m *Board) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt == nil {
		m.CreatedAt = &DateTime{Time: now}
	}
	if m.UpdatedAt == nil {
		m.UpdatedAt = &DateTime{Time: now}
	}
	return nil
}

func (*Board) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", &DateTime{Time: now})
	}
	return nil
}

type Share struct {
	ID         string         `gorm:"column:share_id;primaryKey;type:char(36)" json:"share_id"`
	EntityID   string         `gorm:"column:entity_id;index:share_entity_id_idx;type:char(36);not null" json:"entity_id"`
	Name       string         `gorm:"column:name;type:varchar(200);not null" json:"name"`
	ShareType  int            `gorm:"column:share_type;not null" json:"share_type"`
	Slug       string         `gorm:"column:slug;uniqueIndex;type:varchar(100);not null" json:"slug"`
	Parameters datatypes.JSON `gorm:"column:parameters;not null" json:"parameters"`
	CreatedAt  *DateTime      `gorm:"column:created_at;type:timestamptz(6);autoCreateTime:false" json:"created_at"`
	UpdatedAt  *DateTime      `gorm:"column:updated_at;type:timestamptz(6);autoUpdateTime:false" json:"updated_at"`
}

func (Share) TableName() string {
	return "share"
}

func (m *Share) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt == nil {
		m.CreatedAt = &DateTime{Time: now}
	}
	if m.UpdatedAt == nil {
		m.UpdatedAt = &DateTime{Time: now}
	}
	return nil
}

func (*Share) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", &DateTime{Time: now})
	}
	return nil
}

type SessionReplay struct {
	ID         string    `gorm:"column:replay_id;primaryKey;type:char(36)" json:"replay_id"`
	WebsiteID  string    `gorm:"column:website_id;index:session_replay_website_id_idx;index:session_replay_website_id_session_id_idx;index:session_replay_website_id_visit_id_idx;index:session_replay_website_id_created_at_idx;type:char(36);not null" json:"website_id"`
	SessionID  string    `gorm:"column:session_id;index:session_replay_session_id_idx;index:session_replay_website_id_session_id_idx;index:session_replay_session_id_chunk_index_idx;type:char(36);not null" json:"session_id"`
	VisitID    string    `gorm:"column:visit_id;index:session_replay_visit_id_idx;index:session_replay_website_id_visit_id_idx;type:char(36);not null" json:"visit_id"`
	ChunkIndex int       `gorm:"column:chunk_index;index:session_replay_session_id_chunk_index_idx;not null" json:"chunk_index"`
	Events     []byte    `gorm:"column:events;not null" json:"events"`
	EventCount int       `gorm:"column:event_count;not null" json:"event_count"`
	StartedAt  DateTime  `gorm:"column:started_at;type:timestamptz(6);not null" json:"started_at"`
	EndedAt    DateTime  `gorm:"column:ended_at;type:timestamptz(6);not null" json:"ended_at"`
	CreatedAt  *DateTime `gorm:"column:created_at;index:session_replay_website_id_created_at_idx;type:timestamptz(6);autoCreateTime:false" json:"created_at"`
	Website    Website
}

func (SessionReplay) TableName() string {
	return "session_replay"
}

func (m *SessionReplay) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt == nil {
		m.CreatedAt = &DateTime{Time: now}
	}
	return nil
}

type SessionReplaySaved struct {
	ID        string    `gorm:"column:saved_replay_id;primaryKey;type:char(36)" json:"saved_replay_id"`
	Name      string    `gorm:"column:name;type:varchar(100);not null" json:"name"`
	WebsiteID string    `gorm:"column:website_id;uniqueIndex:session_replay_saved_website_id_visit_id_key;index:session_replay_saved_website_id_idx;index:session_replay_saved_website_id_created_at_idx;type:char(36);not null" json:"website_id"`
	VisitID   string    `gorm:"column:visit_id;uniqueIndex:session_replay_saved_website_id_visit_id_key;index:session_replay_saved_visit_id_idx;type:char(36);not null" json:"visit_id"`
	CreatedAt *DateTime `gorm:"column:created_at;index:session_replay_saved_website_id_created_at_idx;type:timestamptz(6);autoCreateTime:false" json:"created_at"`
	UpdatedAt *DateTime `gorm:"column:updated_at;type:timestamptz(6);autoUpdateTime:false" json:"updated_at"`
	Website   Website
}

func (SessionReplaySaved) TableName() string {
	return "session_replay_saved"
}

func (m *SessionReplaySaved) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt == nil {
		m.CreatedAt = &DateTime{Time: now}
	}
	if m.UpdatedAt == nil {
		m.UpdatedAt = &DateTime{Time: now}
	}
	return nil
}

func (*SessionReplaySaved) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", &DateTime{Time: now})
	}
	return nil
}

type HeatmapEvent struct {
	ID        string   `gorm:"column:heatmap_event_id;primaryKey;type:char(36)" json:"heatmap_event_id"`
	WebsiteID string   `gorm:"column:website_id;index:heatmap_event_website_id_idx;index:heatmap_event_website_id_created_at_idx;index:heatmap_event_website_id_url_path_event_type_created_at_idx;type:char(36);not null" json:"website_id"`
	SessionID string   `gorm:"column:session_id;type:char(36);not null" json:"session_id"`
	VisitID   string   `gorm:"column:visit_id;index:heatmap_event_visit_id_idx;type:char(36);not null" json:"visit_id"`
	URLPath   string   `gorm:"column:url_path;index:heatmap_event_website_id_url_path_event_type_created_at_idx;type:varchar(500);not null" json:"url_path"`
	EventType int      `gorm:"column:event_type;index:heatmap_event_website_id_url_path_event_type_created_at_idx;not null" json:"event_type"`
	X         *int     `gorm:"column:x" json:"x"`
	Y         *int     `gorm:"column:y" json:"y"`
	PageX     *int     `gorm:"column:page_x" json:"page_x"`
	PageY     *int     `gorm:"column:page_y" json:"page_y"`
	PageW     *int     `gorm:"column:page_w" json:"page_w"`
	ViewportW *int     `gorm:"column:viewport_w" json:"viewport_w"`
	ViewportH *int     `gorm:"column:viewport_h" json:"viewport_h"`
	PageH     *int     `gorm:"column:page_h" json:"page_h"`
	ScrollPct *int     `gorm:"column:scroll_pct" json:"scroll_pct"`
	CreatedAt DateTime `gorm:"column:created_at;index:heatmap_event_website_id_created_at_idx;index:heatmap_event_website_id_url_path_event_type_created_at_idx;type:timestamptz(6);autoCreateTime:false;not null" json:"created_at"`
	Website   Website
}

func (HeatmapEvent) TableName() string {
	return "heatmap_event"
}

func (m *HeatmapEvent) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type TwoFactorAuth struct {
	ID        string   `gorm:"column:id;primaryKey" json:"id"`
	UserID    string   `gorm:"column:user_id;uniqueIndex;type:char(36);not null" json:"user_id"`
	Secret    string   `gorm:"column:secret;not null" json:"secret"`
	IsEnabled bool     `gorm:"column:is_enabled;default:false;not null" json:"is_enabled"`
	CreatedAt DateTime `gorm:"column:created_at;type:timestamptz(6);autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt DateTime `gorm:"column:updated_at;type:timestamptz(6);autoUpdateTime:false;not null" json:"updated_at"`
	User      User
}

func (TwoFactorAuth) TableName() string {
	return "two_factor_auth"
}

func (m *TwoFactorAuth) BeforeCreate(tx *gorm.DB) error {
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

func (*TwoFactorAuth) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type TwoFactorBackupCode struct {
	ID        string   `gorm:"column:id;primaryKey" json:"id"`
	UserID    string   `gorm:"column:user_id;index:two_factor_backup_code_user_id_idx;type:char(36);not null" json:"user_id"`
	CodeHash  string   `gorm:"column:code_hash;not null" json:"code_hash"`
	Used      bool     `gorm:"column:used;default:false;not null" json:"used"`
	CreatedAt DateTime `gorm:"column:created_at;type:timestamptz(6);autoCreateTime:false;not null" json:"created_at"`
	User      User
}

func (TwoFactorBackupCode) TableName() string {
	return "two_factor_backup_code"
}

func (m *TwoFactorBackupCode) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type TwoFactorOtpUsed struct {
	ID        string   `gorm:"column:id;primaryKey" json:"id"`
	UserID    string   `gorm:"column:user_id;uniqueIndex:two_factor_otp_used_user_id_otp_key;type:char(36);not null" json:"user_id"`
	Otp       string   `gorm:"column:otp;uniqueIndex:two_factor_otp_used_user_id_otp_key;not null" json:"otp"`
	ExpiresAt DateTime `gorm:"column:expires_at;type:timestamptz(6);not null" json:"expires_at"`
	User      User
}

func (TwoFactorOtpUsed) TableName() string {
	return "two_factor_otp_used"
}

func (m *TwoFactorOtpUsed) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = cuid.New()
	}
	return nil
}

type TwoFactorRateLimit struct {
	ID          string    `gorm:"column:id;primaryKey" json:"id"`
	UserID      string    `gorm:"column:user_id;uniqueIndex;type:char(36);not null" json:"user_id"`
	Attempts    int       `gorm:"column:attempts;default:0;not null" json:"attempts"`
	LockedUntil *DateTime `gorm:"column:locked_until;type:timestamptz(6)" json:"locked_until"`
	UpdatedAt   DateTime  `gorm:"column:updated_at;type:timestamptz(6);autoUpdateTime:false;not null" json:"updated_at"`
	User        User
}

func (TwoFactorRateLimit) TableName() string {
	return "two_factor_rate_limit"
}

func (m *TwoFactorRateLimit) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*TwoFactorRateLimit) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type AppSetting struct {
	Key   string `gorm:"column:key;primaryKey" json:"key"`
	Value string `gorm:"column:value;not null" json:"value"`
}

func (AppSetting) TableName() string {
	return "app_setting"
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
