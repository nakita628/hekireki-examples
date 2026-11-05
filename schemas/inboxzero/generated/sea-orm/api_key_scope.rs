use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "ApiKeyScope")]
pub enum ApiKeyScope {
    #[sea_orm(string_value = "STATS_READ")]
    StatsRead,
    #[sea_orm(string_value = "RULES_READ")]
    RulesRead,
    #[sea_orm(string_value = "RULES_WRITE")]
    RulesWrite,
    #[sea_orm(string_value = "SETTINGS_READ")]
    SettingsRead,
    #[sea_orm(string_value = "SETTINGS_WRITE")]
    SettingsWrite,
    #[sea_orm(string_value = "ASSISTANT_CHAT")]
    AssistantChat,
    #[sea_orm(string_value = "SENDERS_UNSUBSCRIBE")]
    SendersUnsubscribe,
}
