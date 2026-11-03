use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "BlobStorageIntegrationFileType"
)]
pub enum BlobStorageIntegrationFileType {
    #[sea_orm(string_value = "JSON")]
    Json,
    #[sea_orm(string_value = "CSV")]
    Csv,
    #[sea_orm(string_value = "JSONL")]
    Jsonl,
    #[sea_orm(string_value = "PARQUET")]
    Parquet,
}
