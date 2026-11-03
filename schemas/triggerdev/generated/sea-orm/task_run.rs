use super::run_engine_version::RunEngineVersion;
use super::runtime_environment_type::RuntimeEnvironmentType;
use super::task_run_status::TaskRunStatus;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "TaskRun")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(default_value = 0)]
    pub number: i32,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "friendlyId")]
    pub friendly_id: String,
    #[sea_orm(default_value = "V1")]
    pub engine: RunEngineVersion,
    #[sea_orm(default_value = "PENDING")]
    pub status: TaskRunStatus,
    #[sea_orm(column_name = "statusReason")]
    pub status_reason: Option<String>,
    #[sea_orm(column_name = "idempotencyKey")]
    pub idempotency_key: Option<String>,
    #[sea_orm(column_name = "idempotencyKeyExpiresAt")]
    pub idempotency_key_expires_at: Option<DateTime>,
    #[sea_orm(column_name = "idempotencyKeyOptions")]
    pub idempotency_key_options: Option<Json>,
    pub debounce: Option<Json>,
    #[sea_orm(column_name = "taskIdentifier")]
    pub task_identifier: String,
    #[sea_orm(column_name = "isTest", default_value = false)]
    pub is_test: bool,
    pub payload: String,
    #[sea_orm(column_name = "payloadType", default_value = "application/json")]
    pub payload_type: String,
    pub context: Option<Json>,
    #[sea_orm(column_name = "traceContext")]
    pub trace_context: Option<Json>,
    #[sea_orm(column_name = "traceId")]
    pub trace_id: String,
    #[sea_orm(column_name = "spanId")]
    pub span_id: String,
    #[sea_orm(column_name = "runtimeEnvironmentId")]
    pub runtime_environment_id: String,
    #[sea_orm(column_name = "environmentType")]
    pub environment_type: Option<RuntimeEnvironmentType>,
    #[sea_orm(column_name = "projectId")]
    pub project_id: String,
    #[sea_orm(column_name = "organizationId")]
    pub organization_id: Option<String>,
    pub queue: String,
    #[sea_orm(column_name = "lockedQueueId")]
    pub locked_queue_id: Option<String>,
    #[sea_orm(column_name = "masterQueue", default_value = "main")]
    pub worker_queue: String,
    pub region: Option<String>,
    #[sea_orm(column_name = "secondaryMasterQueue")]
    pub secondary_master_queue: Option<String>,
    #[sea_orm(column_name = "attemptNumber")]
    pub attempt_number: Option<i32>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "runTags")]
    pub run_tags: Vec<String>,
    #[sea_orm(column_name = "taskVersion")]
    pub task_version: Option<String>,
    #[sea_orm(column_name = "sdkVersion")]
    pub sdk_version: Option<String>,
    #[sea_orm(column_name = "cliVersion")]
    pub cli_version: Option<String>,
    #[sea_orm(column_name = "startedAt")]
    pub started_at: Option<DateTime>,
    #[sea_orm(column_name = "executedAt")]
    pub executed_at: Option<DateTime>,
    #[sea_orm(column_name = "completedAt")]
    pub completed_at: Option<DateTime>,
    #[sea_orm(column_name = "machinePreset")]
    pub machine_preset: Option<String>,
    #[sea_orm(column_name = "usageDurationMs", default_value = 0)]
    pub usage_duration_ms: i32,
    #[sea_orm(column_name = "costInCents", default_value = 0)]
    pub cost_in_cents: f64,
    #[sea_orm(column_name = "baseCostInCents", default_value = 0)]
    pub base_cost_in_cents: f64,
    #[sea_orm(column_name = "lockedAt")]
    pub locked_at: Option<DateTime>,
    #[sea_orm(column_name = "lockedById")]
    pub locked_by_id: Option<String>,
    #[sea_orm(column_name = "lockedToVersionId")]
    pub locked_to_version_id: Option<String>,
    #[sea_orm(column_name = "priorityMs", default_value = 0)]
    pub priority_ms: i32,
    #[sea_orm(column_name = "concurrencyKey")]
    pub concurrency_key: Option<String>,
    pub gates: Option<Json>,
    #[sea_orm(column_name = "delayUntil")]
    pub delay_until: Option<DateTime>,
    #[sea_orm(column_name = "queuedAt")]
    pub queued_at: Option<DateTime>,
    pub ttl: Option<String>,
    #[sea_orm(column_name = "expiredAt")]
    pub expired_at: Option<DateTime>,
    #[sea_orm(column_name = "maxAttempts")]
    pub max_attempts: Option<i32>,
    #[sea_orm(column_name = "lockedRetryConfig")]
    pub locked_retry_config: Option<Json>,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "oneTimeUseToken")]
    pub one_time_use_token: Option<String>,
    #[sea_orm(column_name = "taskEventStore", default_value = "taskEvent")]
    pub task_event_store: String,
    #[sea_orm(column_name = "queueTimestamp")]
    pub queue_timestamp: Option<DateTime>,
    #[sea_orm(column_name = "scheduleInstanceId")]
    pub schedule_instance_id: Option<String>,
    #[sea_orm(column_name = "scheduleId")]
    pub schedule_id: Option<String>,
    #[sea_orm(column_name = "bulkActionGroupIds")]
    pub bulk_action_group_ids: Vec<String>,
    #[sea_orm(column_name = "logsDeletedAt")]
    pub logs_deleted_at: Option<DateTime>,
    #[sea_orm(column_name = "replayedFromTaskRunFriendlyId")]
    pub replayed_from_task_run_friendly_id: Option<String>,
    #[sea_orm(column_name = "rootTaskRunId")]
    pub root_task_run_id: Option<String>,
    #[sea_orm(column_name = "parentTaskRunId")]
    pub parent_task_run_id: Option<String>,
    #[sea_orm(column_name = "parentTaskRunAttemptId")]
    pub parent_task_run_attempt_id: Option<String>,
    #[sea_orm(column_name = "batchId")]
    pub batch_id: Option<String>,
    #[sea_orm(column_name = "resumeParentOnCompletion", default_value = false)]
    pub resume_parent_on_completion: bool,
    #[sea_orm(default_value = 0)]
    pub depth: i32,
    #[sea_orm(column_name = "parentSpanId")]
    pub parent_span_id: Option<String>,
    #[sea_orm(column_name = "runChainState")]
    pub run_chain_state: Option<Json>,
    #[sea_orm(column_name = "seedMetadata")]
    pub seed_metadata: Option<String>,
    #[sea_orm(column_name = "seedMetadataType", default_value = "application/json")]
    pub seed_metadata_type: String,
    pub metadata: Option<String>,
    #[sea_orm(column_name = "metadataType", default_value = "application/json")]
    pub metadata_type: String,
    #[sea_orm(column_name = "metadataVersion", default_value = 1)]
    pub metadata_version: i32,
    pub annotations: Option<Json>,
    #[sea_orm(column_name = "isWarmStart")]
    pub is_warm_start: Option<bool>,
    pub output: Option<String>,
    #[sea_orm(column_name = "outputType", default_value = "application/json")]
    pub output_type: String,
    pub error: Option<Json>,
    #[sea_orm(column_name = "planType")]
    pub plan_type: Option<String>,
    #[sea_orm(column_name = "maxDurationInSeconds")]
    pub max_duration_in_seconds: Option<i32>,
    #[sea_orm(column_name = "realtimeStreamsVersion", default_value = "v1")]
    pub realtime_streams_version: String,
    #[sea_orm(column_name = "realtimeStreams")]
    pub realtime_streams: Vec<String>,
    #[sea_orm(column_name = "streamBasinName")]
    pub stream_basin_name: Option<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::runtime_environment::Entity",
        from = "Column::RuntimeEnvironmentId",
        to = "super::runtime_environment::Column::Id",
        on_update = "Cascade",
        on_delete = "Cascade"
    )]
    RuntimeEnvironment,
    #[sea_orm(
        belongs_to = "super::project::Entity",
        from = "Column::ProjectId",
        to = "super::project::Column::Id",
        on_update = "Cascade",
        on_delete = "Cascade"
    )]
    Project,
    #[sea_orm(
        belongs_to = "super::background_worker_task::Entity",
        from = "Column::LockedById",
        to = "super::background_worker_task::Column::Id"
    )]
    LockedBy,
    #[sea_orm(
        belongs_to = "super::background_worker::Entity",
        from = "Column::LockedToVersionId",
        to = "super::background_worker::Column::Id"
    )]
    LockedToVersion,
    #[sea_orm(
        belongs_to = "super::task_run::Entity",
        from = "Column::RootTaskRunId",
        to = "super::task_run::Column::Id",
        on_update = "NoAction",
        on_delete = "SetNull"
    )]
    RootTaskRun,
    #[sea_orm(
        belongs_to = "super::task_run::Entity",
        from = "Column::ParentTaskRunId",
        to = "super::task_run::Column::Id",
        on_update = "NoAction",
        on_delete = "SetNull"
    )]
    ParentTaskRun,
    #[sea_orm(
        belongs_to = "super::task_run_attempt::Entity",
        from = "Column::ParentTaskRunAttemptId",
        to = "super::task_run_attempt::Column::Id",
        on_update = "NoAction",
        on_delete = "SetNull"
    )]
    ParentTaskRunAttempt,
    #[sea_orm(
        belongs_to = "super::batch_task_run::Entity",
        from = "Column::BatchId",
        to = "super::batch_task_run::Column::Id",
        on_update = "NoAction",
        on_delete = "SetNull"
    )]
    Batch,
    #[sea_orm(
        has_many = "super::task_run_attempt::Entity",
        from = "Column::Id",
        to = "super::task_run_attempt::Column::TaskRunId"
    )]
    Attempts,
    #[sea_orm(has_many = "super::checkpoint::Entity")]
    Checkpoints,
    #[sea_orm(has_many = "super::task_run_waitpoint::Entity")]
    BlockedByWaitpoints,
    #[sea_orm(has_many = "super::batch_task_run_item::Entity")]
    BatchItems,
    #[sea_orm(has_many = "super::checkpoint_restore_event::Entity")]
    CheckpointRestoreEvent,
    #[sea_orm(has_many = "super::task_run_execution_snapshot::Entity")]
    ExecutionSnapshots,
    #[sea_orm(
        has_many = "super::bulk_action_item::Entity",
        from = "Column::Id",
        to = "super::bulk_action_item::Column::SourceRunId"
    )]
    SourceBulkActionItems,
    #[sea_orm(
        has_many = "super::bulk_action_item::Entity",
        from = "Column::Id",
        to = "super::bulk_action_item::Column::DestinationRunId"
    )]
    DestinationBulkActionItems,
    #[sea_orm(
        has_many = "super::task_run::Entity",
        from = "Column::Id",
        to = "super::task_run::Column::RootTaskRunId"
    )]
    DescendantRuns,
    #[sea_orm(
        has_many = "super::task_run::Entity",
        from = "Column::Id",
        to = "super::task_run::Column::ParentTaskRunId"
    )]
    ChildRuns,
    #[sea_orm(has_many = "super::playground_conversation::Entity")]
    PlaygroundConversations,
    #[sea_orm(has_one = "super::waitpoint::Entity")]
    AssociatedWaitpoint,
    #[sea_orm(has_one = "super::task_run_dependency::Entity")]
    Dependency,
}

