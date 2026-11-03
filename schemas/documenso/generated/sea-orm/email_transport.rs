use super::email_transport_type::EmailTransportType;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "EmailTransport")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    pub name: String,
    pub r#type: EmailTransportType,
    #[sea_orm(column_name = "fromName")]
    pub from_name: String,
    #[sea_orm(column_name = "fromAddress")]
    pub from_address: String,
    pub config: String,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(has_many = "super::subscription_claim::Entity")]
    SubscriptionClaims,
    #[sea_orm(has_many = "super::organisation_claim::Entity")]
    OrganisationClaims,
}

impl Related<super::subscription_claim::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::SubscriptionClaims.def()
    }
}

impl Related<super::organisation_claim::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::OrganisationClaims.def()
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
