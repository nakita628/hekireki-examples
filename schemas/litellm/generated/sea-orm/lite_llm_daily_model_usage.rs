use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "LiteLLM_DailyModelUsage")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub date: String,
    #[sea_orm(primary_key, auto_increment = false)]
    pub model_group: String,
    #[sea_orm(primary_key, auto_increment = false)]
    pub model: String,
    #[sea_orm(primary_key, auto_increment = false)]
    pub custom_llm_provider: String,
    #[sea_orm(primary_key, auto_increment = false)]
    pub task_type: String,
    #[sea_orm(default_value = 0)]
    pub spend: f64,
    #[sea_orm(default_value = "0")]
    pub prompt_tokens: i64,
    #[sea_orm(default_value = "0")]
    pub completion_tokens: i64,
    #[sea_orm(default_value = "0")]
    pub request_count: i64,
    #[sea_orm(default_value = "0")]
    pub successful_requests: i64,
    #[sea_orm(default_value = "0")]
    pub failed_requests: i64,
    pub created_at: DateTime,
    pub updated_at: DateTime,
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
        if insert && self.created_at.is_not_set() {
            self.created_at = Set(now.naive_utc());
        }
        if !self.updated_at.is_set() {
            self.updated_at = Set(now.naive_utc());
        }
        Ok(self)
    }
}