impl Related<super::runtime_environment::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::RuntimeEnvironment.def()
    }
}

impl Related<super::project::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Project.def()
    }
}

impl Related<super::background_worker_task::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::LockedBy.def()
    }
}

impl Related<super::background_worker::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::LockedToVersion.def()
    }
}

impl Related<super::task_run::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::RootTaskRun.def()
    }
}

impl Related<super::task_run_attempt::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ParentTaskRunAttempt.def()
    }
}

impl Related<super::batch_task_run::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Batch.def()
    }
}

impl Related<super::checkpoint::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Checkpoints.def()
    }
}

impl Related<super::task_run_waitpoint::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::BlockedByWaitpoints.def()
    }
}

impl Related<super::batch_task_run_item::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::BatchItems.def()
    }
}

impl Related<super::checkpoint_restore_event::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::CheckpointRestoreEvent.def()
    }
}

impl Related<super::task_run_execution_snapshot::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ExecutionSnapshots.def()
    }
}

impl Related<super::bulk_action_item::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::SourceBulkActionItems.def()
    }
}

impl Related<super::playground_conversation::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::PlaygroundConversations.def()
    }
}

impl Related<super::waitpoint::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::AssociatedWaitpoint.def()
    }
}

impl Related<super::task_run_dependency::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Dependency.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.created_at.is_not_set() {
            self.created_at = Set(now.naive_utc());
        }
        if !self.updated_at.is_set() {
            self.updated_at = Set(now.naive_utc());
        }
        Ok(self)
    }
}
