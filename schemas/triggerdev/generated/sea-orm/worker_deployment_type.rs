use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "WorkerDeploymentType"
)]
pub enum WorkerDeploymentType {
    #[sea_orm(string_value = "MANAGED")]
    Managed,
    #[sea_orm(string_value = "UNMANAGED")]
    Unmanaged,
    #[sea_orm(string_value = "V1")]
    V1,
}
