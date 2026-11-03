use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "TaskRunStatus")]
pub enum TaskRunStatus {
    #[sea_orm(string_value = "DELAYED")]
    Delayed,
    #[sea_orm(string_value = "PENDING")]
    Pending,
    #[sea_orm(string_value = "PENDING_VERSION")]
    PendingVersion,
    #[sea_orm(string_value = "WAITING_FOR_DEPLOY")]
    WaitingForDeploy,
    #[sea_orm(string_value = "DEQUEUED")]
    Dequeued,
    #[sea_orm(string_value = "EXECUTING")]
    Executing,
    #[sea_orm(string_value = "WAITING_TO_RESUME")]
    WaitingToResume,
    #[sea_orm(string_value = "RETRYING_AFTER_FAILURE")]
    RetryingAfterFailure,
    #[sea_orm(string_value = "PAUSED")]
    Paused,
    #[sea_orm(string_value = "CANCELED")]
    Canceled,
    #[sea_orm(string_value = "INTERRUPTED")]
    Interrupted,
    #[sea_orm(string_value = "COMPLETED_SUCCESSFULLY")]
    CompletedSuccessfully,
    #[sea_orm(string_value = "COMPLETED_WITH_ERRORS")]
    CompletedWithErrors,
    #[sea_orm(string_value = "SYSTEM_FAILURE")]
    SystemFailure,
    #[sea_orm(string_value = "CRASHED")]
    Crashed,
    #[sea_orm(string_value = "EXPIRED")]
    Expired,
    #[sea_orm(string_value = "TIMED_OUT")]
    TimedOut,
}
