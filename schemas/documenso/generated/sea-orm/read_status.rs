use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "ReadStatus")]
pub enum ReadStatus {
    #[sea_orm(string_value = "NOT_OPENED")]
    NotOpened,
    #[sea_orm(string_value = "OPENED")]
    Opened,
}
