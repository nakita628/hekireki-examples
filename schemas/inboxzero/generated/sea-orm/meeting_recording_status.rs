use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "MeetingRecordingStatus"
)]
pub enum MeetingRecordingStatus {
    #[sea_orm(string_value = "PENDING")]
    Pending,
    #[sea_orm(string_value = "SCHEDULED")]
    Scheduled,
    #[sea_orm(string_value = "CANCELLING")]
    Cancelling,
    #[sea_orm(string_value = "JOINING")]
    Joining,
    #[sea_orm(string_value = "IN_WAITING_ROOM")]
    InWaitingRoom,
    #[sea_orm(string_value = "IN_CALL")]
    InCall,
    #[sea_orm(string_value = "RECORDING")]
    Recording,
    #[sea_orm(string_value = "CALL_ENDED")]
    CallEnded,
    #[sea_orm(string_value = "DONE")]
    Done,
    #[sea_orm(string_value = "FAILED")]
    Failed,
    #[sea_orm(string_value = "CANCELLED")]
    Cancelled,
}
