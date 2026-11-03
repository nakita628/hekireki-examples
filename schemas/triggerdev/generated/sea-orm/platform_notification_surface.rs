use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "PlatformNotificationSurface"
)]
pub enum PlatformNotificationSurface {
    #[sea_orm(string_value = "WEBAPP")]
    Webapp,
    #[sea_orm(string_value = "CLI")]
    Cli,
}
