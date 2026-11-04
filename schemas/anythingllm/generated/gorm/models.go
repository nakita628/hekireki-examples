package model

import (
	"database/sql/driver"
	"fmt"
	"strconv"
	"time"

	"gorm.io/gorm"
)

type APIKeys struct {
	ID            int      `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	Name          *string  `gorm:"column:name" json:"name"`
	Secret        *string  `gorm:"column:secret;uniqueIndex" json:"secret"`
	CreatedBy     *int     `gorm:"column:createdBy" json:"createdBy"`
	CreatedAt     DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	LastUpdatedAt DateTime `gorm:"column:lastUpdatedAt;not null" json:"lastUpdatedAt"`
}

func (APIKeys) TableName() string {
	return "api_keys"
}

func (m *APIKeys) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.LastUpdatedAt.IsZero() {
		m.LastUpdatedAt = DateTime{Time: now}
	}
	return nil
}

type WorkspaceDocuments struct {
	ID                 int                 `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	DocID              string              `gorm:"column:docId;uniqueIndex;not null" json:"docId"`
	Filename           string              `gorm:"column:filename;not null" json:"filename"`
	Docpath            string              `gorm:"column:docpath;not null" json:"docpath"`
	WorkspaceID        int                 `gorm:"column:workspaceId;not null" json:"workspaceId"`
	Metadata           *string             `gorm:"column:metadata" json:"metadata"`
	Pinned             *bool               `gorm:"column:pinned;default:false" json:"pinned"`
	Watched            *bool               `gorm:"column:watched;default:false" json:"watched"`
	CreatedAt          DateTime            `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	LastUpdatedAt      DateTime            `gorm:"column:lastUpdatedAt;not null" json:"lastUpdatedAt"`
	Workspace          *Workspaces         `gorm:"foreignKey:WorkspaceID"`
	DocumentSyncQueues *DocumentSyncQueues `gorm:"foreignKey:WorkspaceDocID;constraint:OnDelete:CASCADE"`
}

func (WorkspaceDocuments) TableName() string {
	return "workspace_documents"
}

func (m *WorkspaceDocuments) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.LastUpdatedAt.IsZero() {
		m.LastUpdatedAt = DateTime{Time: now}
	}
	return nil
}

type Invites struct {
	ID            int      `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	Code          string   `gorm:"column:code;uniqueIndex;not null" json:"code"`
	Status        *string  `gorm:"column:status;default:'pending';not null" json:"status"`
	ClaimedBy     *int     `gorm:"column:claimedBy" json:"claimedBy"`
	WorkspaceIds  *string  `gorm:"column:workspaceIds" json:"workspaceIds"`
	CreatedAt     DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	CreatedBy     int      `gorm:"column:createdBy;not null" json:"createdBy"`
	LastUpdatedAt DateTime `gorm:"column:lastUpdatedAt;not null" json:"lastUpdatedAt"`
}

func (Invites) TableName() string {
	return "invites"
}

func (m *Invites) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.LastUpdatedAt.IsZero() {
		m.LastUpdatedAt = DateTime{Time: now}
	}
	return nil
}

