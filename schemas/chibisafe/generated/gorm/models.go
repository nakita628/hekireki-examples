package model

import (
	"database/sql/driver"
	"fmt"
	"strconv"
	"time"

	"gorm.io/gorm"
	"gorm.io/gorm/schema"
)

var NamingStrategy = schema.NamingStrategy{SingularTable: true, NoLowerCase: true}

type Users struct {
	ID               int              `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	UUID             string           `gorm:"column:uuid;uniqueIndex;not null" json:"uuid"`
	Username         string           `gorm:"column:username;uniqueIndex;not null" json:"username"`
	Password         string           `gorm:"column:password;not null" json:"password"`
	Enabled          *bool            `gorm:"column:enabled;default:true;not null" json:"enabled"`
	StorageQuota     *string          `gorm:"column:storageQuota;default:'0';not null" json:"storageQuota"`
	APIKey           *string          `gorm:"column:apiKey;uniqueIndex" json:"apiKey"`
	PasswordEditedAt *DateTime        `gorm:"column:passwordEditedAt" json:"passwordEditedAt"`
	APIKeyEditedAt   *DateTime        `gorm:"column:apiKeyEditedAt" json:"apiKeyEditedAt"`
	CreatedAt        DateTime         `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	EditedAt         *DateTime        `gorm:"column:editedAt" json:"editedAt"`
	Files            []Files          `gorm:"foreignKey:UserID"`
	Links            []ShortenedLinks `gorm:"foreignKey:UserID"`
	Roles            []Roles          `gorm:"many2many:_rolesTousers;joinForeignKey:B;joinReferences:A"`
}

func (Users) TableName() string {
	return "users"
}

func (m *Users) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type Roles struct {
	ID    int     `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	Name  string  `gorm:"column:name;uniqueIndex;not null" json:"name"`
	Users []Users `gorm:"many2many:_rolesTousers;joinForeignKey:A;joinReferences:B"`
}

func (Roles) TableName() string {
	return "roles"
}

type Files struct {
	ID             int              `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	UUID           string           `gorm:"column:uuid;uniqueIndex;not null" json:"uuid"`
	UserID         *int             `gorm:"column:userId" json:"userId"`
	Name           string           `gorm:"column:name;not null" json:"name"`
	Original       string           `gorm:"column:original;not null" json:"original"`
	Type           string           `gorm:"column:type;not null" json:"type"`
	Size           string           `gorm:"column:size;not null" json:"size"`
	Hash           string           `gorm:"column:hash;not null" json:"hash"`
	IP             string           `gorm:"column:ip;not null" json:"ip"`
	SourceURL      *string          `gorm:"column:sourceUrl" json:"sourceUrl"`
	IsS3           bool             `gorm:"column:isS3;default:false;not null" json:"isS3"`
	IsWatched      bool             `gorm:"column:isWatched;default:false;not null" json:"isWatched"`
	CreatedAt      DateTime         `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	EditedAt       *DateTime        `gorm:"column:editedAt" json:"editedAt"`
	Quarantine     bool             `gorm:"column:quarantine;default:false;not null" json:"quarantine"`
	User           *Users           `gorm:"foreignKey:UserID"`
	QuarantineFile *QuarantineFiles `gorm:"foreignKey:FileID;references:UUID"`
	Albums         []Albums         `gorm:"many2many:_albumsTofiles;joinForeignKey:B;joinReferences:A"`
	Tags           []Tags           `gorm:"many2many:_filesTotags;joinForeignKey:A;joinReferences:B"`
}

func (Files) TableName() string {
	return "files"
}

func (m *Files) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type QuarantineFiles struct {
	FileID string  `gorm:"column:fileId;primaryKey;uniqueIndex" json:"fileId"`
	Name   string  `gorm:"column:name;not null" json:"name"`
	Reason *string `gorm:"column:reason" json:"reason"`
	File   *Files  `gorm:"foreignKey:FileID;references:UUID"`
}

func (QuarantineFiles) TableName() string {
	return "quarantineFiles"
}

type Snippets struct {
	ID          int       `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	UUID        string    `gorm:"column:uuid;uniqueIndex;not null" json:"uuid"`
	UserID      *int      `gorm:"column:userId" json:"userId"`
	Identifier  string    `gorm:"column:identifier;uniqueIndex;not null" json:"identifier"`
	Name        string    `gorm:"column:name;not null" json:"name"`
	Description *string   `gorm:"column:description" json:"description"`
	Content     string    `gorm:"column:content;not null" json:"content"`
	Language    string    `gorm:"column:language;not null" json:"language"`
	ParentUUID  *string   `gorm:"column:parentUuid" json:"parentUuid"`
	CreatedAt   DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	EditedAt    *DateTime `gorm:"column:editedAt" json:"editedAt"`
}

func (Snippets) TableName() string {
	return "snippets"
}

