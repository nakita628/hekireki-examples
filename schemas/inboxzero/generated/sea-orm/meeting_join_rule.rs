use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "MeetingJoinRule")]
pub enum MeetingJoinRule {
    #[sea_orm(string_value = "ALL")]
    All,
    #[sea_orm(string_value = "EXTERNAL_ONLY")]
    ExternalOnly,
    #[sea_orm(string_value = "HOST_ONLY")]
    HostOnly,
    #[sea_orm(string_value = "OFF")]
    Off,
}
