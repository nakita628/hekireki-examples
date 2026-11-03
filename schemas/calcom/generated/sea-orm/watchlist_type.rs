use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "WatchlistType")]
pub enum WatchlistType {
    #[sea_orm(string_value = "EMAIL")]
    Email,
    #[sea_orm(string_value = "DOMAIN")]
    Domain,
    #[sea_orm(string_value = "USERNAME")]
    Username,
}
