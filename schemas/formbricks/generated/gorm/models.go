package model

import (
	"database/sql/driver"
	"fmt"
	"strconv"
	"time"

	"github.com/google/uuid"
	"github.com/lucsky/cuid"
	"github.com/nrednav/cuid2"
	"gorm.io/datatypes"
	"gorm.io/gorm"
)

type Creative struct {
	ID                 string         `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt          DateTime       `gorm:"column:created_at;index:Creative_workspaceId_created_at_idx;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt          DateTime       `gorm:"column:updated_at;index:Creative_workspaceId_updated_at_idx;autoUpdateTime:false;not null" json:"updated_at"`
	Name               string         `gorm:"column:name;index:Creative_workspaceId_name_idx;not null" json:"name"`
	Type               string         `gorm:"column:type;not null" json:"type"`
	Status             string         `gorm:"column:status;default:'draft';not null" json:"status"`
	PublishedAt        *DateTime      `gorm:"column:publishedAt" json:"publishedAt"`
	ArchivedAt         *DateTime      `gorm:"column:archivedAt" json:"archivedAt"`
	WorkspaceID        string         `gorm:"column:workspaceId;index:Creative_workspaceId_updated_at_idx;index:Creative_workspaceId_created_at_idx;index:Creative_workspaceId_name_idx;not null" json:"workspaceId"`
	CreatedBy          *string        `gorm:"column:createdBy" json:"createdBy"`
	Content            datatypes.JSON `gorm:"column:content;not null" json:"content"`
	Styling            datatypes.JSON `gorm:"column:styling;not null" json:"styling"`
	SurveyID           *string        `gorm:"column:surveyId;index:Creative_surveyId_idx" json:"surveyId"`
	LinkURL            *string        `gorm:"column:linkUrl" json:"linkUrl"`
	ShowOn             string         `gorm:"column:showOn;default:'pageView';not null" json:"showOn"`
	ShowConditions     datatypes.JSON `gorm:"column:showConditions" json:"showConditions"`
	DisplayPercentage  *float64       `gorm:"column:displayPercentage" json:"displayPercentage"`
	StartAt            *DateTime      `gorm:"column:startAt" json:"startAt"`
	EndAt              *DateTime      `gorm:"column:endAt" json:"endAt"`
	AutoSelectLanguage *bool          `gorm:"column:autoSelectLanguage" json:"autoSelectLanguage"`
	Workspace          *Workspace
	Creator            *User                   `gorm:"foreignKey:CreatedBy"`
	Survey             *Survey                 `gorm:"foreignKey:SurveyID,WorkspaceID;references:ID,WorkspaceID"`
	Triggers           []CreativeTrigger       `gorm:"foreignKey:CreativeID;constraint:OnDelete:CASCADE"`
	Languages          []CreativeLanguage      `gorm:"foreignKey:CreativeID;constraint:OnDelete:CASCADE"`
	DailyStats         []CreativeDailyStat     `gorm:"foreignKey:CreativeID;constraint:OnDelete:CASCADE"`
	DailyPageStats     []CreativeDailyPageStat `gorm:"foreignKey:CreativeID;constraint:OnDelete:CASCADE"`
}

func (Creative) TableName() string {
	return "Creative"
}

