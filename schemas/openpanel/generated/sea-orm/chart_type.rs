use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "ChartType")]
pub enum ChartType {
    #[sea_orm(string_value = "linear")]
    Linear,
    #[sea_orm(string_value = "bar")]
    Bar,
    #[sea_orm(string_value = "histogram")]
    Histogram,
    #[sea_orm(string_value = "pie")]
    Pie,
    #[sea_orm(string_value = "metric")]
    Metric,
    #[sea_orm(string_value = "area")]
    Area,
    #[sea_orm(string_value = "map")]
    Map,
    #[sea_orm(string_value = "funnel")]
    Funnel,
    #[sea_orm(string_value = "retention")]
    Retention,
    #[sea_orm(string_value = "conversion")]
    Conversion,
    #[sea_orm(string_value = "sankey")]
    Sankey,
}
