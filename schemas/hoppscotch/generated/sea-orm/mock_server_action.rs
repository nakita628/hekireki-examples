use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "MockServerAction")]
pub enum MockServerAction {
    #[sea_orm(string_value = "CREATED")]
    Created,
    #[sea_orm(string_value = "DELETED")]
    Deleted,
    #[sea_orm(string_value = "ACTIVATED")]
    Activated,
    #[sea_orm(string_value = "DEACTIVATED")]
    Deactivated,
}
