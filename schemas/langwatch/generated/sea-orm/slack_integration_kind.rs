use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "SlackIntegrationKind"
)]
pub enum SlackIntegrationKind {
    #[sea_orm(string_value = "BOT")]
    Bot,
    #[sea_orm(string_value = "INCOMING_WEBHOOK")]
    IncomingWebhook,
}
