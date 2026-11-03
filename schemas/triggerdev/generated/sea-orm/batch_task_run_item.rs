use super::batch_task_run_item_status::BatchTaskRunItemStatus;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "BatchTaskRunItem")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(default_value = "PENDING")]
    pub status: BatchTaskRunItemStatus,
    #[sea_orm(column_name = "batchTaskRunId")]
    pub batch_task_run_id: String,
    #[sea_orm(column_name = "taskRunId")]
    pub task_run_id: String,
    #[sea_orm(column_name = "taskRunAttemptId")]
    pub task_run_attempt_id: Option<String>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "completedAt")]
    pub completed_at: Option<DateTime>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::batch_task_run::Entity",
        from = "Column::BatchTaskRunId",
        to = "super::batch_task_run::Column::Id",
        on_update = "Cascade",
        on_delete = "Cascade"
    )]
    BatchTaskRun,
    #[sea_orm(
        belongs_to = "super::task_run::Entity",
        from = "Column::TaskRunId",
        to = "super::task_run::Column::Id",
        on_update = "Cascade",
        on_delete = "Cascade"
    )]
    TaskRun,
    #[sea_orm(
        belongs_to = "super::task_run_attempt::Entity",
        from = "Column::TaskRunAttemptId",
        to = "super::task_run_attempt::Column::Id",
        on_update = "Cascade",
        on_delete = "SetNull"
    )]
    TaskRunAttempt,
}

impl Related<super::batch_task_run::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::BatchTaskRun.def()
    }
}

impl Related<super::task_run::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::TaskRun.def()
    }
}

impl Related<super::task_run_attempt::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::TaskRunAttempt.def()
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
