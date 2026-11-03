use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "SupportAccessMode")]
pub enum SupportAccessMode {
    #[sea_orm(string_value = "ALLOW")]
    Allow,
    #[sea_orm(string_value = "REQUIRES_REQUEST")]
    RequiresRequest,
}