func (m *Creative) BeforeCreate(tx *gorm.DB) error {
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

func (*Creative) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type CreativeDailyStat struct {
	CreativeID  string `gorm:"column:creativeId;primaryKey" json:"creativeId"`
	Date        Date   `gorm:"column:date;primaryKey;type:date" json:"date"`
	DeviceType  string `gorm:"column:deviceType;primaryKey" json:"deviceType"`
	Impressions int    `gorm:"column:impressions;default:0;not null" json:"impressions"`
	Clicks      int    `gorm:"column:clicks;default:0;not null" json:"clicks"`
	Creative    *Creative
}

func (CreativeDailyStat) TableName() string {
	return "CreativeDailyStat"
}

type CreativeDailyPageStat struct {
	CreativeID  string `gorm:"column:creativeId;primaryKey" json:"creativeId"`
	Date        Date   `gorm:"column:date;primaryKey;type:date" json:"date"`
	Page        string `gorm:"column:page;primaryKey" json:"page"`
	Impressions int    `gorm:"column:impressions;default:0;not null" json:"impressions"`
	Clicks      int    `gorm:"column:clicks;default:0;not null" json:"clicks"`
	Creative    *Creative
}

func (CreativeDailyPageStat) TableName() string {
	return "CreativeDailyPageStat"
}

type CreativeTrigger struct {
	ID            string   `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt     DateTime `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt     DateTime `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	CreativeID    string   `gorm:"column:creativeId;uniqueIndex:CreativeTrigger_creativeId_actionClassId_key;not null" json:"creativeId"`
	ActionClassID string   `gorm:"column:actionClassId;uniqueIndex:CreativeTrigger_creativeId_actionClassId_key;index:CreativeTrigger_actionClassId_idx;not null" json:"actionClassId"`
	Creative      *Creative
	ActionClass   *ActionClass
}

func (CreativeTrigger) TableName() string {
	return "CreativeTrigger"
}

func (m *CreativeTrigger) BeforeCreate(tx *gorm.DB) error {
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

func (*CreativeTrigger) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type CreativeLanguage struct {
	LanguageID string `gorm:"column:languageId;primaryKey" json:"languageId"`
	CreativeID string `gorm:"column:creativeId;primaryKey;index:CreativeLanguage_creativeId_idx" json:"creativeId"`
	Default    bool   `gorm:"column:default;default:false;not null" json:"default"`
	Enabled    *bool  `gorm:"column:enabled;default:true;not null" json:"enabled"`
	Language   *Language
	Creative   *Creative
}

func (CreativeLanguage) TableName() string {
	return "CreativeLanguage"
}

type OutboundEmailAudit struct {
	ID                    string    `gorm:"column:id;primaryKey;type:char(36);index:OutboundEmailAudit_environment_createdAt_id_idx;index:OutboundEmailAudit_organizationId_createdAt_id_idx;index:OutboundEmailAudit_surveyId_createdAt_id_idx;index:OutboundEmailAudit_responseId_createdAt_id_idx;index:OutboundEmailAudit_actorUserId_createdAt_id_idx;index:OutboundEmailAudit_configuredByUserId_createdAt_id_idx;index:OutboundEmailAudit_actorUserHash_createdAt_id_idx;index:OutboundEmailAudit_configuredByUserHash_createdAt_id_idx;index:OutboundEmailAudit_recipientMac_createdAt_id_idx;index:OutboundEmailAudit_status_createdAt_id_idx;index:OutboundEmailAudit_contentExpiresAt_id_idx;index:OutboundEmailAudit_expiresAt_id_idx;index:OutboundEmailAudit_erasureRequestedAt_id_idx;index:OutboundEmailAudit_contentDeleteRetryAt_id_idx" json:"id"`
	CreatedAt             DateTime  `gorm:"column:createdAt;index:OutboundEmailAudit_environment_createdAt_id_idx;index:OutboundEmailAudit_organizationId_createdAt_id_idx;index:OutboundEmailAudit_surveyId_createdAt_id_idx;index:OutboundEmailAudit_responseId_createdAt_id_idx;index:OutboundEmailAudit_actorUserId_createdAt_id_idx;index:OutboundEmailAudit_configuredByUserId_createdAt_id_idx;index:OutboundEmailAudit_actorUserHash_createdAt_id_idx;index:OutboundEmailAudit_configuredByUserHash_createdAt_id_idx;index:OutboundEmailAudit_recipientMac_createdAt_id_idx;index:OutboundEmailAudit_status_createdAt_id_idx;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt             DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	ExpiresAt             DateTime  `gorm:"column:expiresAt;index:OutboundEmailAudit_expiresAt_id_idx;not null" json:"expiresAt"`
	ContentExpiresAt      DateTime  `gorm:"column:contentExpiresAt;index:OutboundEmailAudit_contentExpiresAt_id_idx;not null" json:"contentExpiresAt"`
	Environment           string    `gorm:"column:environment;index:OutboundEmailAudit_environment_createdAt_id_idx;not null" json:"environment"`
	EmailType             string    `gorm:"column:emailType;not null" json:"emailType"`
	Trigger               string    `gorm:"column:trigger;not null" json:"trigger"`
	ActorKind             string    `gorm:"column:actorKind;not null" json:"actorKind"`
	ActorUserID           *string   `gorm:"column:actorUserId;index:OutboundEmailAudit_actorUserId_createdAt_id_idx" json:"actorUserId"`
	ConfiguredByUserID    *string   `gorm:"column:configuredByUserId;index:OutboundEmailAudit_configuredByUserId_createdAt_id_idx" json:"configuredByUserId"`
	ActorUserHash         *string   `gorm:"column:actorUserHash;index:OutboundEmailAudit_actorUserHash_createdAt_id_idx" json:"actorUserHash"`
	ConfiguredByUserHash  *string   `gorm:"column:configuredByUserHash;index:OutboundEmailAudit_configuredByUserHash_createdAt_id_idx" json:"configuredByUserHash"`
	OrganizationID        *string   `gorm:"column:organizationId;index:OutboundEmailAudit_organizationId_createdAt_id_idx" json:"organizationId"`
	SurveyID              *string   `gorm:"column:surveyId;index:OutboundEmailAudit_surveyId_createdAt_id_idx" json:"surveyId"`
	ResponseID            *string   `gorm:"column:responseId;index:OutboundEmailAudit_responseId_createdAt_id_idx" json:"responseId"`
	FollowUpID            *string   `gorm:"column:followUpId" json:"followUpId"`
	WorkflowID            *string   `gorm:"column:workflowId" json:"workflowId"`
	WorkflowRunID         *string   `gorm:"column:workflowRunId" json:"workflowRunId"`
	InviteID              *string   `gorm:"column:inviteId" json:"inviteId"`
	RecipientMac          *string   `gorm:"column:recipientMac;index:OutboundEmailAudit_recipientMac_createdAt_id_idx" json:"recipientMac"`
	SesMessageHash        *string   `gorm:"column:sesMessageHash;index:OutboundEmailAudit_sesMessageHash_idx" json:"sesMessageHash"`
	SnapshotKey           *string   `gorm:"column:snapshotKey" json:"snapshotKey"`
	SnapshotSha256        *string   `gorm:"column:snapshotSha256" json:"snapshotSha256"`
	Status                *string   `gorm:"column:status;index:OutboundEmailAudit_status_createdAt_id_idx;default:'prepared';not null" json:"status"`
	StatusRecordedAt      *DateTime `gorm:"column:statusRecordedAt" json:"statusRecordedAt"`
	AuditGap              bool      `gorm:"column:auditGap;default:false;not null" json:"auditGap"`
	ErasureRequestedAt    *DateTime `gorm:"column:erasureRequestedAt;index:OutboundEmailAudit_erasureRequestedAt_id_idx" json:"erasureRequestedAt"`
	ErasedAt              *DateTime `gorm:"column:erasedAt" json:"erasedAt"`
	ContentDeleteRetryAt  *DateTime `gorm:"column:contentDeleteRetryAt;index:OutboundEmailAudit_contentDeleteRetryAt_id_idx" json:"contentDeleteRetryAt"`
	ContentDeleteFailures int       `gorm:"column:contentDeleteFailures;default:0;not null" json:"contentDeleteFailures"`
}

func (OutboundEmailAudit) TableName() string {
	return "OutboundEmailAudit"
}

func (m *OutboundEmailAudit) BeforeCreate(tx *gorm.DB) error {
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

func (*OutboundEmailAudit) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type OutboundEmailAuditErasureIntent struct {
	ID           string    `gorm:"column:id;primaryKey;type:char(36);index:OutboundEmailAuditErasureIntent_checkedAt_id_idx" json:"id"`
	UserID       *string   `gorm:"column:userId;index:OutboundEmailAuditErasureIntent_userId_idx" json:"userId"`
	UserHash     string    `gorm:"column:userHash;index:OutboundEmailAuditErasureIntent_userHash_activatedAt_idx;not null" json:"userHash"`
	RecipientMac string    `gorm:"column:recipientMac;index:OutboundEmailAuditErasureIntent_recipientMac_activatedAt_idx;not null" json:"recipientMac"`
	CreatedAt    DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	ActivatedAt  *DateTime `gorm:"column:activatedAt;index:OutboundEmailAuditErasureIntent_recipientMac_activatedAt_idx;index:OutboundEmailAuditErasureIntent_userHash_activatedAt_idx" json:"activatedAt"`
	CheckedAt    *DateTime `gorm:"column:checkedAt;index:OutboundEmailAuditErasureIntent_checkedAt_id_idx" json:"checkedAt"`
	ExpiresAt    DateTime  `gorm:"column:expiresAt;index:OutboundEmailAuditErasureIntent_expiresAt_idx;not null" json:"expiresAt"`
}

func (OutboundEmailAuditErasureIntent) TableName() string {
	return "OutboundEmailAuditErasureIntent"
}

func (m *OutboundEmailAuditErasureIntent) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = uuid.NewString()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type Webhook struct {
	ID          string   `gorm:"column:id;primaryKey" json:"id"`
	Name        *string  `gorm:"column:name" json:"name"`
	CreatedAt   DateTime `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt   DateTime `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	URL         string   `gorm:"column:url;not null" json:"url"`
	Source      string   `gorm:"column:source;default:'user';not null" json:"source"`
	WorkspaceID string   `gorm:"column:workspaceId;index:Webhook_workspaceId_idx;not null" json:"workspaceId"`
	Triggers    []string `gorm:"column:triggers;serializer:json;not null" json:"triggers"`
	SurveyIds   []string `gorm:"column:surveyIds;serializer:json;not null" json:"surveyIds"`
	Secret      *string  `gorm:"column:secret" json:"secret"`
	Workspace   *Workspace
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

type ContactAttribute struct {
	ID             string               `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt      DateTime             `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt      DateTime             `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	AttributeKeyID string               `gorm:"column:attributeKeyId;uniqueIndex:ContactAttribute_contactId_attributeKeyId_key;index:ContactAttribute_attributeKeyId_value_idx;index:ContactAttribute_attributeKeyId_valueNumber_idx;index:ContactAttribute_attributeKeyId_valueDate_idx;not null" json:"attributeKeyId"`
	ContactID      string               `gorm:"column:contactId;uniqueIndex:ContactAttribute_contactId_attributeKeyId_key;not null" json:"contactId"`
	Value          string               `gorm:"column:value;index:ContactAttribute_attributeKeyId_value_idx;not null" json:"value"`
	ValueNumber    *float64             `gorm:"column:valueNumber;index:ContactAttribute_attributeKeyId_valueNumber_idx" json:"valueNumber"`
	ValueDate      *DateTime            `gorm:"column:valueDate;index:ContactAttribute_attributeKeyId_valueDate_idx" json:"valueDate"`
	AttributeKey   *ContactAttributeKey `gorm:"foreignKey:AttributeKeyID"`
	Contact        *Contact
}

func (ContactAttribute) TableName() string {
	return "ContactAttribute"
}

func (m *ContactAttribute) BeforeCreate(tx *gorm.DB) error {
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

func (*ContactAttribute) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type ContactAttributeKey struct {
	ID               string   `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt        DateTime `gorm:"column:created_at;index:ContactAttributeKey_workspaceId_created_at_idx;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt        DateTime `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	IsUnique         bool     `gorm:"column:isUnique;default:false;not null" json:"isUnique"`
	Key              string   `gorm:"column:key;uniqueIndex:ContactAttributeKey_key_workspaceId_key;not null" json:"key"`
	Name             *string  `gorm:"column:name" json:"name"`
	Description      *string  `gorm:"column:description" json:"description"`
	Type             string   `gorm:"column:type;default:'custom';not null" json:"type"`
	DataType         string   `gorm:"column:dataType;default:'string';not null" json:"dataType"`
	WorkspaceID      string   `gorm:"column:workspaceId;uniqueIndex:ContactAttributeKey_key_workspaceId_key;index:ContactAttributeKey_workspaceId_created_at_idx;not null" json:"workspaceId"`
	Workspace        *Workspace
	Attributes       []ContactAttribute      `gorm:"foreignKey:AttributeKeyID;constraint:OnDelete:CASCADE"`
	AttributeFilters []SurveyAttributeFilter `gorm:"foreignKey:AttributeKeyID;constraint:OnDelete:CASCADE"`
}

func (ContactAttributeKey) TableName() string {
	return "ContactAttributeKey"
}

func (m *ContactAttributeKey) BeforeCreate(tx *gorm.DB) error {
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

func (*ContactAttributeKey) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Contact struct {
	ID          string   `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt   DateTime `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt   DateTime `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	WorkspaceID string   `gorm:"column:workspaceId;index:Contact_workspaceId_idx;not null" json:"workspaceId"`
	Workspace   *Workspace
	Responses   []Response         `gorm:"foreignKey:ContactID;constraint:OnDelete:CASCADE"`
	Attributes  []ContactAttribute `gorm:"foreignKey:ContactID;constraint:OnDelete:CASCADE"`
	Displays    []Display          `gorm:"foreignKey:ContactID;constraint:OnDelete:CASCADE"`
}

func (Contact) TableName() string {
	return "Contact"
}

func (m *Contact) BeforeCreate(tx *gorm.DB) error {
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

func (*Contact) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Response struct {
	ID                string         `gorm:"column:id;primaryKey;index:Response_created_at_id_idx;index:Response_surveyId_created_at_id_idx;index:Response_contactId_created_at_id_idx" json:"id"`
	CreatedAt         DateTime       `gorm:"column:created_at;index:Response_created_at_idx;index:Response_created_at_id_idx;index:Response_surveyId_created_at_idx;index:Response_surveyId_created_at_id_idx;index:Response_contactId_created_at_idx;index:Response_contactId_created_at_id_idx;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt         DateTime       `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	Finished          bool           `gorm:"column:finished;index:Response_surveyId_finished_idx;default:false;not null" json:"finished"`
	SurveyID          string         `gorm:"column:surveyId;uniqueIndex:Response_surveyId_singleUseId_key;index:Response_surveyId_created_at_idx;index:Response_surveyId_created_at_id_idx;index:Response_surveyId_finished_idx;not null" json:"surveyId"`
	ContactID         *string        `gorm:"column:contactId;index:Response_contactId_created_at_idx;index:Response_contactId_created_at_id_idx" json:"contactId"`
	EndingID          *string        `gorm:"column:endingId" json:"endingId"`
	Data              datatypes.JSON `gorm:"column:data;default:'{}';not null" json:"data"`
	Variables         datatypes.JSON `gorm:"column:variables;default:'{}';not null" json:"variables"`
	Ttc               datatypes.JSON `gorm:"column:ttc;default:'{}';not null" json:"ttc"`
	Meta              datatypes.JSON `gorm:"column:meta;default:'{}';not null" json:"meta"`
	IngestFlags       datatypes.JSON `gorm:"column:ingest_flags" json:"ingest_flags"`
	Outcome           *string        `gorm:"column:outcome" json:"outcome"`
	NotShown          datatypes.JSON `gorm:"column:not_shown" json:"not_shown"`
	ContactAttributes datatypes.JSON `gorm:"column:contactAttributes" json:"contactAttributes"`
	SingleUseID       *string        `gorm:"column:singleUseId;uniqueIndex:Response_surveyId_singleUseId_key" json:"singleUseId"`
	Language          *string        `gorm:"column:language" json:"language"`
	DisplayID         *string        `gorm:"column:displayId;uniqueIndex" json:"displayId"`
	Survey            *Survey
	Contact           *Contact
	Display           *Display
	Tags              []TagsOnResponses   `gorm:"foreignKey:ResponseID;constraint:OnDelete:CASCADE"`
	QuotaLinks        []ResponseQuotaLink `gorm:"foreignKey:ResponseID;constraint:OnDelete:CASCADE"`
	WorkflowRuns      []WorkflowRun       `gorm:"foreignKey:ResponseID;constraint:OnDelete:CASCADE"`
}

func (Response) TableName() string {
	return "Response"
}

func (m *Response) BeforeCreate(tx *gorm.DB) error {
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

func (*Response) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Tag struct {
	ID          string   `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt   DateTime `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt   DateTime `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	Name        string   `gorm:"column:name;uniqueIndex:Tag_workspaceId_name_key;not null" json:"name"`
	WorkspaceID string   `gorm:"column:workspaceId;uniqueIndex:Tag_workspaceId_name_key;index:Tag_workspaceId_idx;not null" json:"workspaceId"`
	Workspace   *Workspace
	Responses   []TagsOnResponses `gorm:"foreignKey:TagID;constraint:OnDelete:CASCADE"`
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

type TagsOnResponses struct {
	ResponseID string `gorm:"column:responseId;primaryKey" json:"responseId"`
	TagID      string `gorm:"column:tagId;primaryKey" json:"tagId"`
	Response   *Response
	Tag        *Tag
}

func (TagsOnResponses) TableName() string {
	return "TagsOnResponses"
}

type Display struct {
	ID         string   `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt  DateTime `gorm:"column:created_at;index:Display_contactId_created_at_idx;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt  DateTime `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	SurveyID   string   `gorm:"column:surveyId;index:Display_surveyId_idx;not null" json:"surveyId"`
	ContactID  *string  `gorm:"column:contactId;index:Display_contactId_created_at_idx" json:"contactId"`
	CreativeID *string  `gorm:"column:creativeId" json:"creativeId"`
	Survey     *Survey
	Contact    *Contact
	Response   *Response `gorm:"foreignKey:DisplayID"`
}

func (Display) TableName() string {
	return "Display"
}

func (m *Display) BeforeCreate(tx *gorm.DB) error {
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

func (*Display) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type SurveyTrigger struct {
	ID            string   `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt     DateTime `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt     DateTime `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	SurveyID      string   `gorm:"column:surveyId;uniqueIndex:SurveyTrigger_surveyId_actionClassId_key;not null" json:"surveyId"`
	ActionClassID string   `gorm:"column:actionClassId;uniqueIndex:SurveyTrigger_surveyId_actionClassId_key;not null" json:"actionClassId"`
	Survey        *Survey
	ActionClass   *ActionClass
}

func (SurveyTrigger) TableName() string {
	return "SurveyTrigger"
}

func (m *SurveyTrigger) BeforeCreate(tx *gorm.DB) error {
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

func (*SurveyTrigger) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type SurveyAttributeFilter struct {
	ID             string               `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt      DateTime             `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt      DateTime             `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	AttributeKeyID string               `gorm:"column:attributeKeyId;uniqueIndex:SurveyAttributeFilter_surveyId_attributeKeyId_key;index:SurveyAttributeFilter_attributeKeyId_idx;not null" json:"attributeKeyId"`
	SurveyID       string               `gorm:"column:surveyId;uniqueIndex:SurveyAttributeFilter_surveyId_attributeKeyId_key;not null" json:"surveyId"`
	Condition      string               `gorm:"column:condition;not null" json:"condition"`
	Value          string               `gorm:"column:value;not null" json:"value"`
	AttributeKey   *ContactAttributeKey `gorm:"foreignKey:AttributeKeyID"`
	Survey         *Survey
}

func (SurveyAttributeFilter) TableName() string {
	return "SurveyAttributeFilter"
}

func (m *SurveyAttributeFilter) BeforeCreate(tx *gorm.DB) error {
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

func (*SurveyAttributeFilter) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Survey struct {
	HiddenFields                datatypes.JSON   `gorm:"column:hiddenFields;default:'{\"enabled\": false}';not null" json:"hiddenFields"`
	Variables                   datatypes.JSON   `gorm:"column:variables;default:'[]';not null" json:"variables"`
	OwnershipInitialized        bool             `gorm:"column:ownershipInitialized;default:false;not null" json:"ownershipInitialized"`
	ID                          string           `gorm:"column:id;primaryKey;uniqueIndex:Survey_id_workspaceId_key" json:"id"`
	CreatedAt                   DateTime         `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt                   DateTime         `gorm:"column:updated_at;index:Survey_workspaceId_updated_at_idx;autoUpdateTime:false;not null" json:"updated_at"`
	Name                        string           `gorm:"column:name;not null" json:"name"`
	RedirectURL                 *string          `gorm:"column:redirectUrl" json:"redirectUrl"`
	Type                        string           `gorm:"column:type;default:'app';not null" json:"type"`
	WorkspaceID                 string           `gorm:"column:workspaceId;uniqueIndex:Survey_id_workspaceId_key;index:Survey_workspaceId_updated_at_idx;index:Survey_workspaceId_ownerId_idx;not null" json:"workspaceId"`
	CreatedBy                   *string          `gorm:"column:createdBy" json:"createdBy"`
	Visibility                  string           `gorm:"column:visibility;default:'workspace';not null" json:"visibility"`
	OwnerID                     *string          `gorm:"column:ownerId;index:Survey_workspaceId_ownerId_idx" json:"ownerId"`
	VisibilityVersion           int              `gorm:"column:visibilityVersion;default:0;not null" json:"visibilityVersion"`
	VisibilityProjectedVersion  int              `gorm:"column:visibilityProjectedVersion;default:0;not null" json:"visibilityProjectedVersion"`
	VisibilityPending           bool             `gorm:"column:visibilityPending;default:false;not null" json:"visibilityPending"`
	VisibilityChangedAt         *DateTime        `gorm:"column:visibilityChangedAt" json:"visibilityChangedAt"`
	VisibilityChangedByID       *string          `gorm:"column:visibilityChangedById" json:"visibilityChangedById"`
	Status                      string           `gorm:"column:status;index:Survey_status_publishOn_idx;index:Survey_status_closeOn_idx;default:'draft';not null" json:"status"`
	WelcomeCard                 datatypes.JSON   `gorm:"column:welcomeCard;default:'{\"enabled\": false}';not null" json:"welcomeCard"`
	Questions                   datatypes.JSON   `gorm:"column:questions;default:'[]';not null" json:"questions"`
	Blocks                      []datatypes.JSON `gorm:"column:blocks;serializer:json;not null" json:"blocks"`
	Endings                     []datatypes.JSON `gorm:"column:endings;serializer:json;not null" json:"endings"`
	DisplayOption               string           `gorm:"column:displayOption;default:'displayOnce';not null" json:"displayOption"`
	RecontactDays               *int             `gorm:"column:recontactDays" json:"recontactDays"`
	DisplayLimit                *int             `gorm:"column:displayLimit" json:"displayLimit"`
	InlineTriggers              datatypes.JSON   `gorm:"column:inlineTriggers" json:"inlineTriggers"`
	AutoClose                   *int             `gorm:"column:autoClose" json:"autoClose"`
	KeepEndingCardOpen          bool             `gorm:"column:keepEndingCardOpen;default:false;not null" json:"keepEndingCardOpen"`
	AutoComplete                *int             `gorm:"column:autoComplete" json:"autoComplete"`
	Delay                       int              `gorm:"column:delay;default:0;not null" json:"delay"`
	PublishOn                   *DateTime        `gorm:"column:publishOn;index:Survey_status_publishOn_idx" json:"publishOn"`
	CloseOn                     *DateTime        `gorm:"column:closeOn;index:Survey_status_closeOn_idx" json:"closeOn"`
	ArchivedAt                  *DateTime        `gorm:"column:archivedAt;index:Survey_archivedAt_idx" json:"archivedAt"`
	SurveyClosedMessage         datatypes.JSON   `gorm:"column:surveyClosedMessage" json:"surveyClosedMessage"`
	SegmentID                   *string          `gorm:"column:segmentId;index:Survey_segmentId_idx" json:"segmentId"`
	WorkspaceOverwrites         datatypes.JSON   `gorm:"column:workspaceOverwrites" json:"workspaceOverwrites"`
	Styling                     datatypes.JSON   `gorm:"column:styling" json:"styling"`
	SingleUse                   datatypes.JSON   `gorm:"column:singleUse;default:'{\"enabled\": false, \"isEncrypted\": true}'" json:"singleUse"`
	IsVerifyEmailEnabled        bool             `gorm:"column:isVerifyEmailEnabled;default:false;not null" json:"isVerifyEmailEnabled"`
	IsBackButtonHidden          bool             `gorm:"column:isBackButtonHidden;default:false;not null" json:"isBackButtonHidden"`
	IsAutoProgressingEnabled    bool             `gorm:"column:isAutoProgressingEnabled;default:false;not null" json:"isAutoProgressingEnabled"`
	IsCaptureIPEnabled          bool             `gorm:"column:isCaptureIpEnabled;default:false;not null" json:"isCaptureIpEnabled"`
	IsAnonymizeResponsesEnabled bool             `gorm:"column:is_anonymize_responses_enabled;default:false;not null" json:"is_anonymize_responses_enabled"`
	Pin                         *string          `gorm:"column:pin" json:"pin"`
	DisplayPercentage           *float64         `gorm:"column:displayPercentage" json:"displayPercentage"`
	ShowLanguageSwitch          *bool            `gorm:"column:showLanguageSwitch" json:"showLanguageSwitch"`
	AutoSelectLanguage          *bool            `gorm:"column:autoSelectLanguage" json:"autoSelectLanguage"`
	Recaptcha                   datatypes.JSON   `gorm:"column:recaptcha;default:'{\"enabled\": false, \"threshold\":0.1}'" json:"recaptcha"`
	Metadata                    datatypes.JSON   `gorm:"column:metadata;default:'{}';not null" json:"metadata"`
	Slug                        *string          `gorm:"column:slug;uniqueIndex" json:"slug"`
	CustomHeadScripts           *string          `gorm:"column:customHeadScripts" json:"customHeadScripts"`
	CustomHeadScriptsMode       *string          `gorm:"column:customHeadScriptsMode;default:'add'" json:"customHeadScriptsMode"`
	CustomCSS                   datatypes.JSON   `gorm:"column:customCss" json:"customCss"`
	ShowConditions              datatypes.JSON   `gorm:"column:showConditions" json:"showConditions"`
	StartLogic                  datatypes.JSON   `gorm:"column:startLogic" json:"startLogic"`
	Workspace                   *Workspace
	Creator                     *User `gorm:"foreignKey:CreatedBy"`
	Owner                       *User `gorm:"foreignKey:OwnerID"`
	Segment                     *Segment
	Responses                   []Response                        `gorm:"foreignKey:SurveyID;constraint:OnDelete:CASCADE"`
	Quotas                      []SurveyQuota                     `gorm:"foreignKey:SurveyID;constraint:OnDelete:CASCADE"`
	Triggers                    []SurveyTrigger                   `gorm:"foreignKey:SurveyID;constraint:OnDelete:CASCADE"`
	AttributeFilters            []SurveyAttributeFilter           `gorm:"foreignKey:SurveyID;constraint:OnDelete:CASCADE"`
	Displays                    []Display                         `gorm:"foreignKey:SurveyID;constraint:OnDelete:CASCADE"`
	Languages                   []SurveyLanguage                  `gorm:"foreignKey:SurveyID;constraint:OnDelete:CASCADE"`
	EmbeddedData                []EmbeddedData                    `gorm:"foreignKey:SurveyID,WorkspaceID;references:ID,WorkspaceID;constraint:OnDelete:CASCADE"`
	EmbeddedDataLinks           []SurveyEmbeddedData              `gorm:"foreignKey:SurveyID,WorkspaceID;references:ID,WorkspaceID;constraint:OnDelete:CASCADE"`
	FollowUps                   []SurveyFollowUp                  `gorm:"foreignKey:SurveyID;constraint:OnDelete:CASCADE"`
	FeedbackSourceMappings      []FeedbackSourceFormbricksMapping `gorm:"foreignKey:SurveyID,WorkspaceID;references:ID,WorkspaceID;constraint:OnDelete:CASCADE"`
	RetentionExemptions         []RetentionExemption              `gorm:"foreignKey:SurveyID;constraint:OnDelete:CASCADE"`
	RetentionNotices            []RetentionNotice                 `gorm:"foreignKey:SurveyID;constraint:OnDelete:CASCADE"`
	Creatives                   []Creative                        `gorm:"foreignKey:SurveyID,WorkspaceID;references:ID,WorkspaceID;constraint:OnUpdate:NO ACTION,OnDelete:NO ACTION"`
	AttachmentExports           []AttachmentExport                `gorm:"foreignKey:SurveyID,WorkspaceID;references:ID,WorkspaceID;constraint:OnDelete:CASCADE"`
	RetentionRestore            *RetentionSurveyRestore           `gorm:"foreignKey:SurveyID;constraint:OnDelete:CASCADE"`
}

func (Survey) TableName() string {
	return "Survey"
}

func (m *Survey) BeforeCreate(tx *gorm.DB) error {
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

func (*Survey) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type SurveyQuota struct {
	ID                      string         `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt               DateTime       `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt               DateTime       `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	SurveyID                string         `gorm:"column:surveyId;uniqueIndex:SurveyQuota_surveyId_name_key;not null" json:"surveyId"`
	Name                    string         `gorm:"column:name;uniqueIndex:SurveyQuota_surveyId_name_key;not null" json:"name"`
	Limit                   int            `gorm:"column:limit;not null" json:"limit"`
	Logic                   datatypes.JSON `gorm:"column:logic;default:'{}';not null" json:"logic"`
	Action                  string         `gorm:"column:action;not null" json:"action"`
	EndingCardID            *string        `gorm:"column:endingCardId" json:"endingCardId"`
	CountPartialSubmissions bool           `gorm:"column:countPartialSubmissions;default:false;not null" json:"countPartialSubmissions"`
	Survey                  *Survey
	QuotaLinks              []ResponseQuotaLink `gorm:"foreignKey:QuotaID;constraint:OnDelete:CASCADE"`
}

func (SurveyQuota) TableName() string {
	return "SurveyQuota"
}

func (m *SurveyQuota) BeforeCreate(tx *gorm.DB) error {
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

func (*SurveyQuota) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type ResponseQuotaLink struct {
	ResponseID string `gorm:"column:responseId;primaryKey" json:"responseId"`
	QuotaID    string `gorm:"column:quotaId;primaryKey;index:ResponseQuotaLink_quotaId_status_idx" json:"quotaId"`
	Status     string `gorm:"column:status;index:ResponseQuotaLink_quotaId_status_idx;not null" json:"status"`
	Response   *Response
	Quota      *SurveyQuota `gorm:"foreignKey:QuotaID"`
}

func (ResponseQuotaLink) TableName() string {
	return "ResponseQuotaLink"
}

type SurveyFollowUp struct {
	ID        string         `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt DateTime       `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt DateTime       `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	SurveyID  string         `gorm:"column:surveyId;not null" json:"surveyId"`
	Name      string         `gorm:"column:name;not null" json:"name"`
	Trigger   datatypes.JSON `gorm:"column:trigger;not null" json:"trigger"`
	Action    datatypes.JSON `gorm:"column:action;not null" json:"action"`
	Survey    *Survey
}

func (SurveyFollowUp) TableName() string {
	return "SurveyFollowUp"
}

func (m *SurveyFollowUp) BeforeCreate(tx *gorm.DB) error {
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

func (*SurveyFollowUp) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type ActionClass struct {
	ID               string         `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt        DateTime       `gorm:"column:created_at;index:ActionClass_workspaceId_created_at_idx;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt        DateTime       `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	Name             string         `gorm:"column:name;uniqueIndex:ActionClass_name_workspaceId_key;not null" json:"name"`
	Description      *string        `gorm:"column:description" json:"description"`
	Type             string         `gorm:"column:type;not null" json:"type"`
	Key              *string        `gorm:"column:key;uniqueIndex:ActionClass_key_workspaceId_key" json:"key"`
	NoCodeConfig     datatypes.JSON `gorm:"column:noCodeConfig" json:"noCodeConfig"`
	WorkspaceID      string         `gorm:"column:workspaceId;uniqueIndex:ActionClass_key_workspaceId_key;uniqueIndex:ActionClass_name_workspaceId_key;index:ActionClass_workspaceId_created_at_idx;not null" json:"workspaceId"`
	Workspace        *Workspace
	SurveyTriggers   []SurveyTrigger   `gorm:"foreignKey:ActionClassID;constraint:OnDelete:CASCADE"`
	CreativeTriggers []CreativeTrigger `gorm:"foreignKey:ActionClassID;constraint:OnDelete:CASCADE"`
}

func (ActionClass) TableName() string {
	return "ActionClass"
}

func (m *ActionClass) BeforeCreate(tx *gorm.DB) error {
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

func (*ActionClass) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Integration struct {
	ID          string         `gorm:"column:id;primaryKey" json:"id"`
	Type        string         `gorm:"column:type;uniqueIndex:Integration_type_workspaceId_key;not null" json:"type"`
	Config      datatypes.JSON `gorm:"column:config;not null" json:"config"`
	WorkspaceID string         `gorm:"column:workspaceId;uniqueIndex:Integration_type_workspaceId_key;index:Integration_workspaceId_idx;not null" json:"workspaceId"`
	Workspace   *Workspace
}

func (Integration) TableName() string {
	return "Integration"
}

func (m *Integration) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = cuid.New()
	}
	return nil
}

type DataMigration struct {
	ID         string    `gorm:"column:id;primaryKey" json:"id"`
	StartedAt  DateTime  `gorm:"column:started_at;not null" json:"started_at"`
	FinishedAt *DateTime `gorm:"column:finished_at" json:"finished_at"`
	Name       string    `gorm:"column:name;uniqueIndex;not null" json:"name"`
	Status     string    `gorm:"column:status;not null" json:"status"`
}

func (DataMigration) TableName() string {
	return "DataMigration"
}

func (m *DataMigration) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.StartedAt.IsZero() {
		m.StartedAt = DateTime{Time: now}
	}
	return nil
}

type Workspace struct {
	ID                          string         `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt                   DateTime       `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt                   DateTime       `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	Name                        string         `gorm:"column:name;uniqueIndex:Workspace_organizationId_name_key;not null" json:"name"`
	LegacyEnvironmentID         *string        `gorm:"column:legacyEnvironmentId;uniqueIndex" json:"legacyEnvironmentId"`
	OrganizationID              string         `gorm:"column:organizationId;uniqueIndex:Workspace_organizationId_name_key;not null" json:"organizationId"`
	Styling                     datatypes.JSON `gorm:"column:styling;default:'{\"allowStyleOverwrite\":true}';not null" json:"styling"`
	Config                      datatypes.JSON `gorm:"column:config;default:'{}';not null" json:"config"`
	RecontactDays               *int           `gorm:"column:recontactDays;default:7;not null" json:"recontactDays"`
	LinkSurveyBranding          *bool          `gorm:"column:linkSurveyBranding;default:true;not null" json:"linkSurveyBranding"`
	InAppSurveyBranding         *bool          `gorm:"column:inAppSurveyBranding;default:true;not null" json:"inAppSurveyBranding"`
	Placement                   string         `gorm:"column:placement;default:'bottomRight';not null" json:"placement"`
	ClickOutsideClose           *bool          `gorm:"column:clickOutsideClose;default:true;not null" json:"clickOutsideClose"`
	Overlay                     string         `gorm:"column:overlay;default:'none';not null" json:"overlay"`
	OverlayColor                *string        `gorm:"column:overlayColor" json:"overlayColor"`
	OverlayOpacity              *int           `gorm:"column:overlayOpacity" json:"overlayOpacity"`
	Logo                        datatypes.JSON `gorm:"column:logo" json:"logo"`
	AppSetupCompleted           bool           `gorm:"column:appSetupCompleted;default:false;not null" json:"appSetupCompleted"`
	CustomHeadScripts           *string        `gorm:"column:customHeadScripts" json:"customHeadScripts"`
	CustomCSS                   datatypes.JSON `gorm:"column:customCss" json:"customCss"`
	CustomCSSPrevious           datatypes.JSON `gorm:"column:customCssPrevious" json:"customCssPrevious"`
	Organization                *Organization
	Languages                   []Language                   `gorm:"foreignKey:WorkspaceID;constraint:OnDelete:CASCADE"`
	EmbeddedData                []EmbeddedData               `gorm:"foreignKey:WorkspaceID;constraint:OnDelete:CASCADE"`
	WorkspaceTeams              []WorkspaceTeam              `gorm:"foreignKey:WorkspaceID;constraint:OnDelete:CASCADE"`
	FeedbackDirectoryWorkspaces []FeedbackDirectoryWorkspace `gorm:"foreignKey:WorkspaceID;constraint:OnDelete:CASCADE"`
	Charts                      []Chart                      `gorm:"foreignKey:WorkspaceID;constraint:OnDelete:CASCADE"`
	Dashboards                  []Dashboard                  `gorm:"foreignKey:WorkspaceID;constraint:OnDelete:CASCADE"`
	Creatives                   []Creative                   `gorm:"foreignKey:WorkspaceID;constraint:OnDelete:CASCADE"`
	AttachmentExports           []AttachmentExport           `gorm:"foreignKey:WorkspaceID;constraint:OnDelete:CASCADE"`
	Surveys                     []Survey                     `gorm:"foreignKey:WorkspaceID;constraint:OnDelete:CASCADE"`
	Contacts                    []Contact                    `gorm:"foreignKey:WorkspaceID;constraint:OnDelete:CASCADE"`
	ActionClasses               []ActionClass                `gorm:"foreignKey:WorkspaceID;constraint:OnDelete:CASCADE"`
	ContactAttributeKeys        []ContactAttributeKey        `gorm:"foreignKey:WorkspaceID;constraint:OnDelete:CASCADE"`
	Webhooks                    []Webhook                    `gorm:"foreignKey:WorkspaceID;constraint:OnDelete:CASCADE"`
	Tags                        []Tag                        `gorm:"foreignKey:WorkspaceID;constraint:OnDelete:CASCADE"`
	Segments                    []Segment                    `gorm:"foreignKey:WorkspaceID;constraint:OnDelete:CASCADE"`
	Integrations                []Integration                `gorm:"foreignKey:WorkspaceID;constraint:OnDelete:CASCADE"`
	APIKeyWorkspaces            []APIKeyWorkspace            `gorm:"foreignKey:WorkspaceID;constraint:OnDelete:CASCADE"`
	FeedbackSources             []FeedbackSource             `gorm:"foreignKey:WorkspaceID;constraint:OnDelete:CASCADE"`
	Workflows                   []Workflow                   `gorm:"foreignKey:WorkspaceID;constraint:OnDelete:CASCADE"`
	WorkflowRuns                []WorkflowRun                `gorm:"foreignKey:WorkspaceID;constraint:OnDelete:CASCADE"`
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

type Organization struct {
	ID                    string               `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt             DateTime             `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt             DateTime             `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	Name                  string               `gorm:"column:name;not null" json:"name"`
	Whitelabel            datatypes.JSON       `gorm:"column:whitelabel;default:'{}';not null" json:"whitelabel"`
	IsAISmartToolsEnabled bool                 `gorm:"column:isAISmartToolsEnabled;default:false;not null" json:"isAISmartToolsEnabled"`
	DisplayTimeZone       *string              `gorm:"column:displayTimeZone" json:"displayTimeZone"`
	Memberships           []Membership         `gorm:"foreignKey:OrganizationID;constraint:OnDelete:CASCADE"`
	Workspaces            []Workspace          `gorm:"foreignKey:OrganizationID;constraint:OnDelete:CASCADE"`
	Invites               []Invite             `gorm:"foreignKey:OrganizationID;constraint:OnDelete:CASCADE"`
	Teams                 []Team               `gorm:"foreignKey:OrganizationID;constraint:OnDelete:CASCADE"`
	APIKeys               []APIKey             `gorm:"foreignKey:OrganizationID;constraint:OnDelete:CASCADE"`
	FeedbackDirectories   []FeedbackDirectory  `gorm:"foreignKey:OrganizationID;constraint:OnDelete:CASCADE"`
	RetentionPolicies     []RetentionPolicy    `gorm:"foreignKey:OrganizationID;constraint:OnDelete:CASCADE"`
	RetentionExemptions   []RetentionExemption `gorm:"foreignKey:OrganizationID;constraint:OnDelete:CASCADE"`
	RetentionRuns         []RetentionRun       `gorm:"foreignKey:OrganizationID;constraint:OnDelete:CASCADE"`
	RetentionNotices      []RetentionNotice    `gorm:"foreignKey:OrganizationID;constraint:OnDelete:CASCADE"`
	Billing               *OrganizationBilling `gorm:"foreignKey:OrganizationID;constraint:OnDelete:CASCADE"`
}

func (Organization) TableName() string {
	return "Organization"
}

func (m *Organization) BeforeCreate(tx *gorm.DB) error {
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

func (*Organization) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type OrganizationBilling struct {
	OrganizationID   string         `gorm:"column:organization_id;primaryKey" json:"organization_id"`
	StripeCustomerID *string        `gorm:"column:stripe_customer_id;uniqueIndex" json:"stripe_customer_id"`
	Limits           datatypes.JSON `gorm:"column:limits;not null" json:"limits"`
	UsageCycleAnchor *DateTime      `gorm:"column:usage_cycle_anchor" json:"usage_cycle_anchor"`
	Stripe           datatypes.JSON `gorm:"column:stripe" json:"stripe"`
	CreatedAt        DateTime       `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt        DateTime       `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	Organization     *Organization
}

func (OrganizationBilling) TableName() string {
	return "OrganizationBilling"
}

func (m *OrganizationBilling) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*OrganizationBilling) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Membership struct {
	OrganizationID string `gorm:"column:organizationId;primaryKey;index:Membership_organizationId_idx" json:"organizationId"`
	UserID         string `gorm:"column:userId;primaryKey" json:"userId"`
	Accepted       bool   `gorm:"column:accepted;default:false;not null" json:"accepted"`
	Role           string `gorm:"column:role;default:'member';not null" json:"role"`
	Organization   *Organization
	User           *User
}

func (Membership) TableName() string {
	return "Membership"
}

type Invite struct {
	ID             string   `gorm:"column:id;primaryKey;type:char(36)" json:"id"`
	Email          string   `gorm:"column:email;index:Invite_email_organizationId_idx;not null" json:"email"`
	Name           *string  `gorm:"column:name" json:"name"`
	OrganizationID string   `gorm:"column:organizationId;index:Invite_email_organizationId_idx;index:Invite_organizationId_idx;not null" json:"organizationId"`
	CreatorID      string   `gorm:"column:creatorId;not null" json:"creatorId"`
	AcceptorID     *string  `gorm:"column:acceptorId" json:"acceptorId"`
	CreatedAt      DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	ExpiresAt      DateTime `gorm:"column:expiresAt;not null" json:"expiresAt"`
	Role           string   `gorm:"column:role;default:'member';not null" json:"role"`
	TeamIds        []string `gorm:"column:teamIds;serializer:json;not null" json:"teamIds"`
	Organization   *Organization
	Creator        *User `gorm:"foreignKey:CreatorID"`
	Acceptor       *User `gorm:"foreignKey:AcceptorID"`
}

func (Invite) TableName() string {
	return "Invite"
}

func (m *Invite) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = uuid.NewString()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type APIKey struct {
	ID                 string         `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt          DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	CreatedBy          *string        `gorm:"column:createdBy" json:"createdBy"`
	LastUsedAt         *DateTime      `gorm:"column:lastUsedAt" json:"lastUsedAt"`
	Label              string         `gorm:"column:label;not null" json:"label"`
	HashedKey          string         `gorm:"column:hashedKey;not null" json:"hashedKey"`
	LookupHash         *string        `gorm:"column:lookupHash;uniqueIndex" json:"lookupHash"`
	OrganizationID     string         `gorm:"column:organizationId;index:ApiKey_organizationId_idx;not null" json:"organizationId"`
	OrganizationAccess datatypes.JSON `gorm:"column:organizationAccess;default:'{}';not null" json:"organizationAccess"`
	Organization       *Organization
	APIKeyWorkspaces   []APIKeyWorkspace `gorm:"foreignKey:APIKeyID;constraint:OnDelete:CASCADE"`
}

func (APIKey) TableName() string {
	return "ApiKey"
}

func (m *APIKey) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type APIKeyWorkspace struct {
	ID          string   `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt   DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt   DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	APIKeyID    string   `gorm:"column:apiKeyId;uniqueIndex:ApiKeyWorkspace_apiKeyId_workspaceId_key;not null" json:"apiKeyId"`
	WorkspaceID string   `gorm:"column:workspaceId;uniqueIndex:ApiKeyWorkspace_apiKeyId_workspaceId_key;index:ApiKeyWorkspace_workspaceId_idx;not null" json:"workspaceId"`
	Permission  string   `gorm:"column:permission;not null" json:"permission"`
	APIKey      *APIKey
	Workspace   *Workspace
}

func (APIKeyWorkspace) TableName() string {
	return "ApiKeyWorkspace"
}

func (m *APIKeyWorkspace) BeforeCreate(tx *gorm.DB) error {
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

func (*APIKeyWorkspace) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Account struct {
	ID                    string    `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt             DateTime  `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt             DateTime  `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	UserID                string    `gorm:"column:userId;index:Account_userId_idx;not null" json:"userId"`
	Type                  *string   `gorm:"column:type" json:"type"`
	Provider              string    `gorm:"column:provider;uniqueIndex:Account_provider_providerAccountId_key;not null" json:"provider"`
	ProviderAccountID     string    `gorm:"column:providerAccountId;uniqueIndex:Account_provider_providerAccountId_key;uniqueIndex:Account_issuer_providerAccountId_key;not null" json:"providerAccountId"`
	AccessToken           *string   `gorm:"column:access_token;type:text" json:"access_token"`
	RefreshToken          *string   `gorm:"column:refresh_token;type:text" json:"refresh_token"`
	ExpiresAt             *int      `gorm:"column:expires_at" json:"expires_at"`
	ExtExpiresIn          *int      `gorm:"column:ext_expires_in" json:"ext_expires_in"`
	TokenType             *string   `gorm:"column:token_type" json:"token_type"`
	Scope                 *string   `gorm:"column:scope" json:"scope"`
	IDToken               *string   `gorm:"column:id_token;type:text" json:"id_token"`
	SessionState          *string   `gorm:"column:session_state" json:"session_state"`
	Password              *string   `gorm:"column:password" json:"password"`
	AccessTokenExpiresAt  *DateTime `gorm:"column:accessTokenExpiresAt" json:"accessTokenExpiresAt"`
	RefreshTokenExpiresAt *DateTime `gorm:"column:refreshTokenExpiresAt" json:"refreshTokenExpiresAt"`
	Issuer                *string   `gorm:"column:issuer;uniqueIndex:Account_issuer_providerAccountId_key" json:"issuer"`
	User                  *User
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

type Session struct {
	ID                 string   `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt          DateTime `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt          DateTime `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	SessionToken       string   `gorm:"column:sessionToken;uniqueIndex;not null" json:"sessionToken"`
	UserID             string   `gorm:"column:userId;index:Session_userId_idx;not null" json:"userId"`
	Expires            DateTime `gorm:"column:expires;not null" json:"expires"`
	IPAddress          *string  `gorm:"column:ipAddress" json:"ipAddress"`
	UserAgent          *string  `gorm:"column:userAgent" json:"userAgent"`
	User               *User
	OauthAccessTokens  []OauthAccessToken  `gorm:"foreignKey:SessionID;constraint:OnDelete:SET NULL"`
	OauthRefreshTokens []OauthRefreshToken `gorm:"foreignKey:SessionID;constraint:OnDelete:SET NULL"`
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

type VerificationToken struct {
	Identifier string   `gorm:"column:identifier;primaryKey;uniqueIndex:VerificationToken_identifier_token_key" json:"identifier"`
	Token      string   `gorm:"column:token;primaryKey;uniqueIndex:VerificationToken_identifier_token_key" json:"token"`
	Expires    DateTime `gorm:"column:expires;not null" json:"expires"`
}

func (VerificationToken) TableName() string {
	return "VerificationToken"
}

type TwoFactor struct {
	ID                      string    `gorm:"column:id;primaryKey" json:"id"`
	Secret                  string    `gorm:"column:secret;not null" json:"secret"`
	BackupCodes             string    `gorm:"column:backupCodes;not null" json:"backupCodes"`
	UserID                  string    `gorm:"column:userId;uniqueIndex;not null" json:"userId"`
	Verified                *bool     `gorm:"column:verified;default:true;not null" json:"verified"`
	FailedVerificationCount int       `gorm:"column:failedVerificationCount;default:0;not null" json:"failedVerificationCount"`
	LockedUntil             *DateTime `gorm:"column:lockedUntil" json:"lockedUntil"`
	User                    *User
}

func (TwoFactor) TableName() string {
	return "TwoFactor"
}

func (m *TwoFactor) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = cuid.New()
	}
	return nil
}

type Jwks struct {
	ID         string    `gorm:"column:id;primaryKey" json:"id"`
	PublicKey  string    `gorm:"column:publicKey;not null" json:"publicKey"`
	PrivateKey string    `gorm:"column:privateKey;not null" json:"privateKey"`
	CreatedAt  DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	ExpiresAt  *DateTime `gorm:"column:expiresAt" json:"expiresAt"`
	Alg        *string   `gorm:"column:alg" json:"alg"`
	Crv        *string   `gorm:"column:crv" json:"crv"`
}

func (Jwks) TableName() string {
	return "jwks"
}

func (m *Jwks) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = cuid.New()
	}
	return nil
}

type OauthClient struct {
	ID                               string         `gorm:"column:id;primaryKey" json:"id"`
	ClientID                         string         `gorm:"column:clientId;uniqueIndex;not null" json:"clientId"`
	ClientSecret                     *string        `gorm:"column:clientSecret" json:"clientSecret"`
	Disabled                         *bool          `gorm:"column:disabled;default:false" json:"disabled"`
	SkipConsent                      *bool          `gorm:"column:skipConsent" json:"skipConsent"`
	EnableEndSession                 *bool          `gorm:"column:enableEndSession" json:"enableEndSession"`
	SubjectType                      *string        `gorm:"column:subjectType" json:"subjectType"`
	Scopes                           []string       `gorm:"column:scopes;serializer:json;not null" json:"scopes"`
	UserID                           *string        `gorm:"column:userId;index:oauthClient_userId_idx" json:"userId"`
	CreatedAt                        *DateTime      `gorm:"column:createdAt;autoCreateTime:false" json:"createdAt"`
	UpdatedAt                        *DateTime      `gorm:"column:updatedAt;autoUpdateTime:false" json:"updatedAt"`
	Name                             *string        `gorm:"column:name" json:"name"`
	URI                              *string        `gorm:"column:uri" json:"uri"`
	Icon                             *string        `gorm:"column:icon" json:"icon"`
	Contacts                         []string       `gorm:"column:contacts;serializer:json;not null" json:"contacts"`
	Tos                              *string        `gorm:"column:tos" json:"tos"`
	Policy                           *string        `gorm:"column:policy" json:"policy"`
	SoftwareID                       *string        `gorm:"column:softwareId" json:"softwareId"`
	SoftwareVersion                  *string        `gorm:"column:softwareVersion" json:"softwareVersion"`
	SoftwareStatement                *string        `gorm:"column:softwareStatement" json:"softwareStatement"`
	RedirectUris                     []string       `gorm:"column:redirectUris;serializer:json;not null" json:"redirectUris"`
	PostLogoutRedirectUris           []string       `gorm:"column:postLogoutRedirectUris;serializer:json;not null" json:"postLogoutRedirectUris"`
	TokenEndpointAuthMethod          *string        `gorm:"column:tokenEndpointAuthMethod" json:"tokenEndpointAuthMethod"`
	GrantTypes                       []string       `gorm:"column:grantTypes;serializer:json;not null" json:"grantTypes"`
	ResponseTypes                    []string       `gorm:"column:responseTypes;serializer:json;not null" json:"responseTypes"`
	Public                           *bool          `gorm:"column:public" json:"public"`
	Type                             *string        `gorm:"column:type" json:"type"`
	RequirePKCE                      *bool          `gorm:"column:requirePKCE" json:"requirePKCE"`
	ReferenceID                      *string        `gorm:"column:referenceId" json:"referenceId"`
	Metadata                         datatypes.JSON `gorm:"column:metadata" json:"metadata"`
	ClientDiscoveryID                *string        `gorm:"column:clientDiscoveryId" json:"clientDiscoveryId"`
	ClientCredentialsScopes          []string       `gorm:"column:clientCredentialsScopes;serializer:json;not null" json:"clientCredentialsScopes"`
	BackchannelLogoutURI             *string        `gorm:"column:backchannelLogoutUri" json:"backchannelLogoutUri"`
	BackchannelLogoutSessionRequired *bool          `gorm:"column:backchannelLogoutSessionRequired" json:"backchannelLogoutSessionRequired"`
	ApplicationType                  *string        `gorm:"column:applicationType" json:"applicationType"`
	Jwks                             *string        `gorm:"column:jwks" json:"jwks"`
	JwksURI                          *string        `gorm:"column:jwksUri" json:"jwksUri"`
	DpopBoundAccessTokens            *bool          `gorm:"column:dpopBoundAccessTokens;default:false" json:"dpopBoundAccessTokens"`
	User                             *User
	AccessTokens                     []OauthAccessToken    `gorm:"foreignKey:ClientID;references:ClientID;constraint:OnDelete:CASCADE"`
	RefreshTokens                    []OauthRefreshToken   `gorm:"foreignKey:ClientID;references:ClientID;constraint:OnDelete:CASCADE"`
	Consents                         []OauthConsent        `gorm:"foreignKey:ClientID;references:ClientID;constraint:OnDelete:CASCADE"`
	Resources                        []OauthClientResource `gorm:"foreignKey:ClientID;references:ClientID;constraint:OnDelete:CASCADE"`
}

func (OauthClient) TableName() string {
	return "oauthClient"
}

func (m *OauthClient) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = cuid.New()
	}
	return nil
}

type OauthAccessToken struct {
	ID                      string         `gorm:"column:id;primaryKey" json:"id"`
	Token                   string         `gorm:"column:token;uniqueIndex;not null" json:"token"`
	ClientID                string         `gorm:"column:clientId;index:oauthAccessToken_clientId_idx;not null" json:"clientId"`
	SessionID               *string        `gorm:"column:sessionId;index:oauthAccessToken_sessionId_idx" json:"sessionId"`
	UserID                  *string        `gorm:"column:userId;index:oauthAccessToken_userId_idx" json:"userId"`
	ReferenceID             *string        `gorm:"column:referenceId" json:"referenceId"`
	RefreshID               *string        `gorm:"column:refreshId;index:oauthAccessToken_refreshId_idx" json:"refreshId"`
	ExpiresAt               DateTime       `gorm:"column:expiresAt;not null" json:"expiresAt"`
	CreatedAt               DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	Scopes                  []string       `gorm:"column:scopes;serializer:json;not null" json:"scopes"`
	AuthorizationCodeID     *string        `gorm:"column:authorizationCodeId;index:oauthAccessToken_authorizationCodeId_idx" json:"authorizationCodeId"`
	Resources               []string       `gorm:"column:resources;serializer:json;not null" json:"resources"`
	RequestedUserInfoClaims []string       `gorm:"column:requestedUserInfoClaims;serializer:json;not null" json:"requestedUserInfoClaims"`
	Revoked                 *DateTime      `gorm:"column:revoked" json:"revoked"`
	Confirmation            datatypes.JSON `gorm:"column:confirmation" json:"confirmation"`
	Client                  *OauthClient   `gorm:"foreignKey:ClientID;references:ClientID"`
	Session                 *Session
	User                    *User
	Refresh                 *OauthRefreshToken `gorm:"foreignKey:RefreshID"`
}

func (OauthAccessToken) TableName() string {
	return "oauthAccessToken"
}

func (m *OauthAccessToken) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = cuid.New()
	}
	return nil
}

