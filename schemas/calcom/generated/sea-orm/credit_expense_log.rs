use super::credit_type::CreditType;
use super::credit_usage_type::CreditUsageType;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "CreditExpenseLog")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "creditBalanceId")]
    pub credit_balance_id: String,
    #[sea_orm(column_name = "bookingUid")]
    pub booking_uid: Option<String>,
    pub credits: Option<i32>,
    #[sea_orm(column_name = "creditType")]
    pub credit_type: CreditType,
    pub date: DateTime,
    #[sea_orm(column_name = "smsSid")]
    pub sms_sid: Option<String>,
    #[sea_orm(column_name = "smsSegments")]
    pub sms_segments: Option<i32>,
    #[sea_orm(column_name = "phoneNumber")]
    pub phone_number: Option<String>,
    pub email: Option<String>,
    #[sea_orm(column_name = "callDuration")]
    pub call_duration: Option<i32>,
    #[sea_orm(column_name = "creditFor")]
    pub credit_for: Option<CreditUsageType>,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "externalRef")]
    pub external_ref: Option<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::credit_balance::Entity",
        from = "Column::CreditBalanceId",
        to = "super::credit_balance::Column::Id",
        on_delete = "Cascade"
    )]
    CreditBalance,
    #[sea_orm(
        belongs_to = "super::booking::Entity",
        from = "Column::BookingUid",
        to = "super::booking::Column::Uid",
        on_delete = "Cascade"
    )]
    Booking,
}

impl Related<super::credit_balance::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::CreditBalance.def()
    }
}

impl Related<super::booking::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Booking.def()
    }
}

impl ActiveModelBehavior for ActiveModel {
    fn new() -> Self {
        Self {
            id: Set(uuid::Uuid::new_v4().to_string()),
            ..ActiveModelTrait::default()
        }
    }
}
