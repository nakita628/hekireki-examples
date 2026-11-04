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

type Space struct {
	ID                         string                    `gorm:"column:id;primaryKey" json:"id"`
	Name                       string                    `gorm:"column:name;not null" json:"name"`
	Avatar                     *string                   `gorm:"column:avatar" json:"avatar"`
	Credit                     *int                      `gorm:"column:credit" json:"credit"`
	DeletedTime                *DateTime                 `gorm:"column:deleted_time" json:"deleted_time"`
	CreatedTime                DateTime                  `gorm:"column:created_time;not null" json:"created_time"`
	CreatedBy                  string                    `gorm:"column:created_by;not null" json:"created_by"`
	LastModifiedBy             *string                   `gorm:"column:last_modified_by" json:"last_modified_by"`
	LastModifiedTime           *DateTime                 `gorm:"column:last_modified_time" json:"last_modified_time"`
	IsTemplate                 *bool                     `gorm:"column:is_template" json:"is_template"`
	BaseGroup                  []Base                    `gorm:"foreignKey:SpaceID"`
	DataDbMigrationJobs        []SpaceDataDbMigrationJob `gorm:"foreignKey:SpaceID;constraint:OnDelete:CASCADE"`
	BaseDataDbMoveJobsAsSource []BaseDataDbMoveJob       `gorm:"foreignKey:SourceSpaceID;constraint:OnDelete:CASCADE"`
	BaseDataDbMoveJobsAsTarget []BaseDataDbMoveJob       `gorm:"foreignKey:TargetSpaceID;constraint:OnDelete:CASCADE"`
	DataDbBinding              *SpaceDataDbBinding       `gorm:"foreignKey:SpaceID;constraint:OnDelete:CASCADE"`
}

func (Space) TableName() string {
	return "space"
}

func (m *Space) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedTime.IsZero() {
		m.CreatedTime = DateTime{Time: now}
	}
	if m.LastModifiedTime == nil {
		m.LastModifiedTime = &DateTime{Time: now}
	}
	return nil
}

func (*Space) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("LastModifiedTime") {
		tx.Statement.SetColumn("LastModifiedTime", &DateTime{Time: now})
	}
	return nil
}

type DataDbConnection struct {
	ID                        string                    `gorm:"column:id;primaryKey" json:"id"`
	Provider                  string                    `gorm:"column:provider;default:'postgres';not null" json:"provider"`
	EncryptedURL              string                    `gorm:"column:encrypted_url;not null" json:"encrypted_url"`
	URLFingerprint            string                    `gorm:"column:url_fingerprint;uniqueIndex;not null" json:"url_fingerprint"`
	DisplayHost               *string                   `gorm:"column:display_host" json:"display_host"`
	DisplayDatabase           *string                   `gorm:"column:display_database" json:"display_database"`
	InternalSchema            string                    `gorm:"column:internal_schema;not null" json:"internal_schema"`
	Status                    string                    `gorm:"column:status;default:'pending';not null" json:"status"`
	SchemaVersion             *string                   `gorm:"column:schema_version" json:"schema_version"`
	Capabilities              datatypes.JSON            `gorm:"column:capabilities" json:"capabilities"`
	LastValidatedAt           *DateTime                 `gorm:"column:last_validated_at" json:"last_validated_at"`
	LastError                 *string                   `gorm:"column:last_error" json:"last_error"`
	HealthState               string                    `gorm:"column:health_state;default:'healthy';not null" json:"health_state"`
	HealthReason              *string                   `gorm:"column:health_reason" json:"health_reason"`
	HealthChangedAt           *DateTime                 `gorm:"column:health_changed_at" json:"health_changed_at"`
	LastHealthCheckAt         *DateTime                 `gorm:"column:last_health_check_at" json:"last_health_check_at"`
	ConsecutiveHealthFailures int                       `gorm:"column:consecutive_health_failures;default:0;not null" json:"consecutive_health_failures"`
	CreatedBy                 string                    `gorm:"column:created_by;not null" json:"created_by"`
	CreatedTime               DateTime                  `gorm:"column:created_time;not null" json:"created_time"`
	LastModifiedTime          *DateTime                 `gorm:"column:last_modified_time" json:"last_modified_time"`
	SpaceBindings             []SpaceDataDbBinding      `gorm:"foreignKey:DataDbConnectionID"`
	SourceMigrationJobs       []SpaceDataDbMigrationJob `gorm:"foreignKey:SourceConnectionID"`
	TargetMigrationJobs       []SpaceDataDbMigrationJob `gorm:"foreignKey:TargetConnectionID"`
}

func (DataDbConnection) TableName() string {
	return "data_db_connection"
}

func (m *DataDbConnection) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedTime.IsZero() {
		m.CreatedTime = DateTime{Time: now}
	}
	if m.LastModifiedTime == nil {
		m.LastModifiedTime = &DateTime{Time: now}
	}
	return nil
}

func (*DataDbConnection) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("LastModifiedTime") {
		tx.Statement.SetColumn("LastModifiedTime", &DateTime{Time: now})
	}
	return nil
}

type SpaceDataDbBinding struct {
	ID                   string    `gorm:"column:id;primaryKey" json:"id"`
	SpaceID              string    `gorm:"column:space_id;uniqueIndex;not null" json:"space_id"`
	DataDbConnectionID   *string   `gorm:"column:data_db_connection_id;index:space_data_db_binding_data_db_connection_id_idx" json:"data_db_connection_id"`
	Mode                 string    `gorm:"column:mode;default:'default';not null" json:"mode"`
	State                string    `gorm:"column:state;default:'ready';not null" json:"state"`
	CreatedBy            string    `gorm:"column:created_by;not null" json:"created_by"`
	CreatedTime          DateTime  `gorm:"column:created_time;not null" json:"created_time"`
	LastModifiedTime     *DateTime `gorm:"column:last_modified_time" json:"last_modified_time"`
	LastHistoryFlushedAt *DateTime `gorm:"column:last_history_flushed_at" json:"last_history_flushed_at"`
	LastRemovalFlushedAt *DateTime `gorm:"column:last_removal_flushed_at" json:"last_removal_flushed_at"`
	Space                *Space
	DataDbConnection     *DataDbConnection
}

func (SpaceDataDbBinding) TableName() string {
	return "space_data_db_binding"
}

func (m *SpaceDataDbBinding) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedTime.IsZero() {
		m.CreatedTime = DateTime{Time: now}
	}
	if m.LastModifiedTime == nil {
		m.LastModifiedTime = &DateTime{Time: now}
	}
	return nil
}

func (*SpaceDataDbBinding) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("LastModifiedTime") {
		tx.Statement.SetColumn("LastModifiedTime", &DateTime{Time: now})
	}
	return nil
}

type SpaceDataDbMigrationJob struct {
	ID                   string         `gorm:"column:id;primaryKey" json:"id"`
	SpaceID              string         `gorm:"column:space_id;index:space_data_db_migration_job_space_id_state_idx;not null" json:"space_id"`
	SourceConnectionID   *string        `gorm:"column:source_connection_id;index:space_data_db_migration_job_source_connection_id_idx" json:"source_connection_id"`
	TargetConnectionID   *string        `gorm:"column:target_connection_id;index:space_data_db_migration_job_target_connection_id_idx" json:"target_connection_id"`
	TargetMode           *string        `gorm:"column:target_mode;default:'migrate-space';not null" json:"target_mode"`
	SwitchOnCompletion   bool           `gorm:"column:switch_on_completion;default:false;not null" json:"switch_on_completion"`
	State                string         `gorm:"column:state;index:space_data_db_migration_job_space_id_state_idx;default:'pending';not null" json:"state"`
	TargetURLFingerprint string         `gorm:"column:target_url_fingerprint;not null" json:"target_url_fingerprint"`
	TargetInternalSchema string         `gorm:"column:target_internal_schema;not null" json:"target_internal_schema"`
	Inventory            datatypes.JSON `gorm:"column:inventory" json:"inventory"`
	CopyStats            datatypes.JSON `gorm:"column:copy_stats" json:"copy_stats"`
	ValidationStats      datatypes.JSON `gorm:"column:validation_stats" json:"validation_stats"`
	LastError            *string        `gorm:"column:last_error" json:"last_error"`
	StartedAt            *DateTime      `gorm:"column:started_at" json:"started_at"`
	CompletedAt          *DateTime      `gorm:"column:completed_at" json:"completed_at"`
	CreatedBy            string         `gorm:"column:created_by;not null" json:"created_by"`
	CreatedTime          DateTime       `gorm:"column:created_time;not null" json:"created_time"`
	LastModifiedTime     *DateTime      `gorm:"column:last_modified_time" json:"last_modified_time"`
	Space                *Space
	SourceConnection     *DataDbConnection `gorm:"foreignKey:SourceConnectionID"`
	TargetConnection     *DataDbConnection `gorm:"foreignKey:TargetConnectionID"`
}

func (SpaceDataDbMigrationJob) TableName() string {
	return "space_data_db_migration_job"
}

func (m *SpaceDataDbMigrationJob) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedTime.IsZero() {
		m.CreatedTime = DateTime{Time: now}
	}
	if m.LastModifiedTime == nil {
		m.LastModifiedTime = &DateTime{Time: now}
	}
	return nil
}

func (*SpaceDataDbMigrationJob) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("LastModifiedTime") {
		tx.Statement.SetColumn("LastModifiedTime", &DateTime{Time: now})
	}
	return nil
}

type BaseDataDbMoveJob struct {
	ID                 string         `gorm:"column:id;primaryKey" json:"id"`
	BaseID             string         `gorm:"column:base_id;index:base_data_db_move_job_base_id_state_idx;not null" json:"base_id"`
	SourceSpaceID      string         `gorm:"column:source_space_id;index:base_data_db_move_job_source_space_id_state_idx;not null" json:"source_space_id"`
	TargetSpaceID      string         `gorm:"column:target_space_id;index:base_data_db_move_job_target_space_id_state_idx;not null" json:"target_space_id"`
	SourceConnectionID *string        `gorm:"column:source_connection_id" json:"source_connection_id"`
	TargetConnectionID *string        `gorm:"column:target_connection_id" json:"target_connection_id"`
	State              string         `gorm:"column:state;index:base_data_db_move_job_base_id_state_idx;index:base_data_db_move_job_state_idx;index:base_data_db_move_job_source_space_id_state_idx;index:base_data_db_move_job_target_space_id_state_idx;default:'pending';not null" json:"state"`
	Inventory          datatypes.JSON `gorm:"column:inventory" json:"inventory"`
	CopyStats          datatypes.JSON `gorm:"column:copy_stats" json:"copy_stats"`
	ValidationStats    datatypes.JSON `gorm:"column:validation_stats" json:"validation_stats"`
	LastError          *string        `gorm:"column:last_error" json:"last_error"`
	StartedAt          *DateTime      `gorm:"column:started_at" json:"started_at"`
	CompletedAt        *DateTime      `gorm:"column:completed_at" json:"completed_at"`
	CreatedBy          string         `gorm:"column:created_by;not null" json:"created_by"`
	CreatedTime        DateTime       `gorm:"column:created_time;not null" json:"created_time"`
	LastModifiedTime   *DateTime      `gorm:"column:last_modified_time" json:"last_modified_time"`
	Base               *Base
	SourceSpace        *Space `gorm:"foreignKey:SourceSpaceID"`
	TargetSpace        *Space `gorm:"foreignKey:TargetSpaceID"`
}

func (BaseDataDbMoveJob) TableName() string {
	return "base_data_db_move_job"
}

func (m *BaseDataDbMoveJob) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedTime.IsZero() {
		m.CreatedTime = DateTime{Time: now}
	}
	if m.LastModifiedTime == nil {
		m.LastModifiedTime = &DateTime{Time: now}
	}
	return nil
}

func (*BaseDataDbMoveJob) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("LastModifiedTime") {
		tx.Statement.SetColumn("LastModifiedTime", &DateTime{Time: now})
	}
	return nil
}

type PinResource struct {
	ID          string   `gorm:"column:id;primaryKey" json:"id"`
	Type        string   `gorm:"column:type;not null" json:"type"`
	ResourceID  string   `gorm:"column:resource_id;uniqueIndex:pin_resource_created_by_resource_id_key;not null" json:"resource_id"`
	CreatedTime DateTime `gorm:"column:created_time;not null" json:"created_time"`
	CreatedBy   string   `gorm:"column:created_by;uniqueIndex:pin_resource_created_by_resource_id_key;not null" json:"created_by"`
	Order       float64  `gorm:"column:order;index:pin_resource_order_idx;not null" json:"order"`
}

func (PinResource) TableName() string {
	return "pin_resource"
}

