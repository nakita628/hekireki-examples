use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "ProjectAlertChannelType"
)]
pub enum ProjectAlertChannelType {
    #[sea_orm(string_value = "EMAIL")]
    Email,
    #[sea_orm(string_value = "SLACK")]
    Slack,
    #[sea_orm(string_value = "WEBHOOK")]
    Webhook,
}
