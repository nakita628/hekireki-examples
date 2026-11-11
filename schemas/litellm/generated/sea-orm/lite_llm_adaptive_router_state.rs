use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "LiteLLM_AdaptiveRouterState")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub router_name: String,
    #[sea_orm(primary_key, auto_increment = false)]
    pub request_type: String,
    #[sea_orm(primary_key, auto_increment = false)]
    pub model_name: String,
    pub alpha: f64,
    pub beta: f64,
    #[sea_orm(default_value = 0)]
    pub total_samples: i32,
    pub last_updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, _insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if !self.last_updated_at.is_set() {
            self.last_updated_at = Set(now.naive_utc());
        }
        Ok(self)
    }
}
