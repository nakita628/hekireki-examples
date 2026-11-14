use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "SsoConnectionMigrationPhase"
)]
pub enum SsoConnectionMigrationPhase {
    #[sea_orm(string_value = "SETUP")]
    Setup,
    #[sea_orm(string_value = "GRACE_LEGACY")]
    GraceLegacy,
    #[sea_orm(string_value = "GRACE_DIRECT")]
    GraceDirect,
    #[sea_orm(string_value = "FINALIZING")]
    Finalizing,
    #[sea_orm(string_value = "FINALIZED")]
    Finalized,
}
