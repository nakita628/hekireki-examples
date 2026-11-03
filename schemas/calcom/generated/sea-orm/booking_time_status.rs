use super::booking_status::BookingStatus;
use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "BookingTimeStatus")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(unique)]
    pub id: i32,
    pub uid: Option<String>,
    #[sea_orm(column_name = "eventTypeId")]
    pub event_type_id: Option<i32>,
    pub title: Option<String>,
    pub description: Option<String>,
    #[sea_orm(column_name = "startTime")]
    pub start_time: Option<DateTime>,
    #[sea_orm(column_name = "endTime")]
    pub end_time: Option<DateTime>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: Option<DateTime>,
    pub location: Option<String>,
    pub paid: Option<bool>,
    pub status: Option<BookingStatus>,
    pub rescheduled: Option<bool>,
    #[sea_orm(column_name = "userId")]
    pub user_id: Option<i32>,
    #[sea_orm(column_name = "teamId")]
    pub team_id: Option<i32>,
    #[sea_orm(column_name = "eventLength")]
    pub event_length: Option<i32>,
    #[sea_orm(column_name = "timeStatus")]
    pub time_status: Option<String>,
    #[sea_orm(column_name = "eventParentId")]
    pub event_parent_id: Option<i32>,
    #[sea_orm(column_name = "userEmail")]
    pub user_email: Option<String>,
    pub username: Option<String>,
    #[sea_orm(column_name = "ratingFeedback")]
    pub rating_feedback: Option<String>,
    pub rating: Option<i32>,
    #[sea_orm(column_name = "noShowHost")]
    pub no_show_host: Option<bool>,
    #[sea_orm(column_name = "isTeamBooking")]
    pub is_team_booking: bool,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {}

impl ActiveModelBehavior for ActiveModel {}
