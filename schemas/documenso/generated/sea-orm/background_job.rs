use super::background_job_status::BackgroundJobStatus;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "BackgroundJob")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(default_value = "PENDING")]
    pub status: BackgroundJobStatus,
    pub payload: Option<Json>,
    #[sea_orm(default_value = 0)]
    pub retried: i32,
    #[sea_orm(column_name = "maxRetries", default_value = 3)]
    pub max_retries: i32,
    #[sea_orm(column_name = "jobId")]
    pub job_id: String,
    pub name: String,
    pub version: String,
    #[sea_orm(column_name = "submittedAt")]
    pub submitted_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "completedAt")]
    pub completed_at: Option<DateTime>,
    #[sea_orm(column_name = "lastRetriedAt")]
    pub last_retried_at: Option<DateTime>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(has_many = "super::background_job_task::Entity")]
    Tasks,
}

impl Related<super::background_job_task::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Tasks.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.submitted_at.is_not_set() {
            self.submitted_at = Set(now.naive_utc());
        }
        if !self.updated_at.is_set() {
            self.updated_at = Set(now.naive_utc());
        }
        Ok(self)
    }
}
