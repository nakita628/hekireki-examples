use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "LiteLLM_AutoRouterSession")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub api_key: String,
    #[sea_orm(primary_key, auto_increment = false)]
    pub session_id: String,
    #[sea_orm(primary_key, auto_increment = false)]
    pub router_name: String,
    pub router_type: String,
    pub first_turn_at: DateTime,
    pub last_turn_at: DateTime,
    pub last_model: String,
    #[sea_orm(default_value = "{}")]
    pub models: Json,
    #[sea_orm(default_value = 0)]
    pub turns: i32,
    #[sea_orm(default_value = 0)]
    pub unordered_turns: i32,
    #[sea_orm(default_value = 0)]
    pub covered_turns: i32,
    #[sea_orm(default_value = 0)]
    pub cache_hits: i32,
    #[sea_orm(default_value = 0)]
    pub same_model_turns: i32,
    #[sea_orm(default_value = 0)]
    pub same_model_hits: i32,
    #[sea_orm(default_value = 0)]
    pub first_visit_turns: i32,
    #[sea_orm(default_value = 0)]
    pub first_visit_hits: i32,
    #[sea_orm(default_value = 0)]
    pub return_turns: i32,
    #[sea_orm(default_value = 0)]
    pub return_hits: i32,
    #[sea_orm(default_value = 0)]
    pub return_expired_misses: i32,
    #[sea_orm(default_value = 0)]
    pub return_within_ttl_misses: i32,
    #[sea_orm(default_value = 0)]
    pub ttl_5m_turns: i32,
    #[sea_orm(default_value = 0)]
    pub ttl_1h_turns: i32,
    #[sea_orm(default_value = "0")]
    pub total_tokens: i64,
    #[sea_orm(default_value = 0)]
    pub spend: f64,
    #[sea_orm(default_value = 0)]
    pub saved_spend: f64,
    #[sea_orm(default_value = 0)]
    pub savings_estimated_turns: i32,
    #[sea_orm(default_value = 0)]
    pub savings_estimated_actual_spend: f64,
    #[sea_orm(default_value = 0)]
    pub savings_estimated_saved_spend: f64,
    #[sea_orm(default_value = "{}")]
    pub savings_estimated_baseline_models: Json,
    #[sea_orm(default_value = 0)]
    pub classifier_cost: f64,
    #[sea_orm(default_value = 0)]
    pub classifier_cost_recorded_turns: i32,
    #[sea_orm(default_value = "{}")]
    pub tier_turns: Json,
    #[sea_orm(default_value = "{}")]
    pub baseline_models: Json,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {}

impl ActiveModelBehavior for ActiveModel {}
