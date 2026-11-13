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

type LiteLLMBudgetTable struct {
	BudgetID                     string                               `gorm:"column:budget_id;primaryKey;type:char(36)" json:"budget_id"`
	MaxBudget                    *float64                             `gorm:"column:max_budget" json:"max_budget"`
	SoftBudget                   *float64                             `gorm:"column:soft_budget" json:"soft_budget"`
	MaxParallelRequests          *int                                 `gorm:"column:max_parallel_requests" json:"max_parallel_requests"`
	TpmLimit                     *int64                               `gorm:"column:tpm_limit" json:"tpm_limit"`
	RpmLimit                     *int64                               `gorm:"column:rpm_limit" json:"rpm_limit"`
	TpdLimit                     *int64                               `gorm:"column:tpd_limit" json:"tpd_limit"`
	ModelMaxBudget               datatypes.JSON                       `gorm:"column:model_max_budget" json:"model_max_budget"`
	ModelTpmLimit                datatypes.JSON                       `gorm:"column:model_tpm_limit" json:"model_tpm_limit"`
	ModelRpmLimit                datatypes.JSON                       `gorm:"column:model_rpm_limit" json:"model_rpm_limit"`
	BudgetDuration               *string                              `gorm:"column:budget_duration" json:"budget_duration"`
	BudgetResetAt                *DateTime                            `gorm:"column:budget_reset_at" json:"budget_reset_at"`
	AllowedModels                []string                             `gorm:"column:allowed_models;serializer:json;not null" json:"allowed_models"`
	TempBudgetIncrease           *float64                             `gorm:"column:temp_budget_increase" json:"temp_budget_increase"`
	TempBudgetExpiry             *DateTime                            `gorm:"column:temp_budget_expiry" json:"temp_budget_expiry"`
	CreatedAt                    DateTime                             `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	CreatedBy                    string                               `gorm:"column:created_by;not null" json:"created_by"`
	UpdatedAt                    DateTime                             `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	UpdatedBy                    string                               `gorm:"column:updated_by;not null" json:"updated_by"`
	CustomerOrganizationDefaults []LiteLLMCustomerOrganizationTable   `gorm:"foreignKey:DefaultBudgetID;references:BudgetID"`
	CustomerOrganizations        []LiteLLMCustomerOrganizationTable   `gorm:"foreignKey:BudgetID;references:BudgetID"`
	CustomerMemberTemplates      []LiteLLMCustomerMemberTable         `gorm:"foreignKey:BudgetTemplateID;references:BudgetID;constraint:OnDelete:RESTRICT"`
	CustomerTeams                []LiteLLMCustomerTeamTable           `gorm:"foreignKey:BudgetID;references:BudgetID"`
	CustomerMembers              []LiteLLMCustomerMemberTable         `gorm:"foreignKey:BudgetID;references:BudgetID;constraint:OnDelete:RESTRICT"`
	Organization                 []LiteLLMOrganizationTable           `gorm:"foreignKey:BudgetID;references:BudgetID"`
	Projects                     []LiteLLMProjectTable                `gorm:"foreignKey:BudgetID;references:BudgetID"`
	Keys                         []LiteLLMVerificationToken           `gorm:"foreignKey:BudgetID;references:BudgetID"`
	EndUsers                     []LiteLLMEndUserTable                `gorm:"foreignKey:BudgetID;references:BudgetID"`
	Tags                         []LiteLLMTagTable                    `gorm:"foreignKey:BudgetID;references:BudgetID"`
	ModelAccessGroups            []LiteLLMModelAccessGroupBudgetTable `gorm:"foreignKey:BudgetID;references:BudgetID"`
	TeamMembership               []LiteLLMTeamMembership              `gorm:"foreignKey:BudgetID;references:BudgetID"`
	OrganizationMembership       []LiteLLMOrganizationMembership      `gorm:"foreignKey:BudgetID;references:BudgetID"`
}

func (LiteLLMBudgetTable) TableName() string {
	return "LiteLLM_BudgetTable"
}

func (m *LiteLLMBudgetTable) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.BudgetID == "" {
		m.BudgetID = uuid.NewString()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*LiteLLMBudgetTable) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LiteLLMCustomerOrganizationTable struct {
	Spend           float64                    `gorm:"column:spend;default:0;not null" json:"spend"`
	BudgetResetAt   *DateTime                  `gorm:"column:budget_reset_at" json:"budget_reset_at"`
	BudgetIds       []string                   `gorm:"column:budget_ids;serializer:json;not null" json:"budget_ids"`
	BudgetNames     datatypes.JSON             `gorm:"column:budget_names;default:'{}';not null" json:"budget_names"`
	BudgetID        *string                    `gorm:"column:budget_id" json:"budget_id"`
	CustomerOrgID   string                     `gorm:"column:customer_org_id;primaryKey;type:char(36)" json:"customer_org_id"`
	Alias           *string                    `gorm:"column:alias" json:"alias"`
	DefaultBudgetID *string                    `gorm:"column:default_budget_id" json:"default_budget_id"`
	CreatedAt       DateTime                   `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	CreatedBy       string                     `gorm:"column:created_by;not null" json:"created_by"`
	UpdatedAt       DateTime                   `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	UpdatedBy       string                     `gorm:"column:updated_by;not null" json:"updated_by"`
	Budget          *LiteLLMBudgetTable        `gorm:"foreignKey:BudgetID;references:BudgetID"`
	DefaultBudget   *LiteLLMBudgetTable        `gorm:"foreignKey:DefaultBudgetID;references:BudgetID"`
	Teams           []LiteLLMCustomerTeamTable `gorm:"foreignKey:ParentOrgID;references:CustomerOrgID"`
}

func (LiteLLMCustomerOrganizationTable) TableName() string {
	return "LiteLLM_CustomerOrganizationTable"
}

func (m *LiteLLMCustomerOrganizationTable) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CustomerOrgID == "" {
		m.CustomerOrgID = uuid.NewString()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*LiteLLMCustomerOrganizationTable) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LiteLLMCustomerTeamTable struct {
	Spend          float64                           `gorm:"column:spend;default:0;not null" json:"spend"`
	BudgetResetAt  *DateTime                         `gorm:"column:budget_reset_at" json:"budget_reset_at"`
	CustomerTeamID string                            `gorm:"column:customer_team_id;primaryKey;type:char(36)" json:"customer_team_id"`
	Alias          *string                           `gorm:"column:alias" json:"alias"`
	ParentOrgID    string                            `gorm:"column:parent_org_id;index:LiteLLM_CustomerTeamTable_parent_org_id_idx;not null" json:"parent_org_id"`
	BudgetID       *string                           `gorm:"column:budget_id" json:"budget_id"`
	CreatedAt      DateTime                          `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	CreatedBy      string                            `gorm:"column:created_by;not null" json:"created_by"`
	UpdatedAt      DateTime                          `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	UpdatedBy      string                            `gorm:"column:updated_by;not null" json:"updated_by"`
	ParentOrg      *LiteLLMCustomerOrganizationTable `gorm:"foreignKey:ParentOrgID;references:CustomerOrgID"`
	Budget         *LiteLLMBudgetTable               `gorm:"foreignKey:BudgetID;references:BudgetID"`
	Members        []LiteLLMCustomerMemberTable      `gorm:"foreignKey:ParentTeamID;references:CustomerTeamID"`
}

func (LiteLLMCustomerTeamTable) TableName() string {
	return "LiteLLM_CustomerTeamTable"
}

func (m *LiteLLMCustomerTeamTable) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CustomerTeamID == "" {
		m.CustomerTeamID = uuid.NewString()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*LiteLLMCustomerTeamTable) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LiteLLMCustomerMemberTable struct {
	Spend                   float64                   `gorm:"column:spend;default:0;not null" json:"spend"`
	BudgetResetAt           *DateTime                 `gorm:"column:budget_reset_at" json:"budget_reset_at"`
	CustomerMemberID        string                    `gorm:"column:customer_member_id;primaryKey;type:char(36)" json:"customer_member_id"`
	Alias                   *string                   `gorm:"column:alias" json:"alias"`
	ParentTeamID            string                    `gorm:"column:parent_team_id;index:LiteLLM_CustomerMemberTable_parent_team_id_idx;not null" json:"parent_team_id"`
	BudgetID                *string                   `gorm:"column:budget_id" json:"budget_id"`
	BudgetTemplateID        *string                   `gorm:"column:budget_template_id;index:LiteLLM_CustomerMemberTable_budget_template_id_idx" json:"budget_template_id"`
	BudgetOverrideExpiresAt *DateTime                 `gorm:"column:budget_override_expires_at" json:"budget_override_expires_at"`
	CreatedAt               DateTime                  `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	CreatedBy               string                    `gorm:"column:created_by;not null" json:"created_by"`
	UpdatedAt               DateTime                  `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	UpdatedBy               string                    `gorm:"column:updated_by;not null" json:"updated_by"`
	ParentTeam              *LiteLLMCustomerTeamTable `gorm:"foreignKey:ParentTeamID;references:CustomerTeamID"`
	Budget                  *LiteLLMBudgetTable       `gorm:"foreignKey:BudgetID;references:BudgetID"`
	BudgetTemplate          *LiteLLMBudgetTable       `gorm:"foreignKey:BudgetTemplateID;references:BudgetID"`
}

func (LiteLLMCustomerMemberTable) TableName() string {
	return "LiteLLM_CustomerMemberTable"
}

func (m *LiteLLMCustomerMemberTable) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CustomerMemberID == "" {
		m.CustomerMemberID = uuid.NewString()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*LiteLLMCustomerMemberTable) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LiteLLMCredentialsTable struct {
	CredentialID     string         `gorm:"column:credential_id;primaryKey;type:char(36)" json:"credential_id"`
	CredentialName   string         `gorm:"column:credential_name;uniqueIndex;not null" json:"credential_name"`
	DisplayName      *string        `gorm:"column:display_name" json:"display_name"`
	CredentialValues datatypes.JSON `gorm:"column:credential_values;not null" json:"credential_values"`
	CredentialInfo   datatypes.JSON `gorm:"column:credential_info" json:"credential_info"`
	CreatedAt        DateTime       `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	CreatedBy        string         `gorm:"column:created_by;not null" json:"created_by"`
	UpdatedAt        DateTime       `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	UpdatedBy        string         `gorm:"column:updated_by;not null" json:"updated_by"`
}

func (LiteLLMCredentialsTable) TableName() string {
	return "LiteLLM_CredentialsTable"
}

func (m *LiteLLMCredentialsTable) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CredentialID == "" {
		m.CredentialID = uuid.NewString()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*LiteLLMCredentialsTable) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LiteLLMProxyModelTable struct {
	ModelID       string         `gorm:"column:model_id;primaryKey;type:char(36)" json:"model_id"`
	ModelName     string         `gorm:"column:model_name;not null" json:"model_name"`
	LitellmParams datatypes.JSON `gorm:"column:litellm_params;not null" json:"litellm_params"`
	ModelInfo     datatypes.JSON `gorm:"column:model_info" json:"model_info"`
	Blocked       bool           `gorm:"column:blocked;default:false;not null" json:"blocked"`
	CreatedAt     DateTime       `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	CreatedBy     string         `gorm:"column:created_by;not null" json:"created_by"`
	UpdatedAt     DateTime       `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	UpdatedBy     string         `gorm:"column:updated_by;not null" json:"updated_by"`
}

func (LiteLLMProxyModelTable) TableName() string {
	return "LiteLLM_ProxyModelTable"
}

func (m *LiteLLMProxyModelTable) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ModelID == "" {
		m.ModelID = uuid.NewString()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*LiteLLMProxyModelTable) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LiteLLMAgentsTable struct {
	AgentID            string                        `gorm:"column:agent_id;primaryKey;type:char(36)" json:"agent_id"`
	AgentName          string                        `gorm:"column:agent_name;uniqueIndex;not null" json:"agent_name"`
	LitellmParams      datatypes.JSON                `gorm:"column:litellm_params" json:"litellm_params"`
	AgentCardParams    datatypes.JSON                `gorm:"column:agent_card_params;not null" json:"agent_card_params"`
	StaticHeaders      datatypes.JSON                `gorm:"column:static_headers;default:'{}'" json:"static_headers"`
	ExtraHeaders       []string                      `gorm:"column:extra_headers;serializer:json;not null" json:"extra_headers"`
	KillSwitch         datatypes.JSON                `gorm:"column:kill_switch" json:"kill_switch"`
	AgentAccessGroups  []string                      `gorm:"column:agent_access_groups;serializer:json;not null" json:"agent_access_groups"`
	AccessGroupIds     []string                      `gorm:"column:access_group_ids;serializer:json;not null" json:"access_group_ids"`
	ObjectPermissionID *string                       `gorm:"column:object_permission_id" json:"object_permission_id"`
	Spend              float64                       `gorm:"column:spend;default:0;not null" json:"spend"`
	IdentityManaged    bool                          `gorm:"column:identity_managed;default:false;not null" json:"identity_managed"`
	Enabled            *bool                         `gorm:"column:enabled;default:true;not null" json:"enabled"`
	ExecutionMode      *string                       `gorm:"column:execution_mode;default:'autonomous';not null" json:"execution_mode"`
	TpmLimit           *int                          `gorm:"column:tpm_limit" json:"tpm_limit"`
	RpmLimit           *int                          `gorm:"column:rpm_limit" json:"rpm_limit"`
	SessionTpmLimit    *int                          `gorm:"column:session_tpm_limit" json:"session_tpm_limit"`
	SessionRpmLimit    *int                          `gorm:"column:session_rpm_limit" json:"session_rpm_limit"`
	CreatedAt          DateTime                      `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	CreatedBy          string                        `gorm:"column:created_by;not null" json:"created_by"`
	UpdatedAt          DateTime                      `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	UpdatedBy          string                        `gorm:"column:updated_by;not null" json:"updated_by"`
	ObjectPermission   *LiteLLMObjectPermissionTable `gorm:"foreignKey:ObjectPermissionID;references:ObjectPermissionID"`
	RetiredIdentities  []LiteLLMRetiredAgentIdentity `gorm:"foreignKey:AgentID;references:AgentID;constraint:OnDelete:SET NULL"`
	Identity           *LiteLLMAgentIdentity         `gorm:"foreignKey:AgentID;references:AgentID;constraint:OnDelete:CASCADE"`
}

func (LiteLLMAgentsTable) TableName() string {
	return "LiteLLM_AgentsTable"
}

func (m *LiteLLMAgentsTable) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.AgentID == "" {
		m.AgentID = uuid.NewString()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*LiteLLMAgentsTable) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LiteLLMAgentIdentity struct {
	AgentID             string              `gorm:"column:agent_id;primaryKey" json:"agent_id"`
	Active              *bool               `gorm:"column:active;default:true;not null" json:"active"`
	Provider            string              `gorm:"column:provider;uniqueIndex:LiteLLM_AgentIdentity_provider_tenant_id_client_id_key;not null" json:"provider"`
	Issuer              string              `gorm:"column:issuer;uniqueIndex:LiteLLM_AgentIdentity_issuer_service_principal_id_key;not null" json:"issuer"`
	TenantID            string              `gorm:"column:tenant_id;uniqueIndex:LiteLLM_AgentIdentity_provider_tenant_id_client_id_key;not null" json:"tenant_id"`
	ClientID            string              `gorm:"column:client_id;uniqueIndex:LiteLLM_AgentIdentity_provider_tenant_id_client_id_key;not null" json:"client_id"`
	ServicePrincipalID  *string             `gorm:"column:service_principal_id;uniqueIndex:LiteLLM_AgentIdentity_issuer_service_principal_id_key" json:"service_principal_id"`
	RequiredRoles       []string            `gorm:"column:required_roles;serializer:json;not null" json:"required_roles"`
	RequiredScopes      []string            `gorm:"column:required_scopes;serializer:json;not null" json:"required_scopes"`
	Revision            string              `gorm:"column:revision;not null" json:"revision"`
	LastAuthenticatedAt *DateTime           `gorm:"column:last_authenticated_at" json:"last_authenticated_at"`
	Agent               *LiteLLMAgentsTable `gorm:"foreignKey:AgentID;references:AgentID"`
}

func (LiteLLMAgentIdentity) TableName() string {
	return "LiteLLM_AgentIdentity"
}

func (m *LiteLLMAgentIdentity) BeforeCreate(_ *gorm.DB) error {
	if m.Revision == "" {
		m.Revision = uuid.NewString()
	}
	return nil
}

type LiteLLMRetiredAgentIdentity struct {
	BindingID string              `gorm:"column:binding_id;primaryKey;type:char(36)" json:"binding_id"`
	AgentID   *string             `gorm:"column:agent_id" json:"agent_id"`
	Provider  string              `gorm:"column:provider;uniqueIndex:LiteLLM_RetiredAgentIdentity_provider_tenant_id_client_id_key;not null" json:"provider"`
	Issuer    string              `gorm:"column:issuer;not null" json:"issuer"`
	TenantID  string              `gorm:"column:tenant_id;uniqueIndex:LiteLLM_RetiredAgentIdentity_provider_tenant_id_client_id_key;not null" json:"tenant_id"`
	ClientID  string              `gorm:"column:client_id;uniqueIndex:LiteLLM_RetiredAgentIdentity_provider_tenant_id_client_id_key;not null" json:"client_id"`
	Agent     *LiteLLMAgentsTable `gorm:"foreignKey:AgentID;references:AgentID"`
}

func (LiteLLMRetiredAgentIdentity) TableName() string {
	return "LiteLLM_RetiredAgentIdentity"
}

func (m *LiteLLMRetiredAgentIdentity) BeforeCreate(_ *gorm.DB) error {
	if m.BindingID == "" {
		m.BindingID = uuid.NewString()
	}
	return nil
}

type LiteLLMRetiredAgent struct {
	OriginalAgentID string   `gorm:"column:original_agent_id;primaryKey" json:"original_agent_id"`
	RetiredAt       DateTime `gorm:"column:retired_at;not null" json:"retired_at"`
}

func (LiteLLMRetiredAgent) TableName() string {
	return "LiteLLM_RetiredAgent"
}

func (m *LiteLLMRetiredAgent) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.RetiredAt.IsZero() {
		m.RetiredAt = DateTime{Time: now}
	}
	return nil
}

type LiteLLMVerifiedSubject struct {
	SubjectID   string            `gorm:"column:subject_id;primaryKey;type:char(36)" json:"subject_id"`
	Issuer      string            `gorm:"column:issuer;uniqueIndex:LiteLLM_VerifiedSubject_issuer_tenant_id_oid_key;not null" json:"issuer"`
	TenantID    string            `gorm:"column:tenant_id;uniqueIndex:LiteLLM_VerifiedSubject_issuer_tenant_id_oid_key;not null" json:"tenant_id"`
	Oid         string            `gorm:"column:oid;uniqueIndex:LiteLLM_VerifiedSubject_issuer_tenant_id_oid_key;not null" json:"oid"`
	Kind        *string           `gorm:"column:kind;default:'human';not null" json:"kind"`
	UserID      *string           `gorm:"column:user_id;index:LiteLLM_VerifiedSubject_user_id_idx" json:"user_id"`
	VerifiedVia *string           `gorm:"column:verified_via;default:'sso_interactive';not null" json:"verified_via"`
	VerifiedAt  DateTime          `gorm:"column:verified_at;not null" json:"verified_at"`
	User        *LiteLLMUserTable `gorm:"foreignKey:UserID;references:UserID"`
}

func (LiteLLMVerifiedSubject) TableName() string {
	return "LiteLLM_VerifiedSubject"
}

func (m *LiteLLMVerifiedSubject) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.SubjectID == "" {
		m.SubjectID = uuid.NewString()
	}
	if m.VerifiedAt.IsZero() {
		m.VerifiedAt = DateTime{Time: now}
	}
	return nil
}

type LiteLLMOrganizationTable struct {
	OrganizationID     string                          `gorm:"column:organization_id;primaryKey;type:char(36)" json:"organization_id"`
	OrganizationAlias  string                          `gorm:"column:organization_alias;not null" json:"organization_alias"`
	BudgetID           string                          `gorm:"column:budget_id;not null" json:"budget_id"`
	Metadata           datatypes.JSON                  `gorm:"column:metadata;default:'{}';not null" json:"metadata"`
	Models             []string                        `gorm:"column:models;serializer:json;not null" json:"models"`
	Spend              float64                         `gorm:"column:spend;default:0;not null" json:"spend"`
	ModelSpend         datatypes.JSON                  `gorm:"column:model_spend;default:'{}';not null" json:"model_spend"`
	ObjectPermissionID *string                         `gorm:"column:object_permission_id" json:"object_permission_id"`
	CreatedAt          DateTime                        `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	CreatedBy          string                          `gorm:"column:created_by;not null" json:"created_by"`
	UpdatedAt          DateTime                        `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	UpdatedBy          string                          `gorm:"column:updated_by;not null" json:"updated_by"`
	LitellmBudgetTable *LiteLLMBudgetTable             `gorm:"foreignKey:BudgetID;references:BudgetID"`
	ObjectPermission   *LiteLLMObjectPermissionTable   `gorm:"foreignKey:ObjectPermissionID;references:ObjectPermissionID"`
	Teams              []LiteLLMTeamTable              `gorm:"foreignKey:OrganizationID;references:OrganizationID"`
	Users              []LiteLLMUserTable              `gorm:"foreignKey:OrganizationID;references:OrganizationID"`
	Keys               []LiteLLMVerificationToken      `gorm:"foreignKey:OrganizationID;references:OrganizationID"`
	Members            []LiteLLMOrganizationMembership `gorm:"foreignKey:OrganizationID;references:OrganizationID"`
}

func (LiteLLMOrganizationTable) TableName() string {
	return "LiteLLM_OrganizationTable"
}

func (m *LiteLLMOrganizationTable) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.OrganizationID == "" {
		m.OrganizationID = uuid.NewString()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*LiteLLMOrganizationTable) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LiteLLMModelTable struct {
	ID           int               `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	ModelAliases datatypes.JSON    `gorm:"column:aliases" json:"aliases"`
	CreatedAt    DateTime          `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	CreatedBy    string            `gorm:"column:created_by;not null" json:"created_by"`
	UpdatedAt    DateTime          `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	UpdatedBy    string            `gorm:"column:updated_by;not null" json:"updated_by"`
	Team         *LiteLLMTeamTable `gorm:"foreignKey:ModelID"`
}

func (LiteLLMModelTable) TableName() string {
	return "LiteLLM_ModelTable"
}

func (m *LiteLLMModelTable) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*LiteLLMModelTable) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LiteLLMTeamTable struct {
	TeamID                   string                        `gorm:"column:team_id;primaryKey;type:char(36)" json:"team_id"`
	TeamAlias                *string                       `gorm:"column:team_alias;index:LiteLLM_TeamTable_team_alias_idx" json:"team_alias"`
	OrganizationID           *string                       `gorm:"column:organization_id;index:LiteLLM_TeamTable_organization_id_idx" json:"organization_id"`
	ObjectPermissionID       *string                       `gorm:"column:object_permission_id" json:"object_permission_id"`
	Admins                   []string                      `gorm:"column:admins;serializer:json;not null" json:"admins"`
	Members                  []string                      `gorm:"column:members;serializer:json;not null" json:"members"`
	MembersWithRoles         datatypes.JSON                `gorm:"column:members_with_roles;default:'{}';not null" json:"members_with_roles"`
	Metadata                 datatypes.JSON                `gorm:"column:metadata;default:'{}';not null" json:"metadata"`
	MaxBudget                *float64                      `gorm:"column:max_budget" json:"max_budget"`
	SoftBudget               *float64                      `gorm:"column:soft_budget" json:"soft_budget"`
	Spend                    float64                       `gorm:"column:spend;default:0;not null" json:"spend"`
	Models                   []string                      `gorm:"column:models;serializer:json;not null" json:"models"`
	MaxParallelRequests      *int                          `gorm:"column:max_parallel_requests" json:"max_parallel_requests"`
	TpmLimit                 *int64                        `gorm:"column:tpm_limit" json:"tpm_limit"`
	RpmLimit                 *int64                        `gorm:"column:rpm_limit" json:"rpm_limit"`
	TpdLimit                 *int64                        `gorm:"column:tpd_limit" json:"tpd_limit"`
	BudgetDuration           *string                       `gorm:"column:budget_duration" json:"budget_duration"`
	BudgetResetAt            *DateTime                     `gorm:"column:budget_reset_at" json:"budget_reset_at"`
	Blocked                  bool                          `gorm:"column:blocked;default:false;not null" json:"blocked"`
	CreatedAt                DateTime                      `gorm:"column:created_at;index:LiteLLM_TeamTable_created_at_idx;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt                DateTime                      `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	ModelSpend               datatypes.JSON                `gorm:"column:model_spend;default:'{}';not null" json:"model_spend"`
	ModelMaxBudget           datatypes.JSON                `gorm:"column:model_max_budget;default:'{}';not null" json:"model_max_budget"`
	RouterSettings           datatypes.JSON                `gorm:"column:router_settings;default:'{}'" json:"router_settings"`
	TeamMemberPermissions    []string                      `gorm:"column:team_member_permissions;serializer:json;not null" json:"team_member_permissions"`
	AccessGroupIds           []string                      `gorm:"column:access_group_ids;serializer:json;not null" json:"access_group_ids"`
	Policies                 []string                      `gorm:"column:policies;serializer:json;not null" json:"policies"`
	DefaultTeamMemberModels  []string                      `gorm:"column:default_team_member_models;serializer:json;not null" json:"default_team_member_models"`
	BudgetLimits             datatypes.JSON                `gorm:"column:budget_limits" json:"budget_limits"`
	ModelID                  *int                          `gorm:"column:model_id;uniqueIndex" json:"model_id"`
	AllowTeamGuardrailConfig bool                          `gorm:"column:allow_team_guardrail_config;default:false;not null" json:"allow_team_guardrail_config"`
	LitellmOrganizationTable *LiteLLMOrganizationTable     `gorm:"foreignKey:OrganizationID;references:OrganizationID"`
	LitellmModelTable        *LiteLLMModelTable            `gorm:"foreignKey:ModelID"`
	ObjectPermission         *LiteLLMObjectPermissionTable `gorm:"foreignKey:ObjectPermissionID;references:ObjectPermissionID"`
	Projects                 []LiteLLMProjectTable         `gorm:"foreignKey:TeamID;references:TeamID"`
}

