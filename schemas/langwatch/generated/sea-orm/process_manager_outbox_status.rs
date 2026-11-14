use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "ProcessManagerOutboxStatus"
)]
pub enum ProcessManagerOutboxStatus {
    #[sea_orm(string_value = "pending")]
    Pending,
    #[sea_orm(string_value = "dispatched")]
    Dispatched,
    #[sea_orm(string_value = "dead")]
    Dead,
    #[sea_orm(string_value = "discarded")]
    Discarded,
}
