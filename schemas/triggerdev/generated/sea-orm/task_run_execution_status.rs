use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "TaskRunExecutionStatus"
)]
pub enum TaskRunExecutionStatus {
    #[sea_orm(string_value = "RUN_CREATED")]
    RunCreated,
    #[sea_orm(string_value = "DELAYED")]
    Delayed,
    #[sea_orm(string_value = "QUEUED")]
    Queued,
    #[sea_orm(string_value = "QUEUED_EXECUTING")]
    QueuedExecuting,
    #[sea_orm(string_value = "PENDING_EXECUTING")]
    PendingExecuting,
    #[sea_orm(string_value = "EXECUTING")]
    Executing,
    #[sea_orm(string_value = "EXECUTING_WITH_WAITPOINTS")]
    ExecutingWithWaitpoints,
    #[sea_orm(string_value = "SUSPENDED")]
    Suspended,
    #[sea_orm(string_value = "PENDING_CANCEL")]
    PendingCancel,
    #[sea_orm(string_value = "FINISHED")]
    Finished,
}