type SystemSettings struct {
	ID            int      `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	Label         string   `gorm:"column:label;uniqueIndex;not null" json:"label"`
	Value         *string  `gorm:"column:value" json:"value"`
	CreatedAt     DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	LastUpdatedAt DateTime `gorm:"column:lastUpdatedAt;not null" json:"lastUpdatedAt"`
}

func (SystemSettings) TableName() string {
	return "system_settings"
}

func (m *SystemSettings) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.LastUpdatedAt.IsZero() {
		m.LastUpdatedAt = DateTime{Time: now}
	}
	return nil
}

type Users struct {
	ID                        int                         `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	Username                  *string                     `gorm:"column:username;uniqueIndex" json:"username"`
	Password                  string                      `gorm:"column:password;not null" json:"password"`
	PfpFilename               *string                     `gorm:"column:pfpFilename" json:"pfpFilename"`
	Role                      *string                     `gorm:"column:role;default:'default';not null" json:"role"`
	Suspended                 int                         `gorm:"column:suspended;default:0;not null" json:"suspended"`
	SeenRecoveryCodes         *bool                       `gorm:"column:seen_recovery_codes;default:false" json:"seen_recovery_codes"`
	CreatedAt                 DateTime                    `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	LastUpdatedAt             DateTime                    `gorm:"column:lastUpdatedAt;not null" json:"lastUpdatedAt"`
	DailyMessageLimit         *int                        `gorm:"column:dailyMessageLimit" json:"dailyMessageLimit"`
	Bio                       *string                     `gorm:"column:bio;default:''" json:"bio"`
	WebPushSubscriptionConfig *string                     `gorm:"column:web_push_subscription_config" json:"web_push_subscription_config"`
	WorkspaceChats            []WorkspaceChats            `gorm:"foreignKey:UserID;constraint:OnUpdate:CASCADE,OnDelete:CASCADE"`
	WorkspaceUsers            []WorkspaceUsers            `gorm:"foreignKey:UserID;constraint:OnUpdate:CASCADE,OnDelete:CASCADE"`
	EmbedConfigs              []EmbedConfigs              `gorm:"foreignKey:UsersID"`
	EmbedChats                []EmbedChats                `gorm:"foreignKey:UsersID"`
	Threads                   []WorkspaceThreads          `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	RecoveryCodes             []RecoveryCodes             `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	PasswordResetTokens       []PasswordResetTokens       `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	WorkspaceAgentInvocations []WorkspaceAgentInvocations `gorm:"foreignKey:UserID;constraint:OnUpdate:CASCADE,OnDelete:CASCADE"`
	SlashCommandPresets       []SlashCommandPresets       `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	BrowserExtensionAPIKeys   []BrowserExtensionAPIKeys   `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	TemporaryAuthTokens       []TemporaryAuthTokens       `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	SystemPromptVariables     []SystemPromptVariables     `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	PromptHistory             []PromptHistory             `gorm:"foreignKey:ModifiedBy"`
	DesktopMobileDevices      []DesktopMobileDevices      `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	WorkspaceParsedFiles      []WorkspaceParsedFiles      `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	Memories                  []Memories                  `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
}

func (Users) TableName() string {
	return "users"
}

func (m *Users) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.LastUpdatedAt.IsZero() {
		m.LastUpdatedAt = DateTime{Time: now}
	}
	return nil
}

type RecoveryCodes struct {
	ID        int      `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	UserID    int      `gorm:"column:user_id;index:recovery_codes_user_id_idx;not null" json:"user_id"`
	CodeHash  string   `gorm:"column:code_hash;not null" json:"code_hash"`
	CreatedAt DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	User      *Users   `gorm:"foreignKey:UserID"`
}

func (RecoveryCodes) TableName() string {
	return "recovery_codes"
}

func (m *RecoveryCodes) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type PasswordResetTokens struct {
	ID        int      `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	UserID    int      `gorm:"column:user_id;index:password_reset_tokens_user_id_idx;not null" json:"user_id"`
	Token     string   `gorm:"column:token;uniqueIndex;not null" json:"token"`
	ExpiresAt DateTime `gorm:"column:expiresAt;not null" json:"expiresAt"`
	CreatedAt DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	User      *Users   `gorm:"foreignKey:UserID"`
}

func (PasswordResetTokens) TableName() string {
	return "password_reset_tokens"
}

func (m *PasswordResetTokens) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type DocumentVectors struct {
	ID            int      `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	DocID         string   `gorm:"column:docId;not null" json:"docId"`
	VectorID      string   `gorm:"column:vectorId;not null" json:"vectorId"`
	CreatedAt     DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	LastUpdatedAt DateTime `gorm:"column:lastUpdatedAt;not null" json:"lastUpdatedAt"`
}

func (DocumentVectors) TableName() string {
	return "document_vectors"
}

func (m *DocumentVectors) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.LastUpdatedAt.IsZero() {
		m.LastUpdatedAt = DateTime{Time: now}
	}
	return nil
}

