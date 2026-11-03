use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "RetentionRunKind")]
pub enum RetentionRunKind {
    #[sea_orm(string_value = "sweep")]
    Sweep,
    #[sea_orm(string_value = "purge")]
    Purge,
}
