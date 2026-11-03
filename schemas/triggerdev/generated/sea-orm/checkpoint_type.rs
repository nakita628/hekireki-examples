use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "CheckpointType")]
pub enum CheckpointType {
    #[sea_orm(string_value = "DOCKER")]
    Docker,
    #[sea_orm(string_value = "KUBERNETES")]
    Kubernetes,
}
