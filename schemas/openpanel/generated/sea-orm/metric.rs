use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "Metric")]
pub enum Metric {
    #[sea_orm(string_value = "sum")]
    Sum,
    #[sea_orm(string_value = "average")]
    Average,
    #[sea_orm(string_value = "min")]
    Min,
    #[sea_orm(string_value = "max")]
    Max,
    #[sea_orm(string_value = "count")]
    Count,
}
