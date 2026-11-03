use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "SpaceDataDbMigrationJobState"
)]
pub enum SpaceDataDbMigrationJobState {
    #[sea_orm(string_value = "pending")]
    Pending,
    #[sea_orm(string_value = "waiting_worker")]
    WaitingWorker,
    #[sea_orm(string_value = "preflight")]
    Preflight,
    #[sea_orm(string_value = "freezing_writes")]
    FreezingWrites,
    #[sea_orm(string_value = "copying")]
    Copying,
    #[sea_orm(string_value = "validating")]
    Validating,
    #[sea_orm(string_value = "switching")]
    Switching,
    #[sea_orm(string_value = "succeeded")]
    Succeeded,
    #[sea_orm(string_value = "failed")]
    Failed,
    #[sea_orm(string_value = "canceled")]
    Canceled,
    #[sea_orm(string_value = "rolled_back")]
    RolledBack,
}