type OauthRefreshToken struct {
	ID                      string         `gorm:"column:id;primaryKey" json:"id"`
	Token                   string         `gorm:"column:token;uniqueIndex;not null" json:"token"`
	ClientID                string         `gorm:"column:clientId;index:oauthRefreshToken_clientId_idx;not null" json:"clientId"`
	SessionID               *string        `gorm:"column:sessionId;index:oauthRefreshToken_sessionId_idx" json:"sessionId"`
	UserID                  string         `gorm:"column:userId;index:oauthRefreshToken_userId_idx;not null" json:"userId"`
	ReferenceID             *string        `gorm:"column:referenceId" json:"referenceId"`
	ExpiresAt               DateTime       `gorm:"column:expiresAt;not null" json:"expiresAt"`
	CreatedAt               DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	Revoked                 *DateTime      `gorm:"column:revoked" json:"revoked"`
	AuthTime                *DateTime      `gorm:"column:authTime" json:"authTime"`
	Scopes                  []string       `gorm:"column:scopes;serializer:json;not null" json:"scopes"`
	AuthorizationCodeID     *string        `gorm:"column:authorizationCodeId;index:oauthRefreshToken_authorizationCodeId_idx" json:"authorizationCodeId"`
	Resources               []string       `gorm:"column:resources;serializer:json;not null" json:"resources"`
	RequestedUserInfoClaims []string       `gorm:"column:requestedUserInfoClaims;serializer:json;not null" json:"requestedUserInfoClaims"`
	RotatedAt               *DateTime      `gorm:"column:rotatedAt" json:"rotatedAt"`
	RotationReplayResponse  *string        `gorm:"column:rotationReplayResponse" json:"rotationReplayResponse"`
	RotationReplayExpiresAt *DateTime      `gorm:"column:rotationReplayExpiresAt" json:"rotationReplayExpiresAt"`
	Confirmation            datatypes.JSON `gorm:"column:confirmation" json:"confirmation"`
	Client                  *OauthClient   `gorm:"foreignKey:ClientID;references:ClientID"`
	Session                 *Session
	User                    *User
	AccessTokens            []OauthAccessToken `gorm:"foreignKey:RefreshID;constraint:OnDelete:CASCADE"`
}