func (LiteLLMTeamTable) TableName() string {
	return "LiteLLM_TeamTable"
}

func (m *LiteLLMTeamTable) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.TeamID == "" {
		m.TeamID = uuid.NewString()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*LiteLLMTeamTable) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LiteLLMProjectTable struct {
	ProjectID          string                        `gorm:"column:project_id;primaryKey;type:char(36)" json:"project_id"`
	ProjectAlias       *string                       `gorm:"column:project_alias" json:"project_alias"`
	Description        *string                       `gorm:"column:description" json:"description"`
	TeamID             *string                       `gorm:"column:team_id" json:"team_id"`
	BudgetID           *string                       `gorm:"column:budget_id" json:"budget_id"`
	Metadata           datatypes.JSON                `gorm:"column:metadata;default:'{}';not null" json:"metadata"`
	Models             []string                      `gorm:"column:models;serializer:json;not null" json:"models"`
	Spend              float64                       `gorm:"column:spend;default:0;not null" json:"spend"`
	ModelSpend         datatypes.JSON                `gorm:"column:model_spend;default:'{}';not null" json:"model_spend"`
	ModelRpmLimit      datatypes.JSON                `gorm:"column:model_rpm_limit;default:'{}';not null" json:"model_rpm_limit"`
	ModelTpmLimit      datatypes.JSON                `gorm:"column:model_tpm_limit;default:'{}';not null" json:"model_tpm_limit"`
	Blocked            bool                          `gorm:"column:blocked;default:false;not null" json:"blocked"`
	ObjectPermissionID *string                       `gorm:"column:object_permission_id" json:"object_permission_id"`
	CreatedAt          DateTime                      `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	CreatedBy          string                        `gorm:"column:created_by;not null" json:"created_by"`
	UpdatedAt          DateTime                      `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	UpdatedBy          string                        `gorm:"column:updated_by;not null" json:"updated_by"`
	LitellmTeamTable   *LiteLLMTeamTable             `gorm:"foreignKey:TeamID;references:TeamID"`
	LitellmBudgetTable *LiteLLMBudgetTable           `gorm:"foreignKey:BudgetID;references:BudgetID"`
	ObjectPermission   *LiteLLMObjectPermissionTable `gorm:"foreignKey:ObjectPermissionID;references:ObjectPermissionID"`
	Keys               []LiteLLMVerificationToken    `gorm:"foreignKey:ProjectID;references:ProjectID"`
}

func (LiteLLMProjectTable) TableName() string {
	return "LiteLLM_ProjectTable"
}

func (m *LiteLLMProjectTable) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ProjectID == "" {
		m.ProjectID = uuid.NewString()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*LiteLLMProjectTable) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LiteLLMDeletedTeamTable struct {
	ID                       string         `gorm:"column:id;primaryKey;type:char(36)" json:"id"`
	TeamID                   string         `gorm:"column:team_id;index:LiteLLM_DeletedTeamTable_team_id_idx;not null" json:"team_id"`
	TeamAlias                *string        `gorm:"column:team_alias;index:LiteLLM_DeletedTeamTable_team_alias_idx" json:"team_alias"`
	OrganizationID           *string        `gorm:"column:organization_id;index:LiteLLM_DeletedTeamTable_organization_id_idx" json:"organization_id"`
	ObjectPermissionID       *string        `gorm:"column:object_permission_id" json:"object_permission_id"`
	Admins                   []string       `gorm:"column:admins;serializer:json;not null" json:"admins"`
	Members                  []string       `gorm:"column:members;serializer:json;not null" json:"members"`
	MembersWithRoles         datatypes.JSON `gorm:"column:members_with_roles;default:'{}';not null" json:"members_with_roles"`
	Metadata                 datatypes.JSON `gorm:"column:metadata;default:'{}';not null" json:"metadata"`
	MaxBudget                *float64       `gorm:"column:max_budget" json:"max_budget"`
	SoftBudget               *float64       `gorm:"column:soft_budget" json:"soft_budget"`
	Spend                    float64        `gorm:"column:spend;default:0;not null" json:"spend"`
	Models                   []string       `gorm:"column:models;serializer:json;not null" json:"models"`
	MaxParallelRequests      *int           `gorm:"column:max_parallel_requests" json:"max_parallel_requests"`
	TpmLimit                 *int64         `gorm:"column:tpm_limit" json:"tpm_limit"`
	RpmLimit                 *int64         `gorm:"column:rpm_limit" json:"rpm_limit"`
	TpdLimit                 *int64         `gorm:"column:tpd_limit" json:"tpd_limit"`
	BudgetDuration           *string        `gorm:"column:budget_duration" json:"budget_duration"`
	BudgetResetAt            *DateTime      `gorm:"column:budget_reset_at" json:"budget_reset_at"`
	Blocked                  bool           `gorm:"column:blocked;default:false;not null" json:"blocked"`
	ModelSpend               datatypes.JSON `gorm:"column:model_spend;default:'{}';not null" json:"model_spend"`
	ModelMaxBudget           datatypes.JSON `gorm:"column:model_max_budget;default:'{}';not null" json:"model_max_budget"`
	RouterSettings           datatypes.JSON `gorm:"column:router_settings;default:'{}'" json:"router_settings"`
	TeamMemberPermissions    []string       `gorm:"column:team_member_permissions;serializer:json;not null" json:"team_member_permissions"`
	AccessGroupIds           []string       `gorm:"column:access_group_ids;serializer:json;not null" json:"access_group_ids"`
	Policies                 []string       `gorm:"column:policies;serializer:json;not null" json:"policies"`
	ModelID                  *int           `gorm:"column:model_id" json:"model_id"`
	AllowTeamGuardrailConfig bool           `gorm:"column:allow_team_guardrail_config;default:false;not null" json:"allow_team_guardrail_config"`
	CreatedAt                *DateTime      `gorm:"column:created_at;index:LiteLLM_DeletedTeamTable_created_at_idx;autoCreateTime:false" json:"created_at"`
	UpdatedAt                *DateTime      `gorm:"column:updated_at;autoUpdateTime:false" json:"updated_at"`
	DeletedAt                DateTime       `gorm:"column:deleted_at;index:LiteLLM_DeletedTeamTable_deleted_at_idx;not null" json:"deleted_at"`
	DeletedBy                *string        `gorm:"column:deleted_by" json:"deleted_by"`
	DeletedByAPIKey          *string        `gorm:"column:deleted_by_api_key" json:"deleted_by_api_key"`
	LitellmChangedBy         *string        `gorm:"column:litellm_changed_by" json:"litellm_changed_by"`
}

func (LiteLLMDeletedTeamTable) TableName() string {
	return "LiteLLM_DeletedTeamTable"
}

func (m *LiteLLMDeletedTeamTable) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = uuid.NewString()
	}
	if m.DeletedAt.IsZero() {
		m.DeletedAt = DateTime{Time: now}
	}
	return nil
}

type LiteLLMUserTable struct {
	UserID                   string                          `gorm:"column:user_id;primaryKey" json:"user_id"`
	UserAlias                *string                         `gorm:"column:user_alias" json:"user_alias"`
	TeamID                   *string                         `gorm:"column:team_id" json:"team_id"`
	SsoUserID                *string                         `gorm:"column:sso_user_id;uniqueIndex" json:"sso_user_id"`
	OrganizationID           *string                         `gorm:"column:organization_id" json:"organization_id"`
	ObjectPermissionID       *string                         `gorm:"column:object_permission_id" json:"object_permission_id"`
	Password                 *string                         `gorm:"column:password" json:"password"`
	PasswordResetRequired    *bool                           `gorm:"column:password_reset_required" json:"password_reset_required"`
	LastBreachCheckAt        *DateTime                       `gorm:"column:last_breach_check_at" json:"last_breach_check_at"`
	Teams                    []string                        `gorm:"column:teams;serializer:json;not null" json:"teams"`
	UserRole                 *string                         `gorm:"column:user_role" json:"user_role"`
	MaxBudget                *float64                        `gorm:"column:max_budget" json:"max_budget"`
	Spend                    float64                         `gorm:"column:spend;default:0;not null" json:"spend"`
	UserEmail                *string                         `gorm:"column:user_email" json:"user_email"`
	Models                   []string                        `gorm:"column:models;serializer:json;not null" json:"models"`
	Metadata                 datatypes.JSON                  `gorm:"column:metadata;default:'{}';not null" json:"metadata"`
	MaxParallelRequests      *int                            `gorm:"column:max_parallel_requests" json:"max_parallel_requests"`
	TpmLimit                 *int64                          `gorm:"column:tpm_limit" json:"tpm_limit"`
	RpmLimit                 *int64                          `gorm:"column:rpm_limit" json:"rpm_limit"`
	BudgetDuration           *string                         `gorm:"column:budget_duration" json:"budget_duration"`
	BudgetResetAt            *DateTime                       `gorm:"column:budget_reset_at" json:"budget_reset_at"`
	AllowedCacheControls     []string                        `gorm:"column:allowed_cache_controls;serializer:json;not null" json:"allowed_cache_controls"`
	Policies                 []string                        `gorm:"column:policies;serializer:json;not null" json:"policies"`
	ModelSpend               datatypes.JSON                  `gorm:"column:model_spend;default:'{}';not null" json:"model_spend"`
	ModelMaxBudget           datatypes.JSON                  `gorm:"column:model_max_budget;default:'{}';not null" json:"model_max_budget"`
	CreatedAt                *DateTime                       `gorm:"column:created_at;autoCreateTime:false" json:"created_at"`
	UpdatedAt                *DateTime                       `gorm:"column:updated_at;autoUpdateTime:false" json:"updated_at"`
	LitellmOrganizationTable *LiteLLMOrganizationTable       `gorm:"foreignKey:OrganizationID;references:OrganizationID"`
	ObjectPermission         *LiteLLMObjectPermissionTable   `gorm:"foreignKey:ObjectPermissionID;references:ObjectPermissionID"`
	VerifiedSubjects         []LiteLLMVerifiedSubject        `gorm:"foreignKey:UserID;references:UserID;constraint:OnDelete:CASCADE"`
	OrganizationMemberships  []LiteLLMOrganizationMembership `gorm:"foreignKey:UserID;references:UserID"`
	InvitationsCreated       []LiteLLMInvitationLink         `gorm:"foreignKey:CreatedBy;references:UserID"`
	InvitationsUpdated       []LiteLLMInvitationLink         `gorm:"foreignKey:UpdatedBy;references:UserID"`
	InvitationsUser          []LiteLLMInvitationLink         `gorm:"foreignKey:UserID;references:UserID"`
}

func (LiteLLMUserTable) TableName() string {
	return "LiteLLM_UserTable"
}

func (m *LiteLLMUserTable) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt == nil {
		m.CreatedAt = &DateTime{Time: now}
	}
	if m.UpdatedAt == nil {
		m.UpdatedAt = &DateTime{Time: now}
	}
	return nil
}

func (*LiteLLMUserTable) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", &DateTime{Time: now})
	}
	return nil
}

type LiteLLMObjectPermissionTable struct {
	ObjectPermissionID   string                     `gorm:"column:object_permission_id;primaryKey;type:char(36)" json:"object_permission_id"`
	McpServers           []string                   `gorm:"column:mcp_servers;serializer:json;not null" json:"mcp_servers"`
	McpAccessGroups      []string                   `gorm:"column:mcp_access_groups;serializer:json;not null" json:"mcp_access_groups"`
	McpToolPermissions   datatypes.JSON             `gorm:"column:mcp_tool_permissions" json:"mcp_tool_permissions"`
	VectorStores         []string                   `gorm:"column:vector_stores;serializer:json;not null" json:"vector_stores"`
	Agents               []string                   `gorm:"column:agents;serializer:json;not null" json:"agents"`
	AgentAccessGroups    []string                   `gorm:"column:agent_access_groups;serializer:json;not null" json:"agent_access_groups"`
	Models               []string                   `gorm:"column:models;serializer:json;not null" json:"models"`
	BlockedTools         []string                   `gorm:"column:blocked_tools;serializer:json;not null" json:"blocked_tools"`
	McpToolsets          []string                   `gorm:"column:mcp_toolsets;serializer:json;not null" json:"mcp_toolsets"`
	SearchTools          []string                   `gorm:"column:search_tools;serializer:json;not null" json:"search_tools"`
	McpToolSearchEnabled *bool                      `gorm:"column:mcp_tool_search_enabled" json:"mcp_tool_search_enabled"`
	Skills               []string                   `gorm:"column:skills;serializer:json;not null" json:"skills"`
	Teams                []LiteLLMTeamTable         `gorm:"foreignKey:ObjectPermissionID;references:ObjectPermissionID"`
	Projects             []LiteLLMProjectTable      `gorm:"foreignKey:ObjectPermissionID;references:ObjectPermissionID"`
	VerificationTokens   []LiteLLMVerificationToken `gorm:"foreignKey:ObjectPermissionID;references:ObjectPermissionID"`
	Organizations        []LiteLLMOrganizationTable `gorm:"foreignKey:ObjectPermissionID;references:ObjectPermissionID"`
	Users                []LiteLLMUserTable         `gorm:"foreignKey:ObjectPermissionID;references:ObjectPermissionID"`
	EndUsers             []LiteLLMEndUserTable      `gorm:"foreignKey:ObjectPermissionID;references:ObjectPermissionID"`
	AgentsTable          []LiteLLMAgentsTable       `gorm:"foreignKey:ObjectPermissionID;references:ObjectPermissionID"`
}

func (LiteLLMObjectPermissionTable) TableName() string {
	return "LiteLLM_ObjectPermissionTable"
}

func (m *LiteLLMObjectPermissionTable) BeforeCreate(_ *gorm.DB) error {
	if m.ObjectPermissionID == "" {
		m.ObjectPermissionID = uuid.NewString()
	}
	return nil
}

type LiteLLMMCPServerTable struct {
	ServerID                  string         `gorm:"column:server_id;primaryKey;type:char(36)" json:"server_id"`
	ServerName                *string        `gorm:"column:server_name" json:"server_name"`
	Alias                     *string        `gorm:"column:alias" json:"alias"`
	Description               *string        `gorm:"column:description" json:"description"`
	Instructions              *string        `gorm:"column:instructions" json:"instructions"`
	URL                       *string        `gorm:"column:url" json:"url"`
	SpecPath                  *string        `gorm:"column:spec_path" json:"spec_path"`
	Transport                 *string        `gorm:"column:transport;default:'sse';not null" json:"transport"`
	AuthType                  *string        `gorm:"column:auth_type" json:"auth_type"`
	Credentials               datatypes.JSON `gorm:"column:credentials;default:'{}'" json:"credentials"`
	CreatedAt                 *DateTime      `gorm:"column:created_at;autoCreateTime:false" json:"created_at"`
	CreatedBy                 *string        `gorm:"column:created_by" json:"created_by"`
	UpdatedAt                 *DateTime      `gorm:"column:updated_at;autoUpdateTime:false" json:"updated_at"`
	UpdatedBy                 *string        `gorm:"column:updated_by" json:"updated_by"`
	McpInfo                   datatypes.JSON `gorm:"column:mcp_info;default:'{}'" json:"mcp_info"`
	McpAccessGroups           []string       `gorm:"column:mcp_access_groups;serializer:json;not null" json:"mcp_access_groups"`
	AllowedTools              []string       `gorm:"column:allowed_tools;serializer:json;not null" json:"allowed_tools"`
	ToolNameToDisplayName     datatypes.JSON `gorm:"column:tool_name_to_display_name;default:'{}'" json:"tool_name_to_display_name"`
	ToolNameToDescription     datatypes.JSON `gorm:"column:tool_name_to_description;default:'{}'" json:"tool_name_to_description"`
	PinnedTools               datatypes.JSON `gorm:"column:pinned_tools;default:'{}'" json:"pinned_tools"`
	ExtraHeaders              []string       `gorm:"column:extra_headers;serializer:json;not null" json:"extra_headers"`
	StaticHeaders             datatypes.JSON `gorm:"column:static_headers;default:'{}'" json:"static_headers"`
	EnvVars                   datatypes.JSON `gorm:"column:env_vars;default:'[]'" json:"env_vars"`
	Status                    *string        `gorm:"column:status;default:'unknown'" json:"status"`
	LastHealthCheck           *DateTime      `gorm:"column:last_health_check" json:"last_health_check"`
	HealthCheckError          *string        `gorm:"column:health_check_error" json:"health_check_error"`
	Command                   *string        `gorm:"column:command" json:"command"`
	Args                      []string       `gorm:"column:args;serializer:json;not null" json:"args"`
	Env                       datatypes.JSON `gorm:"column:env;default:'{}'" json:"env"`
	Issuer                    *string        `gorm:"column:issuer" json:"issuer"`
	AuthorizationURL          *string        `gorm:"column:authorization_url" json:"authorization_url"`
	TokenURL                  *string        `gorm:"column:token_url" json:"token_url"`
	RegistrationURL           *string        `gorm:"column:registration_url" json:"registration_url"`
	Oauth2Flow                *string        `gorm:"column:oauth2_flow" json:"oauth2_flow"`
	TokenExchangeEndpoint     *string        `gorm:"column:token_exchange_endpoint" json:"token_exchange_endpoint"`
	Audience                  *string        `gorm:"column:audience" json:"audience"`
	SubjectTokenType          *string        `gorm:"column:subject_token_type" json:"subject_token_type"`
	TokenExchangeProfile      *string        `gorm:"column:token_exchange_profile" json:"token_exchange_profile"`
	AllowAllKeys              bool           `gorm:"column:allow_all_keys;default:false;not null" json:"allow_all_keys"`
	AvailableOnPublicInternet *bool          `gorm:"column:available_on_public_internet;default:true;not null" json:"available_on_public_internet"`
	DelegateAuthToUpstream    bool           `gorm:"column:delegate_auth_to_upstream;default:false;not null" json:"delegate_auth_to_upstream"`
	OauthPassthrough          bool           `gorm:"column:oauth_passthrough;default:false;not null" json:"oauth_passthrough"`
	DcrBridge                 *bool          `gorm:"column:dcr_bridge" json:"dcr_bridge"`
	PerServerOauthDiscovery   bool           `gorm:"column:per_server_oauth_discovery;default:false;not null" json:"per_server_oauth_discovery"`
	IsByok                    bool           `gorm:"column:is_byok;default:false;not null" json:"is_byok"`
	ByokDescription           []string       `gorm:"column:byok_description;serializer:json;not null" json:"byok_description"`
	ByokAPIKeyHelpURL         *string        `gorm:"column:byok_api_key_help_url" json:"byok_api_key_help_url"`
	SourceURL                 *string        `gorm:"column:source_url" json:"source_url"`
	Timeout                   *float64       `gorm:"column:timeout" json:"timeout"`
	MaxConcurrentRequests     *int           `gorm:"column:max_concurrent_requests" json:"max_concurrent_requests"`
	Rpm                       *int           `gorm:"column:rpm" json:"rpm"`
	ApprovalStatus            *string        `gorm:"column:approval_status;index:LiteLLM_MCPServerTable_approval_status_idx;default:'active'" json:"approval_status"`
	SubmittedBy               *string        `gorm:"column:submitted_by" json:"submitted_by"`
	SubmittedAt               *DateTime      `gorm:"column:submitted_at" json:"submitted_at"`
	ReviewedAt                *DateTime      `gorm:"column:reviewed_at" json:"reviewed_at"`
	ReviewNotes               *string        `gorm:"column:review_notes" json:"review_notes"`
}

func (LiteLLMMCPServerTable) TableName() string {
	return "LiteLLM_MCPServerTable"
}

func (m *LiteLLMMCPServerTable) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ServerID == "" {
		m.ServerID = uuid.NewString()
	}
	if m.CreatedAt == nil {
		m.CreatedAt = &DateTime{Time: now}
	}
	if m.UpdatedAt == nil {
		m.UpdatedAt = &DateTime{Time: now}
	}
	return nil
}

func (*LiteLLMMCPServerTable) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", &DateTime{Time: now})
	}
	return nil
}

type LiteLLMMCPToolsetTable struct {
	ToolsetID   string         `gorm:"column:toolset_id;primaryKey;type:char(36)" json:"toolset_id"`
	ToolsetName string         `gorm:"column:toolset_name;uniqueIndex;not null" json:"toolset_name"`
	Description *string        `gorm:"column:description" json:"description"`
	Tools       datatypes.JSON `gorm:"column:tools;default:'[]';not null" json:"tools"`
	CreatedAt   DateTime       `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	CreatedBy   *string        `gorm:"column:created_by" json:"created_by"`
	UpdatedAt   DateTime       `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	UpdatedBy   *string        `gorm:"column:updated_by" json:"updated_by"`
}

func (LiteLLMMCPToolsetTable) TableName() string {
	return "LiteLLM_MCPToolsetTable"
}

func (m *LiteLLMMCPToolsetTable) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ToolsetID == "" {
		m.ToolsetID = uuid.NewString()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*LiteLLMMCPToolsetTable) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LiteLLMMCPUserCredentials struct {
	ID            string   `gorm:"column:id;primaryKey;type:char(36)" json:"id"`
	UserID        string   `gorm:"column:user_id;uniqueIndex:LiteLLM_MCPUserCredentials_user_id_server_id_key;not null" json:"user_id"`
	ServerID      string   `gorm:"column:server_id;uniqueIndex:LiteLLM_MCPUserCredentials_user_id_server_id_key;not null" json:"server_id"`
	CredentialB64 string   `gorm:"column:credential_b64;not null" json:"credential_b64"`
	CreatedAt     DateTime `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt     DateTime `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
}

func (LiteLLMMCPUserCredentials) TableName() string {
	return "LiteLLM_MCPUserCredentials"
}

