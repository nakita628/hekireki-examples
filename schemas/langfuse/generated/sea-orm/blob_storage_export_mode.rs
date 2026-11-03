use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "BlobStorageExportMode"
)]
pub enum BlobStorageExportMode {
    #[sea_orm(string_value = "FULL_HISTORY")]
    FullHistory,
    #[sea_orm(string_value = "FROM_TODAY")]
    FromToday,
    #[sea_orm(string_value = "FROM_CUSTOM_DATE")]
    FromCustomDate,
}