func (m *Snippets) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type ShortenedLinks struct {
	ID          int      `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	UUID        string   `gorm:"column:uuid;uniqueIndex;not null" json:"uuid"`
	UserID      *int     `gorm:"column:userId" json:"userId"`
	Identifier  string   `gorm:"column:identifier;uniqueIndex;not null" json:"identifier"`
	Destination string   `gorm:"column:destination;not null" json:"destination"`
	Views       int      `gorm:"column:views;default:0;not null" json:"views"`
	CreatedAt   DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	User        *Users   `gorm:"foreignKey:UserID"`
}

func (ShortenedLinks) TableName() string {
	return "shortenedLinks"
}

func (m *ShortenedLinks) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type Albums struct {
	ID          int       `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	UUID        string    `gorm:"column:uuid;uniqueIndex;not null" json:"uuid"`
	UserID      int       `gorm:"column:userId;not null" json:"userId"`
	Name        string    `gorm:"column:name;not null" json:"name"`
	Description *string   `gorm:"column:description" json:"description"`
	ZippedAt    *DateTime `gorm:"column:zippedAt" json:"zippedAt"`
	CreatedAt   DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	EditedAt    *DateTime `gorm:"column:editedAt" json:"editedAt"`
	Nsfw        bool      `gorm:"column:nsfw;default:false;not null" json:"nsfw"`
	SortOrder   *string   `gorm:"column:sortOrder" json:"sortOrder"`
	Files       []Files   `gorm:"many2many:_albumsTofiles;joinForeignKey:A;joinReferences:B"`
}

func (Albums) TableName() string {
	return "albums"
}

func (m *Albums) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type Bans struct {
	ID        int      `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	IP        string   `gorm:"column:ip;not null" json:"ip"`
	Reason    *string  `gorm:"column:reason" json:"reason"`
	CreatedAt DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
}

func (Bans) TableName() string {
	return "bans"
}

func (m *Bans) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type Links struct {
	ID             int       `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	UUID           string    `gorm:"column:uuid;uniqueIndex;not null" json:"uuid"`
	UserID         int       `gorm:"column:userId;not null" json:"userId"`
	AlbumID        int       `gorm:"column:albumId;not null" json:"albumId"`
	Identifier     string    `gorm:"column:identifier;uniqueIndex;not null" json:"identifier"`
	Views          int       `gorm:"column:views;default:0;not null" json:"views"`
	Enabled        *bool     `gorm:"column:enabled;default:true;not null" json:"enabled"`
	EnableDownload bool      `gorm:"column:enableDownload;default:false;not null" json:"enableDownload"`
	ExpiresAt      *DateTime `gorm:"column:expiresAt" json:"expiresAt"`
	CreatedAt      DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	EditedAt       *DateTime `gorm:"column:editedAt" json:"editedAt"`
}

func (Links) TableName() string {
	return "links"
}

