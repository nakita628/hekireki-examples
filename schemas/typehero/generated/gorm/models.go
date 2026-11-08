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
	UserID            string  `gorm:"column:userId;index:Account_userId_idx;not null" json:"userId"`
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

type Session struct {
	ID           string   `gorm:"column:id;primaryKey" json:"id"`
	SessionToken string   `gorm:"column:sessionToken;uniqueIndex;not null" json:"sessionToken"`
	UserID       string   `gorm:"column:userId;index:Session_userId_idx;not null" json:"userId"`
	Expires      DateTime `gorm:"column:expires;not null" json:"expires"`
	User         *User
}

func (Session) TableName() string {
	return "Session"
}

func (m *Session) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = cuid.New()
	}
	return nil
}

type Role struct {
	ID   string `gorm:"column:id;primaryKey;type:char(36)" json:"id"`
	Role string `gorm:"column:role;uniqueIndex;not null" json:"role"`
	User []User `gorm:"many2many:_RoleToUser;joinForeignKey:A;joinReferences:B"`
}

func (Role) TableName() string {
	return "Role"
}

func (m *Role) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = uuid.NewString()
	}
	return nil
}

type UserLink struct {
	ID        string   `gorm:"column:id;primaryKey;type:char(36)" json:"id"`
	URL       string   `gorm:"column:url;type:varchar(256);not null" json:"url"`
	CreatedAt DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	User      []User   `gorm:"many2many:_UserToUserLink;joinForeignKey:B;joinReferences:A"`
}

func (UserLink) TableName() string {
	return "UserLink"
}

func (m *UserLink) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = uuid.NewString()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*UserLink) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type User struct {
	ID                   string           `gorm:"column:id;primaryKey;type:char(36)" json:"id"`
	Name                 string           `gorm:"column:name;not null" json:"name"`
	Email                string           `gorm:"column:email;uniqueIndex;not null" json:"email"`
	EmailVerified        *DateTime        `gorm:"column:emailVerified" json:"emailVerified"`
	Image                *string          `gorm:"column:image" json:"image"`
	Bio                  string           `gorm:"column:bio;type:varchar(1000);default:'';not null" json:"bio"`
	Status               string           `gorm:"column:status;default:'ACTIVE';not null" json:"status"`
	BanReason            *string          `gorm:"column:banReason;type:text" json:"banReason"`
	CreatedAt            DateTime         `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt            DateTime         `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Accounts             []Account        `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	Sessions             []Session        `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	Challenge            []Challenge      `gorm:"foreignKey:UserID"`
	Vote                 []Vote           `gorm:"foreignKey:UserID"`
	Submission           []Submission     `gorm:"foreignKey:UserID"`
	Bookmark             []Bookmark       `gorm:"foreignKey:UserID"`
	SharedSolution       []SharedSolution `gorm:"foreignKey:UserID"`
	Comment              []Comment        `gorm:"foreignKey:UserID"`
	Reports              []Report         `gorm:"foreignKey:ReporterID"`
	ReportsAbout         []Report         `gorm:"foreignKey:UserID"`
	Moderated            []Report         `gorm:"foreignKey:ModeratorID"`
	ImageUpload          []ImageUpload    `gorm:"foreignKey:UserID"`
	BetaTokens           []BetaTokens     `gorm:"foreignKey:UserID"`
	Donations            []Donation       `gorm:"foreignKey:UserID"`
	ShortUrls            []ShortURL       `gorm:"foreignKey:UserID"`
	ToUserNotification   []Notification   `gorm:"foreignKey:ToUserID"`
	FromUserNotification []Notification   `gorm:"foreignKey:FromUserID"`
	Roles                []Role           `gorm:"many2many:_RoleToUser;joinForeignKey:B;joinReferences:A"`
	UserLinks            []UserLink       `gorm:"many2many:_UserToUserLink;joinForeignKey:A;joinReferences:B"`
	Tracks               []Track          `gorm:"many2many:_TrackToUser;joinForeignKey:B;joinReferences:A"`
}

func (User) TableName() string {
	return "User"
}

func (m *User) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = uuid.NewString()
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
}

func (VerificationToken) TableName() string {
	return "VerificationToken"
}

