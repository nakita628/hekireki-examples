use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "CostReferenceType")]
pub enum CostReferenceType {
    #[sea_orm(string_value = "CHECK")]
    Check,
    #[sea_orm(string_value = "TRACE")]
    Trace,
    #[sea_orm(string_value = "PROJECT")]
    Project,
    #[sea_orm(string_value = "BATCH")]
    Batch,
}
