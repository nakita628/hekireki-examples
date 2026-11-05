use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "McpAuthType")]
pub enum McpAuthType {
    #[sea_orm(string_value = "OAUTH")]
    Oauth,
    #[sea_orm(string_value = "API_TOKEN")]
    ApiToken,
    #[sea_orm(string_value = "NONE")]
    None,
}
