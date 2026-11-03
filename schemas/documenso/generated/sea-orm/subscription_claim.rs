use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "SubscriptionClaim")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    pub name: String,
    #[sea_orm(default_value = false)]
    pub locked: bool,
    #[sea_orm(column_name = "teamCount")]
    pub team_count: i32,
    #[sea_orm(column_name = "memberCount")]
    pub member_count: i32,
    #[sea_orm(column_name = "envelopeItemCount")]
    pub envelope_item_count: i32,
    #[sea_orm(column_name = "recipientCount")]
    pub recipient_count: i32,
    pub flags: Json,
    #[sea_orm(column_name = "documentRateLimits")]
    pub document_rate_limits: Json,
    #[sea_orm(column_name = "documentQuota")]
    pub document_quota: Option<i32>,
    #[sea_orm(column_name = "emailRateLimits")]
    pub email_rate_limits: Json,
    #[sea_orm(column_name = "emailQuota")]
    pub email_quota: Option<i32>,
    #[sea_orm(column_name = "apiRateLimits")]
    pub api_rate_limits: Json,
    #[sea_orm(column_name = "apiQuota")]
    pub api_quota: Option<i32>,
    #[sea_orm(column_name = "emailTransportId")]
    pub email_transport_id: Option<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::email_transport::Entity",
        from = "Column::EmailTransportId",
        to = "super::email_transport::Column::Id",
        on_delete = "SetNull"
    )]
    EmailTransport,
}

impl Related<super::email_transport::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::EmailTransport.def()
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
