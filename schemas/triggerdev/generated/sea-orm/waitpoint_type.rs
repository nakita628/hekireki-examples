use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "WaitpointType")]
pub enum WaitpointType {
    #[sea_orm(string_value = "RUN")]
    Run,
    #[sea_orm(string_value = "DATETIME")]
    Datetime,
    #[sea_orm(string_value = "MANUAL")]
    Manual,
    #[sea_orm(string_value = "BATCH")]
    Batch,
}