func (m *PinResource) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedTime.IsZero() {
		m.CreatedTime = DateTime{Time: now}
	}
	return nil
}

type Base struct {
	ID               string    `gorm:"column:id;primaryKey" json:"id"`
	SpaceID          string    `gorm:"column:space_id;index:base_space_id_idx;not null" json:"space_id"`
	Name             *string   `gorm:"column:name" json:"name"`
	Order            float64   `gorm:"column:order;index:base_order_idx;not null" json:"order"`
	Icon             *string   `gorm:"column:icon" json:"icon"`
	SchemaPass       *string   `gorm:"column:schema_pass" json:"schema_pass"`
	V2Enabled        bool      `gorm:"column:v2_enabled;default:false;not null" json:"v2_enabled"`
	ProvisionState   string    `gorm:"column:provision_state;default:'ready';not null" json:"provision_state"`
	DeletedTime      *DateTime `gorm:"column:deleted_time" json:"deleted_time"`
	CreatedTime      DateTime  `gorm:"column:created_time;not null" json:"created_time"`
	CreatedBy        string    `gorm:"column:created_by;not null" json:"created_by"`
	LastModifiedBy   *string   `gorm:"column:last_modified_by" json:"last_modified_by"`
	LastModifiedTime *DateTime `gorm:"column:last_modified_time" json:"last_modified_time"`
	Space            *Space
	Tables           []TableMeta         `gorm:"foreignKey:BaseID"`
	DataDbMoveJobs   []BaseDataDbMoveJob `gorm:"foreignKey:BaseID;constraint:OnDelete:CASCADE"`
	UserOrders       []UserBaseOrder     `gorm:"foreignKey:BaseID;constraint:OnDelete:CASCADE"`
}

func (Base) TableName() string {
	return "base"
}

func (m *Base) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedTime.IsZero() {
		m.CreatedTime = DateTime{Time: now}
	}
	if m.LastModifiedTime == nil {
		m.LastModifiedTime = &DateTime{Time: now}
	}
	return nil
}

func (*Base) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("LastModifiedTime") {
		tx.Statement.SetColumn("LastModifiedTime", &DateTime{Time: now})
	}
	return nil
}

type TableMeta struct {
	ID                string         `gorm:"column:id;primaryKey" json:"id"`
	BaseID            string         `gorm:"column:base_id;index:table_meta_base_id_deleted_time_idx;not null" json:"base_id"`
	Name              string         `gorm:"column:name;not null" json:"name"`
	Description       *string        `gorm:"column:description" json:"description"`
	Icon              *string        `gorm:"column:icon" json:"icon"`
	DbTableName       string         `gorm:"column:db_table_name;index:table_meta_db_table_name_idx;not null" json:"db_table_name"`
	DbViewName        *string        `gorm:"column:db_view_name" json:"db_view_name"`
	SearchIndex       datatypes.JSON `gorm:"column:search_index;type:jsonb" json:"search_index"`
	SearchIndexPolicy datatypes.JSON `gorm:"column:search_index_policy;type:jsonb" json:"search_index_policy"`
	QueryIndexPolicy  datatypes.JSON `gorm:"column:query_index_policy;type:jsonb" json:"query_index_policy"`
	ProvisionState    string         `gorm:"column:provision_state;default:'ready';not null" json:"provision_state"`
	Version           int            `gorm:"column:version;not null" json:"version"`
	Order             float64        `gorm:"column:order;index:table_meta_order_idx;not null" json:"order"`
	CreatedTime       DateTime       `gorm:"column:created_time;not null" json:"created_time"`
	LastModifiedTime  *DateTime      `gorm:"column:last_modified_time" json:"last_modified_time"`
	DeletedTime       *DateTime      `gorm:"column:deleted_time;index:table_meta_base_id_deleted_time_idx" json:"deleted_time"`
	CreatedBy         string         `gorm:"column:created_by;not null" json:"created_by"`
	LastModifiedBy    *string        `gorm:"column:last_modified_by" json:"last_modified_by"`
	Base              *Base
	Fields            []Field             `gorm:"foreignKey:TableID"`
	Views             []View              `gorm:"foreignKey:TableID"`
	PluginPanel       []PluginPanel       `gorm:"foreignKey:TableID;constraint:OnDelete:CASCADE"`
	PluginContextMenu []PluginContextMenu `gorm:"foreignKey:TableID;constraint:OnDelete:CASCADE"`
}

func (TableMeta) TableName() string {
	return "table_meta"
}

func (m *TableMeta) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedTime.IsZero() {
		m.CreatedTime = DateTime{Time: now}
	}
	if m.LastModifiedTime == nil {
		m.LastModifiedTime = &DateTime{Time: now}
	}
	return nil
}

func (*TableMeta) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("LastModifiedTime") {
		tx.Statement.SetColumn("LastModifiedTime", &DateTime{Time: now})
	}
	return nil
}

type TableQueryObservationShard struct {
	SpaceID           *string        `gorm:"column:space_id" json:"space_id"`
	BaseID            string         `gorm:"column:base_id;index:table_query_observation_shard_base_start_idx;not null" json:"base_id"`
	TableID           string         `gorm:"column:table_id;primaryKey;index:table_query_observation_shard_table_start_idx;index:table_query_observation_shard_search_activity_idx" json:"table_id"`
	QueryKind         string         `gorm:"column:query_kind;primaryKey;index:table_query_observation_shard_search_activity_idx" json:"query_kind"`
	ShapeHash         string         `gorm:"column:shape_hash;primaryKey" json:"shape_hash"`
	WindowStart       DateTime       `gorm:"column:window_start;primaryKey;index:table_query_observation_shard_window_start_idx;index:table_query_observation_shard_table_start_idx;index:table_query_observation_shard_search_activity_idx;index:table_query_observation_shard_base_start_idx;type:timestamptz(6)" json:"window_start"`
	WriterID          string         `gorm:"column:writer_id;primaryKey" json:"writer_id"`
	WindowSizeSeconds int            `gorm:"column:window_size_seconds;not null" json:"window_size_seconds"`
	RequestCount      int            `gorm:"column:request_count;not null" json:"request_count"`
	SlowCount         int            `gorm:"column:slow_count;not null" json:"slow_count"`
	TimeoutCount      int            `gorm:"column:timeout_count;not null" json:"timeout_count"`
	DbErrorCount      int            `gorm:"column:db_error_count;not null" json:"db_error_count"`
	TotalDurationMs   float64        `gorm:"column:total_duration_ms;not null" json:"total_duration_ms"`
	MaxDurationMs     float64        `gorm:"column:max_duration_ms;not null" json:"max_duration_ms"`
	TotalDbDurationMs *float64       `gorm:"column:total_db_duration_ms" json:"total_db_duration_ms"`
	MaxDbDurationMs   *float64       `gorm:"column:max_db_duration_ms" json:"max_db_duration_ms"`
	Shape             datatypes.JSON `gorm:"column:shape;not null" json:"shape"`
	SQLDiagnostics    datatypes.JSON `gorm:"column:sql_diagnostics" json:"sql_diagnostics"`
	CreatedTime       DateTime       `gorm:"column:created_time;type:timestamptz(6);not null" json:"created_time"`
	LastModifiedTime  *DateTime      `gorm:"column:last_modified_time;type:timestamptz(6)" json:"last_modified_time"`
}

func (TableQueryObservationShard) TableName() string {
	return "table_query_observation_shard"
}

func (m *TableQueryObservationShard) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedTime.IsZero() {
		m.CreatedTime = DateTime{Time: now}
	}
	return nil
}

type SchemaOperation struct {
	ID               string         `gorm:"column:id;primaryKey" json:"id"`
	Type             string         `gorm:"column:type;not null" json:"type"`
	Status           string         `gorm:"column:status;index:schema_operation_status_next_run_at_idx;index:schema_operation_resource_status_idx;index:schema_operation_base_status_idx;index:schema_operation_table_status_idx;not null" json:"status"`
	Phase            string         `gorm:"column:phase;not null" json:"phase"`
	ResourceType     string         `gorm:"column:resource_type;index:schema_operation_resource_status_idx;not null" json:"resource_type"`
	ResourceID       string         `gorm:"column:resource_id;index:schema_operation_resource_status_idx;not null" json:"resource_id"`
	BaseID           *string        `gorm:"column:base_id;index:schema_operation_base_status_idx" json:"base_id"`
	TableID          *string        `gorm:"column:table_id;index:schema_operation_table_status_idx" json:"table_id"`
	IdempotencyKey   string         `gorm:"column:idempotency_key;uniqueIndex;not null" json:"idempotency_key"`
	Payload          datatypes.JSON `gorm:"column:payload" json:"payload"`
	Result           datatypes.JSON `gorm:"column:result" json:"result"`
	Attempts         int            `gorm:"column:attempts;default:0;not null" json:"attempts"`
	MaxAttempts      *int           `gorm:"column:max_attempts;default:8;not null" json:"max_attempts"`
	NextRunAt        DateTime       `gorm:"column:next_run_at;index:schema_operation_status_next_run_at_idx;type:timestamptz(6);not null" json:"next_run_at"`
	LockedAt         *DateTime      `gorm:"column:locked_at;type:timestamptz(6)" json:"locked_at"`
	LockedBy         *string        `gorm:"column:locked_by" json:"locked_by"`
	LastError        *string        `gorm:"column:last_error" json:"last_error"`
	CreatedTime      DateTime       `gorm:"column:created_time;type:timestamptz(6);not null" json:"created_time"`
	CreatedBy        string         `gorm:"column:created_by;not null" json:"created_by"`
	LastModifiedTime *DateTime      `gorm:"column:last_modified_time;type:timestamptz(6)" json:"last_modified_time"`
	LastModifiedBy   *string        `gorm:"column:last_modified_by" json:"last_modified_by"`
}

func (SchemaOperation) TableName() string {
	return "schema_operation"
}

func (m *SchemaOperation) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.NextRunAt.IsZero() {
		m.NextRunAt = DateTime{Time: now}
	}
	if m.CreatedTime.IsZero() {
		m.CreatedTime = DateTime{Time: now}
	}
	if m.LastModifiedTime == nil {
		m.LastModifiedTime = &DateTime{Time: now}
	}
	return nil
}

func (*SchemaOperation) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("LastModifiedTime") {
		tx.Statement.SetColumn("LastModifiedTime", &DateTime{Time: now})
	}
	return nil
}

type Field struct {
	ID                  string     `gorm:"column:id;primaryKey" json:"id"`
	Name                string     `gorm:"column:name;not null" json:"name"`
	Description         *string    `gorm:"column:description" json:"description"`
	Options             *string    `gorm:"column:options" json:"options"`
	Meta                *string    `gorm:"column:meta" json:"meta"`
	AiConfig            *string    `gorm:"column:ai_config" json:"ai_config"`
	Type                string     `gorm:"column:type;not null" json:"type"`
	CellValueType       string     `gorm:"column:cell_value_type;not null" json:"cell_value_type"`
	IsMultipleCellValue *bool      `gorm:"column:is_multiple_cell_value" json:"is_multiple_cell_value"`
	DbFieldType         string     `gorm:"column:db_field_type;not null" json:"db_field_type"`
	DbFieldName         string     `gorm:"column:db_field_name;not null" json:"db_field_name"`
	ProvisionState      string     `gorm:"column:provision_state;default:'ready';not null" json:"provision_state"`
	NotNull             *bool      `gorm:"column:not_null" json:"not_null"`
	Unique              *bool      `gorm:"column:unique" json:"unique"`
	IsPrimary           *bool      `gorm:"column:is_primary" json:"is_primary"`
	IsComputed          *bool      `gorm:"column:is_computed" json:"is_computed"`
	IsLookup            *bool      `gorm:"column:is_lookup" json:"is_lookup"`
	IsConditionalLookup *bool      `gorm:"column:is_conditional_lookup" json:"is_conditional_lookup"`
	IsPending           *bool      `gorm:"column:is_pending" json:"is_pending"`
	HasError            *bool      `gorm:"column:has_error" json:"has_error"`
	LookupLinkedFieldID *string    `gorm:"column:lookup_linked_field_id;index:field_lookup_linked_field_id_idx" json:"lookup_linked_field_id"`
	LookupOptions       *string    `gorm:"column:lookup_options" json:"lookup_options"`
	TableID             string     `gorm:"column:table_id;index:field_table_id_deleted_time_idx;not null" json:"table_id"`
	Order               float64    `gorm:"column:order;not null" json:"order"`
	Version             int        `gorm:"column:version;not null" json:"version"`
	CreatedTime         DateTime   `gorm:"column:created_time;not null" json:"created_time"`
	LastModifiedTime    *DateTime  `gorm:"column:last_modified_time" json:"last_modified_time"`
	DeletedTime         *DateTime  `gorm:"column:deleted_time;index:field_table_id_deleted_time_idx" json:"deleted_time"`
	CreatedBy           string     `gorm:"column:created_by;not null" json:"created_by"`
	LastModifiedBy      *string    `gorm:"column:last_modified_by" json:"last_modified_by"`
	Table               *TableMeta `gorm:"foreignKey:TableID"`
}

