use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "MediaAssociationOrigin"
)]
pub enum MediaAssociationOrigin {
    #[sea_orm(string_value = "UNKNOWN")]
    Unknown,
    #[sea_orm(string_value = "CLIENT_UPLOAD")]
    ClientUpload,
    #[sea_orm(string_value = "INGESTION_MEDIA_EXTRACTION")]
    IngestionMediaExtraction,
    #[sea_orm(string_value = "INGESTION_FIELD_OVERFLOW")]
    IngestionFieldOverflow,
}
