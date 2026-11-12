use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "tagsToNote")]
pub struct Model {
    pub id: i32,
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_name = "noteId", default_value = 0)]
    pub note_id: i32,
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_name = "tagId", default_value = 0)]
    pub tag_id: i32,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::notes::Entity",
        from = "Column::NoteId",
        to = "super::notes::Column::Id"
    )]
    Note,
    #[sea_orm(
        belongs_to = "super::tag::Entity",
        from = "Column::TagId",
        to = "super::tag::Column::Id"
    )]
    Tag,
}

impl Related<super::notes::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Note.def()
    }
}

impl Related<super::tag::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Tag.def()
    }
}

impl ActiveModelBehavior for ActiveModel {}
