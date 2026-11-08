use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "EmailStatus")]
pub enum EmailStatus {
    #[sea_orm(string_value = "SCHEDULED")]
    Scheduled,
    #[sea_orm(string_value = "CANCELLED")]
    Cancelled,
    #[sea_orm(string_value = "PENDING")]
    Pending,
    #[sea_orm(string_value = "SENDING")]
    Sending,
    #[sea_orm(string_value = "SENT")]
    Sent,
    #[sea_orm(string_value = "DELIVERED")]
    Delivered,
    #[sea_orm(string_value = "RECEIVED")]
    Received,
    #[sea_orm(string_value = "OPENED")]
    Opened,
    #[sea_orm(string_value = "CLICKED")]
    Clicked,
    #[sea_orm(string_value = "BOUNCED")]
    Bounced,
    #[sea_orm(string_value = "COMPLAINED")]
    Complained,
    #[sea_orm(string_value = "FAILED")]
    Failed,
}