type Tag struct {
	ID         string      `gorm:"column:id;primaryKey;type:char(36)" json:"id"`
	Tag        string      `gorm:"column:tag;uniqueIndex;not null" json:"tag"`
	Challenges []Challenge `gorm:"many2many:_ChallengeToTag;joinForeignKey:B;joinReferences:A"`
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

type Challenge struct {
	ID               int            `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	CreatedAt        DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt        DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Difficulty       string         `gorm:"column:difficulty;not null" json:"difficulty"`
	Name             string         `gorm:"column:name;uniqueIndex;not null" json:"name"`
	Slug             string         `gorm:"column:slug;uniqueIndex;not null" json:"slug"`
	Description      string         `gorm:"column:description;type:text;not null" json:"description"`
	ShortDescription string         `gorm:"column:shortDescription;not null" json:"shortDescription"`
	Code             string         `gorm:"column:code;type:text;not null" json:"code"`
	Tests            string         `gorm:"column:tests;type:text;not null" json:"tests"`
	Status           string         `gorm:"column:status;default:'PENDING';not null" json:"status"`
	Tsconfig         datatypes.JSON `gorm:"column:tsconfig" json:"tsconfig"`
	UserID           string         `gorm:"column:userId;index:Challenge_userId_idx;not null" json:"userId"`
	User             *User
	Vote             []Vote           `gorm:"foreignKey:ChallengeID"`
	Submission       []Submission     `gorm:"foreignKey:ChallengeID"`
	Comment          []Comment        `gorm:"foreignKey:RootChallengeID"`
	Bookmark         []Bookmark       `gorm:"foreignKey:ChallengeID"`
	SharedSolution   []SharedSolution `gorm:"foreignKey:ChallengeID"`
	Report           []Report         `gorm:"foreignKey:ChallengeID"`
	TrackChallenge   []TrackChallenge `gorm:"foreignKey:ChallengeID"`
	Notification     []Notification   `gorm:"foreignKey:ChallengeID"`
	Tags             []Tag            `gorm:"many2many:_ChallengeToTag;joinForeignKey:A;joinReferences:B"`
}

func (Challenge) TableName() string {
	return "Challenge"
}

func (m *Challenge) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*Challenge) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Vote struct {
	ID               int    `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	UserID           string `gorm:"column:userId;index:Vote_userId_idx;not null" json:"userId"`
	RootType         string `gorm:"column:rootType;not null" json:"rootType"`
	ChallengeID      *int   `gorm:"column:challengeId;index:Vote_challengeId_idx" json:"challengeId"`
	CommentID        *int   `gorm:"column:commentId;index:Vote_commentId_idx" json:"commentId"`
	SubmissionID     *int   `gorm:"column:submissionId;index:Vote_submissionId_idx" json:"submissionId"`
	SharedSolutionID *int   `gorm:"column:sharedSolutionId;index:Vote_sharedSolutionId_idx" json:"sharedSolutionId"`
	User             *User
	Challenge        *Challenge
	Comment          *Comment
	Submission       *Submission
	SharedSolution   *SharedSolution
}

func (Vote) TableName() string {
	return "Vote"
}

type SharedSolution struct {
	ID              int      `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	CreatedAt       DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	IsPinned        bool     `gorm:"column:isPinned;default:false;not null" json:"isPinned"`
	Title           string   `gorm:"column:title;not null" json:"title"`
	Description     string   `gorm:"column:description;type:text;not null" json:"description"`
	UserID          *string  `gorm:"column:userId;index:SharedSolution_userId_idx" json:"userId"`
	ChallengeID     *int     `gorm:"column:challengeId;index:SharedSolution_challengeId_idx" json:"challengeId"`
	User            *User
	Challenge       *Challenge
	Vote            []Vote         `gorm:"foreignKey:SharedSolutionID"`
	SolutionComment []Comment      `gorm:"foreignKey:RootSolutionID"`
	Report          []Report       `gorm:"foreignKey:SolutionID"`
	Notification    []Notification `gorm:"foreignKey:SharedSolutionID"`
}

func (SharedSolution) TableName() string {
	return "SharedSolution"
}

func (m *SharedSolution) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type Submission struct {
	ID           int      `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	Code         string   `gorm:"column:code;type:text;not null" json:"code"`
	CreatedAt    DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	IsSuccessful bool     `gorm:"column:isSuccessful;not null" json:"isSuccessful"`
	UserID       string   `gorm:"column:userId;index:Submission_userId_idx;not null" json:"userId"`
	ChallengeID  int      `gorm:"column:challengeId;index:Submission_challengeId_idx;not null" json:"challengeId"`
	User         *User
	Challenge    *Challenge
	Vote         []Vote `gorm:"foreignKey:SubmissionID"`
}

func (Submission) TableName() string {
	return "Submission"
}

func (m *Submission) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type Bookmark struct {
	ID          int       `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	UserID      string    `gorm:"column:userId;uniqueIndex:Bookmark_userId_challengeId_key;index:Bookmark_userId_idx;not null" json:"userId"`
	ChallengeID *int      `gorm:"column:challengeId;uniqueIndex:Bookmark_userId_challengeId_key;index:Bookmark_challengeId_idx" json:"challengeId"`
	CreatedAt   *DateTime `gorm:"column:createdAt;autoCreateTime:false" json:"createdAt"`
	User        *User
	Challenge   *Challenge
}

func (Bookmark) TableName() string {
	return "Bookmark"
}

func (m *Bookmark) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt == nil {
		m.CreatedAt = &DateTime{Time: now}
	}
	return nil
}

type ReportIssue struct {
	ID       int    `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	ReportID int    `gorm:"column:reportId;index:ReportIssue_reportId_idx;not null" json:"reportId"`
	Type     string `gorm:"column:type;not null" json:"type"`
	Report   *Report
}

func (ReportIssue) TableName() string {
	return "ReportIssue"
}

type Report struct {
	ID          int      `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	Text        string   `gorm:"column:text;type:text;not null" json:"text"`
	Type        string   `gorm:"column:type;not null" json:"type"`
	Status      string   `gorm:"column:status;not null" json:"status"`
	ReporterID  string   `gorm:"column:reporterId;index:Report_reporterId_idx;not null" json:"reporterId"`
	UserID      *string  `gorm:"column:userId;index:Report_userId_idx" json:"userId"`
	ChallengeID *int     `gorm:"column:challengeId;index:Report_challengeId_idx" json:"challengeId"`
	CommentID   *int     `gorm:"column:commentId;index:Report_commentId_idx" json:"commentId"`
	SolutionID  *int     `gorm:"column:solutionId;index:Report_solutionId_idx" json:"solutionId"`
	ModeratorID *string  `gorm:"column:moderatorId;index:Report_moderatorId_idx" json:"moderatorId"`
	CreatedAt   DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt   DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Reporter    *User    `gorm:"foreignKey:ReporterID"`
	User        *User    `gorm:"foreignKey:UserID"`
	Challenge   *Challenge
	Comment     *Comment
	Solution    *SharedSolution `gorm:"foreignKey:SolutionID"`
	Moderator   *User           `gorm:"foreignKey:ModeratorID"`
	Issues      []ReportIssue   `gorm:"foreignKey:ReportID"`
}

func (Report) TableName() string {
	return "Report"
}

func (m *Report) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*Report) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Comment struct {
	ID              int      `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	Text            string   `gorm:"column:text;type:text;not null" json:"text"`
	Visible         *bool    `gorm:"column:visible;default:true;not null" json:"visible"`
	ParentID        *int     `gorm:"column:parentId;index:Comment_parentId_idx" json:"parentId"`
	UserID          string   `gorm:"column:userId;index:Comment_userId_idx;not null" json:"userId"`
	RootType        string   `gorm:"column:rootType;default:'CHALLENGE';not null" json:"rootType"`
	RootChallengeID *int     `gorm:"column:rootChallengeId;index:Comment_rootChallengeId_idx" json:"rootChallengeId"`
	RootSolutionID  *int     `gorm:"column:rootSolutionId;index:Comment_rootSolutionId_idx" json:"rootSolutionId"`
	CreatedAt       DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt       DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	ParentComment   *Comment `gorm:"foreignKey:ParentID"`
	User            *User
	RootChallenge   *Challenge      `gorm:"foreignKey:RootChallengeID"`
	RootSolution    *SharedSolution `gorm:"foreignKey:RootSolutionID"`
	Replies         []Comment       `gorm:"foreignKey:ParentID;constraint:OnUpdate:NO ACTION,OnDelete:NO ACTION"`
	Report          []Report        `gorm:"foreignKey:CommentID"`
	Vote            []Vote          `gorm:"foreignKey:CommentID"`
	Notification    []Notification  `gorm:"foreignKey:CommentID"`
}

func (Comment) TableName() string {
	return "Comment"
}

func (m *Comment) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*Comment) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type ImageUpload struct {
	ID        string   `gorm:"column:id;primaryKey;type:char(36)" json:"id"`
	UserID    string   `gorm:"column:userId;index:ImageUpload_userId_idx;not null" json:"userId"`
	URL       string   `gorm:"column:url;type:varchar(256);not null" json:"url"`
	CreatedAt DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	User      *User
}

