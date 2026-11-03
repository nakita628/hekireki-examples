use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "BookingSeat")]
pub struct Model {
    #[sea_orm(primary_key)]
    pub id: i32,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "referenceUid")]
    pub reference_uid: String,
    #[sea_orm(column_name = "bookingId")]
    pub booking_id: i32,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "attendeeId")]
    pub attendee_id: i32,
    pub data: Option<Json>,
    pub metadata: Option<Json>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::booking::Entity",
        from = "Column::BookingId",
        to = "super::booking::Column::Id",
        on_delete = "Cascade"
    )]
    Booking,
    #[sea_orm(
        belongs_to = "super::attendee::Entity",
        from = "Column::AttendeeId",
        to = "super::attendee::Column::Id",
        on_delete = "Cascade"
    )]
    Attendee,
}

impl Related<super::booking::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Booking.def()
    }
}

impl Related<super::attendee::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Attendee.def()
    }
}

impl ActiveModelBehavior for ActiveModel {}
