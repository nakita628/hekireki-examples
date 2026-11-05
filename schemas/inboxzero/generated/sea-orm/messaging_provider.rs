use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "MessagingProvider")]
pub enum MessagingProvider {
    #[sea_orm(string_value = "SLACK")]
    Slack,
    #[sea_orm(string_value = "TEAMS")]
    Teams,
    #[sea_orm(string_value = "TELEGRAM")]
    Telegram,
}
