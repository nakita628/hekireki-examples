package model

import (
	"database/sql/driver"
	"fmt"
	"strconv"
	"time"

	"github.com/google/uuid"
	"gorm.io/datatypes"
	"gorm.io/gorm"
)

type User struct {
	ID            string       `gorm:"column:id;primaryKey;type:char(36)" json:"id"`
	Email         string       `gorm:"column:email;uniqueIndex;not null" json:"email"`
	Password      *string      `gorm:"column:password" json:"password"`
	Type          string       `gorm:"column:type;not null" json:"type"`
	EmailVerified bool         `gorm:"column:emailVerified;default:false;not null" json:"emailVerified"`
	CreatedAt     DateTime     `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt     DateTime     `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Memberships   []Membership `gorm:"foreignKey:UserID"`
}

func (User) TableName() string {
	return "users"
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

type Project struct {
	ID                            string           `gorm:"column:id;primaryKey;type:char(36)" json:"id"`
	Name                          string           `gorm:"column:name;not null" json:"name"`
	Public                        string           `gorm:"column:public;uniqueIndex;not null" json:"public"`
	Secret                        string           `gorm:"column:secret;uniqueIndex;not null" json:"secret"`
	Disabled                      bool             `gorm:"column:disabled;default:false;not null" json:"disabled"`
	DisabledReason                *string          `gorm:"column:disabledReason" json:"disabledReason"`
	Customer                      *string          `gorm:"column:customer;uniqueIndex" json:"customer"`
	Subscription                  *string          `gorm:"column:subscription;uniqueIndex" json:"subscription"`
	CardVerification              *string          `gorm:"column:cardVerification;index:projects_cardVerification_cardVerificationAt_idx" json:"cardVerification"`
	CardVerificationIntent        *string          `gorm:"column:cardVerificationIntent" json:"cardVerificationIntent"`
	CardVerificationAt            *DateTime        `gorm:"column:cardVerificationAt;index:projects_cardVerification_cardVerificationAt_idx" json:"cardVerificationAt"`
	CardVerificationSession       *string          `gorm:"column:cardVerificationSession" json:"cardVerificationSession"`
	CardVerificationPaymentMethod *string          `gorm:"column:cardVerificationPaymentMethod" json:"cardVerificationPaymentMethod"`
	CardVerificationFingerprint   *string          `gorm:"column:cardVerificationFingerprint" json:"cardVerificationFingerprint"`
	CardVerificationAttempts      int              `gorm:"column:cardVerificationAttempts;default:0;not null" json:"cardVerificationAttempts"`
	BillingLimitWorkflows         *int             `gorm:"column:billingLimitWorkflows" json:"billingLimitWorkflows"`
	BillingLimitCampaigns         *int             `gorm:"column:billingLimitCampaigns" json:"billingLimitCampaigns"`
	BillingLimitTransactional     *int             `gorm:"column:billingLimitTransactional" json:"billingLimitTransactional"`
	BillingLimitInbound           *int             `gorm:"column:billingLimitInbound" json:"billingLimitInbound"`
	Tracking                      string           `gorm:"column:tracking;default:'ENABLED';not null" json:"tracking"`
	Language                      *string          `gorm:"column:language;default:'en';not null" json:"language"`
	CreatedAt                     DateTime         `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt                     DateTime         `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Members                       []Membership     `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	Contacts                      []Contact        `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	Templates                     []Template       `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	Segments                      []Segment        `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	Workflows                     []Workflow       `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	Campaigns                     []Campaign       `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	Emails                        []Email          `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	Events                        []Event          `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	Domains                       []Domain         `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	APIRequests                   []APIRequest     `gorm:"foreignKey:ProjectID;constraint:OnDelete:SET NULL"`
	IdempotencyKeys               []IdempotencyKey `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	ThrottleClaims                []ThrottleClaim  `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
}

func (Project) TableName() string {
	return "projects"
}

func (m *Project) BeforeCreate(tx *gorm.DB) error {
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

func (*Project) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Membership struct {
	UserID    string   `gorm:"column:userId;primaryKey" json:"userId"`
	ProjectID string   `gorm:"column:projectId;primaryKey" json:"projectId"`
	Role      string   `gorm:"column:role;default:'MEMBER';not null" json:"role"`
	CreatedAt DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	User      *User
	Project   *Project
}

func (Membership) TableName() string {
	return "memberships"
}

func (m *Membership) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*Membership) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Domain struct {
	ID         string         `gorm:"column:id;primaryKey;type:char(36)" json:"id"`
	Domain     string         `gorm:"column:domain;uniqueIndex:domains_projectId_domain_key;not null" json:"domain"`
	Verified   bool           `gorm:"column:verified;index:domains_projectId_verified_idx;default:false;not null" json:"verified"`
	DkimTokens datatypes.JSON `gorm:"column:dkimTokens" json:"dkimTokens"`
	ProjectID  string         `gorm:"column:projectId;uniqueIndex:domains_projectId_domain_key;index:domains_projectId_idx;index:domains_projectId_verified_idx;not null" json:"projectId"`
	CreatedAt  DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt  DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Project    *Project
}

func (Domain) TableName() string {
	return "domains"
}

func (m *Domain) BeforeCreate(tx *gorm.DB) error {
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

func (*Domain) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Contact struct {
	ID                 string         `gorm:"column:id;primaryKey;type:char(36)" json:"id"`
	Email              string         `gorm:"column:email;uniqueIndex:contacts_projectId_email_key;not null" json:"email"`
	Data               datatypes.JSON `gorm:"column:data;index:contacts_data_idx" json:"data"`
	Subscribed         *bool          `gorm:"column:subscribed;index:contacts_projectId_subscribed_idx;default:true;not null" json:"subscribed"`
	SnoozedUntil       *DateTime      `gorm:"column:snoozedUntil;index:contacts_snoozedUntil_idx" json:"snoozedUntil"`
	ProjectID          string         `gorm:"column:projectId;uniqueIndex:contacts_projectId_email_key;index:contacts_projectId_idx;index:contacts_projectId_subscribed_idx;not null" json:"projectId"`
	CreatedAt          DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt          DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Project            *Project
	Emails             []Email             `gorm:"foreignKey:ContactID;constraint:OnDelete:CASCADE"`
	WorkflowExecutions []WorkflowExecution `gorm:"foreignKey:ContactID;constraint:OnDelete:CASCADE"`
	Events             []Event             `gorm:"foreignKey:ContactID;constraint:OnDelete:CASCADE"`
	SegmentMemberships []SegmentMembership `gorm:"foreignKey:ContactID;constraint:OnDelete:CASCADE"`
	ThrottleClaims     []ThrottleClaim     `gorm:"foreignKey:ContactID;constraint:OnDelete:CASCADE"`
}

func (Contact) TableName() string {
	return "contacts"
}

func (m *Contact) BeforeCreate(tx *gorm.DB) error {
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

func (*Contact) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Template struct {
	ID            string   `gorm:"column:id;primaryKey;type:char(36)" json:"id"`
	Name          string   `gorm:"column:name;not null" json:"name"`
	Description   *string  `gorm:"column:description" json:"description"`
	Subject       string   `gorm:"column:subject;not null" json:"subject"`
	Body          string   `gorm:"column:body;not null" json:"body"`
	From          string   `gorm:"column:from;not null" json:"from"`
	FromName      *string  `gorm:"column:fromName" json:"fromName"`
	ReplyTo       *string  `gorm:"column:replyTo" json:"replyTo"`
	Type          string   `gorm:"column:type;index:templates_projectId_type_idx;default:'MARKETING';not null" json:"type"`
	ProjectID     string   `gorm:"column:projectId;index:templates_projectId_idx;index:templates_projectId_type_idx;not null" json:"projectId"`
	CreatedAt     DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt     DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Project       *Project
	Emails        []Email        `gorm:"foreignKey:TemplateID"`
	WorkflowSteps []WorkflowStep `gorm:"foreignKey:TemplateID"`
}

func (Template) TableName() string {
	return "templates"
}

func (m *Template) BeforeCreate(tx *gorm.DB) error {
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

func (*Template) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Segment struct {
	ID              string         `gorm:"column:id;primaryKey;type:char(36)" json:"id"`
	Name            string         `gorm:"column:name;not null" json:"name"`
	Description     *string        `gorm:"column:description" json:"description"`
	Type            string         `gorm:"column:type;default:'DYNAMIC';not null" json:"type"`
	Condition       datatypes.JSON `gorm:"column:condition;index:segments_condition_idx" json:"condition"`
	TrackMembership bool           `gorm:"column:trackMembership;default:false;not null" json:"trackMembership"`
	MemberCount     int            `gorm:"column:memberCount;default:0;not null" json:"memberCount"`
	ProjectID       string         `gorm:"column:projectId;index:segments_projectId_idx;not null" json:"projectId"`
	CreatedAt       DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt       DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Project         *Project
	Memberships     []SegmentMembership `gorm:"foreignKey:SegmentID;constraint:OnDelete:CASCADE"`
	Campaigns       []Campaign          `gorm:"foreignKey:SegmentID"`
}

func (Segment) TableName() string {
	return "segments"
}

func (m *Segment) BeforeCreate(tx *gorm.DB) error {
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

func (*Segment) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type SegmentMembership struct {
	ContactID string    `gorm:"column:contactId;primaryKey;index:segment_memberships_contactId_exitedAt_idx" json:"contactId"`
	SegmentID string    `gorm:"column:segmentId;primaryKey;index:segment_memberships_segmentId_exitedAt_idx" json:"segmentId"`
	EnteredAt DateTime  `gorm:"column:enteredAt;index:segment_memberships_enteredAt_idx;not null" json:"enteredAt"`
	ExitedAt  *DateTime `gorm:"column:exitedAt;index:segment_memberships_segmentId_exitedAt_idx;index:segment_memberships_contactId_exitedAt_idx" json:"exitedAt"`
	CreatedAt DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Contact   *Contact
	Segment   *Segment
}

func (SegmentMembership) TableName() string {
	return "segment_memberships"
}

func (m *SegmentMembership) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.EnteredAt.IsZero() {
		m.EnteredAt = DateTime{Time: now}
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*SegmentMembership) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Campaign struct {
	ID                string         `gorm:"column:id;primaryKey;type:char(36)" json:"id"`
	Name              string         `gorm:"column:name;not null" json:"name"`
	Description       *string        `gorm:"column:description" json:"description"`
	Status            string         `gorm:"column:status;index:campaigns_projectId_status_idx;default:'DRAFT';not null" json:"status"`
	Subject           string         `gorm:"column:subject;not null" json:"subject"`
	Body              string         `gorm:"column:body;type:text;not null" json:"body"`
	From              string         `gorm:"column:from;not null" json:"from"`
	FromName          *string        `gorm:"column:fromName" json:"fromName"`
	ReplyTo           *string        `gorm:"column:replyTo" json:"replyTo"`
	Type              string         `gorm:"column:type;default:'MARKETING';not null" json:"type"`
	AudienceType      string         `gorm:"column:audienceType;default:'ALL';not null" json:"audienceType"`
	AudienceCondition datatypes.JSON `gorm:"column:audienceCondition;index:campaigns_audienceCondition_idx" json:"audienceCondition"`
	SegmentID         *string        `gorm:"column:segmentId;index:campaigns_segmentId_idx" json:"segmentId"`
	ScheduledFor      *DateTime      `gorm:"column:scheduledFor;index:campaigns_scheduledFor_idx" json:"scheduledFor"`
	TotalRecipients   int            `gorm:"column:totalRecipients;default:0;not null" json:"totalRecipients"`
	SentCount         int            `gorm:"column:sentCount;default:0;not null" json:"sentCount"`
	DeliveredCount    int            `gorm:"column:deliveredCount;default:0;not null" json:"deliveredCount"`
	OpenedCount       int            `gorm:"column:openedCount;default:0;not null" json:"openedCount"`
	ClickedCount      int            `gorm:"column:clickedCount;default:0;not null" json:"clickedCount"`
	BouncedCount      int            `gorm:"column:bouncedCount;default:0;not null" json:"bouncedCount"`
	ComplainedCount   int            `gorm:"column:complainedCount;default:0;not null" json:"complainedCount"`
	UnsubscribedCount int            `gorm:"column:unsubscribedCount;default:0;not null" json:"unsubscribedCount"`
	Archived          bool           `gorm:"column:archived;index:campaigns_projectId_archived_createdAt_idx;default:false;not null" json:"archived"`
	ProjectID         string         `gorm:"column:projectId;index:campaigns_projectId_status_idx;index:campaigns_projectId_archived_createdAt_idx;not null" json:"projectId"`
	SentAt            *DateTime      `gorm:"column:sentAt" json:"sentAt"`
	CreatedAt         DateTime       `gorm:"column:createdAt;index:campaigns_projectId_archived_createdAt_idx;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt         DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Segment           *Segment
	Project           *Project
	Emails            []Email `gorm:"foreignKey:CampaignID;constraint:OnDelete:CASCADE"`
}

func (Campaign) TableName() string {
	return "campaigns"
}

func (m *Campaign) BeforeCreate(tx *gorm.DB) error {
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

func (*Campaign) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Workflow struct {
	ID                    string         `gorm:"column:id;primaryKey;type:char(36)" json:"id"`
	Name                  string         `gorm:"column:name;not null" json:"name"`
	Description           *string        `gorm:"column:description" json:"description"`
	Enabled               bool           `gorm:"column:enabled;index:workflows_projectId_enabled_idx;default:false;not null" json:"enabled"`
	TriggerType           string         `gorm:"column:triggerType;not null" json:"triggerType"`
	TriggerConfig         datatypes.JSON `gorm:"column:triggerConfig" json:"triggerConfig"`
	AllowReentry          bool           `gorm:"column:allowReentry;default:false;not null" json:"allowReentry"`
	ThrottleWindowSeconds *int           `gorm:"column:throttleWindowSeconds" json:"throttleWindowSeconds"`
	ThrottleKey           *string        `gorm:"column:throttleKey" json:"throttleKey"`
	ProjectID             string         `gorm:"column:projectId;index:workflows_projectId_idx;index:workflows_projectId_enabled_idx;not null" json:"projectId"`
	CreatedAt             DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt             DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Project               *Project
	Steps                 []WorkflowStep      `gorm:"foreignKey:WorkflowID;constraint:OnDelete:CASCADE"`
	Executions            []WorkflowExecution `gorm:"foreignKey:WorkflowID;constraint:OnDelete:CASCADE"`
	ThrottleClaims        []ThrottleClaim     `gorm:"foreignKey:WorkflowID;constraint:OnDelete:CASCADE"`
}

func (Workflow) TableName() string {
	return "workflows"
}

func (m *Workflow) BeforeCreate(tx *gorm.DB) error {
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

func (*Workflow) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type WorkflowStep struct {
	ID                        string         `gorm:"column:id;primaryKey;type:char(36)" json:"id"`
	Type                      string         `gorm:"column:type;not null" json:"type"`
	Name                      string         `gorm:"column:name;not null" json:"name"`
	Position                  datatypes.JSON `gorm:"column:position;not null" json:"position"`
	Config                    datatypes.JSON `gorm:"column:config;not null" json:"config"`
	WorkflowID                string         `gorm:"column:workflowId;index:workflow_steps_workflowId_idx;not null" json:"workflowId"`
	TemplateID                *string        `gorm:"column:templateId;index:workflow_steps_templateId_idx" json:"templateId"`
	CreatedAt                 DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt                 DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Workflow                  *Workflow
	Template                  *Template
	OutgoingTransitions       []WorkflowTransition    `gorm:"foreignKey:FromStepID;constraint:OnDelete:CASCADE"`
	IncomingTransitions       []WorkflowTransition    `gorm:"foreignKey:ToStepID;constraint:OnDelete:CASCADE"`
	Executions                []WorkflowStepExecution `gorm:"foreignKey:StepID;constraint:OnDelete:CASCADE"`
	CurrentWorkflowExecutions []WorkflowExecution     `gorm:"foreignKey:CurrentStepID"`
}

func (WorkflowStep) TableName() string {
	return "workflow_steps"
}

func (m *WorkflowStep) BeforeCreate(tx *gorm.DB) error {
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

func (*WorkflowStep) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type WorkflowTransition struct {
	ID         string         `gorm:"column:id;primaryKey;type:char(36)" json:"id"`
	FromStepID string         `gorm:"column:fromStepId;index:workflow_transitions_fromStepId_idx;not null" json:"fromStepId"`
	ToStepID   string         `gorm:"column:toStepId;index:workflow_transitions_toStepId_idx;not null" json:"toStepId"`
	Condition  datatypes.JSON `gorm:"column:condition" json:"condition"`
	Priority   int            `gorm:"column:priority;default:0;not null" json:"priority"`
	CreatedAt  DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt  DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	FromStep   *WorkflowStep  `gorm:"foreignKey:FromStepID"`
	ToStep     *WorkflowStep  `gorm:"foreignKey:ToStepID"`
}

func (WorkflowTransition) TableName() string {
	return "workflow_transitions"
}

func (m *WorkflowTransition) BeforeCreate(tx *gorm.DB) error {
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

func (*WorkflowTransition) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type WorkflowExecution struct {
	ID             string         `gorm:"column:id;primaryKey;type:char(36)" json:"id"`
	WorkflowID     string         `gorm:"column:workflowId;index:workflow_executions_workflowId_contactId_idx;index:workflow_executions_workflowId_status_idx;not null" json:"workflowId"`
	ContactID      string         `gorm:"column:contactId;index:workflow_executions_workflowId_contactId_idx;index:workflow_executions_contactId_status_idx;not null" json:"contactId"`
	Status         string         `gorm:"column:status;index:workflow_executions_workflowId_status_idx;index:workflow_executions_contactId_status_idx;index:workflow_executions_status_currentStepId_idx;default:'RUNNING';not null" json:"status"`
	CurrentStepID  *string        `gorm:"column:currentStepId;index:workflow_executions_status_currentStepId_idx" json:"currentStepId"`
	ExitReason     *string        `gorm:"column:exitReason" json:"exitReason"`
	Context        datatypes.JSON `gorm:"column:context" json:"context"`
	StartedAt      DateTime       `gorm:"column:startedAt;not null" json:"startedAt"`
	CompletedAt    *DateTime      `gorm:"column:completedAt" json:"completedAt"`
	CreatedAt      DateTime       `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt      DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Workflow       *Workflow
	Contact        *Contact
	CurrentStep    *WorkflowStep           `gorm:"foreignKey:CurrentStepID"`
	StepExecutions []WorkflowStepExecution `gorm:"foreignKey:ExecutionID;constraint:OnDelete:CASCADE"`
	Emails         []Email                 `gorm:"foreignKey:WorkflowExecutionID;constraint:OnDelete:CASCADE"`
}

func (WorkflowExecution) TableName() string {
	return "workflow_executions"
}

func (m *WorkflowExecution) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = uuid.NewString()
	}
	if m.StartedAt.IsZero() {
		m.StartedAt = DateTime{Time: now}
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*WorkflowExecution) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type WorkflowStepExecution struct {
	ID           string             `gorm:"column:id;primaryKey;type:char(36)" json:"id"`
	ExecutionID  string             `gorm:"column:executionId;index:workflow_step_executions_executionId_status_idx;not null" json:"executionId"`
	StepID       string             `gorm:"column:stepId;index:workflow_step_executions_stepId_idx;not null" json:"stepId"`
	Status       string             `gorm:"column:status;index:workflow_step_executions_executionId_status_idx;index:workflow_step_executions_status_scheduledFor_idx;default:'PENDING';not null" json:"status"`
	ScheduledFor *DateTime          `gorm:"column:scheduledFor;index:workflow_step_executions_status_scheduledFor_idx;index:workflow_step_executions_scheduledFor_idx" json:"scheduledFor"`
	ExecuteAfter *DateTime          `gorm:"column:executeAfter" json:"executeAfter"`
	Output       datatypes.JSON     `gorm:"column:output" json:"output"`
	Error        *string            `gorm:"column:error" json:"error"`
	StartedAt    *DateTime          `gorm:"column:startedAt" json:"startedAt"`
	CompletedAt  *DateTime          `gorm:"column:completedAt" json:"completedAt"`
	CreatedAt    DateTime           `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt    DateTime           `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Execution    *WorkflowExecution `gorm:"foreignKey:ExecutionID"`
	Step         *WorkflowStep      `gorm:"foreignKey:StepID"`
	Emails       []Email            `gorm:"foreignKey:WorkflowStepExecutionID;constraint:OnDelete:CASCADE"`
}

func (WorkflowStepExecution) TableName() string {
	return "workflow_step_executions"
}

func (m *WorkflowStepExecution) BeforeCreate(tx *gorm.DB) error {
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

func (*WorkflowStepExecution) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Email struct {
	ID                      string         `gorm:"column:id;primaryKey;type:char(36)" json:"id"`
	ContactID               string         `gorm:"column:contactId;index:emails_projectId_contactId_idx;index:emails_contactId_idx;index:emails_contactId_openedAt_idx;index:emails_contactId_clickedAt_idx;index:emails_contactId_bouncedAt_idx;index:emails_contactId_complainedAt_idx;not null" json:"contactId"`
	ToName                  *string        `gorm:"column:toName" json:"toName"`
	Subject                 string         `gorm:"column:subject;not null" json:"subject"`
	Body                    string         `gorm:"column:body;not null" json:"body"`
	From                    string         `gorm:"column:from;not null" json:"from"`
	FromName                *string        `gorm:"column:fromName" json:"fromName"`
	ReplyTo                 *string        `gorm:"column:replyTo" json:"replyTo"`
	Headers                 datatypes.JSON `gorm:"column:headers" json:"headers"`
	Attachments             datatypes.JSON `gorm:"column:attachments" json:"attachments"`
	MessageID               *string        `gorm:"column:messageId;uniqueIndex" json:"messageId"`
	SourceType              string         `gorm:"column:sourceType;index:emails_projectId_sourceType_createdAt_idx;not null" json:"sourceType"`
	TemplateID              *string        `gorm:"column:templateId" json:"templateId"`
	CampaignID              *string        `gorm:"column:campaignId;index:emails_campaignId_idx;index:emails_campaignId_status_idx" json:"campaignId"`
	WorkflowExecutionID     *string        `gorm:"column:workflowExecutionId;index:emails_workflowExecutionId_idx" json:"workflowExecutionId"`
	WorkflowStepExecutionID *string        `gorm:"column:workflowStepExecutionId;index:emails_workflowStepExecutionId_idx" json:"workflowStepExecutionId"`
	Status                  string         `gorm:"column:status;index:emails_campaignId_status_idx;index:emails_status_idx;default:'PENDING';not null" json:"status"`
	ScheduledFor            *DateTime      `gorm:"column:scheduledFor" json:"scheduledFor"`
	SentAt                  *DateTime      `gorm:"column:sentAt" json:"sentAt"`
	DeliveredAt             *DateTime      `gorm:"column:deliveredAt" json:"deliveredAt"`
	OpenedAt                *DateTime      `gorm:"column:openedAt;index:emails_contactId_openedAt_idx" json:"openedAt"`
	ClickedAt               *DateTime      `gorm:"column:clickedAt;index:emails_contactId_clickedAt_idx" json:"clickedAt"`
	BouncedAt               *DateTime      `gorm:"column:bouncedAt;index:emails_contactId_bouncedAt_idx" json:"bouncedAt"`
	ComplainedAt            *DateTime      `gorm:"column:complainedAt;index:emails_contactId_complainedAt_idx" json:"complainedAt"`
	UnsubscribedAt          *DateTime      `gorm:"column:unsubscribedAt" json:"unsubscribedAt"`
	Opens                   int            `gorm:"column:opens;default:0;not null" json:"opens"`
	Clicks                  int            `gorm:"column:clicks;default:0;not null" json:"clicks"`
	Simulated               bool           `gorm:"column:simulated;default:false;not null" json:"simulated"`
	Error                   *string        `gorm:"column:error" json:"error"`
	ProjectID               string         `gorm:"column:projectId;index:emails_projectId_contactId_idx;index:emails_projectId_sourceType_createdAt_idx;not null" json:"projectId"`
	CreatedAt               DateTime       `gorm:"column:createdAt;index:emails_createdAt_idx;index:emails_projectId_sourceType_createdAt_idx;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt               DateTime       `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Contact                 *Contact
	Template                *Template
	Campaign                *Campaign
	WorkflowExecution       *WorkflowExecution
	WorkflowStepExecution   *WorkflowStepExecution
	Project                 *Project
	Events                  []Event `gorm:"foreignKey:EmailID;constraint:OnDelete:CASCADE"`
}

func (Email) TableName() string {
	return "emails"
}

func (m *Email) BeforeCreate(tx *gorm.DB) error {
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

func (*Email) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Event struct {
	ID        string         `gorm:"column:id;primaryKey;type:char(36)" json:"id"`
	Name      string         `gorm:"column:name;index:events_projectId_name_idx;index:events_projectId_contactId_name_createdAt_idx;index:events_contactId_name_createdAt_idx;index:events_projectId_name_createdAt_idx;not null" json:"name"`
	Data      datatypes.JSON `gorm:"column:data;index:events_data_idx" json:"data"`
	ProjectID string         `gorm:"column:projectId;index:events_projectId_name_idx;index:events_projectId_contactId_name_createdAt_idx;index:events_projectId_name_createdAt_idx;not null" json:"projectId"`
	ContactID *string        `gorm:"column:contactId;index:events_contactId_idx;index:events_projectId_contactId_name_createdAt_idx;index:events_contactId_name_createdAt_idx;index:events_contactId_createdAt_idx" json:"contactId"`
	EmailID   *string        `gorm:"column:emailId;index:events_emailId_idx" json:"emailId"`
	CreatedAt DateTime       `gorm:"column:createdAt;index:events_createdAt_idx;index:events_projectId_contactId_name_createdAt_idx;index:events_contactId_name_createdAt_idx;index:events_projectId_name_createdAt_idx;index:events_contactId_createdAt_idx;autoCreateTime:false;not null" json:"createdAt"`
	Project   *Project
	Contact   *Contact
	Email     *Email
}

func (Event) TableName() string {
	return "events"
}

func (m *Event) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = uuid.NewString()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type IdempotencyKey struct {
	ID         string   `gorm:"column:id;primaryKey;type:char(36)" json:"id"`
	Key        string   `gorm:"column:key;uniqueIndex:idempotency_keys_projectId_key_key;not null" json:"key"`
	ProjectID  string   `gorm:"column:projectId;uniqueIndex:idempotency_keys_projectId_key_key;not null" json:"projectId"`
	Method     string   `gorm:"column:method;not null" json:"method"`
	Path       string   `gorm:"column:path;not null" json:"path"`
	StatusCode *int     `gorm:"column:statusCode" json:"statusCode"`
	CreatedAt  DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	ExpiresAt  DateTime `gorm:"column:expiresAt;index:idempotency_keys_expiresAt_idx;not null" json:"expiresAt"`
	Project    *Project
}

func (IdempotencyKey) TableName() string {
	return "idempotency_keys"
}

func (m *IdempotencyKey) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = uuid.NewString()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type ThrottleClaim struct {
	ID            string    `gorm:"column:id;primaryKey;type:char(36)" json:"id"`
	ProjectID     string    `gorm:"column:projectId;uniqueIndex:throttle_claims_projectId_key_recipientKey_key;not null" json:"projectId"`
	Key           string    `gorm:"column:key;uniqueIndex:throttle_claims_projectId_key_recipientKey_key;not null" json:"key"`
	RecipientKey  string    `gorm:"column:recipientKey;uniqueIndex:throttle_claims_projectId_key_recipientKey_key;not null" json:"recipientKey"`
	ContactID     *string   `gorm:"column:contactId;index:throttle_claims_contactId_idx" json:"contactId"`
	EmailID       *string   `gorm:"column:emailId;index:throttle_claims_emailId_idx" json:"emailId"`
	WorkflowID    *string   `gorm:"column:workflowId;index:throttle_claims_workflowId_idx" json:"workflowId"`
	WindowSeconds *int      `gorm:"column:windowSeconds" json:"windowSeconds"`
	ExpiresAt     *DateTime `gorm:"column:expiresAt;index:throttle_claims_expiresAt_idx" json:"expiresAt"`
	CreatedAt     DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt     DateTime  `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Project       *Project
	Contact       *Contact
	Workflow      *Workflow
}

func (ThrottleClaim) TableName() string {
	return "throttle_claims"
}

func (m *ThrottleClaim) BeforeCreate(tx *gorm.DB) error {
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

func (*ThrottleClaim) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type APIRequest struct {
	ID           string   `gorm:"column:id;primaryKey" json:"id"`
	Method       string   `gorm:"column:method;not null" json:"method"`
	Path         string   `gorm:"column:path;index:api_requests_path_createdAt_idx;not null" json:"path"`
	StatusCode   int      `gorm:"column:statusCode;index:api_requests_statusCode_createdAt_idx;index:api_requests_projectId_statusCode_createdAt_idx;not null" json:"statusCode"`
	Duration     int      `gorm:"column:duration;not null" json:"duration"`
	ProjectID    *string  `gorm:"column:projectId;index:api_requests_projectId_createdAt_idx;index:api_requests_projectId_statusCode_createdAt_idx" json:"projectId"`
	UserID       *string  `gorm:"column:userId;index:api_requests_userId_createdAt_idx" json:"userId"`
	AuthType     *string  `gorm:"column:authType" json:"authType"`
	IP           *string  `gorm:"column:ip" json:"ip"`
	UserAgent    *string  `gorm:"column:userAgent" json:"userAgent"`
	ErrorCode    *string  `gorm:"column:errorCode" json:"errorCode"`
	ErrorMessage *string  `gorm:"column:errorMessage" json:"errorMessage"`
	RequestSize  *int     `gorm:"column:requestSize" json:"requestSize"`
	ResponseSize *int     `gorm:"column:responseSize" json:"responseSize"`
	CreatedAt    DateTime `gorm:"column:createdAt;index:api_requests_projectId_createdAt_idx;index:api_requests_userId_createdAt_idx;index:api_requests_statusCode_createdAt_idx;index:api_requests_path_createdAt_idx;index:api_requests_createdAt_idx;index:api_requests_projectId_statusCode_createdAt_idx;autoCreateTime:false;not null" json:"createdAt"`
	Project      *Project
}

func (APIRequest) TableName() string {
	return "api_requests"
}

func (m *APIRequest) BeforeCreate(tx *gorm.DB) error {
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
