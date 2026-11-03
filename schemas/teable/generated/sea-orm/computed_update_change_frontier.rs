use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "computed_update_change_frontier")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub scope_id: String,
    #[sea_orm(primary_key, auto_increment = false)]
    pub kind: String,
    #[sea_orm(primary_key, auto_increment = false)]
    pub table_id: String,
    #[sea_orm(primary_key, auto_increment = false)]
    pub record_id: String,
    #[sea_orm(primary_key, auto_increment = false)]
    pub field_id: String,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {}

impl ActiveModelBehavior for ActiveModel {}
