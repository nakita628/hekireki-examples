use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "RequestClient")]
pub enum RequestClient {
    #[sea_orm(string_value = "API")]
    Api,
    #[sea_orm(string_value = "MCP")]
    Mcp,
    #[sea_orm(string_value = "SELF_HOSTING")]
    SelfHosting,
    #[sea_orm(string_value = "WEB")]
    Web,
}
