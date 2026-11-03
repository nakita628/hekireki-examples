use super::checkpoint_restore_event_type::CheckpointRestoreEventType;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "CheckpointRestoreEvent")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub r#type: CheckpointRestoreEventType,
    pub reason: Option<String>,
    pub metadata: Option<String>,
    #[sea_orm(column_name = "checkpointId")]
    pub checkpoint_id: String,
    #[sea_orm(column_name = "runId")]
    pub run_id: String,
    #[sea_orm(column_name = "attemptId")]
    pub attempt_id: String,
    #[sea_orm(column_name = "projectId")]
    pub project_id: String,
    #[sea_orm(column_name = "runtimeEnvironmentId")]
    pub runtime_environment_id: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::checkpoint::Entity",
        from = "Column::CheckpointId",
        to = "super::checkpoint::Column::Id",
        on_update = "Cascade",
        on_delete = "Cascade"
    )]
    Checkpoint,
    #[sea_orm(
        belongs_to = "super::task_run::Entity",
        from = "Column::RunId",
        to = "super::task_run::Column::Id",
        on_update = "Cascade",
        on_delete = "Cascade"
    )]
    Run,
    #[sea_orm(
        belongs_to = "super::task_run_attempt::Entity",
        from = "Column::AttemptId",
        to = "super::task_run_attempt::Column::Id",
        on_update = "Cascade",
        on_delete = "Cascade"
    )]
    Attempt,
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
        from = "Column::RuntimeEnvironmentId",
        to = "super::runtime_environment::Column::Id",
        on_update = "Cascade",
        on_delete = "Cascade"
    )]
    RuntimeEnvironment,
    #[sea_orm(has_one = "super::task_run_dependency::Entity")]
    TaskRunDependency,
    #[sea_orm(has_one = "super::batch_task_run::Entity")]
    BatchTaskRunDependency,
}

impl Related<super::checkpoint::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Checkpoint.def()
    }
}

impl Related<super::task_run::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Run.def()
    }
}

impl Related<super::task_run_attempt::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Attempt.def()
    }
}

impl Related<super::project::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Project.def()
    }
}

impl Related<super::runtime_environment::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::RuntimeEnvironment.def()
    }
}

impl Related<super::task_run_dependency::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::TaskRunDependency.def()
    }
}

impl Related<super::batch_task_run::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::BatchTaskRunDependency.def()
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