func (OauthRefreshToken) TableName() string {
	return "oauthRefreshToken"
}

func (m *OauthRefreshToken) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = cuid.New()
	}
	return nil
}

type OauthConsent struct {
	ID                      string       `gorm:"column:id;primaryKey" json:"id"`
	ClientID                string       `gorm:"column:clientId;index:oauthConsent_clientId_idx;not null" json:"clientId"`
	UserID                  *string      `gorm:"column:userId;index:oauthConsent_userId_idx" json:"userId"`
	ReferenceID             *string      `gorm:"column:referenceId" json:"referenceId"`
	Scopes                  []string     `gorm:"column:scopes;serializer:json;not null" json:"scopes"`
	CreatedAt               DateTime     `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt               DateTime     `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Resources               []string     `gorm:"column:resources;serializer:json;not null" json:"resources"`
	RequestedUserInfoClaims []string     `gorm:"column:requestedUserInfoClaims;serializer:json;not null" json:"requestedUserInfoClaims"`
	Client                  *OauthClient `gorm:"foreignKey:ClientID;references:ClientID"`
	User                    *User
}

func (OauthConsent) TableName() string {
	return "oauthConsent"
}

func (m *OauthConsent) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = cuid.New()
	}
	return nil
}

type OauthResource struct {
	ID                            string                `gorm:"column:id;primaryKey" json:"id"`
	Identifier                    string                `gorm:"column:identifier;uniqueIndex;not null" json:"identifier"`
	Name                          string                `gorm:"column:name;not null" json:"name"`
	AccessTokenTTL                *int                  `gorm:"column:accessTokenTtl" json:"accessTokenTtl"`
	RefreshTokenTTL               *int                  `gorm:"column:refreshTokenTtl" json:"refreshTokenTtl"`
	SigningAlgorithm              *string               `gorm:"column:signingAlgorithm" json:"signingAlgorithm"`
	SigningKeyID                  *string               `gorm:"column:signingKeyId" json:"signingKeyId"`
	AllowedScopes                 []string              `gorm:"column:allowedScopes;serializer:json;not null" json:"allowedScopes"`
	CustomClaims                  datatypes.JSON        `gorm:"column:customClaims" json:"customClaims"`
	DpopBoundAccessTokensRequired *bool                 `gorm:"column:dpopBoundAccessTokensRequired;default:false" json:"dpopBoundAccessTokensRequired"`
	Disabled                      *bool                 `gorm:"column:disabled;default:false" json:"disabled"`
	CreatedAt                     *DateTime             `gorm:"column:createdAt;autoCreateTime:false" json:"createdAt"`
	UpdatedAt                     *DateTime             `gorm:"column:updatedAt;autoUpdateTime:false" json:"updatedAt"`
	PolicyVersion                 *int                  `gorm:"column:policyVersion;default:1" json:"policyVersion"`
	Metadata                      datatypes.JSON        `gorm:"column:metadata" json:"metadata"`
	Clients                       []OauthClientResource `gorm:"foreignKey:ResourceID;references:Identifier;constraint:OnDelete:CASCADE"`
}

