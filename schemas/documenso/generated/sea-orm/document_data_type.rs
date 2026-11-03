use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "DocumentDataType")]
pub enum DocumentDataType {
    #[sea_orm(string_value = "S3_PATH")]
    S3Path,
    #[sea_orm(string_value = "BYTES")]
    Bytes,
    #[sea_orm(string_value = "BYTES_64")]
    Bytes64,
}
