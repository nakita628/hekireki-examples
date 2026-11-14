use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "OrganizationUserRole"
)]
pub enum OrganizationUserRole {
    #[sea_orm(string_value = "ADMIN")]
    Admin,
    #[sea_orm(string_value = "MEMBER")]
    Member,
    #[sea_orm(string_value = "EXTERNAL")]
    External,
    #[sea_orm(string_value = "DEVELOPER")]
    Developer,
}
