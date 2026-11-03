use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "ErrorGroupStatus")]
pub enum ErrorGroupStatus {
    #[sea_orm(string_value = "UNRESOLVED")]
    Unresolved,
    #[sea_orm(string_value = "RESOLVED")]
    Resolved,
    #[sea_orm(string_value = "IGNORED")]
    Ignored,
}