type Workspaces struct {
	ID                         int                          `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	Name                       string                       `gorm:"column:name;not null" json:"name"`
	Slug                       string                       `gorm:"column:slug;uniqueIndex;not null" json:"slug"`
	VectorTag                  *string                      `gorm:"column:vectorTag" json:"vectorTag"`
	CreatedAt                  DateTime                     `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	OpenAiTemp                 *float64                     `gorm:"column:openAiTemp" json:"openAiTemp"`
	OpenAiHistory              *int                         `gorm:"column:openAiHistory;default:20;not null" json:"openAiHistory"`
	LastUpdatedAt              DateTime                     `gorm:"column:lastUpdatedAt;not null" json:"lastUpdatedAt"`
	OpenAiPrompt               *string                      `gorm:"column:openAiPrompt" json:"openAiPrompt"`
	SimilarityThreshold        *float64                     `gorm:"column:similarityThreshold;default:0.25" json:"similarityThreshold"`
	ChatProvider               *string                      `gorm:"column:chatProvider" json:"chatProvider"`
	ChatModel                  *string                      `gorm:"column:chatModel" json:"chatModel"`
	TopN                       *int                         `gorm:"column:topN;default:4" json:"topN"`
	ChatMode                   *string                      `gorm:"column:chatMode;default:'chat'" json:"chatMode"`
	PfpFilename                *string                      `gorm:"column:pfpFilename" json:"pfpFilename"`
	AgentProvider              *string                      `gorm:"column:agentProvider" json:"agentProvider"`
	AgentModel                 *string                      `gorm:"column:agentModel" json:"agentModel"`
	QueryRefusalResponse       *string                      `gorm:"column:queryRefusalResponse" json:"queryRefusalResponse"`
	VectorSearchMode           *string                      `gorm:"column:vectorSearchMode;default:'default'" json:"vectorSearchMode"`
	RouterID                   *int                         `gorm:"column:router_id" json:"router_id"`
	WorkspaceUsers             []WorkspaceUsers             `gorm:"foreignKey:WorkspaceID;constraint:OnUpdate:CASCADE,OnDelete:CASCADE"`
	Documents                  []WorkspaceDocuments         `gorm:"foreignKey:WorkspaceID"`
	WorkspaceSuggestedMessages []WorkspaceSuggestedMessages `gorm:"foreignKey:WorkspaceID;constraint:OnDelete:CASCADE"`
	EmbedConfigs               []EmbedConfigs               `gorm:"foreignKey:WorkspaceID;constraint:OnDelete:CASCADE"`
	Threads                    []WorkspaceThreads           `gorm:"foreignKey:WorkspaceID;constraint:OnDelete:CASCADE"`
	WorkspaceAgentInvocations  []WorkspaceAgentInvocations  `gorm:"foreignKey:WorkspaceID;constraint:OnUpdate:CASCADE,OnDelete:CASCADE"`
	PromptHistory              []PromptHistory              `gorm:"foreignKey:WorkspaceID;constraint:OnDelete:CASCADE"`
	WorkspaceParsedFiles       []WorkspaceParsedFiles       `gorm:"foreignKey:WorkspaceID;constraint:OnDelete:CASCADE"`
	Memories                   []Memories                   `gorm:"foreignKey:WorkspaceID;constraint:OnDelete:CASCADE"`
}

func (Workspaces) TableName() string {
	return "workspaces"
}

func (m *Workspaces) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.LastUpdatedAt.IsZero() {
		m.LastUpdatedAt = DateTime{Time: now}
	}
	return nil
}

type WorkspaceThreads struct {
	ID                   int                    `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	Name                 string                 `gorm:"column:name;not null" json:"name"`
	Slug                 string                 `gorm:"column:slug;uniqueIndex;not null" json:"slug"`
	WorkspaceID          int                    `gorm:"column:workspace_id;index:workspace_threads_workspace_id_idx;not null" json:"workspace_id"`
	UserID               *int                   `gorm:"column:user_id;index:workspace_threads_user_id_idx" json:"user_id"`
	CreatedAt            DateTime               `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	LastUpdatedAt        DateTime               `gorm:"column:lastUpdatedAt;not null" json:"lastUpdatedAt"`
	Workspace            *Workspaces            `gorm:"foreignKey:WorkspaceID"`
	User                 *Users                 `gorm:"foreignKey:UserID"`
	WorkspaceParsedFiles []WorkspaceParsedFiles `gorm:"foreignKey:ThreadID;constraint:OnDelete:CASCADE"`
}

func (WorkspaceThreads) TableName() string {
	return "workspace_threads"
}

func (m *WorkspaceThreads) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.LastUpdatedAt.IsZero() {
		m.LastUpdatedAt = DateTime{Time: now}
	}
	return nil
}

type WorkspaceSuggestedMessages struct {
	ID            int         `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	WorkspaceID   int         `gorm:"column:workspaceId;index:workspace_suggested_messages_workspaceId_idx;not null" json:"workspaceId"`
	Heading       string      `gorm:"column:heading;not null" json:"heading"`
	Message       string      `gorm:"column:message;not null" json:"message"`
	CreatedAt     DateTime    `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	LastUpdatedAt DateTime    `gorm:"column:lastUpdatedAt;not null" json:"lastUpdatedAt"`
	Workspace     *Workspaces `gorm:"foreignKey:WorkspaceID"`
}

