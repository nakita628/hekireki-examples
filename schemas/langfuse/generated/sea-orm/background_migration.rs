use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "background_migrations")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(unique)]
    pub name: String,
    pub script: String,
    pub args: Json,
    #[sea_orm(default_value = "{}")]
    pub state: Json,
    pub finished_at: Option<DateTime>,
    pub failed_at: Option<DateTime>,
    pub failed_reason: Option<String>,
    pub worker_id: Option<String>,
    pub locked_at: Option<DateTime>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {}

impl ActiveModelBehavior for ActiveModel {}
