use super::payment_option::PaymentOption;
use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Payment")]
pub struct Model {
    #[sea_orm(primary_key)]
    pub id: i32,
    #[sea_orm(unique)]
    pub uid: String,
    #[sea_orm(column_name = "appId")]
    pub app_id: Option<String>,
    #[sea_orm(column_name = "bookingId")]
    pub booking_id: i32,
    pub amount: i32,
    pub fee: i32,
    pub currency: String,
    pub success: bool,
    pub refunded: bool,
    pub data: Json,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "externalId")]
    pub external_id: String,
    #[sea_orm(column_name = "paymentOption", default_value = "ON_BOOKING")]
    pub payment_option: Option<PaymentOption>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::app::Entity",
        from = "Column::AppId",
        to = "super::app::Column::Slug",
        on_delete = "Cascade"
    )]
    App,
    #[sea_orm(
        belongs_to = "super::booking::Entity",
        from = "Column::BookingId",
        to = "super::booking::Column::Id",
        on_delete = "Cascade"
    )]
    Booking,
}

impl Related<super::app::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::App.def()
    }
}

impl Related<super::booking::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Booking.def()
    }
}

impl ActiveModelBehavior for ActiveModel {}
