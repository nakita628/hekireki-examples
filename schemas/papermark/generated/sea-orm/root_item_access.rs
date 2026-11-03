use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "RootItemAccess")]
pub enum RootItemAccess {
    #[sea_orm(string_value = "VIEW_ONLY")]
    ViewOnly,
    #[sea_orm(string_value = "VIEW_AND_DOWNLOAD")]
    ViewAndDownload,
    #[sea_orm(string_value = "HIDDEN")]
    Hidden,
}