func (WorkspaceSuggestedMessages) TableName() string {
	return "workspace_suggested_messages"
}

func (m *WorkspaceSuggestedMessages) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.LastUpdatedAt.IsZero() {
		m.LastUpdatedAt = DateTime{Time: now}
	}
	return nil
}

type WorkspaceChats struct {
	ID              int      `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	WorkspaceID     int      `gorm:"column:workspaceId;not null" json:"workspaceId"`
	Prompt          string   `gorm:"column:prompt;not null" json:"prompt"`
	Response        string   `gorm:"column:response;not null" json:"response"`
	Include         *bool    `gorm:"column:include;default:true;not null" json:"include"`
	UserID          *int     `gorm:"column:user_id" json:"user_id"`
	ThreadID        *int     `gorm:"column:thread_id" json:"thread_id"`
	APISessionID    *string  `gorm:"column:api_session_id" json:"api_session_id"`
	CreatedAt       DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	LastUpdatedAt   DateTime `gorm:"column:lastUpdatedAt;not null" json:"lastUpdatedAt"`
	FeedbackScore   *bool    `gorm:"column:feedbackScore" json:"feedbackScore"`
	MemoryProcessed *bool    `gorm:"column:memory_processed" json:"memory_processed"`
	Users           *Users   `gorm:"foreignKey:UserID"`
}

func (WorkspaceChats) TableName() string {
	return "workspace_chats"
}

func (m *WorkspaceChats) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.LastUpdatedAt.IsZero() {
		m.LastUpdatedAt = DateTime{Time: now}
	}
	return nil
}

type WorkspaceAgentInvocations struct {
	ID            int         `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	UUID          string      `gorm:"column:uuid;uniqueIndex;index:workspace_agent_invocations_uuid_idx;not null" json:"uuid"`
	Prompt        string      `gorm:"column:prompt;not null" json:"prompt"`
	Closed        bool        `gorm:"column:closed;default:false;not null" json:"closed"`
	UserID        *int        `gorm:"column:user_id" json:"user_id"`
	ThreadID      *int        `gorm:"column:thread_id" json:"thread_id"`
	WorkspaceID   int         `gorm:"column:workspace_id;not null" json:"workspace_id"`
	CreatedAt     DateTime    `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	LastUpdatedAt DateTime    `gorm:"column:lastUpdatedAt;not null" json:"lastUpdatedAt"`
	User          *Users      `gorm:"foreignKey:UserID"`
	Workspace     *Workspaces `gorm:"foreignKey:WorkspaceID"`
}

func (WorkspaceAgentInvocations) TableName() string {
	return "workspace_agent_invocations"
}

func (m *WorkspaceAgentInvocations) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.LastUpdatedAt.IsZero() {
		m.LastUpdatedAt = DateTime{Time: now}
	}
	return nil
}

type WorkspaceUsers struct {
	ID            int         `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	UserID        int         `gorm:"column:user_id;not null" json:"user_id"`
	WorkspaceID   int         `gorm:"column:workspace_id;not null" json:"workspace_id"`
	CreatedAt     DateTime    `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	LastUpdatedAt DateTime    `gorm:"column:lastUpdatedAt;not null" json:"lastUpdatedAt"`
	Workspaces    *Workspaces `gorm:"foreignKey:WorkspaceID"`
	Users         *Users      `gorm:"foreignKey:UserID"`
}

func (WorkspaceUsers) TableName() string {
	return "workspace_users"
}

func (m *WorkspaceUsers) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.LastUpdatedAt.IsZero() {
		m.LastUpdatedAt = DateTime{Time: now}
	}
	return nil
}

type CacheData struct {
	ID            int       `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	Name          string    `gorm:"column:name;not null" json:"name"`
	Data          string    `gorm:"column:data;not null" json:"data"`
	BelongsTo     *string   `gorm:"column:belongsTo" json:"belongsTo"`
	ByID          *int      `gorm:"column:byId" json:"byId"`
	ExpiresAt     *DateTime `gorm:"column:expiresAt" json:"expiresAt"`
	CreatedAt     DateTime  `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	LastUpdatedAt DateTime  `gorm:"column:lastUpdatedAt;not null" json:"lastUpdatedAt"`
}

func (CacheData) TableName() string {
	return "cache_data"
}

func (m *CacheData) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.LastUpdatedAt.IsZero() {
		m.LastUpdatedAt = DateTime{Time: now}
	}
	return nil
}

type EmbedConfigs struct {
	ID                       int         `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	UUID                     string      `gorm:"column:uuid;uniqueIndex;not null" json:"uuid"`
	Enabled                  bool        `gorm:"column:enabled;default:false;not null" json:"enabled"`
	ChatMode                 *string     `gorm:"column:chat_mode;default:'query';not null" json:"chat_mode"`
	AllowlistDomains         *string     `gorm:"column:allowlist_domains" json:"allowlist_domains"`
	AllowModelOverride       bool        `gorm:"column:allow_model_override;default:false;not null" json:"allow_model_override"`
	AllowTemperatureOverride bool        `gorm:"column:allow_temperature_override;default:false;not null" json:"allow_temperature_override"`
	AllowPromptOverride      bool        `gorm:"column:allow_prompt_override;default:false;not null" json:"allow_prompt_override"`
	MaxChatsPerDay           *int        `gorm:"column:max_chats_per_day" json:"max_chats_per_day"`
	MaxChatsPerSession       *int        `gorm:"column:max_chats_per_session" json:"max_chats_per_session"`
	MessageLimit             *int        `gorm:"column:message_limit;default:20" json:"message_limit"`
	WorkspaceID              int         `gorm:"column:workspace_id;not null" json:"workspace_id"`
	CreatedBy                *int        `gorm:"column:createdBy" json:"createdBy"`
	UsersID                  *int        `gorm:"column:usersId" json:"usersId"`
	CreatedAt                DateTime    `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	Workspace                *Workspaces `gorm:"foreignKey:WorkspaceID"`
	Users                    *Users
	EmbedChats               []EmbedChats `gorm:"foreignKey:EmbedID;constraint:OnDelete:CASCADE"`
}

func (EmbedConfigs) TableName() string {
	return "embed_configs"
}

func (m *EmbedConfigs) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type EmbedChats struct {
	ID                    int           `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	Prompt                string        `gorm:"column:prompt;not null" json:"prompt"`
	Response              string        `gorm:"column:response;not null" json:"response"`
	SessionID             string        `gorm:"column:session_id;not null" json:"session_id"`
	Include               *bool         `gorm:"column:include;default:true;not null" json:"include"`
	ConnectionInformation *string       `gorm:"column:connection_information" json:"connection_information"`
	EmbedID               int           `gorm:"column:embed_id;not null" json:"embed_id"`
	UsersID               *int          `gorm:"column:usersId" json:"usersId"`
	CreatedAt             DateTime      `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	EmbedConfig           *EmbedConfigs `gorm:"foreignKey:EmbedID"`
	Users                 *Users
}

func (EmbedChats) TableName() string {
	return "embed_chats"
}

func (m *EmbedChats) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type EventLogs struct {
	ID         int      `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	Event      string   `gorm:"column:event;index:event_logs_event_idx;not null" json:"event"`
	Metadata   *string  `gorm:"column:metadata" json:"metadata"`
	UserID     *int     `gorm:"column:userId" json:"userId"`
	OccurredAt DateTime `gorm:"column:occurredAt;not null" json:"occurredAt"`
}

