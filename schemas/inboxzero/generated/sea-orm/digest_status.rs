use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "DigestStatus")]
pub enum DigestStatus {
    #[sea_orm(string_value = "PENDING")]
    Pending,
    #[sea_orm(string_value = "PROCESSING")]
    Processing,
    #[sea_orm(string_value = "SENT")]
    Sent,
    #[sea_orm(string_value = "FAILED")]
    Failed,
}
