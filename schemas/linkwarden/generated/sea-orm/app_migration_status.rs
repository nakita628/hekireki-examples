use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "AppMigrationStatus")]
pub enum AppMigrationStatus {
    #[sea_orm(string_value = "APPLIED")]
    Applied,
    #[sea_orm(string_value = "PENDING")]
    Pending,
    #[sea_orm(string_value = "FAILED")]
    Failed,
}
