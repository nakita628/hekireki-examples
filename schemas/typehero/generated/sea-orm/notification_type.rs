use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "NotificationType")]
pub enum NotificationType {
    #[sea_orm(string_value = "LIKE")]
    Like,
    #[sea_orm(string_value = "REPLY")]
    Reply,
    #[sea_orm(string_value = "MENTION")]
    Mention,
    #[sea_orm(string_value = "MISC")]
    Misc,
    #[sea_orm(string_value = "SYSTEM")]
    System,
}
