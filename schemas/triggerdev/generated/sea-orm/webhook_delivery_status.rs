use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "WebhookDeliveryStatus"
)]
pub enum WebhookDeliveryStatus {
    #[sea_orm(string_value = "PENDING")]
    Pending,
    #[sea_orm(string_value = "PROCESSING")]
    Processing,
    #[sea_orm(string_value = "SUCCEEDED")]
    Succeeded,
    #[sea_orm(string_value = "FAILED")]
    Failed,
    #[sea_orm(string_value = "FILTERED")]
    Filtered,
    #[sea_orm(string_value = "UNMATCHED")]
    Unmatched,
}