func (ImageUpload) TableName() string {
	return "ImageUpload"
}

func (m *ImageUpload) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = uuid.NewString()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*ImageUpload) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type TrackChallenge struct {
	ID          int  `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	OrderID     int  `gorm:"column:orderId;not null" json:"orderId"`
	ChallengeID int  `gorm:"column:challengeId;index:TrackChallenge_challengeId_idx;not null" json:"challengeId"`
	TrackID     *int `gorm:"column:trackId;index:TrackChallenge_trackId_idx" json:"trackId"`
	Challenge   *Challenge
	Track       *Track
}

func (TrackChallenge) TableName() string {
	return "TrackChallenge"
}

type Track struct {
	ID              int              `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	Name            string           `gorm:"column:name;uniqueIndex;not null" json:"name"`
	Slug            string           `gorm:"column:slug;uniqueIndex;not null" json:"slug"`
	Description     string           `gorm:"column:description;type:text;not null" json:"description"`
	Visible         *bool            `gorm:"column:visible;default:true;not null" json:"visible"`
	IsComingSoon    bool             `gorm:"column:isComingSoon;default:false;not null" json:"isComingSoon"`
	TrackChallenges []TrackChallenge `gorm:"foreignKey:TrackID"`
	EnrolledUsers   []User           `gorm:"many2many:_TrackToUser;joinForeignKey:A;joinReferences:B"`
}

