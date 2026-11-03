use super::chat_provider::ChatProvider;
use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "RuntimeMediaIdCache")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub provider: ChatProvider,
    #[sea_orm(primary_key, auto_increment = false)]
    pub url: String,
    #[sea_orm(column_name = "mediaId")]
    pub media_id: String,
    #[sea_orm(column_name = "expiresAt")]
    pub expires_at: Option<DateTime>,
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_name = "publicTypebotId")]
    pub public_typebot_id: String,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::public_typebot::Entity",
        from = "Column::PublicTypebotId",
        to = "super::public_typebot::Column::Id",
        on_delete = "Cascade"
    )]
    PublicTypebot,
}

impl Related<super::public_typebot::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::PublicTypebot.def()
    }
}

impl ActiveModelBehavior for ActiveModel {}
