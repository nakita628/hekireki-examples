use super::messaging_route_purpose::MessagingRoutePurpose;
use super::messaging_route_target_type::MessagingRouteTargetType;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "MessagingRoute")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    pub purpose: MessagingRoutePurpose,
    #[sea_orm(column_name = "targetType")]
    pub target_type: MessagingRouteTargetType,
    #[sea_orm(column_name = "targetId")]
    pub target_id: String,
    #[sea_orm(column_name = "messagingChannelId")]
    pub messaging_channel_id: String,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::messaging_channel::Entity",
        from = "Column::MessagingChannelId",
        to = "super::messaging_channel::Column::Id",
        on_delete = "Cascade"
    )]
    MessagingChannel,
}

impl Related<super::messaging_channel::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::MessagingChannel.def()
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
