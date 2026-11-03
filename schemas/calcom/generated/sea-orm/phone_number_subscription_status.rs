use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "PhoneNumberSubscriptionStatus"
)]
pub enum PhoneNumberSubscriptionStatus {
    #[sea_orm(string_value = "ACTIVE")]
    Active,
    #[sea_orm(string_value = "PAST_DUE")]
    PastDue,
    #[sea_orm(string_value = "CANCELLED")]
    Cancelled,
    #[sea_orm(string_value = "INCOMPLETE")]
    Incomplete,
    #[sea_orm(string_value = "INCOMPLETE_EXPIRED")]
    IncompleteExpired,
    #[sea_orm(string_value = "TRIALING")]
    Trialing,
    #[sea_orm(string_value = "UNPAID")]
    Unpaid,
}
