use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Attendee")]
pub struct Model {
    #[sea_orm(primary_key)]
    pub id: i32,
    pub email: String,
    pub name: String,
    #[sea_orm(column_name = "timeZone")]
    pub time_zone: String,
    #[sea_orm(column_name = "phoneNumber")]
    pub phone_number: Option<String>,
    #[sea_orm(default_value = "en")]
    pub locale: Option<String>,
    #[sea_orm(column_name = "bookingId")]
    pub booking_id: Option<i32>,
    #[sea_orm(column_name = "noShow", default_value = false)]
    pub no_show: Option<bool>,
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
    #[sea_orm(has_one = "super::booking_seat::Entity")]
    BookingSeat,
}

impl Related<super::booking::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Booking.def()
    }
}

impl Related<super::booking_seat::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::BookingSeat.def()
    }
}

impl ActiveModelBehavior for ActiveModel {}