func (Track) TableName() string {
	return "Track"
}

type BetaTokens struct {
	ID     int     `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	Token  string  `gorm:"column:token;uniqueIndex;not null" json:"token"`
	UserID *string `gorm:"column:userId;index:BetaTokens_userId_idx" json:"userId"`
	User   *User
}

func (BetaTokens) TableName() string {
	return "BetaTokens"
}

type Donation struct {
	ID                      int       `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	UserID                  string    `gorm:"column:userId;index:Donation_userId_idx;not null" json:"userId"`
	Amount                  int       `gorm:"column:amount;not null" json:"amount"`
	StripeCheckoutSessionID string    `gorm:"column:stripeCheckoutSessionId;uniqueIndex;not null" json:"stripeCheckoutSessionId"`
	CreatedAt               *DateTime `gorm:"column:createdAt;autoCreateTime:false" json:"createdAt"`
	User                    *User
}

func (Donation) TableName() string {
	return "Donation"
}

func (m *Donation) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt == nil {
		m.CreatedAt = &DateTime{Time: now}
	}
	return nil
}

type Notification struct {
	ID               int      `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	CreatedAt        DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt        DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	IsRead           bool     `gorm:"column:isRead;default:false;not null" json:"isRead"`
	Type             string   `gorm:"column:type;not null" json:"type"`
	ToUserID         string   `gorm:"column:toUserId;index:Notification_toUserId_idx;not null" json:"toUserId"`
	FromUserID       string   `gorm:"column:fromUserId;index:Notification_fromUserId_idx;not null" json:"fromUserId"`
	CommentID        *int     `gorm:"column:commentId;index:Notification_commentId_idx" json:"commentId"`
	Content          *string  `gorm:"column:content" json:"content"`
	Blurb            *string  `gorm:"column:blurb" json:"blurb"`
	URL              string   `gorm:"column:url;not null" json:"url"`
	ChallengeID      *int     `gorm:"column:challengeId;index:Notification_challengeId_idx" json:"challengeId"`
	SharedSolutionID *int     `gorm:"column:sharedSolutionId;index:Notification_sharedSolutionId_idx" json:"sharedSolutionId"`
	ToUser           *User    `gorm:"foreignKey:ToUserID"`
	FromUser         *User    `gorm:"foreignKey:FromUserID"`
	Comment          *Comment
	Challenge        *Challenge
	SharedSolution   *SharedSolution
}

func (Notification) TableName() string {
	return "Notification"
}

func (m *Notification) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
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

type ShortURL struct {
	ID           int       `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	OriginalURL  string    `gorm:"column:originalUrl;type:text;not null" json:"originalUrl"`
	ShortURLSlug string    `gorm:"column:shortUrlSlug;uniqueIndex;index:ShortURL_shortUrlSlug_idx;not null" json:"shortUrlSlug"`
	CreatedAt    DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	ExpiresAt    *DateTime `gorm:"column:expiresAt" json:"expiresAt"`
	Clicks       int       `gorm:"column:clicks;default:0;not null" json:"clicks"`
	UserID       *string   `gorm:"column:userId;index:ShortURL_userId_idx" json:"userId"`
	User         *User
}

func (ShortURL) TableName() string {
	return "ShortURL"
}

func (m *ShortURL) BeforeCreate(tx *gorm.DB) error {
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
	return dateTime.UTC().Truncate(time.Millisecond).Format("2006-01-02 15:04:05.000"), nil
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
		return time.Date(value.Year(), value.Month(), value.Day(), value.Hour(), value.Minute(), value.Second(), value.Nanosecond(), time.UTC).Truncate(time.Millisecond), nil
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