func (m *LiteLLMMCPUserCredentials) BeforeCreate(tx *gorm.DB) error {
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

func (*LiteLLMMCPUserCredentials) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LiteLLMUserProviderCredentials struct {
	ID             string   `gorm:"column:id;primaryKey;type:char(36)" json:"id"`
	UserID         string   `gorm:"column:user_id;uniqueIndex:LiteLLM_UserProviderCredentials_user_id_credential_name_key;not null" json:"user_id"`
	CredentialName string   `gorm:"column:credential_name;uniqueIndex:LiteLLM_UserProviderCredentials_user_id_credential_name_key;index:LiteLLM_UserProviderCredentials_credential_name_idx;not null" json:"credential_name"`
	Provider       string   `gorm:"column:provider;not null" json:"provider"`
	CredentialB64  string   `gorm:"column:credential_b64;not null" json:"credential_b64"`
	CreatedAt      DateTime `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt      DateTime `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
}

func (LiteLLMUserProviderCredentials) TableName() string {
	return "LiteLLM_UserProviderCredentials"
}

func (m *LiteLLMUserProviderCredentials) BeforeCreate(tx *gorm.DB) error {
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

func (*LiteLLMUserProviderCredentials) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LiteLLMMCPUserEnvVars struct {
	ID        string   `gorm:"column:id;primaryKey;type:char(36)" json:"id"`
	UserID    string   `gorm:"column:user_id;uniqueIndex:LiteLLM_MCPUserEnvVars_user_id_server_id_key;index:LiteLLM_MCPUserEnvVars_user_id_idx;not null" json:"user_id"`
	ServerID  string   `gorm:"column:server_id;uniqueIndex:LiteLLM_MCPUserEnvVars_user_id_server_id_key;index:LiteLLM_MCPUserEnvVars_server_id_idx;not null" json:"server_id"`
	ValuesB64 string   `gorm:"column:values_b64;not null" json:"values_b64"`
	CreatedAt DateTime `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt DateTime `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
}

func (LiteLLMMCPUserEnvVars) TableName() string {
	return "LiteLLM_MCPUserEnvVars"
}

func (m *LiteLLMMCPUserEnvVars) BeforeCreate(tx *gorm.DB) error {
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

func (*LiteLLMMCPUserEnvVars) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LiteLLMMCPServerOAuthClient struct {
	ServerID    string         `gorm:"column:server_id;primaryKey" json:"server_id"`
	Credentials datatypes.JSON `gorm:"column:credentials" json:"credentials"`
	CreatedAt   DateTime       `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt   DateTime       `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
}

func (LiteLLMMCPServerOAuthClient) TableName() string {
	return "LiteLLM_MCPServerOAuthClient"
}

func (m *LiteLLMMCPServerOAuthClient) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*LiteLLMMCPServerOAuthClient) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LiteLLMSSOIdentityAssertion struct {
	UserID       string   `gorm:"column:user_id;primaryKey" json:"user_id"`
	AssertionB64 string   `gorm:"column:assertion_b64;not null" json:"assertion_b64"`
	CreatedAt    DateTime `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt    DateTime `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
}

func (LiteLLMSSOIdentityAssertion) TableName() string {
	return "LiteLLM_SSOIdentityAssertion"
}

func (m *LiteLLMSSOIdentityAssertion) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*LiteLLMSSOIdentityAssertion) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LiteLLMVerificationToken struct {
	Token                    string                        `gorm:"column:token;primaryKey" json:"token"`
	KeyName                  *string                       `gorm:"column:key_name" json:"key_name"`
	KeyAlias                 *string                       `gorm:"column:key_alias" json:"key_alias"`
	SoftBudgetCooldown       bool                          `gorm:"column:soft_budget_cooldown;default:false;not null" json:"soft_budget_cooldown"`
	Spend                    float64                       `gorm:"column:spend;default:0;not null" json:"spend"`
	TotalSpend               float64                       `gorm:"column:total_spend;default:0;not null" json:"total_spend"`
	Expires                  *DateTime                     `gorm:"column:expires;index:LiteLLM_VerificationToken_budget_reset_at_expires_idx" json:"expires"`
	Models                   []string                      `gorm:"column:models;serializer:json;not null" json:"models"`
	Aliases                  datatypes.JSON                `gorm:"column:aliases;default:'{}';not null" json:"aliases"`
	Config                   datatypes.JSON                `gorm:"column:config;default:'{}';not null" json:"config"`
	RouterSettings           datatypes.JSON                `gorm:"column:router_settings;default:'{}'" json:"router_settings"`
	UserID                   *string                       `gorm:"column:user_id;index:LiteLLM_VerificationToken_user_id_team_id_idx" json:"user_id"`
	TeamID                   *string                       `gorm:"column:team_id;index:LiteLLM_VerificationToken_user_id_team_id_idx;index:LiteLLM_VerificationToken_team_id_idx" json:"team_id"`
	AgentID                  *string                       `gorm:"column:agent_id" json:"agent_id"`
	ProjectID                *string                       `gorm:"column:project_id" json:"project_id"`
	Permissions              datatypes.JSON                `gorm:"column:permissions;default:'{}';not null" json:"permissions"`
	MaxParallelRequests      *int                          `gorm:"column:max_parallel_requests" json:"max_parallel_requests"`
	Metadata                 datatypes.JSON                `gorm:"column:metadata;default:'{}';not null" json:"metadata"`
	Blocked                  *bool                         `gorm:"column:blocked" json:"blocked"`
	TpmLimit                 *int64                        `gorm:"column:tpm_limit" json:"tpm_limit"`
	RpmLimit                 *int64                        `gorm:"column:rpm_limit" json:"rpm_limit"`
	TpdLimit                 *int64                        `gorm:"column:tpd_limit" json:"tpd_limit"`
	MaxBudget                *float64                      `gorm:"column:max_budget" json:"max_budget"`
	BudgetDuration           *string                       `gorm:"column:budget_duration" json:"budget_duration"`
	BudgetResetAt            *DateTime                     `gorm:"column:budget_reset_at;index:LiteLLM_VerificationToken_budget_reset_at_expires_idx" json:"budget_reset_at"`
	AllowedCacheControls     []string                      `gorm:"column:allowed_cache_controls;serializer:json;not null" json:"allowed_cache_controls"`
	AllowedRoutes            []string                      `gorm:"column:allowed_routes;serializer:json;not null" json:"allowed_routes"`
	KeyType                  *string                       `gorm:"column:key_type" json:"key_type"`
	Policies                 []string                      `gorm:"column:policies;serializer:json;not null" json:"policies"`
	AccessGroupIds           []string                      `gorm:"column:access_group_ids;serializer:json;not null" json:"access_group_ids"`
	ModelSpend               datatypes.JSON                `gorm:"column:model_spend;default:'{}';not null" json:"model_spend"`
	ModelMaxBudget           datatypes.JSON                `gorm:"column:model_max_budget;default:'{}';not null" json:"model_max_budget"`
	BudgetFallbacks          datatypes.JSON                `gorm:"column:budget_fallbacks;default:'{}';not null" json:"budget_fallbacks"`
	BudgetID                 *string                       `gorm:"column:budget_id" json:"budget_id"`
	OrganizationID           *string                       `gorm:"column:organization_id" json:"organization_id"`
	ObjectPermissionID       *string                       `gorm:"column:object_permission_id" json:"object_permission_id"`
	CreatedAt                *DateTime                     `gorm:"column:created_at;autoCreateTime:false" json:"created_at"`
	CreatedBy                *string                       `gorm:"column:created_by" json:"created_by"`
	UpdatedAt                *DateTime                     `gorm:"column:updated_at;autoUpdateTime:false" json:"updated_at"`
	UpdatedBy                *string                       `gorm:"column:updated_by" json:"updated_by"`
	SettingsUpdatedAt        *DateTime                     `gorm:"column:settings_updated_at" json:"settings_updated_at"`
	LastActive               *DateTime                     `gorm:"column:last_active" json:"last_active"`
	RotationCount            *int                          `gorm:"column:rotation_count;default:0" json:"rotation_count"`
	AutoRotate               *bool                         `gorm:"column:auto_rotate;default:false" json:"auto_rotate"`
	RotationInterval         *string                       `gorm:"column:rotation_interval" json:"rotation_interval"`
	LastRotationAt           *DateTime                     `gorm:"column:last_rotation_at" json:"last_rotation_at"`
	KeyRotationAt            *DateTime                     `gorm:"column:key_rotation_at" json:"key_rotation_at"`
	BudgetLimits             datatypes.JSON                `gorm:"column:budget_limits" json:"budget_limits"`
	LitellmBudgetTable       *LiteLLMBudgetTable           `gorm:"foreignKey:BudgetID;references:BudgetID"`
	LitellmOrganizationTable *LiteLLMOrganizationTable     `gorm:"foreignKey:OrganizationID;references:OrganizationID"`
	LitellmProjectTable      *LiteLLMProjectTable          `gorm:"foreignKey:ProjectID;references:ProjectID"`
	ObjectPermission         *LiteLLMObjectPermissionTable `gorm:"foreignKey:ObjectPermissionID;references:ObjectPermissionID"`
	JwtKeyMappings           []LiteLLMJWTKeyMapping        `gorm:"foreignKey:Token;references:Token;constraint:OnDelete:CASCADE"`
}

func (LiteLLMVerificationToken) TableName() string {
	return "LiteLLM_VerificationToken"
}

func (m *LiteLLMVerificationToken) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt == nil {
		m.CreatedAt = &DateTime{Time: now}
	}
	if m.UpdatedAt == nil {
		m.UpdatedAt = &DateTime{Time: now}
	}
	return nil
}

func (*LiteLLMVerificationToken) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", &DateTime{Time: now})
	}
	return nil
}

type LiteLLMJWTKeyMapping struct {
	ID                       string                    `gorm:"column:id;primaryKey;type:char(36)" json:"id"`
	JwtIssuer                string                    `gorm:"column:jwt_issuer;uniqueIndex:LiteLLM_JWTKeyMapping_jwt_issuer_jwt_claim_name_jwt_claim_value_key;index:LiteLLM_JWTKeyMapping_jwt_issuer_jwt_claim_name_jwt_claim_value_is_active_idx;default:'';not null" json:"jwt_issuer"`
	JwtClaimName             string                    `gorm:"column:jwt_claim_name;uniqueIndex:LiteLLM_JWTKeyMapping_jwt_issuer_jwt_claim_name_jwt_claim_value_key;index:LiteLLM_JWTKeyMapping_jwt_issuer_jwt_claim_name_jwt_claim_value_is_active_idx;not null" json:"jwt_claim_name"`
	JwtClaimValue            string                    `gorm:"column:jwt_claim_value;uniqueIndex:LiteLLM_JWTKeyMapping_jwt_issuer_jwt_claim_name_jwt_claim_value_key;index:LiteLLM_JWTKeyMapping_jwt_issuer_jwt_claim_name_jwt_claim_value_is_active_idx;not null" json:"jwt_claim_value"`
	Token                    string                    `gorm:"column:token;not null" json:"token"`
	Description              *string                   `gorm:"column:description" json:"description"`
	IsActive                 *bool                     `gorm:"column:is_active;index:LiteLLM_JWTKeyMapping_jwt_issuer_jwt_claim_name_jwt_claim_value_is_active_idx;default:true;not null" json:"is_active"`
	CreatedAt                DateTime                  `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	CreatedBy                *string                   `gorm:"column:created_by" json:"created_by"`
	UpdatedAt                DateTime                  `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	UpdatedBy                *string                   `gorm:"column:updated_by" json:"updated_by"`
	LitellmVerificationToken *LiteLLMVerificationToken `gorm:"foreignKey:Token;references:Token"`
}

func (LiteLLMJWTKeyMapping) TableName() string {
	return "LiteLLM_JWTKeyMapping"
}

