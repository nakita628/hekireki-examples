use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "CreativeDeviceType")]
pub enum CreativeDeviceType {
    #[sea_orm(string_value = "phone")]
    Phone,
    #[sea_orm(string_value = "tablet")]
    Tablet,
    #[sea_orm(string_value = "desktop")]
    Desktop,
}
