use super::calendar_cache_event_status::CalendarCacheEventStatus;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "CalendarCacheEvent")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "selectedCalendarId")]
    pub selected_calendar_id: String,
    #[sea_orm(column_name = "externalId")]
    pub external_id: String,
    #[sea_orm(column_name = "externalEtag")]
    pub external_etag: String,
    #[sea_orm(column_name = "iCalUID")]
    pub i_cal_uid: Option<String>,
    #[sea_orm(column_name = "iCalSequence", default_value = 0)]
    pub i_cal_sequence: i32,
    pub summary: Option<String>,
    pub description: Option<String>,
    pub location: Option<String>,
    pub start: DateTime,
    pub end: DateTime,
    #[sea_orm(column_name = "isAllDay", default_value = false)]
    pub is_all_day: bool,
    #[sea_orm(column_name = "timeZone")]
    pub time_zone: Option<String>,
    #[sea_orm(default_value = "confirmed")]
    pub status: CalendarCacheEventStatus,
    #[sea_orm(column_name = "recurringEventId")]
    pub recurring_event_id: Option<String>,
    #[sea_orm(column_name = "originalStartTime")]
    pub original_start_time: Option<DateTime>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "externalCreatedAt")]
    pub external_created_at: Option<DateTime>,
    #[sea_orm(column_name = "externalUpdatedAt")]
    pub external_updated_at: Option<DateTime>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::selected_calendar::Entity",
        from = "Column::SelectedCalendarId",
        to = "super::selected_calendar::Column::Id",
        on_delete = "Cascade"
    )]
    SelectedCalendar,
}

impl Related<super::selected_calendar::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::SelectedCalendar.def()
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