func (Field) TableName() string {
	return "field"
}

func (m *Field) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedTime.IsZero() {
		m.CreatedTime = DateTime{Time: now}
	}
	if m.LastModifiedTime == nil {
		m.LastModifiedTime = &DateTime{Time: now}
	}
	return nil
}

func (*Field) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("LastModifiedTime") {
		tx.Statement.SetColumn("LastModifiedTime", &DateTime{Time: now})
	}
	return nil
}

type ComputedUpdateOutbox struct {
	ID                      string                     `gorm:"column:id;primaryKey" json:"id"`
	BaseID                  string                     `gorm:"column:base_id;index:computed_update_outbox_base_id_seed_table_id_idx;not null" json:"base_id"`
	WorkerLane              *string                    `gorm:"column:worker_lane;index:computed_update_outbox_worker_lane_status_next_run_at_idx;default:'production';not null" json:"worker_lane"`
	SeedTableID             string                     `gorm:"column:seed_table_id;index:computed_update_outbox_base_id_seed_table_id_idx;not null" json:"seed_table_id"`
	SeedRecordIds           datatypes.JSON             `gorm:"column:seed_record_ids" json:"seed_record_ids"`
	ChangeType              string                     `gorm:"column:change_type;not null" json:"change_type"`
	Steps                   datatypes.JSON             `gorm:"column:steps" json:"steps"`
	Edges                   datatypes.JSON             `gorm:"column:edges" json:"edges"`
	Status                  string                     `gorm:"column:status;index:computed_update_outbox_status_next_run_at_idx;index:computed_update_outbox_worker_lane_status_next_run_at_idx;not null" json:"status"`
	Attempts                int                        `gorm:"column:attempts;default:0;not null" json:"attempts"`
	MaxAttempts             *int                       `gorm:"column:max_attempts;default:8;not null" json:"max_attempts"`
	NextRunAt               DateTime                   `gorm:"column:next_run_at;index:computed_update_outbox_status_next_run_at_idx;index:computed_update_outbox_worker_lane_status_next_run_at_idx;not null" json:"next_run_at"`
	LockedAt                *DateTime                  `gorm:"column:locked_at" json:"locked_at"`
	LockedBy                *string                    `gorm:"column:locked_by" json:"locked_by"`
	LastError               *string                    `gorm:"column:last_error" json:"last_error"`
	EstimatedComplexity     int                        `gorm:"column:estimated_complexity;default:0;not null" json:"estimated_complexity"`
	PlanHash                string                     `gorm:"column:plan_hash;index:computed_update_outbox_plan_hash_idx;not null" json:"plan_hash"`
	DirtyStats              datatypes.JSON             `gorm:"column:dirty_stats" json:"dirty_stats"`
	RunID                   string                     `gorm:"column:run_id;index:computed_update_outbox_run_id_idx;not null" json:"run_id"`
	OriginRunIds            []string                   `gorm:"column:origin_run_ids;serializer:json;not null" json:"origin_run_ids"`
	RunTotalSteps           int                        `gorm:"column:run_total_steps;default:0;not null" json:"run_total_steps"`
	RunCompletedStepsBefore int                        `gorm:"column:run_completed_steps_before;default:0;not null" json:"run_completed_steps_before"`
	AffectedTableIds        []string                   `gorm:"column:affected_table_ids;serializer:json;not null" json:"affected_table_ids"`
	AffectedFieldIds        []string                   `gorm:"column:affected_field_ids;serializer:json;not null" json:"affected_field_ids"`
	SyncMaxLevel            *int                       `gorm:"column:sync_max_level" json:"sync_max_level"`
	SourceChangedAt         *DateTime                  `gorm:"column:source_changed_at" json:"source_changed_at"`
	StageDepth              int                        `gorm:"column:stage_depth;default:0;not null" json:"stage_depth"`
	PredecessorTaskID       *string                    `gorm:"column:predecessor_task_id" json:"predecessor_task_id"`
	CreatedAt               DateTime                   `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt               DateTime                   `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	Seeds                   []ComputedUpdateOutboxSeed `gorm:"foreignKey:TaskID;constraint:OnDelete:CASCADE"`
}

func (ComputedUpdateOutbox) TableName() string {
	return "computed_update_outbox"
}

func (m *ComputedUpdateOutbox) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.NextRunAt.IsZero() {
		m.NextRunAt = DateTime{Time: now}
	}
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*ComputedUpdateOutbox) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type ComputedUpdateOutboxSeed struct {
	ID       string                `gorm:"column:id;primaryKey" json:"id"`
	TaskID   string                `gorm:"column:task_id;uniqueIndex:computed_update_outbox_seed_task_id_table_id_record_id_key;index:computed_update_outbox_seed_task_id_idx;not null" json:"task_id"`
	TableID  string                `gorm:"column:table_id;uniqueIndex:computed_update_outbox_seed_task_id_table_id_record_id_key;not null" json:"table_id"`
	RecordID string                `gorm:"column:record_id;uniqueIndex:computed_update_outbox_seed_task_id_table_id_record_id_key;not null" json:"record_id"`
	Task     *ComputedUpdateOutbox `gorm:"foreignKey:TaskID"`
}

func (ComputedUpdateOutboxSeed) TableName() string {
	return "computed_update_outbox_seed"
}

func (m *ComputedUpdateOutboxSeed) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = cuid.New()
	}
	return nil
}

type ComputedUpdateChangeFrontier struct {
	ScopeID  string `gorm:"column:scope_id;primaryKey" json:"scope_id"`
	Kind     string `gorm:"column:kind;primaryKey" json:"kind"`
	TableID  string `gorm:"column:table_id;primaryKey" json:"table_id"`
	RecordID string `gorm:"column:record_id;primaryKey" json:"record_id"`
	FieldID  string `gorm:"column:field_id;primaryKey" json:"field_id"`
}

func (ComputedUpdateChangeFrontier) TableName() string {
	return "computed_update_change_frontier"
}

type ComputedUpdateStageLedger struct {
	ScopeID  string `gorm:"column:scope_id;primaryKey;index:computed_update_stage_ledger_scope_id_kind_seq_idx" json:"scope_id"`
	Kind     string `gorm:"column:kind;primaryKey;index:computed_update_stage_ledger_scope_id_kind_seq_idx" json:"kind"`
	TableID  string `gorm:"column:table_id;primaryKey" json:"table_id"`
	RecordID string `gorm:"column:record_id;primaryKey" json:"record_id"`
	Seq      int64  `gorm:"column:seq;index:computed_update_stage_ledger_scope_id_kind_seq_idx;default:0;not null" json:"seq"`
}

func (ComputedUpdateStageLedger) TableName() string {
	return "computed_update_stage_ledger"
}

type ComputedUpdateDeadLetter struct {
	ID                      string                         `gorm:"column:id;primaryKey" json:"id"`
	BaseID                  string                         `gorm:"column:base_id;index:computed_update_dead_letter_base_id_seed_table_id_idx;not null" json:"base_id"`
	WorkerLane              *string                        `gorm:"column:worker_lane;default:'production';not null" json:"worker_lane"`
	SeedTableID             string                         `gorm:"column:seed_table_id;index:computed_update_dead_letter_base_id_seed_table_id_idx;not null" json:"seed_table_id"`
	SeedRecordIds           datatypes.JSON                 `gorm:"column:seed_record_ids" json:"seed_record_ids"`
	ChangeType              string                         `gorm:"column:change_type;not null" json:"change_type"`
	Steps                   datatypes.JSON                 `gorm:"column:steps" json:"steps"`
	Edges                   datatypes.JSON                 `gorm:"column:edges" json:"edges"`
	Status                  string                         `gorm:"column:status;not null" json:"status"`
	Attempts                int                            `gorm:"column:attempts;default:0;not null" json:"attempts"`
	MaxAttempts             *int                           `gorm:"column:max_attempts;default:8;not null" json:"max_attempts"`
	NextRunAt               DateTime                       `gorm:"column:next_run_at;not null" json:"next_run_at"`
	LockedAt                *DateTime                      `gorm:"column:locked_at" json:"locked_at"`
	LockedBy                *string                        `gorm:"column:locked_by" json:"locked_by"`
	LastError               *string                        `gorm:"column:last_error" json:"last_error"`
	EstimatedComplexity     int                            `gorm:"column:estimated_complexity;default:0;not null" json:"estimated_complexity"`
	PlanHash                string                         `gorm:"column:plan_hash;index:computed_update_dead_letter_plan_hash_idx;not null" json:"plan_hash"`
	DirtyStats              datatypes.JSON                 `gorm:"column:dirty_stats" json:"dirty_stats"`
	RunID                   string                         `gorm:"column:run_id;index:computed_update_dead_letter_run_id_idx;not null" json:"run_id"`
	OriginRunIds            []string                       `gorm:"column:origin_run_ids;serializer:json;not null" json:"origin_run_ids"`
	RunTotalSteps           int                            `gorm:"column:run_total_steps;default:0;not null" json:"run_total_steps"`
	RunCompletedStepsBefore int                            `gorm:"column:run_completed_steps_before;default:0;not null" json:"run_completed_steps_before"`
	AffectedTableIds        []string                       `gorm:"column:affected_table_ids;serializer:json;not null" json:"affected_table_ids"`
	AffectedFieldIds        []string                       `gorm:"column:affected_field_ids;serializer:json;not null" json:"affected_field_ids"`
	SyncMaxLevel            *int                           `gorm:"column:sync_max_level" json:"sync_max_level"`
	SourceChangedAt         *DateTime                      `gorm:"column:source_changed_at" json:"source_changed_at"`
	StageDepth              int                            `gorm:"column:stage_depth;default:0;not null" json:"stage_depth"`
	PredecessorTaskID       *string                        `gorm:"column:predecessor_task_id" json:"predecessor_task_id"`
	TraceData               datatypes.JSON                 `gorm:"column:trace_data" json:"trace_data"`
	FailedAt                DateTime                       `gorm:"column:failed_at;not null" json:"failed_at"`
	CreatedAt               DateTime                       `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
	UpdatedAt               DateTime                       `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	Seeds                   []ComputedUpdateDeadLetterSeed `gorm:"foreignKey:TaskID;constraint:OnUpdate:CASCADE,OnDelete:CASCADE"`
}

func (ComputedUpdateDeadLetter) TableName() string {
	return "computed_update_dead_letter"
}

type ComputedUpdateDeadLetterSeed struct {
	TaskID   string                    `gorm:"column:task_id;primaryKey" json:"task_id"`
	TableID  string                    `gorm:"column:table_id;primaryKey" json:"table_id"`
	RecordID string                    `gorm:"column:record_id;primaryKey" json:"record_id"`
	Task     *ComputedUpdateDeadLetter `gorm:"foreignKey:TaskID"`
}

func (ComputedUpdateDeadLetterSeed) TableName() string {
	return "computed_update_dead_letter_seed"
}

