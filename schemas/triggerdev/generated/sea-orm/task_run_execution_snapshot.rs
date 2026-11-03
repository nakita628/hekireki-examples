use super::run_engine_version::RunEngineVersion;
use super::runtime_environment_type::RuntimeEnvironmentType;
use super::task_run_execution_status::TaskRunExecutionStatus;
use super::task_run_status::TaskRunStatus;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "TaskRunExecutionSnapshot")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(default_value = "V2")]
    pub engine: RunEngineVersion,
    #[sea_orm(column_name = "executionStatus")]
    pub execution_status: TaskRunExecutionStatus,
    pub description: String,
    #[sea_orm(column_name = "isValid", default_value = true)]
    pub is_valid: bool,
    pub error: Option<String>,
    #[sea_orm(column_name = "previousSnapshotId")]
    pub previous_snapshot_id: Option<String>,
    #[sea_orm(column_name = "runId")]
    pub run_id: String,
    #[sea_orm(column_name = "runStatus")]
    pub run_status: TaskRunStatus,
    #[sea_orm(column_name = "batchId")]
    pub batch_id: Option<String>,
    #[sea_orm(column_name = "attemptNumber")]
    pub attempt_number: Option<i32>,
    #[sea_orm(column_name = "environmentId")]
    pub environment_id: String,
    #[sea_orm(column_name = "environmentType")]
    pub environment_type: RuntimeEnvironmentType,
    #[sea_orm(column_name = "projectId")]
    pub project_id: String,
    #[sea_orm(column_name = "organizationId")]
    pub organization_id: String,
    #[sea_orm(column_name = "completedWaitpointOrder")]
    pub completed_waitpoint_order: Vec<String>,
    #[sea_orm(column_name = "checkpointId")]
    pub checkpoint_id: Option<String>,
    #[sea_orm(column_name = "workerId")]
    pub worker_id: Option<String>,
    #[sea_orm(column_name = "runnerId")]
    pub runner_id: Option<String>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "lastHeartbeatAt")]
    pub last_heartbeat_at: Option<DateTime>,
    pub metadata: Option<Json>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::task_run::Entity",
        from = "Column::RunId",
        to = "super::task_run::Column::Id"
    )]
    Run,
    #[sea_orm(
        belongs_to = "super::task_run_checkpoint::Entity",
        from = "Column::CheckpointId",
        to = "super::task_run_checkpoint::Column::Id"
    )]
    Checkpoint,
}

impl Related<super::task_run::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Run.def()
    }
}

impl Related<super::task_run_checkpoint::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Checkpoint.def()
    }
}

impl Related<super::waitpoint::Entity> for Entity {
    fn to() -> RelationDef {
        super::completed_waitpoints::Relation::Waitpoint.def()
    }
    fn via() -> Option<RelationDef> {
        Some(super::completed_waitpoints::Relation::TaskRunExecutionSnapshot.def().rev())
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
