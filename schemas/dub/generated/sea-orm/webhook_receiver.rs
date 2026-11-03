use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "WebhookReceiver")]
pub enum WebhookReceiver {
    #[sea_orm(string_value = "user")]
    User,
    #[sea_orm(string_value = "zapier")]
    Zapier,
    #[sea_orm(string_value = "make")]
    Make,
    #[sea_orm(string_value = "slack")]
    Slack,
    #[sea_orm(string_value = "segment")]
    Segment,
}
