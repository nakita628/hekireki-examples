use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "webhook_delivery_status"
)]
pub enum WebhookDeliveryStatus {
    #[sea_orm(string_value = "pending")]
    Pending,
    #[sea_orm(string_value = "in_flight")]
    InFlight,
    #[sea_orm(string_value = "succeeded")]
    Succeeded,
    #[sea_orm(string_value = "failed")]
    Failed,
    #[sea_orm(string_value = "exhausted")]
    Exhausted,
}
