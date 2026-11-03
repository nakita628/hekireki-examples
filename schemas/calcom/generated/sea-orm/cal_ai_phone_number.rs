use super::phone_number_subscription_status::PhoneNumberSubscriptionStatus;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "CalAiPhoneNumber")]
pub struct Model {
    #[sea_orm(primary_key)]
    pub id: i32,
    #[sea_orm(column_name = "userId")]
    pub user_id: Option<i32>,
    #[sea_orm(column_name = "teamId")]
    pub team_id: Option<i32>,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "phoneNumber")]
    pub phone_number: String,
    pub provider: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "providerPhoneNumberId")]
    pub provider_phone_number_id: Option<String>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "stripeCustomerId")]
    pub stripe_customer_id: Option<String>,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "stripeSubscriptionId")]
    pub stripe_subscription_id: Option<String>,
    #[sea_orm(column_name = "subscriptionStatus")]
    pub subscription_status: Option<PhoneNumberSubscriptionStatus>,
    #[sea_orm(column_name = "inboundAgentId")]
    pub inbound_agent_id: Option<String>,
    #[sea_orm(column_name = "outboundAgentId")]
    pub outbound_agent_id: Option<String>,
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
    #[sea_orm(
        belongs_to = "super::team::Entity",
        from = "Column::TeamId",
        to = "super::team::Column::Id",
        on_delete = "Cascade"
    )]
    Team,
    #[sea_orm(
        belongs_to = "super::agent::Entity",
        from = "Column::InboundAgentId",
        to = "super::agent::Column::Id",
        on_delete = "SetNull"
    )]
    InboundAgent,
    #[sea_orm(
        belongs_to = "super::agent::Entity",
        from = "Column::OutboundAgentId",
        to = "super::agent::Column::Id",
        on_delete = "SetNull"
    )]
    OutboundAgent,
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::User.def()
    }
}

impl Related<super::team::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Team.def()
    }
}

impl Related<super::agent::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::InboundAgent.def()
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
