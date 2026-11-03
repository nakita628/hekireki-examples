use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "queued_email_status"
)]
pub enum QueuedEmailStatus {
    #[sea_orm(string_value = "pending")]
    Pending,
    #[sea_orm(string_value = "sent")]
    Sent,
    #[sea_orm(string_value = "skipped")]
    Skipped,
    #[sea_orm(string_value = "failed")]
    Failed,
}
