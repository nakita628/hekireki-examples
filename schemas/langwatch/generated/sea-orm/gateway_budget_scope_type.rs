use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "GatewayBudgetScopeType"
)]
pub enum GatewayBudgetScopeType {
    #[sea_orm(string_value = "ORGANIZATION")]
    Organization,
    #[sea_orm(string_value = "TEAM")]
    Team,
    #[sea_orm(string_value = "PROJECT")]
    Project,
    #[sea_orm(string_value = "VIRTUAL_KEY")]
    VirtualKey,
    #[sea_orm(string_value = "PRINCIPAL")]
    Principal,
    #[sea_orm(string_value = "GROUP")]
    Group,
    #[sea_orm(string_value = "ATTRIBUTED_USER")]
    AttributedUser,
}
