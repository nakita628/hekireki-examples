use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "LiteLLM_Config")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub param_name: String,
    pub param_value: Option<Json>,
    pub last_run_at: Option<DateTime>,
    #[sea_orm(default_value = "0")]
    pub reload_revision: i64,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {}

impl ActiveModelBehavior for ActiveModel {}
