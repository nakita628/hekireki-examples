use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "WebhookDeliveryOutcome"
)]
pub enum WebhookDeliveryOutcome {
    #[sea_orm(string_value = "success")]
    Success,
    #[sea_orm(string_value = "retryable")]
    Retryable,
    #[sea_orm(string_value = "terminal")]
    Terminal,
    #[sea_orm(string_value = "pending")]
    Pending,
}
