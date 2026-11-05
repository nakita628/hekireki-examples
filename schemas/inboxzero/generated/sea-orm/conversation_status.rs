use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "ConversationStatus")]
pub enum ConversationStatus {
    #[sea_orm(string_value = "ACTIVE")]
    Active,
    #[sea_orm(string_value = "STOPPED")]
    Stopped,
}
