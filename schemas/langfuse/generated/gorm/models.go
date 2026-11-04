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

type Account struct {
	ID                    string  `gorm:"column:id;primaryKey" json:"id"`
	UserID                string  `gorm:"column:user_id;index:Account_user_id_idx;not null" json:"user_id"`
	Type                  string  `gorm:"column:type;not null" json:"type"`
	Provider              string  `gorm:"column:provider;uniqueIndex:Account_provider_providerAccountId_key;not null" json:"provider"`
	ProviderAccountID     string  `gorm:"column:providerAccountId;uniqueIndex:Account_provider_providerAccountId_key;not null" json:"providerAccountId"`
	RefreshToken          *string `gorm:"column:refresh_token" json:"refresh_token"`
	AccessToken           *string `gorm:"column:access_token" json:"access_token"`
	ExpiresAt             *int    `gorm:"column:expires_at" json:"expires_at"`
	ExpiresIn             *int    `gorm:"column:expires_in" json:"expires_in"`
	ExtExpiresIn          *int    `gorm:"column:ext_expires_in" json:"ext_expires_in"`
	TokenType             *string `gorm:"column:token_type" json:"token_type"`
	Scope                 *string `gorm:"column:scope" json:"scope"`
	IDToken               *string `gorm:"column:id_token" json:"id_token"`
	SessionState          *string `gorm:"column:session_state" json:"session_state"`
	RefreshTokenExpiresIn *int    `gorm:"column:refresh_token_expires_in" json:"refresh_token_expires_in"`
	CreatedAt             *int    `gorm:"column:created_at;autoCreateTime:false" json:"created_at"`
	User                  *User
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
	SessionToken string   `gorm:"column:session_token;uniqueIndex;not null" json:"session_token"`
	UserID       string   `gorm:"column:user_id;not null" json:"user_id"`
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

type User struct {
	ID                        string                      `gorm:"column:id;primaryKey" json:"id"`
	Name                      *string                     `gorm:"column:name" json:"name"`
	Email                     *string                     `gorm:"column:email;uniqueIndex" json:"email"`
	EmailVerified             *DateTime                   `gorm:"column:email_verified" json:"email_verified"`
	Password                  *string                     `gorm:"column:password" json:"password"`
	SessionsExpiredAt         *DateTime                   `gorm:"column:sessions_expired_at" json:"sessions_expired_at"`
	Image                     *string                     `gorm:"column:image" json:"image"`
	Admin                     bool                        `gorm:"column:admin;default:false;not null" json:"admin"`
	V4BetaEnabled             bool                        `gorm:"column:v4_beta_enabled;default:false;not null" json:"v4_beta_enabled"`
	SupportAccess             datatypes.JSON              `gorm:"column:support_access" json:"support_access"`
	CreatedAt                 DateTime                    `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt                 DateTime                    `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	FeatureFlags              []string                    `gorm:"column:feature_flags;serializer:json;not null" json:"feature_flags"`
	Accounts                  []Account                   `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	Sessions                  []Session                   `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	OrganizationMemberships   []OrganizationMembership    `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	ProjectMemberships        []ProjectMembership         `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	Invitations               []MembershipInvitation      `gorm:"foreignKey:InvitedByUserID;constraint:OnDelete:SET NULL"`
	AnnotatedLockedItem       []AnnotationQueueItem       `gorm:"foreignKey:LockedByUserID;constraint:OnDelete:SET NULL"`
	AnnotatedCompletedItem    []AnnotationQueueItem       `gorm:"foreignKey:AnnotatorUserID;constraint:OnDelete:SET NULL"`
	DashboardWidgetsCreated   []DashboardWidget           `gorm:"foreignKey:CreatedBy;constraint:OnDelete:SET NULL"`
	DashboardWidgetsUpdated   []DashboardWidget           `gorm:"foreignKey:UpdatedBy;constraint:OnDelete:SET NULL"`
	DashboardCreated          []Dashboard                 `gorm:"foreignKey:CreatedBy;constraint:OnDelete:SET NULL"`
	DashboardUpdated          []Dashboard                 `gorm:"foreignKey:UpdatedBy;constraint:OnDelete:SET NULL"`
	TableViewPresetCreated    []TableViewPreset           `gorm:"foreignKey:CreatedBy;constraint:OnDelete:SET NULL"`
	TableViewPresetUpdated    []TableViewPreset           `gorm:"foreignKey:UpdatedBy;constraint:OnDelete:SET NULL"`
	MonitorsCreated           []Monitor                   `gorm:"foreignKey:CreatedBy;constraint:OnDelete:SET NULL"`
	MonitorsUpdated           []Monitor                   `gorm:"foreignKey:UpdatedBy;constraint:OnDelete:SET NULL"`
	TopicConfigsCreated       []TopicConfig               `gorm:"foreignKey:CreatedBy;constraint:OnDelete:SET NULL"`
	TopicConfigsUpdated       []TopicConfig               `gorm:"foreignKey:UpdatedBy;constraint:OnDelete:SET NULL"`
	TracingReadinessCreated   []TracingReadinessSettings  `gorm:"foreignKey:CreatedBy;constraint:OnDelete:SET NULL"`
	TracingReadinessUpdated   []TracingReadinessSettings  `gorm:"foreignKey:UpdatedBy;constraint:OnDelete:SET NULL"`
	AnnotationQueueAssignment []AnnotationQueueAssignment `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	Surveys                   []Survey                    `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	CommentReactions          []CommentReaction           `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	NotificationPreferences   []NotificationPreference    `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	DefaultViews              []DefaultView               `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	InAppAgentConversations   []InAppAgentConversation    `gorm:"foreignKey:CreatedByUserID;constraint:OnDelete:SET NULL"`
	InAppAgentRuns            []InAppAgentRun             `gorm:"foreignKey:TriggeredByUserID;constraint:OnDelete:SET NULL"`
	APIKeysCreated            []APIKey                    `gorm:"foreignKey:CreatedByUserID;constraint:OnDelete:SET NULL"`
	APIKeys                   []APIKey                    `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	RoleAssignments           []RoleAssignment            `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	EvaluatorsCreated         []Evaluator                 `gorm:"foreignKey:CreatedByUserID;constraint:OnDelete:SET NULL"`
	EvaluatorVersionsCreated  []EvaluatorVersion          `gorm:"foreignKey:CreatedByUserID;constraint:OnDelete:SET NULL"`
	EvaluationRulesCreated    []EvaluationRule            `gorm:"foreignKey:CreatedByUserID;constraint:OnDelete:SET NULL"`
}

func (User) TableName() string {
	return "users"
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

type VerificationToken struct {
	Identifier string   `gorm:"column:identifier;uniqueIndex:verification_tokens_identifier_token_key;not null" json:"identifier"`
	Token      string   `gorm:"column:token;primaryKey;uniqueIndex;uniqueIndex:verification_tokens_identifier_token_key" json:"token"`
	Expires    DateTime `gorm:"column:expires;not null" json:"expires"`
}

func (VerificationToken) TableName() string {
	return "verification_tokens"
}

type Organization struct {
	ID                               string                   `gorm:"column:id;primaryKey" json:"id"`
	Name                             string                   `gorm:"column:name;not null" json:"name"`
	CreatedAt                        DateTime                 `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt                        DateTime                 `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	CloudConfig                      datatypes.JSON           `gorm:"column:cloud_config" json:"cloud_config"`
	Metadata                         datatypes.JSON           `gorm:"column:metadata" json:"metadata"`
	CloudBillingCycleAnchor          *DateTime                `gorm:"column:cloud_billing_cycle_anchor" json:"cloud_billing_cycle_anchor"`
	CloudBillingCycleUpdatedAt       *DateTime                `gorm:"column:cloud_billing_cycle_updated_at" json:"cloud_billing_cycle_updated_at"`
	CloudCurrentCycleUsage           *int                     `gorm:"column:cloud_current_cycle_usage" json:"cloud_current_cycle_usage"`
	CloudFreeTierUsageThresholdState *string                  `gorm:"column:cloud_free_tier_usage_threshold_state" json:"cloud_free_tier_usage_threshold_state"`
	AiFeaturesEnabled                bool                     `gorm:"column:ai_features_enabled;default:false;not null" json:"ai_features_enabled"`
	AiTelemetryEnabled               *bool                    `gorm:"column:ai_telemetry_enabled;default:true;not null" json:"ai_telemetry_enabled"`
	AccessAuditLogEnabled            bool                     `gorm:"column:access_audit_log_enabled;default:false;not null" json:"access_audit_log_enabled"`
	FeatureFlagOrgDefaults           []string                 `gorm:"column:feature_flag_org_defaults;serializer:json;not null" json:"feature_flag_org_defaults"`
	SfdcOrgID                        *string                  `gorm:"column:sfdc_org_id" json:"sfdc_org_id"`
	OrganizationMemberships          []OrganizationMembership `gorm:"foreignKey:OrgID;constraint:OnDelete:CASCADE"`
	Projects                         []Project                `gorm:"foreignKey:OrgID;constraint:OnUpdate:CASCADE,OnDelete:CASCADE"`
	MembershipInvitation             []MembershipInvitation   `gorm:"foreignKey:OrgID;constraint:OnDelete:CASCADE"`
	APIKey                           []APIKey                 `gorm:"foreignKey:OrgID;constraint:OnDelete:CASCADE"`
	Surveys                          []Survey                 `gorm:"foreignKey:OrgID;constraint:OnDelete:CASCADE"`
	CloudSpendAlerts                 []CloudSpendAlert        `gorm:"foreignKey:OrgID;constraint:OnDelete:CASCADE"`
	VerifiedDomains                  []VerifiedDomain         `gorm:"foreignKey:OrganizationID;constraint:OnDelete:CASCADE"`
	GatewayAiConnections             []GatewayAiConnection    `gorm:"foreignKey:OrganizationID;constraint:OnDelete:CASCADE"`
	OrganizationLlmAPIKeys           []LlmAPIKeys             `gorm:"foreignKey:OrganizationID;constraint:OnDelete:CASCADE"`
	RoleAssignments                  []RoleAssignment         `gorm:"foreignKey:OrgID;constraint:OnDelete:CASCADE"`
	GatewayConfig                    *GatewayConfig           `gorm:"foreignKey:OrganizationID;constraint:OnDelete:CASCADE"`
}

func (Organization) TableName() string {
	return "organizations"
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
	if m.CloudBillingCycleAnchor == nil {
		m.CloudBillingCycleAnchor = &DateTime{Time: now}
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

type Project struct {
	ID                                string                              `gorm:"column:id;primaryKey" json:"id"`
	OrgID                             string                              `gorm:"column:org_id;index:projects_org_id_idx;not null" json:"org_id"`
	CreatedAt                         DateTime                            `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt                         DateTime                            `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	DeletedAt                         *DateTime                           `gorm:"column:deleted_at" json:"deleted_at"`
	Name                              string                              `gorm:"column:name;not null" json:"name"`
	RetentionDays                     *int                                `gorm:"column:retention_days" json:"retention_days"`
	HasTraces                         bool                                `gorm:"column:has_traces;default:false;not null" json:"has_traces"`
	Metadata                          datatypes.JSON                      `gorm:"column:metadata" json:"metadata"`
	HomeDashboardID                   *string                             `gorm:"column:home_dashboard_id" json:"home_dashboard_id"`
	HomeDashboard                     *Dashboard                          `gorm:"foreignKey:HomeDashboardID"`
	Organization                      *Organization                       `gorm:"foreignKey:OrgID"`
	ProjectMembers                    []ProjectMembership                 `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	APIKeys                           []APIKey                            `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	RoleAssignments                   []RoleAssignment                    `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	Dataset                           []Dataset                           `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	Invitations                       []MembershipInvitation              `gorm:"foreignKey:ProjectID;constraint:OnDelete:SET NULL"`
	Sessions                          []TraceSession                      `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	Prompt                            []Prompt                            `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	Skill                             []Skill                             `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	SkillFile                         []SkillFile                         `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	SkillBlob                         []SkillBlob                         `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	Model                             []Model                             `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	EvalTemplate                      []EvalTemplate                      `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	JobConfiguration                  []JobConfiguration                  `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	JobExecution                      []JobExecution                      `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	Evaluator                         []Evaluator                         `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	EvaluationRule                    []EvaluationRule                    `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	EvaluationRuleEvaluatorAssignment []EvaluationRuleEvaluatorAssignment `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	LlmAPIKeys                        []LlmAPIKeys                        `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	PosthogIntegration                []PosthogIntegration                `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	MixpanelIntegration               []MixpanelIntegration               `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	BlobStorageIntegration            []BlobStorageIntegration            `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	WebCalloutEndpoints               []WebCalloutEndpoint                `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	ScoreConfig                       []ScoreConfig                       `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	BatchExport                       []BatchExport                       `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	BatchAction                       []BatchAction                       `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	Comment                           []Comment                           `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	CommentReactions                  []CommentReaction                   `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	AnnotationQueue                   []AnnotationQueue                   `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	AnnotationQueueItem               []AnnotationQueueItem               `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	Media                             []Media                             `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	PromptDependency                  []PromptDependency                  `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	LlmSchema                         []LlmSchema                         `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	LlmTool                           []LlmTool                           `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	PromptProtectedLabels             []PromptProtectedLabels             `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	Dashboard                         []Dashboard                         `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	DashboardWidget                   []DashboardWidget                   `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	TableViewPreset                   []TableViewPreset                   `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	Actions                           []Action                            `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	Triggers                          []Trigger                           `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	AutomationExecutions              []AutomationExecution               `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	Automation                        []Automation                        `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	Monitors                          []Monitor                           `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	DefaultLlmModel                   []DefaultLlmModel                   `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	Price                             []Price                             `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	PendingDeletion                   []PendingDeletion                   `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	AnnotationQueueAssignment         []AnnotationQueueAssignment         `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	NotificationPreference            []NotificationPreference            `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	DefaultView                       []DefaultView                       `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	InAppAgentConversation            []InAppAgentConversation            `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	InAppAgentEvent                   []InAppAgentEvent                   `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	InAppAgentRun                     []InAppAgentRun                     `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	InAppAgentPendingToolApproval     []InAppAgentPendingToolApproval     `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	TopicClusteringRuns               []TopicClusteringRun                `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	GatewayConfigs                    []GatewayConfig                     `gorm:"foreignKey:DefaultIngestionProjectID;constraint:OnDelete:SET NULL"`
	TracingReadinessSettings          *TracingReadinessSettings           `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	ExternalMediaStorageIntegration   *ExternalMediaStorageIntegration    `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	TopicConfig                       *TopicConfig                        `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
	SlackIntegration                  *SlackIntegration                   `gorm:"foreignKey:ProjectID;constraint:OnDelete:CASCADE"`
}

func (Project) TableName() string {
	return "projects"
}

func (m *Project) BeforeCreate(tx *gorm.DB) error {
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

func (*Project) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type APIKey struct {
	ID                  string    `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt           DateTime  `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	Note                *string   `gorm:"column:note" json:"note"`
	PublicKey           string    `gorm:"column:public_key;uniqueIndex;index:api_keys_public_key_idx;not null" json:"public_key"`
	HashedSecretKey     string    `gorm:"column:hashed_secret_key;uniqueIndex;index:api_keys_hashed_secret_key_idx;not null" json:"hashed_secret_key"`
	FastHashedSecretKey *string   `gorm:"column:fast_hashed_secret_key;uniqueIndex;index:api_keys_fast_hashed_secret_key_idx" json:"fast_hashed_secret_key"`
	DisplaySecretKey    string    `gorm:"column:display_secret_key;not null" json:"display_secret_key"`
	LastUsedAt          *DateTime `gorm:"column:last_used_at" json:"last_used_at"`
	ExpiresAt           *DateTime `gorm:"column:expires_at" json:"expires_at"`
	IsInAppAgentKey     bool      `gorm:"column:is_in_app_agent_key;default:false;not null" json:"is_in_app_agent_key"`
	ProjectID           *string   `gorm:"column:project_id;index:api_keys_project_id_idx" json:"project_id"`
	OrgID               *string   `gorm:"column:organization_id;index:api_keys_organization_id_idx;index:api_keys_user_id_organization_id_idx" json:"organization_id"`
	Scope               string    `gorm:"column:scope;default:'PROJECT';not null" json:"scope"`
	CreatedByUserID     *string   `gorm:"column:created_by_user_id" json:"created_by_user_id"`
	UserID              *string   `gorm:"column:user_id;index:api_keys_user_id_organization_id_idx" json:"user_id"`
	CreatedByAPIKeyID   *string   `gorm:"column:created_by_api_key_id;index:api_keys_created_by_api_key_id_idx" json:"created_by_api_key_id"`
	Project             *Project
	Organization        *Organization             `gorm:"foreignKey:OrgID"`
	CreatedByUser       *User                     `gorm:"foreignKey:CreatedByUserID"`
	User                *User                     `gorm:"foreignKey:UserID"`
	CreatedByAPIKey     *APIKey                   `gorm:"foreignKey:CreatedByAPIKeyID"`
	APIKeysCreated      []APIKey                  `gorm:"foreignKey:CreatedByAPIKeyID;constraint:OnDelete:SET NULL"`
	RoleAssignments     []RoleAssignment          `gorm:"foreignKey:APIKeyID;constraint:OnDelete:CASCADE"`
	GatewayAssociation  *GatewayAPIKeyAssociation `gorm:"foreignKey:APIKeyID;constraint:OnDelete:CASCADE"`
}

func (APIKey) TableName() string {
	return "api_keys"
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

type RoleAssignment struct {
	ID           string   `gorm:"column:id;primaryKey" json:"id"`
	OwnerID      string   `gorm:"column:owner_id;uniqueIndex:role_assignments_owner_id_principal_id_role_id_key;not null" json:"owner_id"`
	ProjectID    *string  `gorm:"column:project_id;index:role_assignments_project_id_idx" json:"project_id"`
	OrgID        string   `gorm:"column:org_id;index:role_assignments_org_id_idx;not null" json:"org_id"`
	PrincipalID  string   `gorm:"column:principal_id;uniqueIndex:role_assignments_owner_id_principal_id_role_id_key;index:role_assignments_principal_id_idx;not null" json:"principal_id"`
	UserID       *string  `gorm:"column:user_id;index:role_assignments_user_id_idx" json:"user_id"`
	APIKeyID     *string  `gorm:"column:api_key_id;index:role_assignments_api_key_id_idx" json:"api_key_id"`
	RoleID       string   `gorm:"column:role_id;uniqueIndex:role_assignments_owner_id_principal_id_role_id_key;not null" json:"role_id"`
	SystemRole   string   `gorm:"column:system_role;uniqueIndex:role_assignments_deprecatedPrincipalUserId_deprecatedOwnerOrgId_system_role_key;uniqueIndex:role_assignments_deprecatedPrincipalUserId_deprecatedOwnerProjectId_system_role_key;uniqueIndex:role_assignments_deprecatedPrincipalApiKeyId_deprecatedOwnerOrgId_system_role_key;uniqueIndex:role_assignments_deprecatedPrincipalApiKeyId_deprecatedOwnerProjectId_system_role_key;not null" json:"system_role"`
	CreatedAt    DateTime `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt    DateTime `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	Project      *Project
	Organization *Organization `gorm:"foreignKey:OrgID"`
	User         *User
	APIKey       *APIKey
}

func (RoleAssignment) TableName() string {
	return "role_assignments"
}

func (m *RoleAssignment) BeforeCreate(tx *gorm.DB) error {
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

func (*RoleAssignment) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type GatewayConfig struct {
	OrganizationID            string   `gorm:"column:organization_id;primaryKey" json:"organization_id"`
	DefaultIngestionProjectID *string  `gorm:"column:default_ingestion_project_id" json:"default_ingestion_project_id"`
	IngestionMode             string   `gorm:"column:ingestion_mode;default:'usage';not null" json:"ingestion_mode"`
	CreatedAt                 DateTime `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt                 DateTime `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	Organization              *Organization
	DefaultIngestionProject   *Project `gorm:"foreignKey:DefaultIngestionProjectID"`
}

func (GatewayConfig) TableName() string {
	return "gateway_configs"
}

func (m *GatewayConfig) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*GatewayConfig) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type GatewayAiConnection struct {
	ID                  string         `gorm:"column:id;primaryKey;index:gateway_ai_conn_org_created_at_id_idx;index:gateway_ai_conn_org_status_created_at_id_idx" json:"id"`
	OrganizationID      string         `gorm:"column:organization_id;uniqueIndex:gateway_ai_connections_organization_id_name_key;index:gateway_ai_conn_org_created_at_id_idx;index:gateway_ai_conn_org_status_created_at_id_idx;not null" json:"organization_id"`
	Name                string         `gorm:"column:name;uniqueIndex:gateway_ai_connections_organization_id_name_key;not null" json:"name"`
	Provider            string         `gorm:"column:provider;not null" json:"provider"`
	EncryptedCredential string         `gorm:"column:encrypted_credentials;not null" json:"encrypted_credentials"`
	DisplaySecret       string         `gorm:"column:display_secret_key;not null" json:"display_secret_key"`
	CreatedByID         *string        `gorm:"column:created_by_id" json:"created_by_id"`
	Status              string         `gorm:"column:status;index:gateway_ai_conn_org_status_created_at_id_idx;default:'enabled';not null" json:"status"`
	AllowedModels       datatypes.JSON `gorm:"column:allowed_models;type:jsonb" json:"allowed_models"`
	CreatedAt           DateTime       `gorm:"column:created_at;index:gateway_ai_conn_org_created_at_id_idx;index:gateway_ai_conn_org_status_created_at_id_idx;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt           DateTime       `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	Organization        *Organization
}

func (GatewayAiConnection) TableName() string {
	return "gateway_ai_connections"
}

func (m *GatewayAiConnection) BeforeCreate(tx *gorm.DB) error {
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

func (*GatewayAiConnection) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type GatewayAPIKeyAssociation struct {
	APIKeyID string         `gorm:"column:api_key_id;primaryKey" json:"api_key_id"`
	Metadata datatypes.JSON `gorm:"column:metadata;type:jsonb;default:'{}';not null" json:"metadata"`
	APIKey   *APIKey
}

func (GatewayAPIKeyAssociation) TableName() string {
	return "gateway_api_key_associations"
}

type InAppAgentConversation struct {
	ID                   string    `gorm:"column:id;primaryKey;index:in_app_agent_conversations_project_user_list_idx" json:"id"`
	ProjectID            string    `gorm:"column:project_id;primaryKey;index:in_app_agent_conversations_project_user_list_idx;index:in_app_agent_conversations_project_updated_at_idx" json:"project_id"`
	CreatedByUserID      *string   `gorm:"column:created_by_user_id;index:in_app_agent_conversations_project_user_list_idx" json:"created_by_user_id"`
	Title                *string   `gorm:"column:title" json:"title"`
	RenamedByUserAt      *DateTime `gorm:"column:renamed_by_user_at" json:"renamed_by_user_at"`
	VisibilityScope      string    `gorm:"column:visibility_scope;default:'PERSONAL';not null" json:"visibility_scope"`
	ProviderSessionID    *string   `gorm:"column:provider_session_id" json:"provider_session_id"`
	AlwaysAllowedTools   []string  `gorm:"column:always_allowed_tools;serializer:json;not null" json:"always_allowed_tools"`
	DeletedAt            *DateTime `gorm:"column:deleted_at;index:in_app_agent_conversations_project_user_list_idx" json:"deleted_at"`
	CreatedAt            DateTime  `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt            DateTime  `gorm:"column:updated_at;index:in_app_agent_conversations_project_user_list_idx;index:in_app_agent_conversations_project_updated_at_idx;autoUpdateTime:false;not null" json:"updated_at"`
	Project              *Project
	CreatedByUser        *User                           `gorm:"foreignKey:CreatedByUserID"`
	Events               []InAppAgentEvent               `gorm:"foreignKey:ConversationID,ProjectID;references:ID,ProjectID;constraint:OnDelete:CASCADE"`
	Runs                 []InAppAgentRun                 `gorm:"foreignKey:ConversationID,ProjectID;references:ID,ProjectID;constraint:OnDelete:CASCADE"`
	PendingToolApprovals []InAppAgentPendingToolApproval `gorm:"foreignKey:ConversationID,ProjectID;references:ID,ProjectID;constraint:OnDelete:CASCADE"`
}

func (InAppAgentConversation) TableName() string {
	return "in_app_agent_conversations"
}

func (m *InAppAgentConversation) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*InAppAgentConversation) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type InAppAgentEvent struct {
	ProjectID      string         `gorm:"column:project_id;primaryKey;index:in_app_agent_events_project_run_idx" json:"project_id"`
	ConversationID string         `gorm:"column:conversation_id;primaryKey" json:"conversation_id"`
	RunID          string         `gorm:"column:run_id;index:in_app_agent_events_project_run_idx;not null" json:"run_id"`
	SequenceNumber int            `gorm:"column:sequence_number;primaryKey" json:"sequence_number"`
	Type           string         `gorm:"column:type;not null" json:"type"`
	Event          datatypes.JSON `gorm:"column:event;type:jsonb;not null" json:"event"`
	CreatedAt      DateTime       `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	Project        *Project
	Conversation   *InAppAgentConversation `gorm:"foreignKey:ConversationID,ProjectID;references:ID,ProjectID"`
	Run            *InAppAgentRun          `gorm:"foreignKey:RunID,ProjectID;references:ID,ProjectID"`
}

func (InAppAgentEvent) TableName() string {
	return "in_app_agent_events"
}

func (m *InAppAgentEvent) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type InAppAgentRun struct {
	ID                string         `gorm:"column:id;primaryKey" json:"id"`
	ProjectID         string         `gorm:"column:project_id;primaryKey;index:in_app_agent_runs_project_conversation_created_idx" json:"project_id"`
	ConversationID    string         `gorm:"column:conversation_id;index:in_app_agent_runs_project_conversation_created_idx;not null" json:"conversation_id"`
	TriggeredByUserID *string        `gorm:"column:triggered_by_user_id" json:"triggered_by_user_id"`
	Model             *string        `gorm:"column:model" json:"model"`
	McpAPIKeyID       *string        `gorm:"column:mcp_api_key_id" json:"mcp_api_key_id"`
	ErrorCode         *string        `gorm:"column:error_code" json:"error_code"`
	ErrorMessage      *string        `gorm:"column:error_message" json:"error_message"`
	FinishedAt        *DateTime      `gorm:"column:finished_at" json:"finished_at"`
	Status            *string        `gorm:"column:status" json:"status"`
	Request           datatypes.JSON `gorm:"column:request" json:"request"`
	ClaimedAt         *DateTime      `gorm:"column:claimed_at" json:"claimed_at"`
	HeartbeatAt       *DateTime      `gorm:"column:heartbeat_at" json:"heartbeat_at"`
	CancelRequestedAt *DateTime      `gorm:"column:cancel_requested_at" json:"cancel_requested_at"`
	CreatedAt         DateTime       `gorm:"column:created_at;index:in_app_agent_runs_project_conversation_created_idx;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt         DateTime       `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	Project           *Project
	Conversation      *InAppAgentConversation `gorm:"foreignKey:ConversationID,ProjectID;references:ID,ProjectID"`
	TriggeredByUser   *User                   `gorm:"foreignKey:TriggeredByUserID"`
	Events            []InAppAgentEvent       `gorm:"foreignKey:RunID,ProjectID;references:ID,ProjectID;constraint:OnDelete:CASCADE"`
}

func (InAppAgentRun) TableName() string {
	return "in_app_agent_runs"
}

func (m *InAppAgentRun) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*InAppAgentRun) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type InAppAgentPendingToolApproval struct {
	ProjectID           string   `gorm:"column:project_id;primaryKey" json:"project_id"`
	ConversationID      string   `gorm:"column:conversation_id;primaryKey" json:"conversation_id"`
	ToolCallID          string   `gorm:"column:tool_call_id;primaryKey" json:"tool_call_id"`
	ApprovalFingerprint string   `gorm:"column:approval_fingerprint;not null" json:"approval_fingerprint"`
	ExpiresAt           DateTime `gorm:"column:expires_at;index:in_app_agent_pending_tool_approvals_expires_at_idx;not null" json:"expires_at"`
	CreatedAt           DateTime `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt           DateTime `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	Project             *Project
	Conversation        *InAppAgentConversation `gorm:"foreignKey:ConversationID,ProjectID;references:ID,ProjectID"`
}

func (InAppAgentPendingToolApproval) TableName() string {
	return "in_app_agent_pending_tool_approvals"
}

func (m *InAppAgentPendingToolApproval) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*InAppAgentPendingToolApproval) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type BackgroundMigration struct {
	ID           string         `gorm:"column:id;primaryKey" json:"id"`
	Name         string         `gorm:"column:name;uniqueIndex;not null" json:"name"`
	Script       string         `gorm:"column:script;not null" json:"script"`
	Args         datatypes.JSON `gorm:"column:args;not null" json:"args"`
	State        datatypes.JSON `gorm:"column:state;default:'{}';not null" json:"state"`
	FinishedAt   *DateTime      `gorm:"column:finished_at" json:"finished_at"`
	FailedAt     *DateTime      `gorm:"column:failed_at" json:"failed_at"`
	FailedReason *string        `gorm:"column:failed_reason" json:"failed_reason"`
	WorkerID     *string        `gorm:"column:worker_id" json:"worker_id"`
	LockedAt     *DateTime      `gorm:"column:locked_at" json:"locked_at"`
}

func (BackgroundMigration) TableName() string {
	return "background_migrations"
}

func (m *BackgroundMigration) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = cuid.New()
	}
	return nil
}

