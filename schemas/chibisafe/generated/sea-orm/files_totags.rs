use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "_filesTotags")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false, column_name = "A")]
    pub files_id: i32,
    #[sea_orm(primary_key, auto_increment = false, column_name = "B")]
    pub tags_id: i32,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::files::Entity",
        from = "Column::FilesId",
        to = "super::files::Column::Id"
    )]
    Files,
    #[sea_orm(
        belongs_to = "super::tags::Entity",
        from = "Column::TagsId",
        to = "super::tags::Column::Id"
    )]
    Tags,
}

impl ActiveModelBehavior for ActiveModel {}
