use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "StripeConnectMode")]
pub enum StripeConnectMode {
    #[sea_orm(string_value = "live")]
    Live,
    #[sea_orm(string_value = "test")]
    Test,
    #[sea_orm(string_value = "sandbox")]
    Sandbox,
}