type LlmAPIKeys struct {
	ID                     string         `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt              DateTime       `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt              DateTime       `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	Provider               string         `gorm:"column:provider;uniqueIndex:llm_api_keys_project_id_provider_key;uniqueIndex:llm_api_keys_organization_id_provider_key;not null" json:"provider"`
	Adapter                string         `gorm:"column:adapter;not null" json:"adapter"`
	DisplaySecretKey       string         `gorm:"column:display_secret_key;not null" json:"display_secret_key"`
	SecretKey              string         `gorm:"column:secret_key;not null" json:"secret_key"`
	BaseURL                *string        `gorm:"column:base_url" json:"base_url"`
	CustomModels           []string       `gorm:"column:custom_models;serializer:json;not null" json:"custom_models"`
	WithDefaultModels      *bool          `gorm:"column:with_default_models;default:true;not null" json:"with_default_models"`
	ExtraHeaders           *string        `gorm:"column:extra_headers" json:"extra_headers"`
	ExtraHeaderKeys        []string       `gorm:"column:extra_header_keys;serializer:json;not null" json:"extra_header_keys"`
	Config                 datatypes.JSON `gorm:"column:config" json:"config"`
	ProjectID              *string        `gorm:"column:project_id;uniqueIndex:llm_api_keys_project_id_provider_key" json:"project_id"`
	OrganizationID         *string        `gorm:"column:organization_id;uniqueIndex:llm_api_keys_organization_id_provider_key" json:"organization_id"`
	Project                *Project
	Organization           *Organization
	DefaultLlmModel        []DefaultLlmModel `gorm:"foreignKey:LlmAPIKeyID;constraint:OnDelete:CASCADE"`
	TopicSummaryConfigs    []TopicConfig     `gorm:"foreignKey:SummaryLlmAPIKeyID;constraint:OnDelete:SET NULL"`
	TopicEmbeddingConfigs  []TopicConfig     `gorm:"foreignKey:EmbeddingLlmAPIKeyID;constraint:OnDelete:SET NULL"`
	TopicClusteringConfigs []TopicConfig     `gorm:"foreignKey:ClusteringLlmAPIKeyID;constraint:OnDelete:SET NULL"`
}

func (LlmAPIKeys) TableName() string {
	return "llm_api_keys"
}

func (m *LlmAPIKeys) BeforeCreate(tx *gorm.DB) error {
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

func (*LlmAPIKeys) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type OrganizationMembership struct {
	ID                 string        `gorm:"column:id;primaryKey" json:"id"`
	OrgID              string        `gorm:"column:org_id;uniqueIndex:organization_memberships_org_id_user_id_key;not null" json:"org_id"`
	UserID             string        `gorm:"column:user_id;uniqueIndex:organization_memberships_org_id_user_id_key;index:organization_memberships_user_id_idx;not null" json:"user_id"`
	Role               string        `gorm:"column:role;not null" json:"role"`
	CreatedAt          DateTime      `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt          DateTime      `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	Organization       *Organization `gorm:"foreignKey:OrgID"`
	User               *User
	ProjectMemberships []ProjectMembership `gorm:"foreignKey:OrgMembershipID;constraint:OnDelete:CASCADE"`
}

func (OrganizationMembership) TableName() string {
	return "organization_memberships"
}

func (m *OrganizationMembership) BeforeCreate(tx *gorm.DB) error {
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

func (*OrganizationMembership) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type ProjectMembership struct {
	OrgMembershipID        string                  `gorm:"column:org_membership_id;index:project_memberships_org_membership_id_idx;not null" json:"org_membership_id"`
	ProjectID              string                  `gorm:"column:project_id;primaryKey;index:project_memberships_project_id_idx" json:"project_id"`
	UserID                 string                  `gorm:"column:user_id;primaryKey;index:project_memberships_user_id_idx" json:"user_id"`
	Role                   string                  `gorm:"column:role;not null" json:"role"`
	CreatedAt              DateTime                `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt              DateTime                `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	OrganizationMembership *OrganizationMembership `gorm:"foreignKey:OrgMembershipID"`
	Project                *Project
	User                   *User
}

func (ProjectMembership) TableName() string {
	return "project_memberships"
}

func (m *ProjectMembership) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*ProjectMembership) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type MembershipInvitation struct {
	ID              string        `gorm:"column:id;primaryKey" json:"id"`
	Email           string        `gorm:"column:email;uniqueIndex:membership_invitations_email_org_id_key;index:membership_invitations_email_idx;not null" json:"email"`
	OrgID           string        `gorm:"column:org_id;uniqueIndex:membership_invitations_email_org_id_key;index:membership_invitations_org_id_idx;not null" json:"org_id"`
	OrgRole         string        `gorm:"column:org_role;not null" json:"org_role"`
	ProjectID       *string       `gorm:"column:project_id;index:membership_invitations_project_id_idx" json:"project_id"`
	ProjectRole     *string       `gorm:"column:project_role" json:"project_role"`
	InvitedByUserID *string       `gorm:"column:invited_by_user_id" json:"invited_by_user_id"`
	CreatedAt       DateTime      `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt       DateTime      `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	Organization    *Organization `gorm:"foreignKey:OrgID"`
	Project         *Project
	InvitedByUser   *User `gorm:"foreignKey:InvitedByUserID"`
}

func (MembershipInvitation) TableName() string {
	return "membership_invitations"
}

func (m *MembershipInvitation) BeforeCreate(tx *gorm.DB) error {
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

func (*MembershipInvitation) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type TraceSession struct {
	ID          string   `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt   DateTime `gorm:"column:created_at;index:trace_sessions_project_id_created_at_idx;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt   DateTime `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	ProjectID   string   `gorm:"column:project_id;primaryKey;index:trace_sessions_project_id_created_at_idx" json:"project_id"`
	Bookmarked  bool     `gorm:"column:bookmarked;default:false;not null" json:"bookmarked"`
	Public      bool     `gorm:"column:public;default:false;not null" json:"public"`
	Environment *string  `gorm:"column:environment;default:'default';not null" json:"environment"`
	Project     *Project
}

