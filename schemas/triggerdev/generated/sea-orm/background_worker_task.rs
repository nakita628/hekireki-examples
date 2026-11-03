use super::task_trigger_source::TaskTriggerSource;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "BackgroundWorkerTask")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub slug: String,
    pub description: Option<String>,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "friendlyId")]
    pub friendly_id: String,
    #[sea_orm(column_name = "filePath")]
    pub file_path: String,
    #[sea_orm(column_name = "exportName")]
    pub export_name: Option<String>,
    #[sea_orm(column_name = "workerId")]
    pub worker_id: String,
    #[sea_orm(column_name = "projectId")]
    pub project_id: String,
    #[sea_orm(column_name = "fileId")]
    pub file_id: Option<String>,
    #[sea_orm(column_name = "runtimeEnvironmentId")]
    pub runtime_environment_id: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "queueConfig")]
    pub queue_config: Option<Json>,
    #[sea_orm(column_name = "retryConfig")]
    pub retry_config: Option<Json>,
    #[sea_orm(column_name = "machineConfig")]
    pub machine_config: Option<Json>,
    pub gates: Option<Json>,
    #[sea_orm(column_name = "queueId")]
    pub queue_id: Option<String>,
    #[sea_orm(column_name = "maxDurationInSeconds")]
    pub max_duration_in_seconds: Option<i32>,
    pub ttl: Option<String>,
    #[sea_orm(column_name = "triggerSource", default_value = "STANDARD")]
    pub trigger_source: TaskTriggerSource,
    pub config: Option<Json>,
    #[sea_orm(column_name = "payloadSchema")]
    pub payload_schema: Option<Json>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::background_worker::Entity",
        from = "Column::WorkerId",
        to = "super::background_worker::Column::Id",
        on_update = "Cascade",
        on_delete = "Cascade"
    )]
    Worker,
    #[sea_orm(
        belongs_to = "super::project::Entity",
        from = "Column::ProjectId",
        to = "super::project::Column::Id",
        on_update = "Cascade",
        on_delete = "Cascade"
    )]
    Project,
    #[sea_orm(
        belongs_to = "super::background_worker_file::Entity",
        from = "Column::FileId",
        to = "super::background_worker_file::Column::Id",
        on_update = "Cascade",
        on_delete = "Cascade"
    )]
    File,
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
        on_delete = "SetNull"
    )]
    Queue,
    #[sea_orm(has_many = "super::task_run_attempt::Entity")]
    Attempts,
    #[sea_orm(has_many = "super::task_run::Entity")]
    Runs,
}

impl Related<super::background_worker::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Worker.def()
    }
}

impl Related<super::project::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Project.def()
    }
}

impl Related<super::background_worker_file::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::File.def()
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

impl Related<super::task_run_attempt::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Attempts.def()
    }
}

impl Related<super::task_run::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Runs.def()
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
