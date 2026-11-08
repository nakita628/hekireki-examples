use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "WorkoutSetType")]
pub enum WorkoutSetType {
    #[sea_orm(string_value = "TIME")]
    Time,
    #[sea_orm(string_value = "WEIGHT")]
    Weight,
    #[sea_orm(string_value = "REPS")]
    Reps,
    #[sea_orm(string_value = "BODYWEIGHT")]
    Bodyweight,
    #[sea_orm(string_value = "NA")]
    Na,
}
