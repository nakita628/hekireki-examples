use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "GrantPrincipalType")]
pub enum GrantPrincipalType {
    #[sea_orm(string_value = "USER")]
    User,
    #[sea_orm(string_value = "API_KEY")]
    ApiKey,
    #[sea_orm(string_value = "GROUP")]
    Group,
    #[sea_orm(string_value = "TEAM")]
    Team,
    #[sea_orm(string_value = "ORGANIZATION")]
    Organization,
    #[sea_orm(string_value = "PROJECT")]
    Project,
    #[sea_orm(string_value = "ANYONE")]
    Anyone,
}
