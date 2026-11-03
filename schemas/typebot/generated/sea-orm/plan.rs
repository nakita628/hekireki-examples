use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "Plan")]
pub enum Plan {
    #[sea_orm(string_value = "FREE")]
    Free,
    #[sea_orm(string_value = "STARTER")]
    Starter,
    #[sea_orm(string_value = "PRO")]
    Pro,
    #[sea_orm(string_value = "LIFETIME")]
    Lifetime,
    #[sea_orm(string_value = "OFFERED")]
    Offered,
    #[sea_orm(string_value = "CUSTOM")]
    Custom,
    #[sea_orm(string_value = "UNLIMITED")]
    Unlimited,
    #[sea_orm(string_value = "ENTERPRISE")]
    Enterprise,
}
