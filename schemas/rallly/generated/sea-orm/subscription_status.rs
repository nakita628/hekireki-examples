use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "subscription_status"
)]
pub enum SubscriptionStatus {
    #[sea_orm(string_value = "incomplete")]
    Incomplete,
    #[sea_orm(string_value = "incomplete_expired")]
    IncompleteExpired,
    #[sea_orm(string_value = "active")]
    Active,
    #[sea_orm(string_value = "paused")]
    Paused,
    #[sea_orm(string_value = "trialing")]
    Trialing,
    #[sea_orm(string_value = "past_due")]
    PastDue,
    #[sea_orm(string_value = "canceled")]
    Canceled,
    #[sea_orm(string_value = "unpaid")]
    Unpaid,
}
