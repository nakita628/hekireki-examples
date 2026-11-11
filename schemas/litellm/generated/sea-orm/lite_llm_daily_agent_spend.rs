use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "LiteLLM_DailyAgentSpend")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub agent_id: Option<String>,
    pub date: String,
    pub api_key: String,
    pub model: Option<String>,
    pub model_group: Option<String>,
    pub custom_llm_provider: Option<String>,
    pub mcp_namespaced_tool_name: Option<String>,
    pub endpoint: Option<String>,
    #[sea_orm(default_value = "0")]
    pub prompt_tokens: i64,
    #[sea_orm(default_value = "0")]
    pub completion_tokens: i64,
    #[sea_orm(default_value = "0")]
    pub cache_read_input_tokens: i64,
    #[sea_orm(default_value = "0")]
    pub cache_creation_input_tokens: i64,
    #[sea_orm(default_value = "0")]
    pub compression_saved_tokens: i64,
    #[sea_orm(default_value = 0)]
    pub compression_savings_spend: f64,
    #[sea_orm(default_value = 0)]
    pub prompt_caching_savings_spend: f64,
    #[sea_orm(default_value = 0)]
    pub gateway_injected_caching_savings_spend: f64,
    #[sea_orm(default_value = 0)]
    pub autorouter_savings_spend: f64,
    #[sea_orm(default_value = 0)]
    pub guardrail_spend: f64,
    #[sea_orm(default_value = 0)]
    pub spend: f64,
    #[sea_orm(default_value = "0")]
    pub api_requests: i64,
    #[sea_orm(default_value = "0")]
    pub successful_requests: i64,
    #[sea_orm(default_value = "0")]
    pub failed_requests: i64,
    #[sea_orm(default_value = "0")]
    pub total_response_time_ms: i64,
    #[sea_orm(default_value = "0")]
    pub timed_requests: i64,
    pub created_at: DateTime,
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    fn new() -> Self {
        Self {
            id: Set(uuid::Uuid::new_v4().to_string()),
            ..ActiveModelTrait::default()
        }
    }

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
