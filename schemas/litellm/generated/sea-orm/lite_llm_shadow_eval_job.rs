use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "LiteLLM_ShadowEvalJob")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub group_id: String,
    #[sea_orm(default_value = "key")]
    pub target_type: String,
    pub target_id: String,
    pub router_name: String,
    pub router_names: Vec<String>,
    pub models: Vec<String>,
    #[sea_orm(default_value = "forward")]
    pub direction: String,
    pub baseline_model: Option<String>,
    pub judge_model: String,
    pub shadow_percentage: f64,
    pub max_turns: i32,
    pub max_budget: Option<f64>,
    pub created_at: DateTime,
    pub created_by: Option<String>,
    pub ends_at: DateTime,
    pub stopped_at: Option<DateTime>,
    pub stopped_by: Option<String>,
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
        Ok(self)
    }
}
