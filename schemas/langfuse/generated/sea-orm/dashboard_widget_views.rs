use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "DashboardWidgetViews"
)]
pub enum DashboardWidgetViews {
    #[sea_orm(string_value = "TRACES")]
    Traces,
    #[sea_orm(string_value = "OBSERVATIONS")]
    Observations,
    #[sea_orm(string_value = "SCORES_NUMERIC")]
    ScoresNumeric,
    #[sea_orm(string_value = "SCORES_CATEGORICAL")]
    ScoresCategorical,
    #[sea_orm(string_value = "SCORES_BOOLEAN")]
    ScoresBoolean,
}
