use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "SecondaryEmail")]
pub struct Model {
    #[sea_orm(primary_key)]
    pub id: i32,
    #[sea_orm(column_name = "userId")]
    pub user_id: i32,
    #[sea_orm(unique)]
    pub email: String,
    #[sea_orm(column_name = "emailVerified")]
    pub email_verified: Option<DateTime>,
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
    #[sea_orm(has_many = "super::verification_token::Entity")]
    VerificationTokens,
    #[sea_orm(has_many = "super::event_type::Entity")]
    EventTypes,
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::User.def()
    }
}

impl Related<super::verification_token::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::VerificationTokens.def()
    }
}

impl Related<super::event_type::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::EventTypes.def()
    }
}

impl ActiveModelBehavior for ActiveModel {}
