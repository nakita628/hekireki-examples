use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "LiteLLM_DailyGuardrailMetrics")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub guardrail_id: String,
    #[sea_orm(primary_key, auto_increment = false)]
    pub date: String,
    #[sea_orm(default_value = "0")]
    pub requests_evaluated: i64,
    #[sea_orm(default_value = "0")]
    pub passed_count: i64,
    #[sea_orm(default_value = "0")]
    pub blocked_count: i64,
    #[sea_orm(default_value = "0")]
    pub flagged_count: i64,
    pub avg_score: Option<f64>,
    pub avg_latency_ms: Option<f64>,
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
