use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "BulkActionType")]
pub enum BulkActionType {
    #[sea_orm(string_value = "CANCEL")]
    Cancel,
    #[sea_orm(string_value = "REPLAY")]
    Replay,
}
