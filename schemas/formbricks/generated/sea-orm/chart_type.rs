use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "ChartType")]
pub enum ChartType {
    #[sea_orm(string_value = "area")]
    Area,
    #[sea_orm(string_value = "bar")]
    Bar,
    #[sea_orm(string_value = "pie")]
    Pie,
    #[sea_orm(string_value = "big_number")]
    BigNumber,
    #[sea_orm(string_value = "matrix")]
    Matrix,
}
