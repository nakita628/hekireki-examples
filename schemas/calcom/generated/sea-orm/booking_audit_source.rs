use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "BookingAuditSource")]
pub enum BookingAuditSource {
    #[sea_orm(string_value = "api_v1")]
    ApiV1,
    #[sea_orm(string_value = "api_v2")]
    ApiV2,
    #[sea_orm(string_value = "webapp")]
    Webapp,
    #[sea_orm(string_value = "webhook")]
    Webhook,
    #[sea_orm(string_value = "system")]
    System,
    #[sea_orm(string_value = "magic_link")]
    MagicLink,
    #[sea_orm(string_value = "unknown")]
    Unknown,
}
