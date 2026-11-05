use super::booking_status::BookingStatus;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Booking")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "guestName")]
    pub guest_name: String,
    #[sea_orm(column_name = "guestEmail")]
    pub guest_email: String,
    #[sea_orm(column_name = "guestNote")]
    pub guest_note: Option<String>,
    #[sea_orm(column_name = "startTime")]
    pub start_time: DateTime,
    #[sea_orm(column_name = "endTime")]
    pub end_time: DateTime,
    #[sea_orm(default_value = "PENDING_PROVIDER_EVENT")]
    pub status: BookingStatus,
    pub provider: Option<String>,
    #[sea_orm(column_name = "providerConnectionId")]
    pub provider_connection_id: Option<String>,
    #[sea_orm(column_name = "providerCalendarId")]
    pub provider_calendar_id: Option<String>,
    #[sea_orm(column_name = "providerEventId")]
    pub provider_event_id: Option<String>,
    #[sea_orm(column_name = "videoConferenceLink")]
    pub video_conference_link: Option<String>,
    #[sea_orm(column_name = "cancelTokenHash")]
    pub cancel_token_hash: String,
    #[sea_orm(column_name = "cancellationReason")]
    pub cancellation_reason: Option<String>,
    #[sea_orm(column_name = "idempotencyToken")]
    pub idempotency_token: Option<String>,
    #[sea_orm(column_name = "bookingLinkId")]
    pub booking_link_id: String,
    #[sea_orm(column_name = "emailAccountId")]
    pub email_account_id: String,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::booking_link::Entity",
        from = "Column::BookingLinkId",
        to = "super::booking_link::Column::Id",
        on_delete = "Cascade"
    )]
    BookingLink,
    #[sea_orm(
        belongs_to = "super::email_account::Entity",
        from = "Column::EmailAccountId",
        to = "super::email_account::Column::Id",
        on_delete = "Cascade"
    )]
    EmailAccount,
}

impl Related<super::booking_link::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::BookingLink.def()
    }
}

impl Related<super::email_account::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::EmailAccount.def()
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
