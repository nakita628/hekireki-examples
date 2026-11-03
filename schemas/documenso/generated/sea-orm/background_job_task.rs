use super::background_job_task_status::BackgroundJobTaskStatus;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "BackgroundJobTask")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub name: String,
    #[sea_orm(default_value = "PENDING")]
    pub status: BackgroundJobTaskStatus,
    pub result: Option<Json>,
    #[sea_orm(default_value = 0)]
    pub retried: i32,
    #[sea_orm(column_name = "maxRetries", default_value = 3)]
    pub max_retries: i32,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "completedAt")]
    pub completed_at: Option<DateTime>,
    #[sea_orm(column_name = "jobId")]
    pub job_id: String,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::background_job::Entity",
        from = "Column::JobId",
        to = "super::background_job::Column::Id",
        on_delete = "Cascade"
    )]
    BackgroundJob,
}

impl Related<super::background_job::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::BackgroundJob.def()
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
