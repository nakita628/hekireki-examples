use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "RuntimeEnvironmentType"
)]
pub enum RuntimeEnvironmentType {
    #[sea_orm(string_value = "PRODUCTION")]
    Production,
    #[sea_orm(string_value = "STAGING")]
    Staging,
    #[sea_orm(string_value = "DEVELOPMENT")]
    Development,
    #[sea_orm(string_value = "PREVIEW")]
    Preview,
}
