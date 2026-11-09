use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "reverse_sync_mode")]
pub enum reverse_sync_mode {
    #[sea_orm(string_value = "upsert")]
    Upsert,
    #[sea_orm(string_value = "mirror")]
    Mirror,
}
