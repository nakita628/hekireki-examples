use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "AuditActorType")]
pub enum AuditActorType {
    #[sea_orm(string_value = "user")]
    User,
    #[sea_orm(string_value = "guest")]
    Guest,
    #[sea_orm(string_value = "attendee")]
    Attendee,
    #[sea_orm(string_value = "system")]
    System,
    #[sea_orm(string_value = "app")]
    App,
}
