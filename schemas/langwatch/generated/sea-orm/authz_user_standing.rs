use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "AuthzUserStanding")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_name = "userId")]
    pub user_id: String,
    #[sea_orm(column_name = "deactivatedAt")]
    pub deactivated_at: Option<DateTime>,
    #[sea_orm(column_name = "erasedAt")]
    pub erased_at: Option<DateTime>,
    #[sea_orm(column_name = "standingChangedAt")]
    pub standing_changed_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {}

impl ActiveModelBehavior for ActiveModel {}
