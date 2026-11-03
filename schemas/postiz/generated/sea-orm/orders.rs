use super::order_status::OrderStatus;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Orders")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "buyerId")]
    pub buyer_id: String,
    #[sea_orm(column_name = "sellerId")]
    pub seller_id: String,
    pub status: OrderStatus,
    #[sea_orm(column_name = "messageGroupId")]
    pub message_group_id: String,
    #[sea_orm(column_name = "captureId")]
    pub capture_id: Option<String>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::BuyerId",
        to = "super::user::Column::Id"
    )]
    Buyer,
    #[sea_orm(
        belongs_to = "super::messages_group::Entity",
        from = "Column::MessageGroupId",
        to = "super::messages_group::Column::Id"
    )]
    MessageGroup,
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::SellerId",
        to = "super::user::Column::Id"
    )]
    Seller,
    #[sea_orm(has_many = "super::order_items::Entity")]
    OrdersItems,
    #[sea_orm(has_many = "super::payout_problems::Entity")]
    PayoutProblems,
    #[sea_orm(has_many = "super::post::Entity")]
    Posts,
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Buyer.def()
    }
}

impl Related<super::messages_group::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::MessageGroup.def()
    }
}

impl Related<super::order_items::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::OrdersItems.def()
    }
}

impl Related<super::payout_problems::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::PayoutProblems.def()
    }
}

impl Related<super::post::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Posts.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    fn new() -> Self {
        Self {
            id: Set(uuid::Uuid::new_v4().to_string()),
            ..ActiveModelTrait::default()
        }
    }

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
