use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "SignupIntent")]
pub enum SignupIntent {
    #[sea_orm(string_value = "AI_ASSISTANT")]
    AiAssistant,
    #[sea_orm(string_value = "MAIL_CLIENT")]
    MailClient,
    #[sea_orm(string_value = "BOTH")]
    Both,
}
