use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "PlatformNotificationScope"
)]
pub enum PlatformNotificationScope {
    #[sea_orm(string_value = "USER")]
    User,
    #[sea_orm(string_value = "PROJECT")]
    Project,
    #[sea_orm(string_value = "ORGANIZATION")]
    Organization,
    #[sea_orm(string_value = "GLOBAL")]
    Global,
}
