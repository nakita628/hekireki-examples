use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "WorkerInstanceGroupType"
)]
pub enum WorkerInstanceGroupType {
    #[sea_orm(string_value = "MANAGED")]
    Managed,
    #[sea_orm(string_value = "UNMANAGED")]
    Unmanaged,
}