func (m *LiteLLMJWTKeyMapping) BeforeCreate(tx *gorm.DB) error {
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

func (*LiteLLMJWTKeyMapping) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LiteLLMDeprecatedVerificationToken struct {
	ID            string   `gorm:"column:id;primaryKey;type:char(36)" json:"id"`
	Token         string   `gorm:"column:token;uniqueIndex;index:LiteLLM_DeprecatedVerificationToken_token_revoke_at_idx;not null" json:"token"`
	ActiveTokenID string   `gorm:"column:active_token_id;not null" json:"active_token_id"`
	RevokeAt      DateTime `gorm:"column:revoke_at;index:LiteLLM_DeprecatedVerificationToken_token_revoke_at_idx;index:LiteLLM_DeprecatedVerificationToken_revoke_at_idx;not null" json:"revoke_at"`
	CreatedAt     DateTime `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
}

func (LiteLLMDeprecatedVerificationToken) TableName() string {
	return "LiteLLM_DeprecatedVerificationToken"
}

func (m *LiteLLMDeprecatedVerificationToken) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = uuid.NewString()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type LiteLLMDeletedVerificationToken struct {
	ID                   string         `gorm:"column:id;primaryKey;type:char(36)" json:"id"`
	Token                string         `gorm:"column:token;index:LiteLLM_DeletedVerificationToken_token_idx;not null" json:"token"`
	KeyName              *string        `gorm:"column:key_name" json:"key_name"`
	KeyAlias             *string        `gorm:"column:key_alias;index:LiteLLM_DeletedVerificationToken_key_alias_idx" json:"key_alias"`
	SoftBudgetCooldown   bool           `gorm:"column:soft_budget_cooldown;default:false;not null" json:"soft_budget_cooldown"`
	Spend                float64        `gorm:"column:spend;default:0;not null" json:"spend"`
	TotalSpend           float64        `gorm:"column:total_spend;default:0;not null" json:"total_spend"`
	Expires              *DateTime      `gorm:"column:expires" json:"expires"`
	Models               []string       `gorm:"column:models;serializer:json;not null" json:"models"`
	Aliases              datatypes.JSON `gorm:"column:aliases;default:'{}';not null" json:"aliases"`
	Config               datatypes.JSON `gorm:"column:config;default:'{}';not null" json:"config"`
	UserID               *string        `gorm:"column:user_id;index:LiteLLM_DeletedVerificationToken_user_id_idx" json:"user_id"`
	TeamID               *string        `gorm:"column:team_id;index:LiteLLM_DeletedVerificationToken_team_id_idx" json:"team_id"`
	AgentID              *string        `gorm:"column:agent_id" json:"agent_id"`
	ProjectID            *string        `gorm:"column:project_id" json:"project_id"`
	Permissions          datatypes.JSON `gorm:"column:permissions;default:'{}';not null" json:"permissions"`
	MaxParallelRequests  *int           `gorm:"column:max_parallel_requests" json:"max_parallel_requests"`
	Metadata             datatypes.JSON `gorm:"column:metadata;default:'{}';not null" json:"metadata"`
	Blocked              *bool          `gorm:"column:blocked" json:"blocked"`
	TpmLimit             *int64         `gorm:"column:tpm_limit" json:"tpm_limit"`
	RpmLimit             *int64         `gorm:"column:rpm_limit" json:"rpm_limit"`
	TpdLimit             *int64         `gorm:"column:tpd_limit" json:"tpd_limit"`
	MaxBudget            *float64       `gorm:"column:max_budget" json:"max_budget"`
	BudgetDuration       *string        `gorm:"column:budget_duration" json:"budget_duration"`
	BudgetResetAt        *DateTime      `gorm:"column:budget_reset_at" json:"budget_reset_at"`
	AllowedCacheControls []string       `gorm:"column:allowed_cache_controls;serializer:json;not null" json:"allowed_cache_controls"`
	AllowedRoutes        []string       `gorm:"column:allowed_routes;serializer:json;not null" json:"allowed_routes"`
	KeyType              *string        `gorm:"column:key_type" json:"key_type"`
	Policies             []string       `gorm:"column:policies;serializer:json;not null" json:"policies"`
	AccessGroupIds       []string       `gorm:"column:access_group_ids;serializer:json;not null" json:"access_group_ids"`
	ModelSpend           datatypes.JSON `gorm:"column:model_spend;default:'{}';not null" json:"model_spend"`
	ModelMaxBudget       datatypes.JSON `gorm:"column:model_max_budget;default:'{}';not null" json:"model_max_budget"`
	BudgetFallbacks      datatypes.JSON `gorm:"column:budget_fallbacks;default:'{}';not null" json:"budget_fallbacks"`
	RouterSettings       datatypes.JSON `gorm:"column:router_settings;default:'{}'" json:"router_settings"`
	BudgetID             *string        `gorm:"column:budget_id" json:"budget_id"`
	OrganizationID       *string        `gorm:"column:organization_id;index:LiteLLM_DeletedVerificationToken_organization_id_idx" json:"organization_id"`
	ObjectPermissionID   *string        `gorm:"column:object_permission_id" json:"object_permission_id"`
	CreatedAt            *DateTime      `gorm:"column:created_at;index:LiteLLM_DeletedVerificationToken_created_at_idx;autoCreateTime:false" json:"created_at"`
	CreatedBy            *string        `gorm:"column:created_by" json:"created_by"`
	UpdatedAt            *DateTime      `gorm:"column:updated_at;autoUpdateTime:false" json:"updated_at"`
	UpdatedBy            *string        `gorm:"column:updated_by" json:"updated_by"`
	SettingsUpdatedAt    *DateTime      `gorm:"column:settings_updated_at" json:"settings_updated_at"`
	LastActive           *DateTime      `gorm:"column:last_active" json:"last_active"`
	RotationCount        *int           `gorm:"column:rotation_count;default:0" json:"rotation_count"`
	AutoRotate           *bool          `gorm:"column:auto_rotate;default:false" json:"auto_rotate"`
	RotationInterval     *string        `gorm:"column:rotation_interval" json:"rotation_interval"`
	LastRotationAt       *DateTime      `gorm:"column:last_rotation_at" json:"last_rotation_at"`
	KeyRotationAt        *DateTime      `gorm:"column:key_rotation_at" json:"key_rotation_at"`
	DeletedAt            DateTime       `gorm:"column:deleted_at;index:LiteLLM_DeletedVerificationToken_deleted_at_idx;not null" json:"deleted_at"`
	DeletedBy            *string        `gorm:"column:deleted_by" json:"deleted_by"`
	DeletedByAPIKey      *string        `gorm:"column:deleted_by_api_key" json:"deleted_by_api_key"`
	LitellmChangedBy     *string        `gorm:"column:litellm_changed_by" json:"litellm_changed_by"`
}

func (LiteLLMDeletedVerificationToken) TableName() string {
	return "LiteLLM_DeletedVerificationToken"
}

func (m *LiteLLMDeletedVerificationToken) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = uuid.NewString()
	}
	if m.DeletedAt.IsZero() {
		m.DeletedAt = DateTime{Time: now}
	}
	return nil
}

type LiteLLMEndUserTable struct {
	UserID             string                        `gorm:"column:user_id;primaryKey" json:"user_id"`
	Alias              *string                       `gorm:"column:alias" json:"alias"`
	Spend              float64                       `gorm:"column:spend;default:0;not null" json:"spend"`
	AllowedModelRegion *string                       `gorm:"column:allowed_model_region" json:"allowed_model_region"`
	DefaultModel       *string                       `gorm:"column:default_model" json:"default_model"`
	Models             []string                      `gorm:"column:models;serializer:json;not null" json:"models"`
	BudgetID           *string                       `gorm:"column:budget_id" json:"budget_id"`
	ObjectPermissionID *string                       `gorm:"column:object_permission_id" json:"object_permission_id"`
	Blocked            bool                          `gorm:"column:blocked;default:false;not null" json:"blocked"`
	LitellmBudgetTable *LiteLLMBudgetTable           `gorm:"foreignKey:BudgetID;references:BudgetID"`
	ObjectPermission   *LiteLLMObjectPermissionTable `gorm:"foreignKey:ObjectPermissionID;references:ObjectPermissionID"`
}

func (LiteLLMEndUserTable) TableName() string {
	return "LiteLLM_EndUserTable"
}

type LiteLLMModelAccessGroupBudgetTable struct {
	AccessGroupName    string              `gorm:"column:access_group_name;primaryKey" json:"access_group_name"`
	Spend              float64             `gorm:"column:spend;default:0;not null" json:"spend"`
	BudgetID           *string             `gorm:"column:budget_id" json:"budget_id"`
	CreatedAt          DateTime            `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	CreatedBy          *string             `gorm:"column:created_by" json:"created_by"`
	UpdatedAt          DateTime            `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	UpdatedBy          *string             `gorm:"column:updated_by" json:"updated_by"`
	LitellmBudgetTable *LiteLLMBudgetTable `gorm:"foreignKey:BudgetID;references:BudgetID"`
}

func (LiteLLMModelAccessGroupBudgetTable) TableName() string {
	return "LiteLLM_ModelAccessGroupBudgetTable"
}

func (m *LiteLLMModelAccessGroupBudgetTable) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*LiteLLMModelAccessGroupBudgetTable) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LiteLLMTagTable struct {
	TagName            string              `gorm:"column:tag_name;primaryKey" json:"tag_name"`
	Description        *string             `gorm:"column:description" json:"description"`
	Models             []string            `gorm:"column:models;serializer:json;not null" json:"models"`
	ModelInfo          datatypes.JSON      `gorm:"column:model_info" json:"model_info"`
	Spend              float64             `gorm:"column:spend;default:0;not null" json:"spend"`
	BudgetID           *string             `gorm:"column:budget_id" json:"budget_id"`
	CreatedAt          DateTime            `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	CreatedBy          *string             `gorm:"column:created_by" json:"created_by"`
	UpdatedAt          DateTime            `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	LitellmBudgetTable *LiteLLMBudgetTable `gorm:"foreignKey:BudgetID;references:BudgetID"`
}

func (LiteLLMTagTable) TableName() string {
	return "LiteLLM_TagTable"
}

func (m *LiteLLMTagTable) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*LiteLLMTagTable) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LiteLLMConfig struct {
	ParamName      string         `gorm:"column:param_name;primaryKey" json:"param_name"`
	ParamValue     datatypes.JSON `gorm:"column:param_value" json:"param_value"`
	LastRunAt      *DateTime      `gorm:"column:last_run_at" json:"last_run_at"`
	ReloadRevision int64          `gorm:"column:reload_revision;default:0;not null" json:"reload_revision"`
}

func (LiteLLMConfig) TableName() string {
	return "LiteLLM_Config"
}

type LiteLLMSpendLogs struct {
	RequestID             string         `gorm:"column:request_id;primaryKey;index:LiteLLM_SpendLogs_startTime_request_id_idx" json:"request_id"`
	CallType              string         `gorm:"column:call_type;not null" json:"call_type"`
	APIKey                string         `gorm:"column:api_key;index:LiteLLM_SpendLogs_api_key_startTime_idx;default:'';not null" json:"api_key"`
	Spend                 float64        `gorm:"column:spend;default:0;not null" json:"spend"`
	TotalTokens           int            `gorm:"column:total_tokens;default:0;not null" json:"total_tokens"`
	PromptTokens          int            `gorm:"column:prompt_tokens;default:0;not null" json:"prompt_tokens"`
	CompletionTokens      int            `gorm:"column:completion_tokens;default:0;not null" json:"completion_tokens"`
	StartTime             DateTime       `gorm:"column:startTime;index:LiteLLM_SpendLogs_startTime_idx;index:LiteLLM_SpendLogs_startTime_request_id_idx;index:LiteLLM_SpendLogs_api_key_startTime_idx;not null" json:"startTime"`
	EndTime               DateTime       `gorm:"column:endTime;not null" json:"endTime"`
	RequestDurationMs     *int           `gorm:"column:request_duration_ms" json:"request_duration_ms"`
	CompletionStartTime   *DateTime      `gorm:"column:completionStartTime" json:"completionStartTime"`
	Model                 string         `gorm:"column:model;default:'';not null" json:"model"`
	ModelID               *string        `gorm:"column:model_id;default:''" json:"model_id"`
	ModelGroup            *string        `gorm:"column:model_group;default:''" json:"model_group"`
	CustomLlmProvider     *string        `gorm:"column:custom_llm_provider;default:''" json:"custom_llm_provider"`
	APIBase               *string        `gorm:"column:api_base;default:''" json:"api_base"`
	User                  *string        `gorm:"column:user;default:''" json:"user"`
	Metadata              datatypes.JSON `gorm:"column:metadata;default:'{}'" json:"metadata"`
	CacheHit              *string        `gorm:"column:cache_hit;default:''" json:"cache_hit"`
	CacheKey              *string        `gorm:"column:cache_key;default:''" json:"cache_key"`
	RequestTags           datatypes.JSON `gorm:"column:request_tags;default:'[]'" json:"request_tags"`
	TeamID                *string        `gorm:"column:team_id" json:"team_id"`
	OrganizationID        *string        `gorm:"column:organization_id" json:"organization_id"`
	EndUser               *string        `gorm:"column:end_user;index:LiteLLM_SpendLogs_end_user_idx" json:"end_user"`
	RequesterIPAddress    *string        `gorm:"column:requester_ip_address" json:"requester_ip_address"`
	Messages              datatypes.JSON `gorm:"column:messages;default:'{}'" json:"messages"`
	Response              datatypes.JSON `gorm:"column:response;default:'{}'" json:"response"`
	SessionID             *string        `gorm:"column:session_id;index:LiteLLM_SpendLogs_session_id_idx" json:"session_id"`
	Status                *string        `gorm:"column:status" json:"status"`
	McpNamespacedToolName *string        `gorm:"column:mcp_namespaced_tool_name" json:"mcp_namespaced_tool_name"`
	BillingAgentID        *string        `gorm:"column:billing_agent_id" json:"billing_agent_id"`
	AgentID               *string        `gorm:"column:agent_id" json:"agent_id"`
	ProxyServerRequest    datatypes.JSON `gorm:"column:proxy_server_request;default:'{}'" json:"proxy_server_request"`
	LitellmCallID         *string        `gorm:"column:litellm_call_id;index:LiteLLM_SpendLogs_litellm_call_id_idx" json:"litellm_call_id"`
	CreatedAt             DateTime       `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt             DateTime       `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
}

func (LiteLLMSpendLogs) TableName() string {
	return "LiteLLM_SpendLogs"
}

func (m *LiteLLMSpendLogs) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*LiteLLMSpendLogs) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LiteLLMBudgetWindowSpend struct {
	EntityType     string   `gorm:"column:entity_type;primaryKey" json:"entity_type"`
	EntityID       string   `gorm:"column:entity_id;primaryKey" json:"entity_id"`
	WindowDuration string   `gorm:"column:window_duration;primaryKey" json:"window_duration"`
	WindowStart    DateTime `gorm:"column:window_start;not null" json:"window_start"`
	Spend          float64  `gorm:"column:spend;default:0;not null" json:"spend"`
	CreatedAt      DateTime `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt      DateTime `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
}

func (LiteLLMBudgetWindowSpend) TableName() string {
	return "LiteLLM_BudgetWindowSpend"
}

func (m *LiteLLMBudgetWindowSpend) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*LiteLLMBudgetWindowSpend) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LiteLLMErrorLogs struct {
	RequestID        string         `gorm:"column:request_id;primaryKey;type:char(36)" json:"request_id"`
	StartTime        DateTime       `gorm:"column:startTime;not null" json:"startTime"`
	EndTime          DateTime       `gorm:"column:endTime;not null" json:"endTime"`
	APIBase          string         `gorm:"column:api_base;default:'';not null" json:"api_base"`
	ModelGroup       string         `gorm:"column:model_group;default:'';not null" json:"model_group"`
	LitellmModelName string         `gorm:"column:litellm_model_name;default:'';not null" json:"litellm_model_name"`
	ModelID          string         `gorm:"column:model_id;default:'';not null" json:"model_id"`
	RequestKwargs    datatypes.JSON `gorm:"column:request_kwargs;default:'{}';not null" json:"request_kwargs"`
	ExceptionType    string         `gorm:"column:exception_type;default:'';not null" json:"exception_type"`
	ExceptionString  string         `gorm:"column:exception_string;default:'';not null" json:"exception_string"`
	StatusCode       string         `gorm:"column:status_code;default:'';not null" json:"status_code"`
}

func (LiteLLMErrorLogs) TableName() string {
	return "LiteLLM_ErrorLogs"
}

func (m *LiteLLMErrorLogs) BeforeCreate(_ *gorm.DB) error {
	if m.RequestID == "" {
		m.RequestID = uuid.NewString()
	}
	return nil
}

type LiteLLMUserNotifications struct {
	RequestID     string   `gorm:"column:request_id;primaryKey" json:"request_id"`
	UserID        string   `gorm:"column:user_id;not null" json:"user_id"`
	Models        []string `gorm:"column:models;serializer:json;not null" json:"models"`
	Justification string   `gorm:"column:justification;not null" json:"justification"`
	Status        string   `gorm:"column:status;not null" json:"status"`
}

func (LiteLLMUserNotifications) TableName() string {
	return "LiteLLM_UserNotifications"
}

type LiteLLMTeamMembership struct {
	UserID             string              `gorm:"column:user_id;primaryKey" json:"user_id"`
	TeamID             string              `gorm:"column:team_id;primaryKey" json:"team_id"`
	Spend              float64             `gorm:"column:spend;default:0;not null" json:"spend"`
	TotalSpend         float64             `gorm:"column:total_spend;default:0;not null" json:"total_spend"`
	BudgetID           *string             `gorm:"column:budget_id" json:"budget_id"`
	LitellmBudgetTable *LiteLLMBudgetTable `gorm:"foreignKey:BudgetID;references:BudgetID"`
}

func (LiteLLMTeamMembership) TableName() string {
	return "LiteLLM_TeamMembership"
}

type LiteLLMOrganizationMembership struct {
	UserID             string                    `gorm:"column:user_id;primaryKey;uniqueIndex:LiteLLM_OrganizationMembership_user_id_organization_id_key" json:"user_id"`
	OrganizationID     string                    `gorm:"column:organization_id;primaryKey;uniqueIndex:LiteLLM_OrganizationMembership_user_id_organization_id_key" json:"organization_id"`
	UserRole           *string                   `gorm:"column:user_role" json:"user_role"`
	Spend              *float64                  `gorm:"column:spend;default:0" json:"spend"`
	BudgetID           *string                   `gorm:"column:budget_id" json:"budget_id"`
	CreatedAt          *DateTime                 `gorm:"column:created_at;autoCreateTime:false" json:"created_at"`
	UpdatedAt          *DateTime                 `gorm:"column:updated_at;autoUpdateTime:false" json:"updated_at"`
	User               *LiteLLMUserTable         `gorm:"foreignKey:UserID;references:UserID"`
	Organization       *LiteLLMOrganizationTable `gorm:"foreignKey:OrganizationID;references:OrganizationID"`
	LitellmBudgetTable *LiteLLMBudgetTable       `gorm:"foreignKey:BudgetID;references:BudgetID"`
}

func (LiteLLMOrganizationMembership) TableName() string {
	return "LiteLLM_OrganizationMembership"
}

func (m *LiteLLMOrganizationMembership) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt == nil {
		m.CreatedAt = &DateTime{Time: now}
	}
	if m.UpdatedAt == nil {
		m.UpdatedAt = &DateTime{Time: now}
	}
	return nil
}

func (*LiteLLMOrganizationMembership) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", &DateTime{Time: now})
	}
	return nil
}

type LiteLLMInvitationLink struct {
	ID                      string            `gorm:"column:id;primaryKey;type:char(36)" json:"id"`
	UserID                  string            `gorm:"column:user_id;not null" json:"user_id"`
	IsAccepted              bool              `gorm:"column:is_accepted;default:false;not null" json:"is_accepted"`
	AcceptedAt              *DateTime         `gorm:"column:accepted_at" json:"accepted_at"`
	ExpiresAt               DateTime          `gorm:"column:expires_at;not null" json:"expires_at"`
	CreatedAt               DateTime          `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	CreatedBy               string            `gorm:"column:created_by;not null" json:"created_by"`
	UpdatedAt               DateTime          `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	UpdatedBy               string            `gorm:"column:updated_by;not null" json:"updated_by"`
	LiteLLMUserTableUser    *LiteLLMUserTable `gorm:"foreignKey:UserID;references:UserID"`
	LiteLLMUserTableCreated *LiteLLMUserTable `gorm:"foreignKey:CreatedBy;references:UserID"`
	LiteLLMUserTableUpdated *LiteLLMUserTable `gorm:"foreignKey:UpdatedBy;references:UserID"`
}

func (LiteLLMInvitationLink) TableName() string {
	return "LiteLLM_InvitationLink"
}

func (m *LiteLLMInvitationLink) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = uuid.NewString()
	}
	return nil
}

type LiteLLMAuditLog struct {
	ID              string         `gorm:"column:id;primaryKey;type:char(36)" json:"id"`
	UpdatedAt       DateTime       `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	ChangedBy       string         `gorm:"column:changed_by;default:'';not null" json:"changed_by"`
	ChangedByAPIKey string         `gorm:"column:changed_by_api_key;default:'';not null" json:"changed_by_api_key"`
	Action          string         `gorm:"column:action;not null" json:"action"`
	TableName_      string         `gorm:"column:table_name;not null" json:"table_name"`
	ObjectID        string         `gorm:"column:object_id;not null" json:"object_id"`
	BeforeValue     datatypes.JSON `gorm:"column:before_value" json:"before_value"`
	UpdatedValues   datatypes.JSON `gorm:"column:updated_values" json:"updated_values"`
}

func (LiteLLMAuditLog) TableName() string {
	return "LiteLLM_AuditLog"
}

func (m *LiteLLMAuditLog) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = uuid.NewString()
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

type LiteLLMDailyUserSpend struct {
	ID                                 string   `gorm:"column:id;primaryKey;type:char(36)" json:"id"`
	UserID                             *string  `gorm:"column:user_id;uniqueIndex:LiteLLM_DailyUserSpend_user_id_date_api_key_model_custom_llm_provider_mcp_namespaced_tool_name_endpoint_key;index:LiteLLM_DailyUserSpend_user_id_date_idx" json:"user_id"`
	Date                               string   `gorm:"column:date;uniqueIndex:LiteLLM_DailyUserSpend_user_id_date_api_key_model_custom_llm_provider_mcp_namespaced_tool_name_endpoint_key;index:LiteLLM_DailyUserSpend_date_idx;index:LiteLLM_DailyUserSpend_user_id_date_idx;index:LiteLLM_DailyUserSpend_api_key_date_idx;not null" json:"date"`
	APIKey                             string   `gorm:"column:api_key;uniqueIndex:LiteLLM_DailyUserSpend_user_id_date_api_key_model_custom_llm_provider_mcp_namespaced_tool_name_endpoint_key;index:LiteLLM_DailyUserSpend_api_key_idx;index:LiteLLM_DailyUserSpend_api_key_date_idx;not null" json:"api_key"`
	Model                              *string  `gorm:"column:model;uniqueIndex:LiteLLM_DailyUserSpend_user_id_date_api_key_model_custom_llm_provider_mcp_namespaced_tool_name_endpoint_key;index:LiteLLM_DailyUserSpend_model_idx" json:"model"`
	ModelGroup                         *string  `gorm:"column:model_group" json:"model_group"`
	CustomLlmProvider                  *string  `gorm:"column:custom_llm_provider;uniqueIndex:LiteLLM_DailyUserSpend_user_id_date_api_key_model_custom_llm_provider_mcp_namespaced_tool_name_endpoint_key" json:"custom_llm_provider"`
	McpNamespacedToolName              *string  `gorm:"column:mcp_namespaced_tool_name;uniqueIndex:LiteLLM_DailyUserSpend_user_id_date_api_key_model_custom_llm_provider_mcp_namespaced_tool_name_endpoint_key;index:LiteLLM_DailyUserSpend_mcp_namespaced_tool_name_idx" json:"mcp_namespaced_tool_name"`
	Endpoint                           *string  `gorm:"column:endpoint;uniqueIndex:LiteLLM_DailyUserSpend_user_id_date_api_key_model_custom_llm_provider_mcp_namespaced_tool_name_endpoint_key;index:LiteLLM_DailyUserSpend_endpoint_idx" json:"endpoint"`
	PromptTokens                       int64    `gorm:"column:prompt_tokens;default:0;not null" json:"prompt_tokens"`
	CompletionTokens                   int64    `gorm:"column:completion_tokens;default:0;not null" json:"completion_tokens"`
	CacheReadInputTokens               int64    `gorm:"column:cache_read_input_tokens;default:0;not null" json:"cache_read_input_tokens"`
	CacheCreationInputTokens           int64    `gorm:"column:cache_creation_input_tokens;default:0;not null" json:"cache_creation_input_tokens"`
	CompressionSavedTokens             int64    `gorm:"column:compression_saved_tokens;default:0;not null" json:"compression_saved_tokens"`
	CompressionSavingsSpend            float64  `gorm:"column:compression_savings_spend;default:0;not null" json:"compression_savings_spend"`
	PromptCachingSavingsSpend          float64  `gorm:"column:prompt_caching_savings_spend;default:0;not null" json:"prompt_caching_savings_spend"`
	GatewayInjectedCachingSavingsSpend float64  `gorm:"column:gateway_injected_caching_savings_spend;default:0;not null" json:"gateway_injected_caching_savings_spend"`
	AutorouterSavingsSpend             float64  `gorm:"column:autorouter_savings_spend;default:0;not null" json:"autorouter_savings_spend"`
	GuardrailSpend                     float64  `gorm:"column:guardrail_spend;default:0;not null" json:"guardrail_spend"`
	Spend                              float64  `gorm:"column:spend;default:0;not null" json:"spend"`
	APIRequests                        int64    `gorm:"column:api_requests;default:0;not null" json:"api_requests"`
	SuccessfulRequests                 int64    `gorm:"column:successful_requests;default:0;not null" json:"successful_requests"`
	FailedRequests                     int64    `gorm:"column:failed_requests;default:0;not null" json:"failed_requests"`
	TotalResponseTimeMs                int64    `gorm:"column:total_response_time_ms;default:0;not null" json:"total_response_time_ms"`
	TimedRequests                      int64    `gorm:"column:timed_requests;default:0;not null" json:"timed_requests"`
	CreatedAt                          DateTime `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt                          DateTime `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
}

func (LiteLLMDailyUserSpend) TableName() string {
	return "LiteLLM_DailyUserSpend"
}

func (m *LiteLLMDailyUserSpend) BeforeCreate(tx *gorm.DB) error {
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

func (*LiteLLMDailyUserSpend) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LiteLLMDailyGlobalSpend struct {
	ID                                 string   `gorm:"column:id;primaryKey;type:char(36)" json:"id"`
	Date                               string   `gorm:"column:date;uniqueIndex:LiteLLM_DailyGlobalSpend_date_model_model_group_custom_llm_provider_mcp_namespaced_tool_name_endpoint_key;index:LiteLLM_DailyGlobalSpend_date_idx;not null" json:"date"`
	Model                              *string  `gorm:"column:model;uniqueIndex:LiteLLM_DailyGlobalSpend_date_model_model_group_custom_llm_provider_mcp_namespaced_tool_name_endpoint_key" json:"model"`
	ModelGroup                         *string  `gorm:"column:model_group;uniqueIndex:LiteLLM_DailyGlobalSpend_date_model_model_group_custom_llm_provider_mcp_namespaced_tool_name_endpoint_key" json:"model_group"`
	CustomLlmProvider                  *string  `gorm:"column:custom_llm_provider;uniqueIndex:LiteLLM_DailyGlobalSpend_date_model_model_group_custom_llm_provider_mcp_namespaced_tool_name_endpoint_key" json:"custom_llm_provider"`
	McpNamespacedToolName              *string  `gorm:"column:mcp_namespaced_tool_name;uniqueIndex:LiteLLM_DailyGlobalSpend_date_model_model_group_custom_llm_provider_mcp_namespaced_tool_name_endpoint_key" json:"mcp_namespaced_tool_name"`
	Endpoint                           *string  `gorm:"column:endpoint;uniqueIndex:LiteLLM_DailyGlobalSpend_date_model_model_group_custom_llm_provider_mcp_namespaced_tool_name_endpoint_key" json:"endpoint"`
	PromptTokens                       int64    `gorm:"column:prompt_tokens;default:0;not null" json:"prompt_tokens"`
	CompletionTokens                   int64    `gorm:"column:completion_tokens;default:0;not null" json:"completion_tokens"`
	CacheReadInputTokens               int64    `gorm:"column:cache_read_input_tokens;default:0;not null" json:"cache_read_input_tokens"`
	CacheCreationInputTokens           int64    `gorm:"column:cache_creation_input_tokens;default:0;not null" json:"cache_creation_input_tokens"`
	CompressionSavedTokens             int64    `gorm:"column:compression_saved_tokens;default:0;not null" json:"compression_saved_tokens"`
	CompressionSavingsSpend            float64  `gorm:"column:compression_savings_spend;default:0;not null" json:"compression_savings_spend"`
	PromptCachingSavingsSpend          float64  `gorm:"column:prompt_caching_savings_spend;default:0;not null" json:"prompt_caching_savings_spend"`
	GatewayInjectedCachingSavingsSpend float64  `gorm:"column:gateway_injected_caching_savings_spend;default:0;not null" json:"gateway_injected_caching_savings_spend"`
	AutorouterSavingsSpend             float64  `gorm:"column:autorouter_savings_spend;default:0;not null" json:"autorouter_savings_spend"`
	GuardrailSpend                     float64  `gorm:"column:guardrail_spend;default:0;not null" json:"guardrail_spend"`
	Spend                              float64  `gorm:"column:spend;default:0;not null" json:"spend"`
	APIRequests                        int64    `gorm:"column:api_requests;default:0;not null" json:"api_requests"`
	SuccessfulRequests                 int64    `gorm:"column:successful_requests;default:0;not null" json:"successful_requests"`
	FailedRequests                     int64    `gorm:"column:failed_requests;default:0;not null" json:"failed_requests"`
	TotalResponseTimeMs                int64    `gorm:"column:total_response_time_ms;default:0;not null" json:"total_response_time_ms"`
	TimedRequests                      int64    `gorm:"column:timed_requests;default:0;not null" json:"timed_requests"`
	CreatedAt                          DateTime `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt                          DateTime `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
}

func (LiteLLMDailyGlobalSpend) TableName() string {
	return "LiteLLM_DailyGlobalSpend"
}

func (m *LiteLLMDailyGlobalSpend) BeforeCreate(tx *gorm.DB) error {
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

func (*LiteLLMDailyGlobalSpend) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LiteLLMDailyOrganizationSpend struct {
	ID                                 string   `gorm:"column:id;primaryKey;type:char(36)" json:"id"`
	OrganizationID                     *string  `gorm:"column:organization_id;uniqueIndex:LiteLLM_DailyOrganizationSpend_organization_id_date_api_key_model_custom_llm_provider_mcp_namespaced_tool_name_endpoint_key;index:LiteLLM_DailyOrganizationSpend_organization_id_date_idx" json:"organization_id"`
	Date                               string   `gorm:"column:date;uniqueIndex:LiteLLM_DailyOrganizationSpend_organization_id_date_api_key_model_custom_llm_provider_mcp_namespaced_tool_name_endpoint_key;index:LiteLLM_DailyOrganizationSpend_date_idx;index:LiteLLM_DailyOrganizationSpend_organization_id_date_idx;not null" json:"date"`
	APIKey                             string   `gorm:"column:api_key;uniqueIndex:LiteLLM_DailyOrganizationSpend_organization_id_date_api_key_model_custom_llm_provider_mcp_namespaced_tool_name_endpoint_key;index:LiteLLM_DailyOrganizationSpend_api_key_idx;not null" json:"api_key"`
	Model                              *string  `gorm:"column:model;uniqueIndex:LiteLLM_DailyOrganizationSpend_organization_id_date_api_key_model_custom_llm_provider_mcp_namespaced_tool_name_endpoint_key;index:LiteLLM_DailyOrganizationSpend_model_idx" json:"model"`
	ModelGroup                         *string  `gorm:"column:model_group" json:"model_group"`
	CustomLlmProvider                  *string  `gorm:"column:custom_llm_provider;uniqueIndex:LiteLLM_DailyOrganizationSpend_organization_id_date_api_key_model_custom_llm_provider_mcp_namespaced_tool_name_endpoint_key" json:"custom_llm_provider"`
	McpNamespacedToolName              *string  `gorm:"column:mcp_namespaced_tool_name;uniqueIndex:LiteLLM_DailyOrganizationSpend_organization_id_date_api_key_model_custom_llm_provider_mcp_namespaced_tool_name_endpoint_key;index:LiteLLM_DailyOrganizationSpend_mcp_namespaced_tool_name_idx" json:"mcp_namespaced_tool_name"`
	Endpoint                           *string  `gorm:"column:endpoint;uniqueIndex:LiteLLM_DailyOrganizationSpend_organization_id_date_api_key_model_custom_llm_provider_mcp_namespaced_tool_name_endpoint_key;index:LiteLLM_DailyOrganizationSpend_endpoint_idx" json:"endpoint"`
	PromptTokens                       int64    `gorm:"column:prompt_tokens;default:0;not null" json:"prompt_tokens"`
	CompletionTokens                   int64    `gorm:"column:completion_tokens;default:0;not null" json:"completion_tokens"`
	CacheReadInputTokens               int64    `gorm:"column:cache_read_input_tokens;default:0;not null" json:"cache_read_input_tokens"`
	CacheCreationInputTokens           int64    `gorm:"column:cache_creation_input_tokens;default:0;not null" json:"cache_creation_input_tokens"`
	CompressionSavedTokens             int64    `gorm:"column:compression_saved_tokens;default:0;not null" json:"compression_saved_tokens"`
	CompressionSavingsSpend            float64  `gorm:"column:compression_savings_spend;default:0;not null" json:"compression_savings_spend"`
	PromptCachingSavingsSpend          float64  `gorm:"column:prompt_caching_savings_spend;default:0;not null" json:"prompt_caching_savings_spend"`
	GatewayInjectedCachingSavingsSpend float64  `gorm:"column:gateway_injected_caching_savings_spend;default:0;not null" json:"gateway_injected_caching_savings_spend"`
	AutorouterSavingsSpend             float64  `gorm:"column:autorouter_savings_spend;default:0;not null" json:"autorouter_savings_spend"`
	GuardrailSpend                     float64  `gorm:"column:guardrail_spend;default:0;not null" json:"guardrail_spend"`
	Spend                              float64  `gorm:"column:spend;default:0;not null" json:"spend"`
	APIRequests                        int64    `gorm:"column:api_requests;default:0;not null" json:"api_requests"`
	SuccessfulRequests                 int64    `gorm:"column:successful_requests;default:0;not null" json:"successful_requests"`
	FailedRequests                     int64    `gorm:"column:failed_requests;default:0;not null" json:"failed_requests"`
	TotalResponseTimeMs                int64    `gorm:"column:total_response_time_ms;default:0;not null" json:"total_response_time_ms"`
	TimedRequests                      int64    `gorm:"column:timed_requests;default:0;not null" json:"timed_requests"`
	CreatedAt                          DateTime `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt                          DateTime `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
}

func (LiteLLMDailyOrganizationSpend) TableName() string {
	return "LiteLLM_DailyOrganizationSpend"
}

func (m *LiteLLMDailyOrganizationSpend) BeforeCreate(tx *gorm.DB) error {
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

func (*LiteLLMDailyOrganizationSpend) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LiteLLMDailyEndUserSpend struct {
	ID                                 string   `gorm:"column:id;primaryKey;type:char(36)" json:"id"`
	EndUserID                          *string  `gorm:"column:end_user_id;uniqueIndex:LiteLLM_DailyEndUserSpend_end_user_id_date_api_key_model_custom_llm_provider_mcp_namespaced_tool_name_endpoint_key;index:LiteLLM_DailyEndUserSpend_end_user_id_date_idx" json:"end_user_id"`
	Date                               string   `gorm:"column:date;uniqueIndex:LiteLLM_DailyEndUserSpend_end_user_id_date_api_key_model_custom_llm_provider_mcp_namespaced_tool_name_endpoint_key;index:LiteLLM_DailyEndUserSpend_date_idx;index:LiteLLM_DailyEndUserSpend_end_user_id_date_idx;not null" json:"date"`
	APIKey                             string   `gorm:"column:api_key;uniqueIndex:LiteLLM_DailyEndUserSpend_end_user_id_date_api_key_model_custom_llm_provider_mcp_namespaced_tool_name_endpoint_key;index:LiteLLM_DailyEndUserSpend_api_key_idx;not null" json:"api_key"`
	Model                              *string  `gorm:"column:model;uniqueIndex:LiteLLM_DailyEndUserSpend_end_user_id_date_api_key_model_custom_llm_provider_mcp_namespaced_tool_name_endpoint_key;index:LiteLLM_DailyEndUserSpend_model_idx" json:"model"`
	ModelGroup                         *string  `gorm:"column:model_group" json:"model_group"`
	CustomLlmProvider                  *string  `gorm:"column:custom_llm_provider;uniqueIndex:LiteLLM_DailyEndUserSpend_end_user_id_date_api_key_model_custom_llm_provider_mcp_namespaced_tool_name_endpoint_key" json:"custom_llm_provider"`
	McpNamespacedToolName              *string  `gorm:"column:mcp_namespaced_tool_name;uniqueIndex:LiteLLM_DailyEndUserSpend_end_user_id_date_api_key_model_custom_llm_provider_mcp_namespaced_tool_name_endpoint_key;index:LiteLLM_DailyEndUserSpend_mcp_namespaced_tool_name_idx" json:"mcp_namespaced_tool_name"`
	Endpoint                           *string  `gorm:"column:endpoint;uniqueIndex:LiteLLM_DailyEndUserSpend_end_user_id_date_api_key_model_custom_llm_provider_mcp_namespaced_tool_name_endpoint_key;index:LiteLLM_DailyEndUserSpend_endpoint_idx" json:"endpoint"`
	PromptTokens                       int64    `gorm:"column:prompt_tokens;default:0;not null" json:"prompt_tokens"`
	CompletionTokens                   int64    `gorm:"column:completion_tokens;default:0;not null" json:"completion_tokens"`
	CacheReadInputTokens               int64    `gorm:"column:cache_read_input_tokens;default:0;not null" json:"cache_read_input_tokens"`
	CacheCreationInputTokens           int64    `gorm:"column:cache_creation_input_tokens;default:0;not null" json:"cache_creation_input_tokens"`
	CompressionSavedTokens             int64    `gorm:"column:compression_saved_tokens;default:0;not null" json:"compression_saved_tokens"`
	CompressionSavingsSpend            float64  `gorm:"column:compression_savings_spend;default:0;not null" json:"compression_savings_spend"`
	PromptCachingSavingsSpend          float64  `gorm:"column:prompt_caching_savings_spend;default:0;not null" json:"prompt_caching_savings_spend"`
	GatewayInjectedCachingSavingsSpend float64  `gorm:"column:gateway_injected_caching_savings_spend;default:0;not null" json:"gateway_injected_caching_savings_spend"`
	AutorouterSavingsSpend             float64  `gorm:"column:autorouter_savings_spend;default:0;not null" json:"autorouter_savings_spend"`
	GuardrailSpend                     float64  `gorm:"column:guardrail_spend;default:0;not null" json:"guardrail_spend"`
	Spend                              float64  `gorm:"column:spend;default:0;not null" json:"spend"`
	APIRequests                        int64    `gorm:"column:api_requests;default:0;not null" json:"api_requests"`
	SuccessfulRequests                 int64    `gorm:"column:successful_requests;default:0;not null" json:"successful_requests"`
	FailedRequests                     int64    `gorm:"column:failed_requests;default:0;not null" json:"failed_requests"`
	TotalResponseTimeMs                int64    `gorm:"column:total_response_time_ms;default:0;not null" json:"total_response_time_ms"`
	TimedRequests                      int64    `gorm:"column:timed_requests;default:0;not null" json:"timed_requests"`
	CreatedAt                          DateTime `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt                          DateTime `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
}

func (LiteLLMDailyEndUserSpend) TableName() string {
	return "LiteLLM_DailyEndUserSpend"
}

func (m *LiteLLMDailyEndUserSpend) BeforeCreate(tx *gorm.DB) error {
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

func (*LiteLLMDailyEndUserSpend) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LiteLLMDailyAgentSpend struct {
	ID                                 string   `gorm:"column:id;primaryKey;type:char(36)" json:"id"`
	AgentID                            *string  `gorm:"column:agent_id;uniqueIndex:LiteLLM_DailyAgentSpend_agent_id_date_api_key_model_custom_llm_provider_mcp_namespaced_tool_name_endpoint_key;index:LiteLLM_DailyAgentSpend_agent_id_date_idx" json:"agent_id"`
	Date                               string   `gorm:"column:date;uniqueIndex:LiteLLM_DailyAgentSpend_agent_id_date_api_key_model_custom_llm_provider_mcp_namespaced_tool_name_endpoint_key;index:LiteLLM_DailyAgentSpend_date_idx;index:LiteLLM_DailyAgentSpend_agent_id_date_idx;not null" json:"date"`
	APIKey                             string   `gorm:"column:api_key;uniqueIndex:LiteLLM_DailyAgentSpend_agent_id_date_api_key_model_custom_llm_provider_mcp_namespaced_tool_name_endpoint_key;index:LiteLLM_DailyAgentSpend_api_key_idx;not null" json:"api_key"`
	Model                              *string  `gorm:"column:model;uniqueIndex:LiteLLM_DailyAgentSpend_agent_id_date_api_key_model_custom_llm_provider_mcp_namespaced_tool_name_endpoint_key;index:LiteLLM_DailyAgentSpend_model_idx" json:"model"`
	ModelGroup                         *string  `gorm:"column:model_group" json:"model_group"`
	CustomLlmProvider                  *string  `gorm:"column:custom_llm_provider;uniqueIndex:LiteLLM_DailyAgentSpend_agent_id_date_api_key_model_custom_llm_provider_mcp_namespaced_tool_name_endpoint_key" json:"custom_llm_provider"`
	McpNamespacedToolName              *string  `gorm:"column:mcp_namespaced_tool_name;uniqueIndex:LiteLLM_DailyAgentSpend_agent_id_date_api_key_model_custom_llm_provider_mcp_namespaced_tool_name_endpoint_key;index:LiteLLM_DailyAgentSpend_mcp_namespaced_tool_name_idx" json:"mcp_namespaced_tool_name"`
	Endpoint                           *string  `gorm:"column:endpoint;uniqueIndex:LiteLLM_DailyAgentSpend_agent_id_date_api_key_model_custom_llm_provider_mcp_namespaced_tool_name_endpoint_key;index:LiteLLM_DailyAgentSpend_endpoint_idx" json:"endpoint"`
	PromptTokens                       int64    `gorm:"column:prompt_tokens;default:0;not null" json:"prompt_tokens"`
	CompletionTokens                   int64    `gorm:"column:completion_tokens;default:0;not null" json:"completion_tokens"`
	CacheReadInputTokens               int64    `gorm:"column:cache_read_input_tokens;default:0;not null" json:"cache_read_input_tokens"`
	CacheCreationInputTokens           int64    `gorm:"column:cache_creation_input_tokens;default:0;not null" json:"cache_creation_input_tokens"`
	CompressionSavedTokens             int64    `gorm:"column:compression_saved_tokens;default:0;not null" json:"compression_saved_tokens"`
	CompressionSavingsSpend            float64  `gorm:"column:compression_savings_spend;default:0;not null" json:"compression_savings_spend"`
	PromptCachingSavingsSpend          float64  `gorm:"column:prompt_caching_savings_spend;default:0;not null" json:"prompt_caching_savings_spend"`
	GatewayInjectedCachingSavingsSpend float64  `gorm:"column:gateway_injected_caching_savings_spend;default:0;not null" json:"gateway_injected_caching_savings_spend"`
	AutorouterSavingsSpend             float64  `gorm:"column:autorouter_savings_spend;default:0;not null" json:"autorouter_savings_spend"`
	GuardrailSpend                     float64  `gorm:"column:guardrail_spend;default:0;not null" json:"guardrail_spend"`
	Spend                              float64  `gorm:"column:spend;default:0;not null" json:"spend"`
	APIRequests                        int64    `gorm:"column:api_requests;default:0;not null" json:"api_requests"`
	SuccessfulRequests                 int64    `gorm:"column:successful_requests;default:0;not null" json:"successful_requests"`
	FailedRequests                     int64    `gorm:"column:failed_requests;default:0;not null" json:"failed_requests"`
	TotalResponseTimeMs                int64    `gorm:"column:total_response_time_ms;default:0;not null" json:"total_response_time_ms"`
	TimedRequests                      int64    `gorm:"column:timed_requests;default:0;not null" json:"timed_requests"`
	CreatedAt                          DateTime `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt                          DateTime `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
}

func (LiteLLMDailyAgentSpend) TableName() string {
	return "LiteLLM_DailyAgentSpend"
}

func (m *LiteLLMDailyAgentSpend) BeforeCreate(tx *gorm.DB) error {
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

func (*LiteLLMDailyAgentSpend) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LiteLLMDailyTeamSpend struct {
	ID                                 string   `gorm:"column:id;primaryKey;type:char(36)" json:"id"`
	TeamID                             *string  `gorm:"column:team_id;uniqueIndex:LiteLLM_DailyTeamSpend_team_id_date_api_key_model_custom_llm_provider_mcp_namespaced_tool_name_endpoint_key;index:LiteLLM_DailyTeamSpend_team_id_date_idx" json:"team_id"`
	Date                               string   `gorm:"column:date;uniqueIndex:LiteLLM_DailyTeamSpend_team_id_date_api_key_model_custom_llm_provider_mcp_namespaced_tool_name_endpoint_key;index:LiteLLM_DailyTeamSpend_date_idx;index:LiteLLM_DailyTeamSpend_team_id_date_idx;not null" json:"date"`
	APIKey                             string   `gorm:"column:api_key;uniqueIndex:LiteLLM_DailyTeamSpend_team_id_date_api_key_model_custom_llm_provider_mcp_namespaced_tool_name_endpoint_key;index:LiteLLM_DailyTeamSpend_api_key_idx;not null" json:"api_key"`
	Model                              *string  `gorm:"column:model;uniqueIndex:LiteLLM_DailyTeamSpend_team_id_date_api_key_model_custom_llm_provider_mcp_namespaced_tool_name_endpoint_key;index:LiteLLM_DailyTeamSpend_model_idx" json:"model"`
	ModelGroup                         *string  `gorm:"column:model_group" json:"model_group"`
	CustomLlmProvider                  *string  `gorm:"column:custom_llm_provider;uniqueIndex:LiteLLM_DailyTeamSpend_team_id_date_api_key_model_custom_llm_provider_mcp_namespaced_tool_name_endpoint_key" json:"custom_llm_provider"`
	McpNamespacedToolName              *string  `gorm:"column:mcp_namespaced_tool_name;uniqueIndex:LiteLLM_DailyTeamSpend_team_id_date_api_key_model_custom_llm_provider_mcp_namespaced_tool_name_endpoint_key;index:LiteLLM_DailyTeamSpend_mcp_namespaced_tool_name_idx" json:"mcp_namespaced_tool_name"`
	Endpoint                           *string  `gorm:"column:endpoint;uniqueIndex:LiteLLM_DailyTeamSpend_team_id_date_api_key_model_custom_llm_provider_mcp_namespaced_tool_name_endpoint_key;index:LiteLLM_DailyTeamSpend_endpoint_idx" json:"endpoint"`
	PromptTokens                       int64    `gorm:"column:prompt_tokens;default:0;not null" json:"prompt_tokens"`
	CompletionTokens                   int64    `gorm:"column:completion_tokens;default:0;not null" json:"completion_tokens"`
	CacheReadInputTokens               int64    `gorm:"column:cache_read_input_tokens;default:0;not null" json:"cache_read_input_tokens"`
	CacheCreationInputTokens           int64    `gorm:"column:cache_creation_input_tokens;default:0;not null" json:"cache_creation_input_tokens"`
	CompressionSavedTokens             int64    `gorm:"column:compression_saved_tokens;default:0;not null" json:"compression_saved_tokens"`
	CompressionSavingsSpend            float64  `gorm:"column:compression_savings_spend;default:0;not null" json:"compression_savings_spend"`
	PromptCachingSavingsSpend          float64  `gorm:"column:prompt_caching_savings_spend;default:0;not null" json:"prompt_caching_savings_spend"`
	GatewayInjectedCachingSavingsSpend float64  `gorm:"column:gateway_injected_caching_savings_spend;default:0;not null" json:"gateway_injected_caching_savings_spend"`
	AutorouterSavingsSpend             float64  `gorm:"column:autorouter_savings_spend;default:0;not null" json:"autorouter_savings_spend"`
	GuardrailSpend                     float64  `gorm:"column:guardrail_spend;default:0;not null" json:"guardrail_spend"`
	Spend                              float64  `gorm:"column:spend;default:0;not null" json:"spend"`
	APIRequests                        int64    `gorm:"column:api_requests;default:0;not null" json:"api_requests"`
	SuccessfulRequests                 int64    `gorm:"column:successful_requests;default:0;not null" json:"successful_requests"`
	FailedRequests                     int64    `gorm:"column:failed_requests;default:0;not null" json:"failed_requests"`
	TotalResponseTimeMs                int64    `gorm:"column:total_response_time_ms;default:0;not null" json:"total_response_time_ms"`
	TimedRequests                      int64    `gorm:"column:timed_requests;default:0;not null" json:"timed_requests"`
	PtuFlatCost                        float64  `gorm:"column:ptu_flat_cost;default:0;not null" json:"ptu_flat_cost"`
	CreatedAt                          DateTime `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt                          DateTime `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
}

func (LiteLLMDailyTeamSpend) TableName() string {
	return "LiteLLM_DailyTeamSpend"
}

func (m *LiteLLMDailyTeamSpend) BeforeCreate(tx *gorm.DB) error {
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

func (*LiteLLMDailyTeamSpend) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LiteLLMDailyTagSpend struct {
	ID                                 string   `gorm:"column:id;primaryKey;type:char(36)" json:"id"`
	RequestID                          *string  `gorm:"column:request_id" json:"request_id"`
	Tag                                *string  `gorm:"column:tag;uniqueIndex:LiteLLM_DailyTagSpend_tag_date_api_key_model_custom_llm_provider_mcp_namespaced_tool_name_endpoint_key;index:LiteLLM_DailyTagSpend_tag_date_idx" json:"tag"`
	Date                               string   `gorm:"column:date;uniqueIndex:LiteLLM_DailyTagSpend_tag_date_api_key_model_custom_llm_provider_mcp_namespaced_tool_name_endpoint_key;index:LiteLLM_DailyTagSpend_date_idx;index:LiteLLM_DailyTagSpend_tag_date_idx;not null" json:"date"`
	APIKey                             string   `gorm:"column:api_key;uniqueIndex:LiteLLM_DailyTagSpend_tag_date_api_key_model_custom_llm_provider_mcp_namespaced_tool_name_endpoint_key;index:LiteLLM_DailyTagSpend_api_key_idx;not null" json:"api_key"`
	Model                              *string  `gorm:"column:model;uniqueIndex:LiteLLM_DailyTagSpend_tag_date_api_key_model_custom_llm_provider_mcp_namespaced_tool_name_endpoint_key;index:LiteLLM_DailyTagSpend_model_idx" json:"model"`
	ModelGroup                         *string  `gorm:"column:model_group" json:"model_group"`
	CustomLlmProvider                  *string  `gorm:"column:custom_llm_provider;uniqueIndex:LiteLLM_DailyTagSpend_tag_date_api_key_model_custom_llm_provider_mcp_namespaced_tool_name_endpoint_key" json:"custom_llm_provider"`
	McpNamespacedToolName              *string  `gorm:"column:mcp_namespaced_tool_name;uniqueIndex:LiteLLM_DailyTagSpend_tag_date_api_key_model_custom_llm_provider_mcp_namespaced_tool_name_endpoint_key;index:LiteLLM_DailyTagSpend_mcp_namespaced_tool_name_idx" json:"mcp_namespaced_tool_name"`
	Endpoint                           *string  `gorm:"column:endpoint;uniqueIndex:LiteLLM_DailyTagSpend_tag_date_api_key_model_custom_llm_provider_mcp_namespaced_tool_name_endpoint_key;index:LiteLLM_DailyTagSpend_endpoint_idx" json:"endpoint"`
	PromptTokens                       int64    `gorm:"column:prompt_tokens;default:0;not null" json:"prompt_tokens"`
	CompletionTokens                   int64    `gorm:"column:completion_tokens;default:0;not null" json:"completion_tokens"`
	CacheReadInputTokens               int64    `gorm:"column:cache_read_input_tokens;default:0;not null" json:"cache_read_input_tokens"`
	CacheCreationInputTokens           int64    `gorm:"column:cache_creation_input_tokens;default:0;not null" json:"cache_creation_input_tokens"`
	CompressionSavedTokens             int64    `gorm:"column:compression_saved_tokens;default:0;not null" json:"compression_saved_tokens"`
	CompressionSavingsSpend            float64  `gorm:"column:compression_savings_spend;default:0;not null" json:"compression_savings_spend"`
	PromptCachingSavingsSpend          float64  `gorm:"column:prompt_caching_savings_spend;default:0;not null" json:"prompt_caching_savings_spend"`
	GatewayInjectedCachingSavingsSpend float64  `gorm:"column:gateway_injected_caching_savings_spend;default:0;not null" json:"gateway_injected_caching_savings_spend"`
	AutorouterSavingsSpend             float64  `gorm:"column:autorouter_savings_spend;default:0;not null" json:"autorouter_savings_spend"`
	GuardrailSpend                     float64  `gorm:"column:guardrail_spend;default:0;not null" json:"guardrail_spend"`
	Spend                              float64  `gorm:"column:spend;default:0;not null" json:"spend"`
	APIRequests                        int64    `gorm:"column:api_requests;default:0;not null" json:"api_requests"`
	SuccessfulRequests                 int64    `gorm:"column:successful_requests;default:0;not null" json:"successful_requests"`
	FailedRequests                     int64    `gorm:"column:failed_requests;default:0;not null" json:"failed_requests"`
	TotalResponseTimeMs                int64    `gorm:"column:total_response_time_ms;default:0;not null" json:"total_response_time_ms"`
	TimedRequests                      int64    `gorm:"column:timed_requests;default:0;not null" json:"timed_requests"`
	CreatedAt                          DateTime `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt                          DateTime `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
}

func (LiteLLMDailyTagSpend) TableName() string {
	return "LiteLLM_DailyTagSpend"
}

func (m *LiteLLMDailyTagSpend) BeforeCreate(tx *gorm.DB) error {
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

func (*LiteLLMDailyTagSpend) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LiteLLMProxyWorkerHeartbeat struct {
	WorkerID        string   `gorm:"column:worker_id;primaryKey" json:"worker_id"`
	Hostname        string   `gorm:"column:hostname;not null" json:"hostname"`
	StartedAt       DateTime `gorm:"column:started_at;not null" json:"started_at"`
	LastHeartbeatAt DateTime `gorm:"column:last_heartbeat_at;not null" json:"last_heartbeat_at"`
}

func (LiteLLMProxyWorkerHeartbeat) TableName() string {
	return "LiteLLM_ProxyWorkerHeartbeat"
}

func (m *LiteLLMProxyWorkerHeartbeat) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.StartedAt.IsZero() {
		m.StartedAt = DateTime{Time: now}
	}
	if m.LastHeartbeatAt.IsZero() {
		m.LastHeartbeatAt = DateTime{Time: now}
	}
	return nil
}

type LiteLLMCronJob struct {
	CronjobID   string   `gorm:"column:cronjob_id;primaryKey" json:"cronjob_id"`
	PodID       string   `gorm:"column:pod_id;not null" json:"pod_id"`
	Status      string   `gorm:"column:status;default:'INACTIVE';not null" json:"status"`
	LastUpdated DateTime `gorm:"column:last_updated;not null" json:"last_updated"`
	TTL         DateTime `gorm:"column:ttl;not null" json:"ttl"`
}

func (LiteLLMCronJob) TableName() string {
	return "LiteLLM_CronJob"
}

func (m *LiteLLMCronJob) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CronjobID == "" {
		m.CronjobID = cuid.New()
	}
	if m.LastUpdated.IsZero() {
		m.LastUpdated = DateTime{Time: now}
	}
	return nil
}

type LiteLLMScheduledJobRun struct {
	ID             string    `gorm:"column:id;primaryKey;type:char(36)" json:"id"`
	JobName        string    `gorm:"column:job_name;index:LiteLLM_ScheduledJobRun_job_name_status_idx;not null" json:"job_name"`
	LockGroup      string    `gorm:"column:lock_group;index:LiteLLM_ScheduledJobRun_lock_group_status_idx;not null" json:"lock_group"`
	Schedule       string    `gorm:"column:schedule;not null" json:"schedule"`
	ScheduledFor   DateTime  `gorm:"column:scheduled_for;type:timestamptz(3);not null" json:"scheduled_for"`
	Status         string    `gorm:"column:status;index:LiteLLM_ScheduledJobRun_job_name_status_idx;index:LiteLLM_ScheduledJobRun_lock_group_status_idx;default:'PENDING';not null" json:"status"`
	ClaimedBy      *string   `gorm:"column:claimed_by" json:"claimed_by"`
	ClaimedAt      *DateTime `gorm:"column:claimed_at;type:timestamptz(3)" json:"claimed_at"`
	LeaseExpiresAt *DateTime `gorm:"column:lease_expires_at;type:timestamptz(3)" json:"lease_expires_at"`
	FinishedAt     *DateTime `gorm:"column:finished_at;type:timestamptz(3)" json:"finished_at"`
	CreatedAt      DateTime  `gorm:"column:created_at;type:timestamptz(3);autoCreateTime:false;not null" json:"created_at"`
}

func (LiteLLMScheduledJobRun) TableName() string {
	return "LiteLLM_ScheduledJobRun"
}

func (m *LiteLLMScheduledJobRun) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = uuid.NewString()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type LiteLLMManagedFileTable struct {
	ID               string         `gorm:"column:id;primaryKey;type:char(36)" json:"id"`
	UnifiedFileID    string         `gorm:"column:unified_file_id;uniqueIndex;index:LiteLLM_ManagedFileTable_unified_file_id_idx;not null" json:"unified_file_id"`
	FileObject       datatypes.JSON `gorm:"column:file_object" json:"file_object"`
	ModelMappings    datatypes.JSON `gorm:"column:model_mappings;not null" json:"model_mappings"`
	FlatModelFileIds []string       `gorm:"column:flat_model_file_ids;index:LiteLLM_ManagedFileTable_flat_model_file_ids_idx;serializer:json;not null" json:"flat_model_file_ids"`
	StorageBackend   *string        `gorm:"column:storage_backend" json:"storage_backend"`
	StorageURL       *string        `gorm:"column:storage_url" json:"storage_url"`
	CreatedAt        DateTime       `gorm:"column:created_at;index:LiteLLM_ManagedFileTable_team_id_created_at_idx;index:LiteLLM_ManagedFileTable_created_by_created_at_idx;autoCreateTime:false;not null" json:"created_at"`
	CreatedBy        *string        `gorm:"column:created_by;index:LiteLLM_ManagedFileTable_created_by_created_at_idx" json:"created_by"`
	TeamID           *string        `gorm:"column:team_id;index:LiteLLM_ManagedFileTable_team_id_created_at_idx" json:"team_id"`
	UpdatedAt        DateTime       `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	UpdatedBy        *string        `gorm:"column:updated_by" json:"updated_by"`
}

func (LiteLLMManagedFileTable) TableName() string {
	return "LiteLLM_ManagedFileTable"
}

func (m *LiteLLMManagedFileTable) BeforeCreate(tx *gorm.DB) error {
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

func (*LiteLLMManagedFileTable) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LiteLLMManagedObjectTable struct {
	ID              string         `gorm:"column:id;primaryKey;type:char(36)" json:"id"`
	UnifiedObjectID string         `gorm:"column:unified_object_id;uniqueIndex;index:LiteLLM_ManagedObjectTable_unified_object_id_idx;not null" json:"unified_object_id"`
	ModelObjectID   string         `gorm:"column:model_object_id;uniqueIndex;index:LiteLLM_ManagedObjectTable_model_object_id_idx;not null" json:"model_object_id"`
	FileObject      datatypes.JSON `gorm:"column:file_object;not null" json:"file_object"`
	FilePurpose     string         `gorm:"column:file_purpose;not null" json:"file_purpose"`
	Status          *string        `gorm:"column:status" json:"status"`
	BatchProcessed  bool           `gorm:"column:batch_processed;default:false;not null" json:"batch_processed"`
	CreatedAt       DateTime       `gorm:"column:created_at;index:LiteLLM_ManagedObjectTable_team_id_created_at_idx;autoCreateTime:false;not null" json:"created_at"`
	CreatedBy       *string        `gorm:"column:created_by" json:"created_by"`
	TeamID          *string        `gorm:"column:team_id;index:LiteLLM_ManagedObjectTable_team_id_created_at_idx" json:"team_id"`
	OrgID           *string        `gorm:"column:org_id" json:"org_id"`
	APIKey          *string        `gorm:"column:api_key" json:"api_key"`
	RequestTags     datatypes.JSON `gorm:"column:request_tags;default:'[]'" json:"request_tags"`
	EndUserID       *string        `gorm:"column:end_user_id" json:"end_user_id"`
	UpdatedAt       DateTime       `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	UpdatedBy       *string        `gorm:"column:updated_by" json:"updated_by"`
}

func (LiteLLMManagedObjectTable) TableName() string {
	return "LiteLLM_ManagedObjectTable"
}

func (m *LiteLLMManagedObjectTable) BeforeCreate(tx *gorm.DB) error {
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

func (*LiteLLMManagedObjectTable) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LiteLLMManagedFileContentTable struct {
	ID        string   `gorm:"column:id;primaryKey;type:char(36)" json:"id"`
	Content   []byte   `gorm:"column:content;not null" json:"content"`
	CreatedAt DateTime `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
}

func (LiteLLMManagedFileContentTable) TableName() string {
	return "LiteLLM_ManagedFileContentTable"
}

func (m *LiteLLMManagedFileContentTable) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = uuid.NewString()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type LiteLLMManagedVectorStoreTable struct {
	ID                   string         `gorm:"column:id;primaryKey;type:char(36)" json:"id"`
	UnifiedResourceID    string         `gorm:"column:unified_resource_id;uniqueIndex;index:LiteLLM_ManagedVectorStoreTable_unified_resource_id_idx;not null" json:"unified_resource_id"`
	ResourceObject       datatypes.JSON `gorm:"column:resource_object" json:"resource_object"`
	ModelMappings        datatypes.JSON `gorm:"column:model_mappings;not null" json:"model_mappings"`
	FlatModelResourceIds []string       `gorm:"column:flat_model_resource_ids;serializer:json;not null" json:"flat_model_resource_ids"`
	StorageBackend       *string        `gorm:"column:storage_backend" json:"storage_backend"`
	StorageURL           *string        `gorm:"column:storage_url" json:"storage_url"`
	CreatedAt            DateTime       `gorm:"column:created_at;index:LiteLLM_ManagedVectorStoreTable_team_id_created_at_idx;autoCreateTime:false;not null" json:"created_at"`
	CreatedBy            *string        `gorm:"column:created_by" json:"created_by"`
	TeamID               *string        `gorm:"column:team_id;index:LiteLLM_ManagedVectorStoreTable_team_id_created_at_idx" json:"team_id"`
	UpdatedAt            DateTime       `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	UpdatedBy            *string        `gorm:"column:updated_by" json:"updated_by"`
}

func (LiteLLMManagedVectorStoreTable) TableName() string {
	return "LiteLLM_ManagedVectorStoreTable"
}

func (m *LiteLLMManagedVectorStoreTable) BeforeCreate(tx *gorm.DB) error {
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

func (*LiteLLMManagedVectorStoreTable) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LiteLLMManagedVectorStoresTable struct {
	VectorStoreID          string         `gorm:"column:vector_store_id;primaryKey" json:"vector_store_id"`
	CustomLlmProvider      string         `gorm:"column:custom_llm_provider;not null" json:"custom_llm_provider"`
	VectorStoreName        *string        `gorm:"column:vector_store_name" json:"vector_store_name"`
	VectorStoreDescription *string        `gorm:"column:vector_store_description" json:"vector_store_description"`
	VectorStoreMetadata    datatypes.JSON `gorm:"column:vector_store_metadata" json:"vector_store_metadata"`
	CreatedAt              DateTime       `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt              DateTime       `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	LitellmCredentialName  *string        `gorm:"column:litellm_credential_name" json:"litellm_credential_name"`
	LitellmParams          datatypes.JSON `gorm:"column:litellm_params" json:"litellm_params"`
	TeamID                 *string        `gorm:"column:team_id;index:LiteLLM_ManagedVectorStoresTable_team_id_idx" json:"team_id"`
	UserID                 *string        `gorm:"column:user_id;index:LiteLLM_ManagedVectorStoresTable_user_id_idx" json:"user_id"`
}

func (LiteLLMManagedVectorStoresTable) TableName() string {
	return "LiteLLM_ManagedVectorStoresTable"
}

func (m *LiteLLMManagedVectorStoresTable) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*LiteLLMManagedVectorStoresTable) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LiteLLMGuardrailsTable struct {
	GuardrailID   string         `gorm:"column:guardrail_id;primaryKey;type:char(36)" json:"guardrail_id"`
	GuardrailName string         `gorm:"column:guardrail_name;uniqueIndex;not null" json:"guardrail_name"`
	LitellmParams datatypes.JSON `gorm:"column:litellm_params;not null" json:"litellm_params"`
	GuardrailInfo datatypes.JSON `gorm:"column:guardrail_info" json:"guardrail_info"`
	TeamID        *string        `gorm:"column:team_id" json:"team_id"`
	CreatedAt     DateTime       `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt     DateTime       `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	Status        *string        `gorm:"column:status;index:LiteLLM_GuardrailsTable_status_idx;default:'active';not null" json:"status"`
	SubmittedAt   *DateTime      `gorm:"column:submitted_at" json:"submitted_at"`
	ReviewedAt    *DateTime      `gorm:"column:reviewed_at" json:"reviewed_at"`
}

func (LiteLLMGuardrailsTable) TableName() string {
	return "LiteLLM_GuardrailsTable"
}

func (m *LiteLLMGuardrailsTable) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.GuardrailID == "" {
		m.GuardrailID = uuid.NewString()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*LiteLLMGuardrailsTable) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LiteLLMDailyGuardrailMetrics struct {
	GuardrailID       string   `gorm:"column:guardrail_id;primaryKey;index:LiteLLM_DailyGuardrailMetrics_guardrail_id_idx" json:"guardrail_id"`
	Date              string   `gorm:"column:date;primaryKey;index:LiteLLM_DailyGuardrailMetrics_date_idx" json:"date"`
	RequestsEvaluated int64    `gorm:"column:requests_evaluated;default:0;not null" json:"requests_evaluated"`
	PassedCount       int64    `gorm:"column:passed_count;default:0;not null" json:"passed_count"`
	BlockedCount      int64    `gorm:"column:blocked_count;default:0;not null" json:"blocked_count"`
	FlaggedCount      int64    `gorm:"column:flagged_count;default:0;not null" json:"flagged_count"`
	AvgScore          *float64 `gorm:"column:avg_score" json:"avg_score"`
	AvgLatencyMs      *float64 `gorm:"column:avg_latency_ms" json:"avg_latency_ms"`
	CreatedAt         DateTime `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt         DateTime `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
}

func (LiteLLMDailyGuardrailMetrics) TableName() string {
	return "LiteLLM_DailyGuardrailMetrics"
}

func (m *LiteLLMDailyGuardrailMetrics) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*LiteLLMDailyGuardrailMetrics) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LiteLLMDailyGuardrailUsageUnits struct {
	GuardrailID    string   `gorm:"column:guardrail_id;primaryKey" json:"guardrail_id"`
	Date           string   `gorm:"column:date;primaryKey;index:LiteLLM_DailyGuardrailUsageUnits_date_idx" json:"date"`
	TeamID         string   `gorm:"column:team_id;primaryKey" json:"team_id"`
	APIKey         string   `gorm:"column:api_key;primaryKey" json:"api_key"`
	UsageUnit      string   `gorm:"column:usage_unit;primaryKey" json:"usage_unit"`
	Units          int64    `gorm:"column:units;default:0;not null" json:"units"`
	Cost           *float64 `gorm:"column:cost" json:"cost"`
	UntrackedUnits int64    `gorm:"column:untracked_units;default:0;not null" json:"untracked_units"`
	CreatedAt      DateTime `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt      DateTime `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
}

func (LiteLLMDailyGuardrailUsageUnits) TableName() string {
	return "LiteLLM_DailyGuardrailUsageUnits"
}

func (m *LiteLLMDailyGuardrailUsageUnits) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*LiteLLMDailyGuardrailUsageUnits) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LiteLLMDailyPolicyMetrics struct {
	PolicyID          string   `gorm:"column:policy_id;primaryKey;index:LiteLLM_DailyPolicyMetrics_policy_id_idx" json:"policy_id"`
	Date              string   `gorm:"column:date;primaryKey;index:LiteLLM_DailyPolicyMetrics_date_idx" json:"date"`
	RequestsEvaluated int64    `gorm:"column:requests_evaluated;default:0;not null" json:"requests_evaluated"`
	PassedCount       int64    `gorm:"column:passed_count;default:0;not null" json:"passed_count"`
	BlockedCount      int64    `gorm:"column:blocked_count;default:0;not null" json:"blocked_count"`
	FlaggedCount      int64    `gorm:"column:flagged_count;default:0;not null" json:"flagged_count"`
	AvgScore          *float64 `gorm:"column:avg_score" json:"avg_score"`
	AvgLatencyMs      *float64 `gorm:"column:avg_latency_ms" json:"avg_latency_ms"`
	CreatedAt         DateTime `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt         DateTime `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
}

func (LiteLLMDailyPolicyMetrics) TableName() string {
	return "LiteLLM_DailyPolicyMetrics"
}

func (m *LiteLLMDailyPolicyMetrics) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*LiteLLMDailyPolicyMetrics) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LiteLLMSpendLogGuardrailIndex struct {
	RequestID   string   `gorm:"column:request_id;primaryKey" json:"request_id"`
	GuardrailID string   `gorm:"column:guardrail_id;primaryKey;index:LiteLLM_SpendLogGuardrailIndex_guardrail_id_start_time_idx" json:"guardrail_id"`
	PolicyID    *string  `gorm:"column:policy_id;index:LiteLLM_SpendLogGuardrailIndex_policy_id_start_time_idx" json:"policy_id"`
	StartTime   DateTime `gorm:"column:start_time;index:LiteLLM_SpendLogGuardrailIndex_guardrail_id_start_time_idx;index:LiteLLM_SpendLogGuardrailIndex_policy_id_start_time_idx;not null" json:"start_time"`
}

func (LiteLLMSpendLogGuardrailIndex) TableName() string {
	return "LiteLLM_SpendLogGuardrailIndex"
}

type LiteLLMSpendLogToolIndex struct {
	RequestID string   `gorm:"column:request_id;primaryKey" json:"request_id"`
	ToolName  string   `gorm:"column:tool_name;primaryKey;index:LiteLLM_SpendLogToolIndex_tool_name_start_time_idx" json:"tool_name"`
	StartTime DateTime `gorm:"column:start_time;index:LiteLLM_SpendLogToolIndex_tool_name_start_time_idx;index:LiteLLM_SpendLogToolIndex_start_time_idx;not null" json:"start_time"`
}

func (LiteLLMSpendLogToolIndex) TableName() string {
	return "LiteLLM_SpendLogToolIndex"
}

type LiteLLMDailyToolSpend struct {
	Date         string   `gorm:"column:date;primaryKey" json:"date"`
	ToolName     string   `gorm:"column:tool_name;primaryKey" json:"tool_name"`
	Spend        float64  `gorm:"column:spend;default:0;not null" json:"spend"`
	TotalTokens  int64    `gorm:"column:total_tokens;default:0;not null" json:"total_tokens"`
	RequestCount int64    `gorm:"column:request_count;default:0;not null" json:"request_count"`
	CreatedAt    DateTime `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt    DateTime `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
}

func (LiteLLMDailyToolSpend) TableName() string {
	return "LiteLLM_DailyToolSpend"
}

func (m *LiteLLMDailyToolSpend) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*LiteLLMDailyToolSpend) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LiteLLMDailyModelUsage struct {
	Date                string   `gorm:"column:date;primaryKey;index:LiteLLM_DailyModelUsage_date_idx" json:"date"`
	ModelGroup          string   `gorm:"column:model_group;primaryKey;index:LiteLLM_DailyModelUsage_model_group_idx" json:"model_group"`
	Model               string   `gorm:"column:model;primaryKey" json:"model"`
	CustomLlmProvider   string   `gorm:"column:custom_llm_provider;primaryKey" json:"custom_llm_provider"`
	TaskType            string   `gorm:"column:task_type;primaryKey" json:"task_type"`
	Spend               float64  `gorm:"column:spend;default:0;not null" json:"spend"`
	PromptTokens        int64    `gorm:"column:prompt_tokens;default:0;not null" json:"prompt_tokens"`
	CompletionTokens    int64    `gorm:"column:completion_tokens;default:0;not null" json:"completion_tokens"`
	RequestCount        int64    `gorm:"column:request_count;default:0;not null" json:"request_count"`
	SuccessfulRequests  int64    `gorm:"column:successful_requests;default:0;not null" json:"successful_requests"`
	FailedRequests      int64    `gorm:"column:failed_requests;default:0;not null" json:"failed_requests"`
	TotalResponseTimeMs int64    `gorm:"column:total_response_time_ms;default:0;not null" json:"total_response_time_ms"`
	TimedRequests       int64    `gorm:"column:timed_requests;default:0;not null" json:"timed_requests"`
	CreatedAt           DateTime `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt           DateTime `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
}

func (LiteLLMDailyModelUsage) TableName() string {
	return "LiteLLM_DailyModelUsage"
}

func (m *LiteLLMDailyModelUsage) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*LiteLLMDailyModelUsage) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LiteLLMDailyGatewayRequests struct {
	Date               string   `gorm:"column:date;primaryKey;index:LiteLLM_DailyGatewayRequests_date_idx" json:"date"`
	Category           string   `gorm:"column:category;primaryKey" json:"category"`
	Route              string   `gorm:"column:route;primaryKey" json:"route"`
	SuccessfulRequests int64    `gorm:"column:successful_requests;default:0;not null" json:"successful_requests"`
	FailedRequests     int64    `gorm:"column:failed_requests;default:0;not null" json:"failed_requests"`
	CreatedAt          DateTime `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt          DateTime `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
}

func (LiteLLMDailyGatewayRequests) TableName() string {
	return "LiteLLM_DailyGatewayRequests"
}

func (m *LiteLLMDailyGatewayRequests) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*LiteLLMDailyGatewayRequests) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LiteLLMDailyGatewayFailedRequests struct {
	Date           string   `gorm:"column:date;primaryKey;index:LiteLLM_DailyGatewayFailedRequests_date_idx" json:"date"`
	Category       string   `gorm:"column:category;primaryKey" json:"category"`
	Route          string   `gorm:"column:route;primaryKey" json:"route"`
	StatusCode     int      `gorm:"column:status_code;primaryKey" json:"status_code"`
	FailedRequests int64    `gorm:"column:failed_requests;default:0;not null" json:"failed_requests"`
	CreatedAt      DateTime `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt      DateTime `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
}

func (LiteLLMDailyGatewayFailedRequests) TableName() string {
	return "LiteLLM_DailyGatewayFailedRequests"
}

func (m *LiteLLMDailyGatewayFailedRequests) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*LiteLLMDailyGatewayFailedRequests) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LiteLLMDailyRequestErrors struct {
	Date           string   `gorm:"column:date;primaryKey;index:LiteLLM_DailyRequestErrors_date_idx" json:"date"`
	APIKey         string   `gorm:"column:api_key;primaryKey" json:"api_key"`
	TeamID         string   `gorm:"column:team_id;primaryKey;default:''" json:"team_id"`
	UserID         string   `gorm:"column:user_id;primaryKey;default:''" json:"user_id"`
	ModelGroup     string   `gorm:"column:model_group;primaryKey;default:''" json:"model_group"`
	StatusCode     int      `gorm:"column:status_code;primaryKey" json:"status_code"`
	FailedRequests int64    `gorm:"column:failed_requests;default:0;not null" json:"failed_requests"`
	CreatedAt      DateTime `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt      DateTime `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
}

