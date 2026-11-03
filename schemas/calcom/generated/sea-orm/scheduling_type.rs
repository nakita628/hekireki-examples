use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "SchedulingType")]
pub enum SchedulingType {
    #[sea_orm(string_value = "roundRobin")]
    RoundRobin,
    #[sea_orm(string_value = "collective")]
    Collective,
    #[sea_orm(string_value = "managed")]
    Managed,
}
