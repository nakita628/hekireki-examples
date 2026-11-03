use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "WebhookScheduledTriggers")]
pub struct Model {
    #[sea_orm(primary_key)]
    pub id: i32,
    #[sea_orm(column_name = "jobName")]
    pub job_name: Option<String>,
    #[sea_orm(column_name = "subscriberUrl")]
    pub subscriber_url: String,
    pub payload: String,
    #[sea_orm(column_name = "startAfter")]
    pub start_after: DateTime,
    #[sea_orm(column_name = "retryCount", default_value = 0)]
    pub retry_count: i32,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: Option<DateTime>,
    #[sea_orm(column_name = "appId")]
    pub app_id: Option<String>,
    #[sea_orm(column_name = "webhookId")]
    pub webhook_id: Option<String>,
    #[sea_orm(column_name = "bookingId")]
    pub booking_id: Option<i32>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::webhook::Entity",
        from = "Column::WebhookId",
        to = "super::webhook::Column::Id",
        on_delete = "Cascade"
    )]
    Webhook,
    #[sea_orm(
        belongs_to = "super::booking::Entity",
        from = "Column::BookingId",
        to = "super::booking::Column::Id",
        on_delete = "Cascade"
    )]
    Booking,
}

impl Related<super::webhook::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Webhook.def()
    }
}

impl Related<super::booking::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Booking.def()
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
            self.created_at = Set(Some(now.naive_utc()));
        }
        Ok(self)
    }
}
