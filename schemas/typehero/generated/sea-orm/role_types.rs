use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "RoleTypes")]
pub enum RoleTypes {
    #[sea_orm(string_value = "USER")]
    User,
    #[sea_orm(string_value = "ADMIN")]
    Admin,
    #[sea_orm(string_value = "MODERATOR")]
    Moderator,
    #[sea_orm(string_value = "CREATOR")]
    Creator,
    #[sea_orm(string_value = "SUPPORTER")]
    Supporter,
    #[sea_orm(string_value = "CONTRIBUTOR")]
    Contributor,
}
