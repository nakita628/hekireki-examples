use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "ViewMode")]
pub enum ViewMode {
    #[sea_orm(string_value = "DEFAULT")]
    Default,
    #[sea_orm(string_value = "ZEN")]
    Zen,
}
