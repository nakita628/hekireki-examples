use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "AttachmentSourceType"
)]
pub enum AttachmentSourceType {
    #[sea_orm(string_value = "FILE")]
    File,
    #[sea_orm(string_value = "FOLDER")]
    Folder,
}
