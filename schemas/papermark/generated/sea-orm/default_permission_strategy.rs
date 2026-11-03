use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "DefaultPermissionStrategy"
)]
pub enum DefaultPermissionStrategy {
    #[sea_orm(string_value = "INHERIT_FROM_PARENT")]
    InheritFromParent,
    #[sea_orm(string_value = "ASK_EVERY_TIME")]
    AskEveryTime,
    #[sea_orm(string_value = "HIDDEN_BY_DEFAULT")]
    HiddenByDefault,
}
