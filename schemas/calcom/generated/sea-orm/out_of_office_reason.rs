use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "OutOfOfficeReason")]
pub struct Model {
    #[sea_orm(primary_key)]
    pub id: i32,
    pub emoji: String,
    #[sea_orm(unique)]
    pub reason: String,
    #[sea_orm(default_value = true)]
    pub enabled: bool,
    #[sea_orm(column_name = "userId")]
    pub user_id: Option<i32>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::UserId",
        to = "super::user::Column::Id",
        on_delete = "Cascade"
    )]
    User,
    #[sea_orm(has_many = "super::out_of_office_entry::Entity")]
    Entries,
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::User.def()
    }
}

impl Related<super::out_of_office_entry::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Entries.def()
    }
}

impl ActiveModelBehavior for ActiveModel {}
