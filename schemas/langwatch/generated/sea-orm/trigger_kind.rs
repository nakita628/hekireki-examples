use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "TriggerKind")]
pub enum TriggerKind {
    #[sea_orm(string_value = "AUTOMATION")]
    Automation,
    #[sea_orm(string_value = "ALERT")]
    Alert,
    #[sea_orm(string_value = "REPORT")]
    Report,
}
