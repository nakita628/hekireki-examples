use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "BlobStorageIntegrationType"
)]
pub enum BlobStorageIntegrationType {
    #[sea_orm(string_value = "S3")]
    S3,
    #[sea_orm(string_value = "S3_COMPATIBLE")]
    S3Compatible,
    #[sea_orm(string_value = "AZURE_BLOB_STORAGE")]
    AzureBlobStorage,
    #[sea_orm(string_value = "GOOGLE_CLOUD_STORAGE")]
    GoogleCloudStorage,
}