func (EventLogs) TableName() string {
	return "event_logs"
}

func (m *EventLogs) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.OccurredAt.IsZero() {
		m.OccurredAt = DateTime{Time: now}
	}
	return nil
}

type SlashCommandPresets struct {
	ID            int      `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	Command       string   `gorm:"column:command;uniqueIndex:slash_command_presets_uid_command_key;not null" json:"command"`
	Prompt        string   `gorm:"column:prompt;not null" json:"prompt"`
	Description   string   `gorm:"column:description;not null" json:"description"`
	UID           int      `gorm:"column:uid;uniqueIndex:slash_command_presets_uid_command_key;default:0;not null" json:"uid"`
	UserID        *int     `gorm:"column:userId" json:"userId"`
	CreatedAt     DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	LastUpdatedAt DateTime `gorm:"column:lastUpdatedAt;not null" json:"lastUpdatedAt"`
	User          *Users   `gorm:"foreignKey:UserID"`
}

func (SlashCommandPresets) TableName() string {
	return "slash_command_presets"
}

func (m *SlashCommandPresets) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.LastUpdatedAt.IsZero() {
		m.LastUpdatedAt = DateTime{Time: now}
	}
	return nil
}

type DocumentSyncQueues struct {
	ID             int                      `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	StaleAfterMs   *int                     `gorm:"column:staleAfterMs;default:604800000;not null" json:"staleAfterMs"`
	NextSyncAt     DateTime                 `gorm:"column:nextSyncAt;not null" json:"nextSyncAt"`
	CreatedAt      DateTime                 `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	LastSyncedAt   DateTime                 `gorm:"column:lastSyncedAt;not null" json:"lastSyncedAt"`
	WorkspaceDocID int                      `gorm:"column:workspaceDocId;uniqueIndex;not null" json:"workspaceDocId"`
	WorkspaceDoc   *WorkspaceDocuments      `gorm:"foreignKey:WorkspaceDocID"`
	Runs           []DocumentSyncExecutions `gorm:"foreignKey:QueueID;constraint:OnDelete:CASCADE"`
}

func (DocumentSyncQueues) TableName() string {
	return "document_sync_queues"
}

func (m *DocumentSyncQueues) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.LastSyncedAt.IsZero() {
		m.LastSyncedAt = DateTime{Time: now}
	}
	return nil
}

type DocumentSyncExecutions struct {
	ID        int                 `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	QueueID   int                 `gorm:"column:queueId;not null" json:"queueId"`
	Status    *string             `gorm:"column:status;default:'unknown';not null" json:"status"`
	Result    *string             `gorm:"column:result" json:"result"`
	CreatedAt DateTime            `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	Queue     *DocumentSyncQueues `gorm:"foreignKey:QueueID"`
}

func (DocumentSyncExecutions) TableName() string {
	return "document_sync_executions"
}

func (m *DocumentSyncExecutions) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type BrowserExtensionAPIKeys struct {
	ID            int      `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	Key           string   `gorm:"column:key;uniqueIndex;not null" json:"key"`
	UserID        *int     `gorm:"column:user_id;index:browser_extension_api_keys_user_id_idx" json:"user_id"`
	CreatedAt     DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	LastUpdatedAt DateTime `gorm:"column:lastUpdatedAt;not null" json:"lastUpdatedAt"`
	User          *Users   `gorm:"foreignKey:UserID"`
}