func (OauthResource) TableName() string {
	return "oauthResource"
}

func (m *OauthResource) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = cuid.New()
	}
	return nil
}

type OauthClientResource struct {
	ID         string         `gorm:"column:id;primaryKey" json:"id"`
	ClientID   string         `gorm:"column:clientId;uniqueIndex:oauthClientResource_clientId_resourceId_key;not null" json:"clientId"`
	ResourceID string         `gorm:"column:resourceId;uniqueIndex:oauthClientResource_clientId_resourceId_key;index:oauthClientResource_resourceId_idx;not null" json:"resourceId"`
	Metadata   datatypes.JSON `gorm:"column:metadata" json:"metadata"`
	CreatedAt  *DateTime      `gorm:"column:createdAt;autoCreateTime:false" json:"createdAt"`
	Client     *OauthClient   `gorm:"foreignKey:ClientID;references:ClientID"`
	Resource   *OauthResource `gorm:"foreignKey:ResourceID;references:Identifier"`
}

func (OauthClientResource) TableName() string {
	return "oauthClientResource"
}

func (m *OauthClientResource) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = cuid.New()
	}
	return nil
}

type OauthClientAssertion struct {
	ID        string   `gorm:"column:id;primaryKey" json:"id"`
	ExpiresAt DateTime `gorm:"column:expiresAt;index:oauthClientAssertion_expiresAt_idx;not null" json:"expiresAt"`
}

func (OauthClientAssertion) TableName() string {
	return "oauthClientAssertion"
}

func (m *OauthClientAssertion) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = cuid.New()
	}
	return nil
}

type PasswordResetToken struct {
	ID        string   `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt DateTime `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt DateTime `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	TokenHash string   `gorm:"column:token_hash;uniqueIndex;not null" json:"token_hash"`
	ExpiresAt DateTime `gorm:"column:expires_at;index:PasswordResetToken_expires_at_idx;not null" json:"expires_at"`
	UserID    string   `gorm:"column:userId;uniqueIndex;not null" json:"userId"`
	User      *User
}

func (PasswordResetToken) TableName() string {
	return "PasswordResetToken"
}

func (m *PasswordResetToken) BeforeCreate(tx *gorm.DB) error {
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

func (*PasswordResetToken) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type User struct {
	ID                        string               `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt                 DateTime             `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt                 DateTime             `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	Name                      string               `gorm:"column:name;not null" json:"name"`
	Email                     string               `gorm:"column:email;uniqueIndex;not null" json:"email"`
	EmailVerified             bool                 `gorm:"column:email_verified;default:false;not null" json:"email_verified"`
	TwoFactorSecret           *string              `gorm:"column:twoFactorSecret" json:"twoFactorSecret"`
	TwoFactorEnabled          bool                 `gorm:"column:twoFactorEnabled;default:false;not null" json:"twoFactorEnabled"`
	BackupCodes               *string              `gorm:"column:backupCodes" json:"backupCodes"`
	Password                  *string              `gorm:"column:password" json:"password"`
	IdentityProvider          string               `gorm:"column:identityProvider;default:'email';not null" json:"identityProvider"`
	IdentityProviderAccountID *string              `gorm:"column:identityProviderAccountId" json:"identityProviderAccountId"`
	GroupID                   *string              `gorm:"column:groupId" json:"groupId"`
	NotificationSettings      datatypes.JSON       `gorm:"column:notificationSettings;default:'{}';not null" json:"notificationSettings"`
	Locale                    *string              `gorm:"column:locale;default:'en-US';not null" json:"locale"`
	LastLoginAt               *DateTime            `gorm:"column:lastLoginAt" json:"lastLoginAt"`
	IsActive                  *bool                `gorm:"column:isActive;default:true;not null" json:"isActive"`
	ReactivatedAt             *DateTime            `gorm:"column:reactivatedAt" json:"reactivatedAt"`
	LastActiveAt              *DateTime            `gorm:"column:lastActiveAt" json:"lastActiveAt"`
	IsBootstrapAdmin          *string              `gorm:"column:isBootstrapAdmin;uniqueIndex" json:"isBootstrapAdmin"`
	Memberships               []Membership         `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	Accounts                  []Account            `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	Sessions                  []Session            `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	OauthClients              []OauthClient        `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	OauthAccessTokens         []OauthAccessToken   `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	OauthRefreshTokens        []OauthRefreshToken  `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	OauthConsents             []OauthConsent       `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	InvitesCreated            []Invite             `gorm:"foreignKey:CreatorID"`
	InvitesAccepted           []Invite             `gorm:"foreignKey:AcceptorID;constraint:OnDelete:CASCADE"`
	Surveys                   []Survey             `gorm:"foreignKey:CreatedBy"`
	OwnedSurveys              []Survey             `gorm:"foreignKey:OwnerID;constraint:OnDelete:SET NULL"`
	Charts                    []Chart              `gorm:"foreignKey:CreatedBy;constraint:OnDelete:SET NULL"`
	Dashboards                []Dashboard          `gorm:"foreignKey:CreatedBy;constraint:OnDelete:SET NULL"`
	Creatives                 []Creative           `gorm:"foreignKey:CreatedBy;constraint:OnDelete:SET NULL"`
	FeedbackSources           []FeedbackSource     `gorm:"foreignKey:CreatedBy;constraint:OnDelete:SET NULL"`
	TeamUsers                 []TeamUser           `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	Workflows                 []Workflow           `gorm:"foreignKey:CreatedBy;constraint:OnDelete:SET NULL"`
	WorkflowVersionsPublished []WorkflowVersion    `gorm:"foreignKey:PublishedBy;constraint:OnDelete:SET NULL"`
	RetentionPoliciesUpdated  []RetentionPolicy    `gorm:"foreignKey:UpdatedByID;constraint:OnDelete:SET NULL"`
	ExemptionsCreated         []RetentionExemption `gorm:"foreignKey:CreatedByID;constraint:OnDelete:SET NULL"`
	ExemptionsRevoked         []RetentionExemption `gorm:"foreignKey:RevokedByID;constraint:OnDelete:SET NULL"`
	RetentionNotices          []RetentionNotice    `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	AttachmentExports         []AttachmentExport   `gorm:"foreignKey:RequestedByID;constraint:OnDelete:SET NULL"`
	TwoFactor                 *TwoFactor           `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	PasswordResetToken        *PasswordResetToken  `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
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

type Segment struct {
	ID          string         `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt   DateTime       `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt   DateTime       `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	Title       string         `gorm:"column:title;uniqueIndex:Segment_workspaceId_title_key;not null" json:"title"`
	Description *string        `gorm:"column:description" json:"description"`
	IsPrivate   *bool          `gorm:"column:isPrivate;default:true;not null" json:"isPrivate"`
	Filters     datatypes.JSON `gorm:"column:filters;default:'[]';not null" json:"filters"`
	WorkspaceID string         `gorm:"column:workspaceId;uniqueIndex:Segment_workspaceId_title_key;index:Segment_workspaceId_idx;not null" json:"workspaceId"`
	Workspace   *Workspace
	Surveys     []Survey `gorm:"foreignKey:SegmentID"`
}

