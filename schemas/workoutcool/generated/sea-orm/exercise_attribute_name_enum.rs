use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "ExerciseAttributeNameEnum"
)]
pub enum ExerciseAttributeNameEnum {
    #[sea_orm(string_value = "TYPE")]
    Type,
    #[sea_orm(string_value = "PRIMARY_MUSCLE")]
    PrimaryMuscle,
    #[sea_orm(string_value = "SECONDARY_MUSCLE")]
    SecondaryMuscle,
    #[sea_orm(string_value = "EQUIPMENT")]
    Equipment,
    #[sea_orm(string_value = "MECHANICS_TYPE")]
    MechanicsType,
}
