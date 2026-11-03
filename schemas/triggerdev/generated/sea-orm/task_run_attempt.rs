use super::task_run_attempt_status::TaskRunAttemptStatus;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "TaskRunAttempt")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(default_value = 0)]
    pub number: i32,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "friendlyId")]
    pub friendly_id: String,
    #[sea_orm(column_name = "taskRunId")]
    pub task_run_id: String,
    #[sea_orm(column_name = "backgroundWorkerId")]
    pub background_worker_id: String,
    #[sea_orm(column_name = "backgroundWorkerTaskId")]
    pub background_worker_task_id: String,
    #[sea_orm(column_name = "runtimeEnvironmentId")]
    pub runtime_environment_id: String,
    #[sea_orm(column_name = "queueId")]
    pub queue_id: String,
    #[sea_orm(default_value = "PENDING")]
    pub status: TaskRunAttemptStatus,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "startedAt")]
    pub started_at: Option<DateTime>,
    #[sea_orm(column_name = "completedAt")]
    pub completed_at: Option<DateTime>,
    #[sea_orm(column_name = "usageDurationMs", default_value = 0)]
    pub usage_duration_ms: i32,
    pub error: Option<Json>,
    pub output: Option<String>,
    #[sea_orm(column_name = "outputType", default_value = "application/json")]
    pub output_type: String,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::task_run::Entity",
        from = "Column::TaskRunId",
        to = "super::task_run::Column::Id",
        on_update = "Cascade",
        on_delete = "Cascade"
    )]
    TaskRun,
    #[sea_orm(
        belongs_to = "super::background_worker::Entity",
        from = "Column::BackgroundWorkerId",
        to = "super::background_worker::Column::Id",
        on_update = "Cascade",
        on_delete = "Cascade"
    )]
    BackgroundWorker,
    #[sea_orm(
        belongs_to = "super::background_worker_task::Entity",
        from = "Column::BackgroundWorkerTaskId",
        to = "super::background_worker_task::Column::Id",
        on_update = "Cascade",
        on_delete = "Cascade"
    )]
    BackgroundWorkerTask,
    #[sea_orm(
        belongs_to = "super::runtime_environment::Entity",
        from = "Column::RuntimeEnvironmentId",
        to = "super::runtime_environment::Column::Id",
        on_update = "Cascade",
        on_delete = "Cascade"
    )]
    RuntimeEnvironment,
    #[sea_orm(
        belongs_to = "super::task_queue::Entity",
        from = "Column::QueueId",
        to = "super::task_queue::Column::Id",
        on_update = "Cascade",
        on_delete = "Cascade"
    )]
    Queue,
    #[sea_orm(has_many = "super::task_run_dependency::Entity")]
    Dependencies,
    #[sea_orm(has_many = "super::batch_task_run::Entity")]
    BatchDependencies,
    #[sea_orm(has_many = "super::checkpoint::Entity")]
    Checkpoints,
    #[sea_orm(has_many = "super::batch_task_run_item::Entity")]
    BatchTaskRunItems,
    #[sea_orm(has_many = "super::checkpoint_restore_event::Entity")]
    CheckpointRestoreEvent,
    #[sea_orm(
        has_many = "super::task_run::Entity",
        from = "Column::Id",
        to = "super::task_run::Column::ParentTaskRunAttemptId"
    )]
    ChildRuns,
}

impl Related<super::task_run::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::TaskRun.def()
    }
}

impl Related<super::background_worker::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::BackgroundWorker.def()
    }
}

impl Related<super::background_worker_task::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::BackgroundWorkerTask.def()
    }
}

impl Related<super::runtime_environment::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::RuntimeEnvironment.def()
    }
}

impl Related<super::task_queue::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Queue.def()
    }
}

impl Related<super::task_run_dependency::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Dependencies.def()
    }
}

impl Related<super::batch_task_run::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::BatchDependencies.def()
    }
}

impl Related<super::checkpoint::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Checkpoints.def()
    }
}

impl Related<super::batch_task_run_item::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::BatchTaskRunItems.def()
    }
}

impl Related<super::checkpoint_restore_event::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::CheckpointRestoreEvent.def()
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
