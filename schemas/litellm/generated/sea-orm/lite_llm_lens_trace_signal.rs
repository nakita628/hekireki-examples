use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "LiteLLM_LensTraceSignal")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub trace_id: String,
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(default_value = "")]
    pub trace_ref: String,
    pub config_key: String,
    pub span_count: i32,
    pub claimed_until: Option<DateTime>,
    pub classified_at: Option<DateTime>,
    pub data: Json,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {}

impl ActiveModelBehavior for ActiveModel {}
