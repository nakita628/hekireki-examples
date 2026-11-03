use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "DocumentVisibility")]
pub enum DocumentVisibility {
    #[sea_orm(string_value = "EVERYONE")]
    Everyone,
    #[sea_orm(string_value = "MANAGER_AND_ABOVE")]
    ManagerAndAbove,
    #[sea_orm(string_value = "ADMIN")]
    Admin,
}
