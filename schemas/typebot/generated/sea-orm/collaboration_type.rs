use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "CollaborationType")]
pub enum CollaborationType {
    #[sea_orm(string_value = "READ")]
    Read,
    #[sea_orm(string_value = "WRITE")]
    Write,
    #[sea_orm(string_value = "FULL_ACCESS")]
    FullAccess,
}
