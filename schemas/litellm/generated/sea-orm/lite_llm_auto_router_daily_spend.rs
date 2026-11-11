use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "LiteLLM_AutoRouterDailySpend")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub date: String,
    #[sea_orm(primary_key, auto_increment = false)]
    pub api_key: String,
    #[sea_orm(primary_key, auto_increment = false)]
    pub user_id: String,
    #[sea_orm(primary_key, auto_increment = false)]
    pub router_name: String,
    #[sea_orm(primary_key, auto_increment = false)]
    pub router_type: String,
    #[sea_orm(default_value = 0)]
    pub turns: i32,
    #[sea_orm(default_value = "0")]
    pub total_tokens: i64,
    #[sea_orm(default_value = 0)]
    pub token_recorded_turns: i32,
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
    #[sea_orm(default_value = 0)]
    pub classifier_cost: f64,
    #[sea_orm(default_value = 0)]
    pub classifier_cost_recorded_turns: i32,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {}

impl ActiveModelBehavior for ActiveModel {}
