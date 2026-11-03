use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "OrganizationRole")]
pub enum OrganizationRole {
    #[sea_orm(string_value = "owner")]
    Owner,
    #[sea_orm(string_value = "manager")]
    Manager,
    #[sea_orm(string_value = "member")]
    Member,
    #[sea_orm(string_value = "billing")]
    Billing,
}