type ComputedUpdateRunHistory struct {
	TaskID                  string         `gorm:"column:task_id;primaryKey" json:"task_id"`
	BaseID                  string         `gorm:"column:base_id;index:computed_update_run_history_base_id_completed_at_idx;not null" json:"base_id"`
	SeedTableID             string         `gorm:"column:seed_table_id;not null" json:"seed_table_id"`
	ChangeType              string         `gorm:"column:change_type;not null" json:"change_type"`
	RunID                   string         `gorm:"column:run_id;index:computed_update_run_history_run_id_idx;not null" json:"run_id"`
	OriginRunIds            []string       `gorm:"column:origin_run_ids;index:computed_update_run_history_origin_run_ids_gin;serializer:json;not null" json:"origin_run_ids"`
	Steps                   datatypes.JSON `gorm:"column:steps" json:"steps"`
	Edges                   datatypes.JSON `gorm:"column:edges" json:"edges"`
	AffectedTableIds        []string       `gorm:"column:affected_table_ids;serializer:json;not null" json:"affected_table_ids"`
	AffectedFieldIds        []string       `gorm:"column:affected_field_ids;serializer:json;not null" json:"affected_field_ids"`
	SourceFieldIds          []string       `gorm:"column:source_field_ids;serializer:json;not null" json:"source_field_ids"`
	SeedRecordCount         int            `gorm:"column:seed_record_count;default:0;not null" json:"seed_record_count"`
	StageDepth              int            `gorm:"column:stage_depth;default:0;not null" json:"stage_depth"`
	PredecessorTaskID       *string        `gorm:"column:predecessor_task_id" json:"predecessor_task_id"`
	RunTotalSteps           int            `gorm:"column:run_total_steps;default:0;not null" json:"run_total_steps"`
	RunCompletedStepsBefore int            `gorm:"column:run_completed_steps_before;default:0;not null" json:"run_completed_steps_before"`
	SyncMaxLevel            *int           `gorm:"column:sync_max_level" json:"sync_max_level"`
	EstimatedComplexity     int            `gorm:"column:estimated_complexity;default:0;not null" json:"estimated_complexity"`
	Attempts                int            `gorm:"column:attempts;default:0;not null" json:"attempts"`
	Outcome                 string         `gorm:"column:outcome;not null" json:"outcome"`
	SourceChangedAt         *DateTime      `gorm:"column:source_changed_at" json:"source_changed_at"`
	EnqueuedAt              DateTime       `gorm:"column:enqueued_at;not null" json:"enqueued_at"`
	StartedAt               *DateTime      `gorm:"column:started_at" json:"started_at"`
	CompletedAt             DateTime       `gorm:"column:completed_at;index:computed_update_run_history_base_id_completed_at_idx;index:computed_update_run_history_completed_at_idx;not null" json:"completed_at"`
	DurationMs              int            `gorm:"column:duration_ms;default:0;not null" json:"duration_ms"`
}

func (ComputedUpdateRunHistory) TableName() string {
	return "computed_update_run_history"
}

type ComputedUpdatePauseScope struct {
	ID          string    `gorm:"column:id;primaryKey" json:"id"`
	ScopeType   string    `gorm:"column:scope_type;uniqueIndex:computed_update_pause_scope_scope_type_scope_id_key;not null" json:"scope_type"`
	ScopeID     string    `gorm:"column:scope_id;uniqueIndex:computed_update_pause_scope_scope_type_scope_id_key;not null" json:"scope_id"`
	PausedAt    DateTime  `gorm:"column:paused_at;not null" json:"paused_at"`
	PausedBy    *string   `gorm:"column:paused_by" json:"paused_by"`
	ResumeAt    *DateTime `gorm:"column:resume_at;index:computed_update_pause_scope_resume_at_idx" json:"resume_at"`
	Reason      *string   `gorm:"column:reason" json:"reason"`
	WritePolicy *string   `gorm:"column:write_policy;default:'allow_bounded';not null" json:"write_policy"`
	UpdatedAt   DateTime  `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
	UpdatedBy   *string   `gorm:"column:updated_by" json:"updated_by"`
}

func (ComputedUpdatePauseScope) TableName() string {
	return "computed_update_pause_scope"
}

func (m *ComputedUpdatePauseScope) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.PausedAt.IsZero() {
		m.PausedAt = DateTime{Time: now}
	}
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

func (*ComputedUpdatePauseScope) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("UpdatedAt") {
		tx.Statement.SetColumn("UpdatedAt", DateTime{Time: now})
	}
	return nil
}

type ComputedFieldActivity struct {
	FieldID               string         `gorm:"column:field_id;primaryKey" json:"field_id"`
	TableID               string         `gorm:"column:table_id;index:computed_field_activity_table_id_status_idx;not null" json:"table_id"`
	BaseID                string         `gorm:"column:base_id;index:computed_field_activity_base_id_status_idx;not null" json:"base_id"`
	Status                string         `gorm:"column:status;index:computed_field_activity_table_id_status_idx;index:computed_field_activity_base_id_status_idx;not null" json:"status"`
	ActiveTaskCount       int            `gorm:"column:active_task_count;default:0;not null" json:"active_task_count"`
	ProcessingTaskCount   int            `gorm:"column:processing_task_count;default:0;not null" json:"processing_task_count"`
	Generation            int64          `gorm:"column:generation;default:0;not null" json:"generation"`
	EstimatedComplexity   int64          `gorm:"column:estimated_complexity;default:0;not null" json:"estimated_complexity"`
	EstimatedDirtyRecords int64          `gorm:"column:estimated_dirty_records;default:0;not null" json:"estimated_dirty_records"`
	HasAllTargetRecords   bool           `gorm:"column:has_all_target_records;default:false;not null" json:"has_all_target_records"`
	QueuedAt              *DateTime      `gorm:"column:queued_at" json:"queued_at"`
	StartedAt             *DateTime      `gorm:"column:started_at" json:"started_at"`
	LastCompletedAt       *DateTime      `gorm:"column:last_completed_at" json:"last_completed_at"`
	LastDurationMs        *int           `gorm:"column:last_duration_ms" json:"last_duration_ms"`
	LastError             datatypes.JSON `gorm:"column:last_error" json:"last_error"`
	Extensions            datatypes.JSON `gorm:"column:extensions" json:"extensions"`
	UpdatedAt             DateTime       `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
}

func (ComputedFieldActivity) TableName() string {
	return "computed_field_activity"
}

func (m *ComputedFieldActivity) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

type ComputedTableActivity struct {
	TableID               string         `gorm:"column:table_id;primaryKey" json:"table_id"`
	BaseID                string         `gorm:"column:base_id;index:computed_table_activity_base_id_status_idx;not null" json:"base_id"`
	Status                string         `gorm:"column:status;index:computed_table_activity_base_id_status_idx;not null" json:"status"`
	CalculatingFieldCount int            `gorm:"column:calculating_field_count;default:0;not null" json:"calculating_field_count"`
	QueuedFieldCount      int            `gorm:"column:queued_field_count;default:0;not null" json:"queued_field_count"`
	EstimatedComplexity   int64          `gorm:"column:estimated_complexity;default:0;not null" json:"estimated_complexity"`
	RecentCompletions     datatypes.JSON `gorm:"column:recent_completions;default:'[]';not null" json:"recent_completions"`
	Generation            int64          `gorm:"column:generation;default:0;not null" json:"generation"`
	UpdatedAt             DateTime       `gorm:"column:updated_at;autoUpdateTime:false;not null" json:"updated_at"`
}

func (ComputedTableActivity) TableName() string {
	return "computed_table_activity"
}

func (m *ComputedTableActivity) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.UpdatedAt.IsZero() {
		m.UpdatedAt = DateTime{Time: now}
	}
	return nil
}

type ComputedTaskFieldRef struct {
	TaskID        string   `gorm:"column:task_id;primaryKey" json:"task_id"`
	FieldID       string   `gorm:"column:field_id;primaryKey;index:computed_task_field_ref_field_id_idx" json:"field_id"`
	TableID       string   `gorm:"column:table_id;index:computed_task_field_ref_table_id_idx;not null" json:"table_id"`
	BaseID        string   `gorm:"column:base_id;not null" json:"base_id"`
	WasProcessing bool     `gorm:"column:was_processing;default:false;not null" json:"was_processing"`
	CreatedAt     DateTime `gorm:"column:created_at;autoCreateTime:false;not null" json:"created_at"`
}

func (ComputedTaskFieldRef) TableName() string {
	return "computed_task_field_ref"
}

func (m *ComputedTaskFieldRef) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedAt.IsZero() {
		m.CreatedAt = DateTime{Time: now}
	}
	return nil
}

type View struct {
	ID               string     `gorm:"column:id;primaryKey" json:"id"`
	Name             string     `gorm:"column:name;not null" json:"name"`
	Description      *string    `gorm:"column:description" json:"description"`
	TableID          string     `gorm:"column:table_id;index:view_table_id_deleted_time_idx;not null" json:"table_id"`
	Type             string     `gorm:"column:type;not null" json:"type"`
	Sort             *string    `gorm:"column:sort" json:"sort"`
	Filter           *string    `gorm:"column:filter" json:"filter"`
	Group            *string    `gorm:"column:group" json:"group"`
	Options          *string    `gorm:"column:options" json:"options"`
	Order            float64    `gorm:"column:order;index:view_order_idx;not null" json:"order"`
	Version          int        `gorm:"column:version;not null" json:"version"`
	ColumnMeta       string     `gorm:"column:column_meta;not null" json:"column_meta"`
	IsLocked         *bool      `gorm:"column:is_locked" json:"is_locked"`
	EnableShare      *bool      `gorm:"column:enable_share" json:"enable_share"`
	ShareID          *string    `gorm:"column:share_id;uniqueIndex" json:"share_id"`
	ShareMeta        *string    `gorm:"column:share_meta" json:"share_meta"`
	CreatedTime      DateTime   `gorm:"column:created_time;not null" json:"created_time"`
	LastModifiedTime *DateTime  `gorm:"column:last_modified_time" json:"last_modified_time"`
	DeletedTime      *DateTime  `gorm:"column:deleted_time;index:view_table_id_deleted_time_idx" json:"deleted_time"`
	CreatedBy        string     `gorm:"column:created_by;not null" json:"created_by"`
	LastModifiedBy   *string    `gorm:"column:last_modified_by" json:"last_modified_by"`
	Table            *TableMeta `gorm:"foreignKey:TableID"`
}

func (View) TableName() string {
	return "view"
}

func (m *View) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedTime.IsZero() {
		m.CreatedTime = DateTime{Time: now}
	}
	if m.LastModifiedTime == nil {
		m.LastModifiedTime = &DateTime{Time: now}
	}
	return nil
}

func (*View) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("LastModifiedTime") {
		tx.Statement.SetColumn("LastModifiedTime", &DateTime{Time: now})
	}
	return nil
}

type Ops struct {
	ID          string   `gorm:"column:id;primaryKey" json:"id"`
	Collection  string   `gorm:"column:collection;uniqueIndex:ops_collection_doc_id_version_key;index:ops_collection_created_time_idx;not null" json:"collection"`
	DocID       string   `gorm:"column:doc_id;uniqueIndex:ops_collection_doc_id_version_key;not null" json:"doc_id"`
	DocType     string   `gorm:"column:doc_type;not null" json:"doc_type"`
	Version     int      `gorm:"column:version;uniqueIndex:ops_collection_doc_id_version_key;not null" json:"version"`
	Operation   string   `gorm:"column:operation;not null" json:"operation"`
	CreatedTime DateTime `gorm:"column:created_time;index:ops_collection_created_time_idx;not null" json:"created_time"`
	CreatedBy   string   `gorm:"column:created_by;not null" json:"created_by"`
}

func (Ops) TableName() string {
	return "ops"
}

func (m *Ops) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedTime.IsZero() {
		m.CreatedTime = DateTime{Time: now}
	}
	return nil
}

type Reference struct {
	ID          string   `gorm:"column:id;primaryKey" json:"id"`
	FromFieldID string   `gorm:"column:from_field_id;uniqueIndex:reference_to_field_id_from_field_id_key;index:reference_from_field_id_idx;not null" json:"from_field_id"`
	ToFieldID   string   `gorm:"column:to_field_id;uniqueIndex:reference_to_field_id_from_field_id_key;index:reference_to_field_id_idx;not null" json:"to_field_id"`
	CreatedTime DateTime `gorm:"column:created_time;not null" json:"created_time"`
}

func (Reference) TableName() string {
	return "reference"
}

func (m *Reference) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedTime.IsZero() {
		m.CreatedTime = DateTime{Time: now}
	}
	return nil
}

