use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "MessagingMessageStatus"
)]
pub enum MessagingMessageStatus {
    #[sea_orm(string_value = "SENT")]
    Sent,
    #[sea_orm(string_value = "DRAFT_SENT")]
    DraftSent,
    #[sea_orm(string_value = "DRAFT_EDITED")]
    DraftEdited,
    #[sea_orm(string_value = "DISMISSED")]
    Dismissed,
    #[sea_orm(string_value = "EXPIRED")]
    Expired,
    #[sea_orm(string_value = "FAILED")]
    Failed,
}