func (BrowserExtensionAPIKeys) TableName() string {
	return "browser_extension_api_keys"
}

func (m *BrowserExtensionAPIKeys) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.LastUpdatedAt.IsZero() {
		m.LastUpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*BrowserExtensionAPIKeys) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("LastUpdatedAt") {
		tx.Statement.SetColumn("LastUpdatedAt", DateTime{Time: now})
	}
	return nil
}

type TemporaryAuthTokens struct {
	ID        int      `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	Token     string   `gorm:"column:token;uniqueIndex;index:temporary_auth_tokens_token_idx;not null" json:"token"`
	UserID    int      `gorm:"column:userId;index:temporary_auth_tokens_userId_idx;not null" json:"userId"`
	ExpiresAt DateTime `gorm:"column:expiresAt;not null" json:"expiresAt"`
	CreatedAt DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	User      *Users   `gorm:"foreignKey:UserID"`
}

func (TemporaryAuthTokens) TableName() string {
	return "temporary_auth_tokens"
}

func (m *TemporaryAuthTokens) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type SystemPromptVariables struct {
	ID          int      `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	Key         string   `gorm:"column:key;uniqueIndex;not null" json:"key"`
	Value       *string  `gorm:"column:value" json:"value"`
	Description *string  `gorm:"column:description" json:"description"`
	Type        *string  `gorm:"column:type;default:'system';not null" json:"type"`
	UserID      *int     `gorm:"column:userId;index:system_prompt_variables_userId_idx" json:"userId"`
	CreatedAt   DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt   DateTime `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	User        *Users   `gorm:"foreignKey:UserID"`
}

func (SystemPromptVariables) TableName() string {
	return "system_prompt_variables"
}

func (m *SystemPromptVariables) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*SystemPromptVariables) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type PromptHistory struct {
	ID          int         `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	WorkspaceID int         `gorm:"column:workspaceId;index:prompt_history_workspaceId_idx;not null" json:"workspaceId"`
	Prompt      string      `gorm:"column:prompt;not null" json:"prompt"`
	ModifiedBy  *int        `gorm:"column:modifiedBy" json:"modifiedBy"`
	ModifiedAt  DateTime    `gorm:"column:modifiedAt;not null" json:"modifiedAt"`
	Workspace   *Workspaces `gorm:"foreignKey:WorkspaceID"`
	User        *Users      `gorm:"foreignKey:ModifiedBy"`
}