func (LiteLLMDailyRequestErrors) TableName() string {
	return "LiteLLM_DailyRequestErrors"
}

func (m *LiteLLMDailyRequestErrors) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*LiteLLMDailyRequestErrors) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LiteLLMPromptTable struct {
	ID            string         `gorm:"column:id;primaryKey;type:char(36)" json:"id"`
	PromptID      string         `gorm:"column:prompt_id;uniqueIndex:LiteLLM_PromptTable_prompt_id_version_environment_key;index:LiteLLM_PromptTable_prompt_id_environment_idx;index:LiteLLM_PromptTable_prompt_id_idx;not null" json:"prompt_id"`
	Version       *int           `gorm:"column:version;uniqueIndex:LiteLLM_PromptTable_prompt_id_version_environment_key;default:1;not null" json:"version"`
	Environment   *string        `gorm:"column:environment;uniqueIndex:LiteLLM_PromptTable_prompt_id_version_environment_key;index:LiteLLM_PromptTable_prompt_id_environment_idx;default:'development';not null" json:"environment"`
	CreatedBy     *string        `gorm:"column:created_by" json:"created_by"`
	LitellmParams datatypes.JSON `gorm:"column:litellm_params;not null" json:"litellm_params"`
	PromptInfo    datatypes.JSON `gorm:"column:prompt_info" json:"prompt_info"`
	CreatedAt     DateTime       `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt     DateTime       `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
}

