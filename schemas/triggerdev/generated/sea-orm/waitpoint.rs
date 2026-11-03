use super::waitpoint_status::WaitpointStatus;
use super::waitpoint_type::WaitpointType;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Waitpoint")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "friendlyId")]
    pub friendly_id: String,
    pub r#type: WaitpointType,
    #[sea_orm(default_value = "PENDING")]
    pub status: WaitpointStatus,
    #[sea_orm(column_name = "completedAt")]
    pub completed_at: Option<DateTime>,
    #[sea_orm(column_name = "idempotencyKey")]
    pub idempotency_key: String,
    #[sea_orm(column_name = "userProvidedIdempotencyKey")]
    pub user_provided_idempotency_key: bool,
    #[sea_orm(column_name = "idempotencyKeyExpiresAt")]
    pub idempotency_key_expires_at: Option<DateTime>,
    #[sea_orm(column_name = "inactiveIdempotencyKey")]
    pub inactive_idempotency_key: Option<String>,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "completedByTaskRunId")]
    pub completed_by_task_run_id: Option<String>,
    #[sea_orm(column_name = "completedAfter")]
    pub completed_after: Option<DateTime>,
    #[sea_orm(column_name = "completedByBatchId")]
    pub completed_by_batch_id: Option<String>,
    pub output: Option<String>,
    #[sea_orm(column_name = "outputType", default_value = "application/json")]
    pub output_type: String,
    #[sea_orm(column_name = "outputIsError", default_value = false)]
    pub output_is_error: bool,
    #[sea_orm(column_name = "projectId")]
    pub project_id: String,
    #[sea_orm(column_name = "environmentId")]
    pub environment_id: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    pub tags: Vec<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::task_run::Entity",
        from = "Column::CompletedByTaskRunId",
        to = "super::task_run::Column::Id",
        on_delete = "SetNull"
    )]
    CompletedByTaskRun,
    #[sea_orm(
        belongs_to = "super::batch_task_run::Entity",
        from = "Column::CompletedByBatchId",
        to = "super::batch_task_run::Column::Id",
        on_delete = "SetNull"
    )]
    CompletedByBatch,
    #[sea_orm(
        belongs_to = "super::project::Entity",
        from = "Column::ProjectId",
        to = "super::project::Column::Id",
        on_update = "Cascade",
        on_delete = "Cascade"
    )]
    Project,
    #[sea_orm(
        belongs_to = "super::runtime_environment::Entity",
        from = "Column::EnvironmentId",
        to = "super::runtime_environment::Column::Id",
        on_update = "Cascade",
        on_delete = "Cascade"
    )]
    Environment,
    #[sea_orm(has_many = "super::task_run_waitpoint::Entity")]
    BlockingTaskRuns,
}

impl Related<super::task_run::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::CompletedByTaskRun.def()
    }
}

impl Related<super::batch_task_run::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::CompletedByBatch.def()
    }
}

impl Related<super::project::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Project.def()
    }
}

impl Related<super::runtime_environment::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Environment.def()
    }
}

impl Related<super::task_run_waitpoint::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::BlockingTaskRuns.def()
    }
}

impl Related<super::task_run_execution_snapshot::Entity> for Entity {
    fn to() -> RelationDef {
        super::completed_waitpoints::Relation::TaskRunExecutionSnapshot.def()
    }
    fn via() -> Option<RelationDef> {
        Some(super::completed_waitpoints::Relation::Waitpoint.def().rev())
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
