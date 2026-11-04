use super::prisma_date_time::PrismaDateTime;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "scheduled_job_runs")]
pub struct Model {
    #[sea_orm(primary_key)]
    pub id: i32,
    #[sea_orm(column_name = "jobId")]
    pub job_id: i32,
    #[sea_orm(default_value = "queued")]
    pub status: String,
    pub result: Option<String>,
    pub error: Option<String>,
    #[sea_orm(column_name = "startedAt")]
    pub started_at: PrismaDateTime,
    #[sea_orm(column_name = "completedAt")]
    pub completed_at: Option<PrismaDateTime>,
    #[sea_orm(column_name = "readAt")]
    pub read_at: Option<PrismaDateTime>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::scheduled_jobs::Entity",
        from = "Column::JobId",
        to = "super::scheduled_jobs::Column::Id",
        on_delete = "Cascade"
    )]
    Job,
}

impl Related<super::scheduled_jobs::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Job.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.started_at.is_not_set() {
            self.started_at = Set(now.into());
        }
        Ok(self)
    }
}
