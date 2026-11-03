use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "RRTimestampBasis")]
pub enum RRTimestampBasis {
    #[sea_orm(string_value = "CREATED_AT")]
    CreatedAt,
    #[sea_orm(string_value = "START_TIME")]
    StartTime,
}
