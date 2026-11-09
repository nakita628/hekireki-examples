use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "reverse_sync_extraction"
)]
pub enum reverse_sync_extraction {
    #[sea_orm(string_value = "cursor")]
    Cursor,
    #[sea_orm(string_value = "full")]
    Full,
}
