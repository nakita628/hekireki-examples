use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "WorkerDeploymentStatus"
)]
pub enum WorkerDeploymentStatus {
    #[sea_orm(string_value = "PENDING")]
    Pending,
    #[sea_orm(string_value = "INSTALLING")]
    Installing,
    #[sea_orm(string_value = "BUILDING")]
    Building,
    #[sea_orm(string_value = "DEPLOYING")]
    Deploying,
    #[sea_orm(string_value = "DEPLOYED")]
    Deployed,
    #[sea_orm(string_value = "FAILED")]
    Failed,
    #[sea_orm(string_value = "CANCELED")]
    Canceled,
    #[sea_orm(string_value = "TIMED_OUT")]
    TimedOut,
}
