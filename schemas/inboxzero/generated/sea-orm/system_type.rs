use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "SystemType")]
pub enum SystemType {
    #[sea_orm(string_value = "TO_REPLY")]
    ToReply,
    #[sea_orm(string_value = "FYI")]
    Fyi,
    #[sea_orm(string_value = "AWAITING_REPLY")]
    AwaitingReply,
    #[sea_orm(string_value = "ACTIONED")]
    Actioned,
    #[sea_orm(string_value = "COLD_EMAIL")]
    ColdEmail,
    #[sea_orm(string_value = "NEWSLETTER")]
    Newsletter,
    #[sea_orm(string_value = "MARKETING")]
    Marketing,
    #[sea_orm(string_value = "CALENDAR")]
    Calendar,
    #[sea_orm(string_value = "RECEIPT")]
    Receipt,
    #[sea_orm(string_value = "NOTIFICATION")]
    Notification,
    #[sea_orm(string_value = "OTP")]
    Otp,
}
