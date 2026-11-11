use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "LiteLLM_AdaptiveRouterSession")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub session_id: String,
    #[sea_orm(primary_key, auto_increment = false)]
    pub router_name: String,
    #[sea_orm(primary_key, auto_increment = false)]
    pub model_name: String,
    pub classified_type: String,
    #[sea_orm(default_value = 0)]
    pub misalignment_count: i32,
    #[sea_orm(default_value = 0)]
    pub stagnation_count: i32,
    #[sea_orm(default_value = 0)]
    pub disengagement_count: i32,
    #[sea_orm(default_value = 0)]
    pub satisfaction_count: i32,
    #[sea_orm(default_value = 0)]
    pub failure_count: i32,
    #[sea_orm(default_value = 0)]
    pub loop_count: i32,
    #[sea_orm(default_value = 0)]
    pub exhaustion_count: i32,
    pub last_user_content: Option<String>,
    pub last_assistant_content: Option<String>,
    #[sea_orm(default_value = "[]")]
    pub tool_call_history: Json,
    #[sea_orm(default_value = "{}")]
    pub pending_tool_calls: Json,
    #[sea_orm(default_value = 0)]
    pub turn_count: i32,
    #[sea_orm(default_value = -1)]
    pub last_processed_turn: i32,
    #[sea_orm(default_value = false)]
    pub clean_credit_awarded: bool,
    pub terminal_status: Option<i32>,
    pub last_activity_at: DateTime,
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
        if !self.last_activity_at.is_set() {
            self.last_activity_at = Set(now.naive_utc());
        }
        Ok(self)
    }
}