func (PromptHistory) TableName() string {
	return "prompt_history"
}

func (m *PromptHistory) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ModifiedAt.IsZero() {
		m.ModifiedAt = DateTime{Time: now}
	}
	return nil
}

type DesktopMobileDevices struct {
	ID         int      `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	DeviceOs   string   `gorm:"column:deviceOs;not null" json:"deviceOs"`
	DeviceName string   `gorm:"column:deviceName;not null" json:"deviceName"`
	Token      string   `gorm:"column:token;uniqueIndex;not null" json:"token"`
	Approved   bool     `gorm:"column:approved;default:false;not null" json:"approved"`
	UserID     *int     `gorm:"column:userId;index:desktop_mobile_devices_userId_idx" json:"userId"`
	CreatedAt  DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	User       *Users   `gorm:"foreignKey:UserID"`
}

func (DesktopMobileDevices) TableName() string {
	return "desktop_mobile_devices"
}

func (m *DesktopMobileDevices) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type WorkspaceParsedFiles struct {
	ID                 int               `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	Filename           string            `gorm:"column:filename;uniqueIndex;not null" json:"filename"`
	WorkspaceID        int               `gorm:"column:workspaceId;index:workspace_parsed_files_workspaceId_idx;not null" json:"workspaceId"`
	UserID             *int              `gorm:"column:userId;index:workspace_parsed_files_userId_idx" json:"userId"`
	ThreadID           *int              `gorm:"column:threadId" json:"threadId"`
	Metadata           *string           `gorm:"column:metadata" json:"metadata"`
	TokenCountEstimate *int              `gorm:"column:tokenCountEstimate;default:0" json:"tokenCountEstimate"`
	CreatedAt          DateTime          `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	Workspace          *Workspaces       `gorm:"foreignKey:WorkspaceID"`
	User               *Users            `gorm:"foreignKey:UserID"`
	Thread             *WorkspaceThreads `gorm:"foreignKey:ThreadID"`
}

func (WorkspaceParsedFiles) TableName() string {
	return "workspace_parsed_files"
}

func (m *WorkspaceParsedFiles) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type ExternalCommunicationConnectors struct {
	ID            int      `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	Type          string   `gorm:"column:type;uniqueIndex;not null" json:"type"`
	Config        *string  `gorm:"column:config;default:'{}';not null" json:"config"`
	Active        bool     `gorm:"column:active;default:false;not null" json:"active"`
	CreatedAt     DateTime `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	LastUpdatedAt DateTime `gorm:"column:lastUpdatedAt;not null" json:"lastUpdatedAt"`
}

func (ExternalCommunicationConnectors) TableName() string {
	return "external_communication_connectors"
}

func (m *ExternalCommunicationConnectors) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.LastUpdatedAt.IsZero() {
		m.LastUpdatedAt = DateTime{Time: now}
	}
	return nil
}

type ScheduledJobs struct {
	ID        int                `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	Name      string             `gorm:"column:name;not null" json:"name"`
	Prompt    string             `gorm:"column:prompt;not null" json:"prompt"`
	Tools     *string            `gorm:"column:tools" json:"tools"`
	Schedule  string             `gorm:"column:schedule;not null" json:"schedule"`
	Enabled   *bool              `gorm:"column:enabled;default:true;not null" json:"enabled"`
	LastRunAt *DateTime          `gorm:"column:lastRunAt" json:"lastRunAt"`
	NextRunAt *DateTime          `gorm:"column:nextRunAt" json:"nextRunAt"`
	CreatedAt DateTime           `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	UpdatedAt DateTime           `gorm:"column:updatedAt;autoUpdateTime:false;not null" json:"updatedAt"`
	Runs      []ScheduledJobRuns `gorm:"foreignKey:JobID;constraint:OnDelete:CASCADE"`
}

func (ScheduledJobs) TableName() string {
	return "scheduled_jobs"
}

func (m *ScheduledJobs) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

type ScheduledJobRuns struct {
	ID          int            `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	JobID       int            `gorm:"column:jobId;index:scheduled_job_runs_jobId_idx;not null" json:"jobId"`
	Status      *string        `gorm:"column:status;default:'queued';not null" json:"status"`
	Result      *string        `gorm:"column:result" json:"result"`
	Error       *string        `gorm:"column:error" json:"error"`
	StartedAt   DateTime       `gorm:"column:startedAt;not null" json:"startedAt"`
	CompletedAt *DateTime      `gorm:"column:completedAt" json:"completedAt"`
	ReadAt      *DateTime      `gorm:"column:readAt" json:"readAt"`
	Job         *ScheduledJobs `gorm:"foreignKey:JobID"`
}

