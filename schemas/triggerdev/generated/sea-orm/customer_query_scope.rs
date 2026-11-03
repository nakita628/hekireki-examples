use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "CustomerQueryScope")]
pub enum CustomerQueryScope {
    #[sea_orm(string_value = "ORGANIZATION")]
    Organization,
    #[sea_orm(string_value = "PROJECT")]
    Project,
    #[sea_orm(string_value = "ENVIRONMENT")]
    Environment,
}
