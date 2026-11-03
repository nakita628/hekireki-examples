use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "TaskTriggerSource")]
pub enum TaskTriggerSource {
    #[sea_orm(string_value = "STANDARD")]
    Standard,
    #[sea_orm(string_value = "SCHEDULED")]
    Scheduled,
    #[sea_orm(string_value = "AGENT")]
    Agent,
    #[sea_orm(string_value = "WEBHOOK")]
    Webhook,
}