func (ScheduledJobRuns) TableName() string {
	return "scheduled_job_runs"
}

func (m *ScheduledJobRuns) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.StartedAt.IsZero() {
		m.StartedAt = DateTime{Time: now}
	}
	return nil
}

type Memories struct {
	ID          int         `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	UserID      *int        `gorm:"column:user_id;index:memories_user_id_workspace_id_idx;index:memories_user_id_scope_idx" json:"user_id"`
	WorkspaceID *int        `gorm:"column:workspace_id;index:memories_user_id_workspace_id_idx" json:"workspace_id"`
	Scope       *string     `gorm:"column:scope;index:memories_user_id_scope_idx;default:'workspace';not null" json:"scope"`
	Content     string      `gorm:"column:content;not null" json:"content"`
	LastUsedAt  *DateTime   `gorm:"column:last_used_at" json:"last_used_at"`
	CreatedAt   DateTime    `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt   DateTime    `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	User        *Users      `gorm:"foreignKey:UserID"`
	Workspace   *Workspaces `gorm:"foreignKey:WorkspaceID"`
}

func (Memories) TableName() string {
	return "memories"
}

func (m *Memories) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

type ModelRouters struct {
	ID               int                `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	Name             string             `gorm:"column:name;uniqueIndex;not null" json:"name"`
	Description      *string            `gorm:"column:description" json:"description"`
	FallbackProvider string             `gorm:"column:fallback_provider;not null" json:"fallback_provider"`
	FallbackModel    string             `gorm:"column:fallback_model;not null" json:"fallback_model"`
	CooldownSeconds  *int               `gorm:"column:cooldown_seconds;default:30;not null" json:"cooldown_seconds"`
	CreatedBy        *int               `gorm:"column:created_by" json:"created_by"`
	CreatedAt        DateTime           `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	LastUpdatedAt    DateTime           `gorm:"column:lastUpdatedAt;not null" json:"lastUpdatedAt"`
	Rules            []ModelRouterRules `gorm:"foreignKey:RouterID;constraint:OnDelete:CASCADE"`
}

func (ModelRouters) TableName() string {
	return "model_routers"
}

func (m *ModelRouters) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.LastUpdatedAt.IsZero() {
		m.LastUpdatedAt = DateTime{Time: now}
	}
	return nil
}

type ModelRouterRules struct {
	ID             int           `gorm:"column:id;primaryKey;autoIncrement" json:"id"`
	RouterID       int           `gorm:"column:router_id;uniqueIndex:model_router_rules_router_id_title_key;index:model_router_rules_router_id_idx;index:model_router_rules_router_id_enabled_priority_idx;not null" json:"router_id"`
	Enabled        *bool         `gorm:"column:enabled;index:model_router_rules_router_id_enabled_priority_idx;default:true;not null" json:"enabled"`
	Priority       int           `gorm:"column:priority;index:model_router_rules_router_id_enabled_priority_idx;not null" json:"priority"`
	Type           *string       `gorm:"column:type;default:'calculated';not null" json:"type"`
	Title          string        `gorm:"column:title;uniqueIndex:model_router_rules_router_id_title_key;not null" json:"title"`
	Description    *string       `gorm:"column:description" json:"description"`
	ConditionLogic *string       `gorm:"column:condition_logic" json:"condition_logic"`
	Conditions     *string       `gorm:"column:conditions" json:"conditions"`
	RouteProvider  string        `gorm:"column:route_provider;not null" json:"route_provider"`
	RouteModel     string        `gorm:"column:route_model;not null" json:"route_model"`
	CreatedBy      *int          `gorm:"column:created_by" json:"created_by"`
	CreatedAt      DateTime      `gorm:"column:createdAt;autoCreateTime:false;not null" json:"createdAt"`
	LastUpdatedAt  DateTime      `gorm:"column:lastUpdatedAt;not null" json:"lastUpdatedAt"`
	Router         *ModelRouters `gorm:"foreignKey:RouterID"`
}

func (ModelRouterRules) TableName() string {
	return "model_router_rules"
}

func (m *ModelRouterRules) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.LastUpdatedAt.IsZero() {
		m.LastUpdatedAt = DateTime{Time: now}
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