func (LiteLLMPromptTable) TableName() string {
	return "LiteLLM_PromptTable"
}

func (m *LiteLLMPromptTable) BeforeCreate(tx *gorm.DB) error {
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

func (*LiteLLMPromptTable) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LiteLLMHealthCheckTable struct {
	HealthCheckID  string         `gorm:"column:health_check_id;primaryKey;type:char(36)" json:"health_check_id"`
	ModelName      string         `gorm:"column:model_name;index:LiteLLM_HealthCheckTable_model_name_idx;index:LiteLLM_HealthCheckTable_model_id_model_name_checked_at_idx;not null" json:"model_name"`
	ModelID        *string        `gorm:"column:model_id;index:LiteLLM_HealthCheckTable_model_id_model_name_checked_at_idx" json:"model_id"`
	Status         string         `gorm:"column:status;index:LiteLLM_HealthCheckTable_status_idx;not null" json:"status"`
	HealthyCount   int            `gorm:"column:healthy_count;default:0;not null" json:"healthy_count"`
	UnhealthyCount int            `gorm:"column:unhealthy_count;default:0;not null" json:"unhealthy_count"`
	ErrorMessage   *string        `gorm:"column:error_message" json:"error_message"`
	ResponseTimeMs *float64       `gorm:"column:response_time_ms" json:"response_time_ms"`
	Details        datatypes.JSON `gorm:"column:details" json:"details"`
	CheckedBy      *string        `gorm:"column:checked_by" json:"checked_by"`
	CheckedAt      DateTime       `gorm:"column:checked_at;index:LiteLLM_HealthCheckTable_checked_at_idx;index:LiteLLM_HealthCheckTable_model_id_model_name_checked_at_idx;not null" json:"checked_at"`
	CreatedAt      DateTime       `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt      DateTime       `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
}

func (LiteLLMHealthCheckTable) TableName() string {
	return "LiteLLM_HealthCheckTable"
}

func (m *LiteLLMHealthCheckTable) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.HealthCheckID == "" {
		m.HealthCheckID = uuid.NewString()
	}
	if m.CheckedAt.IsZero() {
		m.CheckedAt = DateTime{Time: now}
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*LiteLLMHealthCheckTable) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LiteLLMSearchToolsTable struct {
	SearchToolID   string         `gorm:"column:search_tool_id;primaryKey;type:char(36)" json:"search_tool_id"`
	SearchToolName string         `gorm:"column:search_tool_name;uniqueIndex;not null" json:"search_tool_name"`
	LitellmParams  datatypes.JSON `gorm:"column:litellm_params;not null" json:"litellm_params"`
	SearchToolInfo datatypes.JSON `gorm:"column:search_tool_info" json:"search_tool_info"`
	CreatedAt      DateTime       `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt      DateTime       `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
}

func (LiteLLMSearchToolsTable) TableName() string {
	return "LiteLLM_SearchToolsTable"
}

func (m *LiteLLMSearchToolsTable) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.SearchToolID == "" {
		m.SearchToolID = uuid.NewString()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*LiteLLMSearchToolsTable) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LiteLLMSSOConfig struct {
	ID          string         `gorm:"column:id;primaryKey" json:"id"`
	SsoSettings datatypes.JSON `gorm:"column:sso_settings;not null" json:"sso_settings"`
	CreatedAt   DateTime       `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt   DateTime       `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
}

func (LiteLLMSSOConfig) TableName() string {
	return "LiteLLM_SSOConfig"
}

func (m *LiteLLMSSOConfig) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*LiteLLMSSOConfig) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LiteLLMManagedVectorStoreIndexTable struct {
	ID            string         `gorm:"column:id;primaryKey;type:char(36)" json:"id"`
	IndexName     string         `gorm:"column:index_name;uniqueIndex;not null" json:"index_name"`
	LitellmParams datatypes.JSON `gorm:"column:litellm_params;not null" json:"litellm_params"`
	IndexInfo     datatypes.JSON `gorm:"column:index_info" json:"index_info"`
	CreatedAt     DateTime       `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	CreatedBy     *string        `gorm:"column:created_by" json:"created_by"`
	UpdatedAt     DateTime       `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	UpdatedBy     *string        `gorm:"column:updated_by" json:"updated_by"`
}

func (LiteLLMManagedVectorStoreIndexTable) TableName() string {
	return "LiteLLM_ManagedVectorStoreIndexTable"
}

func (m *LiteLLMManagedVectorStoreIndexTable) BeforeCreate(tx *gorm.DB) error {
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

func (*LiteLLMManagedVectorStoreIndexTable) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LiteLLMCacheConfig struct {
	ID            string         `gorm:"column:id;primaryKey" json:"id"`
	CacheSettings datatypes.JSON `gorm:"column:cache_settings;not null" json:"cache_settings"`
	CreatedAt     DateTime       `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt     DateTime       `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
}

func (LiteLLMCacheConfig) TableName() string {
	return "LiteLLM_CacheConfig"
}

func (m *LiteLLMCacheConfig) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*LiteLLMCacheConfig) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LiteLLMUISettings struct {
	ID         string         `gorm:"column:id;primaryKey" json:"id"`
	UISettings datatypes.JSON `gorm:"column:ui_settings;not null" json:"ui_settings"`
	CreatedAt  DateTime       `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt  DateTime       `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
}

func (LiteLLMUISettings) TableName() string {
	return "LiteLLM_UISettings"
}

func (m *LiteLLMUISettings) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*LiteLLMUISettings) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LiteLLMConfigOverrides struct {
	ConfigType  string         `gorm:"column:config_type;primaryKey" json:"config_type"`
	ConfigValue datatypes.JSON `gorm:"column:config_value;not null" json:"config_value"`
	CreatedAt   DateTime       `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt   DateTime       `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
}

func (LiteLLMConfigOverrides) TableName() string {
	return "LiteLLM_ConfigOverrides"
}

func (m *LiteLLMConfigOverrides) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*LiteLLMConfigOverrides) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LiteLLMSkillsTable struct {
	SkillID       string         `gorm:"column:skill_id;primaryKey;type:char(36)" json:"skill_id"`
	DisplayTitle  *string        `gorm:"column:display_title" json:"display_title"`
	Description   *string        `gorm:"column:description" json:"description"`
	Instructions  *string        `gorm:"column:instructions" json:"instructions"`
	Source        *string        `gorm:"column:source;default:'custom';not null" json:"source"`
	LatestVersion *string        `gorm:"column:latest_version" json:"latest_version"`
	FileContent   []byte         `gorm:"column:file_content" json:"file_content"`
	FileName      *string        `gorm:"column:file_name" json:"file_name"`
	FileType      *string        `gorm:"column:file_type" json:"file_type"`
	Metadata      datatypes.JSON `gorm:"column:metadata;default:'{}'" json:"metadata"`
	CreatedAt     DateTime       `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	CreatedBy     *string        `gorm:"column:created_by" json:"created_by"`
	UpdatedAt     DateTime       `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	UpdatedBy     *string        `gorm:"column:updated_by" json:"updated_by"`
}

func (LiteLLMSkillsTable) TableName() string {
	return "LiteLLM_SkillsTable"
}

func (m *LiteLLMSkillsTable) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.SkillID == "" {
		m.SkillID = uuid.NewString()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*LiteLLMSkillsTable) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LiteLLMPolicyTable struct {
	PolicyID         string         `gorm:"column:policy_id;primaryKey;type:char(36)" json:"policy_id"`
	PolicyName       string         `gorm:"column:policy_name;uniqueIndex:LiteLLM_PolicyTable_policy_name_version_number_key;index:LiteLLM_PolicyTable_policy_name_version_status_idx;not null" json:"policy_name"`
	VersionNumber    *int           `gorm:"column:version_number;uniqueIndex:LiteLLM_PolicyTable_policy_name_version_number_key;default:1;not null" json:"version_number"`
	VersionStatus    *string        `gorm:"column:version_status;index:LiteLLM_PolicyTable_policy_name_version_status_idx;default:'production';not null" json:"version_status"`
	ParentVersionID  *string        `gorm:"column:parent_version_id" json:"parent_version_id"`
	IsLatest         *bool          `gorm:"column:is_latest;default:true;not null" json:"is_latest"`
	PublishedAt      *DateTime      `gorm:"column:published_at" json:"published_at"`
	ProductionAt     *DateTime      `gorm:"column:production_at" json:"production_at"`
	Inherit          *string        `gorm:"column:inherit" json:"inherit"`
	Description      *string        `gorm:"column:description" json:"description"`
	GuardrailsAdd    []string       `gorm:"column:guardrails_add;serializer:json;not null" json:"guardrails_add"`
	GuardrailsRemove []string       `gorm:"column:guardrails_remove;serializer:json;not null" json:"guardrails_remove"`
	Condition        datatypes.JSON `gorm:"column:condition;default:'{}'" json:"condition"`
	Pipeline         datatypes.JSON `gorm:"column:pipeline" json:"pipeline"`
	CreatedAt        DateTime       `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	CreatedBy        *string        `gorm:"column:created_by" json:"created_by"`
	UpdatedAt        DateTime       `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	UpdatedBy        *string        `gorm:"column:updated_by" json:"updated_by"`
}

func (LiteLLMPolicyTable) TableName() string {
	return "LiteLLM_PolicyTable"
}

func (m *LiteLLMPolicyTable) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.PolicyID == "" {
		m.PolicyID = uuid.NewString()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*LiteLLMPolicyTable) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LiteLLMPolicyAttachmentTable struct {
	AttachmentID string   `gorm:"column:attachment_id;primaryKey;type:char(36)" json:"attachment_id"`
	PolicyName   string   `gorm:"column:policy_name;not null" json:"policy_name"`
	Scope        *string  `gorm:"column:scope" json:"scope"`
	Teams        []string `gorm:"column:teams;serializer:json;not null" json:"teams"`
	Keys         []string `gorm:"column:keys;serializer:json;not null" json:"keys"`
	Models       []string `gorm:"column:models;serializer:json;not null" json:"models"`
	Tags         []string `gorm:"column:tags;serializer:json;not null" json:"tags"`
	Priority     *int     `gorm:"column:priority" json:"priority"`
	IsDefault    bool     `gorm:"column:is_default;default:false;not null" json:"is_default"`
	CreatedAt    DateTime `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	CreatedBy    *string  `gorm:"column:created_by" json:"created_by"`
	UpdatedAt    DateTime `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	UpdatedBy    *string  `gorm:"column:updated_by" json:"updated_by"`
}

func (LiteLLMPolicyAttachmentTable) TableName() string {
	return "LiteLLM_PolicyAttachmentTable"
}