func (Segment) TableName() string {
	return "Segment"
}

func (m *Segment) BeforeCreate(tx *gorm.DB) error {
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

func (*Segment) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Language struct {
	ID                string   `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt         DateTime `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt         DateTime `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	Code              string   `gorm:"column:code;uniqueIndex:Language_workspaceId_code_key;not null" json:"code"`
	Alias             *string  `gorm:"column:alias" json:"alias"`
	WorkspaceID       string   `gorm:"column:workspaceId;uniqueIndex:Language_workspaceId_code_key;not null" json:"workspaceId"`
	Workspace         *Workspace
	SurveyLanguages   []SurveyLanguage   `gorm:"foreignKey:LanguageID;constraint:OnDelete:CASCADE"`
	CreativeLanguages []CreativeLanguage `gorm:"foreignKey:LanguageID;constraint:OnDelete:CASCADE"`
}

func (Language) TableName() string {
	return "Language"
}

func (m *Language) BeforeCreate(tx *gorm.DB) error {
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

func (*Language) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type SurveyLanguage struct {
	LanguageID string `gorm:"column:languageId;primaryKey" json:"languageId"`
	SurveyID   string `gorm:"column:surveyId;primaryKey;index:SurveyLanguage_surveyId_idx" json:"surveyId"`
	Default    bool   `gorm:"column:default;default:false;not null" json:"default"`
	Enabled    *bool  `gorm:"column:enabled;default:true;not null" json:"enabled"`
	Language   *Language
	Survey     *Survey
}

func (SurveyLanguage) TableName() string {
	return "SurveyLanguage"
}

type EmbeddedData struct {
	ID           string         `gorm:"column:id;primaryKey;uniqueIndex:EmbeddedData_id_workspaceId_key" json:"id"`
	CreatedAt    DateTime       `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt    DateTime       `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	Key          *string        `gorm:"column:key;uniqueIndex:EmbeddedData_workspaceId_key_key" json:"key"`
	Name         string         `gorm:"column:name;not null" json:"name"`
	Description  *string        `gorm:"column:description" json:"description"`
	Source       string         `gorm:"column:source;not null" json:"source"`
	DataType     string         `gorm:"column:dataType;default:'string';not null" json:"dataType"`
	DefaultValue datatypes.JSON `gorm:"column:defaultValue" json:"defaultValue"`
	Locked       bool           `gorm:"column:locked;default:false;not null" json:"locked"`
	SurveyID     *string        `gorm:"column:surveyId;index:EmbeddedData_surveyId_idx" json:"surveyId"`
	WorkspaceID  string         `gorm:"column:workspaceId;uniqueIndex:EmbeddedData_workspaceId_key_key;uniqueIndex:EmbeddedData_id_workspaceId_key;not null" json:"workspaceId"`
	Survey       *Survey        `gorm:"foreignKey:SurveyID,WorkspaceID;references:ID,WorkspaceID"`
	Workspace    *Workspace
	SurveyLinks  []SurveyEmbeddedData `gorm:"foreignKey:EmbeddedDataID,WorkspaceID;references:ID,WorkspaceID;constraint:OnDelete:CASCADE"`
}

func (EmbeddedData) TableName() string {
	return "EmbeddedData"
}

func (m *EmbeddedData) BeforeCreate(tx *gorm.DB) error {
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

func (*EmbeddedData) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type SurveyEmbeddedData struct {
	ID             string        `gorm:"column:id;primaryKey" json:"id"`
	WorkspaceID    string        `gorm:"column:workspaceId;not null" json:"workspaceId"`
	SurveyID       string        `gorm:"column:surveyId;uniqueIndex:SurveyEmbeddedData_surveyId_embeddedDataId_key;uniqueIndex:SurveyEmbeddedData_surveyId_storageKey_key;index:SurveyEmbeddedData_surveyId_order_idx;not null" json:"surveyId"`
	EmbeddedDataID string        `gorm:"column:embeddedDataId;uniqueIndex:SurveyEmbeddedData_surveyId_embeddedDataId_key;index:SurveyEmbeddedData_embeddedDataId_idx;not null" json:"embeddedDataId"`
	StorageKey     string        `gorm:"column:storageKey;uniqueIndex:SurveyEmbeddedData_surveyId_storageKey_key;not null" json:"storageKey"`
	Order          int           `gorm:"column:order;index:SurveyEmbeddedData_surveyId_order_idx;not null" json:"order"`
	Survey         *Survey       `gorm:"foreignKey:SurveyID,WorkspaceID;references:ID,WorkspaceID"`
	EmbeddedData   *EmbeddedData `gorm:"foreignKey:EmbeddedDataID,WorkspaceID;references:ID,WorkspaceID"`
}

func (SurveyEmbeddedData) TableName() string {
	return "SurveyEmbeddedData"
}

func (m *SurveyEmbeddedData) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = cuid.New()
	}
	return nil
}

type Team struct {
	ID             string   `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt      DateTime `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt      DateTime `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	Name           string   `gorm:"column:name;uniqueIndex:Team_organizationId_name_key;not null" json:"name"`
	OrganizationID string   `gorm:"column:organizationId;uniqueIndex:Team_organizationId_name_key;not null" json:"organizationId"`
	Organization   *Organization
	TeamUsers      []TeamUser      `gorm:"foreignKey:TeamID;constraint:OnDelete:CASCADE"`
	WorkspaceTeams []WorkspaceTeam `gorm:"foreignKey:TeamID;constraint:OnDelete:CASCADE"`
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

type TeamUser struct {
	CreatedAt DateTime `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt DateTime `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	TeamID    string   `gorm:"column:teamId;primaryKey" json:"teamId"`
	UserID    string   `gorm:"column:userId;primaryKey;index:TeamUser_userId_idx" json:"userId"`
	Role      string   `gorm:"column:role;not null" json:"role"`
	Team      *Team
	User      *User
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

type WorkspaceTeam struct {
	CreatedAt   DateTime `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt   DateTime `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	WorkspaceID string   `gorm:"column:workspaceId;primaryKey" json:"workspaceId"`
	TeamID      string   `gorm:"column:teamId;primaryKey;index:WorkspaceTeam_teamId_idx" json:"teamId"`
	Permission  string   `gorm:"column:permission;default:'read';not null" json:"permission"`
	Workspace   *Workspace
	Team        *Team
}

func (WorkspaceTeam) TableName() string {
	return "WorkspaceTeam"
}

func (m *WorkspaceTeam) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*WorkspaceTeam) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Chart struct {
	ID                  string         `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt           DateTime       `gorm:"column:created_at;index:Chart_workspaceId_created_at_idx;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt           DateTime       `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	Name                string         `gorm:"column:name;uniqueIndex:Chart_workspaceId_name_key;not null" json:"name"`
	Type                string         `gorm:"column:type;not null" json:"type"`
	WorkspaceID         string         `gorm:"column:workspaceId;uniqueIndex:Chart_workspaceId_name_key;index:Chart_workspaceId_created_at_idx;not null" json:"workspaceId"`
	Query               datatypes.JSON `gorm:"column:query;default:'{}';not null" json:"query"`
	Config              datatypes.JSON `gorm:"column:config;default:'{}';not null" json:"config"`
	CreatedBy           *string        `gorm:"column:createdBy" json:"createdBy"`
	FeedbackDirectoryID string         `gorm:"column:feedbackDirectoryId;index:Chart_feedbackDirectoryId_idx;not null" json:"feedbackDirectoryId"`
	Workspace           *Workspace
	Creator             *User `gorm:"foreignKey:CreatedBy"`
	FeedbackDirectory   *FeedbackDirectory
	Widgets             []DashboardWidget `gorm:"foreignKey:ChartID;constraint:OnDelete:CASCADE"`
}

func (Chart) TableName() string {
	return "Chart"
}

func (m *Chart) BeforeCreate(tx *gorm.DB) error {
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

func (*Chart) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Dashboard struct {
	ID          string         `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt   DateTime       `gorm:"column:created_at;index:Dashboard_workspaceId_created_at_idx;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt   DateTime       `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	Name        string         `gorm:"column:name;uniqueIndex:Dashboard_workspaceId_name_key;not null" json:"name"`
	WorkspaceID string         `gorm:"column:workspaceId;uniqueIndex:Dashboard_workspaceId_name_key;index:Dashboard_workspaceId_created_at_idx;not null" json:"workspaceId"`
	CreatedBy   *string        `gorm:"column:createdBy" json:"createdBy"`
	Filters     datatypes.JSON `gorm:"column:filters;default:'[]';not null" json:"filters"`
	Workspace   *Workspace
	Creator     *User             `gorm:"foreignKey:CreatedBy"`
	Widgets     []DashboardWidget `gorm:"foreignKey:DashboardID;constraint:OnDelete:CASCADE"`
}

func (Dashboard) TableName() string {
	return "Dashboard"
}

func (m *Dashboard) BeforeCreate(tx *gorm.DB) error {
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

func (*Dashboard) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type DashboardWidget struct {
	ID          string         `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt   DateTime       `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt   DateTime       `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	DashboardID string         `gorm:"column:dashboardId;uniqueIndex:DashboardWidget_dashboardId_chartId_key;index:DashboardWidget_dashboardId_order_idx;not null" json:"dashboardId"`
	ChartID     string         `gorm:"column:chartId;uniqueIndex:DashboardWidget_dashboardId_chartId_key;not null" json:"chartId"`
	Layout      datatypes.JSON `gorm:"column:layout;default:'{\"x\":0,\"y\":0,\"w\":4,\"h\":3}';not null" json:"layout"`
	Order       int            `gorm:"column:order;index:DashboardWidget_dashboardId_order_idx;default:0;not null" json:"order"`
	Dashboard   *Dashboard
	Chart       *Chart
}

func (DashboardWidget) TableName() string {
	return "DashboardWidget"
}

func (m *DashboardWidget) BeforeCreate(tx *gorm.DB) error {
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

func (*DashboardWidget) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type DeletionCleanup struct {
	ID             string   `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt      DateTime `gorm:"column:created_at;index:DeletionCleanup_organizationId_created_at_idx;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt      DateTime `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	Kind           string   `gorm:"column:kind;not null" json:"kind"`
	OrganizationID string   `gorm:"column:organizationId;index:DeletionCleanup_organizationId_created_at_idx;not null" json:"organizationId"`
	WorkspaceID    string   `gorm:"column:workspaceId;not null" json:"workspaceId"`
	SurveyID       string   `gorm:"column:surveyId;not null" json:"surveyId"`
	TenantIds      []string `gorm:"column:tenantIds;serializer:json;not null" json:"tenantIds"`
	ResponseIds    []string `gorm:"column:responseIds;serializer:json;not null" json:"responseIds"`
	FileKeys       []string `gorm:"column:fileKeys;serializer:json;not null" json:"fileKeys"`
	Attempts       int      `gorm:"column:attempts;default:0;not null" json:"attempts"`
	NextAttemptAt  DateTime `gorm:"column:nextAttemptAt;index:DeletionCleanup_nextAttemptAt_idx;not null" json:"nextAttemptAt"`
	LastError      *string  `gorm:"column:lastError" json:"lastError"`
}

func (DeletionCleanup) TableName() string {
	return "DeletionCleanup"
}

func (m *DeletionCleanup) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid2.Generate()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	if m.NextAttemptAt.IsZero() {
		m.NextAttemptAt = DateTime{Time: now}
	}
	return nil
}

func (*DeletionCleanup) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type AttachmentExport struct {
	ID                string         `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt         DateTime       `gorm:"column:created_at;index:AttachmentExport_surveyId_requestedById_created_at_idx;index:AttachmentExport_status_created_at_idx;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt         DateTime       `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	SurveyID          string         `gorm:"column:surveyId;index:AttachmentExport_surveyId_requestedById_created_at_idx;not null" json:"surveyId"`
	WorkspaceID       string         `gorm:"column:workspaceId;index:AttachmentExport_workspaceId_idx;not null" json:"workspaceId"`
	RequestedByID     *string        `gorm:"column:requestedById;index:AttachmentExport_surveyId_requestedById_created_at_idx" json:"requestedById"`
	Status            string         `gorm:"column:status;index:AttachmentExport_status_created_at_idx;default:'queued';not null" json:"status"`
	FilterCriteria    datatypes.JSON `gorm:"column:filterCriteria" json:"filterCriteria"`
	TimeZone          string         `gorm:"column:timeZone;not null" json:"timeZone"`
	DispatchCount     int            `gorm:"column:dispatchCount;default:0;not null" json:"dispatchCount"`
	FileCount         int            `gorm:"column:fileCount;not null" json:"fileCount"`
	ResponseCount     int            `gorm:"column:responseCount;not null" json:"responseCount"`
	IncludedFileCount *int           `gorm:"column:includedFileCount" json:"includedFileCount"`
	TotalBytes        *int64         `gorm:"column:totalBytes" json:"totalBytes"`
	Truncated         bool           `gorm:"column:truncated;default:false;not null" json:"truncated"`
	StorageKey        *string        `gorm:"column:storageKey" json:"storageKey"`
	ErrorCode         *string        `gorm:"column:errorCode" json:"errorCode"`
	StartedAt         *DateTime      `gorm:"column:startedAt" json:"startedAt"`
	FinishedAt        *DateTime      `gorm:"column:finishedAt" json:"finishedAt"`
	ExpiresAt         *DateTime      `gorm:"column:expiresAt;index:AttachmentExport_expiresAt_idx" json:"expiresAt"`
	Survey            *Survey        `gorm:"foreignKey:SurveyID,WorkspaceID;references:ID,WorkspaceID"`
	Workspace         *Workspace
	RequestedBy       *User `gorm:"foreignKey:RequestedByID"`
}

func (AttachmentExport) TableName() string {
	return "AttachmentExport"
}

func (m *AttachmentExport) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid2.Generate()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*AttachmentExport) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type FeedbackSource struct {
	ID                         string    `gorm:"column:id;primaryKey;uniqueIndex:FeedbackSource_id_workspaceId_key" json:"id"`
	CreatedAt                  DateTime  `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt                  DateTime  `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	Name                       string    `gorm:"column:name;uniqueIndex:FeedbackSource_workspaceId_name_key;not null" json:"name"`
	Type                       string    `gorm:"column:type;index:FeedbackSource_type_idx;not null" json:"type"`
	Status                     string    `gorm:"column:status;default:'active';not null" json:"status"`
	ImportMode                 string    `gorm:"column:import_mode;default:'completedOnly';not null" json:"import_mode"`
	ElementScope               string    `gorm:"column:elementScope;default:'specific';not null" json:"elementScope"`
	WorkspaceID                string    `gorm:"column:workspaceId;uniqueIndex:FeedbackSource_id_workspaceId_key;uniqueIndex:FeedbackSource_workspaceId_name_key;index:FeedbackSource_feedbackDirectoryId_workspaceId_idx;not null" json:"workspaceId"`
	FeedbackDirectoryID        string    `gorm:"column:feedbackDirectoryId;index:FeedbackSource_feedbackDirectoryId_workspaceId_idx;not null" json:"feedbackDirectoryId"`
	LastSyncAt                 *DateTime `gorm:"column:last_sync_at" json:"last_sync_at"`
	CreatedBy                  *string   `gorm:"column:created_by" json:"created_by"`
	Workspace                  *Workspace
	FeedbackDirectory          *FeedbackDirectory
	Creator                    *User                             `gorm:"foreignKey:CreatedBy"`
	FeedbackDirectoryWorkspace *FeedbackDirectoryWorkspace       `gorm:"foreignKey:FeedbackDirectoryID,WorkspaceID;references:FeedbackDirectoryID,WorkspaceID"`
	FormbricksMappings         []FeedbackSourceFormbricksMapping `gorm:"foreignKey:FeedbackSourceID,WorkspaceID;references:ID,WorkspaceID;constraint:OnDelete:CASCADE"`
	FieldMappings              []FeedbackSourceFieldMapping      `gorm:"foreignKey:FeedbackSourceID,WorkspaceID;references:ID,WorkspaceID;constraint:OnDelete:CASCADE"`
}

func (FeedbackSource) TableName() string {
	return "FeedbackSource"
}

func (m *FeedbackSource) BeforeCreate(tx *gorm.DB) error {
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

func (*FeedbackSource) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type FeedbackSourceFormbricksMapping struct {
	ID               string          `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt        DateTime        `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	FeedbackSourceID string          `gorm:"column:feedback_source_id;uniqueIndex:FeedbackSourceFormbricksMapping_workspaceId_feedback_source_id_surveyId_elementId_key;not null" json:"feedback_source_id"`
	WorkspaceID      string          `gorm:"column:workspaceId;uniqueIndex:FeedbackSourceFormbricksMapping_workspaceId_feedback_source_id_surveyId_elementId_key;index:FeedbackSourceFormbricksMapping_workspaceId_surveyId_idx;not null" json:"workspaceId"`
	SurveyID         string          `gorm:"column:surveyId;uniqueIndex:FeedbackSourceFormbricksMapping_workspaceId_feedback_source_id_surveyId_elementId_key;index:FeedbackSourceFormbricksMapping_workspaceId_surveyId_idx;index:FeedbackSourceFormbricksMapping_surveyId_idx;not null" json:"surveyId"`
	ElementID        string          `gorm:"column:elementId;uniqueIndex:FeedbackSourceFormbricksMapping_workspaceId_feedback_source_id_surveyId_elementId_key;not null" json:"elementId"`
	HubFieldType     string          `gorm:"column:hubFieldType;not null" json:"hubFieldType"`
	CustomFieldLabel *string         `gorm:"column:custom_field_label" json:"custom_field_label"`
	FeedbackSource   *FeedbackSource `gorm:"foreignKey:FeedbackSourceID,WorkspaceID;references:ID,WorkspaceID"`
	Survey           *Survey         `gorm:"foreignKey:SurveyID,WorkspaceID;references:ID,WorkspaceID"`
}

func (FeedbackSourceFormbricksMapping) TableName() string {
	return "FeedbackSourceFormbricksMapping"
}

func (m *FeedbackSourceFormbricksMapping) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type FeedbackSourceFieldMapping struct {
	ID               string          `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt        DateTime        `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	FeedbackSourceID string          `gorm:"column:feedback_source_id;uniqueIndex:FeedbackSourceFieldMapping_workspaceId_feedback_source_id_source_field_id_target_field_id_key;not null" json:"feedback_source_id"`
	WorkspaceID      string          `gorm:"column:workspaceId;uniqueIndex:FeedbackSourceFieldMapping_workspaceId_feedback_source_id_source_field_id_target_field_id_key;not null" json:"workspaceId"`
	SourceFieldID    string          `gorm:"column:source_field_id;uniqueIndex:FeedbackSourceFieldMapping_workspaceId_feedback_source_id_source_field_id_target_field_id_key;not null" json:"source_field_id"`
	TargetFieldID    string          `gorm:"column:target_field_id;uniqueIndex:FeedbackSourceFieldMapping_workspaceId_feedback_source_id_source_field_id_target_field_id_key;not null" json:"target_field_id"`
	StaticValue      *string         `gorm:"column:static_value" json:"static_value"`
	FeedbackSource   *FeedbackSource `gorm:"foreignKey:FeedbackSourceID,WorkspaceID;references:ID,WorkspaceID"`
}

func (FeedbackSourceFieldMapping) TableName() string {
	return "FeedbackSourceFieldMapping"
}

func (m *FeedbackSourceFieldMapping) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type FeedbackDirectory struct {
	ID              string   `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt       DateTime `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt       DateTime `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	Name            string   `gorm:"column:name;uniqueIndex:FeedbackDirectory_organizationId_name_key;not null" json:"name"`
	IsArchived      bool     `gorm:"column:isArchived;default:false;not null" json:"isArchived"`
	OrganizationID  string   `gorm:"column:organizationId;uniqueIndex:FeedbackDirectory_organizationId_name_key;not null" json:"organizationId"`
	Organization    *Organization
	Workspaces      []FeedbackDirectoryWorkspace `gorm:"foreignKey:FeedbackDirectoryID;constraint:OnDelete:CASCADE"`
	FeedbackSources []FeedbackSource             `gorm:"foreignKey:FeedbackDirectoryID;constraint:OnDelete:CASCADE"`
	Charts          []Chart                      `gorm:"foreignKey:FeedbackDirectoryID"`
}

func (FeedbackDirectory) TableName() string {
	return "FeedbackDirectory"
}

func (m *FeedbackDirectory) BeforeCreate(tx *gorm.DB) error {
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

func (*FeedbackDirectory) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type FeedbackDirectoryWorkspace struct {
	CreatedAt           DateTime `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt           DateTime `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	FeedbackDirectoryID string   `gorm:"column:feedbackDirectoryId;primaryKey" json:"feedbackDirectoryId"`
	WorkspaceID         string   `gorm:"column:workspaceId;primaryKey;index:FeedbackDirectoryWorkspace_workspaceId_idx" json:"workspaceId"`
	FeedbackDirectory   *FeedbackDirectory
	Workspace           *Workspace
	FeedbackSources     []FeedbackSource `gorm:"foreignKey:FeedbackDirectoryID,WorkspaceID;references:FeedbackDirectoryID,WorkspaceID;constraint:OnUpdate:CASCADE,OnDelete:NO ACTION"`
}

func (FeedbackDirectoryWorkspace) TableName() string {
	return "FeedbackDirectoryWorkspace"
}

func (m *FeedbackDirectoryWorkspace) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*FeedbackDirectoryWorkspace) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type AuthzedProjectionScopeState struct {
	Scope     string    `gorm:"column:scope;primaryKey" json:"scope"`
	ReadyAt   *DateTime `gorm:"column:readyAt" json:"readyAt"`
	ReadyBy   *string   `gorm:"column:readyBy" json:"readyBy"`
	UpdatedAt DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
}

func (AuthzedProjectionScopeState) TableName() string {
	return "AuthzedProjectionScopeState"
}

func (m *AuthzedProjectionScopeState) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*AuthzedProjectionScopeState) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type AuthzedProjectionOutbox struct {
	ID                string    `gorm:"column:id;primaryKey;type:char(36)" json:"id"`
	TargetType        string    `gorm:"column:targetType;not null" json:"targetType"`
	PrimaryID         string    `gorm:"column:primaryId;not null" json:"primaryId"`
	SecondaryID       *string   `gorm:"column:secondaryId" json:"secondaryId"`
	IsRevocation      bool      `gorm:"column:isRevocation;default:false;not null" json:"isRevocation"`
	Attempts          int       `gorm:"column:attempts;default:0;not null" json:"attempts"`
	PermanentFailures int       `gorm:"column:permanentFailures;default:0;not null" json:"permanentFailures"`
	AvailableAt       DateTime  `gorm:"column:availableAt;not null" json:"availableAt"`
	LeasedAt          *DateTime `gorm:"column:leasedAt" json:"leasedAt"`
	LeaseExpiresAt    *DateTime `gorm:"column:leaseExpiresAt" json:"leaseExpiresAt"`
	LeaseOwner        *string   `gorm:"column:leaseOwner" json:"leaseOwner"`
	ProcessedAt       *DateTime `gorm:"column:processedAt" json:"processedAt"`
	DeadLetteredAt    *DateTime `gorm:"column:deadLetteredAt" json:"deadLetteredAt"`
	LastAttemptAt     *DateTime `gorm:"column:lastAttemptAt" json:"lastAttemptAt"`
	LastErrorCode     *string   `gorm:"column:lastErrorCode" json:"lastErrorCode"`
	CreatedAt         DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt         DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
}

func (AuthzedProjectionOutbox) TableName() string {
	return "AuthzedProjectionOutbox"
}

func (m *AuthzedProjectionOutbox) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = uuid.NewString()
	}
	if m.AvailableAt.IsZero() {
		m.AvailableAt = DateTime{Time: now}
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*AuthzedProjectionOutbox) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type OnlineUpgradeState struct {
	Release              string    `gorm:"column:release;primaryKey" json:"release"`
	TargetImage          string    `gorm:"column:targetImage;not null" json:"targetImage"`
	Phase                string    `gorm:"column:phase;not null" json:"phase"`
	CompletedPhases      []string  `gorm:"column:completedPhases;serializer:json;not null" json:"completedPhases"`
	SurveyCursor         *string   `gorm:"column:surveyCursor" json:"surveyCursor"`
	SurveyCount          int64     `gorm:"column:surveyCount;default:0;not null" json:"surveyCount"`
	BaselineCompletedAt  *DateTime `gorm:"column:baselineCompletedAt" json:"baselineCompletedAt"`
	ForwardOnlyAt        *DateTime `gorm:"column:forwardOnlyAt" json:"forwardOnlyAt"`
	QueuePausedAt        *DateTime `gorm:"column:queuePausedAt" json:"queuePausedAt"`
	QueuePauseDeadlineAt *DateTime `gorm:"column:queuePauseDeadlineAt" json:"queuePauseDeadlineAt"`
	QueueResumedAt       *DateTime `gorm:"column:queueResumedAt" json:"queueResumedAt"`
	ActivatedAt          *DateTime `gorm:"column:activatedAt" json:"activatedAt"`
	UpdatedAt            DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
}

func (OnlineUpgradeState) TableName() string {
	return "OnlineUpgradeState"
}

func (m *OnlineUpgradeState) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*OnlineUpgradeState) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type RetentionPolicy struct {
	ID             string    `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt      DateTime  `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt      DateTime  `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	OrganizationID string    `gorm:"column:organizationId;uniqueIndex:RetentionPolicy_organizationId_entity_key;not null" json:"organizationId"`
	Entity         string    `gorm:"column:entity;uniqueIndex:RetentionPolicy_organizationId_entity_key;not null" json:"entity"`
	Enabled        bool      `gorm:"column:enabled;default:false;not null" json:"enabled"`
	EnabledAt      *DateTime `gorm:"column:enabledAt" json:"enabledAt"`
	WarnDays       *int      `gorm:"column:warnDays;default:60;not null" json:"warnDays"`
	PeriodDays     int       `gorm:"column:periodDays;not null" json:"periodDays"`
	Conditions     []string  `gorm:"column:conditions;serializer:json;not null" json:"conditions"`
	DeferredAt     *DateTime `gorm:"column:deferredAt" json:"deferredAt"`
	UpdatedByID    *string   `gorm:"column:updatedById;index:RetentionPolicy_updatedById_idx" json:"updatedById"`
	Organization   *Organization
	UpdatedBy      *User `gorm:"foreignKey:UpdatedByID"`
}

func (RetentionPolicy) TableName() string {
	return "RetentionPolicy"
}

func (m *RetentionPolicy) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid2.Generate()
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

type RetentionExemption struct {
	ID             string    `gorm:"column:id;primaryKey;index:RetentionExemption_organizationId_created_at_id_idx" json:"id"`
	CreatedAt      DateTime  `gorm:"column:created_at;index:RetentionExemption_organizationId_created_at_id_idx;autoCreateTime:false;not null" json:"created_at"`
	OrganizationID string    `gorm:"column:organizationId;index:RetentionExemption_organizationId_created_at_id_idx;not null" json:"organizationId"`
	Entity         string    `gorm:"column:entity;not null" json:"entity"`
	SurveyID       *string   `gorm:"column:surveyId;index:RetentionExemption_surveyId_idx" json:"surveyId"`
	Until          DateTime  `gorm:"column:until;not null" json:"until"`
	Source         string    `gorm:"column:source;default:'manual';not null" json:"source"`
	Reason         *string   `gorm:"column:reason" json:"reason"`
	CreatedByID    *string   `gorm:"column:createdById;index:RetentionExemption_createdById_idx" json:"createdById"`
	RevokedAt      *DateTime `gorm:"column:revokedAt" json:"revokedAt"`
	RevokedByID    *string   `gorm:"column:revokedById;index:RetentionExemption_revokedById_idx" json:"revokedById"`
	Organization   *Organization
	Survey         *Survey
	CreatedBy      *User `gorm:"foreignKey:CreatedByID"`
	RevokedBy      *User `gorm:"foreignKey:RevokedByID"`
}

func (RetentionExemption) TableName() string {
	return "RetentionExemption"
}

func (m *RetentionExemption) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid2.Generate()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type RetentionRun struct {
	ID             string    `gorm:"column:id;primaryKey;index:RetentionRun_organizationId_startedAt_id_idx" json:"id"`
	OrganizationID string    `gorm:"column:organizationId;index:RetentionRun_organizationId_startedAt_id_idx;not null" json:"organizationId"`
	Entity         string    `gorm:"column:entity;not null" json:"entity"`
	Kind           string    `gorm:"column:kind;default:'sweep';not null" json:"kind"`
	StartedAt      DateTime  `gorm:"column:startedAt;index:RetentionRun_organizationId_startedAt_id_idx;not null" json:"startedAt"`
	FinishedAt     *DateTime `gorm:"column:finishedAt" json:"finishedAt"`
	NotifiedCount  int       `gorm:"column:notifiedCount;default:0;not null" json:"notifiedCount"`
	ArchivedCount  int       `gorm:"column:archivedCount;default:0;not null" json:"archivedCount"`
	DeletedCount   int       `gorm:"column:deletedCount;default:0;not null" json:"deletedCount"`
	SkippedCount   int       `gorm:"column:skippedCount;default:0;not null" json:"skippedCount"`
	HasChanges     bool      `gorm:"column:hasChanges;default:false;not null" json:"hasChanges"`
	ScanCursor     *string   `gorm:"column:scanCursor" json:"scanCursor"`
	Organization   *Organization
	Items          []RetentionRunItem `gorm:"foreignKey:RunID;constraint:OnDelete:CASCADE"`
}

func (RetentionRun) TableName() string {
	return "RetentionRun"
}

func (m *RetentionRun) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid2.Generate()
	}
	if m.StartedAt.IsZero() {
		m.StartedAt = DateTime{Time: now}
	}
	return nil
}

type RetentionRunItem struct {
	ID         string        `gorm:"column:id;primaryKey;index:RetentionRunItem_runId_id_idx" json:"id"`
	RunID      string        `gorm:"column:runId;index:RetentionRunItem_runId_id_idx;not null" json:"runId"`
	TargetType string        `gorm:"column:targetType;not null" json:"targetType"`
	TargetID   string        `gorm:"column:targetId;index:RetentionRunItem_targetId_idx;not null" json:"targetId"`
	TargetName *string       `gorm:"column:targetName" json:"targetName"`
	Action     string        `gorm:"column:action;not null" json:"action"`
	Count      *int          `gorm:"column:count;default:1;not null" json:"count"`
	Recipient  *string       `gorm:"column:recipient" json:"recipient"`
	SkipReason *string       `gorm:"column:skipReason" json:"skipReason"`
	Run        *RetentionRun `gorm:"foreignKey:RunID"`
}

func (RetentionRunItem) TableName() string {
	return "RetentionRunItem"
}

func (m *RetentionRunItem) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = cuid2.Generate()
	}
	return nil
}

