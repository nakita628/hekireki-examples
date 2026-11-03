use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "VerificationToken")]
pub struct Model {
    #[sea_orm(column_name = "deviceIdentifier")]
    pub device_identifier: String,
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(unique)]
    pub token: String,
    #[sea_orm(column_name = "userUid")]
    pub user_uid: String,
    #[sea_orm(column_name = "expiresOn", column_type = "TimestampWithTimeZone")]
    pub expires_on: DateTimeWithTimeZone,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::UserUid",
        to = "super::user::Column::Uid",
        on_delete = "Cascade"
    )]
    User,
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::User.def()
    }
}

impl ActiveModelBehavior for ActiveModel {}
