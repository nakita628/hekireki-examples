use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "CheckpointRestoreEventType"
)]
pub enum CheckpointRestoreEventType {
    #[sea_orm(string_value = "CHECKPOINT")]
    Checkpoint,
    #[sea_orm(string_value = "RESTORE")]
    Restore,
}