type RetentionNotice struct {
	ID             string    `gorm:"column:id;primaryKey" json:"id"`
	OrganizationID string    `gorm:"column:organizationId;uniqueIndex:RetentionNotice_userId_organizationId_entity_key;index:RetentionNotice_organizationId_idx;not null" json:"organizationId"`
	Entity         string    `gorm:"column:entity;uniqueIndex:RetentionNotice_surveyId_entity_key;uniqueIndex:RetentionNotice_userId_organizationId_entity_key;not null" json:"entity"`
	SurveyID       *string   `gorm:"column:surveyId;uniqueIndex:RetentionNotice_surveyId_entity_key" json:"surveyId"`
	UserID         *string   `gorm:"column:userId;uniqueIndex:RetentionNotice_userId_organizationId_entity_key" json:"userId"`
	SentAt         DateTime  `gorm:"column:sentAt;not null" json:"sentAt"`
	DeliveredAt    *DateTime `gorm:"column:deliveredAt" json:"deliveredAt"`
	EmailSent      bool      `gorm:"column:emailSent;default:false;not null" json:"emailSent"`
	ClaimToken     *string   `gorm:"column:claimToken" json:"claimToken"`
	ClockAt        *DateTime `gorm:"column:clockAt" json:"clockAt"`
	Organization   *Organization
	Survey         *Survey
	User           *User
}

