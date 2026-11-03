use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "avatars")]
pub struct Model {
    #[sea_orm(column_name = "teamId", default_value = 0)]
    pub team_id: i32,
    #[sea_orm(column_name = "userId", default_value = 0)]
    pub user_id: i32,
    pub data: String,
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(unique)]
    #[sea_orm(column_name = "objectKey")]
    pub object_key: String,
    #[sea_orm(column_name = "isBanner", default_value = false)]
    pub is_banner: bool,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {}

impl ActiveModelBehavior for ActiveModel {}