type User struct {
	ID                   string          `gorm:"column:id;primaryKey" json:"id"`
	Name                 string          `gorm:"column:name;not null" json:"name"`
	Password             *string         `gorm:"column:password" json:"password"`
	Salt                 *string         `gorm:"column:salt" json:"salt"`
	Phone                *string         `gorm:"column:phone;uniqueIndex" json:"phone"`
	Email                string          `gorm:"column:email;uniqueIndex;not null" json:"email"`
	Avatar               *string         `gorm:"column:avatar" json:"avatar"`
	IsSystem             *bool           `gorm:"column:is_system" json:"is_system"`
	IsAdmin              *bool           `gorm:"column:is_admin" json:"is_admin"`
	IsTrialUsed          *bool           `gorm:"column:is_trial_used" json:"is_trial_used"`
	Lang                 *string         `gorm:"column:lang" json:"lang"`
	NotifyMeta           *string         `gorm:"column:notify_meta" json:"notify_meta"`
	LastSignTime         *DateTime       `gorm:"column:last_sign_time" json:"last_sign_time"`
	DeactivatedTime      *DateTime       `gorm:"column:deactivated_time" json:"deactivated_time"`
	CreatedTime          DateTime        `gorm:"column:created_time;not null" json:"created_time"`
	DeletedTime          *DateTime       `gorm:"column:deleted_time" json:"deleted_time"`
	LastModifiedTime     *DateTime       `gorm:"column:last_modified_time" json:"last_modified_time"`
	PermanentDeletedTime *DateTime       `gorm:"column:permanent_deleted_time" json:"permanent_deleted_time"`
	RefMeta              *string         `gorm:"column:ref_meta" json:"ref_meta"`
	Accounts             []Account       `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
	BaseOrders           []UserBaseOrder `gorm:"foreignKey:UserID;constraint:OnDelete:CASCADE"`
}

func (User) TableName() string {
	return "users"
}

func (m *User) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedTime.IsZero() {
		m.CreatedTime = DateTime{Time: now}
	}
	if m.LastModifiedTime == nil {
		m.LastModifiedTime = &DateTime{Time: now}
	}
	return nil
}

func (*User) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("LastModifiedTime") {
		tx.Statement.SetColumn("LastModifiedTime", &DateTime{Time: now})
	}
	return nil
}

type Account struct {
	ID           string   `gorm:"column:id;primaryKey" json:"id"`
	UserID       string   `gorm:"column:user_id;not null" json:"user_id"`
	Type         string   `gorm:"column:type;not null" json:"type"`
	Provider     string   `gorm:"column:provider;uniqueIndex:account_provider_provider_id_connection_id_key;not null" json:"provider"`
	ProviderID   string   `gorm:"column:provider_id;uniqueIndex:account_provider_provider_id_connection_id_key;not null" json:"provider_id"`
	ConnectionID string   `gorm:"column:connection_id;uniqueIndex:account_provider_provider_id_connection_id_key;default:'';not null" json:"connection_id"`
	CreatedTime  DateTime `gorm:"column:created_time;not null" json:"created_time"`
	User         *User
}

func (Account) TableName() string {
	return "account"
}

func (m *Account) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedTime.IsZero() {
		m.CreatedTime = DateTime{Time: now}
	}
	return nil
}

type Attachments struct {
	ID             string    `gorm:"column:id;primaryKey" json:"id"`
	Token          string    `gorm:"column:token;uniqueIndex;not null" json:"token"`
	Hash           string    `gorm:"column:hash;not null" json:"hash"`
	Size           int64     `gorm:"column:size;not null" json:"size"`
	Mimetype       string    `gorm:"column:mimetype;not null" json:"mimetype"`
	Path           string    `gorm:"column:path;not null" json:"path"`
	Width          *int      `gorm:"column:width" json:"width"`
	Height         *int      `gorm:"column:height" json:"height"`
	DeletedTime    *DateTime `gorm:"column:deleted_time" json:"deleted_time"`
	CreatedTime    DateTime  `gorm:"column:created_time;not null" json:"created_time"`
	CreatedBy      string    `gorm:"column:created_by;not null" json:"created_by"`
	LastModifiedBy *string   `gorm:"column:last_modified_by" json:"last_modified_by"`
	ThumbnailPath  *string   `gorm:"column:thumbnail_path" json:"thumbnail_path"`
}

func (Attachments) TableName() string {
	return "attachments"
}

func (m *Attachments) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedTime.IsZero() {
		m.CreatedTime = DateTime{Time: now}
	}
	return nil
}

type AttachmentsTable struct {
	ID               string    `gorm:"column:id;primaryKey" json:"id"`
	AttachmentID     string    `gorm:"column:attachment_id;index:attachments_table_attachment_id_idx;not null" json:"attachment_id"`
	Name             string    `gorm:"column:name;not null" json:"name"`
	Token            string    `gorm:"column:token;not null" json:"token"`
	TableID          string    `gorm:"column:table_id;index:attachments_table_table_id_record_id_idx;index:attachments_table_table_id_field_id_idx;not null" json:"table_id"`
	RecordID         string    `gorm:"column:record_id;index:attachments_table_table_id_record_id_idx;not null" json:"record_id"`
	FieldID          string    `gorm:"column:field_id;index:attachments_table_table_id_field_id_idx;not null" json:"field_id"`
	CreatedTime      DateTime  `gorm:"column:created_time;not null" json:"created_time"`
	CreatedBy        string    `gorm:"column:created_by;not null" json:"created_by"`
	LastModifiedBy   *string   `gorm:"column:last_modified_by" json:"last_modified_by"`
	LastModifiedTime *DateTime `gorm:"column:last_modified_time" json:"last_modified_time"`
}

func (AttachmentsTable) TableName() string {
	return "attachments_table"
}

func (m *AttachmentsTable) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedTime.IsZero() {
		m.CreatedTime = DateTime{Time: now}
	}
	if m.LastModifiedTime == nil {
		m.LastModifiedTime = &DateTime{Time: now}
	}
	return nil
}

func (*AttachmentsTable) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("LastModifiedTime") {
		tx.Statement.SetColumn("LastModifiedTime", &DateTime{Time: now})
	}
	return nil
}

type Collaborator struct {
	ID               string    `gorm:"column:id;primaryKey" json:"id"`
	RoleName         string    `gorm:"column:role_name;not null" json:"role_name"`
	ResourceType     string    `gorm:"column:resource_type;uniqueIndex:collaborator_resource_type_resource_id_principal_id_principal_type_key;not null" json:"resource_type"`
	ResourceID       string    `gorm:"column:resource_id;uniqueIndex:collaborator_resource_type_resource_id_principal_id_principal_type_key;index:collaborator_resource_id_idx;not null" json:"resource_id"`
	PrincipalID      string    `gorm:"column:principal_id;uniqueIndex:collaborator_resource_type_resource_id_principal_id_principal_type_key;index:collaborator_principal_id_idx;not null" json:"principal_id"`
	PrincipalType    string    `gorm:"column:principal_type;uniqueIndex:collaborator_resource_type_resource_id_principal_id_principal_type_key;not null" json:"principal_type"`
	CreatedBy        string    `gorm:"column:created_by;not null" json:"created_by"`
	CreatedTime      DateTime  `gorm:"column:created_time;not null" json:"created_time"`
	LastModifiedTime *DateTime `gorm:"column:last_modified_time" json:"last_modified_time"`
	LastModifiedBy   *string   `gorm:"column:last_modified_by" json:"last_modified_by"`
}

func (Collaborator) TableName() string {
	return "collaborator"
}

func (m *Collaborator) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedTime.IsZero() {
		m.CreatedTime = DateTime{Time: now}
	}
	if m.LastModifiedTime == nil {
		m.LastModifiedTime = &DateTime{Time: now}
	}
	return nil
}

func (*Collaborator) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("LastModifiedTime") {
		tx.Statement.SetColumn("LastModifiedTime", &DateTime{Time: now})
	}
	return nil
}

type Invitation struct {
	ID               string    `gorm:"column:id;primaryKey" json:"id"`
	BaseID           *string   `gorm:"column:base_id;index:invitation_base_id_idx" json:"base_id"`
	SpaceID          *string   `gorm:"column:space_id;index:invitation_space_id_idx" json:"space_id"`
	Type             string    `gorm:"column:type;not null" json:"type"`
	Role             string    `gorm:"column:role;not null" json:"role"`
	InvitationCode   string    `gorm:"column:invitation_code;not null" json:"invitation_code"`
	ExpiredTime      *DateTime `gorm:"column:expired_time" json:"expired_time"`
	CreatedBy        string    `gorm:"column:create_by;not null" json:"create_by"`
	CreatedTime      DateTime  `gorm:"column:created_time;not null" json:"created_time"`
	LastModifiedTime *DateTime `gorm:"column:last_modified_time" json:"last_modified_time"`
	LastModifiedBy   *string   `gorm:"column:last_modified_by" json:"last_modified_by"`
	DeletedTime      *DateTime `gorm:"column:deleted_time" json:"deleted_time"`
}

func (Invitation) TableName() string {
	return "invitation"
}

func (m *Invitation) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedTime.IsZero() {
		m.CreatedTime = DateTime{Time: now}
	}
	if m.LastModifiedTime == nil {
		m.LastModifiedTime = &DateTime{Time: now}
	}
	return nil
}

func (*Invitation) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("LastModifiedTime") {
		tx.Statement.SetColumn("LastModifiedTime", &DateTime{Time: now})
	}
	return nil
}

type InvitationRecord struct {
	ID           string   `gorm:"column:id;primaryKey" json:"id"`
	InvitationID string   `gorm:"column:invitation_id;index:invitation_record_invitation_id_idx;not null" json:"invitation_id"`
	BaseID       *string  `gorm:"column:base_id;index:invitation_record_base_id_idx" json:"base_id"`
	SpaceID      *string  `gorm:"column:space_id;index:invitation_record_space_id_idx" json:"space_id"`
	Type         string   `gorm:"column:type;not null" json:"type"`
	Inviter      string   `gorm:"column:inviter;not null" json:"inviter"`
	Accepter     string   `gorm:"column:accepter;not null" json:"accepter"`
	CreatedTime  DateTime `gorm:"column:created_time;not null" json:"created_time"`
}

func (InvitationRecord) TableName() string {
	return "invitation_record"
}

func (m *InvitationRecord) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedTime.IsZero() {
		m.CreatedTime = DateTime{Time: now}
	}
	return nil
}

type Notification struct {
	ID          string   `gorm:"column:id;primaryKey" json:"id"`
	FromUserID  string   `gorm:"column:from_user_id;not null" json:"from_user_id"`
	ToUserID    string   `gorm:"column:to_user_id;index:notification_to_user_id_is_read_created_time_idx;not null" json:"to_user_id"`
	Type        string   `gorm:"column:type;not null" json:"type"`
	Message     string   `gorm:"column:message;not null" json:"message"`
	MessageI18n *string  `gorm:"column:message_i18n" json:"message_i18n"`
	Severity    *string  `gorm:"column:severity;default:'info';not null" json:"severity"`
	URLPath     *string  `gorm:"column:url_path" json:"url_path"`
	IsRead      bool     `gorm:"column:is_read;index:notification_to_user_id_is_read_created_time_idx;default:false;not null" json:"is_read"`
	CreatedTime DateTime `gorm:"column:created_time;index:notification_to_user_id_is_read_created_time_idx;index:notification_created_time_idx;not null" json:"created_time"`
	CreatedBy   string   `gorm:"column:created_by;not null" json:"created_by"`
}

func (Notification) TableName() string {
	return "notification"
}

func (m *Notification) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedTime.IsZero() {
		m.CreatedTime = DateTime{Time: now}
	}
	return nil
}

type AccessToken struct {
	ID               string    `gorm:"column:id;primaryKey" json:"id"`
	Name             string    `gorm:"column:name;not null" json:"name"`
	Description      *string   `gorm:"column:description" json:"description"`
	UserID           string    `gorm:"column:user_id;index:access_token_user_id_idx;not null" json:"user_id"`
	Scopes           string    `gorm:"column:scopes;not null" json:"scopes"`
	SpaceIds         *string   `gorm:"column:space_ids" json:"space_ids"`
	BaseIds          *string   `gorm:"column:base_ids" json:"base_ids"`
	Sign             string    `gorm:"column:sign;not null" json:"sign"`
	ClientID         *string   `gorm:"column:client_id;index:access_token_client_id_idx" json:"client_id"`
	HasFullAccess    *bool     `gorm:"column:has_full_access" json:"has_full_access"`
	ExpiredTime      DateTime  `gorm:"column:expired_time;not null" json:"expired_time"`
	LastUsedTime     *DateTime `gorm:"column:last_used_time" json:"last_used_time"`
	CreatedTime      DateTime  `gorm:"column:created_time;not null" json:"created_time"`
	LastModifiedTime *DateTime `gorm:"column:last_modified_time" json:"last_modified_time"`
}

func (AccessToken) TableName() string {
	return "access_token"
}

func (m *AccessToken) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedTime.IsZero() {
		m.CreatedTime = DateTime{Time: now}
	}
	if m.LastModifiedTime == nil {
		m.LastModifiedTime = &DateTime{Time: now}
	}
	return nil
}

func (*AccessToken) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("LastModifiedTime") {
		tx.Statement.SetColumn("LastModifiedTime", &DateTime{Time: now})
	}
	return nil
}

type Setting struct {
	Name             string    `gorm:"column:name;primaryKey;uniqueIndex" json:"name"`
	Content          *string   `gorm:"column:content" json:"content"`
	CreatedTime      DateTime  `gorm:"column:created_time;not null" json:"created_time"`
	LastModifiedTime *DateTime `gorm:"column:last_modified_time" json:"last_modified_time"`
	CreatedBy        string    `gorm:"column:created_by;not null" json:"created_by"`
	LastModifiedBy   *string   `gorm:"column:last_modified_by" json:"last_modified_by"`
}

func (Setting) TableName() string {
	return "setting"
}

func (m *Setting) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedTime.IsZero() {
		m.CreatedTime = DateTime{Time: now}
	}
	if m.LastModifiedTime == nil {
		m.LastModifiedTime = &DateTime{Time: now}
	}
	return nil
}

func (*Setting) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("LastModifiedTime") {
		tx.Statement.SetColumn("LastModifiedTime", &DateTime{Time: now})
	}
	return nil
}

type OAuthApp struct {
	ID                  string               `gorm:"column:id;primaryKey" json:"id"`
	Name                string               `gorm:"column:name;not null" json:"name"`
	Logo                *string              `gorm:"column:logo" json:"logo"`
	Homepage            string               `gorm:"column:homepage;not null" json:"homepage"`
	Description         *string              `gorm:"column:description" json:"description"`
	ClientID            string               `gorm:"column:client_id;uniqueIndex;not null" json:"client_id"`
	RedirectUris        *string              `gorm:"column:redirect_uris" json:"redirect_uris"`
	Scopes              *string              `gorm:"column:scopes" json:"scopes"`
	AllowDeviceFlow     bool                 `gorm:"column:allow_device_flow;default:false;not null" json:"allow_device_flow"`
	DynamicRegistration bool                 `gorm:"column:dynamic_registration;default:false;not null" json:"dynamic_registration"`
	CreatedTime         DateTime             `gorm:"column:created_time;not null" json:"created_time"`
	LastModifiedTime    *DateTime            `gorm:"column:last_modified_time" json:"last_modified_time"`
	CreatedBy           string               `gorm:"column:created_by;not null" json:"created_by"`
	Secrets             []OAuthAppSecret     `gorm:"foreignKey:ClientID;references:ClientID;constraint:OnDelete:CASCADE"`
	Authorized          []OAuthAppAuthorized `gorm:"foreignKey:ClientID;references:ClientID;constraint:OnDelete:CASCADE"`
	Tokens              []OAuthAppToken      `gorm:"foreignKey:ClientID;references:ClientID;constraint:OnDelete:CASCADE"`
}

func (OAuthApp) TableName() string {
	return "oauth_app"
}

func (m *OAuthApp) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedTime.IsZero() {
		m.CreatedTime = DateTime{Time: now}
	}
	if m.LastModifiedTime == nil {
		m.LastModifiedTime = &DateTime{Time: now}
	}
	return nil
}

func (*OAuthApp) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("LastModifiedTime") {
		tx.Statement.SetColumn("LastModifiedTime", &DateTime{Time: now})
	}
	return nil
}

type OAuthAppAuthorized struct {
	ID             string    `gorm:"column:id;primaryKey" json:"id"`
	ClientID       string    `gorm:"column:client_id;uniqueIndex:oauth_app_authorized_client_id_user_id_key;not null" json:"client_id"`
	UserID         string    `gorm:"column:user_id;uniqueIndex:oauth_app_authorized_client_id_user_id_key;not null" json:"user_id"`
	AuthorizedTime DateTime  `gorm:"column:authorized_time;not null" json:"authorized_time"`
	OAuthApp       *OAuthApp `gorm:"foreignKey:ClientID;references:ClientID"`
}

func (OAuthAppAuthorized) TableName() string {
	return "oauth_app_authorized"
}

func (m *OAuthAppAuthorized) BeforeCreate(_ *gorm.DB) error {
	if m.ID == "" {
		m.ID = cuid.New()
	}
	return nil
}

type OAuthAppSecret struct {
	ID           string          `gorm:"column:id;primaryKey" json:"id"`
	ClientID     string          `gorm:"column:client_id;not null" json:"client_id"`
	Secret       string          `gorm:"column:secret;uniqueIndex;not null" json:"secret"`
	MaskedSecret string          `gorm:"column:masked_secret;not null" json:"masked_secret"`
	CreatedTime  DateTime        `gorm:"column:created_time;not null" json:"created_time"`
	CreatedBy    string          `gorm:"column:created_by;not null" json:"created_by"`
	LastUsedTime *DateTime       `gorm:"column:last_used_time" json:"last_used_time"`
	OAuthApp     *OAuthApp       `gorm:"foreignKey:ClientID;references:ClientID"`
	Tokens       []OAuthAppToken `gorm:"foreignKey:AppSecretID;constraint:OnDelete:CASCADE"`
}

func (OAuthAppSecret) TableName() string {
	return "oauth_app_secret"
}

func (m *OAuthAppSecret) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedTime.IsZero() {
		m.CreatedTime = DateTime{Time: now}
	}
	return nil
}

type OAuthAppToken struct {
	ID               string          `gorm:"column:id;primaryKey" json:"id"`
	ClientID         string          `gorm:"column:client_id;index:oauth_app_token_client_id_idx;not null" json:"client_id"`
	AppSecretID      *string         `gorm:"column:app_secret_id" json:"app_secret_id"`
	RefreshTokenSign string          `gorm:"column:refresh_token_sign;uniqueIndex;not null" json:"refresh_token_sign"`
	ExpiredTime      DateTime        `gorm:"column:expired_time;not null" json:"expired_time"`
	CreatedTime      DateTime        `gorm:"column:created_time;not null" json:"created_time"`
	CreatedBy        string          `gorm:"column:created_by;not null" json:"created_by"`
	OAuthAppSecret   *OAuthAppSecret `gorm:"foreignKey:AppSecretID"`
	OAuthApp         *OAuthApp       `gorm:"foreignKey:ClientID;references:ClientID"`
}

func (OAuthAppToken) TableName() string {
	return "oauth_app_token"
}

func (m *OAuthAppToken) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedTime.IsZero() {
		m.CreatedTime = DateTime{Time: now}
	}
	return nil
}

type RecordHistory struct {
	ID          string   `gorm:"column:id;primaryKey" json:"id"`
	TableID     string   `gorm:"column:table_id;index:record_history_table_id_record_id_created_time_idx;index:record_history_table_id_created_time_idx;not null" json:"table_id"`
	RecordID    string   `gorm:"column:record_id;index:record_history_table_id_record_id_created_time_idx;not null" json:"record_id"`
	FieldID     string   `gorm:"column:field_id;not null" json:"field_id"`
	Before      string   `gorm:"column:before;not null" json:"before"`
	After       string   `gorm:"column:after;not null" json:"after"`
	CreatedTime DateTime `gorm:"column:created_time;index:record_history_table_id_record_id_created_time_idx;index:record_history_table_id_created_time_idx;not null" json:"created_time"`
	CreatedBy   string   `gorm:"column:created_by;not null" json:"created_by"`
}

func (RecordHistory) TableName() string {
	return "record_history"
}

func (m *RecordHistory) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedTime.IsZero() {
		m.CreatedTime = DateTime{Time: now}
	}
	return nil
}

type Trash struct {
	ID           string   `gorm:"column:id;primaryKey" json:"id"`
	ResourceType string   `gorm:"column:resource_type;uniqueIndex:trash_resource_type_resource_id_key;not null" json:"resource_type"`
	ResourceID   string   `gorm:"column:resource_id;uniqueIndex:trash_resource_type_resource_id_key;not null" json:"resource_id"`
	ParentID     *string  `gorm:"column:parent_id" json:"parent_id"`
	DeletedTime  DateTime `gorm:"column:deleted_time;not null" json:"deleted_time"`
	DeletedBy    string   `gorm:"column:deleted_by;not null" json:"deleted_by"`
}

func (Trash) TableName() string {
	return "trash"
}

func (m *Trash) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.DeletedTime.IsZero() {
		m.DeletedTime = DateTime{Time: now}
	}
	return nil
}

type TableTrash struct {
	ID           string   `gorm:"column:id;primaryKey" json:"id"`
	TableID      string   `gorm:"column:table_id;index:table_trash_table_id_idx;not null" json:"table_id"`
	ResourceType string   `gorm:"column:resource_type;not null" json:"resource_type"`
	Snapshot     string   `gorm:"column:snapshot;not null" json:"snapshot"`
	CreatedTime  DateTime `gorm:"column:created_time;not null" json:"created_time"`
	CreatedBy    string   `gorm:"column:created_by;not null" json:"created_by"`
}

func (TableTrash) TableName() string {
	return "table_trash"
}

func (m *TableTrash) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedTime.IsZero() {
		m.CreatedTime = DateTime{Time: now}
	}
	return nil
}

type RecordTrash struct {
	ID                     string    `gorm:"column:id;primaryKey" json:"id"`
	TableID                string    `gorm:"column:table_id;index:record_trash_table_id_record_id_idx;not null" json:"table_id"`
	RecordID               string    `gorm:"column:record_id;index:record_trash_table_id_record_id_idx;not null" json:"record_id"`
	Snapshot               string    `gorm:"column:snapshot;not null" json:"snapshot"`
	CreatedTime            DateTime  `gorm:"column:created_time;not null" json:"created_time"`
	CreatedBy              string    `gorm:"column:created_by;not null" json:"created_by"`
	Reason                 *string   `gorm:"column:reason;default:'deleted';not null" json:"reason"`
	RecordCreatedTime      *DateTime `gorm:"column:record_created_time" json:"record_created_time"`
	RecordCreatedBy        *string   `gorm:"column:record_created_by" json:"record_created_by"`
	RecordLastModifiedTime *DateTime `gorm:"column:record_last_modified_time" json:"record_last_modified_time"`
	RecordLastModifiedBy   *string   `gorm:"column:record_last_modified_by" json:"record_last_modified_by"`
	OperationID            *string   `gorm:"column:operation_id" json:"operation_id"`
}

func (RecordTrash) TableName() string {
	return "record_trash"
}

func (m *RecordTrash) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedTime.IsZero() {
		m.CreatedTime = DateTime{Time: now}
	}
	return nil
}

type RecordRemovalTombstone struct {
	ID          string   `gorm:"column:id;primaryKey" json:"id"`
	TableID     string   `gorm:"column:table_id;index:record_removal_tombstone_table_id_record_id_idx;not null" json:"table_id"`
	RecordID    string   `gorm:"column:record_id;index:record_removal_tombstone_table_id_record_id_idx;not null" json:"record_id"`
	Type        string   `gorm:"column:type;not null" json:"type"`
	CreatedTime DateTime `gorm:"column:created_time;not null" json:"created_time"`
}

func (RecordRemovalTombstone) TableName() string {
	return "record_removal_tombstone"
}

func (m *RecordRemovalTombstone) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedTime.IsZero() {
		m.CreatedTime = DateTime{Time: now}
	}
	return nil
}

type Plugin struct {
	ID               string          `gorm:"column:id;primaryKey" json:"id"`
	Name             string          `gorm:"column:name;not null" json:"name"`
	Description      *string         `gorm:"column:description" json:"description"`
	DetailDesc       *string         `gorm:"column:detail_desc" json:"detail_desc"`
	Logo             string          `gorm:"column:logo;not null" json:"logo"`
	HelpURL          *string         `gorm:"column:help_url" json:"help_url"`
	Status           string          `gorm:"column:status;not null" json:"status"`
	Positions        string          `gorm:"column:positions;not null" json:"positions"`
	URL              *string         `gorm:"column:url" json:"url"`
	Secret           string          `gorm:"column:secret;uniqueIndex;not null" json:"secret"`
	MaskedSecret     string          `gorm:"column:masked_secret;not null" json:"masked_secret"`
	I18n             *string         `gorm:"column:i18n" json:"i18n"`
	Config           *string         `gorm:"column:config" json:"config"`
	PluginUser       *string         `gorm:"column:plugin_user" json:"plugin_user"`
	CreatedTime      DateTime        `gorm:"column:created_time;not null" json:"created_time"`
	LastModifiedTime *DateTime       `gorm:"column:last_modified_time" json:"last_modified_time"`
	CreatedBy        string          `gorm:"column:created_by;not null" json:"created_by"`
	LastModifiedBy   *string         `gorm:"column:last_modified_by" json:"last_modified_by"`
	PluginInstall    []PluginInstall `gorm:"foreignKey:PluginID;constraint:OnDelete:CASCADE"`
}

func (Plugin) TableName() string {
	return "plugin"
}

func (m *Plugin) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedTime.IsZero() {
		m.CreatedTime = DateTime{Time: now}
	}
	if m.LastModifiedTime == nil {
		m.LastModifiedTime = &DateTime{Time: now}
	}
	return nil
}

func (*Plugin) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("LastModifiedTime") {
		tx.Statement.SetColumn("LastModifiedTime", &DateTime{Time: now})
	}
	return nil
}

type PluginInstall struct {
	ID               string    `gorm:"column:id;primaryKey" json:"id"`
	PluginID         string    `gorm:"column:plugin_id;not null" json:"plugin_id"`
	BaseID           string    `gorm:"column:base_id;index:plugin_install_base_id_idx;not null" json:"base_id"`
	Name             string    `gorm:"column:name;not null" json:"name"`
	PositionID       string    `gorm:"column:position_id;index:plugin_install_position_id_idx;not null" json:"position_id"`
	Position         string    `gorm:"column:position;not null" json:"position"`
	Storage          *string   `gorm:"column:storage" json:"storage"`
	CreatedTime      DateTime  `gorm:"column:created_time;not null" json:"created_time"`
	CreatedBy        string    `gorm:"column:created_by;not null" json:"created_by"`
	LastModifiedTime *DateTime `gorm:"column:last_modified_time" json:"last_modified_time"`
	LastModifiedBy   *string   `gorm:"column:last_modified_by" json:"last_modified_by"`
	Plugin           *Plugin
}

func (PluginInstall) TableName() string {
	return "plugin_install"
}

func (m *PluginInstall) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedTime.IsZero() {
		m.CreatedTime = DateTime{Time: now}
	}
	if m.LastModifiedTime == nil {
		m.LastModifiedTime = &DateTime{Time: now}
	}
	return nil
}

func (*PluginInstall) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("LastModifiedTime") {
		tx.Statement.SetColumn("LastModifiedTime", &DateTime{Time: now})
	}
	return nil
}

type Dashboard struct {
	ID               string    `gorm:"column:id;primaryKey" json:"id"`
	Name             string    `gorm:"column:name;not null" json:"name"`
	BaseID           string    `gorm:"column:base_id;index:dashboard_base_id_idx;not null" json:"base_id"`
	Layout           *string   `gorm:"column:layout" json:"layout"`
	CreatedBy        string    `gorm:"column:created_by;not null" json:"created_by"`
	CreatedTime      DateTime  `gorm:"column:created_time;not null" json:"created_time"`
	LastModifiedTime *DateTime `gorm:"column:last_modified_time" json:"last_modified_time"`
	LastModifiedBy   *string   `gorm:"column:last_modified_by" json:"last_modified_by"`
}

func (Dashboard) TableName() string {
	return "dashboard"
}

func (m *Dashboard) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedTime.IsZero() {
		m.CreatedTime = DateTime{Time: now}
	}
	if m.LastModifiedTime == nil {
		m.LastModifiedTime = &DateTime{Time: now}
	}
	return nil
}

func (*Dashboard) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("LastModifiedTime") {
		tx.Statement.SetColumn("LastModifiedTime", &DateTime{Time: now})
	}
	return nil
}

type Comment struct {
	ID               string    `gorm:"column:id;primaryKey" json:"id"`
	TableID          string    `gorm:"column:table_id;index:comment_table_id_record_id_idx;not null" json:"table_id"`
	RecordID         string    `gorm:"column:record_id;index:comment_table_id_record_id_idx;not null" json:"record_id"`
	QuoteID          *string   `gorm:"column:quote_Id" json:"quote_Id"`
	Content          *string   `gorm:"column:content" json:"content"`
	Reaction         *string   `gorm:"column:reaction" json:"reaction"`
	DeletedTime      *DateTime `gorm:"column:deleted_time" json:"deleted_time"`
	CreatedTime      DateTime  `gorm:"column:created_time;not null" json:"created_time"`
	CreatedBy        string    `gorm:"column:created_by;not null" json:"created_by"`
	LastModifiedTime *DateTime `gorm:"column:last_modified_time" json:"last_modified_time"`
}

func (Comment) TableName() string {
	return "comment"
}

func (m *Comment) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedTime.IsZero() {
		m.CreatedTime = DateTime{Time: now}
	}
	if m.LastModifiedTime == nil {
		m.LastModifiedTime = &DateTime{Time: now}
	}
	return nil
}

func (*Comment) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("LastModifiedTime") {
		tx.Statement.SetColumn("LastModifiedTime", &DateTime{Time: now})
	}
	return nil
}

type CommentSubscription struct {
	ID          string   `gorm:"column:id;primaryKey" json:"id"`
	TableID     string   `gorm:"column:table_id;uniqueIndex:comment_subscription_table_id_record_id_key;index:comment_subscription_table_id_record_id_idx;not null" json:"table_id"`
	RecordID    string   `gorm:"column:record_id;uniqueIndex:comment_subscription_table_id_record_id_key;index:comment_subscription_table_id_record_id_idx;not null" json:"record_id"`
	CreatedBy   string   `gorm:"column:created_by;not null" json:"created_by"`
	CreatedTime DateTime `gorm:"column:created_time;not null" json:"created_time"`
}

func (CommentSubscription) TableName() string {
	return "comment_subscription"
}

func (m *CommentSubscription) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedTime.IsZero() {
		m.CreatedTime = DateTime{Time: now}
	}
	return nil
}

type Integration struct {
	ID               string    `gorm:"column:id;primaryKey" json:"id"`
	ResourceID       string    `gorm:"column:resource_id;uniqueIndex;index:integration_resource_id_idx;not null" json:"resource_id"`
	Config           string    `gorm:"column:config;not null" json:"config"`
	Type             string    `gorm:"column:type;not null" json:"type"`
	Enable           *bool     `gorm:"column:enable" json:"enable"`
	CreatedTime      DateTime  `gorm:"column:created_time;not null" json:"created_time"`
	LastModifiedTime *DateTime `gorm:"column:last_modified_time" json:"last_modified_time"`
}

func (Integration) TableName() string {
	return "integration"
}

func (m *Integration) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedTime.IsZero() {
		m.CreatedTime = DateTime{Time: now}
	}
	if m.LastModifiedTime == nil {
		m.LastModifiedTime = &DateTime{Time: now}
	}
	return nil
}

func (*Integration) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("LastModifiedTime") {
		tx.Statement.SetColumn("LastModifiedTime", &DateTime{Time: now})
	}
	return nil
}

type PluginPanel struct {
	ID               string     `gorm:"column:id;primaryKey" json:"id"`
	Name             string     `gorm:"column:name;not null" json:"name"`
	TableID          string     `gorm:"column:table_id;not null" json:"table_id"`
	Layout           *string    `gorm:"column:layout" json:"layout"`
	CreatedBy        string     `gorm:"column:created_by;not null" json:"created_by"`
	CreatedTime      DateTime   `gorm:"column:created_time;not null" json:"created_time"`
	LastModifiedTime *DateTime  `gorm:"column:last_modified_time" json:"last_modified_time"`
	LastModifiedBy   *string    `gorm:"column:last_modified_by" json:"last_modified_by"`
	Table            *TableMeta `gorm:"foreignKey:TableID"`
}

func (PluginPanel) TableName() string {
	return "plugin_panel"
}

func (m *PluginPanel) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedTime.IsZero() {
		m.CreatedTime = DateTime{Time: now}
	}
	if m.LastModifiedTime == nil {
		m.LastModifiedTime = &DateTime{Time: now}
	}
	return nil
}

func (*PluginPanel) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("LastModifiedTime") {
		tx.Statement.SetColumn("LastModifiedTime", &DateTime{Time: now})
	}
	return nil
}

type PluginContextMenu struct {
	ID               string     `gorm:"column:id;primaryKey" json:"id"`
	TableID          string     `gorm:"column:table_id;not null" json:"table_id"`
	PluginInstallID  string     `gorm:"column:plugin_install_id;uniqueIndex;not null" json:"plugin_install_id"`
	Order            float64    `gorm:"column:order;not null" json:"order"`
	CreatedTime      DateTime   `gorm:"column:created_time;not null" json:"created_time"`
	CreatedBy        string     `gorm:"column:created_by;not null" json:"created_by"`
	LastModifiedTime *DateTime  `gorm:"column:last_modified_time" json:"last_modified_time"`
	LastModifiedBy   *string    `gorm:"column:last_modified_by" json:"last_modified_by"`
	Table            *TableMeta `gorm:"foreignKey:TableID"`
}

func (PluginContextMenu) TableName() string {
	return "plugin_context_menu"
}

func (m *PluginContextMenu) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedTime.IsZero() {
		m.CreatedTime = DateTime{Time: now}
	}
	if m.LastModifiedTime == nil {
		m.LastModifiedTime = &DateTime{Time: now}
	}
	return nil
}

func (*PluginContextMenu) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("LastModifiedTime") {
		tx.Statement.SetColumn("LastModifiedTime", &DateTime{Time: now})
	}
	return nil
}

type UserLastVisit struct {
	ID               string   `gorm:"column:id;primaryKey" json:"id"`
	UserID           string   `gorm:"column:user_id;uniqueIndex:user_last_visit_user_id_resource_type_resource_id_key;index:user_last_visit_user_id_resource_type_parent_resource_id_idx;not null" json:"user_id"`
	ResourceType     string   `gorm:"column:resource_type;uniqueIndex:user_last_visit_user_id_resource_type_resource_id_key;index:user_last_visit_user_id_resource_type_parent_resource_id_idx;not null" json:"resource_type"`
	ResourceID       string   `gorm:"column:resource_id;uniqueIndex:user_last_visit_user_id_resource_type_resource_id_key;not null" json:"resource_id"`
	ParentResourceID string   `gorm:"column:parent_resource_id;index:user_last_visit_user_id_resource_type_parent_resource_id_idx;not null" json:"parent_resource_id"`
	LastVisitTime    DateTime `gorm:"column:last_visit_time;not null" json:"last_visit_time"`
}

func (UserLastVisit) TableName() string {
	return "user_last_visit"
}

func (m *UserLastVisit) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.LastVisitTime.IsZero() {
		m.LastVisitTime = DateTime{Time: now}
	}
	return nil
}

type UserBaseOrder struct {
	ID               string    `gorm:"column:id;primaryKey" json:"id"`
	UserID           string    `gorm:"column:user_id;uniqueIndex:user_base_order_user_id_base_id_key;index:user_base_order_user_id_order_idx;not null" json:"user_id"`
	BaseID           string    `gorm:"column:base_id;uniqueIndex:user_base_order_user_id_base_id_key;index:user_base_order_base_id_idx;not null" json:"base_id"`
	Order            float64   `gorm:"column:order;index:user_base_order_user_id_order_idx;not null" json:"order"`
	CreatedTime      DateTime  `gorm:"column:created_time;not null" json:"created_time"`
	LastModifiedTime *DateTime `gorm:"column:last_modified_time" json:"last_modified_time"`
	User             *User
	Base             *Base
}

func (UserBaseOrder) TableName() string {
	return "user_base_order"
}

func (m *UserBaseOrder) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedTime.IsZero() {
		m.CreatedTime = DateTime{Time: now}
	}
	if m.LastModifiedTime == nil {
		m.LastModifiedTime = &DateTime{Time: now}
	}
	return nil
}

func (*UserBaseOrder) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("LastModifiedTime") {
		tx.Statement.SetColumn("LastModifiedTime", &DateTime{Time: now})
	}
	return nil
}

type Template struct {
	ID                  string         `gorm:"column:id;primaryKey" json:"id"`
	BaseID              *string        `gorm:"column:base_id;uniqueIndex" json:"base_id"`
	Cover               *string        `gorm:"column:cover" json:"cover"`
	Name                *string        `gorm:"column:name" json:"name"`
	Description         *string        `gorm:"column:description" json:"description"`
	MarkdownDescription *string        `gorm:"column:markdown_description" json:"markdown_description"`
	CategoryID          []string       `gorm:"column:category_id;serializer:json;not null" json:"category_id"`
	CreatedTime         DateTime       `gorm:"column:created_time;not null" json:"created_time"`
	CreatedBy           string         `gorm:"column:created_by;not null" json:"created_by"`
	LastModifiedTime    *DateTime      `gorm:"column:last_modified_time" json:"last_modified_time"`
	LastModifiedBy      *string        `gorm:"column:last_modified_by" json:"last_modified_by"`
	IsSystem            *bool          `gorm:"column:is_system" json:"is_system"`
	IsPublished         *bool          `gorm:"column:is_published" json:"is_published"`
	Featured            *bool          `gorm:"column:featured" json:"featured"`
	Snapshot            *string        `gorm:"column:snapshot" json:"snapshot"`
	Order               float64        `gorm:"column:order;not null" json:"order"`
	UsageCount          int            `gorm:"column:usage_count;default:0;not null" json:"usage_count"`
	PublishInfo         datatypes.JSON `gorm:"column:publish_info" json:"publish_info"`
	VisitCount          int            `gorm:"column:visit_count;default:0;not null" json:"visit_count"`
	Kind                *string        `gorm:"column:kind" json:"kind"`
	Version             *int           `gorm:"column:version;default:1;not null" json:"version"`
	Manifest            datatypes.JSON `gorm:"column:manifest" json:"manifest"`
	Review              datatypes.JSON `gorm:"column:review" json:"review"`
	History             datatypes.JSON `gorm:"column:history" json:"history"`
	ParentTemplateID    *string        `gorm:"column:parent_template_id" json:"parent_template_id"`
	Slug                *string        `gorm:"column:slug;uniqueIndex" json:"slug"`
}

func (Template) TableName() string {
	return "template"
}

func (m *Template) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedTime.IsZero() {
		m.CreatedTime = DateTime{Time: now}
	}
	if m.LastModifiedTime == nil {
		m.LastModifiedTime = &DateTime{Time: now}
	}
	return nil
}

func (*Template) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("LastModifiedTime") {
		tx.Statement.SetColumn("LastModifiedTime", &DateTime{Time: now})
	}
	return nil
}

type TemplateCategory struct {
	ID               string    `gorm:"column:id;primaryKey" json:"id"`
	Name             string    `gorm:"column:name;uniqueIndex;not null" json:"name"`
	CreatedTime      DateTime  `gorm:"column:created_time;not null" json:"created_time"`
	CreatedBy        string    `gorm:"column:created_by;not null" json:"created_by"`
	LastModifiedTime *DateTime `gorm:"column:last_modified_time" json:"last_modified_time"`
	LastModifiedBy   *string   `gorm:"column:last_modified_by" json:"last_modified_by"`
	Order            float64   `gorm:"column:order;not null" json:"order"`
	ParentID         *string   `gorm:"column:parent_id;index:template_category_parent_id_idx" json:"parent_id"`
	Kind             *string   `gorm:"column:kind;default:'industry';not null" json:"kind"`
	Key              *string   `gorm:"column:key;uniqueIndex" json:"key"`
}

func (TemplateCategory) TableName() string {
	return "template_category"
}

func (m *TemplateCategory) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedTime.IsZero() {
		m.CreatedTime = DateTime{Time: now}
	}
	if m.LastModifiedTime == nil {
		m.LastModifiedTime = &DateTime{Time: now}
	}
	return nil
}

func (*TemplateCategory) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("LastModifiedTime") {
		tx.Statement.SetColumn("LastModifiedTime", &DateTime{Time: now})
	}
	return nil
}

type Task struct {
	ID               string    `gorm:"column:id;primaryKey" json:"id"`
	Type             string    `gorm:"column:type;index:task_type_status_idx;not null" json:"type"`
	Status           string    `gorm:"column:status;index:task_type_status_idx;not null" json:"status"`
	Snapshot         *string   `gorm:"column:snapshot" json:"snapshot"`
	CreatedTime      DateTime  `gorm:"column:created_time;not null" json:"created_time"`
	LastModifiedTime *DateTime `gorm:"column:last_modified_time" json:"last_modified_time"`
	CreatedBy        string    `gorm:"column:created_by;not null" json:"created_by"`
	LastModifiedBy   *string   `gorm:"column:last_modified_by" json:"last_modified_by"`
	Runs             []TaskRun `gorm:"foreignKey:TaskID;constraint:OnDelete:CASCADE"`
}

func (Task) TableName() string {
	return "task"
}

func (m *Task) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedTime.IsZero() {
		m.CreatedTime = DateTime{Time: now}
	}
	if m.LastModifiedTime == nil {
		m.LastModifiedTime = &DateTime{Time: now}
	}
	return nil
}

func (*Task) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("LastModifiedTime") {
		tx.Statement.SetColumn("LastModifiedTime", &DateTime{Time: now})
	}
	return nil
}

type TaskRun struct {
	ID               string    `gorm:"column:id;primaryKey" json:"id"`
	TaskID           string    `gorm:"column:task_id;index:task_run_task_id_status_idx;not null" json:"task_id"`
	BaseID           string    `gorm:"column:base_id;index:task_run_status_base_id_created_time_idx;not null" json:"base_id"`
	Status           string    `gorm:"column:status;index:task_run_task_id_status_idx;index:task_run_status_base_id_created_time_idx;index:task_run_status_last_modified_time_idx;not null" json:"status"`
	Snapshot         string    `gorm:"column:snapshot;not null" json:"snapshot"`
	DependsOnRunIds  []string  `gorm:"column:depends_on_run_ids;serializer:json;not null" json:"depends_on_run_ids"`
	Spent            *int      `gorm:"column:spent" json:"spent"`
	Log              *string   `gorm:"column:log" json:"log"`
	ErrorMsg         *string   `gorm:"column:error_msg" json:"error_msg"`
	StartedTime      *DateTime `gorm:"column:started_time" json:"started_time"`
	CreatedTime      DateTime  `gorm:"column:created_time;index:task_run_status_base_id_created_time_idx;not null" json:"created_time"`
	LastModifiedTime *DateTime `gorm:"column:last_modified_time;index:task_run_status_last_modified_time_idx" json:"last_modified_time"`
	Task             *Task
}

func (TaskRun) TableName() string {
	return "task_run"
}

func (m *TaskRun) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedTime.IsZero() {
		m.CreatedTime = DateTime{Time: now}
	}
	if m.LastModifiedTime == nil {
		m.LastModifiedTime = &DateTime{Time: now}
	}
	return nil
}

func (*TaskRun) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("LastModifiedTime") {
		tx.Statement.SetColumn("LastModifiedTime", &DateTime{Time: now})
	}
	return nil
}

type TaskReference struct {
	ID          string   `gorm:"column:id;primaryKey" json:"id"`
	FromFieldID string   `gorm:"column:from_field_id;uniqueIndex:task_reference_to_field_id_from_field_id_key;index:task_reference_from_field_id_idx;not null" json:"from_field_id"`
	ToFieldID   string   `gorm:"column:to_field_id;uniqueIndex:task_reference_to_field_id_from_field_id_key;index:task_reference_to_field_id_idx;not null" json:"to_field_id"`
	CreatedTime DateTime `gorm:"column:created_time;not null" json:"created_time"`
}

func (TaskReference) TableName() string {
	return "task_reference"
}

func (m *TaskReference) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedTime.IsZero() {
		m.CreatedTime = DateTime{Time: now}
	}
	return nil
}

type Waitlist struct {
	Email       string    `gorm:"column:email;primaryKey;uniqueIndex" json:"email"`
	Invite      *bool     `gorm:"column:invite" json:"invite"`
	InviteTime  *DateTime `gorm:"column:invite_time" json:"invite_time"`
	CreatedTime DateTime  `gorm:"column:created_time;not null" json:"created_time"`
}

func (Waitlist) TableName() string {
	return "waitlist"
}

func (m *Waitlist) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.CreatedTime.IsZero() {
		m.CreatedTime = DateTime{Time: now}
	}
	return nil
}

type BaseNode struct {
	ID               string     `gorm:"column:id;primaryKey" json:"id"`
	ParentID         *string    `gorm:"column:parent_id" json:"parent_id"`
	BaseID           string     `gorm:"column:base_id;uniqueIndex:base_node_base_id_resource_type_resource_id_key;not null" json:"base_id"`
	ResourceType     string     `gorm:"column:resource_type;uniqueIndex:base_node_base_id_resource_type_resource_id_key;not null" json:"resource_type"`
	ResourceID       string     `gorm:"column:resource_id;uniqueIndex:base_node_base_id_resource_type_resource_id_key;not null" json:"resource_id"`
	Order            float64    `gorm:"column:order;not null" json:"order"`
	CreatedTime      DateTime   `gorm:"column:created_time;not null" json:"created_time"`
	CreatedBy        string     `gorm:"column:created_by;not null" json:"created_by"`
	LastModifiedTime *DateTime  `gorm:"column:last_modified_time" json:"last_modified_time"`
	LastModifiedBy   *string    `gorm:"column:last_modified_by" json:"last_modified_by"`
	Parent           *BaseNode  `gorm:"foreignKey:ParentID"`
	Children         []BaseNode `gorm:"foreignKey:ParentID"`
}

func (BaseNode) TableName() string {
	return "base_node"
}

func (m *BaseNode) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedTime.IsZero() {
		m.CreatedTime = DateTime{Time: now}
	}
	if m.LastModifiedTime == nil {
		m.LastModifiedTime = &DateTime{Time: now}
	}
	return nil
}

func (*BaseNode) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("LastModifiedTime") {
		tx.Statement.SetColumn("LastModifiedTime", &DateTime{Time: now})
	}
	return nil
}

type BaseNodeFolder struct {
	ID               string    `gorm:"column:id;primaryKey" json:"id"`
	BaseID           string    `gorm:"column:base_id;uniqueIndex:base_node_folder_base_id_name_key;not null" json:"base_id"`
	Name             string    `gorm:"column:name;uniqueIndex:base_node_folder_base_id_name_key;not null" json:"name"`
	CreatedTime      DateTime  `gorm:"column:created_time;not null" json:"created_time"`
	CreatedBy        string    `gorm:"column:created_by;not null" json:"created_by"`
	LastModifiedTime *DateTime `gorm:"column:last_modified_time" json:"last_modified_time"`
	LastModifiedBy   *string   `gorm:"column:last_modified_by" json:"last_modified_by"`
}

func (BaseNodeFolder) TableName() string {
	return "base_node_folder"
}

func (m *BaseNodeFolder) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedTime.IsZero() {
		m.CreatedTime = DateTime{Time: now}
	}
	if m.LastModifiedTime == nil {
		m.LastModifiedTime = &DateTime{Time: now}
	}
	return nil
}

func (*BaseNodeFolder) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("LastModifiedTime") {
		tx.Statement.SetColumn("LastModifiedTime", &DateTime{Time: now})
	}
	return nil
}

type BaseShare struct {
	ID               string    `gorm:"column:id;primaryKey" json:"id"`
	BaseID           string    `gorm:"column:base_id;uniqueIndex:base_share_base_id_node_id_key;index:base_share_base_id_idx;not null" json:"base_id"`
	ShareID          string    `gorm:"column:share_id;uniqueIndex;not null" json:"share_id"`
	Password         *string   `gorm:"column:password" json:"password"`
	NodeID           *string   `gorm:"column:node_id;uniqueIndex:base_share_base_id_node_id_key" json:"node_id"`
	AllowSave        *bool     `gorm:"column:allow_save" json:"allow_save"`
	AllowCopy        *bool     `gorm:"column:allow_copy" json:"allow_copy"`
	AllowEdit        *bool     `gorm:"column:allow_edit" json:"allow_edit"`
	Enabled          *bool     `gorm:"column:enabled;default:true;not null" json:"enabled"`
	CreatedTime      DateTime  `gorm:"column:created_time;not null" json:"created_time"`
	CreatedBy        string    `gorm:"column:created_by;not null" json:"created_by"`
	LastModifiedTime *DateTime `gorm:"column:last_modified_time" json:"last_modified_time"`
}

func (BaseShare) TableName() string {
	return "base_share"
}

func (m *BaseShare) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedTime.IsZero() {
		m.CreatedTime = DateTime{Time: now}
	}
	if m.LastModifiedTime == nil {
		m.LastModifiedTime = &DateTime{Time: now}
	}
	return nil
}

func (*BaseShare) BeforeUpdate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if !tx.Statement.Changed("LastModifiedTime") {
		tx.Statement.SetColumn("LastModifiedTime", &DateTime{Time: now})
	}
	return nil
}

type ShortLink struct {
	ID          string    `gorm:"column:id;primaryKey" json:"id"`
	Code        string    `gorm:"column:code;uniqueIndex;type:varchar(32);not null" json:"code"`
	Type        string    `gorm:"column:type;uniqueIndex:short_link_type_resource_id_key;type:varchar(32);not null" json:"type"`
	ResourceID  string    `gorm:"column:resource_id;uniqueIndex:short_link_type_resource_id_key;not null" json:"resource_id"`
	CreatedTime DateTime  `gorm:"column:created_time;not null" json:"created_time"`
	CreatedBy   string    `gorm:"column:created_by;not null" json:"created_by"`
	DeletedTime *DateTime `gorm:"column:deleted_time;index:short_link_deleted_time_idx" json:"deleted_time"`
}

func (ShortLink) TableName() string {
	return "short_link"
}

func (m *ShortLink) BeforeCreate(tx *gorm.DB) error {
	now := tx.NowFunc().UTC().Truncate(time.Millisecond)
	if m.ID == "" {
		m.ID = cuid.New()
	}
	if m.CreatedTime.IsZero() {
		m.CreatedTime = DateTime{Time: now}
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
