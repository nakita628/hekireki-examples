use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "DashboardWidgetChartType"
)]
pub enum DashboardWidgetChartType {
    #[sea_orm(string_value = "LINE_TIME_SERIES")]
    LineTimeSeries,
    #[sea_orm(string_value = "AREA_TIME_SERIES")]
    AreaTimeSeries,
    #[sea_orm(string_value = "BAR_TIME_SERIES")]
    BarTimeSeries,
    #[sea_orm(string_value = "HORIZONTAL_BAR")]
    HorizontalBar,
    #[sea_orm(string_value = "VERTICAL_BAR")]
    VerticalBar,
    #[sea_orm(string_value = "PIE")]
    Pie,
    #[sea_orm(string_value = "NUMBER")]
    Number,
    #[sea_orm(string_value = "HISTOGRAM")]
    Histogram,
    #[sea_orm(string_value = "PIVOT_TABLE")]
    PivotTable,
}