func (m *Links) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type Settings struct {
	ID                                     int     `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	RateLimitWindow                        int     `gorm:"column:rateLimitWindow;not null" json:"rateLimitWindow"`
	RateLimitMax                           int     `gorm:"column:rateLimitMax;not null" json:"rateLimitMax"`
	Secret                                 string  `gorm:"column:secret;not null" json:"secret"`
	ServiceName                            string  `gorm:"column:serviceName;not null" json:"serviceName"`
	ChunkSize                              string  `gorm:"column:chunkSize;not null" json:"chunkSize"`
	ChunkedUploadsTimeout                  int     `gorm:"column:chunkedUploadsTimeout;not null" json:"chunkedUploadsTimeout"`
	MaxSize                                string  `gorm:"column:maxSize;not null" json:"maxSize"`
	GenerateZips                           bool    `gorm:"column:generateZips;not null" json:"generateZips"`
	GenerateOriginalFileNameWithIdentifier bool    `gorm:"column:generateOriginalFileNameWithIdentifier;default:false;not null" json:"generateOriginalFileNameWithIdentifier"`
	GeneratedFilenameLength                int     `gorm:"column:generatedFilenameLength;not null" json:"generatedFilenameLength"`
	GeneratedAlbumLength                   int     `gorm:"column:generatedAlbumLength;not null" json:"generatedAlbumLength"`
	GeneratedLinksLength                   *int    `gorm:"column:generatedLinksLength;default:8;not null" json:"generatedLinksLength"`
	SaveDuplicatesToAlbum                  bool    `gorm:"column:saveDuplicatesToAlbum;default:false;not null" json:"saveDuplicatesToAlbum"`
	BlockedExtensions                      string  `gorm:"column:blockedExtensions;not null" json:"blockedExtensions"`
	BlockNoExtension                       bool    `gorm:"column:blockNoExtension;not null" json:"blockNoExtension"`
	PublicMode                             bool    `gorm:"column:publicMode;not null" json:"publicMode"`
	UserAccounts                           bool    `gorm:"column:userAccounts;not null" json:"userAccounts"`
	DisableStatisticsCron                  bool    `gorm:"column:disableStatisticsCron;not null" json:"disableStatisticsCron"`
	DisableUpdateCheck                     bool    `gorm:"column:disableUpdateCheck;default:false;not null" json:"disableUpdateCheck"`
	BackgroundImageURL                     string  `gorm:"column:backgroundImageURL;not null" json:"backgroundImageURL"`
	LogoURL                                string  `gorm:"column:logoURL;not null" json:"logoURL"`
	MetaDescription                        string  `gorm:"column:metaDescription;not null" json:"metaDescription"`
	MetaKeywords                           string  `gorm:"column:metaKeywords;not null" json:"metaKeywords"`
	MetaTwitterHandle                      string  `gorm:"column:metaTwitterHandle;not null" json:"metaTwitterHandle"`
	MetaDomain                             string  `gorm:"column:metaDomain;default:'';not null" json:"metaDomain"`
	ServeUploadsFrom                       string  `gorm:"column:serveUploadsFrom;default:'';not null" json:"serveUploadsFrom"`
	EnableMixedCaseFilenames               *bool   `gorm:"column:enableMixedCaseFilenames;default:true;not null" json:"enableMixedCaseFilenames"`
	UsersStorageQuota                      *string `gorm:"column:usersStorageQuota;default:'0';not null" json:"usersStorageQuota"`
	UseNetworkStorage                      bool    `gorm:"column:useNetworkStorage;default:false;not null" json:"useNetworkStorage"`
	UseMinimalHomepage                     bool    `gorm:"column:useMinimalHomepage;default:false;not null" json:"useMinimalHomepage"`
	UseURLShortener                        bool    `gorm:"column:useUrlShortener;default:false;not null" json:"useUrlShortener"`
	GenerateThumbnails                     *bool   `gorm:"column:generateThumbnails;default:true;not null" json:"generateThumbnails"`
	PrivacyPolicyPageContent               string  `gorm:"column:privacyPolicyPageContent;default:'';not null" json:"privacyPolicyPageContent"`
	TermsOfServicePageContent              string  `gorm:"column:termsOfServicePageContent;default:'';not null" json:"termsOfServicePageContent"`
	RulesPageContent                       string  `gorm:"column:rulesPageContent;default:'';not null" json:"rulesPageContent"`
	S3Region                               string  `gorm:"column:S3Region;default:'';not null" json:"S3Region"`
	S3Bucket                               string  `gorm:"column:S3Bucket;default:'';not null" json:"S3Bucket"`
	S3AccessKey                            string  `gorm:"column:S3AccessKey;default:'';not null" json:"S3AccessKey"`
	S3SecretKey                            string  `gorm:"column:S3SecretKey;default:'';not null" json:"S3SecretKey"`
	S3Endpoint                             string  `gorm:"column:S3Endpoint;default:'';not null" json:"S3Endpoint"`
	S3PathStyle                            bool    `gorm:"column:S3PathStyle;default:false;not null" json:"S3PathStyle"`
	S3PublicURL                            string  `gorm:"column:S3PublicUrl;default:'';not null" json:"S3PublicUrl"`
	DefaultSortOrder                       *string `gorm:"column:defaultSortOrder;default:'createdAt:desc';not null" json:"defaultSortOrder"`
}

func (Settings) TableName() string {
	return "settings"
}

type Statistics struct {
	ID        int      `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	BatchID   *int     `gorm:"column:batchId" json:"batchId"`
	Type      *string  `gorm:"column:type" json:"type"`
	Data      *string  `gorm:"column:data" json:"data"`
	CreatedAt DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
}

func (Statistics) TableName() string {
	return "statistics"
}

func (m *Statistics) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type Tags struct {
	ID        int       `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	UUID      string    `gorm:"column:uuid;uniqueIndex;not null" json:"uuid"`
	UserID    int       `gorm:"column:userId;not null" json:"userId"`
	Name      string    `gorm:"column:name;not null" json:"name"`
	CreatedAt DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	EditedAt  *DateTime `gorm:"column:editedAt" json:"editedAt"`
	Files     []Files   `gorm:"many2many:_filesTotags;joinForeignKey:B;joinReferences:A"`
}

func (Tags) TableName() string {
	return "tags"
}

func (m *Tags) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type Invites struct {
	ID        int       `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	CreatedBy string    `gorm:"column:createdBy;not null" json:"createdBy"`
	UsedBy    *string   `gorm:"column:usedBy" json:"usedBy"`
	Code      string    `gorm:"column:code;uniqueIndex;not null" json:"code"`
	Used      bool      `gorm:"column:used;default:false;not null" json:"used"`
	CreatedAt DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	EditedAt  *DateTime `gorm:"column:editedAt" json:"editedAt"`
}

func (Invites) TableName() string {
	return "invites"
}

func (m *Invites) BeforeCreate(tx *gorm.DB) error {
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
	return dateTime.UTC().Truncate(time.Millisecond).Format("2006-01-02T15:04:05.000-07:00"), nil
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
