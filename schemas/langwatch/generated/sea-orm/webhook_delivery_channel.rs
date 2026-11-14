use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "WebhookDeliveryChannel"
)]
pub enum WebhookDeliveryChannel {
    #[sea_orm(string_value = "platform")]
    Platform,
    #[sea_orm(string_value = "automations")]
    Automations,
}
