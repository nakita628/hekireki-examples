use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "WorkflowStepType")]
pub enum WorkflowStepType {
    #[sea_orm(string_value = "TRIGGER")]
    Trigger,
    #[sea_orm(string_value = "SEND_EMAIL")]
    SendEmail,
    #[sea_orm(string_value = "DELAY")]
    Delay,
    #[sea_orm(string_value = "WAIT_FOR_EVENT")]
    WaitForEvent,
    #[sea_orm(string_value = "CONDITION")]
    Condition,
    #[sea_orm(string_value = "EXIT")]
    Exit,
    #[sea_orm(string_value = "WEBHOOK")]
    Webhook,
    #[sea_orm(string_value = "UPDATE_CONTACT")]
    UpdateContact,
}
