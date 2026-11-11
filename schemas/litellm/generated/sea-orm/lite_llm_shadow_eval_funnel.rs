use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "LiteLLM_ShadowEvalFunnel")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub job_id: String,
    #[sea_orm(default_value = 0)]
    pub not_sampled: i32,
    #[sea_orm(default_value = 0)]
    pub unjudgeable: i32,
    #[sea_orm(default_value = 0)]
    pub shed: i32,
    #[sea_orm(default_value = 0)]
    pub withheld: i32,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {}

impl ActiveModelBehavior for ActiveModel {}
