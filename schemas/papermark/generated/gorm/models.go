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

type DocumentAnnotation struct {
	ID          string         `gorm:"column:id;primaryKey" json:"id"`
	Title       string         `gorm:"column:title;not null" json:"title"`
	Content     datatypes.JSON `gorm:"column:content;not null" json:"content"`
	Pages       []int          `gorm:"column:pages;serializer:json;not null" json:"pages"`
	DocumentID  string         `gorm:"column:documentId;index:DocumentAnnotation_documentId_idx;not null" json:"documentId"`
	TeamID      string         `gorm:"column:teamId;index:DocumentAnnotation_teamId_idx;not null" json:"teamId"`
	CreatedByID string         `gorm:"column:createdById;index:DocumentAnnotation_createdById_idx;not null" json:"createdById"`
	IsVisible   *bool          `gorm:"column:isVisible;default:true;not null" json:"isVisible"`
	CreatedAt   DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt   DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Document    Document
	Team        Team
	CreatedBy   User              `gorm:"foreignKey:CreatedByID"`
	Images      []AnnotationImage `gorm:"foreignKey:AnnotationID;constraint:OnDelete:CASCADE"`
}

func (DocumentAnnotation) TableName() string {
	return "DocumentAnnotation"
}

func (m *DocumentAnnotation) BeforeCreate(tx *gorm.DB) error {
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

func (*DocumentAnnotation) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type AnnotationImage struct {
	ID           string             `gorm:"column:id;primaryKey" json:"id"`
	Filename     string             `gorm:"column:filename;not null" json:"filename"`
	URL          string             `gorm:"column:url;not null" json:"url"`
	Size         *int               `gorm:"column:size" json:"size"`
	MimeType     *string            `gorm:"column:mimeType" json:"mimeType"`
	AnnotationID string             `gorm:"column:annotationId;index:AnnotationImage_annotationId_idx;not null" json:"annotationId"`
	CreatedAt    DateTime           `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	Annotation   DocumentAnnotation `gorm:"foreignKey:AnnotationID"`
}

func (AnnotationImage) TableName() string {
	return "AnnotationImage"
}

func (m *AnnotationImage) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type Brand struct {
	ID                             string   `gorm:"column:id;primaryKey" json:"id"`
	Logo                           *string  `gorm:"column:logo" json:"logo"`
	HideLogo                       bool     `gorm:"column:hideLogo;default:false;not null" json:"hideLogo"`
	Banner                         *string  `gorm:"column:banner" json:"banner"`
	BrandColor                     *string  `gorm:"column:brandColor" json:"brandColor"`
	AccentColor                    *string  `gorm:"column:accentColor" json:"accentColor"`
	AccentButtonColor              *string  `gorm:"column:accentButtonColor" json:"accentButtonColor"`
	ApplyAccentColorToDataroomView bool     `gorm:"column:applyAccentColorToDataroomView;default:false;not null" json:"applyAccentColorToDataroomView"`
	WelcomeMessage                 *string  `gorm:"column:welcomeMessage" json:"welcomeMessage"`
	CtaLabel                       *string  `gorm:"column:ctaLabel" json:"ctaLabel"`
	CtaURL                         *string  `gorm:"column:ctaUrl" json:"ctaUrl"`
	PrivacyPolicyURL               *string  `gorm:"column:privacyPolicyUrl" json:"privacyPolicyUrl"`
	CardLayout                     *string  `gorm:"column:cardLayout;default:'LIST';not null" json:"cardLayout"`
	ShowFolderTree                 *bool    `gorm:"column:showFolderTree;default:true;not null" json:"showFolderTree"`
	ViewerLayoutPreset             *string  `gorm:"column:viewerLayoutPreset;default:'STANDARD';not null" json:"viewerLayoutPreset"`
	ViewerHeaderStyle              *string  `gorm:"column:viewerHeaderStyle;default:'DEFAULT';not null" json:"viewerHeaderStyle"`
	HideFolderIconsInMain          bool     `gorm:"column:hideFolderIconsInMain;default:false;not null" json:"hideFolderIconsInMain"`
	CustomLinkPreviewEnabled       bool     `gorm:"column:customLinkPreviewEnabled;default:false;not null" json:"customLinkPreviewEnabled"`
	LinkPreviewTitle               *string  `gorm:"column:linkPreviewTitle" json:"linkPreviewTitle"`
	LinkPreviewDescription         *string  `gorm:"column:linkPreviewDescription" json:"linkPreviewDescription"`
	LinkPreviewImage               *string  `gorm:"column:linkPreviewImage" json:"linkPreviewImage"`
	LinkPreviewFavicon             *string  `gorm:"column:linkPreviewFavicon" json:"linkPreviewFavicon"`
	DefaultLanguage                *string  `gorm:"column:defaultLanguage;default:'en';not null" json:"defaultLanguage"`
	Name                           *string  `gorm:"column:name;uniqueIndex:Brand_teamId_name_key;default:'Default';not null" json:"name"`
	TeamID                         string   `gorm:"column:teamId;uniqueIndex:Brand_teamId_name_key;index:Brand_teamId_idx;not null" json:"teamId"`
	CreatedAt                      DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt                      DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Team                           Team
	DefaultForTeam                 []Team     `gorm:"foreignKey:DefaultBrandID;constraint:OnDelete:SET NULL"`
	Links                          []Link     `gorm:"foreignKey:BrandID;constraint:OnDelete:SET NULL"`
	Datarooms                      []Dataroom `gorm:"foreignKey:BrandID;constraint:OnDelete:SET NULL"`
}

func (Brand) TableName() string {
	return "Brand"
}

func (m *Brand) BeforeCreate(tx *gorm.DB) error {
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

func (*Brand) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Conversation struct {
	ID                    string    `gorm:"column:id;primaryKey" json:"id"`
	Title                 *string   `gorm:"column:title" json:"title"`
	IsEnabled             *bool     `gorm:"column:isEnabled;default:true;not null" json:"isEnabled"`
	VisibilityMode        string    `gorm:"column:visibilityMode;default:'PRIVATE';not null" json:"visibilityMode"`
	DataroomID            string    `gorm:"column:dataroomId;index:Conversation_dataroomId_idx;not null" json:"dataroomId"`
	DataroomDocumentID    *string   `gorm:"column:dataroomDocumentId;index:Conversation_dataroomDocumentId_idx" json:"dataroomDocumentId"`
	DocumentVersionNumber *int      `gorm:"column:documentVersionNumber" json:"documentVersionNumber"`
	DocumentPageNumber    *int      `gorm:"column:documentPageNumber" json:"documentPageNumber"`
	LinkID                *string   `gorm:"column:linkId;index:Conversation_linkId_idx" json:"linkId"`
	ViewerGroupID         *string   `gorm:"column:viewerGroupId;index:Conversation_viewerGroupId_idx" json:"viewerGroupId"`
	QuestionID            *string   `gorm:"column:questionId;uniqueIndex:Conversation_questionId_recipientViewerId_key;index:Conversation_questionId_idx" json:"questionId"`
	Origin                *string   `gorm:"column:origin;default:'visitor';not null" json:"origin"`
	Category              *string   `gorm:"column:category" json:"category"`
	Priority              *string   `gorm:"column:priority" json:"priority"`
	RecipientViewerID     *string   `gorm:"column:recipientViewerId;uniqueIndex:Conversation_questionId_recipientViewerId_key;index:Conversation_recipientViewerId_idx" json:"recipientViewerId"`
	InitialViewID         *string   `gorm:"column:initialViewId;index:Conversation_initialViewId_idx" json:"initialViewId"`
	TeamID                string    `gorm:"column:teamId;index:Conversation_teamId_idx;not null" json:"teamId"`
	LastMessageAt         *DateTime `gorm:"column:lastMessageAt" json:"lastMessageAt"`
	CreatedAt             DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt             DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Dataroom              Dataroom
	DataroomDocument      DataroomDocument
	Link                  Link
	ViewerGroup           ViewerGroup
	Question              DataroomQuestion `gorm:"foreignKey:QuestionID"`
	RecipientViewer       Viewer           `gorm:"foreignKey:RecipientViewerID"`
	InitialView           View             `gorm:"foreignKey:InitialViewID"`
	Team                  Team
	Views                 []ConversationView        `gorm:"foreignKey:ConversationID;constraint:OnDelete:CASCADE"`
	Participants          []ConversationParticipant `gorm:"foreignKey:ConversationID;constraint:OnDelete:CASCADE"`
	Messages              []Message                 `gorm:"foreignKey:ConversationID;constraint:OnDelete:CASCADE"`
	FaqItems              []DataroomFaqItem         `gorm:"foreignKey:SourceConversationID;constraint:OnDelete:SET NULL"`
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

type DataroomQuestion struct {
	ID                 string   `gorm:"column:id;primaryKey" json:"id"`
	Content            string   `gorm:"column:content;type:text;not null" json:"content"`
	Category           *string  `gorm:"column:category" json:"category"`
	Priority           *string  `gorm:"column:priority" json:"priority"`
	Status             *string  `gorm:"column:status;index:DataroomQuestion_status_idx;default:'unanswered';not null" json:"status"`
	OrderIndex         *int     `gorm:"column:orderIndex" json:"orderIndex"`
	ImportBatchID      *string  `gorm:"column:importBatchId;index:DataroomQuestion_importBatchId_idx" json:"importBatchId"`
	DataroomID         string   `gorm:"column:dataroomId;index:DataroomQuestion_dataroomId_idx;not null" json:"dataroomId"`
	TeamID             string   `gorm:"column:teamId;index:DataroomQuestion_teamId_idx;not null" json:"teamId"`
	CreatedByUserID    *string  `gorm:"column:createdByUserId;index:DataroomQuestion_createdByUserId_idx" json:"createdByUserId"`
	DataroomDocumentID *string  `gorm:"column:dataroomDocumentId;index:DataroomQuestion_dataroomDocumentId_idx" json:"dataroomDocumentId"`
	DocumentPageNumber *int     `gorm:"column:documentPageNumber" json:"documentPageNumber"`
	CreatedAt          DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt          DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Dataroom           Dataroom
	Team               Team
	CreatedByUser      User `gorm:"foreignKey:CreatedByUserID"`
	DataroomDocument   DataroomDocument
	Assignments        []DataroomQuestionAssignment `gorm:"foreignKey:QuestionID;constraint:OnDelete:CASCADE"`
	Conversations      []Conversation               `gorm:"foreignKey:QuestionID;constraint:OnDelete:CASCADE"`
}

func (DataroomQuestion) TableName() string {
	return "DataroomQuestion"
}

func (m *DataroomQuestion) BeforeCreate(tx *gorm.DB) error {
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

func (*DataroomQuestion) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type DataroomQuestionAssignment struct {
	ID         string           `gorm:"column:id;primaryKey" json:"id"`
	QuestionID string           `gorm:"column:questionId;uniqueIndex:DataroomQuestionAssignment_questionId_linkId_key;uniqueIndex:DataroomQuestionAssignment_questionId_groupId_key;uniqueIndex:DataroomQuestionAssignment_questionId_viewerId_key;uniqueIndex:DataroomQuestionAssignment_questionId_email_key;index:DataroomQuestionAssignment_questionId_idx;not null" json:"questionId"`
	LinkID     *string          `gorm:"column:linkId;uniqueIndex:DataroomQuestionAssignment_questionId_linkId_key;index:DataroomQuestionAssignment_linkId_idx" json:"linkId"`
	GroupID    *string          `gorm:"column:groupId;uniqueIndex:DataroomQuestionAssignment_questionId_groupId_key;index:DataroomQuestionAssignment_groupId_idx" json:"groupId"`
	ViewerID   *string          `gorm:"column:viewerId;uniqueIndex:DataroomQuestionAssignment_questionId_viewerId_key;index:DataroomQuestionAssignment_viewerId_idx" json:"viewerId"`
	Email      *string          `gorm:"column:email;uniqueIndex:DataroomQuestionAssignment_questionId_email_key" json:"email"`
	CreatedAt  DateTime         `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	Question   DataroomQuestion `gorm:"foreignKey:QuestionID"`
	Link       Link
	Group      ViewerGroup `gorm:"foreignKey:GroupID"`
	Viewer     Viewer
}

func (DataroomQuestionAssignment) TableName() string {
	return "DataroomQuestionAssignment"
}

func (m *DataroomQuestionAssignment) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type ConversationParticipant struct {
	ID                   string   `gorm:"column:id;primaryKey" json:"id"`
	ConversationID       string   `gorm:"column:conversationId;uniqueIndex:ConversationParticipant_conversationId_viewerId_key;uniqueIndex:ConversationParticipant_conversationId_userId_key;index:ConversationParticipant_conversationId_idx;not null" json:"conversationId"`
	Role                 string   `gorm:"column:role;default:'PARTICIPANT';not null" json:"role"`
	ViewerID             *string  `gorm:"column:viewerId;uniqueIndex:ConversationParticipant_conversationId_viewerId_key;index:ConversationParticipant_viewerId_idx" json:"viewerId"`
	UserID               *string  `gorm:"column:userId;uniqueIndex:ConversationParticipant_conversationId_userId_key;index:ConversationParticipant_userId_idx" json:"userId"`
	ReceiveNotifications bool     `gorm:"column:receiveNotifications;default:false;not null" json:"receiveNotifications"`
	CreatedAt            DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	Conversation         Conversation
	Viewer               Viewer
	User                 User
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

type Message struct {
	ID             string   `gorm:"column:id;primaryKey" json:"id"`
	Content        string   `gorm:"column:content;not null" json:"content"`
	ConversationID string   `gorm:"column:conversationId;index:Message_conversationId_idx;not null" json:"conversationId"`
	UserID         *string  `gorm:"column:userId;index:Message_userId_idx" json:"userId"`
	ViewerID       *string  `gorm:"column:viewerId;index:Message_viewerId_idx" json:"viewerId"`
	ViewID         *string  `gorm:"column:viewId;index:Message_viewId_idx" json:"viewId"`
	IsRead         bool     `gorm:"column:isRead;default:false;not null" json:"isRead"`
	IsInternalNote bool     `gorm:"column:isInternalNote;default:false;not null" json:"isInternalNote"`
	CreatedAt      DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt      DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Conversation   Conversation
	User           User
	Viewer         Viewer
	View           View
	FaqAsQuestion  []DataroomFaqItem `gorm:"foreignKey:QuestionMessageID;constraint:OnDelete:SET NULL"`
	FaqAsAnswer    []DataroomFaqItem `gorm:"foreignKey:AnswerMessageID;constraint:OnDelete:SET NULL"`
}

func (Message) TableName() string {
	return "Message"
}

func (m *Message) BeforeCreate(tx *gorm.DB) error {
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

func (*Message) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type ConversationView struct {
	ID             string   `gorm:"column:id;primaryKey" json:"id"`
	ConversationID string   `gorm:"column:conversationId;uniqueIndex:ConversationView_conversationId_viewId_key;index:ConversationView_conversationId_idx;not null" json:"conversationId"`
	ViewID         string   `gorm:"column:viewId;uniqueIndex:ConversationView_conversationId_viewId_key;index:ConversationView_viewId_idx;not null" json:"viewId"`
	CreatedAt      DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	Conversation   Conversation
	View           View
}

func (ConversationView) TableName() string {
	return "ConversationView"
}

func (m *ConversationView) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type DataroomFaqItem struct {
	ID                    string   `gorm:"column:id;primaryKey" json:"id"`
	Title                 *string  `gorm:"column:title" json:"title"`
	EditedQuestion        string   `gorm:"column:editedQuestion;not null" json:"editedQuestion"`
	OriginalQuestion      *string  `gorm:"column:originalQuestion" json:"originalQuestion"`
	Answer                string   `gorm:"column:answer;not null" json:"answer"`
	Description           *string  `gorm:"column:description" json:"description"`
	DataroomID            string   `gorm:"column:dataroomId;index:DataroomFaqItem_dataroomId_idx;not null" json:"dataroomId"`
	LinkID                *string  `gorm:"column:linkId;index:DataroomFaqItem_linkId_idx" json:"linkId"`
	DataroomDocumentID    *string  `gorm:"column:dataroomDocumentId;index:DataroomFaqItem_dataroomDocumentId_idx" json:"dataroomDocumentId"`
	SourceConversationID  *string  `gorm:"column:sourceConversationId;index:DataroomFaqItem_sourceConversationId_idx" json:"sourceConversationId"`
	QuestionMessageID     *string  `gorm:"column:questionMessageId" json:"questionMessageId"`
	AnswerMessageID       *string  `gorm:"column:answerMessageId" json:"answerMessageId"`
	TeamID                string   `gorm:"column:teamId;index:DataroomFaqItem_teamId_idx;not null" json:"teamId"`
	PublishedByUserID     string   `gorm:"column:publishedByUserId;index:DataroomFaqItem_publishedByUserId_idx;not null" json:"publishedByUserId"`
	VisibilityMode        string   `gorm:"column:visibilityMode;index:DataroomFaqItem_visibilityMode_idx;default:'PUBLIC_DATAROOM';not null" json:"visibilityMode"`
	Status                string   `gorm:"column:status;index:DataroomFaqItem_status_idx;default:'PUBLISHED';not null" json:"status"`
	IsAnonymized          *bool    `gorm:"column:isAnonymized;default:true;not null" json:"isAnonymized"`
	ViewCount             int      `gorm:"column:viewCount;default:0;not null" json:"viewCount"`
	Tags                  []string `gorm:"column:tags;serializer:json;not null" json:"tags"`
	DocumentPageNumber    *int     `gorm:"column:documentPageNumber" json:"documentPageNumber"`
	DocumentVersionNumber *int     `gorm:"column:documentVersionNumber" json:"documentVersionNumber"`
	CreatedAt             DateTime `gorm:"column:createdAt;index:DataroomFaqItem_createdAt_idx;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt             DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Dataroom              Dataroom
	Link                  Link
	DataroomDocument      DataroomDocument
	SourceConversation    Conversation `gorm:"foreignKey:SourceConversationID"`
	QuestionMessage       Message      `gorm:"foreignKey:QuestionMessageID"`
	AnswerMessage         Message      `gorm:"foreignKey:AnswerMessageID"`
	Team                  Team
	PublishedByUser       User `gorm:"foreignKey:PublishedByUserID"`
}

func (DataroomFaqItem) TableName() string {
	return "DataroomFaqItem"
}

func (m *DataroomFaqItem) BeforeCreate(tx *gorm.DB) error {
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

func (*DataroomFaqItem) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Dataroom struct {
	ID                                     string         `gorm:"column:id;primaryKey" json:"id"`
	PID                                    string         `gorm:"column:pId;uniqueIndex;not null" json:"pId"`
	Name                                   string         `gorm:"column:name;not null" json:"name"`
	InternalName                           *string        `gorm:"column:internalName" json:"internalName"`
	Description                            *string        `gorm:"column:description" json:"description"`
	TeamID                                 string         `gorm:"column:teamId;index:Dataroom_teamId_idx;not null" json:"teamId"`
	BrandID                                *string        `gorm:"column:brandId;index:Dataroom_brandId_idx" json:"brandId"`
	ConversationsEnabled                   bool           `gorm:"column:conversationsEnabled;default:false;not null" json:"conversationsEnabled"`
	AgentsEnabled                          bool           `gorm:"column:agentsEnabled;default:false;not null" json:"agentsEnabled"`
	VectorStoreID                          *string        `gorm:"column:vectorStoreId" json:"vectorStoreId"`
	RequestListEnabled                     bool           `gorm:"column:requestListEnabled;default:false;not null" json:"requestListEnabled"`
	EnableChangeNotifications              bool           `gorm:"column:enableChangeNotifications;default:false;not null" json:"enableChangeNotifications"`
	EnableVisitorUploadChangeNotifications bool           `gorm:"column:enableVisitorUploadChangeNotifications;default:false;not null" json:"enableVisitorUploadChangeNotifications"`
	DefaultPermissionStrategy              string         `gorm:"column:defaultPermissionStrategy;default:'INHERIT_FROM_PARENT';not null" json:"defaultPermissionStrategy"`
	DefaultShowBanner                      *bool          `gorm:"column:defaultShowBanner;default:true;not null" json:"defaultShowBanner"`
	DefaultGroupPermissionStrategy         string         `gorm:"column:defaultGroupPermissionStrategy;default:'INHERIT_FROM_PARENT';not null" json:"defaultGroupPermissionStrategy"`
	DefaultRootItemAccess                  string         `gorm:"column:defaultRootItemAccess;default:'VIEW_ONLY';not null" json:"defaultRootItemAccess"`
	DefaultGroupRootItemAccess             string         `gorm:"column:defaultGroupRootItemAccess;default:'VIEW_ONLY';not null" json:"defaultGroupRootItemAccess"`
	AllowBulkDownload                      *bool          `gorm:"column:allowBulkDownload;default:true;not null" json:"allowBulkDownload"`
	ShowLastUpdated                        *bool          `gorm:"column:showLastUpdated;default:true;not null" json:"showLastUpdated"`
	IntroductionEnabled                    bool           `gorm:"column:introductionEnabled;default:false;not null" json:"introductionEnabled"`
	IntroductionContent                    datatypes.JSON `gorm:"column:introductionContent" json:"introductionContent"`
	IsFrozen                               bool           `gorm:"column:isFrozen;default:false;not null" json:"isFrozen"`
	FrozenAt                               *DateTime      `gorm:"column:frozenAt" json:"frozenAt"`
	FrozenBy                               *string        `gorm:"column:frozenBy" json:"frozenBy"`
	FreezeArchiveURL                       *string        `gorm:"column:freezeArchiveUrl" json:"freezeArchiveUrl"`
	FreezeArchiveHash                      *string        `gorm:"column:freezeArchiveHash" json:"freezeArchiveHash"`
	CreatedAt                              DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt                              DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Team                                   Team
	BaseBrand                              Brand              `gorm:"foreignKey:BrandID"`
	FrozenByUser                           User               `gorm:"foreignKey:FrozenBy"`
	Documents                              []DataroomDocument `gorm:"foreignKey:DataroomID;constraint:OnDelete:CASCADE"`
	Folders                                []DataroomFolder   `gorm:"foreignKey:DataroomID;constraint:OnDelete:CASCADE"`
	Links                                  []Link             `gorm:"foreignKey:DataroomID;constraint:OnDelete:CASCADE"`
	Views                                  []View             `gorm:"foreignKey:DataroomID;constraint:OnDelete:CASCADE"`
	Viewers                                []Viewer           `gorm:"foreignKey:DataroomID;constraint:OnDelete:SET NULL"`
	ViewerGroups                           []ViewerGroup      `gorm:"foreignKey:DataroomID;constraint:OnDelete:CASCADE"`
	UserDatarooms                          []UserDataroom     `gorm:"foreignKey:DataroomID;constraint:OnDelete:CASCADE"`
	PermissionGroups                       []PermissionGroup  `gorm:"foreignKey:DataroomID;constraint:OnDelete:CASCADE"`
	Conversations                          []Conversation     `gorm:"foreignKey:DataroomID;constraint:OnDelete:CASCADE"`
	Questions                              []DataroomQuestion `gorm:"foreignKey:DataroomID;constraint:OnDelete:CASCADE"`
	FaqItems                               []DataroomFaqItem  `gorm:"foreignKey:DataroomID;constraint:OnDelete:CASCADE"`
	Chats                                  []Chat             `gorm:"foreignKey:DataroomID;constraint:OnDelete:CASCADE"`
	UploadedDocuments                      []DocumentUpload   `gorm:"foreignKey:DataroomID;constraint:OnDelete:SET NULL"`
	Tags                                   []TagItem          `gorm:"foreignKey:DataroomID;constraint:OnDelete:CASCADE"`
	TaskLists                              []TaskList         `gorm:"foreignKey:DataroomID;constraint:OnDelete:CASCADE"`
	Tasks                                  []Task             `gorm:"foreignKey:DataroomID;constraint:OnDelete:CASCADE"`
	Brand                                  *DataroomBrand     `gorm:"foreignKey:DataroomID;constraint:OnDelete:CASCADE"`
}

func (Dataroom) TableName() string {
	return "Dataroom"
}

func (m *Dataroom) BeforeCreate(tx *gorm.DB) error {
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

func (*Dataroom) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type TaskList struct {
	ID         string   `gorm:"column:id;primaryKey" json:"id"`
	Name       string   `gorm:"column:name;not null" json:"name"`
	OrderIndex *int     `gorm:"column:orderIndex;index:TaskList_dataroomId_orderIndex_idx" json:"orderIndex"`
	DataroomID string   `gorm:"column:dataroomId;index:TaskList_dataroomId_idx;index:TaskList_dataroomId_orderIndex_idx;not null" json:"dataroomId"`
	TeamID     string   `gorm:"column:teamId;index:TaskList_teamId_idx;not null" json:"teamId"`
	CreatedAt  DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt  DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Dataroom   Dataroom
	Team       Team
	Tasks      []Task `gorm:"foreignKey:TaskListID;constraint:OnDelete:CASCADE"`
}

func (TaskList) TableName() string {
	return "TaskList"
}

func (m *TaskList) BeforeCreate(tx *gorm.DB) error {
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

func (*TaskList) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Task struct {
	ID                     string    `gorm:"column:id;primaryKey" json:"id"`
	Title                  string    `gorm:"column:title;not null" json:"title"`
	Description            *string   `gorm:"column:description" json:"description"`
	Type                   *string   `gorm:"column:type;default:'TODO';not null" json:"type"`
	Status                 *string   `gorm:"column:status;default:'OPEN';not null" json:"status"`
	Category               *string   `gorm:"column:category" json:"category"`
	DueDate                *DateTime `gorm:"column:dueDate" json:"dueDate"`
	OrderIndex             *int      `gorm:"column:orderIndex;index:Task_taskListId_orderIndex_idx" json:"orderIndex"`
	TaskListID             string    `gorm:"column:taskListId;index:Task_taskListId_idx;index:Task_taskListId_orderIndex_idx;not null" json:"taskListId"`
	DataroomID             string    `gorm:"column:dataroomId;index:Task_dataroomId_idx;not null" json:"dataroomId"`
	TeamID                 string    `gorm:"column:teamId;index:Task_teamId_idx;not null" json:"teamId"`
	UploadFolderID         *string   `gorm:"column:uploadFolderId;index:Task_uploadFolderId_idx" json:"uploadFolderId"`
	CreatedByUserID        *string   `gorm:"column:createdByUserId" json:"createdByUserId"`
	LastActivityNotifiedAt *DateTime `gorm:"column:lastActivityNotifiedAt" json:"lastActivityNotifiedAt"`
	CreatedAt              DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt              DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	TaskList               TaskList
	Dataroom               Dataroom
	Team                   Team
	UploadFolder           DataroomFolder   `gorm:"foreignKey:UploadFolderID"`
	CreatedByUser          User             `gorm:"foreignKey:CreatedByUserID"`
	Assignments            []TaskAssignment `gorm:"foreignKey:TaskID;constraint:OnDelete:CASCADE"`
	Activities             []TaskActivity   `gorm:"foreignKey:TaskID;constraint:OnDelete:CASCADE"`
	Uploads                []DocumentUpload `gorm:"foreignKey:TaskID;constraint:OnDelete:SET NULL"`
}

func (Task) TableName() string {
	return "Task"
}

func (m *Task) BeforeCreate(tx *gorm.DB) error {
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

func (*Task) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type TaskAssignment struct {
	ID             string    `gorm:"column:id;primaryKey" json:"id"`
	TaskID         string    `gorm:"column:taskId;uniqueIndex:TaskAssignment_taskId_viewerId_key;uniqueIndex:TaskAssignment_taskId_groupId_key;uniqueIndex:TaskAssignment_taskId_linkId_key;uniqueIndex:TaskAssignment_taskId_email_key;index:TaskAssignment_taskId_idx;not null" json:"taskId"`
	ViewerID       *string   `gorm:"column:viewerId;uniqueIndex:TaskAssignment_taskId_viewerId_key;index:TaskAssignment_viewerId_idx" json:"viewerId"`
	GroupID        *string   `gorm:"column:groupId;uniqueIndex:TaskAssignment_taskId_groupId_key;index:TaskAssignment_groupId_idx" json:"groupId"`
	LinkID         *string   `gorm:"column:linkId;uniqueIndex:TaskAssignment_taskId_linkId_key;index:TaskAssignment_linkId_idx" json:"linkId"`
	Email          *string   `gorm:"column:email;uniqueIndex:TaskAssignment_taskId_email_key" json:"email"`
	NotifiedAt     *DateTime `gorm:"column:notifiedAt" json:"notifiedAt"`
	LastReminderAt *DateTime `gorm:"column:lastReminderAt" json:"lastReminderAt"`
	CreatedAt      DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt      DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Task           Task
	Viewer         Viewer
	Group          ViewerGroup `gorm:"foreignKey:GroupID"`
	Link           Link
}

func (TaskAssignment) TableName() string {
	return "TaskAssignment"
}

func (m *TaskAssignment) BeforeCreate(tx *gorm.DB) error {
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

func (*TaskAssignment) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type TaskActivity struct {
	ID         string   `gorm:"column:id;primaryKey" json:"id"`
	TaskID     string   `gorm:"column:taskId;index:TaskActivity_taskId_idx;index:TaskActivity_taskId_createdAt_idx;not null" json:"taskId"`
	Type       string   `gorm:"column:type;not null" json:"type"`
	Comment    *string  `gorm:"column:comment" json:"comment"`
	FromStatus *string  `gorm:"column:fromStatus" json:"fromStatus"`
	ToStatus   *string  `gorm:"column:toStatus" json:"toStatus"`
	UserID     *string  `gorm:"column:userId;index:TaskActivity_userId_idx" json:"userId"`
	ViewerID   *string  `gorm:"column:viewerId;index:TaskActivity_viewerId_idx" json:"viewerId"`
	ViewID     *string  `gorm:"column:viewId;index:TaskActivity_viewId_idx" json:"viewId"`
	CreatedAt  DateTime `gorm:"column:createdAt;index:TaskActivity_taskId_createdAt_idx;autoCreateTime:false;not null" json:"createdAt"`
	Task       Task
	User       User
	Viewer     Viewer
	View       View
}

func (TaskActivity) TableName() string {
	return "TaskActivity"
}

func (m *TaskActivity) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type DataroomDocument struct {
	ID                string   `gorm:"column:id;primaryKey" json:"id"`
	DataroomID        string   `gorm:"column:dataroomId;uniqueIndex:DataroomDocument_dataroomId_documentId_key;index:DataroomDocument_dataroomId_folderId_orderIndex_idx;not null" json:"dataroomId"`
	DocumentID        string   `gorm:"column:documentId;uniqueIndex:DataroomDocument_dataroomId_documentId_key;index:DataroomDocument_documentId_idx;not null" json:"documentId"`
	FolderID          *string  `gorm:"column:folderId;index:DataroomDocument_folderId_idx;index:DataroomDocument_dataroomId_folderId_orderIndex_idx" json:"folderId"`
	OrderIndex        *int     `gorm:"column:orderIndex;index:DataroomDocument_dataroomId_folderId_orderIndex_idx" json:"orderIndex"`
	HierarchicalIndex *string  `gorm:"column:hierarchicalIndex" json:"hierarchicalIndex"`
	VectorStoreFileID *string  `gorm:"column:vectorStoreFileId" json:"vectorStoreFileId"`
	CreatedAt         DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt         DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Dataroom          Dataroom
	Document          Document
	Folder            DataroomFolder     `gorm:"foreignKey:FolderID"`
	Conversations     []Conversation     `gorm:"foreignKey:DataroomDocumentID;constraint:OnDelete:SET NULL"`
	Questions         []DataroomQuestion `gorm:"foreignKey:DataroomDocumentID;constraint:OnDelete:SET NULL"`
	DataroomFAQs      []DataroomFaqItem  `gorm:"foreignKey:DataroomDocumentID;constraint:OnDelete:SET NULL"`
	UploadedDocuments []DocumentUpload   `gorm:"foreignKey:DataroomDocumentID;constraint:OnDelete:SET NULL"`
}

func (DataroomDocument) TableName() string {
	return "DataroomDocument"
}

func (m *DataroomDocument) BeforeCreate(tx *gorm.DB) error {
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

func (*DataroomDocument) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type DataroomFolder struct {
	ID                string          `gorm:"column:id;primaryKey" json:"id"`
	Name              string          `gorm:"column:name;not null" json:"name"`
	Path              string          `gorm:"column:path;uniqueIndex:DataroomFolder_dataroomId_path_key;not null" json:"path"`
	ParentID          *string         `gorm:"column:parentId;index:DataroomFolder_parentId_idx;index:DataroomFolder_dataroomId_parentId_orderIndex_idx" json:"parentId"`
	Icon              *string         `gorm:"column:icon" json:"icon"`
	Color             *string         `gorm:"column:color" json:"color"`
	DataroomID        string          `gorm:"column:dataroomId;uniqueIndex:DataroomFolder_dataroomId_path_key;index:DataroomFolder_dataroomId_parentId_orderIndex_idx;not null" json:"dataroomId"`
	OrderIndex        *int            `gorm:"column:orderIndex;index:DataroomFolder_dataroomId_parentId_orderIndex_idx" json:"orderIndex"`
	HierarchicalIndex *string         `gorm:"column:hierarchicalIndex" json:"hierarchicalIndex"`
	CreatedAt         DateTime        `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt         DateTime        `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	ParentFolder      *DataroomFolder `gorm:"foreignKey:ParentID"`
	Dataroom          Dataroom
	Documents         []DataroomDocument `gorm:"foreignKey:FolderID;constraint:OnDelete:SET NULL"`
	ChildFolders      []DataroomFolder   `gorm:"foreignKey:ParentID;constraint:OnDelete:SET NULL"`
	UploadTasks       []Task             `gorm:"foreignKey:UploadFolderID;constraint:OnDelete:SET NULL"`
}

func (DataroomFolder) TableName() string {
	return "DataroomFolder"
}

func (m *DataroomFolder) BeforeCreate(tx *gorm.DB) error {
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

func (*DataroomFolder) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type DataroomBrand struct {
	ID                             string   `gorm:"column:id;primaryKey" json:"id"`
	Logo                           *string  `gorm:"column:logo" json:"logo"`
	HideLogo                       *bool    `gorm:"column:hideLogo" json:"hideLogo"`
	Banner                         *string  `gorm:"column:banner" json:"banner"`
	BrandColor                     *string  `gorm:"column:brandColor" json:"brandColor"`
	AccentColor                    *string  `gorm:"column:accentColor" json:"accentColor"`
	AccentButtonColor              *string  `gorm:"column:accentButtonColor" json:"accentButtonColor"`
	ApplyAccentColorToDataroomView bool     `gorm:"column:applyAccentColorToDataroomView;default:false;not null" json:"applyAccentColorToDataroomView"`
	WelcomeMessage                 *string  `gorm:"column:welcomeMessage" json:"welcomeMessage"`
	CardLayout                     *string  `gorm:"column:cardLayout;default:'LIST';not null" json:"cardLayout"`
	ShowFolderTree                 *bool    `gorm:"column:showFolderTree;default:true;not null" json:"showFolderTree"`
	ViewerLayoutPreset             *string  `gorm:"column:viewerLayoutPreset;default:'STANDARD';not null" json:"viewerLayoutPreset"`
	ViewerHeaderStyle              *string  `gorm:"column:viewerHeaderStyle;default:'DEFAULT';not null" json:"viewerHeaderStyle"`
	HideFolderIconsInMain          bool     `gorm:"column:hideFolderIconsInMain;default:false;not null" json:"hideFolderIconsInMain"`
	CtaLabel                       *string  `gorm:"column:ctaLabel" json:"ctaLabel"`
	CtaURL                         *string  `gorm:"column:ctaUrl" json:"ctaUrl"`
	CustomLinkPreviewEnabled       bool     `gorm:"column:customLinkPreviewEnabled;default:false;not null" json:"customLinkPreviewEnabled"`
	LinkPreviewTitle               *string  `gorm:"column:linkPreviewTitle" json:"linkPreviewTitle"`
	LinkPreviewDescription         *string  `gorm:"column:linkPreviewDescription" json:"linkPreviewDescription"`
	LinkPreviewImage               *string  `gorm:"column:linkPreviewImage" json:"linkPreviewImage"`
	LinkPreviewFavicon             *string  `gorm:"column:linkPreviewFavicon" json:"linkPreviewFavicon"`
	DefaultLanguage                *string  `gorm:"column:defaultLanguage;default:'en';not null" json:"defaultLanguage"`
	DataroomID                     string   `gorm:"column:dataroomId;uniqueIndex;not null" json:"dataroomId"`
	CreatedAt                      DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt                      DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Dataroom                       Dataroom
}

func (DataroomBrand) TableName() string {
	return "DataroomBrand"
}

func (m *DataroomBrand) BeforeCreate(tx *gorm.DB) error {
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

func (*DataroomBrand) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type ViewerGroup struct {
	ID                  string   `gorm:"column:id;primaryKey" json:"id"`
	Name                string   `gorm:"column:name;not null" json:"name"`
	Domains             []string `gorm:"column:domains;serializer:json;not null" json:"domains"`
	AllowAll            bool     `gorm:"column:allowAll;default:false;not null" json:"allowAll"`
	DataroomID          string   `gorm:"column:dataroomId;index:ViewerGroup_dataroomId_idx;index:ViewerGroup_dataroomId_createdAt_idx;not null" json:"dataroomId"`
	TeamID              string   `gorm:"column:teamId;index:ViewerGroup_teamId_idx;not null" json:"teamId"`
	CreatedAt           DateTime `gorm:"column:createdAt;index:ViewerGroup_dataroomId_createdAt_idx;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt           DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Dataroom            Dataroom
	Team                Team
	Members             []ViewerGroupMembership      `gorm:"foreignKey:GroupID;constraint:OnDelete:CASCADE"`
	Links               []Link                       `gorm:"foreignKey:GroupID;constraint:OnDelete:SET NULL"`
	AccessControls      []ViewerGroupAccessControls  `gorm:"foreignKey:GroupID;constraint:OnDelete:CASCADE"`
	Views               []View                       `gorm:"foreignKey:GroupID;constraint:OnDelete:SET NULL"`
	Conversations       []Conversation               `gorm:"foreignKey:ViewerGroupID;constraint:OnDelete:SET NULL"`
	Invitations         []ViewerInvitation           `gorm:"foreignKey:GroupID;constraint:OnDelete:SET NULL"`
	QuestionAssignments []DataroomQuestionAssignment `gorm:"foreignKey:GroupID;constraint:OnDelete:CASCADE"`
	TaskAssignments     []TaskAssignment             `gorm:"foreignKey:GroupID;constraint:OnDelete:CASCADE"`
}

func (ViewerGroup) TableName() string {
	return "ViewerGroup"
}

func (m *ViewerGroup) BeforeCreate(tx *gorm.DB) error {
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

func (*ViewerGroup) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type ViewerGroupMembership struct {
	ID        string   `gorm:"column:id;primaryKey" json:"id"`
	ViewerID  string   `gorm:"column:viewerId;uniqueIndex:ViewerGroupMembership_viewerId_groupId_key;index:ViewerGroupMembership_viewerId_idx;not null" json:"viewerId"`
	GroupID   string   `gorm:"column:groupId;uniqueIndex:ViewerGroupMembership_viewerId_groupId_key;index:ViewerGroupMembership_groupId_idx;not null" json:"groupId"`
	CreatedAt DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Viewer    Viewer
	Group     ViewerGroup `gorm:"foreignKey:GroupID"`
}

func (ViewerGroupMembership) TableName() string {
	return "ViewerGroupMembership"
}

func (m *ViewerGroupMembership) BeforeCreate(tx *gorm.DB) error {
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

func (*ViewerGroupMembership) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type ViewerGroupAccessControls struct {
	ID          string      `gorm:"column:id;primaryKey" json:"id"`
	GroupID     string      `gorm:"column:groupId;uniqueIndex:ViewerGroupAccessControls_groupId_itemId_key;index:ViewerGroupAccessControls_groupId_idx;not null" json:"groupId"`
	ItemID      string      `gorm:"column:itemId;uniqueIndex:ViewerGroupAccessControls_groupId_itemId_key;not null" json:"itemId"`
	ItemType    string      `gorm:"column:itemType;not null" json:"itemType"`
	CanView     *bool       `gorm:"column:canView;default:true;not null" json:"canView"`
	CanDownload bool        `gorm:"column:canDownload;default:false;not null" json:"canDownload"`
	CreatedAt   DateTime    `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt   DateTime    `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Group       ViewerGroup `gorm:"foreignKey:GroupID"`
}

func (ViewerGroupAccessControls) TableName() string {
	return "ViewerGroupAccessControls"
}

func (m *ViewerGroupAccessControls) BeforeCreate(tx *gorm.DB) error {
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

func (*ViewerGroupAccessControls) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type PermissionGroup struct {
	ID             string   `gorm:"column:id;primaryKey" json:"id"`
	Name           string   `gorm:"column:name;not null" json:"name"`
	Description    *string  `gorm:"column:description" json:"description"`
	DataroomID     string   `gorm:"column:dataroomId;index:PermissionGroup_dataroomId_idx;not null" json:"dataroomId"`
	TeamID         string   `gorm:"column:teamId;index:PermissionGroup_teamId_idx;not null" json:"teamId"`
	CreatedAt      DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt      DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Dataroom       Dataroom
	Team           Team
	Links          []Link                          `gorm:"foreignKey:PermissionGroupID;constraint:OnDelete:SET NULL"`
	AccessControls []PermissionGroupAccessControls `gorm:"foreignKey:GroupID;constraint:OnDelete:CASCADE"`
}

func (PermissionGroup) TableName() string {
	return "PermissionGroup"
}

func (m *PermissionGroup) BeforeCreate(tx *gorm.DB) error {
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

func (*PermissionGroup) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type PermissionGroupAccessControls struct {
	ID                  string          `gorm:"column:id;primaryKey" json:"id"`
	GroupID             string          `gorm:"column:groupId;uniqueIndex:PermissionGroupAccessControls_groupId_itemId_key;index:PermissionGroupAccessControls_groupId_idx;not null" json:"groupId"`
	ItemID              string          `gorm:"column:itemId;uniqueIndex:PermissionGroupAccessControls_groupId_itemId_key;not null" json:"itemId"`
	ItemType            string          `gorm:"column:itemType;not null" json:"itemType"`
	CanView             *bool           `gorm:"column:canView;default:true;not null" json:"canView"`
	CanDownload         bool            `gorm:"column:canDownload;default:false;not null" json:"canDownload"`
	CanDownloadOriginal bool            `gorm:"column:canDownloadOriginal;default:false;not null" json:"canDownloadOriginal"`
	CreatedAt           DateTime        `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt           DateTime        `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Group               PermissionGroup `gorm:"foreignKey:GroupID"`
}

func (PermissionGroupAccessControls) TableName() string {
	return "PermissionGroupAccessControls"
}

func (m *PermissionGroupAccessControls) BeforeCreate(tx *gorm.DB) error {
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

func (*PermissionGroupAccessControls) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Document struct {
	ID                   string   `gorm:"column:id;primaryKey" json:"id"`
	Name                 string   `gorm:"column:name;index:Document_teamId_name_idx;not null" json:"name"`
	Description          *string  `gorm:"column:description" json:"description"`
	File                 string   `gorm:"column:file;not null" json:"file"`
	OriginalFile         *string  `gorm:"column:originalFile" json:"originalFile"`
	Type                 *string  `gorm:"column:type" json:"type"`
	ContentType          *string  `gorm:"column:contentType" json:"contentType"`
	StorageType          string   `gorm:"column:storageType;default:'VERCEL_BLOB';not null" json:"storageType"`
	NumPages             *int     `gorm:"column:numPages" json:"numPages"`
	TeamID               string   `gorm:"column:teamId;index:Document_teamId_idx;index:Document_teamId_folderId_idx;index:Document_teamId_name_idx;index:Document_teamId_hiddenInAllDocuments_idx;not null" json:"teamId"`
	OwnerID              *string  `gorm:"column:ownerId;index:Document_ownerId_idx" json:"ownerId"`
	AssistantEnabled     bool     `gorm:"column:assistantEnabled;default:false;not null" json:"assistantEnabled"`
	AdvancedExcelEnabled bool     `gorm:"column:advancedExcelEnabled;default:false;not null" json:"advancedExcelEnabled"`
	AgentsEnabled        bool     `gorm:"column:agentsEnabled;default:false;not null" json:"agentsEnabled"`
	DownloadOnly         bool     `gorm:"column:downloadOnly;default:false;not null" json:"downloadOnly"`
	HiddenInAllDocuments bool     `gorm:"column:hiddenInAllDocuments;index:Document_teamId_hiddenInAllDocuments_idx;default:false;not null" json:"hiddenInAllDocuments"`
	CreatedAt            DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt            DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	FolderID             *string  `gorm:"column:folderId;index:Document_folderId_idx;index:Document_teamId_folderId_idx" json:"folderId"`
	IsExternalUpload     bool     `gorm:"column:isExternalUpload;default:false;not null" json:"isExternalUpload"`
	Owner                User     `gorm:"foreignKey:OwnerID"`
	Team                 Team
	Folder               Folder
	Links                []Link                 `gorm:"foreignKey:DocumentID;constraint:OnDelete:CASCADE"`
	Views                []View                 `gorm:"foreignKey:DocumentID;constraint:OnDelete:CASCADE"`
	Versions             []DocumentVersion      `gorm:"foreignKey:DocumentID;constraint:OnDelete:CASCADE"`
	Chats                []Chat                 `gorm:"foreignKey:DocumentID;constraint:OnDelete:CASCADE"`
	Datarooms            []DataroomDocument     `gorm:"foreignKey:DocumentID;constraint:OnDelete:CASCADE"`
	Tags                 []TagItem              `gorm:"foreignKey:DocumentID;constraint:OnDelete:CASCADE"`
	Annotations          []DocumentAnnotation   `gorm:"foreignKey:DocumentID;constraint:OnDelete:CASCADE"`
	RedactionJobs        []DocumentRedactionJob `gorm:"foreignKey:DocumentID;constraint:OnDelete:CASCADE"`
	UploadedDocument     []DocumentUpload       `gorm:"foreignKey:DocumentID;constraint:OnDelete:CASCADE"`
}

func (Document) TableName() string {
	return "Document"
}

func (m *Document) BeforeCreate(tx *gorm.DB) error {
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

func (*Document) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type DocumentVersion struct {
	ID                string   `gorm:"column:id;primaryKey" json:"id"`
	VersionNumber     int      `gorm:"column:versionNumber;uniqueIndex:DocumentVersion_versionNumber_documentId_key;not null" json:"versionNumber"`
	DocumentID        string   `gorm:"column:documentId;uniqueIndex:DocumentVersion_versionNumber_documentId_key;index:DocumentVersion_documentId_idx;index:DocumentVersion_documentId_isPrimary_idx;index:DocumentVersion_documentId_createdAt_idx;index:DocumentVersion_documentId_isPrimary_createdAt_idx;not null" json:"documentId"`
	File              string   `gorm:"column:file;not null" json:"file"`
	OriginalFile      *string  `gorm:"column:originalFile" json:"originalFile"`
	Type              *string  `gorm:"column:type" json:"type"`
	ContentType       *string  `gorm:"column:contentType" json:"contentType"`
	FileSize          *int64   `gorm:"column:fileSize" json:"fileSize"`
	StorageType       string   `gorm:"column:storageType;default:'VERCEL_BLOB';not null" json:"storageType"`
	NumPages          *int     `gorm:"column:numPages" json:"numPages"`
	IsPrimary         bool     `gorm:"column:isPrimary;index:DocumentVersion_documentId_isPrimary_idx;index:DocumentVersion_documentId_isPrimary_createdAt_idx;default:false;not null" json:"isPrimary"`
	IsVertical        bool     `gorm:"column:isVertical;default:false;not null" json:"isVertical"`
	FileID            *string  `gorm:"column:fileId" json:"fileId"`
	VectorStoreFileID *string  `gorm:"column:vectorStoreFileId" json:"vectorStoreFileId"`
	HasPages          bool     `gorm:"column:hasPages;default:false;not null" json:"hasPages"`
	Length            *int     `gorm:"column:length" json:"length"`
	CreatedAt         DateTime `gorm:"column:createdAt;index:DocumentVersion_documentId_createdAt_idx;index:DocumentVersion_documentId_isPrimary_createdAt_idx;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt         DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Document          Document
	Pages             []DocumentPage         `gorm:"foreignKey:VersionID;constraint:OnDelete:CASCADE"`
	RedactionJobs     []DocumentRedactionJob `gorm:"foreignKey:DocumentVersionID;constraint:OnDelete:CASCADE"`
}

func (DocumentVersion) TableName() string {
	return "DocumentVersion"
}

func (m *DocumentVersion) BeforeCreate(tx *gorm.DB) error {
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

func (*DocumentVersion) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type DocumentPage struct {
	ID            string          `gorm:"column:id;primaryKey" json:"id"`
	VersionID     string          `gorm:"column:versionId;uniqueIndex:DocumentPage_pageNumber_versionId_key;index:DocumentPage_versionId_idx;not null" json:"versionId"`
	PageNumber    int             `gorm:"column:pageNumber;uniqueIndex:DocumentPage_pageNumber_versionId_key;not null" json:"pageNumber"`
	EmbeddedLinks []string        `gorm:"column:embeddedLinks;serializer:json;not null" json:"embeddedLinks"`
	PageLinks     datatypes.JSON  `gorm:"column:pageLinks" json:"pageLinks"`
	Metadata      datatypes.JSON  `gorm:"column:metadata" json:"metadata"`
	File          string          `gorm:"column:file;not null" json:"file"`
	StorageType   string          `gorm:"column:storageType;default:'VERCEL_BLOB';not null" json:"storageType"`
	CreatedAt     DateTime        `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt     DateTime        `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Version       DocumentVersion `gorm:"foreignKey:VersionID"`
}

func (DocumentPage) TableName() string {
	return "DocumentPage"
}

func (m *DocumentPage) BeforeCreate(tx *gorm.DB) error {
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

func (*DocumentPage) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Folder struct {
	ID                   string   `gorm:"column:id;primaryKey" json:"id"`
	Name                 string   `gorm:"column:name;not null" json:"name"`
	Path                 string   `gorm:"column:path;uniqueIndex:Folder_teamId_path_key;not null" json:"path"`
	ParentID             *string  `gorm:"column:parentId;index:Folder_parentId_idx" json:"parentId"`
	HiddenInAllDocuments bool     `gorm:"column:hiddenInAllDocuments;index:Folder_teamId_hiddenInAllDocuments_idx;default:false;not null" json:"hiddenInAllDocuments"`
	Icon                 *string  `gorm:"column:icon" json:"icon"`
	Color                *string  `gorm:"column:color" json:"color"`
	TeamID               string   `gorm:"column:teamId;uniqueIndex:Folder_teamId_path_key;index:Folder_teamId_hiddenInAllDocuments_idx;not null" json:"teamId"`
	CreatedAt            DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt            DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	ParentFolder         *Folder  `gorm:"foreignKey:ParentID"`
	Team                 Team
	Documents            []Document `gorm:"foreignKey:FolderID;constraint:OnDelete:SET NULL"`
	ChildFolders         []Folder   `gorm:"foreignKey:ParentID;constraint:OnDelete:SET NULL"`
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

type DocumentUpload struct {
	ID                 string   `gorm:"column:id;primaryKey" json:"id"`
	DocumentID         string   `gorm:"column:documentId;index:DocumentUpload_documentId_idx;not null" json:"documentId"`
	TeamID             string   `gorm:"column:teamId;index:DocumentUpload_teamId_idx;not null" json:"teamId"`
	ViewerID           *string  `gorm:"column:viewerId;index:DocumentUpload_viewerId_idx" json:"viewerId"`
	ViewID             *string  `gorm:"column:viewId;index:DocumentUpload_viewId_idx" json:"viewId"`
	LinkID             string   `gorm:"column:linkId;index:DocumentUpload_linkId_idx;not null" json:"linkId"`
	DataroomID         *string  `gorm:"column:dataroomId;index:DocumentUpload_dataroomId_idx" json:"dataroomId"`
	DataroomDocumentID *string  `gorm:"column:dataroomDocumentId;index:DocumentUpload_dataroomDocumentId_idx" json:"dataroomDocumentId"`
	TaskID             *string  `gorm:"column:taskId;index:DocumentUpload_taskId_idx" json:"taskId"`
	OriginalFilename   *string  `gorm:"column:originalFilename" json:"originalFilename"`
	FileSize           *int64   `gorm:"column:fileSize" json:"fileSize"`
	NumPages           *int     `gorm:"column:numPages" json:"numPages"`
	MimeType           *string  `gorm:"column:mimeType" json:"mimeType"`
	UploadedAt         DateTime `gorm:"column:uploadedAt;not null" json:"uploadedAt"`
	CreatedAt          DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt          DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Document           Document
	Team               Team
	Viewer             Viewer
	View               View
	Link               Link
	Dataroom           Dataroom
	DataroomDocument   DataroomDocument
	Task               Task
}

func (DocumentUpload) TableName() string {
	return "DocumentUpload"
}

func (m *DocumentUpload) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.UploadedAt.IsZero() {
		m.UploadedAt = DateTime{Time: now}
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*DocumentUpload) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type PendingDocumentUpload struct {
	ID          string    `gorm:"column:id;primaryKey" json:"id"`
	UploadID    string    `gorm:"column:uploadId;uniqueIndex;not null" json:"uploadId"`
	TeamID      string    `gorm:"column:teamId;index:PendingDocumentUpload_teamId_expiresAt_idx;not null" json:"teamId"`
	TokenID     string    `gorm:"column:tokenId;index:PendingDocumentUpload_tokenId_expiresAt_idx;not null" json:"tokenId"`
	Key         string    `gorm:"column:key;not null" json:"key"`
	ContentType string    `gorm:"column:contentType;not null" json:"contentType"`
	StorageType string    `gorm:"column:storageType;default:'S3_PATH';not null" json:"storageType"`
	ExpiresAt   DateTime  `gorm:"column:expiresAt;index:PendingDocumentUpload_teamId_expiresAt_idx;index:PendingDocumentUpload_tokenId_expiresAt_idx;not null" json:"expiresAt"`
	ConsumedAt  *DateTime `gorm:"column:consumedAt" json:"consumedAt"`
	CreatedAt   DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
}

func (PendingDocumentUpload) TableName() string {
	return "PendingDocumentUpload"
}

func (m *PendingDocumentUpload) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type Integration struct {
	ID                    string                 `gorm:"column:id;primaryKey" json:"id"`
	Name                  string                 `gorm:"column:name;not null" json:"name"`
	Slug                  string                 `gorm:"column:slug;uniqueIndex;not null" json:"slug"`
	Description           *string                `gorm:"column:description" json:"description"`
	Readme                *string                `gorm:"column:readme" json:"readme"`
	Developer             string                 `gorm:"column:developer;not null" json:"developer"`
	Website               string                 `gorm:"column:website;not null" json:"website"`
	Logo                  *string                `gorm:"column:logo" json:"logo"`
	Screenshots           datatypes.JSON         `gorm:"column:screenshots" json:"screenshots"`
	Verified              bool                   `gorm:"column:verified;default:false;not null" json:"verified"`
	InstallURL            *string                `gorm:"column:installUrl" json:"installUrl"`
	Category              *string                `gorm:"column:category" json:"category"`
	ComingSoon            bool                   `gorm:"column:comingSoon;default:false;not null" json:"comingSoon"`
	CreatedAt             DateTime               `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt             DateTime               `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	InstalledIntegrations []InstalledIntegration `gorm:"foreignKey:IntegrationID;constraint:OnDelete:CASCADE"`
}

func (Integration) TableName() string {
	return "Integration"
}

func (m *Integration) BeforeCreate(tx *gorm.DB) error {
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

func (*Integration) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type InstalledIntegration struct {
	ID            string         `gorm:"column:id;primaryKey" json:"id"`
	Credentials   datatypes.JSON `gorm:"column:credentials" json:"credentials"`
	Configuration datatypes.JSON `gorm:"column:configuration" json:"configuration"`
	Enabled       *bool          `gorm:"column:enabled;default:true;not null" json:"enabled"`
	IntegrationID string         `gorm:"column:integrationId;uniqueIndex:InstalledIntegration_teamId_integrationId_key;index:InstalledIntegration_integrationId_idx;not null" json:"integrationId"`
	UserID        *string        `gorm:"column:userId" json:"userId"`
	TeamID        string         `gorm:"column:teamId;uniqueIndex:InstalledIntegration_teamId_integrationId_key;index:InstalledIntegration_teamId_idx;not null" json:"teamId"`
	CreatedAt     DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt     DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Integration   Integration
	User          User
	Team          Team
}

func (InstalledIntegration) TableName() string {
	return "InstalledIntegration"
}

func (m *InstalledIntegration) BeforeCreate(tx *gorm.DB) error {
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

func (*InstalledIntegration) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type JacksonIndex struct {
	ID       int    `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	Key      string `gorm:"column:key;index:_jackson_index_key_store;not null" json:"key"`
	StoreKey string `gorm:"column:storeKey;index:_jackson_index_key_store;not null" json:"storeKey"`
}

func (JacksonIndex) TableName() string {
	return "jackson_index"
}

type JacksonStore struct {
	Key        string    `gorm:"column:key;primaryKey" json:"key"`
	Value      string    `gorm:"column:value;not null" json:"value"`
	Iv         *string   `gorm:"column:iv" json:"iv"`
	Tag        *string   `gorm:"column:tag" json:"tag"`
	Namespace  *string   `gorm:"column:namespace;index:_jackson_store_namespace" json:"namespace"`
	CreatedAt  DateTime  `gorm:"column:createdAt;type:timestamp(0);autoCreateTime:false;not null" json:"createdAt"`
	ModifiedAt *DateTime `gorm:"column:modifiedAt;type:timestamp(0)" json:"modifiedAt"`
}

func (JacksonStore) TableName() string {
	return "jackson_store"
}

func (m *JacksonStore) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type JacksonTTL struct {
	Key       string `gorm:"column:key;primaryKey" json:"key"`
	ExpiresAt int64  `gorm:"column:expiresAt;index:_jackson_ttl_expires_at;not null" json:"expiresAt"`
}

func (JacksonTTL) TableName() string {
	return "jackson_ttl"
}

type Link struct {
	ID                         string         `gorm:"column:id;primaryKey" json:"id"`
	DocumentID                 *string        `gorm:"column:documentId;index:Link_documentId_idx;index:Link_documentId_isArchived_idx" json:"documentId"`
	DataroomID                 *string        `gorm:"column:dataroomId" json:"dataroomId"`
	LinkType                   string         `gorm:"column:linkType;default:'DOCUMENT_LINK';not null" json:"linkType"`
	URL                        *string        `gorm:"column:url;uniqueIndex" json:"url"`
	Name                       *string        `gorm:"column:name" json:"name"`
	Slug                       *string        `gorm:"column:slug;uniqueIndex:Link_domainSlug_slug_key" json:"slug"`
	ExpiresAt                  *DateTime      `gorm:"column:expiresAt" json:"expiresAt"`
	Password                   *string        `gorm:"column:password" json:"password"`
	AllowList                  []string       `gorm:"column:allowList;serializer:json;not null" json:"allowList"`
	DenyList                   []string       `gorm:"column:denyList;serializer:json;not null" json:"denyList"`
	EmailProtected             *bool          `gorm:"column:emailProtected;default:true;not null" json:"emailProtected"`
	EmailAuthenticated         bool           `gorm:"column:emailAuthenticated;default:false;not null" json:"emailAuthenticated"`
	AllowDownload              *bool          `gorm:"column:allowDownload;default:false" json:"allowDownload"`
	IsArchived                 bool           `gorm:"column:isArchived;index:Link_documentId_isArchived_idx;default:false;not null" json:"isArchived"`
	DeletedAt                  *DateTime      `gorm:"column:deletedAt;index:Link_deletedAt_idx" json:"deletedAt"`
	DomainID                   *string        `gorm:"column:domainId" json:"domainId"`
	DomainSlug                 *string        `gorm:"column:domainSlug;uniqueIndex:Link_domainSlug_slug_key" json:"domainSlug"`
	CreatedAt                  DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt                  DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	EnableNotification         *bool          `gorm:"column:enableNotification;default:true" json:"enableNotification"`
	EnableFeedback             *bool          `gorm:"column:enableFeedback;default:false" json:"enableFeedback"`
	EnableQuestion             *bool          `gorm:"column:enableQuestion;default:false" json:"enableQuestion"`
	EnableScreenshotProtection *bool          `gorm:"column:enableScreenshotProtection;default:false" json:"enableScreenshotProtection"`
	EnableConfidentialView     *bool          `gorm:"column:enableConfidentialView;default:false" json:"enableConfidentialView"`
	EnableAgreement            *bool          `gorm:"column:enableAgreement;default:false" json:"enableAgreement"`
	AgreementID                *string        `gorm:"column:agreementId" json:"agreementId"`
	ShowBanner                 *bool          `gorm:"column:showBanner;default:false" json:"showBanner"`
	EnableWatermark            *bool          `gorm:"column:enableWatermark;default:false" json:"enableWatermark"`
	WatermarkConfig            datatypes.JSON `gorm:"column:watermarkConfig" json:"watermarkConfig"`
	AudienceType               string         `gorm:"column:audienceType;default:'GENERAL';not null" json:"audienceType"`
	GroupID                    *string        `gorm:"column:groupId" json:"groupId"`
	PermissionGroupID          *string        `gorm:"column:permissionGroupId;index:Link_permissionGroupId_idx" json:"permissionGroupId"`
	MetaTitle                  *string        `gorm:"column:metaTitle" json:"metaTitle"`
	MetaDescription            *string        `gorm:"column:metaDescription" json:"metaDescription"`
	MetaImage                  *string        `gorm:"column:metaImage" json:"metaImage"`
	MetaFavicon                *string        `gorm:"column:metaFavicon" json:"metaFavicon"`
	EnableCustomMetatag        *bool          `gorm:"column:enableCustomMetatag;default:false" json:"enableCustomMetatag"`
	WelcomeMessage             *string        `gorm:"column:welcomeMessage" json:"welcomeMessage"`
	EnableConversation         bool           `gorm:"column:enableConversation;default:false;not null" json:"enableConversation"`
	EnableAIAgents             *bool          `gorm:"column:enableAIAgents;default:false" json:"enableAIAgents"`
	EnableUpload               *bool          `gorm:"column:enableUpload;default:false" json:"enableUpload"`
	IsFileRequestOnly          *bool          `gorm:"column:isFileRequestOnly;default:false" json:"isFileRequestOnly"`
	UploadFolderID             *string        `gorm:"column:uploadFolderId" json:"uploadFolderId"`
	UploadFolderIds            []string       `gorm:"column:uploadFolderIds;serializer:json;not null" json:"uploadFolderIds"`
	EnableIndexFile            *bool          `gorm:"column:enableIndexFile;default:false" json:"enableIndexFile"`
	TeamID                     *string        `gorm:"column:teamId;index:Link_teamId_idx" json:"teamId"`
	BrandID                    *string        `gorm:"column:brandId;index:Link_brandId_idx" json:"brandId"`
	OwnerID                    *string        `gorm:"column:ownerId;index:Link_ownerId_idx" json:"ownerId"`
	Document                   Document
	Dataroom                   Dataroom
	Domain                     Domain
	Agreement                  Agreement
	Group                      ViewerGroup `gorm:"foreignKey:GroupID"`
	PermissionGroup            PermissionGroup
	Team                       Team
	Brand                      Brand
	Owner                      User                         `gorm:"foreignKey:OwnerID"`
	Views                      []View                       `gorm:"foreignKey:LinkID;constraint:OnDelete:CASCADE"`
	Conversations              []Conversation               `gorm:"foreignKey:LinkID;constraint:OnDelete:SET NULL"`
	QuestionAssignments        []DataroomQuestionAssignment `gorm:"foreignKey:LinkID;constraint:OnDelete:CASCADE"`
	DataroomFaqItems           []DataroomFaqItem            `gorm:"foreignKey:LinkID;constraint:OnDelete:SET NULL"`
	Chats                      []Chat                       `gorm:"foreignKey:LinkID;constraint:OnDelete:CASCADE"`
	UploadedDocuments          []DocumentUpload             `gorm:"foreignKey:LinkID;constraint:OnDelete:CASCADE"`
	CustomFields               []CustomField                `gorm:"foreignKey:LinkID;constraint:OnDelete:CASCADE"`
	Tags                       []TagItem                    `gorm:"foreignKey:LinkID;constraint:OnDelete:CASCADE"`
	ViewerInvitations          []ViewerInvitation           `gorm:"foreignKey:LinkID;constraint:OnDelete:CASCADE"`
	VisitorGroups              []LinkVisitorGroup           `gorm:"foreignKey:LinkID;constraint:OnDelete:CASCADE"`
	TaskAssignments            []TaskAssignment             `gorm:"foreignKey:LinkID;constraint:OnDelete:CASCADE"`
	Feedback                   *Feedback                    `gorm:"foreignKey:LinkID;constraint:OnDelete:CASCADE"`
	Workflow                   *Workflow                    `gorm:"foreignKey:EntryLinkID;constraint:OnDelete:CASCADE"`
}

func (Link) TableName() string {
	return "Link"
}

func (m *Link) BeforeCreate(tx *gorm.DB) error {
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

func (*Link) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LinkPreset struct {
	ID                         string         `gorm:"column:id;primaryKey" json:"id"`
	Name                       string         `gorm:"column:name;not null" json:"name"`
	TeamID                     string         `gorm:"column:teamId;index:LinkPreset_teamId_idx;not null" json:"teamId"`
	PID                        *string        `gorm:"column:pId;uniqueIndex" json:"pId"`
	EnableCustomMetaTag        *bool          `gorm:"column:enableCustomMetaTag;default:false" json:"enableCustomMetaTag"`
	MetaTitle                  *string        `gorm:"column:metaTitle" json:"metaTitle"`
	MetaDescription            *string        `gorm:"column:metaDescription" json:"metaDescription"`
	MetaImage                  *string        `gorm:"column:metaImage" json:"metaImage"`
	MetaFavicon                *string        `gorm:"column:metaFavicon" json:"metaFavicon"`
	EnableNotification         *bool          `gorm:"column:enableNotification;default:false" json:"enableNotification"`
	EmailProtected             *bool          `gorm:"column:emailProtected;default:true" json:"emailProtected"`
	EmailAuthenticated         *bool          `gorm:"column:emailAuthenticated;default:false" json:"emailAuthenticated"`
	AllowDownload              *bool          `gorm:"column:allowDownload;default:false" json:"allowDownload"`
	EnableAllowList            *bool          `gorm:"column:enableAllowList;default:false" json:"enableAllowList"`
	AllowList                  []string       `gorm:"column:allowList;serializer:json;not null" json:"allowList"`
	EnableDenyList             *bool          `gorm:"column:enableDenyList;default:false" json:"enableDenyList"`
	DenyList                   []string       `gorm:"column:denyList;serializer:json;not null" json:"denyList"`
	ExpiresIn                  *int           `gorm:"column:expiresIn" json:"expiresIn"`
	EnableScreenshotProtection *bool          `gorm:"column:enableScreenshotProtection;default:false" json:"enableScreenshotProtection"`
	EnableConfidentialView     *bool          `gorm:"column:enableConfidentialView;default:false" json:"enableConfidentialView"`
	ExpiresAt                  *DateTime      `gorm:"column:expiresAt" json:"expiresAt"`
	EnablePassword             *bool          `gorm:"column:enablePassword;default:false" json:"enablePassword"`
	Password                   *string        `gorm:"column:password" json:"password"`
	EnableWatermark            *bool          `gorm:"column:enableWatermark;default:false" json:"enableWatermark"`
	WatermarkConfig            datatypes.JSON `gorm:"column:watermarkConfig" json:"watermarkConfig"`
	EnableAgreement            *bool          `gorm:"column:enableAgreement;default:false" json:"enableAgreement"`
	AgreementID                *string        `gorm:"column:agreementId" json:"agreementId"`
	EnableCustomFields         *bool          `gorm:"column:enableCustomFields;default:false" json:"enableCustomFields"`
	CustomFields               datatypes.JSON `gorm:"column:customFields" json:"customFields"`
	ShowBanner                 *bool          `gorm:"column:showBanner;default:false" json:"showBanner"`
	WelcomeMessage             *string        `gorm:"column:welcomeMessage" json:"welcomeMessage"`
	IsDefault                  bool           `gorm:"column:isDefault;default:false;not null" json:"isDefault"`
	CreatedAt                  DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt                  DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Team                       Team
}

func (LinkPreset) TableName() string {
	return "LinkPreset"
}

func (m *LinkPreset) BeforeCreate(tx *gorm.DB) error {
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

func (*LinkPreset) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type VisitorGroup struct {
	ID        string   `gorm:"column:id;primaryKey" json:"id"`
	Name      string   `gorm:"column:name;not null" json:"name"`
	Emails    []string `gorm:"column:emails;serializer:json;not null" json:"emails"`
	TeamID    string   `gorm:"column:teamId;index:VisitorGroup_teamId_idx;not null" json:"teamId"`
	CreatedAt DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Team      Team
	Links     []LinkVisitorGroup `gorm:"foreignKey:VisitorGroupID;constraint:OnDelete:CASCADE"`
}

func (VisitorGroup) TableName() string {
	return "VisitorGroup"
}

func (m *VisitorGroup) BeforeCreate(tx *gorm.DB) error {
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

func (*VisitorGroup) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LinkVisitorGroup struct {
	ID             string   `gorm:"column:id;primaryKey" json:"id"`
	LinkID         string   `gorm:"column:linkId;uniqueIndex:LinkVisitorGroup_linkId_visitorGroupId_key;index:LinkVisitorGroup_linkId_idx;not null" json:"linkId"`
	VisitorGroupID string   `gorm:"column:visitorGroupId;uniqueIndex:LinkVisitorGroup_linkId_visitorGroupId_key;index:LinkVisitorGroup_visitorGroupId_idx;not null" json:"visitorGroupId"`
	CreatedAt      DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	Link           Link
	VisitorGroup   VisitorGroup
}

func (LinkVisitorGroup) TableName() string {
	return "LinkVisitorGroup"
}

func (m *LinkVisitorGroup) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type CustomField struct {
	ID          string   `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt   DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt   DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Type        string   `gorm:"column:type;not null" json:"type"`
	Identifier  string   `gorm:"column:identifier;not null" json:"identifier"`
	Label       string   `gorm:"column:label;not null" json:"label"`
	Placeholder *string  `gorm:"column:placeholder" json:"placeholder"`
	Required    bool     `gorm:"column:required;default:false;not null" json:"required"`
	Disabled    bool     `gorm:"column:disabled;default:false;not null" json:"disabled"`
	LinkID      string   `gorm:"column:linkId;index:CustomField_linkId_idx;not null" json:"linkId"`
	OrderIndex  int      `gorm:"column:orderIndex;default:0;not null" json:"orderIndex"`
	Link        Link
}

func (CustomField) TableName() string {
	return "CustomField"
}

func (m *CustomField) BeforeCreate(tx *gorm.DB) error {
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

func (*CustomField) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type CustomFieldResponse struct {
	ID        string         `gorm:"column:id;primaryKey" json:"id"`
	Data      datatypes.JSON `gorm:"column:data;not null" json:"data"`
	ViewID    string         `gorm:"column:viewId;uniqueIndex;index:CustomFieldResponse_viewId_idx;not null" json:"viewId"`
	CreatedAt DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	View      View
}

func (CustomFieldResponse) TableName() string {
	return "CustomFieldResponse"
}

func (m *CustomFieldResponse) BeforeCreate(tx *gorm.DB) error {
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

func (*CustomFieldResponse) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type NotificationPreference struct {
	ID        string   `gorm:"column:id;primaryKey" json:"id"`
	UserID    string   `gorm:"column:userId;uniqueIndex:NotificationPreference_userId_teamId_type_key;index:NotificationPreference_userId_teamId_idx;not null" json:"userId"`
	TeamID    string   `gorm:"column:teamId;uniqueIndex:NotificationPreference_userId_teamId_type_key;index:NotificationPreference_userId_teamId_idx;not null" json:"teamId"`
	Type      string   `gorm:"column:type;uniqueIndex:NotificationPreference_userId_teamId_type_key;not null" json:"type"`
	Frequency *string  `gorm:"column:frequency;default:'IMMEDIATE';not null" json:"frequency"`
	Scope     *string  `gorm:"column:scope;default:'ALL';not null" json:"scope"`
	CreatedAt DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	User      User
	Team      Team
}

func (NotificationPreference) TableName() string {
	return "NotificationPreference"
}

func (m *NotificationPreference) BeforeCreate(tx *gorm.DB) error {
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

func (*NotificationPreference) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type OAuthClient struct {
	ID               string            `gorm:"column:id;primaryKey" json:"id"`
	ClientID         string            `gorm:"column:clientId;uniqueIndex;index:OAuthClient_clientId_idx;not null" json:"clientId"`
	ClientName       string            `gorm:"column:clientName;not null" json:"clientName"`
	ClientURI        *string           `gorm:"column:clientUri" json:"clientUri"`
	LogoURI          *string           `gorm:"column:logoUri" json:"logoUri"`
	RedirectUris     []string          `gorm:"column:redirectUris;serializer:json;not null" json:"redirectUris"`
	GrantTypes       []string          `gorm:"column:grantTypes;serializer:json;not null" json:"grantTypes"`
	Scopes           string            `gorm:"column:scopes;not null" json:"scopes"`
	TokenAuthMethod  string            `gorm:"column:tokenAuthMethod;not null" json:"tokenAuthMethod"`
	HashedSecret     *string           `gorm:"column:hashedSecret" json:"hashedSecret"`
	RegistrationType string            `gorm:"column:registrationType;not null" json:"registrationType"`
	MetadataURL      *string           `gorm:"column:metadataUrl" json:"metadataUrl"`
	CreatedAt        DateTime          `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt        DateTime          `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Tokens           []RestrictedToken `gorm:"foreignKey:ClientID;constraint:OnDelete:SET NULL"`
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

type OAuthClientApproval struct {
	ID                 string    `gorm:"column:id;primaryKey" json:"id"`
	UserID             string    `gorm:"column:userId;uniqueIndex:OAuthClientApproval_userId_clientId_key;not null" json:"userId"`
	ClientID           string    `gorm:"column:clientId;uniqueIndex:OAuthClientApproval_userId_clientId_key;index:OAuthClientApproval_clientId_idx;not null" json:"clientId"`
	ApprovedAt         DateTime  `gorm:"column:approvedAt;not null" json:"approvedAt"`
	RevokedAt          *DateTime `gorm:"column:revokedAt" json:"revokedAt"`
	ClientMetadataHash *string   `gorm:"column:clientMetadataHash" json:"clientMetadataHash"`
}

func (OAuthClientApproval) TableName() string {
	return "OAuthClientApproval"
}

func (m *OAuthClientApproval) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.ApprovedAt.IsZero() {
		m.ApprovedAt = DateTime{Time: now}
	}
	return nil
}

type OAuthGrantTeam struct {
	ID        string   `gorm:"column:id;primaryKey" json:"id"`
	GrantID   string   `gorm:"column:grantId;uniqueIndex;index:OAuthGrantTeam_grantId_idx;not null" json:"grantId"`
	TeamID    string   `gorm:"column:teamId;not null" json:"teamId"`
	UserID    string   `gorm:"column:userId;not null" json:"userId"`
	CreatedAt DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
}

func (OAuthGrantTeam) TableName() string {
	return "OAuthGrantTeam"
}

func (m *OAuthGrantTeam) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type OAuthRecord struct {
	ID         string         `gorm:"column:id;primaryKey" json:"id"`
	Kind       string         `gorm:"column:kind;index:OAuthRecord_kind_idx;not null" json:"kind"`
	Payload    datatypes.JSON `gorm:"column:payload;not null" json:"payload"`
	GrantID    *string        `gorm:"column:grantId;index:OAuthRecord_grantId_idx" json:"grantId"`
	UserCode   *string        `gorm:"column:userCode;index:OAuthRecord_userCode_idx" json:"userCode"`
	UID        *string        `gorm:"column:uid;index:OAuthRecord_uid_idx" json:"uid"`
	ExpiresAt  *DateTime      `gorm:"column:expiresAt;index:OAuthRecord_expiresAt_idx" json:"expiresAt"`
	ConsumedAt *DateTime      `gorm:"column:consumedAt" json:"consumedAt"`
	CreatedAt  DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
}

func (OAuthRecord) TableName() string {
	return "OAuthRecord"
}

func (m *OAuthRecord) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type DocumentRedactionJob struct {
	ID                string   `gorm:"column:id;primaryKey" json:"id"`
	DocumentID        string   `gorm:"column:documentId;index:DocumentRedactionJob_documentId_idx;not null" json:"documentId"`
	DocumentVersionID string   `gorm:"column:documentVersionId;index:DocumentRedactionJob_documentVersionId_idx;not null" json:"documentVersionId"`
	TeamID            string   `gorm:"column:teamId;index:DocumentRedactionJob_teamId_idx;index:DocumentRedactionJob_teamId_status_idx;not null" json:"teamId"`
	CreatedByID       string   `gorm:"column:createdById;not null" json:"createdById"`
	Status            *string  `gorm:"column:status;index:DocumentRedactionJob_teamId_status_idx;default:'PENDING';not null" json:"status"`
	CustomTerms       []string `gorm:"column:customTerms;serializer:json;not null" json:"customTerms"`
	Reasons           []string `gorm:"column:reasons;serializer:json;not null" json:"reasons"`
	ResultVersionID   *string  `gorm:"column:resultVersionId" json:"resultVersionId"`
	Error             *string  `gorm:"column:error" json:"error"`
	DetectRunID       *string  `gorm:"column:detectRunId" json:"detectRunId"`
	ApplyRunID        *string  `gorm:"column:applyRunId" json:"applyRunId"`
	CreatedAt         DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt         DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Document          Document
	DocumentVersion   DocumentVersion
	Team              Team
	CreatedBy         User                `gorm:"foreignKey:CreatedByID"`
	Redactions        []DocumentRedaction `gorm:"foreignKey:JobID;constraint:OnDelete:CASCADE"`
}

func (DocumentRedactionJob) TableName() string {
	return "DocumentRedactionJob"
}

func (m *DocumentRedactionJob) BeforeCreate(tx *gorm.DB) error {
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

func (*DocumentRedactionJob) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type DocumentRedaction struct {
	ID          string               `gorm:"column:id;primaryKey" json:"id"`
	JobID       string               `gorm:"column:jobId;index:DocumentRedaction_jobId_idx;index:DocumentRedaction_jobId_pageNumber_idx;index:DocumentRedaction_jobId_status_idx;not null" json:"jobId"`
	PageNumber  int                  `gorm:"column:pageNumber;index:DocumentRedaction_jobId_pageNumber_idx;not null" json:"pageNumber"`
	X           float64              `gorm:"column:x;not null" json:"x"`
	Y           float64              `gorm:"column:y;not null" json:"y"`
	Width       float64              `gorm:"column:width;not null" json:"width"`
	Height      float64              `gorm:"column:height;not null" json:"height"`
	TextPreview *string              `gorm:"column:textPreview" json:"textPreview"`
	Category    *string              `gorm:"column:category" json:"category"`
	Confidence  *string              `gorm:"column:confidence" json:"confidence"`
	Reason      *string              `gorm:"column:reason" json:"reason"`
	Source      *string              `gorm:"column:source;default:'AI';not null" json:"source"`
	Status      *string              `gorm:"column:status;index:DocumentRedaction_jobId_status_idx;default:'PENDING';not null" json:"status"`
	CreatedAt   DateTime             `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt   DateTime             `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Job         DocumentRedactionJob `gorm:"foreignKey:JobID"`
}

func (DocumentRedaction) TableName() string {
	return "DocumentRedaction"
}

func (m *DocumentRedaction) BeforeCreate(tx *gorm.DB) error {
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

func (*DocumentRedaction) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type ReferralInvite struct {
	ID           string   `gorm:"column:id;primaryKey" json:"id"`
	UserID       string   `gorm:"column:userId;index:ReferralInvite_userId_idx;not null" json:"userId"`
	TeamID       *string  `gorm:"column:teamId;index:ReferralInvite_teamId_idx" json:"teamId"`
	ScopeID      string   `gorm:"column:scopeId;uniqueIndex:ReferralInvite_scopeId_email_key;not null" json:"scopeId"`
	Email        string   `gorm:"column:email;uniqueIndex:ReferralInvite_scopeId_email_key;not null" json:"email"`
	ReferralLink *string  `gorm:"column:referralLink" json:"referralLink"`
	SentAt       DateTime `gorm:"column:sentAt;not null" json:"sentAt"`
	User         User
	Team         Team
}

func (ReferralInvite) TableName() string {
	return "ReferralInvite"
}

func (m *ReferralInvite) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.SentAt.IsZero() {
		m.SentAt = DateTime{Time: now}
	}
	return nil
}

type Account struct {
	ID                string  `gorm:"column:id;primaryKey" json:"id"`
	UserID            string  `gorm:"column:userId;not null" json:"userId"`
	Type              string  `gorm:"column:type;not null" json:"type"`
	Provider          string  `gorm:"column:provider;uniqueIndex:Account_provider_providerAccountId_key;not null" json:"provider"`
	ProviderAccountID string  `gorm:"column:providerAccountId;uniqueIndex:Account_provider_providerAccountId_key;not null" json:"providerAccountId"`
	RefreshToken      *string `gorm:"column:refresh_token;type:text" json:"refresh_token"`
	AccessToken       *string `gorm:"column:access_token;type:text" json:"access_token"`
	ExpiresAt         *int    `gorm:"column:expires_at" json:"expires_at"`
	TokenType         *string `gorm:"column:token_type" json:"token_type"`
	Scope             *string `gorm:"column:scope" json:"scope"`
	IDToken           *string `gorm:"column:id_token;type:text" json:"id_token"`
	SessionState      *string `gorm:"column:session_state" json:"session_state"`
	User              User
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
	UserID       string   `gorm:"column:userId;not null" json:"userId"`
	Expires      DateTime `gorm:"column:expires;not null" json:"expires"`
	User         User
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

type User struct {
	ID                        string                    `gorm:"column:id;primaryKey" json:"id"`
	Name                      *string                   `gorm:"column:name" json:"name"`
	Email                     *string                   `gorm:"column:email;uniqueIndex" json:"email"`
	EmailVerified             *DateTime                 `gorm:"column:emailVerified" json:"emailVerified"`
	Image                     *string                   `gorm:"column:image" json:"image"`
	CreatedAt                 DateTime                  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	ContactID                 *string                   `gorm:"column:contactId" json:"contactId"`
	Plan                      *string                   `gorm:"column:plan;default:'free';not null" json:"plan"`
	StripeID                  *string                   `gorm:"column:stripeId;uniqueIndex" json:"stripeId"`
	SubscriptionID            *string                   `gorm:"column:subscriptionId;uniqueIndex" json:"subscriptionId"`
	StartsAt                  *DateTime                 `gorm:"column:startsAt" json:"startsAt"`
	EndsAt                    *DateTime                 `gorm:"column:endsAt" json:"endsAt"`
	ReferralTrack             *string                   `gorm:"column:referralTrack" json:"referralTrack"`
	ReferralTrackSelectedAt   *DateTime                 `gorm:"column:referralTrackSelectedAt" json:"referralTrackSelectedAt"`
	ReferralProfile           datatypes.JSON            `gorm:"column:referralProfile" json:"referralProfile"`
	Accounts                  []Account                 `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	Sessions                  []Session                 `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	Documents                 []Document                `gorm:"foreignKey:OwnerID;constraint:OnDelete:SET NULL"`
	Teams                     []UserTeam                `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	UserDatarooms             []UserDataroom            `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	Domains                   []Domain                  `gorm:"foreignKey:UserID;constraint:OnDelete:SET NULL"`
	Chats                     []Chat                    `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	RestrictedTokens          []RestrictedToken         `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	ParticipatedConversations []ConversationParticipant `gorm:"foreignKey:UserID;constraint:OnDelete:SET NULL"`
	Messages                  []Message                 `gorm:"foreignKey:UserID;constraint:OnDelete:SET NULL"`
	CreatedDataroomQuestions  []DataroomQuestion        `gorm:"foreignKey:CreatedByUserID;constraint:OnDelete:SET NULL"`
	PublishedFaqItems         []DataroomFaqItem         `gorm:"foreignKey:PublishedByUserID;constraint:OnDelete:CASCADE"`
	CreatedAnnotations        []DocumentAnnotation      `gorm:"foreignKey:CreatedByID;constraint:OnDelete:CASCADE"`
	CreatedRedactionJobs      []DocumentRedactionJob    `gorm:"foreignKey:CreatedByID"`
	InstalledIntegrations     []InstalledIntegration    `gorm:"foreignKey:UserID;constraint:OnDelete:SET NULL"`
	Links                     []Link                    `gorm:"foreignKey:OwnerID;constraint:OnDelete:SET NULL"`
	FrozenDatarooms           []Dataroom                `gorm:"foreignKey:FrozenBy;constraint:OnDelete:SET NULL"`
	CreatedTasks              []Task                    `gorm:"foreignKey:CreatedByUserID;constraint:OnDelete:SET NULL"`
	TaskActivities            []TaskActivity            `gorm:"foreignKey:UserID;constraint:OnDelete:SET NULL"`
	NotificationPreferences   []NotificationPreference  `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	ReferralInvites           []ReferralInvite          `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
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
	return nil
}

type Domain struct {
	ID          string   `gorm:"column:id;primaryKey" json:"id"`
	Slug        string   `gorm:"column:slug;uniqueIndex;not null" json:"slug"`
	UserID      *string  `gorm:"column:userId;index:Domain_userId_idx" json:"userId"`
	TeamID      string   `gorm:"column:teamId;index:Domain_teamId_idx;not null" json:"teamId"`
	Verified    bool     `gorm:"column:verified;default:false;not null" json:"verified"`
	IsDefault   bool     `gorm:"column:isDefault;default:false;not null" json:"isDefault"`
	RedirectURL *string  `gorm:"column:redirectUrl" json:"redirectUrl"`
	LastChecked DateTime `gorm:"column:lastChecked;not null" json:"lastChecked"`
	CreatedAt   DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt   DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	User        User
	Team        Team
	Links       []Link `gorm:"foreignKey:DomainID;constraint:OnDelete:SET NULL"`
}

func (Domain) TableName() string {
	return "Domain"
}

func (m *Domain) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.LastChecked.IsZero() {
		m.LastChecked = DateTime{Time: now}
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*Domain) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type View struct {
	ID                   string         `gorm:"column:id;primaryKey" json:"id"`
	LinkID               string         `gorm:"column:linkId;index:View_linkId_idx;not null" json:"linkId"`
	DocumentID           *string        `gorm:"column:documentId;index:View_documentId_idx;index:View_viewerId_documentId_idx;index:View_documentId_isArchived_idx;index:View_documentId_viewedAt_idx" json:"documentId"`
	DataroomID           *string        `gorm:"column:dataroomId;index:View_dataroomId_idx" json:"dataroomId"`
	DataroomViewID       *string        `gorm:"column:dataroomViewId;index:View_dataroomViewId_idx" json:"dataroomViewId"`
	ViewerEmail          *string        `gorm:"column:viewerEmail;index:View_viewerEmail_idx" json:"viewerEmail"`
	ViewerName           *string        `gorm:"column:viewerName" json:"viewerName"`
	Verified             bool           `gorm:"column:verified;default:false;not null" json:"verified"`
	ViewedAt             DateTime       `gorm:"column:viewedAt;index:View_viewedAt_idx;index:View_documentId_viewedAt_idx;not null" json:"viewedAt"`
	DownloadedAt         *DateTime      `gorm:"column:downloadedAt" json:"downloadedAt"`
	DownloadType         *string        `gorm:"column:downloadType" json:"downloadType"`
	DownloadMetadata     datatypes.JSON `gorm:"column:downloadMetadata" json:"downloadMetadata"`
	ViewType             string         `gorm:"column:viewType;default:'DOCUMENT_VIEW';not null" json:"viewType"`
	ViewerID             *string        `gorm:"column:viewerId;index:View_viewerId_idx;index:View_viewerId_documentId_idx" json:"viewerId"`
	GroupID              *string        `gorm:"column:groupId;index:View_groupId_idx" json:"groupId"`
	IsArchived           bool           `gorm:"column:isArchived;index:View_documentId_isArchived_idx;default:false;not null" json:"isArchived"`
	TeamID               *string        `gorm:"column:teamId;index:View_teamId_idx" json:"teamId"`
	Link                 Link
	Document             Document
	Dataroom             Dataroom
	Viewer               Viewer
	Group                ViewerGroup `gorm:"foreignKey:GroupID"`
	Team                 Team
	Reactions            []Reaction           `gorm:"foreignKey:ViewID;constraint:OnDelete:CASCADE"`
	ConversationViews    []ConversationView   `gorm:"foreignKey:ViewID;constraint:OnDelete:CASCADE"`
	Messages             []Message            `gorm:"foreignKey:ViewID;constraint:OnDelete:SET NULL"`
	InitialConversations []Conversation       `gorm:"foreignKey:InitialViewID;constraint:OnDelete:SET NULL"`
	UploadedDocuments    []DocumentUpload     `gorm:"foreignKey:ViewID;constraint:OnDelete:SET NULL"`
	TaskActivities       []TaskActivity       `gorm:"foreignKey:ViewID;constraint:OnDelete:SET NULL"`
	Chats                []Chat               `gorm:"foreignKey:ViewID;constraint:OnDelete:CASCADE"`
	FeedbackResponse     *FeedbackResponse    `gorm:"foreignKey:ViewID;constraint:OnDelete:CASCADE"`
	AgreementResponse    *AgreementResponse   `gorm:"foreignKey:ViewID;constraint:OnDelete:CASCADE"`
	CustomFieldResponse  *CustomFieldResponse `gorm:"foreignKey:ViewID;constraint:OnDelete:CASCADE"`
}

func (View) TableName() string {
	return "View"
}

func (m *View) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.ViewedAt.IsZero() {
		m.ViewedAt = DateTime{Time: now}
	}
	return nil
}

type Viewer struct {
	ID                        string         `gorm:"column:id;primaryKey" json:"id"`
	Email                     string         `gorm:"column:email;uniqueIndex:Viewer_teamId_email_key;not null" json:"email"`
	Verified                  bool           `gorm:"column:verified;default:false;not null" json:"verified"`
	InvitedAt                 *DateTime      `gorm:"column:invitedAt" json:"invitedAt"`
	NotificationPreferences   datatypes.JSON `gorm:"column:notificationPreferences" json:"notificationPreferences"`
	DataroomID                *string        `gorm:"column:dataroomId;index:Viewer_dataroomId_idx" json:"dataroomId"`
	TeamID                    string         `gorm:"column:teamId;uniqueIndex:Viewer_teamId_email_key;index:Viewer_teamId_idx;not null" json:"teamId"`
	CreatedAt                 DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt                 DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Dataroom                  Dataroom
	Team                      Team
	Views                     []View                       `gorm:"foreignKey:ViewerID;constraint:OnDelete:CASCADE"`
	Groups                    []ViewerGroupMembership      `gorm:"foreignKey:ViewerID;constraint:OnDelete:CASCADE"`
	Invitations               []ViewerInvitation           `gorm:"foreignKey:ViewerID;constraint:OnDelete:CASCADE"`
	ParticipatedConversations []ConversationParticipant    `gorm:"foreignKey:ViewerID;constraint:OnDelete:SET NULL"`
	Messages                  []Message                    `gorm:"foreignKey:ViewerID;constraint:OnDelete:SET NULL"`
	RecipientConversations    []Conversation               `gorm:"foreignKey:RecipientViewerID;constraint:OnDelete:SET NULL"`
	QuestionAssignments       []DataroomQuestionAssignment `gorm:"foreignKey:ViewerID;constraint:OnDelete:CASCADE"`
	UploadedDocuments         []DocumentUpload             `gorm:"foreignKey:ViewerID;constraint:OnDelete:SET NULL"`
	TaskAssignments           []TaskAssignment             `gorm:"foreignKey:ViewerID;constraint:OnDelete:CASCADE"`
	TaskActivities            []TaskActivity               `gorm:"foreignKey:ViewerID;constraint:OnDelete:SET NULL"`
	Chats                     []Chat                       `gorm:"foreignKey:ViewerID;constraint:OnDelete:CASCADE"`
}

func (Viewer) TableName() string {
	return "Viewer"
}

func (m *Viewer) BeforeCreate(tx *gorm.DB) error {
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

func (*Viewer) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Reaction struct {
	ID         string   `gorm:"column:id;primaryKey" json:"id"`
	ViewID     string   `gorm:"column:viewId;index:Reaction_viewId_idx;index:Reaction_viewId_type_idx;not null" json:"viewId"`
	PageNumber int      `gorm:"column:pageNumber;not null" json:"pageNumber"`
	Type       string   `gorm:"column:type;index:Reaction_viewId_type_idx;not null" json:"type"`
	CreatedAt  DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	View       View
}

func (Reaction) TableName() string {
	return "Reaction"
}

func (m *Reaction) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type SentEmail struct {
	ID         string   `gorm:"column:id;primaryKey" json:"id"`
	Type       string   `gorm:"column:type;not null" json:"type"`
	Recipient  string   `gorm:"column:recipient;not null" json:"recipient"`
	Marketing  bool     `gorm:"column:marketing;default:false;not null" json:"marketing"`
	CreatedAt  DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	TeamID     string   `gorm:"column:teamId;index:SentEmail_teamId_idx;not null" json:"teamId"`
	DomainSlug *string  `gorm:"column:domainSlug" json:"domainSlug"`
	Team       Team
}

func (SentEmail) TableName() string {
	return "SentEmail"
}

func (m *SentEmail) BeforeCreate(tx *gorm.DB) error {
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
	ID            string    `gorm:"column:id;primaryKey" json:"id"`
	Title         *string   `gorm:"column:title" json:"title"`
	TeamID        string    `gorm:"column:teamId;index:Chat_teamId_idx;not null" json:"teamId"`
	DocumentID    *string   `gorm:"column:documentId;index:Chat_documentId_idx" json:"documentId"`
	DataroomID    *string   `gorm:"column:dataroomId;index:Chat_dataroomId_idx" json:"dataroomId"`
	LinkID        *string   `gorm:"column:linkId;index:Chat_linkId_idx" json:"linkId"`
	ViewID        *string   `gorm:"column:viewId;index:Chat_viewId_idx" json:"viewId"`
	UserID        *string   `gorm:"column:userId;index:Chat_userId_idx" json:"userId"`
	ViewerID      *string   `gorm:"column:viewerId;index:Chat_viewerId_idx" json:"viewerId"`
	VectorStoreID *string   `gorm:"column:vectorStoreId" json:"vectorStoreId"`
	CreatedAt     DateTime  `gorm:"column:createdAt;index:Chat_createdAt_idx;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt     DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	LastMessageAt *DateTime `gorm:"column:lastMessageAt" json:"lastMessageAt"`
	Team          Team
	Document      Document
	Dataroom      Dataroom
	Link          Link
	View          View
	User          User
	Viewer        Viewer
	Messages      []ChatMessage `gorm:"foreignKey:ChatID;constraint:OnDelete:CASCADE"`
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
	ChatID    string         `gorm:"column:chatId;index:ChatMessage_chatId_idx;index:ChatMessage_chatId_createdAt_idx;not null" json:"chatId"`
	Role      string         `gorm:"column:role;not null" json:"role"`
	Content   string         `gorm:"column:content;type:text;not null" json:"content"`
	Metadata  datatypes.JSON `gorm:"column:metadata" json:"metadata"`
	CreatedAt DateTime       `gorm:"column:createdAt;index:ChatMessage_chatId_createdAt_idx;autoCreateTime:false;not null" json:"createdAt"`
	Chat      Chat
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
	return nil
}

type Feedback struct {
	ID        string         `gorm:"column:id;primaryKey" json:"id"`
	LinkID    string         `gorm:"column:linkId;uniqueIndex;index:Feedback_linkId_idx;not null" json:"linkId"`
	Data      datatypes.JSON `gorm:"column:data;not null" json:"data"`
	CreatedAt DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Link      Link
	Responses []FeedbackResponse `gorm:"foreignKey:FeedbackID;constraint:OnDelete:CASCADE"`
}

func (Feedback) TableName() string {
	return "Feedback"
}

func (m *Feedback) BeforeCreate(tx *gorm.DB) error {
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

func (*Feedback) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type FeedbackResponse struct {
	ID         string         `gorm:"column:id;primaryKey" json:"id"`
	FeedbackID string         `gorm:"column:feedbackId;index:FeedbackResponse_feedbackId_idx;not null" json:"feedbackId"`
	Data       datatypes.JSON `gorm:"column:data;not null" json:"data"`
	ViewID     string         `gorm:"column:viewId;uniqueIndex;index:FeedbackResponse_viewId_idx;not null" json:"viewId"`
	CreatedAt  DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt  DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Feedback   Feedback
	View       View
}

func (FeedbackResponse) TableName() string {
	return "FeedbackResponse"
}

func (m *FeedbackResponse) BeforeCreate(tx *gorm.DB) error {
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

func (*FeedbackResponse) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Agreement struct {
	ID                string    `gorm:"column:id;primaryKey" json:"id"`
	Name              string    `gorm:"column:name;not null" json:"name"`
	Content           string    `gorm:"column:content;not null" json:"content"`
	ContentType       *string   `gorm:"column:contentType;default:'LINK';not null" json:"contentType"`
	SigningProvider   *string   `gorm:"column:signingProvider;default:'LEGACY';not null" json:"signingProvider"`
	SigningExternalID *string   `gorm:"column:signingExternalId;uniqueIndex" json:"signingExternalId"`
	SigningEnvelopeID *string   `gorm:"column:signingEnvelopeId" json:"signingEnvelopeId"`
	SigningTemplateID *string   `gorm:"column:signingTemplateId" json:"signingTemplateId"`
	RequireName       *bool     `gorm:"column:requireName;default:true;not null" json:"requireName"`
	TeamID            string    `gorm:"column:teamId;index:Agreement_teamId_idx;not null" json:"teamId"`
	CreatedAt         DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt         DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	DeletedAt         *DateTime `gorm:"column:deletedAt" json:"deletedAt"`
	DeletedBy         *string   `gorm:"column:deletedBy" json:"deletedBy"`
	Team              Team
	Links             []Link              `gorm:"foreignKey:AgreementID;constraint:OnDelete:SET NULL"`
	Responses         []AgreementResponse `gorm:"foreignKey:AgreementID;constraint:OnDelete:CASCADE"`
}

func (Agreement) TableName() string {
	return "Agreement"
}

func (m *Agreement) BeforeCreate(tx *gorm.DB) error {
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

func (*Agreement) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type AgreementResponse struct {
	ID                    string    `gorm:"column:id;primaryKey" json:"id"`
	AgreementID           string    `gorm:"column:agreementId;index:AgreementResponse_agreementId_idx;not null" json:"agreementId"`
	ViewID                *string   `gorm:"column:viewId;uniqueIndex;index:AgreementResponse_viewId_idx" json:"viewId"`
	LinkID                *string   `gorm:"column:linkId;index:AgreementResponse_linkId_idx" json:"linkId"`
	SignerEmail           *string   `gorm:"column:signerEmail;index:AgreementResponse_signerEmail_idx" json:"signerEmail"`
	SignerName            *string   `gorm:"column:signerName" json:"signerName"`
	SigningStatus         *string   `gorm:"column:signingStatus;default:'PENDING';not null" json:"signingStatus"`
	SigningExternalID     *string   `gorm:"column:signingExternalId;uniqueIndex" json:"signingExternalId"`
	SigningEnvelopeID     *string   `gorm:"column:signingEnvelopeId" json:"signingEnvelopeId"`
	SigningDocumentID     *int      `gorm:"column:signingDocumentId" json:"signingDocumentId"`
	SignedAt              *DateTime `gorm:"column:signedAt" json:"signedAt"`
	CompletedAt           *DateTime `gorm:"column:completedAt" json:"completedAt"`
	SignedFileKey         *string   `gorm:"column:signedFileKey" json:"signedFileKey"`
	SignedFileName        *string   `gorm:"column:signedFileName" json:"signedFileName"`
	SignedFileStorageType *string   `gorm:"column:signedFileStorageType" json:"signedFileStorageType"`
	CreatedAt             DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt             DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Agreement             Agreement
	View                  View
}

func (AgreementResponse) TableName() string {
	return "AgreementResponse"
}

func (m *AgreementResponse) BeforeCreate(tx *gorm.DB) error {
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

func (*AgreementResponse) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type IncomingWebhook struct {
	ID                  string    `gorm:"column:id;primaryKey" json:"id"`
	ExternalID          string    `gorm:"column:externalId;uniqueIndex;not null" json:"externalId"`
	Name                string    `gorm:"column:name;not null" json:"name"`
	Secret              *string   `gorm:"column:secret" json:"secret"`
	Source              *string   `gorm:"column:source" json:"source"`
	Actions             *string   `gorm:"column:actions" json:"actions"`
	ConsecutiveFailures int       `gorm:"column:consecutiveFailures;default:0;not null" json:"consecutiveFailures"`
	LastFailedAt        *DateTime `gorm:"column:lastFailedAt" json:"lastFailedAt"`
	DisabledAt          *DateTime `gorm:"column:disabledAt" json:"disabledAt"`
	TeamID              string    `gorm:"column:teamId;index:IncomingWebhook_teamId_idx;not null" json:"teamId"`
	CreatedAt           DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt           DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Team                Team
}

func (IncomingWebhook) TableName() string {
	return "IncomingWebhook"
}

func (m *IncomingWebhook) BeforeCreate(tx *gorm.DB) error {
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

func (*IncomingWebhook) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type RestrictedToken struct {
	ID          string    `gorm:"column:id;primaryKey" json:"id"`
	Name        string    `gorm:"column:name;not null" json:"name"`
	HashedKey   string    `gorm:"column:hashedKey;uniqueIndex;index:RestrictedToken_hashedKey_idx;not null" json:"hashedKey"`
	PartialKey  string    `gorm:"column:partialKey;not null" json:"partialKey"`
	Scopes      *string   `gorm:"column:scopes" json:"scopes"`
	Mode        *string   `gorm:"column:mode;default:'live';not null" json:"mode"`
	Source      *string   `gorm:"column:source;default:'dashboard';not null" json:"source"`
	SubjectType *string   `gorm:"column:subjectType;default:'user';not null" json:"subjectType"`
	ClientID    *string   `gorm:"column:clientId;index:RestrictedToken_clientId_idx" json:"clientId"`
	Expires     *DateTime `gorm:"column:expires" json:"expires"`
	LastUsed    *DateTime `gorm:"column:lastUsed" json:"lastUsed"`
	RateLimit   *int      `gorm:"column:rateLimit;default:60;not null" json:"rateLimit"`
	UserID      string    `gorm:"column:userId;index:RestrictedToken_userId_idx;not null" json:"userId"`
	TeamID      string    `gorm:"column:teamId;index:RestrictedToken_teamId_idx;not null" json:"teamId"`
	CreatedAt   DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt   DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	User        User
	Team        Team
	OauthClient OAuthClient `gorm:"foreignKey:ClientID"`
}

func (RestrictedToken) TableName() string {
	return "RestrictedToken"
}

func (m *RestrictedToken) BeforeCreate(tx *gorm.DB) error {
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

func (*RestrictedToken) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Webhook struct {
	ID        string         `gorm:"column:id;primaryKey" json:"id"`
	PID       string         `gorm:"column:pId;uniqueIndex;not null" json:"pId"`
	Name      string         `gorm:"column:name;not null" json:"name"`
	URL       string         `gorm:"column:url;not null" json:"url"`
	Secret    string         `gorm:"column:secret;not null" json:"secret"`
	Triggers  datatypes.JSON `gorm:"column:triggers;not null" json:"triggers"`
	TeamID    string         `gorm:"column:teamId;index:Webhook_teamId_idx;not null" json:"teamId"`
	CreatedAt DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Team      Team
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

type YearInReview struct {
	ID            string         `gorm:"column:id;primaryKey" json:"id"`
	TeamID        string         `gorm:"column:teamId;index:YearInReview_teamId_idx;not null" json:"teamId"`
	Status        *string        `gorm:"column:status;index:YearInReview_status_attempts_idx;default:'pending';not null" json:"status"`
	Attempts      int            `gorm:"column:attempts;index:YearInReview_status_attempts_idx;default:0;not null" json:"attempts"`
	LastAttempted *DateTime      `gorm:"column:lastAttempted" json:"lastAttempted"`
	Error         *string        `gorm:"column:error" json:"error"`
	Stats         datatypes.JSON `gorm:"column:stats;not null" json:"stats"`
	CreatedAt     DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt     DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
}

func (YearInReview) TableName() string {
	return "YearInReview"
}

func (m *YearInReview) BeforeCreate(tx *gorm.DB) error {
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

func (*YearInReview) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Tag struct {
	ID          string   `gorm:"column:id;primaryKey;index:Tag_id_idx" json:"id"`
	Name        string   `gorm:"column:name;uniqueIndex:Tag_teamId_name_key;index:Tag_name_idx;not null" json:"name"`
	Color       string   `gorm:"column:color;not null" json:"color"`
	Description *string  `gorm:"column:description" json:"description"`
	TeamID      string   `gorm:"column:teamId;uniqueIndex:Tag_teamId_name_key;index:Tag_teamId_idx;not null" json:"teamId"`
	CreatedBy   *string  `gorm:"column:createdBy" json:"createdBy"`
	CreatedAt   DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt   DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Team        Team
	Items       []TagItem `gorm:"foreignKey:TagID;constraint:OnDelete:CASCADE"`
}

func (Tag) TableName() string {
	return "Tag"
}

func (m *Tag) BeforeCreate(tx *gorm.DB) error {
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

func (*Tag) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type TagItem struct {
	ID         string   `gorm:"column:id;primaryKey" json:"id"`
	TagID      string   `gorm:"column:tagId;index:TagItem_tagId_linkId_idx;index:TagItem_tagId_documentId_idx;index:TagItem_tagId_dataroomId_idx;not null" json:"tagId"`
	ItemType   string   `gorm:"column:itemType;not null" json:"itemType"`
	LinkID     *string  `gorm:"column:linkId;index:TagItem_tagId_linkId_idx" json:"linkId"`
	DocumentID *string  `gorm:"column:documentId;index:TagItem_tagId_documentId_idx" json:"documentId"`
	DataroomID *string  `gorm:"column:dataroomId;index:TagItem_tagId_dataroomId_idx" json:"dataroomId"`
	TaggedBy   *string  `gorm:"column:taggedBy" json:"taggedBy"`
	CreatedAt  DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt  DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Tag        Tag
	Link       Link
	Document   Document
	Dataroom   Dataroom
}

func (TagItem) TableName() string {
	return "TagItem"
}

func (m *TagItem) BeforeCreate(tx *gorm.DB) error {
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

func (*TagItem) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type ViewerInvitation struct {
	ID            string   `gorm:"column:id;primaryKey" json:"id"`
	ViewerID      string   `gorm:"column:viewerId;index:ViewerInvitation_viewerId_idx;not null" json:"viewerId"`
	LinkID        string   `gorm:"column:linkId;index:ViewerInvitation_linkId_idx;not null" json:"linkId"`
	GroupID       *string  `gorm:"column:groupId;index:ViewerInvitation_groupId_idx" json:"groupId"`
	InvitedBy     string   `gorm:"column:invitedBy;not null" json:"invitedBy"`
	CustomMessage *string  `gorm:"column:customMessage" json:"customMessage"`
	SentAt        DateTime `gorm:"column:sentAt;not null" json:"sentAt"`
	Status        string   `gorm:"column:status;default:'SENT';not null" json:"status"`
	CreatedAt     DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	Viewer        Viewer
	Link          Link
	Group         ViewerGroup `gorm:"foreignKey:GroupID"`
}

func (ViewerInvitation) TableName() string {
	return "ViewerInvitation"
}

func (m *ViewerInvitation) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.SentAt.IsZero() {
		m.SentAt = DateTime{Time: now}
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type Team struct {
	ID                       string                   `gorm:"column:id;primaryKey" json:"id"`
	Name                     string                   `gorm:"column:name;not null" json:"name"`
	Slug                     *string                  `gorm:"column:slug;uniqueIndex" json:"slug"`
	DefaultBrandID           *string                  `gorm:"column:defaultBrandId;index:Team_defaultBrandId_idx" json:"defaultBrandId"`
	Plan                     *string                  `gorm:"column:plan;default:'free';not null" json:"plan"`
	StripeID                 *string                  `gorm:"column:stripeId;uniqueIndex" json:"stripeId"`
	SubscriptionID           *string                  `gorm:"column:subscriptionId;uniqueIndex" json:"subscriptionId"`
	StartsAt                 *DateTime                `gorm:"column:startsAt" json:"startsAt"`
	EndsAt                   *DateTime                `gorm:"column:endsAt" json:"endsAt"`
	PausedAt                 *DateTime                `gorm:"column:pausedAt" json:"pausedAt"`
	PauseStartsAt            *DateTime                `gorm:"column:pauseStartsAt" json:"pauseStartsAt"`
	PauseEndsAt              *DateTime                `gorm:"column:pauseEndsAt" json:"pauseEndsAt"`
	CancelledAt              *DateTime                `gorm:"column:cancelledAt" json:"cancelledAt"`
	TrialStartedAt           *DateTime                `gorm:"column:trialStartedAt" json:"trialStartedAt"`
	TrialEndsAt              *DateTime                `gorm:"column:trialEndsAt" json:"trialEndsAt"`
	TrialReminderRunID       *string                  `gorm:"column:trialReminderRunId" json:"trialReminderRunId"`
	TrialExpiredRunID        *string                  `gorm:"column:trialExpiredRunId" json:"trialExpiredRunId"`
	Limits                   datatypes.JSON           `gorm:"column:limits" json:"limits"`
	EnableExcelAdvancedMode  bool                     `gorm:"column:enableExcelAdvancedMode;default:false;not null" json:"enableExcelAdvancedMode"`
	ReplicateDataroomFolders *bool                    `gorm:"column:replicateDataroomFolders;default:true;not null" json:"replicateDataroomFolders"`
	Timezone                 *string                  `gorm:"column:timezone;default:'Etc/UTC';not null" json:"timezone"`
	SsoEnabled               bool                     `gorm:"column:ssoEnabled;default:false;not null" json:"ssoEnabled"`
	SsoEmailDomain           *string                  `gorm:"column:ssoEmailDomain;uniqueIndex" json:"ssoEmailDomain"`
	SsoEnforcedAt            *DateTime                `gorm:"column:ssoEnforcedAt" json:"ssoEnforcedAt"`
	SurveyData               datatypes.JSON           `gorm:"column:surveyData" json:"surveyData"`
	AgentsEnabled            bool                     `gorm:"column:agentsEnabled;default:false;not null" json:"agentsEnabled"`
	VectorStoreID            *string                  `gorm:"column:vectorStoreId" json:"vectorStoreId"`
	CreatedAt                DateTime                 `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt                DateTime                 `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	IgnoredDomains           []string                 `gorm:"column:ignoredDomains;serializer:json;not null" json:"ignoredDomains"`
	GlobalBlockList          []string                 `gorm:"column:globalBlockList;serializer:json;not null" json:"globalBlockList"`
	ReferralTrack            *string                  `gorm:"column:referralTrack" json:"referralTrack"`
	ReferralTrackSelectedAt  *DateTime                `gorm:"column:referralTrackSelectedAt" json:"referralTrackSelectedAt"`
	ReferralProfile          datatypes.JSON           `gorm:"column:referralProfile" json:"referralProfile"`
	DefaultBrand             Brand                    `gorm:"foreignKey:DefaultBrandID"`
	Users                    []UserTeam               `gorm:"foreignKey:TeamID;constraint:OnDelete:CASCADE"`
	UserDatarooms            []UserDataroom           `gorm:"foreignKey:TeamID;constraint:OnDelete:CASCADE"`
	Documents                []Document               `gorm:"foreignKey:TeamID;constraint:OnDelete:CASCADE"`
	Folders                  []Folder                 `gorm:"foreignKey:TeamID;constraint:OnDelete:CASCADE"`
	Domains                  []Domain                 `gorm:"foreignKey:TeamID;constraint:OnDelete:CASCADE"`
	Invitations              []Invitation             `gorm:"foreignKey:TeamID;constraint:OnDelete:CASCADE"`
	SentEmails               []SentEmail              `gorm:"foreignKey:TeamID;constraint:OnDelete:CASCADE"`
	Brands                   []Brand                  `gorm:"foreignKey:TeamID;constraint:OnDelete:CASCADE"`
	Datarooms                []Dataroom               `gorm:"foreignKey:TeamID;constraint:OnDelete:CASCADE"`
	Agreements               []Agreement              `gorm:"foreignKey:TeamID;constraint:OnDelete:CASCADE"`
	ViewerGroups             []ViewerGroup            `gorm:"foreignKey:TeamID;constraint:OnDelete:CASCADE"`
	VisitorGroups            []VisitorGroup           `gorm:"foreignKey:TeamID;constraint:OnDelete:CASCADE"`
	Viewers                  []Viewer                 `gorm:"foreignKey:TeamID;constraint:OnDelete:CASCADE"`
	PermissionGroups         []PermissionGroup        `gorm:"foreignKey:TeamID;constraint:OnDelete:CASCADE"`
	LinkPresets              []LinkPreset             `gorm:"foreignKey:TeamID;constraint:OnDelete:CASCADE"`
	IncomingWebhooks         []IncomingWebhook        `gorm:"foreignKey:TeamID;constraint:OnDelete:CASCADE"`
	RestrictedTokens         []RestrictedToken        `gorm:"foreignKey:TeamID;constraint:OnDelete:CASCADE"`
	Webhooks                 []Webhook                `gorm:"foreignKey:TeamID;constraint:OnDelete:CASCADE"`
	Conversations            []Conversation           `gorm:"foreignKey:TeamID;constraint:OnDelete:CASCADE"`
	DataroomQuestions        []DataroomQuestion       `gorm:"foreignKey:TeamID;constraint:OnDelete:CASCADE"`
	DataroomFaqItems         []DataroomFaqItem        `gorm:"foreignKey:TeamID;constraint:OnDelete:CASCADE"`
	UploadedDocuments        []DocumentUpload         `gorm:"foreignKey:TeamID;constraint:OnDelete:CASCADE"`
	Tag                      []Tag                    `gorm:"foreignKey:TeamID;constraint:OnDelete:CASCADE"`
	Annotations              []DocumentAnnotation     `gorm:"foreignKey:TeamID;constraint:OnDelete:CASCADE"`
	RedactionJobs            []DocumentRedactionJob   `gorm:"foreignKey:TeamID;constraint:OnDelete:CASCADE"`
	InstalledIntegrations    []InstalledIntegration   `gorm:"foreignKey:TeamID;constraint:OnDelete:CASCADE"`
	Links                    []Link                   `gorm:"foreignKey:TeamID;constraint:OnDelete:CASCADE"`
	Views                    []View                   `gorm:"foreignKey:TeamID;constraint:OnDelete:CASCADE"`
	Workflows                []Workflow               `gorm:"foreignKey:TeamID;constraint:OnDelete:CASCADE"`
	TaskLists                []TaskList               `gorm:"foreignKey:TeamID;constraint:OnDelete:CASCADE"`
	Tasks                    []Task                   `gorm:"foreignKey:TeamID;constraint:OnDelete:CASCADE"`
	Chats                    []Chat                   `gorm:"foreignKey:TeamID;constraint:OnDelete:CASCADE"`
	NotificationPreferences  []NotificationPreference `gorm:"foreignKey:TeamID;constraint:OnDelete:CASCADE"`
	ReferralInvites          []ReferralInvite         `gorm:"foreignKey:TeamID;constraint:OnDelete:CASCADE"`
}

func (Team) TableName() string {
	return "Team"
}

func (m *Team) BeforeCreate(tx *gorm.DB) error {
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

func (*Team) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type UserDataroom struct {
	UserID     string   `gorm:"column:userId;primaryKey;index:UserDataroom_userId_idx" json:"userId"`
	TeamID     string   `gorm:"column:teamId;index:UserDataroom_teamId_idx;not null" json:"teamId"`
	DataroomID string   `gorm:"column:dataroomId;primaryKey;index:UserDataroom_dataroomId_idx" json:"dataroomId"`
	CreatedAt  DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	User       User
	Team       Team
	Dataroom   Dataroom
}

func (UserDataroom) TableName() string {
	return "UserDataroom"
}

func (m *UserDataroom) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type UserTeam struct {
	Role                    string         `gorm:"column:role;default:'MEMBER';not null" json:"role"`
	Status                  *string        `gorm:"column:status;default:'ACTIVE';not null" json:"status"`
	UserID                  string         `gorm:"column:userId;primaryKey;index:UserTeam_userId_idx" json:"userId"`
	TeamID                  string         `gorm:"column:teamId;primaryKey;index:UserTeam_teamId_idx" json:"teamId"`
	BlockedAt               *DateTime      `gorm:"column:blockedAt" json:"blockedAt"`
	NotificationPreferences datatypes.JSON `gorm:"column:notificationPreferences" json:"notificationPreferences"`
	User                    User
	Team                    Team
}

func (UserTeam) TableName() string {
	return "UserTeam"
}

type Workflow struct {
	ID          string   `gorm:"column:id;primaryKey" json:"id"`
	Name        string   `gorm:"column:name;not null" json:"name"`
	Description *string  `gorm:"column:description" json:"description"`
	EntryLinkID string   `gorm:"column:entryLinkId;uniqueIndex;index:Workflow_entryLinkId_idx;not null" json:"entryLinkId"`
	TeamID      string   `gorm:"column:teamId;index:Workflow_teamId_idx;not null" json:"teamId"`
	IsActive    *bool    `gorm:"column:isActive;index:Workflow_isActive_idx;default:true;not null" json:"isActive"`
	CreatedAt   DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt   DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	EntryLink   Link     `gorm:"foreignKey:EntryLinkID"`
	Team        Team
	Steps       []WorkflowStep      `gorm:"foreignKey:WorkflowID;constraint:OnDelete:CASCADE"`
	Executions  []WorkflowExecution `gorm:"foreignKey:WorkflowID;constraint:OnDelete:CASCADE"`
}

func (Workflow) TableName() string {
	return "Workflow"
}

func (m *Workflow) BeforeCreate(tx *gorm.DB) error {
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

func (*Workflow) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type WorkflowStep struct {
	ID            string         `gorm:"column:id;primaryKey" json:"id"`
	WorkflowID    string         `gorm:"column:workflowId;uniqueIndex:WorkflowStep_workflowId_stepOrder_key;index:WorkflowStep_workflowId_idx;not null" json:"workflowId"`
	Name          string         `gorm:"column:name;not null" json:"name"`
	StepOrder     int            `gorm:"column:stepOrder;uniqueIndex:WorkflowStep_workflowId_stepOrder_key;not null" json:"stepOrder"`
	StepType      string         `gorm:"column:stepType;default:'ROUTER';not null" json:"stepType"`
	Conditions    datatypes.JSON `gorm:"column:conditions;not null" json:"conditions"`
	Actions       datatypes.JSON `gorm:"column:actions;not null" json:"actions"`
	CreatedAt     DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt     DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Workflow      Workflow
	ExecutionLogs []WorkflowStepLog `gorm:"foreignKey:WorkflowStepID;constraint:OnDelete:CASCADE"`
}

func (WorkflowStep) TableName() string {
	return "WorkflowStep"
}

func (m *WorkflowStep) BeforeCreate(tx *gorm.DB) error {
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

func (*WorkflowStep) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type WorkflowExecution struct {
	ID           string         `gorm:"column:id;primaryKey" json:"id"`
	WorkflowID   string         `gorm:"column:workflowId;index:WorkflowExecution_workflowId_startedAt_idx;not null" json:"workflowId"`
	VisitorEmail *string        `gorm:"column:visitorEmail;index:WorkflowExecution_visitorEmail_idx" json:"visitorEmail"`
	VisitorIP    *string        `gorm:"column:visitorIp" json:"visitorIp"`
	Status       string         `gorm:"column:status;index:WorkflowExecution_status_idx;not null" json:"status"`
	StartedAt    DateTime       `gorm:"column:startedAt;index:WorkflowExecution_workflowId_startedAt_idx;not null" json:"startedAt"`
	CompletedAt  *DateTime      `gorm:"column:completedAt" json:"completedAt"`
	Result       datatypes.JSON `gorm:"column:result" json:"result"`
	Metadata     datatypes.JSON `gorm:"column:metadata" json:"metadata"`
	Workflow     Workflow
	StepLogs     []WorkflowStepLog `gorm:"foreignKey:ExecutionID;constraint:OnDelete:CASCADE"`
}

func (WorkflowExecution) TableName() string {
	return "WorkflowExecution"
}

func (m *WorkflowExecution) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.StartedAt.IsZero() {
		m.StartedAt = DateTime{Time: now}
	}
	return nil
}

type WorkflowStepLog struct {
	ID                string            `gorm:"column:id;primaryKey" json:"id"`
	ExecutionID       string            `gorm:"column:executionId;index:WorkflowStepLog_executionId_idx;not null" json:"executionId"`
	WorkflowStepID    string            `gorm:"column:workflowStepId;index:WorkflowStepLog_workflowStepId_idx;not null" json:"workflowStepId"`
	ConditionsMatched bool              `gorm:"column:conditionsMatched;not null" json:"conditionsMatched"`
	ConditionResults  datatypes.JSON    `gorm:"column:conditionResults" json:"conditionResults"`
	ActionsExecuted   datatypes.JSON    `gorm:"column:actionsExecuted" json:"actionsExecuted"`
	ExecutedAt        DateTime          `gorm:"column:executedAt;not null" json:"executedAt"`
	Duration          *int              `gorm:"column:duration" json:"duration"`
	Error             *string           `gorm:"column:error" json:"error"`
	Execution         WorkflowExecution `gorm:"foreignKey:ExecutionID"`
	Step              WorkflowStep      `gorm:"foreignKey:WorkflowStepID"`
}

func (WorkflowStepLog) TableName() string {
	return "WorkflowStepLog"
}

func (m *WorkflowStepLog) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.ExecutedAt.IsZero() {
		m.ExecutedAt = DateTime{Time: now}
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
