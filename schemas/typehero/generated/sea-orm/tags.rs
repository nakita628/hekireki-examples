use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "Tags")]
pub enum Tags {
    #[sea_orm(string_value = "POPULAR")]
    Popular,
    #[sea_orm(string_value = "NEWEST")]
    Newest,
}