func (m *LiteLLMPolicyAttachmentTable) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.AttachmentID == "" {
		m.AttachmentID = uuid.NewString()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*LiteLLMPolicyAttachmentTable) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LiteLLMToolTable struct {
	ToolID       string         `gorm:"column:tool_id;primaryKey;type:char(36)" json:"tool_id"`
	ToolName     string         `gorm:"column:tool_name;uniqueIndex;not null" json:"tool_name"`
	Origin       *string        `gorm:"column:origin" json:"origin"`
	InputPolicy  *string        `gorm:"column:input_policy;index:LiteLLM_ToolTable_input_policy_idx;default:'untrusted';not null" json:"input_policy"`
	OutputPolicy *string        `gorm:"column:output_policy;index:LiteLLM_ToolTable_output_policy_idx;default:'untrusted';not null" json:"output_policy"`
	CallCount    int            `gorm:"column:call_count;default:0;not null" json:"call_count"`
	Assignments  datatypes.JSON `gorm:"column:assignments;default:'{}'" json:"assignments"`
	KeyHash      *string        `gorm:"column:key_hash" json:"key_hash"`
	TeamID       *string        `gorm:"column:team_id;index:LiteLLM_ToolTable_team_id_idx" json:"team_id"`
	KeyAlias     *string        `gorm:"column:key_alias" json:"key_alias"`
	UserAgent    *string        `gorm:"column:user_agent" json:"user_agent"`
	LastUsedAt   *DateTime      `gorm:"column:last_used_at" json:"last_used_at"`
	CreatedAt    DateTime       `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	CreatedBy    *string        `gorm:"column:created_by" json:"created_by"`
	UpdatedAt    DateTime       `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	UpdatedBy    *string        `gorm:"column:updated_by" json:"updated_by"`
}

func (LiteLLMToolTable) TableName() string {
	return "LiteLLM_ToolTable"
}

func (m *LiteLLMToolTable) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ToolID == "" {
		m.ToolID = uuid.NewString()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*LiteLLMToolTable) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LiteLLMAccessGroupTable struct {
	AccessGroupID        string         `gorm:"column:access_group_id;primaryKey;type:char(36)" json:"access_group_id"`
	AccessGroupName      string         `gorm:"column:access_group_name;uniqueIndex;not null" json:"access_group_name"`
	Description          *string        `gorm:"column:description" json:"description"`
	AccessModelNames     []string       `gorm:"column:access_model_names;serializer:json;not null" json:"access_model_names"`
	AccessMcpServerIds   []string       `gorm:"column:access_mcp_server_ids;serializer:json;not null" json:"access_mcp_server_ids"`
	McpToolPermissions   datatypes.JSON `gorm:"column:mcp_tool_permissions" json:"mcp_tool_permissions"`
	AccessAgentIds       []string       `gorm:"column:access_agent_ids;serializer:json;not null" json:"access_agent_ids"`
	AccessSkillNames     []string       `gorm:"column:access_skill_names;serializer:json;not null" json:"access_skill_names"`
	AccessVectorStoreIds []string       `gorm:"column:access_vector_store_ids;serializer:json;not null" json:"access_vector_store_ids"`
	AssignedTeamIds      []string       `gorm:"column:assigned_team_ids;serializer:json;not null" json:"assigned_team_ids"`
	AssignedKeyIds       []string       `gorm:"column:assigned_key_ids;serializer:json;not null" json:"assigned_key_ids"`
	CreatedAt            DateTime       `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	CreatedBy            *string        `gorm:"column:created_by" json:"created_by"`
	UpdatedAt            DateTime       `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	UpdatedBy            *string        `gorm:"column:updated_by" json:"updated_by"`
}

func (LiteLLMAccessGroupTable) TableName() string {
	return "LiteLLM_AccessGroupTable"
}

func (m *LiteLLMAccessGroupTable) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.AccessGroupID == "" {
		m.AccessGroupID = uuid.NewString()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*LiteLLMAccessGroupTable) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LiteLLMClaudeCodePluginTable struct {
	ID                     string                            `gorm:"column:id;primaryKey;type:char(36)" json:"id"`
	Name                   string                            `gorm:"column:name;uniqueIndex;not null" json:"name"`
	Version                *string                           `gorm:"column:version" json:"version"`
	Description            *string                           `gorm:"column:description" json:"description"`
	ManifestJSON           *string                           `gorm:"column:manifest_json" json:"manifest_json"`
	FilesJSON              *string                           `gorm:"column:files_json;default:'{}'" json:"files_json"`
	Enabled                *bool                             `gorm:"column:enabled;default:true;not null" json:"enabled"`
	CreatedAt              *DateTime                         `gorm:"column:created_at;autoCreateTime:false" json:"created_at"`
	UpdatedAt              *DateTime                         `gorm:"column:updated_at;autoUpdateTime:false" json:"updated_at"`
	CreatedBy              *string                           `gorm:"column:created_by" json:"created_by"`
	AnonymousDownloadCount int64                             `gorm:"column:anonymous_download_count;default:0;not null" json:"anonymous_download_count"`
	Versions               []LiteLLMClaudeCodePluginVersion  `gorm:"foreignKey:PluginID;constraint:OnUpdate:CASCADE,OnDelete:CASCADE"`
	Downloads              []LiteLLMClaudeCodePluginDownload `gorm:"foreignKey:PluginID;constraint:OnUpdate:CASCADE,OnDelete:CASCADE"`
	UploadedArchive        *LiteLLMClaudeCodePluginUpload    `gorm:"foreignKey:PluginName;references:Name;constraint:OnUpdate:CASCADE,OnDelete:CASCADE"`
}

func (LiteLLMClaudeCodePluginTable) TableName() string {
	return "LiteLLM_ClaudeCodePluginTable"
}

func (m *LiteLLMClaudeCodePluginTable) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = uuid.NewString()
	}
	if m.CreatedAt == nil {
		m.CreatedAt = &DateTime{Time: now}
	}
	if m.UpdatedAt == nil {
		m.UpdatedAt = &DateTime{Time: now}
	}
	return nil
}

func (*LiteLLMClaudeCodePluginTable) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", &DateTime{Time: now})
	}
	return nil
}

type LiteLLMClaudeCodePluginVersion struct {
	ID               string                        `gorm:"column:id;primaryKey;type:char(36)" json:"id"`
	PluginID         string                        `gorm:"column:plugin_id;uniqueIndex:LiteLLM_ClaudeCodePluginVersion_plugin_id_version_key;index:LiteLLM_ClaudeCodePluginVersion_plugin_id_published_at_idx;not null" json:"plugin_id"`
	Version          *string                       `gorm:"column:version;uniqueIndex:LiteLLM_ClaudeCodePluginVersion_plugin_id_version_key" json:"version"`
	Description      *string                       `gorm:"column:description" json:"description"`
	ManifestJSON     *string                       `gorm:"column:manifest_json" json:"manifest_json"`
	PublishedAt      *DateTime                     `gorm:"column:published_at;index:LiteLLM_ClaudeCodePluginVersion_plugin_id_published_at_idx" json:"published_at"`
	PublishedBy      *string                       `gorm:"column:published_by" json:"published_by"`
	ArchiveAvailable bool                          `gorm:"column:archive_available;default:false;not null" json:"archive_available"`
	Content          []byte                        `gorm:"column:content" json:"content"`
	Plugin           *LiteLLMClaudeCodePluginTable `gorm:"foreignKey:PluginID"`
}

func (LiteLLMClaudeCodePluginVersion) TableName() string {
	return "LiteLLM_ClaudeCodePluginVersion"
}

func (m *LiteLLMClaudeCodePluginVersion) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = uuid.NewString()
	}
	return nil
}

type LiteLLMSkillSubmission struct {
	ID              string    `gorm:"column:id;primaryKey;type:char(36);index:LiteLLM_SkillSubmission_submitted_by_created_at_id_idx;index:LiteLLM_SkillSubmission_submitted_by_status_created_at_id_idx;index:LiteLLM_SkillSubmission_status_created_at_id_idx;index:LiteLLM_SkillSubmission_created_at_id_idx" json:"id"`
	PluginName      string    `gorm:"column:plugin_name;not null" json:"plugin_name"`
	Operation       string    `gorm:"column:operation;not null" json:"operation"`
	Status          *string   `gorm:"column:status;index:LiteLLM_SkillSubmission_submitted_by_status_created_at_id_idx;index:LiteLLM_SkillSubmission_status_created_at_id_idx;default:'pending_review';not null" json:"status"`
	ProposalJSON    string    `gorm:"column:proposal_json;not null" json:"proposal_json"`
	ArchiveContent  []byte    `gorm:"column:archive_content" json:"archive_content"`
	SubmittedBy     string    `gorm:"column:submitted_by;index:LiteLLM_SkillSubmission_submitted_by_created_at_id_idx;index:LiteLLM_SkillSubmission_submitted_by_status_created_at_id_idx;not null" json:"submitted_by"`
	CreatedAt       DateTime  `gorm:"column:created_at;index:LiteLLM_SkillSubmission_submitted_by_created_at_id_idx;index:LiteLLM_SkillSubmission_submitted_by_status_created_at_id_idx;index:LiteLLM_SkillSubmission_status_created_at_id_idx;index:LiteLLM_SkillSubmission_created_at_id_idx;autoCreateTime:false;not null" json:"created_at"`
	BasePluginID    *string   `gorm:"column:base_plugin_id" json:"base_plugin_id"`
	BaseUpdatedAt   *DateTime `gorm:"column:base_updated_at" json:"base_updated_at"`
	ReviewedBy      *string   `gorm:"column:reviewed_by" json:"reviewed_by"`
	ReviewedAt      *DateTime `gorm:"column:reviewed_at" json:"reviewed_at"`
	RejectionReason *string   `gorm:"column:rejection_reason" json:"rejection_reason"`
	ScanJSON        *string   `gorm:"column:scan_json" json:"scan_json"`
}

func (LiteLLMSkillSubmission) TableName() string {
	return "LiteLLM_SkillSubmission"
}

func (m *LiteLLMSkillSubmission) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = uuid.NewString()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type LiteLLMClaudeCodePluginDownload struct {
	ID            int64                         `gorm:"column:id;primaryKey;autoIncrement;index:LiteLLM_ClaudeCodePluginDownload_plugin_id_id_idx" json:"id"`
	PluginID      string                        `gorm:"column:plugin_id;index:LiteLLM_ClaudeCodePluginDownload_plugin_id_id_idx;not null" json:"plugin_id"`
	DownloadedAt  DateTime                      `gorm:"column:downloaded_at;not null" json:"downloaded_at"`
	Version       *string                       `gorm:"column:version" json:"version"`
	Digest        string                        `gorm:"column:digest;not null" json:"digest"`
	Authenticated bool                          `gorm:"column:authenticated;not null" json:"authenticated"`
	UserID        *string                       `gorm:"column:user_id" json:"user_id"`
	UserEmail     *string                       `gorm:"column:user_email" json:"user_email"`
	KeyAlias      *string                       `gorm:"column:key_alias" json:"key_alias"`
	TeamID        *string                       `gorm:"column:team_id" json:"team_id"`
	TeamAlias     *string                       `gorm:"column:team_alias" json:"team_alias"`
	Plugin        *LiteLLMClaudeCodePluginTable `gorm:"foreignKey:PluginID"`
}

func (LiteLLMClaudeCodePluginDownload) TableName() string {
	return "LiteLLM_ClaudeCodePluginDownload"
}

func (m *LiteLLMClaudeCodePluginDownload) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.DownloadedAt.IsZero() {
		m.DownloadedAt = DateTime{Time: now}
	}
	return nil
}

type LiteLLMClaudeCodePluginUpload struct {
	PluginName string                        `gorm:"column:plugin_name;primaryKey" json:"plugin_name"`
	Content    []byte                        `gorm:"column:content;not null" json:"content"`
	Plugin     *LiteLLMClaudeCodePluginTable `gorm:"foreignKey:PluginName;references:Name"`
}

func (LiteLLMClaudeCodePluginUpload) TableName() string {
	return "LiteLLM_ClaudeCodePluginUpload"
}

type LiteLLMMemoryTable struct {
	MemoryID  string         `gorm:"column:memory_id;primaryKey;type:char(36)" json:"memory_id"`
	Key       string         `gorm:"column:key;uniqueIndex;not null" json:"key"`
	Value     string         `gorm:"column:value;not null" json:"value"`
	Metadata  datatypes.JSON `gorm:"column:metadata" json:"metadata"`
	UserID    *string        `gorm:"column:user_id;index:LiteLLM_MemoryTable_user_id_idx" json:"user_id"`
	TeamID    *string        `gorm:"column:team_id;index:LiteLLM_MemoryTable_team_id_idx" json:"team_id"`
	CreatedAt DateTime       `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	CreatedBy *string        `gorm:"column:created_by" json:"created_by"`
	UpdatedAt DateTime       `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	UpdatedBy *string        `gorm:"column:updated_by" json:"updated_by"`
}

func (LiteLLMMemoryTable) TableName() string {
	return "LiteLLM_MemoryTable"
}

func (m *LiteLLMMemoryTable) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.MemoryID == "" {
		m.MemoryID = uuid.NewString()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*LiteLLMMemoryTable) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LiteLLMAdaptiveRouterState struct {
	RouterName    string   `gorm:"column:router_name;primaryKey" json:"router_name"`
	RequestType   string   `gorm:"column:request_type;primaryKey" json:"request_type"`
	ModelName     string   `gorm:"column:model_name;primaryKey" json:"model_name"`
	Alpha         float64  `gorm:"column:alpha;not null" json:"alpha"`
	Beta          float64  `gorm:"column:beta;not null" json:"beta"`
	TotalSamples  int      `gorm:"column:total_samples;default:0;not null" json:"total_samples"`
	LastUpdatedAt DateTime `gorm:"column:last_updated_at;not null" json:"last_updated_at"`
}

func (LiteLLMAdaptiveRouterState) TableName() string {
	return "LiteLLM_AdaptiveRouterState"
}

func (m *LiteLLMAdaptiveRouterState) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.LastUpdatedAt.IsZero() {
		m.LastUpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*LiteLLMAdaptiveRouterState) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("LastUpdatedAt") {
		tx.Statement.SetColumn("LastUpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LiteLLMAdaptiveRouterSession struct {
	SessionID            string         `gorm:"column:session_id;primaryKey" json:"session_id"`
	RouterName           string         `gorm:"column:router_name;primaryKey" json:"router_name"`
	ModelName            string         `gorm:"column:model_name;primaryKey" json:"model_name"`
	ClassifiedType       string         `gorm:"column:classified_type;not null" json:"classified_type"`
	MisalignmentCount    int            `gorm:"column:misalignment_count;default:0;not null" json:"misalignment_count"`
	StagnationCount      int            `gorm:"column:stagnation_count;default:0;not null" json:"stagnation_count"`
	DisengagementCount   int            `gorm:"column:disengagement_count;default:0;not null" json:"disengagement_count"`
	SatisfactionCount    int            `gorm:"column:satisfaction_count;default:0;not null" json:"satisfaction_count"`
	FailureCount         int            `gorm:"column:failure_count;default:0;not null" json:"failure_count"`
	LoopCount            int            `gorm:"column:loop_count;default:0;not null" json:"loop_count"`
	ExhaustionCount      int            `gorm:"column:exhaustion_count;default:0;not null" json:"exhaustion_count"`
	LastUserContent      *string        `gorm:"column:last_user_content" json:"last_user_content"`
	LastAssistantContent *string        `gorm:"column:last_assistant_content" json:"last_assistant_content"`
	ToolCallHistory      datatypes.JSON `gorm:"column:tool_call_history;default:'[]';not null" json:"tool_call_history"`
	PendingToolCalls     datatypes.JSON `gorm:"column:pending_tool_calls;default:'{}';not null" json:"pending_tool_calls"`
	TurnCount            int            `gorm:"column:turn_count;default:0;not null" json:"turn_count"`
	LastProcessedTurn    *int           `gorm:"column:last_processed_turn;default:-1;not null" json:"last_processed_turn"`
	CleanCreditAwarded   bool           `gorm:"column:clean_credit_awarded;default:false;not null" json:"clean_credit_awarded"`
	TerminalStatus       *int           `gorm:"column:terminal_status" json:"terminal_status"`
	LastActivityAt       DateTime       `gorm:"column:last_activity_at;index:idx_adaptive_router_session_activity;not null" json:"last_activity_at"`
}

func (LiteLLMAdaptiveRouterSession) TableName() string {
	return "LiteLLM_AdaptiveRouterSession"
}

func (m *LiteLLMAdaptiveRouterSession) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.LastActivityAt.IsZero() {
		m.LastActivityAt = DateTime{Time: now}
	}
	return nil
}

func (*LiteLLMAdaptiveRouterSession) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("LastActivityAt") {
		tx.Statement.SetColumn("LastActivityAt", DateTime{Time: now})
	}
	return nil
}

type LiteLLMAutoRouterBaselineComparison struct {
	Scope             string    `gorm:"column:scope;primaryKey" json:"scope"`
	APIKey            string    `gorm:"column:api_key;index:idx_autorouter_baseline_scope;not null" json:"api_key"`
	SessionID         string    `gorm:"column:session_id;index:idx_autorouter_baseline_scope;not null" json:"session_id"`
	RouterName        string    `gorm:"column:router_name;index:idx_autorouter_baseline_scope;not null" json:"router_name"`
	InitialEquivalent bool      `gorm:"column:initial_equivalent;not null" json:"initial_equivalent"`
	Revision          int64     `gorm:"column:revision;default:0;not null" json:"revision"`
	PublishedRevision int64     `gorm:"column:published_revision;default:0;not null" json:"published_revision"`
	History           *string   `gorm:"column:history" json:"history"`
	AttemptedAt       *DateTime `gorm:"column:attempted_at" json:"attempted_at"`
	Retired           bool      `gorm:"column:retired;default:false;not null" json:"retired"`
	UpdatedAt         DateTime  `gorm:"column:updated_at;index:idx_autorouter_baseline_updated;autoUpdateTime:false;not null" json:"updated_at"`
}

func (LiteLLMAutoRouterBaselineComparison) TableName() string {
	return "LiteLLM_AutoRouterBaselineComparison"
}

func (m *LiteLLMAutoRouterBaselineComparison) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

type LiteLLMAutoRouterBaselineObservation struct {
	RequestID   string  `gorm:"column:request_id;primaryKey;index:idx_autorouter_baseline_event_order" json:"request_id"`
	Scope       string  `gorm:"column:scope;index:idx_autorouter_baseline_event_order;index:idx_autorouter_baseline_event_revision;not null" json:"scope"`
	StartedAt   float64 `gorm:"column:started_at;index:idx_autorouter_baseline_event_order;index:idx_autorouter_baseline_event_revision;not null" json:"started_at"`
	Revision    int64   `gorm:"column:revision;index:idx_autorouter_baseline_event_revision;not null" json:"revision"`
	Data        string  `gorm:"column:data;not null" json:"data"`
	Publication *string `gorm:"column:publication" json:"publication"`
	Conflicted  bool    `gorm:"column:conflicted;default:false;not null" json:"conflicted"`
}

func (LiteLLMAutoRouterBaselineObservation) TableName() string {
	return "LiteLLM_AutoRouterBaselineObservation"
}

type LiteLLMAutoRouterSession struct {
	APIKey                         string         `gorm:"column:api_key;primaryKey" json:"api_key"`
	SessionID                      string         `gorm:"column:session_id;primaryKey" json:"session_id"`
	RouterName                     string         `gorm:"column:router_name;primaryKey" json:"router_name"`
	RouterType                     string         `gorm:"column:router_type;not null" json:"router_type"`
	FirstTurnAt                    DateTime       `gorm:"column:first_turn_at;not null" json:"first_turn_at"`
	LastTurnAt                     DateTime       `gorm:"column:last_turn_at;index:idx_autorouter_session_last_turn;not null" json:"last_turn_at"`
	LastModel                      string         `gorm:"column:last_model;not null" json:"last_model"`
	Models                         datatypes.JSON `gorm:"column:models;default:'{}';not null" json:"models"`
	Turns                          int            `gorm:"column:turns;default:0;not null" json:"turns"`
	UnorderedTurns                 int            `gorm:"column:unordered_turns;default:0;not null" json:"unordered_turns"`
	CoveredTurns                   int            `gorm:"column:covered_turns;default:0;not null" json:"covered_turns"`
	CacheHits                      int            `gorm:"column:cache_hits;default:0;not null" json:"cache_hits"`
	SameModelTurns                 int            `gorm:"column:same_model_turns;default:0;not null" json:"same_model_turns"`
	SameModelHits                  int            `gorm:"column:same_model_hits;default:0;not null" json:"same_model_hits"`
	FirstVisitTurns                int            `gorm:"column:first_visit_turns;default:0;not null" json:"first_visit_turns"`
	FirstVisitHits                 int            `gorm:"column:first_visit_hits;default:0;not null" json:"first_visit_hits"`
	ReturnTurns                    int            `gorm:"column:return_turns;default:0;not null" json:"return_turns"`
	ReturnHits                     int            `gorm:"column:return_hits;default:0;not null" json:"return_hits"`
	ReturnExpiredMisses            int            `gorm:"column:return_expired_misses;default:0;not null" json:"return_expired_misses"`
	ReturnWithinTTLMisses          int            `gorm:"column:return_within_ttl_misses;default:0;not null" json:"return_within_ttl_misses"`
	TTL5mTurns                     int            `gorm:"column:ttl_5m_turns;default:0;not null" json:"ttl_5m_turns"`
	TTL1hTurns                     int            `gorm:"column:ttl_1h_turns;default:0;not null" json:"ttl_1h_turns"`
	TotalTokens                    int64          `gorm:"column:total_tokens;default:0;not null" json:"total_tokens"`
	Spend                          float64        `gorm:"column:spend;default:0;not null" json:"spend"`
	SavedSpend                     float64        `gorm:"column:saved_spend;default:0;not null" json:"saved_spend"`
	SavingsEstimatedTurns          int            `gorm:"column:savings_estimated_turns;default:0;not null" json:"savings_estimated_turns"`
	SavingsEstimatedActualSpend    float64        `gorm:"column:savings_estimated_actual_spend;default:0;not null" json:"savings_estimated_actual_spend"`
	SavingsEstimatedSavedSpend     float64        `gorm:"column:savings_estimated_saved_spend;default:0;not null" json:"savings_estimated_saved_spend"`
	SavingsEstimatedBaselineModels datatypes.JSON `gorm:"column:savings_estimated_baseline_models;default:'{}';not null" json:"savings_estimated_baseline_models"`
	ClassifierCost                 float64        `gorm:"column:classifier_cost;default:0;not null" json:"classifier_cost"`
	ClassifierCostRecordedTurns    int            `gorm:"column:classifier_cost_recorded_turns;default:0;not null" json:"classifier_cost_recorded_turns"`
	TierTurns                      datatypes.JSON `gorm:"column:tier_turns;default:'{}';not null" json:"tier_turns"`
	BaselineModels                 datatypes.JSON `gorm:"column:baseline_models;default:'{}';not null" json:"baseline_models"`
}

func (LiteLLMAutoRouterSession) TableName() string {
	return "LiteLLM_AutoRouterSession"
}

