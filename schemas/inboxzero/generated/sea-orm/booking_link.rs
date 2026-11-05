use super::booking_link_location_type::BookingLinkLocationType;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "BookingLink")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(unique)]
    pub slug: String,
    pub title: String,
    pub description: Option<String>,
    #[sea_orm(column_name = "isActive", default_value = true)]
    pub is_active: bool,
    #[sea_orm(column_name = "durationMinutes")]
    pub duration_minutes: i32,
    #[sea_orm(column_name = "slotIntervalMinutes")]
    pub slot_interval_minutes: i32,
    #[sea_orm(column_name = "locationType", default_value = "CUSTOM")]
    pub location_type: BookingLinkLocationType,
    #[sea_orm(column_name = "locationValue")]
    pub location_value: Option<String>,
    #[sea_orm(column_name = "minimumNoticeMinutes", default_value = 120)]
    pub minimum_notice_minutes: i32,
    #[sea_orm(column_name = "maxDaysAhead", default_value = 90)]
    pub max_days_ahead: i32,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "emailAccountId")]
    pub email_account_id: String,
    #[sea_orm(column_name = "destinationCalendarId")]
    pub destination_calendar_id: Option<String>,
    #[sea_orm(column_name = "availabilityScheduleId")]
    pub availability_schedule_id: String,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::email_account::Entity",
        from = "Column::EmailAccountId",
        to = "super::email_account::Column::Id",
        on_delete = "Cascade"
    )]
    EmailAccount,
    #[sea_orm(
        belongs_to = "super::calendar::Entity",
        from = "Column::DestinationCalendarId",
        to = "super::calendar::Column::Id",
        on_delete = "SetNull"
    )]
    DestinationCalendar,
    #[sea_orm(
        belongs_to = "super::availability_schedule::Entity",
        from = "(Column::AvailabilityScheduleId, Column::EmailAccountId)",
        to = "(super::availability_schedule::Column::Id, super::availability_schedule::Column::EmailAccountId)",
        on_delete = "NoAction"
    )]
    AvailabilitySchedule,
    #[sea_orm(has_many = "super::booking::Entity")]
    Bookings,
}

impl Related<super::email_account::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::EmailAccount.def()
    }
}

impl Related<super::calendar::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::DestinationCalendar.def()
    }
}

impl Related<super::availability_schedule::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::AvailabilitySchedule.def()
    }
}

impl Related<super::booking::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Bookings.def()
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
