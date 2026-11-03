use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "ScoreConfigDataType"
)]
pub enum ScoreConfigDataType {
    #[sea_orm(string_value = "CATEGORICAL")]
    Categorical,
    #[sea_orm(string_value = "NUMERIC")]
    Numeric,
    #[sea_orm(string_value = "BOOLEAN")]
    Boolean,
    #[sea_orm(string_value = "TEXT")]
    Text,
}
