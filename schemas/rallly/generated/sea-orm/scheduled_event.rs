use super::scheduled_event_status::ScheduledEventStatus;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "scheduled_events")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub user_id: String,
    pub space_id: String,
    pub event_type_id: Option<String>,
    #[sea_orm(unique)]
    pub sheet_slot_id: Option<String>,
    pub title: String,
    pub description: Option<String>,
    pub location: Option<Json>,
    pub conferencing: Option<Json>,
    pub capacity: Option<i32>,
    pub created_at: DateTime,
    pub updated_at: DateTime,
    #[sea_orm(default_value = "confirmed")]
    pub status: ScheduledEventStatus,
    pub time_zone: Option<String>,
    pub start: DateTime,
    pub end: DateTime,
    #[sea_orm(default_value = false)]
    pub all_day: bool,
    pub deleted_at: Option<DateTime>,
    #[sea_orm(default_value = 0)]
    pub sequence: i32,
    #[sea_orm(unique)]
    pub uid: String,
    #[sea_orm(default_value = false)]
    pub hide_attendees: bool,
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
        belongs_to = "super::space::Entity",
        from = "Column::SpaceId",
        to = "super::space::Column::Id",
        on_delete = "Cascade"
    )]
    Space,
    #[sea_orm(
        belongs_to = "super::event_type::Entity",
        from = "Column::EventTypeId",
        to = "super::event_type::Column::Id",
        on_delete = "SetNull"
    )]
    EventType,
    #[sea_orm(
        belongs_to = "super::sheet_slot::Entity",
        from = "Column::SheetSlotId",
        to = "super::sheet_slot::Column::Id",
        on_delete = "SetNull"
    )]
    SheetSlot,
    #[sea_orm(has_many = "super::rescheduled_event_date::Entity")]
    RescheduledDates,
    #[sea_orm(has_many = "super::scheduled_event_invite::Entity")]
    Invites,
    #[sea_orm(has_many = "super::poll::Entity")]
    Polls,
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::User.def()
    }
}

impl Related<super::space::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Space.def()
    }
}

impl Related<super::event_type::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::EventType.def()
    }
}

impl Related<super::sheet_slot::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::SheetSlot.def()
    }
}

impl Related<super::rescheduled_event_date::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::RescheduledDates.def()
    }
}

impl Related<super::scheduled_event_invite::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Invites.def()
    }
}

impl Related<super::poll::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Polls.def()
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
