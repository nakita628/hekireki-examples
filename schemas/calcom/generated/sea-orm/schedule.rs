use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Schedule")]
pub struct Model {
    #[sea_orm(primary_key)]
    pub id: i32,
    #[sea_orm(column_name = "userId")]
    pub user_id: i32,
    pub name: String,
    #[sea_orm(column_name = "timeZone")]
    pub time_zone: Option<String>,
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
        has_many = "super::event_type::Entity",
        from = "Column::Id",
        to = "super::event_type::Column::ScheduleId"
    )]
    EventType,
    #[sea_orm(
        has_many = "super::event_type::Entity",
        from = "Column::Id",
        to = "super::event_type::Column::InstantMeetingScheduleId"
    )]
    InstantMeetingEvents,
    #[sea_orm(
        has_many = "super::event_type::Entity",
        from = "Column::Id",
        to = "super::event_type::Column::RestrictionScheduleId"
    )]
    RestrictionSchedule,
    #[sea_orm(has_many = "super::availability::Entity")]
    Availability,
    #[sea_orm(has_many = "super::host::Entity")]
    Host,
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::User.def()
    }
}

impl Related<super::event_type::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::EventType.def()
    }
}

impl Related<super::availability::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Availability.def()
    }
}

impl Related<super::host::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Host.def()
    }
}

impl ActiveModelBehavior for ActiveModel {}
