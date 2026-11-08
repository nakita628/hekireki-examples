use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "Difficulty")]
pub enum Difficulty {
    #[sea_orm(string_value = "BEGINNER")]
    Beginner,
    #[sea_orm(string_value = "EASY")]
    Easy,
    #[sea_orm(string_value = "MEDIUM")]
    Medium,
    #[sea_orm(string_value = "HARD")]
    Hard,
    #[sea_orm(string_value = "EXTREME")]
    Extreme,
    #[sea_orm(string_value = "EVENT")]
    Event,
}
