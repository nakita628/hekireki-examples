use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "BaseDataDbMoveJobState"
)]
pub enum BaseDataDbMoveJobState {
    #[sea_orm(string_value = "pending")]
    Pending,
    #[sea_orm(string_value = "waiting_worker")]
    WaitingWorker,
    #[sea_orm(string_value = "copying_base_schema")]
    CopyingBaseSchema,
    #[sea_orm(string_value = "copying_shared_rows")]
    CopyingSharedRows,
    #[sea_orm(string_value = "validating")]
    Validating,
    #[sea_orm(string_value = "switching")]
    Switching,
    #[sea_orm(string_value = "succeeded")]
    Succeeded,
    #[sea_orm(string_value = "failed")]
    Failed,
    #[sea_orm(string_value = "cancelled")]
    Cancelled,
}
