use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "AuthMethod")]
pub enum AuthMethod {
    #[sea_orm(string_value = "PASSWORD")]
    Password,
    #[sea_orm(string_value = "GOOGLE_OAUTH")]
    GoogleOauth,
    #[sea_orm(string_value = "GITHUB_OAUTH")]
    GithubOauth,
}
