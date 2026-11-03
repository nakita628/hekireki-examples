use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "WidgetPlacement")]
pub enum WidgetPlacement {
    #[sea_orm(string_value = "bottomLeft")]
    Bottomleft,
    #[sea_orm(string_value = "bottomRight")]
    Bottomright,
    #[sea_orm(string_value = "topLeft")]
    Topleft,
    #[sea_orm(string_value = "topRight")]
    Topright,
    #[sea_orm(string_value = "center")]
    Center,
}