type LiteLLMAutoRouterUserSession struct {
	UserID                         string         `gorm:"column:user_id;primaryKey;index:idx_autorouter_user_session_user_last_turn" json:"user_id"`
	APIKey                         string         `gorm:"column:api_key;primaryKey" json:"api_key"`
	SessionID                      string         `gorm:"column:session_id;primaryKey" json:"session_id"`
	RouterName                     string         `gorm:"column:router_name;primaryKey" json:"router_name"`
	RouterType                     string         `gorm:"column:router_type;not null" json:"router_type"`
	FirstTurnAt                    DateTime       `gorm:"column:first_turn_at;not null" json:"first_turn_at"`
	LastTurnAt                     DateTime       `gorm:"column:last_turn_at;index:idx_autorouter_user_session_last_turn;index:idx_autorouter_user_session_user_last_turn;not null" json:"last_turn_at"`
	LastModel                      string         `gorm:"column:last_model;not null" json:"last_model"`
	Models                         datatypes.JSON `gorm:"column:models;default:'{}';not null" json:"models"`
	Turns                          int            `gorm:"column:turns;default:0;not null" json:"turns"`
	UnorderedTurns                 int            `gorm:"column:unordered_turns;default:0;not null" json:"unordered_turns"`
	CoveredTurns                   int            `gorm:"column:covered_turns;default:0;not null" json:"covered_turns"`
	CacheHits                      int            `gorm:"column:cache_hits;default:0;not null" json:"cache_hits"`
	SameModelTurns                 int            `gorm:"column:same_model_turns;default:0;not null" json:"same_model_turns"`
	SameModelHits                  int            `gorm:"column:same_model_hits;default:0;not null" json:"same_model_hits"`
	FirstVisitTurns                int            `gorm:"column:first_visit_turns;default:0;not null" json:"first_visit_turns"`
	FirstVisitHits                 int            `gorm:"column:first_visit_hits;default:0;not null" json:"first_visit_hits"`
	ReturnTurns                    int            `gorm:"column:return_turns;default:0;not null" json:"return_turns"`
	ReturnHits                     int            `gorm:"column:return_hits;default:0;not null" json:"return_hits"`
	ReturnExpiredMisses            int            `gorm:"column:return_expired_misses;default:0;not null" json:"return_expired_misses"`
	ReturnWithinTTLMisses          int            `gorm:"column:return_within_ttl_misses;default:0;not null" json:"return_within_ttl_misses"`
	TTL5mTurns                     int            `gorm:"column:ttl_5m_turns;default:0;not null" json:"ttl_5m_turns"`
	TTL1hTurns                     int            `gorm:"column:ttl_1h_turns;default:0;not null" json:"ttl_1h_turns"`
	TotalTokens                    int64          `gorm:"column:total_tokens;default:0;not null" json:"total_tokens"`
	Spend                          float64        `gorm:"column:spend;default:0;not null" json:"spend"`
	SavedSpend                     float64        `gorm:"column:saved_spend;default:0;not null" json:"saved_spend"`
	SavingsEstimatedTurns          int            `gorm:"column:savings_estimated_turns;default:0;not null" json:"savings_estimated_turns"`
	SavingsEstimatedActualSpend    float64        `gorm:"column:savings_estimated_actual_spend;default:0;not null" json:"savings_estimated_actual_spend"`
	SavingsEstimatedSavedSpend     float64        `gorm:"column:savings_estimated_saved_spend;default:0;not null" json:"savings_estimated_saved_spend"`
	SavingsEstimatedBaselineModels datatypes.JSON `gorm:"column:savings_estimated_baseline_models;default:'{}';not null" json:"savings_estimated_baseline_models"`
	ClassifierCost                 float64        `gorm:"column:classifier_cost;default:0;not null" json:"classifier_cost"`
	ClassifierCostRecordedTurns    int            `gorm:"column:classifier_cost_recorded_turns;default:0;not null" json:"classifier_cost_recorded_turns"`
	TierTurns                      datatypes.JSON `gorm:"column:tier_turns;default:'{}';not null" json:"tier_turns"`
	BaselineModels                 datatypes.JSON `gorm:"column:baseline_models;default:'{}';not null" json:"baseline_models"`
}

func (LiteLLMAutoRouterUserSession) TableName() string {
	return "LiteLLM_AutoRouterUserSession"
}

type LiteLLMAutoRouterDailySpend struct {
	Date                        string  `gorm:"column:date;primaryKey" json:"date"`
	APIKey                      string  `gorm:"column:api_key;primaryKey" json:"api_key"`
	UserID                      string  `gorm:"column:user_id;primaryKey" json:"user_id"`
	RouterName                  string  `gorm:"column:router_name;primaryKey" json:"router_name"`
	RouterType                  string  `gorm:"column:router_type;primaryKey" json:"router_type"`
	Turns                       int     `gorm:"column:turns;default:0;not null" json:"turns"`
	TotalTokens                 int64   `gorm:"column:total_tokens;default:0;not null" json:"total_tokens"`
	TokenRecordedTurns          int     `gorm:"column:token_recorded_turns;default:0;not null" json:"token_recorded_turns"`
	Spend                       float64 `gorm:"column:spend;default:0;not null" json:"spend"`
	SavedSpend                  float64 `gorm:"column:saved_spend;default:0;not null" json:"saved_spend"`
	SavingsEstimatedTurns       int     `gorm:"column:savings_estimated_turns;default:0;not null" json:"savings_estimated_turns"`
	SavingsEstimatedActualSpend float64 `gorm:"column:savings_estimated_actual_spend;default:0;not null" json:"savings_estimated_actual_spend"`
	SavingsEstimatedSavedSpend  float64 `gorm:"column:savings_estimated_saved_spend;default:0;not null" json:"savings_estimated_saved_spend"`
	ClassifierCost              float64 `gorm:"column:classifier_cost;default:0;not null" json:"classifier_cost"`
	ClassifierCostRecordedTurns int     `gorm:"column:classifier_cost_recorded_turns;default:0;not null" json:"classifier_cost_recorded_turns"`
}

func (LiteLLMAutoRouterDailySpend) TableName() string {
	return "LiteLLM_AutoRouterDailySpend"
}

type LiteLLMShadowEvalJob struct {
	ID               string    `gorm:"column:id;primaryKey" json:"id"`
	GroupID          string    `gorm:"column:group_id;index:LiteLLM_ShadowEvalJob_group_id_idx;not null" json:"group_id"`
	TargetType       *string   `gorm:"column:target_type;index:LiteLLM_ShadowEvalJob_target_type_target_id_idx;default:'key';not null" json:"target_type"`
	TargetID         string    `gorm:"column:target_id;index:LiteLLM_ShadowEvalJob_target_type_target_id_idx;not null" json:"target_id"`
	RouterName       string    `gorm:"column:router_name;not null" json:"router_name"`
	RouterNames      []string  `gorm:"column:router_names;serializer:json;not null" json:"router_names"`
	Models           []string  `gorm:"column:models;serializer:json;not null" json:"models"`
	Direction        *string   `gorm:"column:direction;default:'forward';not null" json:"direction"`
	BaselineModel    *string   `gorm:"column:baseline_model" json:"baseline_model"`
	JudgeModel       string    `gorm:"column:judge_model;not null" json:"judge_model"`
	ShadowPercentage float64   `gorm:"column:shadow_percentage;not null" json:"shadow_percentage"`
	MaxTurns         int       `gorm:"column:max_turns;not null" json:"max_turns"`
	MaxBudget        *float64  `gorm:"column:max_budget" json:"max_budget"`
	CreatedAt        DateTime  `gorm:"column:created_at;index:LiteLLM_ShadowEvalJob_created_at_idx;autoCreateTime:false;not null" json:"created_at"`
	CreatedBy        *string   `gorm:"column:created_by" json:"created_by"`
	EndsAt           DateTime  `gorm:"column:ends_at;not null" json:"ends_at"`
	StoppedAt        *DateTime `gorm:"column:stopped_at" json:"stopped_at"`
	StoppedBy        *string   `gorm:"column:stopped_by" json:"stopped_by"`
}

func (LiteLLMShadowEvalJob) TableName() string {
	return "LiteLLM_ShadowEvalJob"
}

func (m *LiteLLMShadowEvalJob) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type LiteLLMShadowEvalAttempt struct {
	ID                   string   `gorm:"column:id;primaryKey" json:"id"`
	JobID                string   `gorm:"column:job_id;index:LiteLLM_ShadowEvalAttempt_job_id_idx;not null" json:"job_id"`
	RequestID            string   `gorm:"column:request_id;not null" json:"request_id"`
	Outcome              string   `gorm:"column:outcome;not null" json:"outcome"`
	RouterName           *string  `gorm:"column:router_name" json:"router_name"`
	Tier                 *string  `gorm:"column:tier" json:"tier"`
	RealModel            *string  `gorm:"column:real_model" json:"real_model"`
	ShadowModel          *string  `gorm:"column:shadow_model" json:"shadow_model"`
	Confidence           *float64 `gorm:"column:confidence" json:"confidence"`
	JudgeCost            float64  `gorm:"column:judge_cost;default:0;not null" json:"judge_cost"`
	ShadowCost           float64  `gorm:"column:shadow_cost;default:0;not null" json:"shadow_cost"`
	RealCost             *float64 `gorm:"column:real_cost" json:"real_cost"`
	RealClassifierCost   float64  `gorm:"column:real_classifier_cost;default:0;not null" json:"real_classifier_cost"`
	ShadowClassifierCost float64  `gorm:"column:shadow_classifier_cost;default:0;not null" json:"shadow_classifier_cost"`
	RealCacheHit         bool     `gorm:"column:real_cache_hit;default:false;not null" json:"real_cache_hit"`
	Error                *string  `gorm:"column:error" json:"error"`
	CreatedAt            DateTime `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
}

func (LiteLLMShadowEvalAttempt) TableName() string {
	return "LiteLLM_ShadowEvalAttempt"
}

func (m *LiteLLMShadowEvalAttempt) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type LiteLLMShadowEvalFunnel struct {
	JobID       string `gorm:"column:job_id;primaryKey" json:"job_id"`
	NotSampled  int    `gorm:"column:not_sampled;default:0;not null" json:"not_sampled"`
	Unjudgeable int    `gorm:"column:unjudgeable;default:0;not null" json:"unjudgeable"`
	Shed        int    `gorm:"column:shed;default:0;not null" json:"shed"`
	Withheld    int    `gorm:"column:withheld;default:0;not null" json:"withheld"`
}

func (LiteLLMShadowEvalFunnel) TableName() string {
	return "LiteLLM_ShadowEvalFunnel"
}

type LiteLLMWorkflowRun struct {
	RunID        string                   `gorm:"column:run_id;primaryKey;type:char(36)" json:"run_id"`
	SessionID    string                   `gorm:"column:session_id;uniqueIndex;index:LiteLLM_WorkflowRun_session_id_idx;not null" json:"session_id"`
	WorkflowType string                   `gorm:"column:workflow_type;index:LiteLLM_WorkflowRun_workflow_type_status_idx;not null" json:"workflow_type"`
	Status       *string                  `gorm:"column:status;index:LiteLLM_WorkflowRun_workflow_type_status_idx;default:'pending';not null" json:"status"`
	CreatedBy    *string                  `gorm:"column:created_by;index:LiteLLM_WorkflowRun_created_by_idx" json:"created_by"`
	CreatedAt    DateTime                 `gorm:"column:created_at;index:LiteLLM_WorkflowRun_created_at_idx;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt    DateTime                 `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	Input        datatypes.JSON           `gorm:"column:input" json:"input"`
	Output       datatypes.JSON           `gorm:"column:output" json:"output"`
	Metadata     datatypes.JSON           `gorm:"column:metadata" json:"metadata"`
	Events       []LiteLLMWorkflowEvent   `gorm:"foreignKey:RunID;references:RunID"`
	Messages     []LiteLLMWorkflowMessage `gorm:"foreignKey:RunID;references:RunID"`
}

func (LiteLLMWorkflowRun) TableName() string {
	return "LiteLLM_WorkflowRun"
}

func (m *LiteLLMWorkflowRun) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.RunID == "" {
		m.RunID = uuid.NewString()
	}
	if m.SessionID == "" {
		m.SessionID = uuid.NewString()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*LiteLLMWorkflowRun) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LiteLLMWorkflowEvent struct {
	EventID        string              `gorm:"column:event_id;primaryKey;type:char(36)" json:"event_id"`
	RunID          string              `gorm:"column:run_id;uniqueIndex:LiteLLM_WorkflowEvent_run_id_sequence_number_key;index:LiteLLM_WorkflowEvent_run_id_idx;not null" json:"run_id"`
	EventType      string              `gorm:"column:event_type;not null" json:"event_type"`
	StepName       string              `gorm:"column:step_name;not null" json:"step_name"`
	SequenceNumber int                 `gorm:"column:sequence_number;uniqueIndex:LiteLLM_WorkflowEvent_run_id_sequence_number_key;not null" json:"sequence_number"`
	Data           datatypes.JSON      `gorm:"column:data" json:"data"`
	CreatedAt      DateTime            `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	Run            *LiteLLMWorkflowRun `gorm:"foreignKey:RunID;references:RunID"`
}

func (LiteLLMWorkflowEvent) TableName() string {
	return "LiteLLM_WorkflowEvent"
}

func (m *LiteLLMWorkflowEvent) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.EventID == "" {
		m.EventID = uuid.NewString()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type LiteLLMWorkflowMessage struct {
	MessageID      string              `gorm:"column:message_id;primaryKey;type:char(36)" json:"message_id"`
	RunID          string              `gorm:"column:run_id;uniqueIndex:LiteLLM_WorkflowMessage_run_id_sequence_number_key;index:LiteLLM_WorkflowMessage_run_id_idx;not null" json:"run_id"`
	Role           string              `gorm:"column:role;not null" json:"role"`
	Content        string              `gorm:"column:content;not null" json:"content"`
	SequenceNumber int                 `gorm:"column:sequence_number;uniqueIndex:LiteLLM_WorkflowMessage_run_id_sequence_number_key;not null" json:"sequence_number"`
	SessionID      *string             `gorm:"column:session_id" json:"session_id"`
	CreatedAt      DateTime            `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	Run            *LiteLLMWorkflowRun `gorm:"foreignKey:RunID;references:RunID"`
}

func (LiteLLMWorkflowMessage) TableName() string {
	return "LiteLLM_WorkflowMessage"
}

func (m *LiteLLMWorkflowMessage) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.MessageID == "" {
		m.MessageID = uuid.NewString()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type LiteLLMBackgroundInteractionSettlement struct {
	InteractionID     string         `gorm:"column:interaction_id;primaryKey" json:"interaction_id"`
	CustomLlmProvider string         `gorm:"column:custom_llm_provider;not null" json:"custom_llm_provider"`
	CreateContext     datatypes.JSON `gorm:"column:create_context;not null" json:"create_context"`
	CreatedAt         DateTime       `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	ClaimedAt         *DateTime      `gorm:"column:claimed_at;index:idx_background_interaction_settlement_claimed_at" json:"claimed_at"`
	ClaimedBy         *string        `gorm:"column:claimed_by" json:"claimed_by"`
	SettledAt         *DateTime      `gorm:"column:settled_at;index:idx_background_interaction_settlement_settled_at" json:"settled_at"`
	Outcome           *string        `gorm:"column:outcome" json:"outcome"`
}

func (LiteLLMBackgroundInteractionSettlement) TableName() string {
	return "LiteLLM_BackgroundInteractionSettlement"
}

func (m *LiteLLMBackgroundInteractionSettlement) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type LiteLLMBackgroundResponseSettlement struct {
	ResponseID string   `gorm:"column:response_id;primaryKey" json:"response_id"`
	SettledAt  DateTime `gorm:"column:settled_at;not null" json:"settled_at"`
}

func (LiteLLMBackgroundResponseSettlement) TableName() string {
	return "LiteLLM_BackgroundResponseSettlement"
}

func (m *LiteLLMBackgroundResponseSettlement) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.SettledAt.IsZero() {
		m.SettledAt = DateTime{Time: now}
	}
	return nil
}

type LiteLLMLens struct {
	ID      string              `gorm:"column:id;primaryKey" json:"id"`
	Version int                 `gorm:"column:version;default:0;not null" json:"version"`
	Data    datatypes.JSON      `gorm:"column:data;not null" json:"data"`
	DueAt   *DateTime           `gorm:"column:due_at;index:LiteLLM_Lens_due_at_idx;default:'1970-01-01 00:00:00'::timestamp without time zone" json:"due_at"`
	Reviews []LiteLLMLensReview `gorm:"foreignKey:LensID;constraint:OnDelete:CASCADE"`
}

func (LiteLLMLens) TableName() string {
	return "LiteLLM_Lens"
}

type LiteLLMLensRun struct {
	ID        string         `gorm:"column:id;primaryKey" json:"id"`
	LensID    string         `gorm:"column:lens_id;index:LiteLLM_LensRun_lens_id_created_at_idx;not null" json:"lens_id"`
	CreatedAt DateTime       `gorm:"column:created_at;index:LiteLLM_LensRun_lens_id_created_at_idx;autoCreateTime:false;not null" json:"created_at"`
	Data      datatypes.JSON `gorm:"column:data;not null" json:"data"`
}

func (LiteLLMLensRun) TableName() string {
	return "LiteLLM_LensRun"
}

type LiteLLMLensReview struct {
	LensID      string         `gorm:"column:lens_id;primaryKey" json:"lens_id"`
	CriteriaKey string         `gorm:"column:criteria_key;primaryKey" json:"criteria_key"`
	ExecutionID string         `gorm:"column:execution_id;primaryKey" json:"execution_id"`
	Data        datatypes.JSON `gorm:"column:data;not null" json:"data"`
	Lens        *LiteLLMLens   `gorm:"foreignKey:LensID"`
}

func (LiteLLMLensReview) TableName() string {
	return "LiteLLM_LensReview"
}

type LiteLLMLensWorker struct {
	ID        string         `gorm:"column:id;primaryKey" json:"id"`
	TokenHash string         `gorm:"column:token_hash;uniqueIndex;not null" json:"token_hash"`
	Data      datatypes.JSON `gorm:"column:data;not null" json:"data"`
}

func (LiteLLMLensWorker) TableName() string {
	return "LiteLLM_LensWorker"
}

type LiteLLMLensIngestionKey struct {
	ID   string         `gorm:"column:id;primaryKey" json:"id"`
	Data datatypes.JSON `gorm:"column:data;not null" json:"data"`
}

func (LiteLLMLensIngestionKey) TableName() string {
	return "LiteLLM_LensIngestionKey"
}

type LiteLLMLensDataset struct {
	ID        string         `gorm:"column:id;primaryKey" json:"id"`
	Revision  int            `gorm:"column:revision;primaryKey" json:"revision"`
	CreatedAt DateTime       `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	Data      datatypes.JSON `gorm:"column:data;not null" json:"data"`
}

func (LiteLLMLensDataset) TableName() string {
	return "LiteLLM_LensDataset"
}

type LiteLLMLensSignalConfig struct {
	ID   string         `gorm:"column:id;primaryKey" json:"id"`
	Data datatypes.JSON `gorm:"column:data;not null" json:"data"`
}

func (LiteLLMLensSignalConfig) TableName() string {
	return "LiteLLM_LensSignalConfig"
}

type LiteLLMLensTraceSignal struct {
	TraceID      string         `gorm:"column:trace_id;primaryKey" json:"trace_id"`
	TraceRef     string         `gorm:"column:trace_ref;primaryKey;default:''" json:"trace_ref"`
	ConfigKey    string         `gorm:"column:config_key;not null" json:"config_key"`
	SpanCount    int            `gorm:"column:span_count;not null" json:"span_count"`
	ClaimedUntil *DateTime      `gorm:"column:claimed_until" json:"claimed_until"`
	ClassifiedAt *DateTime      `gorm:"column:classified_at" json:"classified_at"`
	Data         datatypes.JSON `gorm:"column:data;not null" json:"data"`
}

func (LiteLLMLensTraceSignal) TableName() string {
	return "LiteLLM_LensTraceSignal"
}

type LiteLLMTelemetryReport struct {
	ID          string         `gorm:"column:id;primaryKey" json:"id"`
	WindowStart float64        `gorm:"column:window_start;not null" json:"window_start"`
	WindowEnd   float64        `gorm:"column:window_end;index:LiteLLM_TelemetryReport_window_end_idx;not null" json:"window_end"`
	Report      datatypes.JSON `gorm:"column:report;not null" json:"report"`
	CreatedAt   DateTime       `gorm:"column:created_at;index:LiteLLM_TelemetryReport_created_at_idx;autoCreateTime:false;not null" json:"created_at"`
}

func (LiteLLMTelemetryReport) TableName() string {
	return "LiteLLM_TelemetryReport"
}

func (m *LiteLLMTelemetryReport) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type LiteLLMSpendLogDelivery struct {
	DeliveryID        string         `gorm:"column:delivery_id;primaryKey;default:(gen_random_uuid())::text" json:"delivery_id"`
	RequestID         string         `gorm:"column:request_id;index:LiteLLM_SpendLogDelivery_request_id_idx;not null" json:"request_id"`
	State             string         `gorm:"column:state;index:LiteLLM_SpendLogDelivery_state_next_attempt_at_idx;index:LiteLLM_SpendLogDelivery_state_applied_at_idx;default:'pending';not null" json:"state"`
	AccountingPayload datatypes.JSON `gorm:"column:accounting_payload;not null" json:"accounting_payload"`
	IsBatch           bool           `gorm:"column:is_batch;not null" json:"is_batch"`
	Attempts          int            `gorm:"column:attempts;default:0;not null" json:"attempts"`
	NextAttemptAt     DateTime       `gorm:"column:next_attempt_at;index:LiteLLM_SpendLogDelivery_state_next_attempt_at_idx;not null" json:"next_attempt_at"`
	AppliedAt         *DateTime      `gorm:"column:applied_at;index:LiteLLM_SpendLogDelivery_state_applied_at_idx" json:"applied_at"`
	LastError         *string        `gorm:"column:last_error" json:"last_error"`
	CreatedAt         DateTime       `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
}

func (LiteLLMSpendLogDelivery) TableName() string {
	return "LiteLLM_SpendLogDelivery"
}

func (m *LiteLLMSpendLogDelivery) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.NextAttemptAt.IsZero() {
		m.NextAttemptAt = DateTime{Time: now}
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type LiteLLMSubscriptionAccountTable struct {
	SubscriptionAccountID string   `gorm:"column:subscription_account_id;primaryKey;type:char(36)" json:"subscription_account_id"`
	CustomLlmProvider     string   `gorm:"column:custom_llm_provider;uniqueIndex:LiteLLM_SubscriptionAccountTable_custom_llm_provider_account_id_key;not null" json:"custom_llm_provider"`
	AccountID             string   `gorm:"column:account_id;uniqueIndex:LiteLLM_SubscriptionAccountTable_custom_llm_provider_account_id_key;not null" json:"account_id"`
	Label                 *string  `gorm:"column:label" json:"label"`
	MonthlyFee            float64  `gorm:"column:monthly_fee;not null" json:"monthly_fee"`
	Currency              string   `gorm:"column:currency;not null" json:"currency"`
	BillingPeriodStart    string   `gorm:"column:billing_period_start;not null" json:"billing_period_start"`
	CreatedAt             DateTime `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	CreatedBy             string   `gorm:"column:created_by;not null" json:"created_by"`
	UpdatedAt             DateTime `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	UpdatedBy             string   `gorm:"column:updated_by;not null" json:"updated_by"`
}

func (LiteLLMSubscriptionAccountTable) TableName() string {
	return "LiteLLM_SubscriptionAccountTable"
}

func (m *LiteLLMSubscriptionAccountTable) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.SubscriptionAccountID == "" {
		m.SubscriptionAccountID = uuid.NewString()
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*LiteLLMSubscriptionAccountTable) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LiteLLMAlert struct {
	ID           int64          `gorm:"column:id;primaryKey;autoIncrement;index:LiteLLM_Alert_status_id_idx" json:"id"`
	DetectionKey string         `gorm:"column:detection_key;uniqueIndex;type:varchar(64);not null" json:"detection_key"`
	Type         string         `gorm:"column:type;type:varchar(32);not null" json:"type"`
	ResourceType string         `gorm:"column:resource_type;type:varchar(16);not null" json:"resource_type"`
	ResourceID   string         `gorm:"column:resource_id;type:varchar(1024);not null" json:"resource_id"`
	Status       *string        `gorm:"column:status;index:LiteLLM_Alert_status_id_idx;type:varchar(16);default:'open';not null" json:"status"`
	WindowStart  DateTime       `gorm:"column:window_start;not null" json:"window_start"`
	WindowEnd    DateTime       `gorm:"column:window_end;not null" json:"window_end"`
	Evidence     datatypes.JSON `gorm:"column:evidence;not null" json:"evidence"`
	CreatedAt    DateTime       `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt    DateTime       `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	UpdatedBy    *string        `gorm:"column:updated_by;type:varchar(1024)" json:"updated_by"`
}

func (LiteLLMAlert) TableName() string {
	return "LiteLLM_Alert"
}

func (m *LiteLLMAlert) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*LiteLLMAlert) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type LiteLLMPerformanceAlertState struct {
	Enabled     *bool     `gorm:"column:enabled;default:true;not null" json:"enabled"`
	AlertsUsed  *int      `gorm:"column:alerts_used" json:"alerts_used"`
	ID          int       `gorm:"column:id;primaryKey" json:"id"`
	Owner       string    `gorm:"column:owner;type:varchar(32);not null" json:"owner"`
	NextRunAt   DateTime  `gorm:"column:next_run_at;type:timestamptz(3);not null" json:"next_run_at"`
	Day         *string   `gorm:"column:day;type:varchar(10)" json:"day"`
	Scope       *string   `gorm:"column:scope;type:varchar(16)" json:"scope"`
	ResumeAfter string    `gorm:"column:resume_after;type:varchar(1024);default:'';not null" json:"resume_after"`
	Wrapped     bool      `gorm:"column:wrapped;default:false;not null" json:"wrapped"`
	Checked     int       `gorm:"column:checked;default:0;not null" json:"checked"`
	Skipped     int       `gorm:"column:skipped;default:0;not null" json:"skipped"`
	StartScope  *string   `gorm:"column:start_scope;type:varchar(16)" json:"start_scope"`
	StartAfter  string    `gorm:"column:start_after;type:varchar(1024);default:'';not null" json:"start_after"`
	CompletedAt *DateTime `gorm:"column:completed_at" json:"completed_at"`
	UpdatedAt   DateTime  `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
}

func (LiteLLMPerformanceAlertState) TableName() string {
	return "LiteLLM_PerformanceAlertState"
}

func (m *LiteLLMPerformanceAlertState) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*LiteLLMPerformanceAlertState) BeforeUpdate(tx *gorm.DB) error {
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
