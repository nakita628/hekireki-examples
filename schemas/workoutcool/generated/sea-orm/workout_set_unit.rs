use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "WorkoutSetUnit")]
pub enum WorkoutSetUnit {
    #[sea_orm(string_value = "kg")]
    Kg,
    #[sea_orm(string_value = "lbs")]
    Lbs,
}
