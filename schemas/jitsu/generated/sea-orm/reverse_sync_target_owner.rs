use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "reverse_sync_target_owner")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub target_hash: String,
    pub workspace_id: String,
    pub sync_id: String,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {}

impl ActiveModelBehavior for ActiveModel {}
