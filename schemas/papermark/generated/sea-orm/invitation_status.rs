use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "InvitationStatus")]
pub enum InvitationStatus {
    #[sea_orm(string_value = "SENT")]
    Sent,
    #[sea_orm(string_value = "FAILED")]
    Failed,
    #[sea_orm(string_value = "BOUNCED")]
    Bounced,
}
