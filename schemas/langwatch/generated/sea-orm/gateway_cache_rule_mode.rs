use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "GatewayCacheRuleMode"
)]
pub enum GatewayCacheRuleMode {
    #[sea_orm(string_value = "RESPECT")]
    Respect,
    #[sea_orm(string_value = "FORCE")]
    Force,
    #[sea_orm(string_value = "DISABLE")]
    Disable,
}
