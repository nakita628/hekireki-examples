use super::job_status::JobStatus;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "LiteLLM_CronJob")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub cronjob_id: String,
    pub pod_id: String,
    #[sea_orm(default_value = "INACTIVE")]
    pub status: JobStatus,
    pub last_updated: DateTime,
    pub ttl: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.last_updated.is_not_set() {
            self.last_updated = Set(now.naive_utc());
        }
        Ok(self)
    }
}
