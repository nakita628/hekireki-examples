use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "WorkflowTriggerType"
)]
pub enum WorkflowTriggerType {
    #[sea_orm(string_value = "EVENT")]
    Event,
    #[sea_orm(string_value = "MANUAL")]
    Manual,
    #[sea_orm(string_value = "SCHEDULE")]
    Schedule,
}