func (TraceSession) TableName() string {
	return "trace_sessions"
}

func (m *TraceSession) BeforeCreate(tx *gorm.DB) error {
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

func (*TraceSession) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type ScoreConfig struct {
	ID          string         `gorm:"column:id;primaryKey;uniqueIndex:score_configs_id_project_id_key" json:"id"`
	CreatedAt   DateTime       `gorm:"column:created_at;index:score_configs_created_at_idx;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt   DateTime       `gorm:"column:updated_at;index:score_configs_updated_at_idx;autoUpdateTime:false;not null" json:"updated_at"`
	ProjectID   string         `gorm:"column:project_id;uniqueIndex:score_configs_id_project_id_key;index:score_configs_project_id_idx;not null" json:"project_id"`
	Name        string         `gorm:"column:name;not null" json:"name"`
	DataType    string         `gorm:"column:data_type;index:score_configs_data_type_idx;not null" json:"data_type"`
	IsArchived  bool           `gorm:"column:is_archived;index:score_configs_is_archived_idx;default:false;not null" json:"is_archived"`
	MinValue    *float64       `gorm:"column:min_value" json:"min_value"`
	MaxValue    *float64       `gorm:"column:max_value" json:"max_value"`
	Categories  datatypes.JSON `gorm:"column:categories;index:score_configs_categories_idx" json:"categories"`
	Description *string        `gorm:"column:description" json:"description"`
	Project     *Project
}

func (ScoreConfig) TableName() string {
	return "score_configs"
}

func (m *ScoreConfig) BeforeCreate(tx *gorm.DB) error {
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

func (*ScoreConfig) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type AnnotationQueue struct {
	ID                        string   `gorm:"column:id;primaryKey;index:annotation_queues_id_project_id_idx" json:"id"`
	Name                      string   `gorm:"column:name;uniqueIndex:annotation_queues_project_id_name_key;not null" json:"name"`
	Description               *string  `gorm:"column:description" json:"description"`
	ScoreConfigIds            []string `gorm:"column:score_config_ids;serializer:json;not null" json:"score_config_ids"`
	ProjectID                 string   `gorm:"column:project_id;uniqueIndex:annotation_queues_project_id_name_key;index:annotation_queues_id_project_id_idx;index:annotation_queues_project_id_created_at_idx;not null" json:"project_id"`
	CreatedAt                 DateTime `gorm:"column:created_at;index:annotation_queues_project_id_created_at_idx;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt                 DateTime `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	Project                   *Project
	AnnotationQueueItem       []AnnotationQueueItem       `gorm:"foreignKey:QueueID;constraint:OnDelete:CASCADE"`
	AnnotationQueueAssignment []AnnotationQueueAssignment `gorm:"foreignKey:QueueID;constraint:OnDelete:CASCADE"`
}

func (AnnotationQueue) TableName() string {
	return "annotation_queues"
}

func (m *AnnotationQueue) BeforeCreate(tx *gorm.DB) error {
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

func (*AnnotationQueue) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type AnnotationQueueItem struct {
	ID              string           `gorm:"column:id;primaryKey;index:annotation_queue_items_id_project_id_idx" json:"id"`
	QueueID         string           `gorm:"column:queue_id;index:annotation_queue_items_project_id_queue_id_status_idx;index:annotation_queue_items_object_id_object_type_project_id_queue_id_idx;not null" json:"queue_id"`
	ObjectID        string           `gorm:"column:object_id;index:annotation_queue_items_object_id_object_type_project_id_queue_id_idx;not null" json:"object_id"`
	ObjectType      string           `gorm:"column:object_type;index:annotation_queue_items_object_id_object_type_project_id_queue_id_idx;not null" json:"object_type"`
	Status          string           `gorm:"column:status;index:annotation_queue_items_project_id_queue_id_status_idx;default:'PENDING';not null" json:"status"`
	LockedAt        *DateTime        `gorm:"column:locked_at" json:"locked_at"`
	LockedByUserID  *string          `gorm:"column:locked_by_user_id" json:"locked_by_user_id"`
	AnnotatorUserID *string          `gorm:"column:annotator_user_id;index:annotation_queue_items_annotator_user_id_idx" json:"annotator_user_id"`
	CompletedAt     *DateTime        `gorm:"column:completed_at" json:"completed_at"`
	ProjectID       string           `gorm:"column:project_id;index:annotation_queue_items_id_project_id_idx;index:annotation_queue_items_project_id_queue_id_status_idx;index:annotation_queue_items_object_id_object_type_project_id_queue_id_idx;not null" json:"project_id"`
	CreatedAt       DateTime         `gorm:"column:created_at;index:annotation_queue_items_created_at_idx;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt       DateTime         `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	Queue           *AnnotationQueue `gorm:"foreignKey:QueueID"`
	LockedByUser    *User            `gorm:"foreignKey:LockedByUserID"`
	AnnotatorUser   *User            `gorm:"foreignKey:AnnotatorUserID"`
	Project         *Project
}

func (AnnotationQueueItem) TableName() string {
	return "annotation_queue_items"
}

func (m *AnnotationQueueItem) BeforeCreate(tx *gorm.DB) error {
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

func (*AnnotationQueueItem) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type AnnotationQueueAssignment struct {
	ID        string   `gorm:"column:id;primaryKey" json:"id"`
	ProjectID string   `gorm:"column:project_id;uniqueIndex:annotation_queue_assignments_project_id_queue_id_user_id_key;not null" json:"project_id"`
	UserID    string   `gorm:"column:user_id;uniqueIndex:annotation_queue_assignments_project_id_queue_id_user_id_key;not null" json:"user_id"`
	QueueID   string   `gorm:"column:queue_id;uniqueIndex:annotation_queue_assignments_project_id_queue_id_user_id_key;not null" json:"queue_id"`
	CreatedAt DateTime `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt DateTime `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	Project   *Project
	User      *User
	Queue     *AnnotationQueue `gorm:"foreignKey:QueueID"`
}

func (AnnotationQueueAssignment) TableName() string {
	return "annotation_queue_assignments"
}

func (m *AnnotationQueueAssignment) BeforeCreate(tx *gorm.DB) error {
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

func (*AnnotationQueueAssignment) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type CronJobs struct {
	Name         string    `gorm:"column:name;primaryKey" json:"name"`
	LastRun      *DateTime `gorm:"column:last_run" json:"last_run"`
	JobStartedAt *DateTime `gorm:"column:job_started_at" json:"job_started_at"`
	State        *string   `gorm:"column:state" json:"state"`
}

func (CronJobs) TableName() string {
	return "cron_jobs"
}

type Dataset struct {
	ID                               string         `gorm:"column:id;primaryKey" json:"id"`
	ProjectID                        string         `gorm:"column:project_id;primaryKey;uniqueIndex:datasets_project_id_name_key" json:"project_id"`
	Name                             string         `gorm:"column:name;uniqueIndex:datasets_project_id_name_key;not null" json:"name"`
	Description                      *string        `gorm:"column:description" json:"description"`
	Metadata                         datatypes.JSON `gorm:"column:metadata" json:"metadata"`
	RemoteExperimentURL              *string        `gorm:"column:remote_experiment_url" json:"remote_experiment_url"`
	RemoteExperimentPayload          datatypes.JSON `gorm:"column:remote_experiment_payload" json:"remote_experiment_payload"`
	RemoteExperimentEnabled          *bool          `gorm:"column:remote_experiment_enabled;default:true;not null" json:"remote_experiment_enabled"`
	RemoteExperimentSecretKey        *string        `gorm:"column:remote_experiment_secret_key" json:"remote_experiment_secret_key"`
	RemoteExperimentDisplaySecretKey *string        `gorm:"column:remote_experiment_display_secret_key" json:"remote_experiment_display_secret_key"`
	RemoteExperimentRequestHeaders   datatypes.JSON `gorm:"column:remote_experiment_request_headers" json:"remote_experiment_request_headers"`
	RemoteExperimentDisplayHeaders   datatypes.JSON `gorm:"column:remote_experiment_display_headers" json:"remote_experiment_display_headers"`
	InputSchema                      datatypes.JSON `gorm:"column:input_schema" json:"input_schema"`
	ExpectedOutputSchema             datatypes.JSON `gorm:"column:expected_output_schema" json:"expected_output_schema"`
	CreatedAt                        DateTime       `gorm:"column:created_at;index:datasets_created_at_idx;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt                        DateTime       `gorm:"column:updated_at;index:datasets_updated_at_idx;autoUpdateTime:false;not null" json:"updated_at"`
	Project                          *Project
	DatasetItems                     []DatasetItem `gorm:"foreignKey:DatasetID,ProjectID;references:ID,ProjectID;constraint:OnDelete:CASCADE"`
	DatasetRuns                      []DatasetRuns `gorm:"foreignKey:DatasetID,ProjectID;references:ID,ProjectID;constraint:OnDelete:CASCADE"`
}

func (Dataset) TableName() string {
	return "datasets"
}

func (m *Dataset) BeforeCreate(tx *gorm.DB) error {
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

func (*Dataset) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type DatasetItem struct {
	ID                  string         `gorm:"column:id;primaryKey;index:dataset_items_project_id_id_valid_from_idx" json:"id"`
	ProjectID           string         `gorm:"column:project_id;primaryKey;index:dataset_items_project_id_valid_to_idx;index:dataset_items_project_id_id_valid_from_idx" json:"project_id"`
	Status              *string        `gorm:"column:status;default:'ACTIVE'" json:"status"`
	Input               datatypes.JSON `gorm:"column:input" json:"input"`
	ExpectedOutput      datatypes.JSON `gorm:"column:expected_output" json:"expected_output"`
	Metadata            datatypes.JSON `gorm:"column:metadata" json:"metadata"`
	SourceTraceID       *string        `gorm:"column:source_trace_id;index:dataset_items_source_trace_id_idx" json:"source_trace_id"`
	SourceObservationID *string        `gorm:"column:source_observation_id;index:dataset_items_source_observation_id_idx" json:"source_observation_id"`
	DatasetID           string         `gorm:"column:dataset_id;index:dataset_items_dataset_id_idx;not null" json:"dataset_id"`
	CreatedAt           DateTime       `gorm:"column:created_at;index:dataset_items_created_at_idx;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt           DateTime       `gorm:"column:updated_at;index:dataset_items_updated_at_idx;autoUpdateTime:false;not null" json:"updated_at"`
	ValidFrom           DateTime       `gorm:"column:valid_from;primaryKey;index:dataset_items_project_id_id_valid_from_idx" json:"valid_from"`
	ValidTo             *DateTime      `gorm:"column:valid_to;index:dataset_items_project_id_valid_to_idx" json:"valid_to"`
	IsDeleted           bool           `gorm:"column:is_deleted;default:false;not null" json:"is_deleted"`
	Dataset             *Dataset       `gorm:"foreignKey:DatasetID,ProjectID;references:ID,ProjectID"`
}

func (DatasetItem) TableName() string {
	return "dataset_items"
}

func (m *DatasetItem) BeforeCreate(tx *gorm.DB) error {
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
	if m.ValidFrom.IsZero() {
		m.ValidFrom = DateTime{Time: now}
	}
	return nil
}

func (*DatasetItem) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type DatasetRuns struct {
	ID          string         `gorm:"column:id;primaryKey" json:"id"`
	ProjectID   string         `gorm:"column:project_id;primaryKey;uniqueIndex:dataset_runs_dataset_id_project_id_name_key" json:"project_id"`
	Name        string         `gorm:"column:name;uniqueIndex:dataset_runs_dataset_id_project_id_name_key;not null" json:"name"`
	Description *string        `gorm:"column:description" json:"description"`
	Metadata    datatypes.JSON `gorm:"column:metadata" json:"metadata"`
	DatasetID   string         `gorm:"column:dataset_id;uniqueIndex:dataset_runs_dataset_id_project_id_name_key;index:dataset_runs_dataset_id_idx;not null" json:"dataset_id"`
	CreatedAt   DateTime       `gorm:"column:created_at;index:dataset_runs_created_at_idx;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt   DateTime       `gorm:"column:updated_at;index:dataset_runs_updated_at_idx;autoUpdateTime:false;not null" json:"updated_at"`
	Dataset     *Dataset       `gorm:"foreignKey:DatasetID,ProjectID;references:ID,ProjectID"`
}

func (DatasetRuns) TableName() string {
	return "dataset_runs"
}

func (m *DatasetRuns) BeforeCreate(tx *gorm.DB) error {
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

func (*DatasetRuns) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Comment struct {
	ID           string   `gorm:"column:id;primaryKey" json:"id"`
	ProjectID    string   `gorm:"column:project_id;index:comments_project_id_object_type_object_id_idx;not null" json:"project_id"`
	ObjectType   string   `gorm:"column:object_type;index:comments_project_id_object_type_object_id_idx;not null" json:"object_type"`
	ObjectID     string   `gorm:"column:object_id;index:comments_project_id_object_type_object_id_idx;not null" json:"object_id"`
	CreatedAt    DateTime `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt    DateTime `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	Content      string   `gorm:"column:content;not null" json:"content"`
	AuthorUserID *string  `gorm:"column:author_user_id" json:"author_user_id"`
	DataField    *string  `gorm:"column:data_field" json:"data_field"`
	Path         []string `gorm:"column:path;serializer:json;not null" json:"path"`
	RangeStart   []int    `gorm:"column:range_start;serializer:json;not null" json:"range_start"`
	RangeEnd     []int    `gorm:"column:range_end;serializer:json;not null" json:"range_end"`
	Project      *Project
	Reactions    []CommentReaction `gorm:"foreignKey:CommentID;constraint:OnDelete:CASCADE"`
}

func (Comment) TableName() string {
	return "comments"
}

func (m *Comment) BeforeCreate(tx *gorm.DB) error {
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

func (*Comment) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type CommentReaction struct {
	ID        string   `gorm:"column:id;primaryKey" json:"id"`
	ProjectID string   `gorm:"column:project_id;not null" json:"project_id"`
	CommentID string   `gorm:"column:comment_id;uniqueIndex:comment_reactions_comment_id_user_id_emoji_key;not null" json:"comment_id"`
	UserID    string   `gorm:"column:user_id;uniqueIndex:comment_reactions_comment_id_user_id_emoji_key;not null" json:"user_id"`
	Emoji     string   `gorm:"column:emoji;uniqueIndex:comment_reactions_comment_id_user_id_emoji_key;not null" json:"emoji"`
	CreatedAt DateTime `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	Project   *Project
	Comment   *Comment
	User      *User
}

func (CommentReaction) TableName() string {
	return "comment_reactions"
}

func (m *CommentReaction) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type NotificationPreference struct {
	ID        string   `gorm:"column:id;primaryKey" json:"id"`
	UserID    string   `gorm:"column:user_id;uniqueIndex:notification_preferences_user_id_project_id_channel_type_key;not null" json:"user_id"`
	ProjectID string   `gorm:"column:project_id;uniqueIndex:notification_preferences_user_id_project_id_channel_type_key;not null" json:"project_id"`
	Channel   string   `gorm:"column:channel;uniqueIndex:notification_preferences_user_id_project_id_channel_type_key;not null" json:"channel"`
	Type      string   `gorm:"column:type;uniqueIndex:notification_preferences_user_id_project_id_channel_type_key;not null" json:"type"`
	Enabled   *bool    `gorm:"column:enabled;default:true;not null" json:"enabled"`
	CreatedAt DateTime `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt DateTime `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	User      *User
	Project   *Project
}

func (NotificationPreference) TableName() string {
	return "notification_preferences"
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

type Prompt struct {
	ID               string         `gorm:"column:id;primaryKey;index:prompts_project_id_id_idx" json:"id"`
	CreatedAt        DateTime       `gorm:"column:created_at;index:prompts_created_at_idx;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt        DateTime       `gorm:"column:updated_at;index:prompts_updated_at_idx;autoUpdateTime:false;not null" json:"updated_at"`
	ProjectID        string         `gorm:"column:project_id;uniqueIndex:prompts_project_id_name_version_key;index:prompts_project_id_id_idx;not null" json:"project_id"`
	CreatedBy        string         `gorm:"column:created_by;not null" json:"created_by"`
	Prompt           datatypes.JSON `gorm:"column:prompt;not null" json:"prompt"`
	Name             string         `gorm:"column:name;uniqueIndex:prompts_project_id_name_version_key;not null" json:"name"`
	Version          int            `gorm:"column:version;uniqueIndex:prompts_project_id_name_version_key;not null" json:"version"`
	Type             *string        `gorm:"column:type;default:'text';not null" json:"type"`
	IsActive         *bool          `gorm:"column:is_active" json:"is_active"`
	Config           datatypes.JSON `gorm:"column:config;default:'{}';not null" json:"config"`
	Tags             []string       `gorm:"column:tags;index:prompts_tags_idx;serializer:json;not null" json:"tags"`
	Labels           []string       `gorm:"column:labels;serializer:json;not null" json:"labels"`
	CommitMessage    *string        `gorm:"column:commit_message" json:"commit_message"`
	Project          *Project
	PromptDependency []PromptDependency `gorm:"foreignKey:ParentID;constraint:OnDelete:CASCADE"`
}

func (Prompt) TableName() string {
	return "prompts"
}

func (m *Prompt) BeforeCreate(tx *gorm.DB) error {
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

func (*Prompt) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type PromptDependency struct {
	ID           string   `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt    DateTime `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt    DateTime `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	ProjectID    string   `gorm:"column:project_id;index:prompt_dependencies_project_id_parent_id;index:prompt_dependencies_project_id_child_name;not null" json:"project_id"`
	ParentID     string   `gorm:"column:parent_id;index:prompt_dependencies_project_id_parent_id;not null" json:"parent_id"`
	ChildName    string   `gorm:"column:child_name;index:prompt_dependencies_project_id_child_name;not null" json:"child_name"`
	ChildLabel   *string  `gorm:"column:child_label" json:"child_label"`
	ChildVersion *int     `gorm:"column:child_version" json:"child_version"`
	Project      *Project
	Parent       *Prompt `gorm:"foreignKey:ParentID"`
}

func (PromptDependency) TableName() string {
	return "prompt_dependencies"
}

func (m *PromptDependency) BeforeCreate(tx *gorm.DB) error {
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

func (*PromptDependency) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type PromptProtectedLabels struct {
	ID        string   `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt DateTime `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt DateTime `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	ProjectID string   `gorm:"column:project_id;uniqueIndex:prompt_protected_labels_project_id_label_key;not null" json:"project_id"`
	Label     string   `gorm:"column:label;uniqueIndex:prompt_protected_labels_project_id_label_key;not null" json:"label"`
	Project   *Project
}

func (PromptProtectedLabels) TableName() string {
	return "prompt_protected_labels"
}

func (m *PromptProtectedLabels) BeforeCreate(tx *gorm.DB) error {
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

func (*PromptProtectedLabels) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Skill struct {
	ID            string         `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt     DateTime       `gorm:"column:created_at;index:skills_created_at_idx;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt     DateTime       `gorm:"column:updated_at;index:skills_updated_at_idx;autoUpdateTime:false;not null" json:"updated_at"`
	ProjectID     string         `gorm:"column:project_id;uniqueIndex:skills_project_id_name_version_key;not null" json:"project_id"`
	CreatedBy     string         `gorm:"column:created_by;not null" json:"created_by"`
	Name          string         `gorm:"column:name;uniqueIndex:skills_project_id_name_version_key;not null" json:"name"`
	Description   string         `gorm:"column:description;not null" json:"description"`
	Frontmatter   datatypes.JSON `gorm:"column:frontmatter;default:'{}';not null" json:"frontmatter"`
	Version       int            `gorm:"column:version;uniqueIndex:skills_project_id_name_version_key;not null" json:"version"`
	Tags          []string       `gorm:"column:tags;index:skills_tags_idx;serializer:json;not null" json:"tags"`
	Labels        []string       `gorm:"column:labels;index:skills_labels_idx;serializer:json;not null" json:"labels"`
	CommitMessage *string        `gorm:"column:commit_message" json:"commit_message"`
	Project       *Project
	Files         []SkillFile `gorm:"foreignKey:SkillID;constraint:OnDelete:CASCADE"`
}

func (Skill) TableName() string {
	return "skills"
}

func (m *Skill) BeforeCreate(tx *gorm.DB) error {
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

func (*Skill) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type SkillFile struct {
	ID        string   `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt DateTime `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	ProjectID string   `gorm:"column:project_id;uniqueIndex:skill_files_project_id_skill_id_path_key;index:skill_files_project_id_blob_id_idx;not null" json:"project_id"`
	SkillID   string   `gorm:"column:skill_id;uniqueIndex:skill_files_project_id_skill_id_path_key;not null" json:"skill_id"`
	BlobID    string   `gorm:"column:blob_id;index:skill_files_project_id_blob_id_idx;not null" json:"blob_id"`
	Path      string   `gorm:"column:path;uniqueIndex:skill_files_project_id_skill_id_path_key;not null" json:"path"`
	Project   *Project
	Skill     *Skill
	Blob      *SkillBlob `gorm:"foreignKey:BlobID"`
}

func (SkillFile) TableName() string {
	return "skill_files"
}

func (m *SkillFile) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type SkillBlob struct {
	ID            string   `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt     DateTime `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	ProjectID     string   `gorm:"column:project_id;uniqueIndex:skill_blobs_project_id_sha_256_hash_key;not null" json:"project_id"`
	Sha256Hash    string   `gorm:"column:sha_256_hash;uniqueIndex:skill_blobs_project_id_sha_256_hash_key;type:varchar(44);not null" json:"sha_256_hash"`
	ContentType   string   `gorm:"column:content_type;not null" json:"content_type"`
	ContentLength int      `gorm:"column:content_length;not null" json:"content_length"`
	Content       string   `gorm:"column:content;type:text;not null" json:"content"`
	Project       *Project
	Files         []SkillFile `gorm:"foreignKey:BlobID;constraint:OnDelete:RESTRICT"`
}

func (SkillBlob) TableName() string {
	return "skill_blobs"
}

func (m *SkillBlob) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type Model struct {
	ID              string         `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt       DateTime       `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt       DateTime       `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	ProjectID       *string        `gorm:"column:project_id;uniqueIndex:models_project_id_model_name_start_date_unit_key" json:"project_id"`
	ModelName       string         `gorm:"column:model_name;uniqueIndex:models_project_id_model_name_start_date_unit_key;index:models_model_name_idx;not null" json:"model_name"`
	MatchPattern    string         `gorm:"column:match_pattern;not null" json:"match_pattern"`
	StartDate       *DateTime      `gorm:"column:start_date;uniqueIndex:models_project_id_model_name_start_date_unit_key" json:"start_date"`
	InputPrice      *float64       `gorm:"column:input_price" json:"input_price"`
	OutputPrice     *float64       `gorm:"column:output_price" json:"output_price"`
	TotalPrice      *float64       `gorm:"column:total_price" json:"total_price"`
	Unit            *string        `gorm:"column:unit;uniqueIndex:models_project_id_model_name_start_date_unit_key" json:"unit"`
	TokenizerID     *string        `gorm:"column:tokenizer_id" json:"tokenizer_id"`
	TokenizerConfig datatypes.JSON `gorm:"column:tokenizer_config" json:"tokenizer_config"`
	Project         *Project
	Price           []Price       `gorm:"foreignKey:ModelID;constraint:OnDelete:CASCADE"`
	PricingTiers    []PricingTier `gorm:"foreignKey:ModelID;constraint:OnDelete:CASCADE"`
}

func (Model) TableName() string {
	return "models"
}

func (m *Model) BeforeCreate(tx *gorm.DB) error {
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

func (*Model) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Price struct {
	ID            string   `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt     DateTime `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt     DateTime `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	ModelID       string   `gorm:"column:model_id;uniqueIndex:prices_model_id_usage_type_pricing_tier_id_key;not null" json:"model_id"`
	ProjectID     *string  `gorm:"column:project_id" json:"project_id"`
	PricingTierID string   `gorm:"column:pricing_tier_id;uniqueIndex:prices_model_id_usage_type_pricing_tier_id_key;index:prices_pricing_tier_id_idx;not null" json:"pricing_tier_id"`
	UsageType     string   `gorm:"column:usage_type;uniqueIndex:prices_model_id_usage_type_pricing_tier_id_key;not null" json:"usage_type"`
	Price         float64  `gorm:"column:price;not null" json:"price"`
	Model         *Model
	Project       *Project
	PricingTier   *PricingTier
}

func (Price) TableName() string {
	return "prices"
}

func (m *Price) BeforeCreate(tx *gorm.DB) error {
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

func (*Price) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type PricingTier struct {
	ID         string         `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt  DateTime       `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt  DateTime       `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	ModelID    string         `gorm:"column:model_id;uniqueIndex:pricing_tiers_model_id_priority_key;uniqueIndex:pricing_tiers_model_id_name_key;not null" json:"model_id"`
	Name       string         `gorm:"column:name;uniqueIndex:pricing_tiers_model_id_name_key;not null" json:"name"`
	IsDefault  bool           `gorm:"column:is_default;default:false;not null" json:"is_default"`
	Priority   int            `gorm:"column:priority;uniqueIndex:pricing_tiers_model_id_priority_key;not null" json:"priority"`
	Conditions datatypes.JSON `gorm:"column:conditions;type:jsonb;not null" json:"conditions"`
	Model      *Model
	Prices     []Price `gorm:"foreignKey:PricingTierID;constraint:OnDelete:CASCADE"`
}

func (PricingTier) TableName() string {
	return "pricing_tiers"
}

func (m *PricingTier) BeforeCreate(tx *gorm.DB) error {
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

func (*PricingTier) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type AuditLog struct {
	ID              string   `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt       DateTime `gorm:"column:created_at;index:audit_logs_created_at_idx;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt       DateTime `gorm:"column:updated_at;index:audit_logs_updated_at_idx;autoUpdateTime:false;not null" json:"updated_at"`
	Type            string   `gorm:"column:type;default:'USER';not null" json:"type"`
	APIKeyID        *string  `gorm:"column:api_key_id;index:audit_logs_api_key_id_idx" json:"api_key_id"`
	UserID          *string  `gorm:"column:user_id;index:audit_logs_user_id_idx" json:"user_id"`
	OrgID           string   `gorm:"column:org_id;index:audit_logs_org_id_idx;not null" json:"org_id"`
	UserOrgRole     *string  `gorm:"column:user_org_role" json:"user_org_role"`
	ProjectID       *string  `gorm:"column:project_id;index:audit_logs_project_id_idx" json:"project_id"`
	UserProjectRole *string  `gorm:"column:user_project_role" json:"user_project_role"`
	ResourceType    string   `gorm:"column:resource_type;not null" json:"resource_type"`
	ResourceID      string   `gorm:"column:resource_id;not null" json:"resource_id"`
	Action          string   `gorm:"column:action;not null" json:"action"`
	Before          *string  `gorm:"column:before" json:"before"`
	After           *string  `gorm:"column:after" json:"after"`
}

func (AuditLog) TableName() string {
	return "audit_logs"
}

func (m *AuditLog) BeforeCreate(tx *gorm.DB) error {
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

func (*AuditLog) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type AuditLogStaging struct {
	ID        string         `gorm:"column:id;primaryKey;type:char(36)" json:"id"`
	CreatedAt DateTime       `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	Payload   datatypes.JSON `gorm:"column:payload;not null" json:"payload"`
	FailedAt  *DateTime      `gorm:"column:failed_at" json:"failed_at"`
	LastError *string        `gorm:"column:last_error" json:"last_error"`
}

func (AuditLogStaging) TableName() string {
	return "audit_log_staging"
}

func (m *AuditLogStaging) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type EvalTemplate struct {
	ID                 string         `gorm:"column:id;primaryKey;index:eval_templates_project_id_id_idx" json:"id"`
	CreatedAt          DateTime       `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt          DateTime       `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	ProjectID          *string        `gorm:"column:project_id;uniqueIndex:eval_templates_project_id_name_version_key;index:eval_templates_project_id_id_idx" json:"project_id"`
	Name               string         `gorm:"column:name;uniqueIndex:eval_templates_project_id_name_version_key;not null" json:"name"`
	Version            int            `gorm:"column:version;uniqueIndex:eval_templates_project_id_name_version_key;not null" json:"version"`
	Prompt             *string        `gorm:"column:prompt" json:"prompt"`
	Type               string         `gorm:"column:type;default:'LLM_AS_JUDGE';not null" json:"type"`
	Partner            *string        `gorm:"column:partner" json:"partner"`
	Model              *string        `gorm:"column:model" json:"model"`
	Provider           *string        `gorm:"column:provider" json:"provider"`
	ModelParams        datatypes.JSON `gorm:"column:model_params" json:"model_params"`
	Vars               []string       `gorm:"column:vars;serializer:json;not null" json:"vars"`
	OutputDefinition   datatypes.JSON `gorm:"column:output_schema" json:"output_schema"`
	SourceCode         *string        `gorm:"column:source_code;type:varchar(262144)" json:"source_code"`
	SourceCodeLanguage *string        `gorm:"column:source_code_language" json:"source_code_language"`
	Project            *Project
	JobConfiguration   []JobConfiguration `gorm:"foreignKey:EvalTemplateID;constraint:OnDelete:SET NULL"`
}

func (EvalTemplate) TableName() string {
	return "eval_templates"
}

func (m *EvalTemplate) BeforeCreate(tx *gorm.DB) error {
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

func (*EvalTemplate) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Evaluator struct {
	ID              string    `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt       DateTime  `gorm:"column:created_at;index:evaluators_project_id_created_at_idx;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt       DateTime  `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	ProjectID       string    `gorm:"column:project_id;index:evaluators_project_id_created_at_idx;not null" json:"project_id"`
	Name            string    `gorm:"column:name;not null" json:"name"`
	Type            string    `gorm:"column:type;not null" json:"type"`
	IsBuiltIn       bool      `gorm:"column:is_built_in;default:false;not null" json:"is_built_in"`
	Description     *string   `gorm:"column:description" json:"description"`
	CreatedByUserID *string   `gorm:"column:created_by_user_id" json:"created_by_user_id"`
	BlockedAt       *DateTime `gorm:"column:blocked_at" json:"blocked_at"`
	BlockReason     *string   `gorm:"column:block_reason" json:"block_reason"`
	BlockMessage    *string   `gorm:"column:block_message" json:"block_message"`
	Project         *Project
	CreatedByUser   *User                               `gorm:"foreignKey:CreatedByUserID"`
	Versions        []EvaluatorVersion                  `gorm:"foreignKey:EvaluatorID;constraint:OnDelete:CASCADE"`
	Assignments     []EvaluationRuleEvaluatorAssignment `gorm:"foreignKey:EvaluatorID;constraint:OnDelete:CASCADE"`
}

func (Evaluator) TableName() string {
	return "evaluators"
}

func (m *Evaluator) BeforeCreate(tx *gorm.DB) error {
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

func (*Evaluator) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type EvaluatorVersion struct {
	ID                  string         `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt           DateTime       `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	EvaluatorID         string         `gorm:"column:evaluator_id;uniqueIndex:evaluator_versions_evaluator_id_version_key;not null" json:"evaluator_id"`
	Version             int            `gorm:"column:version;uniqueIndex:evaluator_versions_evaluator_id_version_key;not null" json:"version"`
	CreatedByUserID     *string        `gorm:"column:created_by_user_id" json:"created_by_user_id"`
	Prompt              *string        `gorm:"column:prompt" json:"prompt"`
	PromptMessages      datatypes.JSON `gorm:"column:prompt_messages" json:"prompt_messages"`
	Partner             *string        `gorm:"column:partner" json:"partner"`
	Model               *string        `gorm:"column:model" json:"model"`
	Provider            *string        `gorm:"column:provider" json:"provider"`
	ModelParams         datatypes.JSON `gorm:"column:model_params" json:"model_params"`
	Vars                []string       `gorm:"column:vars;serializer:json;not null" json:"vars"`
	VariableMapping     datatypes.JSON `gorm:"column:variable_mapping" json:"variable_mapping"`
	OutputDefinition    datatypes.JSON `gorm:"column:output_definition" json:"output_definition"`
	SourceCode          *string        `gorm:"column:source_code;type:varchar(262144)" json:"source_code"`
	SourceCodeLanguage  *string        `gorm:"column:source_code_language" json:"source_code_language"`
	Questions           datatypes.JSON `gorm:"column:questions" json:"questions"`
	Evaluator           *Evaluator
	CreatedByUser       *User                `gorm:"foreignKey:CreatedByUserID"`
	TopicClusteringRuns []TopicClusteringRun `gorm:"foreignKey:FacetID,FacetVersion;references:EvaluatorID,Version;constraint:OnDelete:CASCADE"`
}

func (EvaluatorVersion) TableName() string {
	return "evaluator_versions"
}

func (m *EvaluatorVersion) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type JobConfiguration struct {
	ID              string         `gorm:"column:id;primaryKey;index:job_configurations_project_id_id_idx" json:"id"`
	CreatedAt       DateTime       `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt       DateTime       `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	ProjectID       string         `gorm:"column:project_id;index:job_configurations_project_id_id_idx;not null" json:"project_id"`
	JobType         string         `gorm:"column:job_type;not null" json:"job_type"`
	Status          string         `gorm:"column:status;default:'ACTIVE';not null" json:"status"`
	BlockedAt       *DateTime      `gorm:"column:blocked_at" json:"blocked_at"`
	BlockReason     *string        `gorm:"column:block_reason" json:"block_reason"`
	BlockMessage    *string        `gorm:"column:block_message" json:"block_message"`
	EvalTemplateID  *string        `gorm:"column:eval_template_id" json:"eval_template_id"`
	ScoreName       string         `gorm:"column:score_name;not null" json:"score_name"`
	Filter          datatypes.JSON `gorm:"column:filter;not null" json:"filter"`
	TargetObject    string         `gorm:"column:target_object;not null" json:"target_object"`
	VariableMapping datatypes.JSON `gorm:"column:variable_mapping;not null" json:"variable_mapping"`
	Sampling        float64        `gorm:"column:sampling;not null" json:"sampling"`
	Delay           int            `gorm:"column:delay;not null" json:"delay"`
	TimeScope       []string       `gorm:"column:time_scope;serializer:json;not null" json:"time_scope"`
	Project         *Project
	EvalTemplate    *EvalTemplate
}

func (JobConfiguration) TableName() string {
	return "job_configurations"
}

func (m *JobConfiguration) BeforeCreate(tx *gorm.DB) error {
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

func (*JobConfiguration) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type EvaluationRule struct {
	ID              string         `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt       DateTime       `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt       DateTime       `gorm:"column:updated_at;index:evaluation_rules_project_id_updated_at_idx;autoUpdateTime:false;not null" json:"updated_at"`
	ProjectID       string         `gorm:"column:project_id;index:evaluation_rules_project_id_updated_at_idx;not null" json:"project_id"`
	CreatedByUserID *string        `gorm:"column:created_by_user_id" json:"created_by_user_id"`
	Name            string         `gorm:"column:name;not null" json:"name"`
	Status          string         `gorm:"column:status;default:'ACTIVE';not null" json:"status"`
	TargetObject    string         `gorm:"column:target_object;not null" json:"target_object"`
	Filter          datatypes.JSON `gorm:"column:filter;not null" json:"filter"`
	Sampling        float64        `gorm:"column:sampling;not null" json:"sampling"`
	Delay           int            `gorm:"column:delay;not null" json:"delay"`
	IdleTime        *int           `gorm:"column:idle_time" json:"idle_time"`
	TimeScope       []string       `gorm:"column:time_scope;serializer:json;not null" json:"time_scope"`
	Project         *Project
	CreatedByUser   *User                               `gorm:"foreignKey:CreatedByUserID"`
	Assignments     []EvaluationRuleEvaluatorAssignment `gorm:"foreignKey:EvaluationRuleID;constraint:OnDelete:CASCADE"`
}

func (EvaluationRule) TableName() string {
	return "evaluation_rules"
}

func (m *EvaluationRule) BeforeCreate(tx *gorm.DB) error {
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

func (*EvaluationRule) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type TracingReadinessSettings struct {
	ProjectID     string   `gorm:"column:project_id;primaryKey" json:"project_id"`
	CreatedAt     DateTime `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt     DateTime `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	CreatedBy     *string  `gorm:"column:created_by" json:"created_by"`
	UpdatedBy     *string  `gorm:"column:updated_by" json:"updated_by"`
	TraceIdleMs   *int     `gorm:"column:trace_idle_ms" json:"trace_idle_ms"`
	Project       *Project
	CreatedByUser *User `gorm:"foreignKey:CreatedBy"`
	UpdatedByUser *User `gorm:"foreignKey:UpdatedBy"`
}

func (TracingReadinessSettings) TableName() string {
	return "tracing_readiness_settings"
}

func (m *TracingReadinessSettings) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*TracingReadinessSettings) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type EvaluationRuleEvaluatorAssignment struct {
	ID               string         `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt        DateTime       `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt        DateTime       `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	ProjectID        string         `gorm:"column:project_id;index:evaluation_rule_assignments_project_id_idx;not null" json:"project_id"`
	EvaluationRuleID string         `gorm:"column:evaluation_rule_id;uniqueIndex:evaluation_rule_evaluator_assignments_evaluation_rule_id_evaluator_id_key;not null" json:"evaluation_rule_id"`
	EvaluatorID      string         `gorm:"column:evaluator_id;uniqueIndex:evaluation_rule_evaluator_assignments_evaluation_rule_id_evaluator_id_key;index:evaluation_rule_assignments_evaluator_id_idx;not null" json:"evaluator_id"`
	VariableMapping  datatypes.JSON `gorm:"column:variable_mapping" json:"variable_mapping"`
	Project          *Project
	EvaluationRule   *EvaluationRule
	Evaluator        *Evaluator
}

func (EvaluationRuleEvaluatorAssignment) TableName() string {
	return "evaluation_rule_evaluator_assignments"
}

func (m *EvaluationRuleEvaluatorAssignment) BeforeCreate(tx *gorm.DB) error {
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

func (*EvaluationRuleEvaluatorAssignment) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type JobExecution struct {
	ID                           string    `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt                    DateTime  `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt                    DateTime  `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	ProjectID                    string    `gorm:"column:project_id;index:job_executions_project_id_job_configuration_id_job_input_trace_id_idx;index:job_executions_project_id_job_output_score_id_idx;not null" json:"project_id"`
	JobConfigurationID           string    `gorm:"column:job_configuration_id;index:job_executions_project_id_job_configuration_id_job_input_trace_id_idx;index:job_executions_job_configuration_id_idx;not null" json:"job_configuration_id"`
	JobTemplateID                *string   `gorm:"column:job_template_id" json:"job_template_id"`
	Status                       string    `gorm:"column:status;not null" json:"status"`
	StartTime                    *DateTime `gorm:"column:start_time" json:"start_time"`
	EndTime                      *DateTime `gorm:"column:end_time" json:"end_time"`
	Error                        *string   `gorm:"column:error" json:"error"`
	JobInputTraceID              *string   `gorm:"column:job_input_trace_id;index:job_executions_project_id_job_configuration_id_job_input_trace_id_idx" json:"job_input_trace_id"`
	JobInputTraceTimestamp       *DateTime `gorm:"column:job_input_trace_timestamp" json:"job_input_trace_timestamp"`
	JobInputObservationID        *string   `gorm:"column:job_input_observation_id" json:"job_input_observation_id"`
	JobInputDatasetItemID        *string   `gorm:"column:job_input_dataset_item_id" json:"job_input_dataset_item_id"`
	JobInputDatasetItemValidFrom *DateTime `gorm:"column:job_input_dataset_item_valid_from" json:"job_input_dataset_item_valid_from"`
	JobOutputScoreID             *string   `gorm:"column:job_output_score_id;index:job_executions_project_id_job_output_score_id_idx" json:"job_output_score_id"`
	ExecutionTraceID             *string   `gorm:"column:execution_trace_id" json:"execution_trace_id"`
	Project                      *Project
}

func (JobExecution) TableName() string {
	return "job_executions"
}

func (m *JobExecution) BeforeCreate(tx *gorm.DB) error {
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

func (*JobExecution) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type DefaultLlmModel struct {
	ID          string         `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt   DateTime       `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt   DateTime       `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	ProjectID   string         `gorm:"column:project_id;uniqueIndex;not null" json:"project_id"`
	LlmAPIKeyID string         `gorm:"column:llm_api_key_id;not null" json:"llm_api_key_id"`
	Provider    string         `gorm:"column:provider;not null" json:"provider"`
	Adapter     string         `gorm:"column:adapter;not null" json:"adapter"`
	Model       string         `gorm:"column:model;not null" json:"model"`
	ModelParams datatypes.JSON `gorm:"column:model_params" json:"model_params"`
	Project     *Project
	LlmAPIKey   *LlmAPIKeys `gorm:"foreignKey:LlmAPIKeyID"`
}

func (DefaultLlmModel) TableName() string {
	return "default_llm_models"
}

func (m *DefaultLlmModel) BeforeCreate(tx *gorm.DB) error {
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

func (*DefaultLlmModel) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type TopicConfig struct {
	ID                    string    `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt             DateTime  `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt             DateTime  `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	CreatedBy             *string   `gorm:"column:created_by" json:"created_by"`
	UpdatedBy             *string   `gorm:"column:updated_by" json:"updated_by"`
	ProjectID             string    `gorm:"column:project_id;uniqueIndex;not null" json:"project_id"`
	Enabled               bool      `gorm:"column:enabled;default:false;not null" json:"enabled"`
	BlockedAt             *DateTime `gorm:"column:blocked_at" json:"blocked_at"`
	BlockReason           *string   `gorm:"column:block_reason" json:"block_reason"`
	BlockMessage          *string   `gorm:"column:block_message" json:"block_message"`
	SummaryLlmAPIKeyID    *string   `gorm:"column:summary_llm_api_key_id;index:topic_configs_summary_llm_api_key_id_idx" json:"summary_llm_api_key_id"`
	SummaryModel          *string   `gorm:"column:summary_model" json:"summary_model"`
	EmbeddingLlmAPIKeyID  *string   `gorm:"column:embedding_llm_api_key_id;index:topic_configs_embedding_llm_api_key_id_idx" json:"embedding_llm_api_key_id"`
	EmbeddingModel        *string   `gorm:"column:embedding_model" json:"embedding_model"`
	EmbeddingDimensions   *int      `gorm:"column:embedding_dimensions;default:1024;not null" json:"embedding_dimensions"`
	ClusteringLlmAPIKeyID *string   `gorm:"column:clustering_llm_api_key_id;index:topic_configs_clustering_llm_api_key_id_idx" json:"clustering_llm_api_key_id"`
	ClusteringModel       *string   `gorm:"column:clustering_model" json:"clustering_model"`
	CreatedByUser         *User     `gorm:"foreignKey:CreatedBy"`
	UpdatedByUser         *User     `gorm:"foreignKey:UpdatedBy"`
	Project               *Project
	SummaryLlmAPIKey      *LlmAPIKeys `gorm:"foreignKey:SummaryLlmAPIKeyID"`
	EmbeddingLlmAPIKey    *LlmAPIKeys `gorm:"foreignKey:EmbeddingLlmAPIKeyID"`
	ClusteringLlmAPIKey   *LlmAPIKeys `gorm:"foreignKey:ClusteringLlmAPIKeyID"`
}

func (TopicConfig) TableName() string {
	return "topic_configs"
}

func (m *TopicConfig) BeforeCreate(tx *gorm.DB) error {
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

func (*TopicConfig) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type SsoConfig struct {
	Domain       string         `gorm:"column:domain;primaryKey" json:"domain"`
	CreatedAt    DateTime       `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt    DateTime       `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	AuthProvider string         `gorm:"column:auth_provider;not null" json:"auth_provider"`
	AuthConfig   datatypes.JSON `gorm:"column:auth_config" json:"auth_config"`
}

func (SsoConfig) TableName() string {
	return "sso_configs"
}

func (m *SsoConfig) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.Domain == "" {
		m.Domain = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*SsoConfig) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type VerifiedDomain struct {
	ID                string    `gorm:"column:id;primaryKey;type:char(36)" json:"id"`
	OrganizationID    string    `gorm:"column:organization_id;uniqueIndex:verified_domains_organization_id_domain_key;index:verified_domains_organization_id_idx;not null" json:"organization_id"`
	Domain            string    `gorm:"column:domain;uniqueIndex:verified_domains_organization_id_domain_key;not null" json:"domain"`
	VerificationToken string    `gorm:"column:verification_token;uniqueIndex;not null" json:"verification_token"`
	VerifiedAt        *DateTime `gorm:"column:verified_at" json:"verified_at"`
	CreatedByUserID   *string   `gorm:"column:created_by_user_id" json:"created_by_user_id"`
	CreatedAt         DateTime  `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt         DateTime  `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	Organization      *Organization
}

func (VerifiedDomain) TableName() string {
	return "verified_domains"
}

func (m *VerifiedDomain) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = uuid.NewString()
	}
	if m.VerificationToken == "" {
		m.VerificationToken = uuid.NewString()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*VerifiedDomain) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type PosthogIntegration struct {
	ProjectID              string    `gorm:"column:project_id;primaryKey" json:"project_id"`
	EncryptedPosthogAPIKey string    `gorm:"column:encrypted_posthog_api_key;not null" json:"encrypted_posthog_api_key"`
	PosthogHostName        string    `gorm:"column:posthog_host_name;not null" json:"posthog_host_name"`
	LastSyncAt             *DateTime `gorm:"column:last_sync_at" json:"last_sync_at"`
	Enabled                bool      `gorm:"column:enabled;not null" json:"enabled"`
	CreatedAt              DateTime  `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	ExportSource           string    `gorm:"column:export_source;default:'TRACES_OBSERVATIONS';not null" json:"export_source"`
	Backfill               *bool     `gorm:"column:backfill;default:true;not null" json:"backfill"`
	LastError              *string   `gorm:"column:last_error" json:"last_error"`
	LastErrorAt            *DateTime `gorm:"column:last_error_at" json:"last_error_at"`
	Project                *Project
}

func (PosthogIntegration) TableName() string {
	return "posthog_integrations"
}

func (m *PosthogIntegration) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type MixpanelIntegration struct {
	ProjectID                     string    `gorm:"column:project_id;primaryKey" json:"project_id"`
	EncryptedMixpanelProjectToken string    `gorm:"column:encrypted_mixpanel_project_token;not null" json:"encrypted_mixpanel_project_token"`
	MixpanelRegion                string    `gorm:"column:mixpanel_region;not null" json:"mixpanel_region"`
	LastSyncAt                    *DateTime `gorm:"column:last_sync_at" json:"last_sync_at"`
	Enabled                       bool      `gorm:"column:enabled;not null" json:"enabled"`
	CreatedAt                     DateTime  `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	ExportSource                  string    `gorm:"column:export_source;default:'TRACES_OBSERVATIONS';not null" json:"export_source"`
	Backfill                      *bool     `gorm:"column:backfill;default:true;not null" json:"backfill"`
	Project                       *Project
}

func (MixpanelIntegration) TableName() string {
	return "mixpanel_integrations"
}

func (m *MixpanelIntegration) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type BlobStorageIntegration struct {
	ProjectID                     string         `gorm:"column:project_id;primaryKey" json:"project_id"`
	Type                          string         `gorm:"column:type;not null" json:"type"`
	BucketName                    string         `gorm:"column:bucket_name;not null" json:"bucket_name"`
	Prefix                        string         `gorm:"column:prefix;not null" json:"prefix"`
	AccessKeyID                   *string        `gorm:"column:access_key_id" json:"access_key_id"`
	SecretAccessKey               *string        `gorm:"column:secret_access_key" json:"secret_access_key"`
	Region                        string         `gorm:"column:region;not null" json:"region"`
	Endpoint                      *string        `gorm:"column:endpoint" json:"endpoint"`
	ForcePathStyle                bool           `gorm:"column:force_path_style;not null" json:"force_path_style"`
	NextSyncAt                    *DateTime      `gorm:"column:next_sync_at" json:"next_sync_at"`
	LastSyncAt                    *DateTime      `gorm:"column:last_sync_at" json:"last_sync_at"`
	Enabled                       bool           `gorm:"column:enabled;not null" json:"enabled"`
	ExportFrequency               string         `gorm:"column:export_frequency;not null" json:"export_frequency"`
	FileType                      string         `gorm:"column:file_type;default:'CSV';not null" json:"file_type"`
	ExportMode                    string         `gorm:"column:export_mode;default:'FULL_HISTORY';not null" json:"export_mode"`
	ExportStartDate               *DateTime      `gorm:"column:export_start_date" json:"export_start_date"`
	ExportSource                  string         `gorm:"column:export_source;default:'TRACES_OBSERVATIONS';not null" json:"export_source"`
	ExportFieldGroups             []string       `gorm:"column:export_field_groups;serializer:json;not null" json:"export_field_groups"`
	Compressed                    *bool          `gorm:"column:compressed;default:true;not null" json:"compressed"`
	ExportTuning                  datatypes.JSON `gorm:"column:export_tuning" json:"export_tuning"`
	RunStartedAt                  *DateTime      `gorm:"column:run_started_at" json:"run_started_at"`
	Backfill                      *bool          `gorm:"column:backfill;default:true;not null" json:"backfill"`
	LastError                     *string        `gorm:"column:last_error" json:"last_error"`
	LastErrorAt                   *DateTime      `gorm:"column:last_error_at" json:"last_error_at"`
	LastFailureNotificationSentAt *DateTime      `gorm:"column:last_failure_notification_sent_at" json:"last_failure_notification_sent_at"`
	CreatedAt                     DateTime       `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt                     DateTime       `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	Project                       *Project
}

func (BlobStorageIntegration) TableName() string {
	return "blob_storage_integrations"
}

func (m *BlobStorageIntegration) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*BlobStorageIntegration) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type ExternalMediaStorageIntegration struct {
	ProjectID       string   `gorm:"column:project_id;primaryKey" json:"project_id"`
	Type            string   `gorm:"column:type;not null" json:"type"`
	BucketName      string   `gorm:"column:bucket_name;not null" json:"bucket_name"`
	Prefix          *string  `gorm:"column:prefix" json:"prefix"`
	AccessKeyID     *string  `gorm:"column:access_key_id" json:"access_key_id"`
	SecretAccessKey *string  `gorm:"column:secret_access_key" json:"secret_access_key"`
	Region          string   `gorm:"column:region;not null" json:"region"`
	Endpoint        *string  `gorm:"column:endpoint" json:"endpoint"`
	ForcePathStyle  bool     `gorm:"column:force_path_style;not null" json:"force_path_style"`
	CreatedAt       DateTime `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt       DateTime `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	Project         *Project
}

func (ExternalMediaStorageIntegration) TableName() string {
	return "external_media_storage_integrations"
}

func (m *ExternalMediaStorageIntegration) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*ExternalMediaStorageIntegration) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type WebCalloutEndpoint struct {
	ID                string   `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt         DateTime `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt         DateTime `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	ProjectID         string   `gorm:"column:project_id;index:web_callout_endpoints_project_id_idx;index:web_callout_endpoints_project_id_enabled_idx;not null" json:"project_id"`
	Name              *string  `gorm:"column:name;default:'Default';not null" json:"name"`
	URL               string   `gorm:"column:url;not null" json:"url"`
	Enabled           *bool    `gorm:"column:enabled;index:web_callout_endpoints_project_id_enabled_idx;default:true;not null" json:"enabled"`
	ToastMessage      *string  `gorm:"column:toast_message;default:'Callout sent';not null" json:"toast_message"`
	RequestHeaders    *string  `gorm:"column:request_headers" json:"request_headers"`
	RequestHeaderKeys []string `gorm:"column:request_header_keys;serializer:json;not null" json:"request_header_keys"`
	Project           *Project
}

func (WebCalloutEndpoint) TableName() string {
	return "web_callout_endpoints"
}

func (m *WebCalloutEndpoint) BeforeCreate(tx *gorm.DB) error {
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

func (*WebCalloutEndpoint) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type BatchExport struct {
	ID         string         `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt  DateTime       `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt  DateTime       `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	ProjectID  string         `gorm:"column:project_id;index:batch_exports_project_id_user_id_idx;not null" json:"project_id"`
	UserID     string         `gorm:"column:user_id;index:batch_exports_project_id_user_id_idx;not null" json:"user_id"`
	FinishedAt *DateTime      `gorm:"column:finished_at" json:"finished_at"`
	ExpiresAt  *DateTime      `gorm:"column:expires_at" json:"expires_at"`
	Name       string         `gorm:"column:name;not null" json:"name"`
	Status     string         `gorm:"column:status;index:batch_exports_status_idx;not null" json:"status"`
	Query      datatypes.JSON `gorm:"column:query;not null" json:"query"`
	Format     string         `gorm:"column:format;not null" json:"format"`
	URL        *string        `gorm:"column:url" json:"url"`
	Log        *string        `gorm:"column:log" json:"log"`
	Project    *Project
}

func (BatchExport) TableName() string {
	return "batch_exports"
}

func (m *BatchExport) BeforeCreate(tx *gorm.DB) error {
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

func (*BatchExport) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type BatchAction struct {
	ID             string         `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt      DateTime       `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt      DateTime       `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	ProjectID      string         `gorm:"column:project_id;index:batch_actions_project_id_user_id_idx;index:batch_actions_project_id_action_type_idx;not null" json:"project_id"`
	UserID         string         `gorm:"column:user_id;index:batch_actions_project_id_user_id_idx;not null" json:"user_id"`
	ActionType     string         `gorm:"column:action_type;index:batch_actions_project_id_action_type_idx;not null" json:"action_type"`
	TableName_     string         `gorm:"column:table_name;not null" json:"table_name"`
	Status         string         `gorm:"column:status;index:batch_actions_status_idx;not null" json:"status"`
	FinishedAt     *DateTime      `gorm:"column:finished_at" json:"finished_at"`
	Query          datatypes.JSON `gorm:"column:query;not null" json:"query"`
	Config         datatypes.JSON `gorm:"column:config" json:"config"`
	TotalCount     *int           `gorm:"column:total_count" json:"total_count"`
	ProcessedCount *int           `gorm:"column:processed_count" json:"processed_count"`
	FailedCount    *int           `gorm:"column:failed_count" json:"failed_count"`
	Log            *string        `gorm:"column:log" json:"log"`
	Project        *Project
}

func (BatchAction) TableName() string {
	return "batch_actions"
}

func (m *BatchAction) BeforeCreate(tx *gorm.DB) error {
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

func (*BatchAction) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Media struct {
	ID               string    `gorm:"column:id;primaryKey;uniqueIndex:media_project_id_id_key" json:"id"`
	Sha256Hash       string    `gorm:"column:sha_256_hash;uniqueIndex:media_project_id_sha_256_hash_key;type:varchar(44);not null" json:"sha_256_hash"`
	ProjectID        string    `gorm:"column:project_id;primaryKey;uniqueIndex:media_project_id_id_key;uniqueIndex:media_project_id_sha_256_hash_key;index:media_project_id_created_at_idx" json:"project_id"`
	CreatedAt        DateTime  `gorm:"column:created_at;index:media_project_id_created_at_idx;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt        DateTime  `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	UploadedAt       *DateTime `gorm:"column:uploaded_at" json:"uploaded_at"`
	UploadHTTPStatus *int      `gorm:"column:upload_http_status" json:"upload_http_status"`
	UploadHTTPError  *string   `gorm:"column:upload_http_error" json:"upload_http_error"`
	BucketPath       string    `gorm:"column:bucket_path;not null" json:"bucket_path"`
	BucketName       string    `gorm:"column:bucket_name;not null" json:"bucket_name"`
	ContentType      string    `gorm:"column:content_type;not null" json:"content_type"`
	ContentLength    int64     `gorm:"column:content_length;not null" json:"content_length"`
	Project          *Project
}

func (Media) TableName() string {
	return "media"
}

func (m *Media) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*Media) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type TraceMedia struct {
	ID        string   `gorm:"column:id;primaryKey" json:"id"`
	ProjectID string   `gorm:"column:project_id;uniqueIndex:trace_media_project_id_trace_id_media_id_field_key;index:trace_media_project_id_media_id_idx;not null" json:"project_id"`
	CreatedAt DateTime `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt DateTime `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	MediaID   string   `gorm:"column:media_id;uniqueIndex:trace_media_project_id_trace_id_media_id_field_key;index:trace_media_project_id_media_id_idx;not null" json:"media_id"`
	TraceID   string   `gorm:"column:trace_id;uniqueIndex:trace_media_project_id_trace_id_media_id_field_key;not null" json:"trace_id"`
	Field     string   `gorm:"column:field;uniqueIndex:trace_media_project_id_trace_id_media_id_field_key;not null" json:"field"`
	Origin    string   `gorm:"column:origin;default:'UNKNOWN';not null" json:"origin"`
}

func (TraceMedia) TableName() string {
	return "trace_media"
}

func (m *TraceMedia) BeforeCreate(tx *gorm.DB) error {
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

func (*TraceMedia) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type ObservationMedia struct {
	ID            string   `gorm:"column:id;primaryKey" json:"id"`
	ProjectID     string   `gorm:"column:project_id;uniqueIndex:observation_media_project_id_trace_id_observation_id_media_id_field_key;index:observation_media_project_id_media_id_idx;not null" json:"project_id"`
	CreatedAt     DateTime `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt     DateTime `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	MediaID       string   `gorm:"column:media_id;uniqueIndex:observation_media_project_id_trace_id_observation_id_media_id_field_key;index:observation_media_project_id_media_id_idx;not null" json:"media_id"`
	TraceID       string   `gorm:"column:trace_id;uniqueIndex:observation_media_project_id_trace_id_observation_id_media_id_field_key;not null" json:"trace_id"`
	ObservationID string   `gorm:"column:observation_id;uniqueIndex:observation_media_project_id_trace_id_observation_id_media_id_field_key;not null" json:"observation_id"`
	Field         string   `gorm:"column:field;uniqueIndex:observation_media_project_id_trace_id_observation_id_media_id_field_key;not null" json:"field"`
	Origin        string   `gorm:"column:origin;default:'UNKNOWN';not null" json:"origin"`
}

func (ObservationMedia) TableName() string {
	return "observation_media"
}

func (m *ObservationMedia) BeforeCreate(tx *gorm.DB) error {
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

func (*ObservationMedia) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type DatasetItemMedia struct {
	ID                   string    `gorm:"column:id;primaryKey" json:"id"`
	ProjectID            string    `gorm:"column:project_id;uniqueIndex:dataset_item_media_project_id_dataset_item_id_dataset_item_valid_from_field_json_path_key;index:dataset_item_media_project_id_media_id_idx;index:dataset_item_media_project_id_dataset_id_idx;not null" json:"project_id"`
	CreatedAt            DateTime  `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt            DateTime  `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	MediaID              string    `gorm:"column:media_id;index:dataset_item_media_project_id_media_id_idx;not null" json:"media_id"`
	DatasetID            string    `gorm:"column:dataset_id;index:dataset_item_media_project_id_dataset_id_idx;not null" json:"dataset_id"`
	DatasetItemID        string    `gorm:"column:dataset_item_id;uniqueIndex:dataset_item_media_project_id_dataset_item_id_dataset_item_valid_from_field_json_path_key;not null" json:"dataset_item_id"`
	DatasetItemValidFrom *DateTime `gorm:"column:dataset_item_valid_from;uniqueIndex:dataset_item_media_project_id_dataset_item_id_dataset_item_valid_from_field_json_path_key" json:"dataset_item_valid_from"`
	Field                string    `gorm:"column:field;uniqueIndex:dataset_item_media_project_id_dataset_item_id_dataset_item_valid_from_field_json_path_key;not null" json:"field"`
	JSONPath             *string   `gorm:"column:json_path;uniqueIndex:dataset_item_media_project_id_dataset_item_id_dataset_item_valid_from_field_json_path_key" json:"json_path"`
	ReferenceString      *string   `gorm:"column:reference_string" json:"reference_string"`
}

func (DatasetItemMedia) TableName() string {
	return "dataset_item_media"
}

func (m *DatasetItemMedia) BeforeCreate(tx *gorm.DB) error {
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

func (*DatasetItemMedia) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type BillingMeterBackup struct {
	StripeCustomerID string   `gorm:"column:stripe_customer_id;primaryKey;uniqueIndex:billing_meter_backups_stripe_customer_id_meter_id_start_time_end_time_key;index:billing_meter_backups_stripe_customer_id_meter_id_start_time_end_time_idx" json:"stripe_customer_id"`
	MeterID          string   `gorm:"column:meter_id;primaryKey;uniqueIndex:billing_meter_backups_stripe_customer_id_meter_id_start_time_end_time_key;index:billing_meter_backups_stripe_customer_id_meter_id_start_time_end_time_idx" json:"meter_id"`
	StartTime        DateTime `gorm:"column:start_time;primaryKey;uniqueIndex:billing_meter_backups_stripe_customer_id_meter_id_start_time_end_time_key;index:billing_meter_backups_stripe_customer_id_meter_id_start_time_end_time_idx" json:"start_time"`
	EndTime          DateTime `gorm:"column:end_time;primaryKey;uniqueIndex:billing_meter_backups_stripe_customer_id_meter_id_start_time_end_time_key;index:billing_meter_backups_stripe_customer_id_meter_id_start_time_end_time_idx" json:"end_time"`
	AggregatedValue  int      `gorm:"column:aggregated_value;not null" json:"aggregated_value"`
	EventName        string   `gorm:"column:event_name;not null" json:"event_name"`
	OrgID            string   `gorm:"column:org_id;not null" json:"org_id"`
	CreatedAt        DateTime `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt        DateTime `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
}

func (BillingMeterBackup) TableName() string {
	return "billing_meter_backups"
}

func (m *BillingMeterBackup) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*BillingMeterBackup) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LlmSchema struct {
	ID          string         `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt   DateTime       `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt   DateTime       `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	ProjectID   string         `gorm:"column:project_id;uniqueIndex:llm_schemas_project_id_name_key;not null" json:"project_id"`
	Name        string         `gorm:"column:name;uniqueIndex:llm_schemas_project_id_name_key;not null" json:"name"`
	Description string         `gorm:"column:description;not null" json:"description"`
	Schema      datatypes.JSON `gorm:"column:schema;not null" json:"schema"`
	Project     *Project
}

func (LlmSchema) TableName() string {
	return "llm_schemas"
}

func (m *LlmSchema) BeforeCreate(tx *gorm.DB) error {
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

func (*LlmSchema) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LlmTool struct {
	ID          string         `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt   DateTime       `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt   DateTime       `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	ProjectID   string         `gorm:"column:project_id;uniqueIndex:llm_tools_project_id_name_key;not null" json:"project_id"`
	Name        string         `gorm:"column:name;uniqueIndex:llm_tools_project_id_name_key;not null" json:"name"`
	Description string         `gorm:"column:description;not null" json:"description"`
	Parameters  datatypes.JSON `gorm:"column:parameters;not null" json:"parameters"`
	Project     *Project
}

func (LlmTool) TableName() string {
	return "llm_tools"
}

func (m *LlmTool) BeforeCreate(tx *gorm.DB) error {
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

func (*LlmTool) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Dashboard struct {
	ID                    string         `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt             DateTime       `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt             DateTime       `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	CreatedBy             *string        `gorm:"column:created_by" json:"created_by"`
	UpdatedBy             *string        `gorm:"column:updated_by" json:"updated_by"`
	ProjectID             *string        `gorm:"column:project_id" json:"project_id"`
	Name                  string         `gorm:"column:name;not null" json:"name"`
	Description           string         `gorm:"column:description;not null" json:"description"`
	Definition            datatypes.JSON `gorm:"column:definition;not null" json:"definition"`
	Filters               datatypes.JSON `gorm:"column:filters;default:'[]';not null" json:"filters"`
	CreatedByUser         *User          `gorm:"foreignKey:CreatedBy"`
	UpdatedByUser         *User          `gorm:"foreignKey:UpdatedBy"`
	Project               *Project
	HomeDashboardProjects []Project `gorm:"foreignKey:HomeDashboardID;constraint:OnDelete:SET NULL"`
}

func (Dashboard) TableName() string {
	return "dashboards"
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
	ID            string         `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt     DateTime       `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt     DateTime       `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	CreatedBy     *string        `gorm:"column:created_by" json:"created_by"`
	UpdatedBy     *string        `gorm:"column:updated_by" json:"updated_by"`
	ProjectID     *string        `gorm:"column:project_id" json:"project_id"`
	Name          string         `gorm:"column:name;not null" json:"name"`
	Description   string         `gorm:"column:description;not null" json:"description"`
	View          string         `gorm:"column:view;not null" json:"view"`
	Dimensions    datatypes.JSON `gorm:"column:dimensions;not null" json:"dimensions"`
	Metrics       datatypes.JSON `gorm:"column:metrics;not null" json:"metrics"`
	Filters       datatypes.JSON `gorm:"column:filters;not null" json:"filters"`
	ChartType     string         `gorm:"column:chart_type;not null" json:"chart_type"`
	ChartConfig   datatypes.JSON `gorm:"column:chart_config;not null" json:"chart_config"`
	MinVersion    *int           `gorm:"column:min_version;default:1;not null" json:"min_version"`
	CreatedByUser *User          `gorm:"foreignKey:CreatedBy"`
	UpdatedByUser *User          `gorm:"foreignKey:UpdatedBy"`
	Project       *Project
}

func (DashboardWidget) TableName() string {
	return "dashboard_widgets"
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

type TableViewPreset struct {
	ID               string         `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt        DateTime       `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt        DateTime       `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	ProjectID        string         `gorm:"column:project_id;uniqueIndex:table_view_presets_project_id_table_name_name_key;not null" json:"project_id"`
	Name             string         `gorm:"column:name;uniqueIndex:table_view_presets_project_id_table_name_name_key;not null" json:"name"`
	TableName_       string         `gorm:"column:table_name;uniqueIndex:table_view_presets_project_id_table_name_name_key;not null" json:"table_name"`
	CreatedBy        *string        `gorm:"column:created_by" json:"created_by"`
	UpdatedBy        *string        `gorm:"column:updated_by" json:"updated_by"`
	Filters          datatypes.JSON `gorm:"column:filters;not null" json:"filters"`
	ColumnOrder      datatypes.JSON `gorm:"column:column_order;not null" json:"column_order"`
	ColumnVisibility datatypes.JSON `gorm:"column:column_visibility;not null" json:"column_visibility"`
	SearchQuery      *string        `gorm:"column:search_query" json:"search_query"`
	OrderBy          datatypes.JSON `gorm:"column:order_by" json:"order_by"`
	Project          *Project
	CreatedByUser    *User `gorm:"foreignKey:CreatedBy"`
	UpdatedByUser    *User `gorm:"foreignKey:UpdatedBy"`
}

func (TableViewPreset) TableName() string {
	return "table_view_presets"
}

func (m *TableViewPreset) BeforeCreate(tx *gorm.DB) error {
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

func (*TableViewPreset) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type DefaultView struct {
	ID        string   `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt DateTime `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt DateTime `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	ProjectID string   `gorm:"column:project_id;index:default_views_project_id_view_name_idx;not null" json:"project_id"`
	UserID    *string  `gorm:"column:user_id" json:"user_id"`
	ViewName  string   `gorm:"column:view_name;index:default_views_project_id_view_name_idx;not null" json:"view_name"`
	ViewID    string   `gorm:"column:view_id;not null" json:"view_id"`
	Project   *Project
	User      *User
}

func (DefaultView) TableName() string {
	return "default_views"
}

func (m *DefaultView) BeforeCreate(tx *gorm.DB) error {
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

func (*DefaultView) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Action struct {
	ID                   string         `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt            DateTime       `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt            DateTime       `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	ProjectID            string         `gorm:"column:project_id;index:actions_project_id_idx;not null" json:"project_id"`
	Type                 string         `gorm:"column:type;not null" json:"type"`
	Config               datatypes.JSON `gorm:"column:config;not null" json:"config"`
	Project              *Project
	Automations          []Automation          `gorm:"foreignKey:ActionID;constraint:OnDelete:CASCADE"`
	AutomationExecutions []AutomationExecution `gorm:"foreignKey:ActionID;constraint:OnDelete:CASCADE"`
}

func (Action) TableName() string {
	return "actions"
}

func (m *Action) BeforeCreate(tx *gorm.DB) error {
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

func (*Action) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Trigger struct {
	ID                   string         `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt            DateTime       `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt            DateTime       `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	ProjectID            string         `gorm:"column:project_id;index:triggers_project_id_idx;not null" json:"project_id"`
	EventSource          string         `gorm:"column:eventSource;not null" json:"eventSource"`
	EventActions         []string       `gorm:"column:eventActions;serializer:json;not null" json:"eventActions"`
	Filter               datatypes.JSON `gorm:"column:filter" json:"filter"`
	Status               string         `gorm:"column:status;default:'ACTIVE';not null" json:"status"`
	Project              *Project
	AutomationExecutions []AutomationExecution `gorm:"foreignKey:TriggerID;constraint:OnDelete:CASCADE"`
	Automations          []Automation          `gorm:"foreignKey:TriggerID;constraint:OnDelete:CASCADE"`
}

func (Trigger) TableName() string {
	return "triggers"
}

func (m *Trigger) BeforeCreate(tx *gorm.DB) error {
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

func (*Trigger) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Automation struct {
	ID                  string   `gorm:"column:id;primaryKey" json:"id"`
	Name                string   `gorm:"column:name;index:automations_project_id_name_idx;not null" json:"name"`
	TriggerID           string   `gorm:"column:trigger_id;index:automations_project_id_action_id_trigger_id_idx;not null" json:"trigger_id"`
	ActionID            string   `gorm:"column:action_id;index:automations_project_id_action_id_trigger_id_idx;not null" json:"action_id"`
	CreatedAt           DateTime `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	ProjectID           string   `gorm:"column:project_id;index:automations_project_id_action_id_trigger_id_idx;index:automations_project_id_name_idx;not null" json:"project_id"`
	Trigger             *Trigger
	Action              *Action
	Project             *Project
	AutomationExecution []AutomationExecution `gorm:"foreignKey:AutomationID;constraint:OnDelete:CASCADE"`
}

func (Automation) TableName() string {
	return "automations"
}

func (m *Automation) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type AutomationExecution struct {
	ID           string         `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt    DateTime       `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt    DateTime       `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	SourceID     string         `gorm:"column:source_id;not null" json:"source_id"`
	AutomationID string         `gorm:"column:automation_id;not null" json:"automation_id"`
	TriggerID    string         `gorm:"column:trigger_id;index:automation_executions_trigger_id_idx;not null" json:"trigger_id"`
	ActionID     string         `gorm:"column:action_id;index:automation_executions_action_id_idx;not null" json:"action_id"`
	ProjectID    string         `gorm:"column:project_id;index:automation_executions_project_id_idx;not null" json:"project_id"`
	Status       string         `gorm:"column:status;default:'PENDING';not null" json:"status"`
	Input        datatypes.JSON `gorm:"column:input;not null" json:"input"`
	Output       datatypes.JSON `gorm:"column:output" json:"output"`
	StartedAt    *DateTime      `gorm:"column:started_at" json:"started_at"`
	FinishedAt   *DateTime      `gorm:"column:finished_at" json:"finished_at"`
	Error        *string        `gorm:"column:error" json:"error"`
	Automation   *Automation
	Trigger      *Trigger
	Action       *Action
	Project      *Project
}

func (AutomationExecution) TableName() string {
	return "automation_executions"
}

func (m *AutomationExecution) BeforeCreate(tx *gorm.DB) error {
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

func (*AutomationExecution) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Monitor struct {
	ID                string         `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt         DateTime       `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt         DateTime       `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	CreatedBy         *string        `gorm:"column:created_by" json:"created_by"`
	UpdatedBy         *string        `gorm:"column:updated_by" json:"updated_by"`
	ProjectID         string         `gorm:"column:project_id;index:monitors_project_id_idx;not null" json:"project_id"`
	View              string         `gorm:"column:view;not null" json:"view"`
	Filters           datatypes.JSON `gorm:"column:filters;not null" json:"filters"`
	Metric            datatypes.JSON `gorm:"column:metric;not null" json:"metric"`
	WindowMs          int64          `gorm:"column:window_ms;not null" json:"window_ms"`
	CadenceMs         int64          `gorm:"column:cadence_ms;not null" json:"cadence_ms"`
	ThresholdOperator string         `gorm:"column:threshold_operator;not null" json:"threshold_operator"`
	AlertThreshold    float64        `gorm:"column:alert_threshold;not null" json:"alert_threshold"`
	WarningThreshold  *float64       `gorm:"column:warning_threshold" json:"warning_threshold"`
	Severity          string         `gorm:"column:severity;default:'UNKNOWN';not null" json:"severity"`
	SeverityChangedAt *DateTime      `gorm:"column:severity_changed_at" json:"severity_changed_at"`
	NoData            datatypes.JSON `gorm:"column:no_data;not null" json:"no_data"`
	Renotify          datatypes.JSON `gorm:"column:renotify;not null" json:"renotify"`
	Status            string         `gorm:"column:status;default:'ACTIVE';not null" json:"status"`
	SchedulerBatchID  int64          `gorm:"column:scheduler_batch_id;index:monitors_scheduler_batch_id_idx;index:monitors_scheduler_tick_idx;not null" json:"scheduler_batch_id"`
	NextRunAt         *DateTime      `gorm:"column:next_run_at;index:monitors_scheduler_tick_idx" json:"next_run_at"`
	LastPublishedAt   *DateTime      `gorm:"column:last_published_at" json:"last_published_at"`
	LastClaimedAt     *DateTime      `gorm:"column:last_claimed_at" json:"last_claimed_at"`
	LastCompletedAt   *DateTime      `gorm:"column:last_completed_at" json:"last_completed_at"`
	Name              string         `gorm:"column:name;not null" json:"name"`
	Tags              []string       `gorm:"column:tags;serializer:json;not null" json:"tags"`
	TriggerIds        []string       `gorm:"column:trigger_ids;serializer:json;not null" json:"trigger_ids"`
	AlertedAt         *DateTime      `gorm:"column:alerted_at" json:"alerted_at"`
	CreatedByUser     *User          `gorm:"foreignKey:CreatedBy"`
	UpdatedByUser     *User          `gorm:"foreignKey:UpdatedBy"`
	Project           *Project
}

func (Monitor) TableName() string {
	return "monitors"
}

func (m *Monitor) BeforeCreate(tx *gorm.DB) error {
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

func (*Monitor) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type SlackIntegration struct {
	ID                   string   `gorm:"column:id;primaryKey" json:"id"`
	ProjectID            string   `gorm:"column:project_id;uniqueIndex;not null" json:"project_id"`
	TeamID               string   `gorm:"column:team_id;index:slack_integrations_team_id_idx;not null" json:"team_id"`
	TeamName             string   `gorm:"column:team_name;not null" json:"team_name"`
	BotToken             string   `gorm:"column:bot_token;not null" json:"bot_token"`
	BotUserID            string   `gorm:"column:bot_user_id;not null" json:"bot_user_id"`
	InstallerUserID      *string  `gorm:"column:installer_user_id" json:"installer_user_id"`
	InstallerSlackUserID *string  `gorm:"column:installer_slack_user_id" json:"installer_slack_user_id"`
	CreatedAt            DateTime `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt            DateTime `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	Project              *Project
}

func (SlackIntegration) TableName() string {
	return "slack_integrations"
}

func (m *SlackIntegration) BeforeCreate(tx *gorm.DB) error {
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

func (*SlackIntegration) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type PendingDeletion struct {
	ID        string   `gorm:"column:id;primaryKey;index:pending_deletions_project_id_object_is_deleted_object_id_id_idx" json:"id"`
	ProjectID string   `gorm:"column:project_id;index:pending_deletions_project_id_object_is_deleted_object_id_id_idx;not null" json:"project_id"`
	Object    string   `gorm:"column:object;index:pending_deletions_project_id_object_is_deleted_object_id_id_idx;index:pending_deletions_object_id_object_idx;not null" json:"object"`
	ObjectID  string   `gorm:"column:object_id;index:pending_deletions_project_id_object_is_deleted_object_id_id_idx;index:pending_deletions_object_id_object_idx;not null" json:"object_id"`
	IsDeleted bool     `gorm:"column:is_deleted;index:pending_deletions_project_id_object_is_deleted_object_id_id_idx;default:false;not null" json:"is_deleted"`
	CreatedAt DateTime `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt DateTime `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	Project   *Project
}

func (PendingDeletion) TableName() string {
	return "pending_deletions"
}

func (m *PendingDeletion) BeforeCreate(tx *gorm.DB) error {
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

func (*PendingDeletion) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type Survey struct {
	ID         string         `gorm:"column:id;primaryKey" json:"id"`
	CreatedAt  DateTime       `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	SurveyName string         `gorm:"column:survey_name;not null" json:"survey_name"`
	Response   datatypes.JSON `gorm:"column:response;not null" json:"response"`
	UserID     *string        `gorm:"column:user_id" json:"user_id"`
	UserEmail  *string        `gorm:"column:user_email" json:"user_email"`
	OrgID      *string        `gorm:"column:org_id" json:"org_id"`
	Org        *Organization  `gorm:"foreignKey:OrgID"`
	User       *User
}

func (Survey) TableName() string {
	return "surveys"
}

func (m *Survey) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type CloudSpendAlert struct {
	ID          string        `gorm:"column:id;primaryKey" json:"id"`
	OrgID       string        `gorm:"column:org_id;index:cloud_spend_alerts_org_id_idx;not null" json:"org_id"`
	Title       string        `gorm:"column:title;not null" json:"title"`
	Threshold   float64       `gorm:"column:threshold;not null" json:"threshold"`
	TriggeredAt *DateTime     `gorm:"column:triggered_at" json:"triggered_at"`
	CreatedAt   DateTime      `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt   DateTime      `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	Org         *Organization `gorm:"foreignKey:OrgID"`
}

func (CloudSpendAlert) TableName() string {
	return "cloud_spend_alerts"
}

func (m *CloudSpendAlert) BeforeCreate(tx *gorm.DB) error {
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

func (*CloudSpendAlert) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type TopicClusteringRun struct {
	ID              string         `gorm:"column:id;primaryKey;index:topic_clustering_runs_serving_map_idx" json:"id"`
	ProjectID       string         `gorm:"column:project_id;index:topic_clustering_runs_serving_map_idx;index:topic_clustering_runs_rule_serving_map_idx;index:topic_clustering_runs_project_id_batch_action_id_idx;not null" json:"project_id"`
	FacetID         string         `gorm:"column:facet_id;index:topic_clustering_runs_serving_map_idx;index:topic_clustering_runs_rule_serving_map_idx;not null" json:"facet_id"`
	FacetVersion    int            `gorm:"column:facet_version;index:topic_clustering_runs_serving_map_idx;not null" json:"facet_version"`
	Status          *string        `gorm:"column:status;index:topic_clustering_runs_serving_map_idx;index:topic_clustering_runs_rule_serving_map_idx;default:'pending';not null" json:"status"`
	Config          datatypes.JSON `gorm:"column:config;default:'{}';not null" json:"config"`
	Error           *string        `gorm:"column:error" json:"error"`
	CreatedAt       DateTime       `gorm:"column:created_at;index:topic_clustering_runs_serving_map_idx;type:timestamptz(3);autoCreateTime:false;not null" json:"created_at"`
	StartedAt       *DateTime      `gorm:"column:started_at;type:timestamptz(3)" json:"started_at"`
	FinishedAt      *DateTime      `gorm:"column:finished_at;type:timestamptz(3)" json:"finished_at"`
	TopicVersionIds []string       `gorm:"column:topic_version_ids;serializer:json;not null" json:"topic_version_ids"`
	RuleID          *string        `gorm:"column:rule_id;index:topic_clustering_runs_rule_serving_map_idx" json:"rule_id"`
	BatchActionID   *string        `gorm:"column:batch_action_id;index:topic_clustering_runs_project_id_batch_action_id_idx" json:"batch_action_id"`
	Project         *Project
	Facet           *EvaluatorVersion `gorm:"foreignKey:FacetID,FacetVersion;references:EvaluatorID,Version"`
}

func (TopicClusteringRun) TableName() string {
	return "topic_clustering_runs"
}

func (m *TopicClusteringRun) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
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
