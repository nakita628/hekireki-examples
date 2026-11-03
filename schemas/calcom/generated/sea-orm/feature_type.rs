use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "FeatureType")]
pub enum FeatureType {
    #[sea_orm(string_value = "RELEASE")]
    Release,
    #[sea_orm(string_value = "EXPERIMENT")]
    Experiment,
    #[sea_orm(string_value = "OPERATIONAL")]
    Operational,
    #[sea_orm(string_value = "KILL_SWITCH")]
    KillSwitch,
    #[sea_orm(string_value = "PERMISSION")]
    Permission,
}
