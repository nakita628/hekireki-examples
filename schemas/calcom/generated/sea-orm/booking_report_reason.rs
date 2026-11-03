use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "BookingReportReason"
)]
pub enum BookingReportReason {
    #[sea_orm(string_value = "SPAM")]
    Spam,
    #[sea_orm(string_value = "dont_know_person")]
    DontKnowPerson,
    #[sea_orm(string_value = "OTHER")]
    Other,
}
