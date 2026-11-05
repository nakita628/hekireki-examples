use super::mobile_push_platform::MobilePushPlatform;
use super::mobile_push_token_type::MobilePushTokenType;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "MobilePushToken")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(unique)]
    pub token: String,
    pub platform: MobilePushPlatform,
    #[sea_orm(column_name = "tokenType", default_value = "expo")]
    pub token_type: MobilePushTokenType,
    #[sea_orm(column_name = "appVersion")]
    pub app_version: Option<String>,
    #[sea_orm(column_name = "userId")]
    pub user_id: String,
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
    #[sea_orm(has_many = "super::mobile_push_delivery::Entity")]
    MobilePushDeliveries,
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::User.def()
    }
}

impl Related<super::mobile_push_delivery::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::MobilePushDeliveries.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.created_at.is_not_set() {
            self.created_at = Set(now.naive_utc());
        }
        if !self.updated_at.is_set() {
            self.updated_at = Set(now.naive_utc());
        }
        Ok(self)
    }
}