func (RetentionNotice) TableName() string {
	return "RetentionNotice"
}

func (m *RetentionNotice) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid2.Generate()
	}
	if m.SentAt.IsZero() {
		m.SentAt = DateTime{Time: now}
	}
	return nil
}

type RetentionSurveyRestore struct {
	SurveyID   string   `gorm:"column:surveyId;primaryKey" json:"surveyId"`
	RestoredAt DateTime `gorm:"column:restoredAt;not null" json:"restoredAt"`
	Survey     *Survey
}

func (RetentionSurveyRestore) TableName() string {
	return "RetentionSurveyRestore"
}

type Workflow struct {
	ID          string         `gorm:"column:id;primaryKey;uniqueIndex:Workflow_id_workspaceId_key" json:"id"`
	CreatedAt   DateTime       `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt   DateTime       `gorm:"column:updated_at;index:Workflow_workspaceId_updated_at_idx;autoUpdateTime:false;not null" json:"updated_at"`
	Name        string         `gorm:"column:name;not null" json:"name"`
	Description *string        `gorm:"column:description" json:"description"`
	Status      string         `gorm:"column:status;index:Workflow_workspaceId_status_idx;default:'draft';not null" json:"status"`
	WorkspaceID string         `gorm:"column:workspaceId;uniqueIndex:Workflow_id_workspaceId_key;index:Workflow_workspaceId_status_idx;index:Workflow_workspaceId_updated_at_idx;not null" json:"workspaceId"`
	CreatedBy   *string        `gorm:"column:createdBy" json:"createdBy"`
	Definition  datatypes.JSON `gorm:"column:definition;not null" json:"definition"`
	Workspace   *Workspace
	Creator     *User             `gorm:"foreignKey:CreatedBy"`
	Versions    []WorkflowVersion `gorm:"foreignKey:WorkflowID,WorkspaceID;references:ID,WorkspaceID;constraint:OnDelete:CASCADE"`
	Runs        []WorkflowRun     `gorm:"foreignKey:WorkflowID,WorkspaceID;references:ID,WorkspaceID;constraint:OnDelete:CASCADE"`
}

func (Workflow) TableName() string {
	return "Workflow"
}

func (m *Workflow) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid2.Generate()
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
	ID          string         `gorm:"column:id;primaryKey" json:"id"`
	WorkflowID  string         `gorm:"column:workflowId;uniqueIndex:WorkflowVersion_workflowId_version_key;index:WorkflowVersion_workflowId_workspaceId_idx;not null" json:"workflowId"`
	WorkspaceID string         `gorm:"column:workspaceId;index:WorkflowVersion_workflowId_workspaceId_idx;not null" json:"workspaceId"`
	Version     int            `gorm:"column:version;uniqueIndex:WorkflowVersion_workflowId_version_key;not null" json:"version"`
	Definition  datatypes.JSON `gorm:"column:definition;not null" json:"definition"`
	PublishedAt DateTime       `gorm:"column:publishedAt;not null" json:"publishedAt"`
	PublishedBy *string        `gorm:"column:publishedBy;index:WorkflowVersion_publishedBy_idx" json:"publishedBy"`
	Workflow    *Workflow      `gorm:"foreignKey:WorkflowID,WorkspaceID;references:ID,WorkspaceID"`
	Publisher   *User          `gorm:"foreignKey:PublishedBy"`
	Runs        []WorkflowRun  `gorm:"foreignKey:WorkflowVersionID;constraint:OnDelete:SET NULL"`
}

func (WorkflowVersion) TableName() string {
	return "WorkflowVersion"
}

func (m *WorkflowVersion) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid2.Generate()
	}
	if m.PublishedAt.IsZero() {
		m.PublishedAt = DateTime{Time: now}
	}
	return nil
}

type WorkflowRun struct {
	ID                string         `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt         DateTime       `gorm:"column:created_at;index:WorkflowRun_workspaceId_created_at_idx;index:WorkflowRun_workflowId_workspaceId_created_at_idx;index:WorkflowRun_status_created_at_idx;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt         DateTime       `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	WorkflowID        string         `gorm:"column:workflowId;uniqueIndex:WorkflowRun_workflowId_idempotencyKey_key;index:WorkflowRun_workflowId_workspaceId_created_at_idx;not null" json:"workflowId"`
	WorkspaceID       string         `gorm:"column:workspaceId;index:WorkflowRun_workspaceId_created_at_idx;index:WorkflowRun_workflowId_workspaceId_created_at_idx;not null" json:"workspaceId"`
	WorkflowVersionID *string        `gorm:"column:workflowVersionId" json:"workflowVersionId"`
	ResponseID        *string        `gorm:"column:responseId;index:WorkflowRun_responseId_idx" json:"responseId"`
	Status            string         `gorm:"column:status;index:WorkflowRun_status_created_at_idx;default:'queued';not null" json:"status"`
	TriggerType       string         `gorm:"column:triggerType;not null" json:"triggerType"`
	SurveyID          *string        `gorm:"column:surveyId" json:"surveyId"`
	IsDryRun          bool           `gorm:"column:isDryRun;index:WorkflowRun_isDryRun_idx;default:false;not null" json:"isDryRun"`
	IdempotencyKey    *string        `gorm:"column:idempotencyKey;uniqueIndex:WorkflowRun_workflowId_idempotencyKey_key" json:"idempotencyKey"`
	Attempt           int            `gorm:"column:attempt;default:0;not null" json:"attempt"`
	NextAttemptAt     *DateTime      `gorm:"column:nextAttemptAt;index:WorkflowRun_nextAttemptAt_idx" json:"nextAttemptAt"`
	LastErrorAt       *DateTime      `gorm:"column:lastErrorAt" json:"lastErrorAt"`
	TriggerPayload    datatypes.JSON `gorm:"column:triggerPayload;not null" json:"triggerPayload"`
	Data              datatypes.JSON `gorm:"column:data;default:'{}';not null" json:"data"`
	Error             *string        `gorm:"column:error" json:"error"`
	DispatchedAt      *DateTime      `gorm:"column:dispatchedAt" json:"dispatchedAt"`
	StartedAt         *DateTime      `gorm:"column:startedAt" json:"startedAt"`
	FinishedAt        *DateTime      `gorm:"column:finishedAt" json:"finishedAt"`
	Workflow          *Workflow      `gorm:"foreignKey:WorkflowID,WorkspaceID;references:ID,WorkspaceID"`
	Workspace         *Workspace
	WorkflowVersion   *WorkflowVersion
	Response          *Response
	Logs              []WorkflowRunLog `gorm:"foreignKey:RunID;constraint:OnDelete:CASCADE"`
}

func (WorkflowRun) TableName() string {
	return "WorkflowRun"
}

func (m *WorkflowRun) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid2.Generate()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*WorkflowRun) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type WorkflowRunLog struct {
	ID         string         `gorm:"column:id;primaryKey" json:"id"`
	RunID      string         `gorm:"column:runId;uniqueIndex:WorkflowRunLog_runId_stepId_key;index:WorkflowRunLog_runId_sequence_idx;not null" json:"runId"`
	Sequence   int            `gorm:"column:sequence;index:WorkflowRunLog_runId_sequence_idx;not null" json:"sequence"`
	StepID     string         `gorm:"column:stepId;uniqueIndex:WorkflowRunLog_runId_stepId_key;not null" json:"stepId"`
	StepType   string         `gorm:"column:stepType;not null" json:"stepType"`
	Status     string         `gorm:"column:status;default:'pending';not null" json:"status"`
	Input      datatypes.JSON `gorm:"column:input;default:'{}';not null" json:"input"`
	Output     datatypes.JSON `gorm:"column:output;default:'{}';not null" json:"output"`
	Error      *string        `gorm:"column:error" json:"error"`
	StartedAt  *DateTime      `gorm:"column:startedAt" json:"startedAt"`
	FinishedAt *DateTime      `gorm:"column:finishedAt" json:"finishedAt"`
	Run        *WorkflowRun   `gorm:"foreignKey:RunID"`
}

func (WorkflowRunLog) TableName() string {
	return "WorkflowRunLog"
}

func (m *WorkflowRunLog) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = cuid2.Generate()
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

type Date struct{ time.Time }

func (Date) GormDataType() string {
	return "time"
}

func (date Date) Value() (driver.Value, error) {
	return date.UTC().Format("2006-01-02"), nil
}

func (date *Date) Scan(src any) error {
	read, err := readPrismaTime(src)
	if err != nil || read.IsZero() {
		date.Time = time.Time{}
		return err
	}
	date.Time = time.Date(read.Year(), read.Month(), read.Day(), 0, 0, 0, 0, time.UTC)
	return nil
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
