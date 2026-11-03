use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "GraphNavigation")]
pub enum GraphNavigation {
    #[sea_orm(string_value = "MOUSE")]
    Mouse,
    #[sea_orm(string_value = "TRACKPAD")]
    Trackpad,
}
