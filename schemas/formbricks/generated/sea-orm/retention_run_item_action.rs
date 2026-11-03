use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "RetentionRunItemAction"
)]
pub enum RetentionRunItemAction {
    #[sea_orm(string_value = "notified")]
    Notified,
    #[sea_orm(string_value = "archived")]
    Archived,
    #[sea_orm(string_value = "deactivated")]
    Deactivated,
    #[sea_orm(string_value = "deleted")]
    Deleted,
    #[sea_orm(string_value = "skipped")]
    Skipped,
}
